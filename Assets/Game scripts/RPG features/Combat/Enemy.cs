using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour {
	
	public AIConfig _enemyInfo;
	public Player Player;

    public float rotSpeed = 2f;


	public bool targetSeen = false;
	private float behind_detectionRange = 3.0f;
	public float outofrangeTimer = 10.0f;

	public CharacterController controller;
	public Transform player;

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

	public bool Elite = false;

    //mob damage
    int Mindamage;
	int MaxDamage;
	int damage;

	//Sound bools
	bool PlayAttackSound = false;
	bool PlayRunningSound = false;

	public float AttackcurTime = 0.0f;
	public int enemyArmor = 0;

	public int EnemyLevel = 0;
	public int expToGive = 0;
	public int health = 0;
	float attackspeed = 0f;
    float AttackRange = 0f;
	float movementspeed = 0f;
    float walkingspeed = 0f;
	public float detectionRange = 0f;

	public bool Dead = false;

    //way point patrol
    string state = "patrol";
    public GameObject[] waypoints;
    int currentWP = 0;
    public float accuracyWP = 1.0f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		Audio = GetComponent<AudioSource> ();

		//Aiconfig variables
		if (_enemyInfo) {
            walkingspeed = _enemyInfo.walkingSpeed;
            enemyArmor = _enemyInfo.Enemyarmor;
            AttackRange = _enemyInfo.attackRange;
			EnemyLevel = _enemyInfo.EnemyLevel;
			expToGive = _enemyInfo.expTogive;
			health = _enemyInfo.EnemyHP;
			attackspeed = _enemyInfo.AttackSpeed;
			movementspeed = _enemyInfo.MovementSpeed;
			detectionRange = _enemyInfo.DetectionRange;

			Mindamage = _enemyInfo.MinAutoDamage;
			MaxDamage = _enemyInfo.MaxAutoDamage; 
		}
	}
	
	// Update is called once per frame
	void Update () {
        //StartCoroutine("EnemySounds");

		if (health <= 0) {
			health = 0;
			Dead = true;
		}

        if (AttackcurTime < attackspeed && targetSeen == true && Vector3.Distance(transform.position, this.player.position) < AttackRange)
        {
            AttackcurTime += Time.deltaTime;
            //count up
            if (AttackcurTime >= attackspeed)
            {
                Attack();
                AttackcurTime = 0;
            }
        }
        else
        {
            AttackcurTime = 0;
        }

		chase ();
        Patrolling();
        isEnemyDead ();
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

	void isEnemyDead() {
		
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
	void chase () {
		Vector3 direction = player.position - this.transform.position;
        direction.y = 0;

        float angle = Vector3.Angle (direction, this.transform.forward);
		if (Dead == false) {
			
			if (Vector3.Distance (transform.position, player.position) < detectionRange && angle < 30 || state == "chase") {
				targetSeen = true;
                state = "chase";

				this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);
				anim.SetBool ("isIdle", false);
				if (direction.magnitude > AttackRange) {
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
				targetSeen = true;
				direction.y = 0;
                state = "chase";

				this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);
				anim.SetBool ("isIdle", false);
				if (direction.magnitude > AttackRange) {
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

				if (outofrangeTimer > 0.0f && targetSeen == true && Vector3.Distance (transform.position, this.player.position) > detectionRange || state == "chase") {
                    state = "chase";
					outofrangeTimer -= Time.deltaTime;
					this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);
					anim.SetBool ("isIdle", false);
                    anim.SetBool("isWalking", false);

                    if (direction.magnitude > AttackRange) {
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
						
				} else if (outofrangeTimer < 0.0f && targetSeen == true && Vector3.Distance (transform.position, this.player.position) > detectionRange) {
					targetSeen = false;
					anim.SetBool ("isIdle", false);
					anim.SetBool ("isRunning", false);
                    anim.SetBool("isWalking", true);
                    anim.SetBool ("isAttacking", false);
					anim.SetBool ("isDead", false);
					PlayRunningSound = false;
					PlayAttackSound = false;
                    state = "patrol";

				} else if (Dead == true) {
					targetSeen = false;
					outofrangeTimer = 0f;
					anim.SetBool ("isIdle", false);
					anim.SetBool ("isDead", true);
					anim.SetBool ("isAttacking", false);
					anim.SetBool ("isRunning", false);
                    anim.SetBool("isWalking", false);
                    expToGive = 0;
                    state = "dead";

					PlayRunningSound = false;
					PlayAttackSound = false;
				}
			}
		} else if (Dead == true) {
			targetSeen = false;
			outofrangeTimer = 0f;
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isDead", true);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isRunning", false);
            anim.SetBool("isWalking", false);
            expToGive = 0;
            state = "dead";

			PlayRunningSound = false;
			PlayAttackSound = false;
		}
	}

	private IEnumerator EnemySounds() {
		
		if (PlayAttackSound == true && targetSeen == true && Dead != true) {
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

		if (PlayRunningSound == true && targetSeen == true && Dead != true) {
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
		}
	}

	void EnemyPowerAndLevel () {
		if (Elite == true) {
			health = (health) * (EnemyLevel * 2);
			expToGive = expToGive + expToGive;

			//mob damage
			Mindamage = Mindamage*(EnemyLevel+(2-1));
			MaxDamage = MaxDamage*(EnemyLevel+(2-1));
			damage  = Random.Range(Mindamage, MaxDamage);

			//defensive attributes
			enemyArmor = enemyArmor + (enemyArmor * EnemyLevel);

		} else if (Elite != true) {
			health = (health) * EnemyLevel;

			//mob damage
			Mindamage = Mindamage*(EnemyLevel);
			MaxDamage = MaxDamage*(EnemyLevel);
			damage  = Random.Range(Mindamage, MaxDamage);
		}
	}

	void Attack () {
		EnemyPowerAndLevel ();

		if (this.player.GetComponent<Player> ().isDead == false && targetSeen == true && Vector3.Distance (transform.position, this.player.position) < AttackRange) {
			this.player.gameObject.transform.GetComponent<Player>().GetHit (damage);
			Debug.Log ("Enemy damage: " + damage);

		} else {
            //dont attack and move away from
            state = "patrol";
		}
	}
}