using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour {
	
	public AIConfig _enemyInfo;
    
	[SerializeField] Player Player;

    [SerializeField] float rotSpeed = 2.5f;


    [SerializeField] bool targetSeen = false;
	[SerializeField] float behind_detectionRange;
    [SerializeField] float outofrangeTimer;

	public CharacterController controller;
    [SerializeField] Transform player;

	// animations
	private Animator anim;

	//audio
	public AudioClip AttackSound;
	public AudioClip runSound;
	public AudioClip deathSound;
	AudioSource Audio;

	//mob info
	float DeadCounter = 0;
	float DeadTime = 90f;

    //mob damage
    int Mindamage;
	int MaxDamage;
	int damage;

	//Sound bools
	bool PlayAttackSound = false;
	bool PlayRunningSound = false;

	float AttackcurTime = 0.0f;
	int enemyDefense = 0;

    [SerializeField] int EnemyLevel;
    [SerializeField] int exptogive;
    int health;
    [SerializeField] float attackspeed;
    float AttackRange;
    [SerializeField] float movementspeed;
    [SerializeField] float walkingspeed;
    [SerializeField] float detectionRange;
    [SerializeField] Texture2D enemyIcon;
    [SerializeField] string toolTip;
    [SerializeField] int Resistance;
    [SerializeField] string enemyClass;
    [SerializeField] int endurance;
    [SerializeField] string mobRarity;
	bool dead;

    //way point patrol
    public string state = "patrol";
    public GameObject[] waypoints;
    int currentWP = 0;
    public float accuracyWP = .8f;

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

    public bool Dead
    {
        get
        {
            return dead;
        }

        set
        {
            dead = value;
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
        //StartCoroutine("EnemySounds");

		if (health <= 0) {
			health = 0;
			Dead = true;
		}

        if (AttackcurTime < attackspeed && TargetSeen == true && Vector3.Distance(transform.position, this.player.position) < AttackRange * .8f)
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
		//EnemySounds();
	}

    void Patrolling()
    {
        if (state == "patrol" && waypoints.Length > 0 && Dead == false)
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
        }
    }

	void IsEnemyDead() {
		
		if (Dead == true) {

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
		if (Dead == false) {
			
			if (Vector3.Distance (transform.position, player.position) < detectionRange && angle < 30 || state == "chase") {
				TargetSeen = true;
                state = "chase";

				this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);
				anim.SetBool ("isIdle", false);
				if (direction.magnitude > AttackRange*1.2f) {
					this.transform.Translate (0, 0, movementspeed*Time.deltaTime);
					anim.SetBool ("isRunning", true);
					anim.SetBool ("isDead", false);
					anim.SetBool ("isAttacking", false);
                    anim.SetBool("isWalking", false);

					PlayRunningSound = true;
					PlayAttackSound = false;


				} else {
					anim.SetBool ("isDead", false);
					anim.SetBool ("isAttacking", true);
					anim.SetBool ("isRunning", false);
                    anim.SetBool("isWalking", false);
                    PlayRunningSound = false;
					PlayAttackSound = true;
				}

			} else if (Vector3.Distance (transform.position, this.player.position) < behind_detectionRange || state == "chase") {
				TargetSeen = true;
				direction.y = 0;
                state = "chase";

				this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);
				anim.SetBool ("isIdle", false);
				if (direction.magnitude > AttackRange*.8f) {
					this.transform.Translate (0, 0, movementspeed * Time.deltaTime);
					anim.SetBool ("isRunning", true);
					anim.SetBool ("isDead", false);
					anim.SetBool ("isAttacking", false);
                    anim.SetBool("isWalking", false);

                    PlayRunningSound = true;
					PlayAttackSound = false;
				} else {
					anim.SetBool ("isDead", false);
					anim.SetBool ("isAttacking", true);
					anim.SetBool ("isRunning", false);
                    anim.SetBool("isWalking", false);

                    PlayRunningSound = false;
					PlayAttackSound = true;
				}

			} else {

				if (OutofrangeTimer > 0.0f && TargetSeen == true && Vector3.Distance (transform.position, this.player.position) > detectionRange || state == "chase") {
                    state = "chase";
					OutofrangeTimer -= Time.deltaTime;
					this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);
					anim.SetBool ("isIdle", false);
                    anim.SetBool("isWalking", false);

                    if (direction.magnitude > AttackRange*1.2f) {
						this.transform.Translate (0, 0, movementspeed * Time.deltaTime);
						anim.SetBool ("isRunning", true);
                        anim.SetBool("isWalking", false);
                        anim.SetBool ("isDead", false);
						anim.SetBool ("isAttacking", false);

						PlayRunningSound = true;
						PlayAttackSound = false;

					} else {
						anim.SetBool ("isDead", false);
						anim.SetBool ("isAttacking", true);
						anim.SetBool ("isRunning", false);
                        anim.SetBool("isWalking", false);
                        anim.SetBool("isIdle", false);

                        PlayRunningSound = false;
						PlayAttackSound = true;
					}
						
				} else if (OutofrangeTimer < 0.0f && TargetSeen == true && Vector3.Distance (transform.position, this.player.position) > detectionRange) {
					TargetSeen = false;
					anim.SetBool ("isIdle", false);
					anim.SetBool ("isRunning", false);
                    anim.SetBool("isWalking", true);
                    anim.SetBool ("isAttacking", false);
					anim.SetBool ("isDead", false);
					PlayRunningSound = false;
					PlayAttackSound = false;
                    state = "patrol";

				} else if (Dead == true) {
					TargetSeen = false;
					OutofrangeTimer = 0f;
					anim.SetBool ("isIdle", false);
					anim.SetBool ("isDead", true);
					anim.SetBool ("isAttacking", false);
					anim.SetBool ("isRunning", false);
                    anim.SetBool("isWalking", false);
                    exptogive = 0;
                    state = "dead";

					PlayRunningSound = false;
					PlayAttackSound = false;
				}
			}
		} else if (Dead == true) {
			TargetSeen = false;
			OutofrangeTimer = 0f;
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isDead", true);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isRunning", false);
            anim.SetBool("isWalking", false);
            exptogive = 0;
            state = "dead";

			PlayRunningSound = false;
			PlayAttackSound = false;
		}
	}

	private IEnumerator EnemySounds() {
		
		if (PlayAttackSound == true && TargetSeen == true && Dead != true) {
			Debug.Log ("Attack sound playing");
			Audio.clip = AttackSound;
			Audio.PlayOneShot (AttackSound);
			yield return new WaitForSeconds(Audio.clip.length);
			//Audio.Stop;
		}

		if (Dead == true) {
			int i = 0;
			i++;

				if (i == 1) {
					Debug.Log ("Death Sound");
					Audio.clip = deathSound;
					Audio.PlayOneShot (deathSound);
					yield return new WaitForSeconds(Audio.clip.length);
					//Audio.Stop;
				}
		}

		if (PlayRunningSound == true && TargetSeen == true && Dead != true) {
			Debug.Log ("Run sound playing");
			Audio.clip = runSound;
			Audio.PlayOneShot (runSound);
			yield return new WaitForSeconds(Audio.clip.length);
			//Audio.Stop(runSound);
		}
	}

	public void GetHit (int playerDamage) {

		health = health - playerDamage;
		Debug.Log ("HP: " + health);

		if (health <= 0) {
			//enemy is dead
			Dead = true;
			health = 0;

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

		if (this.player.GetComponent<CharacterHealthsytem> ().IsDead == true && TargetSeen == true && Vector3.Distance (transform.position, this.player.position) < AttackRange*1.2f) {
			this.player.gameObject.transform.GetComponent<CharacterHealthsytem>().GetHit (damage);
			Debug.Log ("Enemy damage: " + damage);

		} else {
            //dont attack and move away from
            state = "patrol";
		}
	}
}