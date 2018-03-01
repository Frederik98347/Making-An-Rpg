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

    [SerializeField] int EnemyLevel;
    [SerializeField] int exptogive;
    int health;
    float attackspeed;
    float AttackRange;
    float movementspeed;
    float walkingspeed;
    float detectionRange;
    Texture2D enemyIcon;
    string toolTip;
    int Resistance;
    string enemyClass;
    int endurance;
    string mobRarity;

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

    public string EnemyClass
    {
        get
        {
            return enemyClass;
        }

        set
        {
            enemyClass = value;
        }
    }

    public int Endurance
    {
        get
        {
            return endurance;
        }

        set
        {
            endurance = value;
        }
    }

    public string MobRarity
    {
        get
        {
            return mobRarity;
        }

        set
        {
            mobRarity = value;
        }
    }

    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
		Audio = GetComponent<AudioSource> ();

		//Aiconfig variables
		if (_enemyInfo) {
            enemyIcon = _enemyInfo.EnemyIcon;
            toolTip = _enemyInfo.ToolTip;
            outofrangeTimer = _enemyInfo.OutofrangeTimer;
            walkingspeed = _enemyInfo.WalkingSpeed;
            enemyDefense = _enemyInfo.EnemyDefense;
            AttackRange = _enemyInfo.AttackRange;
			EnemyLevel = _enemyInfo.EnemyLevel;
			exptogive = _enemyInfo.Exptogive;
			health = _enemyInfo.EnemyHP;
			attackspeed = _enemyInfo.AttackSpeed;
			movementspeed = _enemyInfo.MovementSpeed;
			detectionRange = _enemyInfo.DetectionRange;

			Mindamage = _enemyInfo.MinAutoDamage;
			MaxDamage = _enemyInfo.MaxAutoDamage;
            Resistance = _enemyInfo.EnemyResistance;
            Endurance = _enemyInfo.Endurance;

            EnemyClass = _enemyInfo.mobClass.ToString();
            MobRarity = _enemyInfo.mobRarity.ToString();

		}
	}
	
	// Update is called once per frame
	void Update () {

        if (AttackcurTime < attackspeed && TargetSeen == true && Vector3.Distance(transform.position, this.player.position) < AttackRange * attackRangeMulti)
        {
            AttackcurTime += Time.deltaTime;
            //count up
            if (AttackcurTime >= attackspeed)
            {
                Attack();
                AttackcurTime = 0;
            }
        }

		Chase ();
        Patrolling();
        IsEnemyDead ();
        StartCoroutine(EnemySounds());

        Idle();
    }

    void Patrolling()
    {
        if (state == State.Patrol && waypoints.Length > 0)
        {
                anim.SetBool("isIdle", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isDead", false);


            if (Vector3.Distance(waypoints[currentWP].transform.position, this.transform.position) < accuracyWP)
            {
                currentWP++;

                if (currentWP >= waypoints.Length)
                {
                    currentWP = 0;
                }
            }

            //rotate and move towards waypoint
                Vector3 direction = waypoints[currentWP].transform.position - this.transform.position;
                direction.y = 0;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
                this.transform.Translate(0, 0, walkingspeed * Time.deltaTime);
        } else if(waypoints.Length == 0)
        {
            state = State.Idle;
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
                state = State.Patrol;
                Patrolling();
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
            yield return new WaitForSeconds(Audio.clip.length / 2);
			//Audio.Stop(runSound);
		}

        if (state == State.Patrol && TargetSeen == true && Audio.isPlaying == false)
        {
            Audio.clip = walkSound;
            Audio.volume = Random.Range(.6f, 1f);
            Audio.pitch = Random.Range(.8f, 1f);
            Audio.PlayOneShot(walkSound);
            yield return new WaitForSeconds(Audio.clip.length / 2);
            //Audio.Stop(runSound);
        }
    }

	public void GetHit (int damage) {

        transform.GetComponent<CharacterHealthsytem>().GetHit(damage);
		if (transform.GetComponent<CharacterHealthsytem>().IsDead == true) {
            //enemy is dead
            state = State.Dead;

            if (player.GetComponent<Experience>())
            {
                //send exp to player here
                player.GetComponent<Experience>().GainExp(exptogive);
            }
        }
	}

	void EnemyPowerAndLevel () {
		if (_enemyInfo.mobRarity.ToString() == "ELITE") {
			health = (health) * (EnemyLevel * 2);
			exptogive = exptogive + exptogive;

			//mob damage
			Mindamage = Mindamage*(EnemyLevel+(2-1));
			MaxDamage = MaxDamage*(EnemyLevel+(2-1));
			damage  = Random.Range(Mindamage, MaxDamage);

			//defensive attributes
			enemyDefense = enemyDefense + (enemyDefense + EnemyLevel);
            Resistance += EnemyLevel*EnemyLevel-1;
            Endurance += EnemyLevel*EnemyLevel-1;

        } else if (_enemyInfo.mobRarity.ToString() == "COMMON") {
			health = (health) * EnemyLevel;

			//mob damage
			Mindamage = Mindamage*(EnemyLevel);
			MaxDamage = MaxDamage*(EnemyLevel);
			damage  = Random.Range(Mindamage, MaxDamage);
		}
	}

	void Attack () {
		EnemyPowerAndLevel ();

		if (this.player.GetComponent<CharacterHealthsytem> ().IsDead == false && TargetSeen == true) {
            state = State.Attacking;
			this.player.transform.GetComponent<CharacterHealthsytem>().GetHit (damage);

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