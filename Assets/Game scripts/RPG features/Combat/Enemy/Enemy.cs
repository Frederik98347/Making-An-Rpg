using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour {
	
	public AIConfig _enemyInfo;
    public enum State
    {
        Idle,
        Chase,
        Attacking,
        Patrol,
        Dead
    }
    
	[SerializeField] Player Player;

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
    int enemyDefense;
    public int[] stats;

    [SerializeField] int EnemyLevel;
    [SerializeField] int exptogive;
    public CharacterHealthsytem healthsystem;
    float health;
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
    [Range(0.1f, 3f)]
    private float attackRangeMulti;
    [Range(0, 20)]
    [Tooltip("Time before you want an idling enemy to begin patrolling")] public float timeBeforeStateChange;

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

    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    // Use this for initialization
    void Start () {
        stats = new int[12];
        anim = GetComponent<Animator> ();
		Audio = GetComponent<AudioSource> ();
        if (healthsystem == null)
        {
            healthsystem = transform.GetComponent<CharacterHealthsytem>();
        }
        

		//Aiconfig variables
		if (_enemyInfo) {
            enemyIcon = _enemyInfo.EnemyIcon;
            toolTip = _enemyInfo.ToolTip;
            outofrangeTimer = _enemyInfo.OutofrangeTimer;
            walkingspeed = _enemyInfo.WalkingSpeed;
            AttackRange = _enemyInfo.AttackRange;
			EnemyLevel = _enemyInfo.EnemyLevel;
			Health = stats[1];
            exptogive = stats[11];
			attackspeed = _enemyInfo.AttackSpeed;
			movementspeed = _enemyInfo.MovementSpeed;
			detectionRange = _enemyInfo.DetectionRange;

            stats[0] = _enemyInfo.EnemyDefense;
            stats[1] = _enemyInfo.EnemyHP;
            stats[2] = _enemyInfo.Endurance;
            stats[3] = _enemyInfo.EnemyResistance;
            stats[4] = _enemyInfo.Agility;
            stats[5] = _enemyInfo.Strength;
            stats[6] = _enemyInfo.Stamina;
            stats[7] = _enemyInfo.Intellect;
            stats[8] = _enemyInfo.Haste;
            stats[9] = _enemyInfo.MinAutoDamage;
            stats[10] = _enemyInfo.MaxAutoDamage;
            Mindamage = stats[9];
            MaxDamage = stats[10];
            stats[11] = _enemyInfo.Exptogive;
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

		Chase ();
        if (waypoints != null)
        {
            Patrolling();
        }
        IsEnemyDead ();
        StartCoroutine(EnemySounds());

        Idle();
    }

    void InRangeForAttack()
    {
        if (AttackcurTime < attackspeed && TargetSeen == true && Vector3.Distance(transform.position, this.player.transform.position) < AttackRange * attackRangeMulti)
        {
            AttackcurTime += Time.deltaTime;
            //count up
            if (AttackcurTime >= attackspeed)
            {
                Attack();
                AttackcurTime = 0;
            }
        }
    }

    void Patrolling()
    {
        // bug here, object not set to error, but everything works
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

    void Idle()
    {
        if (state == State.Idle)
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isDead", false);
        }

        if (waypoints.Length > 0 && targetSeen == false && state != State.Patrol)
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

	void IsEnemyDead() {

        if (state == State.Dead) {

            TargetSeen = false;
            OutofrangeTimer = 0f;
            anim.SetBool("isIdle", false);
            anim.SetBool("isDead", true);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
            movementspeed = 0f;
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

        if (Vector3.Distance(transform.position, player.position) < detectionRange && (angle < 30 || Vector3.Distance(transform.position, this.player.position) < behind_detectionRange))
        {
            TargetSeen = true;
            state = State.Chase;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            if (direction.magnitude > AttackRange)
            {
                //chase
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
            }

        }
        else if (OutofrangeTimer < 0.0f && TargetSeen == true && Vector3.Distance(transform.position, this.player.position) > detectionRange)
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

	private IEnumerator EnemySounds() {
		
		if (state == State.Attacking && TargetSeen == true && Audio.isPlaying == false) {
			Audio.clip = AttackSound;
            Audio.volume = Random.Range(.8f, 1f);
            Audio.pitch = Random.Range(.8f, 1f);
            Audio.PlayDelayed(Random.Range(.8f, 1f));
            yield return new WaitForSeconds(Audio.clip.length*2);
        }

		if (state == State.Dead && Audio.isPlaying == false) {
            Audio.clip = deathSound;
            Audio.PlayOneShot(deathSound);
            yield return new WaitForSeconds(Audio.clip.length);
            //Audio.Stop;
        }

        if (state == State.Chase && TargetSeen == true && Audio.isPlaying == false) {
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

	public void GetHit (int damage) {
        //deal damage
        healthsystem.GetHit(damage);
        // checking if this transform is dead
		if (healthsystem.IsDead) {
            //enemy is dead
            state = State.Dead;

            if (player.GetComponent<Experience>())
            {
                //send exp to player here
                Debug.Log(stats[12]);
                player.GetComponent<Experience>().GainExp(stats[12]);
            }
        }
	}

	void EnemyPowerAndLevel () {
        //Check what class enemy is, give bonus to certain class specific skills or passivs
		if (_enemyInfo.mobRarity == EnemyTypes.MobRarity.RARE) {

            int rareMultiplier = (int)(1.3f * EnemyLevel);

            foreach (int value in stats)
            {
                rareMultiplier *= value;
            }

        } else if (_enemyInfo.mobRarity == EnemyTypes.MobRarity.COMMON) {
            float commonMultiplier = (1.0f);

            foreach (int value in stats)
            {
                commonMultiplier = (int)commonMultiplier * (value + EnemyLevel);
            }
        } else if (_enemyInfo.mobRarity == EnemyTypes.MobRarity.EPIC)
        {
            float epicMultiplier = (1.6f);

            foreach (int value in stats)
            {
                epicMultiplier = (int)epicMultiplier * (value + EnemyLevel);
            }
        } else if (_enemyInfo.mobRarity == EnemyTypes.MobRarity.DEMONIC)
        {
            float demonicMultiplier = (2f);

            foreach (int value in stats)
            {
                demonicMultiplier = (int)demonicMultiplier * (value + EnemyLevel);
            }
        }
	}

	void Attack () {
		EnemyPowerAndLevel ();

		if (TargetSeen == true) {
            state = State.Attacking;
            damage = Random.Range(Mindamage, MaxDamage+1);
            Debug.Log("damage");
            Player.healthsystem.GetHit(damage);

		} else {
            //dont attack and move away from
            transform.position = Vector3.MoveTowards(transform.position, spawnPoint.position, 5f);
            if (Vector3.Distance(spawnPoint.position, transform.position) < offsetSpawnPoint) {
                int i = Random.Range(0, 2);
                if (i == 0)
                {
                    Idle();
                } else if (i == 1)
                {
                    state = State.Patrol;
                    Patrolling();
                }
            }
        }
	}
}