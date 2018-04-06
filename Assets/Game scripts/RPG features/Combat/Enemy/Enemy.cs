using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

/// <summary>
/// Script to control enemy mob's behaviour, sounds and animations
/// </summary>

namespace RpgTools.Enemy
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Interactable))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(CharacterHealthsytem))]
    [RequireComponent(typeof(UI.TargetFrame))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] CharacterHealthsytem healthSystem;
        [SerializeField] AIConfig _enemyInfo;
        public Slider HealthBar;
        [SerializeField]
        private NavMeshAgent agent;

        public enum State
        {
            Idle,
            Chase,
            Attacking,
            Patrol,
            Dead,
            RunAway
        }

        public enum Aggresiveness
        {
            AGGRESIVE = 0,
            NONAGGRESIVE
        }

        [Tooltip("Specifies if an enemy should runaway with 50% hp & no be aggresive")] [SerializeField] bool isCritter;

        [SerializeField] PlayerClass.Player Player;
        [SerializeField] Hp_barPos canvas;

        [SerializeField] float rotSpeed = 2.5f;


        bool targetSeen = false;
        [SerializeField] float behind_detectionRange = 4f;
        [SerializeField] float outofrangeTimer;
        [SerializeField] float offsetSpawnPoint = 2f;
        [SerializeField] Transform spawnPoint;

        [SerializeField] Transform player;

        // animations
        private Animator anim;
        Animation Anim;

        //audio
        public AudioClip AttackSound;
        public AudioClip runSound;
        public AudioClip deathSound;
        public AudioClip walkSound;
        AudioSource Audio;

        //mob info
        float DeadCounter = 0;
        float DeadTime = 90f;

        //mob damage
        int Mindamage;
        int MaxDamage;
        int damage;

        float AttackcurTime = 0.0f;

        [SerializeField] int EnemyLevel;
        [SerializeField] int exptogive;
        float attackspeed;
        [SerializeField] float AttackRange;
        float movementspeed;
        float walkingspeed;
        float detectionRange;
        Texture2D enemyIcon;
        string toolTip;

        //way point patrol
        [Tooltip("Specifies if an enemy should chase when withing certain range or wait for the player to attack, before attacking")]
        public Aggresiveness agg;
        public State state = State.Idle;
        public Transform[] waypoints;
        int currentWP = 0;
        [Range(0.1f, 2f)]
        public float accuracyWP = .8f;
        [Range(0, 20)]
        [Tooltip("Time before you want an idling enemy to begin patrolling")]
        public float timeBeforeStateChange;
        public int Defence;
        public int Haste;
        public int Endurance;
        public int Resistance;
        public int Agility;
        public int Strength;
        public int Stamina;
        public int Intellect;

        [HideInInspector]
        public float MaxHealth;

        private float baseoutofrangeTimer;

        bool beenAttacked;

        public bool TargetSeen
        {
            get
            {
                return targetSeen;
            }

            set
            {
                targetSeen = value;
            }
        }

        public float OutofrangeTimer
        {
            get
            {
                return outofrangeTimer;
            }

            set
            {
                outofrangeTimer = value;
            }
        }

        // Use this for initialization
        void Start()
        {
            if (healthSystem == null)
            {
                healthSystem = transform.GetComponent<CharacterHealthsytem>();
            }

            //Finding the player component
            if (Player == null)
            {
                Player = FindObjectOfType<PlayerClass.Player>().GetComponent<PlayerClass.Player>();
                player = Player.transform;
            }
            if (canvas == null)
            {
                canvas = GetComponentInChildren<Hp_barPos>();
            }

            if (HealthBar == null && canvas != null)
            {
                HealthBar = GetComponentInChildren<Slider>();
            }

            EnemyPowerAndLevel();
            anim = GetComponent<Animator>();
            Audio = GetComponent<AudioSource>();

            this.GetComponent<CharacterController>().enabled = enabled;

            //Aiconfig variables
            if (_enemyInfo)
            {
                enemyIcon = _enemyInfo.EnemyIcon;
                toolTip = _enemyInfo.ToolTip;
                outofrangeTimer = _enemyInfo.OutofrangeTimer;
                walkingspeed = _enemyInfo.WalkingSpeed;
                AttackRange = _enemyInfo.AttackRange;
                EnemyLevel = _enemyInfo.EnemyLevel;
                attackspeed = _enemyInfo.AttackSpeed;
                movementspeed = _enemyInfo.MovementSpeed;
                detectionRange = _enemyInfo.DetectionRange;

                Defence = _enemyInfo.EnemyDefense;
                Haste = _enemyInfo.Haste;
                Endurance = _enemyInfo.Endurance;
                Resistance = _enemyInfo.EnemyResistance;
                Agility = _enemyInfo.Agility;
                Strength = _enemyInfo.Strength;
                Stamina = _enemyInfo.Stamina;
                Intellect = _enemyInfo.Intellect;
                Mindamage = _enemyInfo.MinAutoDamage;
                MaxDamage = _enemyInfo.MaxAutoDamage;
                MaxHealth = _enemyInfo.EnemyHP;
                

                HealthBar.value = MaxHealth;
                exptogive = _enemyInfo.Exptogive;

                //anim speed
                if (Anim != null)
                {
                    Anim["Run"].speed = _enemyInfo.MovementSpeed;
                    Anim["Walk"].speed = _enemyInfo.WalkingSpeed;
                    Anim["Attack"].speed = _enemyInfo.AttackSpeed;
                }

                if (agent == null)
                {
                    //not using navmesh yet
                    //agent = GetComponent<NavMeshAgent>();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

            if (Player.Health <= 0 && state != State.Dead)
            {
                state = State.Patrol;
                TargetSeen = false;
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                MoveToSpawn(true);
            }

            Chase();

            //Implentment into move to spawn script. So the target moves back away from the player before starting to patrol again
            if (Vector3.Distance(transform.position, player.position) > AttackRange * 1.25f && targetSeen)
            {
                if (state == State.Chase || state == State.RunAway || state == State.Attacking)
                {
                    //check if i'm out of dectectionRange and then count down
                    outofrangeTimer -= Time.deltaTime;
                    if (outofrangeTimer <= 0f)
                    {
                        beenAttacked = false;
                        TargetSeen = false;
                        // if target isnt seen or anything 
                        anim.SetBool("isIdle", false);
                        anim.SetBool("isRunning", false);
                        anim.SetBool("isAttacking", false);
                        anim.SetBool("isWalking", true);
                        anim.SetBool("isDead", false);
                        state = State.Patrol;
                        outofrangeTimer = _enemyInfo.OutofrangeTimer;
                    }
                }
            }

            if (waypoints.Length != 0 && state != State.RunAway)
            {
                Patrolling();
            }

            if (state == State.Idle)
            {
                Idle();
            }
            else
            {
                anim.SetBool("isIdle", false);
            }

            RunAway(isCritter);
        }

        void InRangeForAttack()
        {
            if (state != State.Dead && state != State.RunAway)
            {
                if (AttackcurTime < attackspeed && Vector3.Distance(transform.position, this.player.transform.position) < AttackRange)
                {
                    state = State.Attacking;
                    AttackcurTime += Time.deltaTime;
                    //count up
                    if (AttackcurTime >= attackspeed && Vector3.Distance(transform.position, this.player.transform.position) < AttackRange)
                    {
                        Attack();
                        AttackcurTime = 0;
                    }
                }
            }
        }

        void RunAway(bool isCritter)
        {
            if (isCritter == true)
            {
                /*if (healthSystem.CurrentHealth == healthSystem.MaxHealth / 2) // if 50% hp, run away from player
                {
                    state = State.RunAway;
                    //runaway as soon as enemy attacks
                    float furthestDistanceSoFar = 0;
                    Vector3 runPosition = Vector3.zero;
                    Vector3 direction = runPosition - transform.position;

                    //Check each point
                    foreach (Transform point in waypoints)
                    {
                        print(Vector3.Distance(player.position, point.position));
                        float currentCheckDistance = Vector3.Distance(player.position, point.position);
                        if (currentCheckDistance > furthestDistanceSoFar)
                        {
                            furthestDistanceSoFar = currentCheckDistance;
                            runPosition = point.position;
                        }
                    }
                    //Set the right destination for the furthest spot
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
                    transform.Translate(0 + Random.Range(-2, 2), 0, walkingspeed * Time.deltaTime);
                    //agent.SetDestination(runPosition); // for navmesh
                }*/
            }
        }

        void Patrolling()
        {
            // bug here, object not set to error, but everything works
            if (state != State.Dead && state != State.RunAway)
            {
                if (waypoints.Length != 0 && accuracyWP != 0)
                {

                    if (state == State.Patrol && waypoints.Length > 0)
                    {
                        anim.SetBool("isIdle", false);
                        anim.SetBool("isRunning", false);
                        anim.SetBool("isWalking", true);
                        anim.SetBool("isAttacking", false);
                        anim.SetBool("isDead", false);


                        if (Vector3.Distance(waypoints[currentWP].position, transform.position) < accuracyWP)
                        {
                            currentWP++;

                            if (currentWP >= waypoints.Length)
                            {
                                currentWP = 0;
                            }
                        }

                        //rotate and move towards waypoint
                        var targetPos = waypoints[currentWP].position;
                        Vector3 direction = targetPos - transform.position;
                        direction.y = 0;
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
                        transform.Translate(0, 0, walkingspeed * Time.deltaTime);
                        //agent.SetDestination(targetPos); // for navmesh

                        anim.SetFloat("walkingSpeed", walkingspeed * Time.deltaTime);
                    }
                    else if (waypoints.Length == 0)
                    {
                        state = State.Idle;
                    }
                }
            }
        }

        void Idle()
        {
            if (state != State.Dead)
            {
                if (state == State.Idle)
                {
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isDead", false);
                    TargetSeen = false;
                }
                else if (state == State.Chase)
                {
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isRunning", true);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isDead", false);
                }
                else if (state == State.Patrol)
                {
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isDead", false);
                }

                if (waypoints.Length > 0 && TargetSeen == false && state != State.Patrol)
                {
                    timeBeforeStateChange -= Time.deltaTime;

                    if (timeBeforeStateChange <= 0)
                    {
                        timeBeforeStateChange = 0;
                        if (waypoints != null)
                        {
                            state = State.Patrol;
                            Patrolling();
                            StartCoroutine(EnemySounds());
                        }
                    }

                }
            }
        }

        void IsEnemyDead()
        {

            if (this.healthSystem.CurrentHealth <= 0)
            {

                state = State.Dead;
                canvas.gameObject.SetActive(false);
                if (state == State.Dead && Audio.isPlaying == false)
                {
                    Audio.clip = deathSound;
                    Audio.PlayOneShot(deathSound);
                }

                TargetSeen = false;
                OutofrangeTimer = 0f;
                anim.SetBool("isIdle", false);
                anim.SetBool("isDead", true);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", false);
                movementspeed = 0f;
                walkingspeed = movementspeed;
                FindObjectOfType<Experience>().GainExp(exptogive);
                this.GetComponent<CharacterController>().enabled = !enabled;
                exptogive = 0;
                // make loot glow
                //loot gui

                if (DeadCounter < DeadTime)
                {
                    //count up
                    DeadCounter += Time.deltaTime;

                }
                else
                {
                    Destroy(transform.gameObject);
                    DeadCounter = 0;
                }
            }
        }

        void Chase()
        {
            if (isCritter || agg == Aggresiveness.NONAGGRESIVE)
            {
                //been attacked then chase
                if (beenAttacked == true)
                {
                    Vector3 direction = player.position - this.transform.position;
                    direction.y = 0;

                    float angle = Vector3.Angle(direction, this.transform.forward);
                    if (state != State.Dead && state != State.RunAway)
                    {
                        if (Vector3.Distance(transform.position, player.position) < detectionRange && angle < 30 || Vector3.Distance(transform.position, player.position) < behind_detectionRange)
                        {
                            TargetSeen = true;
                            state = State.Chase;

                            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
                            if (direction.magnitude > AttackRange)
                            {
                                //chase
                                TargetSeen = true;
                                this.transform.Translate(0, 0, movementspeed * Time.deltaTime);
                                anim.SetFloat("runningSpeed", movementspeed * Time.deltaTime);
                                anim.SetBool("isRunning", true);
                                anim.SetBool("isDead", false);
                                anim.SetBool("isIdle", false);
                                anim.SetBool("isAttacking", false);
                                anim.SetBool("isWalking", false);
                                anim.SetBool("isIdle", false);

                                StartCoroutine(EnemySounds());

                            }
                            else
                            {
                                //attack
                                state = State.Attacking;
                                InRangeForAttack();
                                anim.SetBool("isDead", false);
                                anim.SetBool("isAttacking", true);
                                anim.SetBool("isRunning", false);
                                anim.SetBool("isWalking", false);
                                TargetSeen = true;
                            }

                        }
                    }
                }
            }
            else
            {
                Vector3 direction = player.position - this.transform.position;
                direction.y = 0;

                float angle = Vector3.Angle(direction, this.transform.forward);
                if (state != State.Dead && state != State.RunAway && agg == Aggresiveness.AGGRESIVE)
                {
                    if (Vector3.Distance(transform.position, player.position) < detectionRange && angle < 30 || Vector3.Distance(transform.position, player.position) < behind_detectionRange)
                    {
                        TargetSeen = true;
                        state = State.Chase;

                        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
                        if (direction.magnitude > AttackRange)
                        {
                            //chase
                            TargetSeen = true;
                            this.transform.Translate(0, 0, movementspeed * Time.deltaTime);
                            anim.SetFloat("runningSpeed", movementspeed * Time.deltaTime);
                            anim.SetBool("isRunning", true);
                            anim.SetBool("isDead", false);
                            anim.SetBool("isIdle", false);
                            anim.SetBool("isAttacking", false);
                            anim.SetBool("isWalking", false);
                            anim.SetBool("isIdle", false);

                            StartCoroutine(EnemySounds());

                        }
                        else if (direction.magnitude <= AttackRange)
                        {
                            //attack
                            state = State.Attacking;
                            InRangeForAttack();
                            anim.SetBool("isDead", false);
                            anim.SetBool("isAttacking", true);
                            anim.SetBool("isRunning", false);
                            anim.SetBool("isWalking", false);
                            TargetSeen = true;
                        }

                    }
                }
            }
        }

        void MoveToSpawn(bool moveToSpawn)
        {
            if (moveToSpawn == true)
            {
                //dont attack and move away from
                Vector3 direction = spawnPoint.position - transform.position;
                direction.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
                transform.Translate(0, 0, movementspeed * Time.deltaTime);
            }

            if (Vector3.Distance(spawnPoint.position, transform.position) < Random.Range(-offsetSpawnPoint, offsetSpawnPoint))
            {
                moveToSpawn = false;
                int i = Random.Range(0, 2);
                if (i == 0)
                {
                    state = State.Idle;
                    Idle();
                }
                else if (i == 1)
                {
                    state = State.Patrol;
                    Patrolling();
                }
            }
        }

        private IEnumerator EnemySounds()
        {
            if (state != State.Dead)
            {
                if (state == State.Attacking && TargetSeen == true && Audio.isPlaying == false)
                {
                    Audio.clip = AttackSound;
                    Audio.volume = Random.Range(.8f, 1f);
                    Audio.pitch = Random.Range(.8f, 1f);
                    Audio.PlayOneShot(AttackSound);
                    yield return new WaitForSeconds(Audio.clip.length);
                }

                if (state == State.Chase && TargetSeen == true && Audio.isPlaying == false)
                {
                    Audio.clip = runSound;
                    Audio.volume = Random.Range(.55f, 1f);
                    Audio.pitch = Random.Range(.75f, 1f);
                    Audio.PlayOneShot(runSound);
                    yield return new WaitForSeconds(Audio.clip.length + (0.15f * Time.deltaTime));
                    //Audio.Stop(runSound);
                }

                if (state == State.Patrol && TargetSeen == true && Audio.isPlaying == false)
                {
                    Audio.clip = walkSound;
                    Audio.volume = Random.Range(.6f, 1f);
                    Audio.pitch = Random.Range(.8f, 1f);
                    Audio.PlayOneShot(walkSound);
                    yield return new WaitForSeconds(Audio.clip.length);
                    //Audio.Stop(runSound);
                }
            }
        }

        public void GetHit(int damage, PlayerClass.PlayerDmgTypes damageType)
        {
            if (healthSystem.IsDead == true)
            {
                //Is dead
                state = State.Dead;
                beenAttacked = false;
                IsEnemyDead();
            }
            else
            {
                beenAttacked = true;
                HealthBar.value = healthSystem.CalculateHealth();
                healthSystem.GetHit(damage);
            }
        }

        public void GetHealth(int HealthValue)
        {
            if (healthSystem.IsDead == true)
            {
                //Is dead
                state = State.Dead;
                beenAttacked = false;
                IsEnemyDead();
            }
            else
            {
                healthSystem.GetHealth(HealthValue);
            }
        }

        void EnemyPowerAndLevel()
        {
            //Check what class enemy is, give bonus to certain class specific skills or passivs
            if (_enemyInfo.mobRarity == EnemyTypes.MobRarity.RARE)
            {

                float rareMultiplier = (1.3f);

                //mulitiplier
                Defence = (int)rareMultiplier * (Defence + EnemyLevel);
                Haste = (int)rareMultiplier * (Haste + EnemyLevel);
                Endurance = (int)rareMultiplier * (Endurance + EnemyLevel);
                Resistance = (int)rareMultiplier * (Endurance + EnemyLevel);
                Agility = (int)rareMultiplier * (Endurance + EnemyLevel);
                Strength = (int)rareMultiplier * (Strength + EnemyLevel);
                Stamina = (int)rareMultiplier * (Stamina + EnemyLevel);
                Intellect = (int)rareMultiplier * (Intellect + EnemyLevel);


            }
            else if (_enemyInfo.mobRarity == EnemyTypes.MobRarity.COMMON)
            {
                float commonMultiplier = (1.0f);
                Defence = (int)commonMultiplier * (Defence + EnemyLevel);
                Haste = (int)commonMultiplier * (Haste + EnemyLevel);
                Endurance = (int)commonMultiplier * (Endurance + EnemyLevel);
                Resistance = (int)commonMultiplier * (Endurance + EnemyLevel);
                Agility = (int)commonMultiplier * (Endurance + EnemyLevel);
                Strength = (int)commonMultiplier * (Strength + EnemyLevel);
                Stamina = (int)commonMultiplier * (Stamina + EnemyLevel);
                Intellect = (int)commonMultiplier * (Intellect + EnemyLevel);

            }
            else if (_enemyInfo.mobRarity == EnemyTypes.MobRarity.EPIC)
            {
                float epicMultiplier = (1.5f);

                //mulitiplier
                Defence = (int)epicMultiplier * (Defence + EnemyLevel);
                Haste = (int)epicMultiplier * (Haste + EnemyLevel);
                Endurance = (int)epicMultiplier * (Endurance + EnemyLevel);
                Resistance = (int)epicMultiplier * (Endurance + EnemyLevel);
                Agility = (int)epicMultiplier * (Endurance + EnemyLevel);
                Strength = (int)epicMultiplier * (Strength + EnemyLevel);
                Stamina = (int)epicMultiplier * (Stamina + EnemyLevel);
                Intellect = (int)epicMultiplier * (Intellect + EnemyLevel);
            }
            else if (_enemyInfo.mobRarity == EnemyTypes.MobRarity.DEMONIC)
            {
                float demonicMultiplier = (1.75f);

                //mulitiplier
                Defence = (int)demonicMultiplier * (Defence + EnemyLevel);
                Haste = (int)demonicMultiplier * (Haste + EnemyLevel);
                Endurance = (int)demonicMultiplier * (Endurance + EnemyLevel);
                Resistance = (int)demonicMultiplier * (Endurance + EnemyLevel);
                Agility = (int)demonicMultiplier * (Endurance + EnemyLevel);
                Strength = (int)demonicMultiplier * (Strength + EnemyLevel);
                Stamina = (int)demonicMultiplier * (Stamina + EnemyLevel);
                Intellect = (int)demonicMultiplier * (Intellect + EnemyLevel);

            }
        }

        void Attack()
        {
            if (state != State.Dead)
            {
                if (TargetSeen == true)
                {
                    state = State.Attacking;
                    damage = Random.Range(Mindamage, MaxDamage);
                    
                    Player.GetHit(damage);
                    StartCoroutine(EnemySounds());

                }
                else
                {
                    //dont attack and move away from
                    transform.position = Vector3.MoveTowards(transform.position, spawnPoint.position, 5f);
                    if (Vector3.Distance(spawnPoint.position, transform.position) < offsetSpawnPoint)
                    {
                        int i = Random.Range(0, 2);
                        if (i == 0)
                        {
                            Idle();
                        }
                        else if (i == 1)
                        {
                            state = State.Patrol;
                            Patrolling();
                        }
                    }
                }
            }
        }
    }
}