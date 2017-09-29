using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour {

	public Player Player;

	//Ai variables
	public float speed = 5.5F;
	public float detectionRange = 10.0f;
	private float behind_detectionRange = 3.0f;
	public bool targetSeen = false;
	public float outofrangeTimer = 7.0f;

	int health = 20;
	float attackSpeed = 2.0f;
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
	public int EnemyLevel = 1;

	//mob damage
	int Mindamage;
	int MaxDamage;
	int damage;

	//Sound bools
	bool PlayAttackSound = false;
	bool PlayRunningSound = false;

	public float AttackcurTime = 0.0f;

	public int expToGive = 1;
	public int enemyArmor  = 1;
	public int HP {
		get{ return health; }
		set{ health = value; }
	}

	public bool Dead = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		Audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		//StartCoroutine("EnemySounds");
		
		if (health <= 0) {
			health = 0;
			Dead = true;
		}

		if (AttackcurTime < attackSpeed) {
			AttackcurTime += Time.deltaTime;
			//count up

		} else {
			Attack ();
			AttackcurTime = 0;
		}

		chase ();
		isEnemyDead ();
		//EnemySounds();
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
		float angle = Vector3.Angle (direction, this.transform.forward);
		if (Dead == false) {
			
			if (Vector3.Distance (transform.position, this.player.position) < detectionRange && angle < 30) {
				targetSeen = true;
				direction.y = 0;

				this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), speed * Time.deltaTime);
				anim.SetBool ("isIdle", false);
				if (direction.magnitude > 3) {
					this.transform.Translate (0, 0, 0.05f);
					anim.SetBool ("isRunning", true);
					anim.SetBool ("isDead", false);
					anim.SetBool ("isAttacking", false);

					PlayRunningSound = true;
					PlayAttackSound = false;


				} else {
					anim.SetBool ("isDead", false);
					anim.SetBool ("isAttacking", true);
					anim.SetBool ("isRunning", false);
					PlayRunningSound = false;
					PlayAttackSound = true;
				}

			} else if (Vector3.Distance (transform.position, this.player.position) < behind_detectionRange) {
				targetSeen = true;
				direction.y = 0;

				this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), speed * Time.deltaTime);
				anim.SetBool ("isIdle", false);
				if (direction.magnitude > 3) {
					this.transform.Translate (0, 0, 0.05f);
					anim.SetBool ("isRunning", true);
					anim.SetBool ("isDead", false);
					anim.SetBool ("isAttacking", false);

					PlayRunningSound = true;
					PlayAttackSound = false;
				} else {
					anim.SetBool ("isDead", false);
					anim.SetBool ("isAttacking", true);
					anim.SetBool ("isRunning", false);

					PlayRunningSound = false;
					PlayAttackSound = true;
				}

			} else {

				if (outofrangeTimer > 0.0f && targetSeen == true && Vector3.Distance (transform.position, this.player.position) > detectionRange) {
					
					outofrangeTimer -= Time.deltaTime;
					this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), speed * Time.deltaTime);
					anim.SetBool ("isIdle", false);

					if (direction.magnitude > 3) {
						this.transform.Translate (0, 0, 0.05f);
						anim.SetBool ("isRunning", true);
						anim.SetBool ("isDead", false);
						anim.SetBool ("isAttacking", false);

						PlayRunningSound = true;
						PlayAttackSound = false;

					} else {
						anim.SetBool ("isDead", false);
						anim.SetBool ("isAttacking", true);
						anim.SetBool ("isRunning", false);

						PlayRunningSound = false;
						PlayAttackSound = true;
					}
						
				} else if (outofrangeTimer < 0.0f && targetSeen == true && Vector3.Distance (transform.position, this.player.position) > detectionRange) {
					targetSeen = false;
					anim.SetBool ("isIdle", true);
					anim.SetBool ("isRunning", false);
					anim.SetBool ("isAttacking", false);
					anim.SetBool ("isDead", false);
					outofrangeTimer = 7.0f;

					PlayRunningSound = false;
					PlayAttackSound = false;

				} else if (Dead == true) {
					targetSeen = false;
					outofrangeTimer = 0f;
					anim.SetBool ("isIdle", false);
					anim.SetBool ("isDead", true);
					anim.SetBool ("isAttacking", false);
					anim.SetBool ("isRunning", false);
					expToGive = 0;

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
			expToGive = 0;

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
		
		if (this.player.GetComponent<Player> ().Level == EnemyLevel-2) {
			float playerDamageTaken = playerDamage / (1.2f + this.player.GetComponent<Player> ().DamageReduction);
			health = health - Mathf.RoundToInt (playerDamageTaken);

		} else if (this.player.GetComponent<Player> ().Level == EnemyLevel-3) {
			float playerDamageTaken = playerDamage / (1.3f + this.player.GetComponent<Player> ().DamageReduction);
			health = health - Mathf.RoundToInt (playerDamageTaken);

		} else if (this.player.GetComponent<Player> ().Level == EnemyLevel-4) {
			float playerDamageTaken = playerDamage / (1.4f + this.player.GetComponent<Player> ().DamageReduction);
			health = health - Mathf.RoundToInt (playerDamageTaken);

		} else if (this.player.GetComponent<Player> ().Level < EnemyLevel-4) {
			float playerDamageTaken = playerDamage * (1.45f - this.player.GetComponent<Player> ().DamageReduction);
			health = health - Mathf.RoundToInt (playerDamageTaken);
		
		}

		if (this.player.GetComponent<Player> ().Level == EnemyLevel+2) {
			float playerDamageTaken = playerDamage * (1.2f - this.player.GetComponent<Player> ().DamageReduction);
			health = health - Mathf.RoundToInt (playerDamageTaken);

		} else if (this.player.GetComponent<Player> ().Level == EnemyLevel+3) {
			float playerDamageTaken = playerDamage * (1.3f - this.player.GetComponent<Player> ().DamageReduction);
			health = health - Mathf.RoundToInt (playerDamageTaken);

		} else if (this.player.GetComponent<Player> ().Level == EnemyLevel+4) {
			float playerDamageTaken = playerDamage * (1.4f - this.player.GetComponent<Player> ().DamageReduction);
			health = health - Mathf.RoundToInt (playerDamageTaken);

		} else if (this.player.GetComponent<Player> ().Level > EnemyLevel+4) {
			float playerDamageTaken = playerDamage * (1.45f - this.player.GetComponent<Player> ().DamageReduction);
			health = health - Mathf.RoundToInt (playerDamageTaken);
		}

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
			Mindamage = 2*(EnemyLevel+2);
			MaxDamage = 4*(EnemyLevel+2);
			damage  = Random.Range(Mindamage, MaxDamage);

			//defensive attributes
			enemyArmor = enemyArmor + (enemyArmor * EnemyLevel);

		} else if (Elite != true) {
			health = (health) * EnemyLevel;

			//mob damage
			Mindamage = 1*(EnemyLevel);
			MaxDamage = 4*(EnemyLevel);
			damage  = Random.Range(Mindamage, MaxDamage);
		}
	}

	void Attack () {
		EnemyPowerAndLevel ();

		if (this.player.GetComponent<Player> ().isDead == false && targetSeen == true) {
			this.player.gameObject.transform.GetComponent<Player>().GetHit (damage);
			Debug.Log ("Enemy damage: " + damage);

			} else {
				//dont attack and move away from

			}
		}
	}