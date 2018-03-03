using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script to control enemy mob's behaviour, sounds and animations
/// </summary>
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour {
	
	public AIConfig _enemyInfo;
    public Slider HealthBar;
    public enum State
    {
        Idle,
        Chase,
        Attacking,
        Patrol,
        Dead
    }
    
	[SerializeField] Player Player;
    public Hp_barPos canvas;

    [SerializeField] float rotSpeed = 2.5f;


    bool targetSeen = false;
	[SerializeField] float behind_detectionRange = 4f;
    float outofrangeTimer;
    [SerializeField] float offsetSpawnPoint = 2f;
    [SerializeField] Transform spawnPoint;

    [SerializeField] Transform player;

	// animations
	private Animator anim;

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
    float AttackRange;
    float movementspeed;
    float walkingspeed;
    float detectionRange;
    Texture2D enemyIcon;
    string toolTip;

    //way point patrol
    public State state = State.Idle;
    public GameObject[] waypoints;
    int currentWP = 0;
    [Range(0.1f, 2f)]
    public float accuracyWP = .8f;
    [Range(0, 20)]
    [Tooltip("Time before you want an idling enemy to begin patrolling")] public float timeBeforeStateChange;
    public int Defence;
    public int Haste;
    public int Endurance;
    public int Resistance;
    public int Agility;
    public int Strength;
    public int Stamina;
    public int Intellect;


    float currentHealth;
    float maxHealth;

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

    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }

        set
        {
            maxHealth = value;
        }
    }

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }

    // Use this for initialization
    void Start () {
        if (canvas == null)
        {
            canvas = GetComponentInChildren<Hp_barPos>();
        }
        EnemyPowerAndLevel();
        anim = GetComponent<Animator> ();
		Audio = GetComponent<AudioSource> ();
        

		//Aiconfig variables
		if (_enemyInfo) {
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
            // Rests health to full on game load
            CurrentHealth = MaxHealth;
            HealthBar.value = CalculateHealth();
            exptogive = _enemyInfo.Exptogive;
        }

        if (Player == null)
        {
            Player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
            if (player == null)
            {
                player = Player.transform;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Player.Health <= 0 && state != State.Dead)
        {
            state = State.Patrol;
            TargetSeen = false;
        }

        Chase ();
        if (waypoints.Length != 0)
        {
            Patrolling();
        }
        StartCoroutine(EnemySounds());
       // InRangeForAttack();

        Idle();
    }

    void InRangeForAttack()
    {
        if (state != State.Dead)
        {
            if (AttackcurTime < attackspeed && Vector3.Distance(transform.position, this.player.transform.position) < AttackRange)
            {
                state = State.Attacking;
                AttackcurTime += Time.deltaTime;
                //count up
                if (AttackcurTime >= attackspeed && state == State.Attacking)
                {
                    Attack();
                    AttackcurTime = 0;
                }
            }
        }
    }

    void Patrolling()
    {
        // bug here, object not set to error, but everything works
        if (state != State.Dead)
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


                    if (Vector3.Distance(waypoints[currentWP].gameObject.transform.position, transform.position) < accuracyWP)
                    {
                        currentWP++;

                        if (currentWP >= waypoints.Length)
                        {
                            currentWP = 0;
                        }
                    }

                    //rotate and move towards waypoint
                    Vector3 direction = waypoints[currentWP].transform.position - transform.position;
                    direction.y = 0;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
                    transform.Translate(0, 0, walkingspeed * Time.deltaTime);
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
                    }
                }

            }
        }
    }

	void IsEnemyDead() {

        if (CurrentHealth <= 0) {
            CurrentHealth = 0;
            HealthBar.value = CurrentHealth;
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
            exptogive = 0;
            // make loot glow
            //loot gui

            if (DeadCounter < DeadTime) {
				//count up
				DeadCounter += Time.deltaTime;

			} else {
				Destroy (transform.gameObject);
				DeadCounter = 0;
			}
		}
	}
	void Chase () {
		Vector3 direction = player.position - this.transform.position;
        direction.y = 0;

        float angle = Vector3.Angle (direction, this.transform.forward);
        if (state != State.Dead) {
            if (Vector3.Distance(transform.position, player.position) < detectionRange && angle < 30 || Vector3.Distance(transform.position, this.player.position) < behind_detectionRange)
            {
                TargetSeen = true;
                state = State.Chase;

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
                if (direction.magnitude > AttackRange)
                {
                    //chase
                    TargetSeen = true;
                    this.transform.Translate(0, 0, movementspeed * Time.deltaTime);
                    anim.SetBool("isRunning", true);
                    anim.SetBool("isDead", false);
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isIdle", false);


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
            else if (OutofrangeTimer < 0.0f && Vector3.Distance(transform.position, this.player.position) > detectionRange)
            {
                TargetSeen = false;
                anim.SetBool("isIdle", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isWalking", true);
                anim.SetBool("isDead", false);
                state = State.Idle;

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

	private IEnumerator EnemySounds() {
        if (state != State.Dead)
        {
            if (state == State.Attacking && TargetSeen == true && Audio.isPlaying == false)
            {
                Audio.clip = AttackSound;
                Audio.volume = Random.Range(.8f, 1f);
                Audio.pitch = Random.Range(.8f, 1f);
                Audio.PlayDelayed(Random.Range(.8f, 1f));
                yield return new WaitForSeconds(Audio.clip.length);
            }

            if (state == State.Chase && TargetSeen == true && Audio.isPlaying == false)
            {
                Audio.clip = runSound;
                Audio.volume = Random.Range(.55f, 1f);
                Audio.pitch = Random.Range(.75f, 1f);
                Audio.PlayOneShot(runSound);
                yield return new WaitForSeconds(Audio.clip.length);
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

	public void GetHit (int damage) {
        //deal damage
        CurrentHealth -= damage;
        HealthBar.value = CalculateHealth();
        // checking if this transform is dead
		if (CurrentHealth <= 0) {
            //enemy is dead
            state = State.Dead;
            CurrentHealth = 0;
            HealthBar.value = CalculateHealth();
            IsEnemyDead();
        }
	}

    public void GetHealth(int healValue)
    {
        CurrentHealth += healValue;
        HealthBar.value = CalculateHealth();
        //Enemy healing spells can work now

        //making sure Health can go above 100%
        if (CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
            HealthBar.value = 1;
        }
    }

    void EnemyPowerAndLevel () {
        //Check what class enemy is, give bonus to certain class specific skills or passivs
		if (_enemyInfo.mobRarity == EnemyTypes.MobRarity.RARE) {

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


        } else if (_enemyInfo.mobRarity == EnemyTypes.MobRarity.COMMON) {
            float commonMultiplier = (1.0f);
            Defence = (int)commonMultiplier * (Defence + EnemyLevel);
            Haste = (int)commonMultiplier * (Haste + EnemyLevel);
            Endurance = (int)commonMultiplier * (Endurance + EnemyLevel);
            Resistance = (int)commonMultiplier * (Endurance + EnemyLevel);
            Agility = (int)commonMultiplier * (Endurance + EnemyLevel);
            Strength = (int)commonMultiplier * (Strength + EnemyLevel);
            Stamina = (int)commonMultiplier * (Stamina + EnemyLevel);
            Intellect = (int)commonMultiplier * (Intellect + EnemyLevel);

        } else if (_enemyInfo.mobRarity == EnemyTypes.MobRarity.EPIC)
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
        } else if (_enemyInfo.mobRarity == EnemyTypes.MobRarity.DEMONIC)
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

	void Attack () {
        if (state != State.Dead)
        {
            if (TargetSeen == true)
            {
                state = State.Attacking;
                damage = Random.Range(Mindamage, MaxDamage);
                Player.GetHit(damage);

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

    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }
}