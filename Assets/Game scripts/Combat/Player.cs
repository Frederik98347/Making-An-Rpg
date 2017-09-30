using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour {

	//GUI
	public Slider healthBar; 
	public Enemy enemyScript;
	public UserMovement usermovement;
	bool expGain = false;

	public string CharacterName;
	public int Level = 1;
	private int Experience = 0;
	public bool isDead = false;

	// DEFENSIVE SKILL VALUES
	int armor = 0;
	int defense = 0;

	//tooltip GUI
	public bool hoverOverActive;
	public string hoverName;
	public GameObject selectedUnit;

	// auto attack timer
	public bool autoAttacking = false;
	public float autoAttackcurTime;
	public float autoAttackCD = 1.5f;

	//health atributes
	float MaxHP = 30f;
	protected float health  = 30f;

	public int Defense {
		get{return defense; }
		set{defense = value; }
	}
	public float DamageReduction = 0f;
	//public float DamageReduction = (armor/ 2f + Defense) / 100f;

	public float range  = 10f;
	private int MinDamage  = 1;
	private int MaxDamage =  4;
	//public Transform selectedUnit;

	//bools to check enemy location
	public bool behindEnemy = false;
	public bool canAttack = false;

	private int autoattackDamage;
	public int AutoAttackDamage {
		get{return autoattackDamage; }
		set{autoattackDamage = value; }
	}
	// Use this for initialization
	void Start () {
		healthBar.value = 0;
	}

	float CalculateHealth() {
		return health / MaxHP;
	}
	
	// Update is called once per frame
	void Update () {
		
		expToLevel ();
		OnMouseEnter ();

		//if dead 
		//Isdead (); // doesnt work
	}

	public void Attack(){
		Debug.Log ("Attack");
	}

	public void OnMouseEnter () {
		//Debug.Log ("Tooltip: Enemyfound");

		if (Input.GetKeyDown(KeyCode.Escape)) {
			autoAttacking = false;
			autoAttackcurTime = 0;
			selectedUnit = null;
			canAttack = false;
			print ("DESELECT");
		}

		if (Input.GetMouseButton (0)) {
			SelectselectedUnit ();
				
		
		} else if (Input.GetMouseButtonUp (1)) {
			SelectselectedUnit ();
			autoAttacking = true;
		}

		if (canAttack == true && autoAttacking == true && enemyScript.Dead != true) {
			if (autoAttackcurTime < autoAttackCD) {
				autoAttackcurTime += Time.deltaTime;
				//count up

			} else {
				// no cd on autoattack
				AutoAttack ();
				autoAttacking = true;
				autoAttackcurTime = 0;
			}

		} else {
			autoAttacking = false;
			autoAttackcurTime = 0;
		}
		//string Tooltip  = "Level " + this.selectedUnit.GetComponent<Enemy>().EnemyLevel + " : Normal Monster Type /n" + " Monster Health: " + this.selectedUnit.GetComponent<Enemy>().HP;
	}

	void SelectselectedUnit () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 5000)) {

			if (hit.transform.tag == "enemy") {

				selectedUnit = hit.transform.gameObject;

				enemyScript = this.selectedUnit.transform.gameObject.transform.GetComponent<Enemy>();
			}
		}
	}

	void AutoAttack() {
		if (selectedUnit != null) {
			//Vectors
			Vector3 direction = selectedUnit.transform.position - this.transform.position;
			float angle = Vector3.Angle (direction, this.transform.forward);

			if (angle > 60.0f) {

				canAttack = false;
			} else {

				canAttack = true;
			}

			if (Vector3.Dot (direction, selectedUnit.transform.forward) < 0) {
				behindEnemy = false;

			} else {
				behindEnemy = true;
			}
		}
			
			//Range
		if (enemyScript.Dead == false) {
			if (Vector3.Distance (selectedUnit.transform.position, transform.position) < range && canAttack == true) {
				AutoAttackDamage = Random.Range (MinDamage, MaxDamage);
				Debug.Log ("AutoAttack: " + AutoAttackDamage);
				enemyScript.GetHit (AutoAttackDamage);

			} else {
				autoAttacking = false;
				Debug.Log ("Out of Range");
			}
		}
	}

	void expToLevel() { // experience function
		int expfunction = Mathf.RoundToInt(Level*Level * 5 + 295);

			if (isDead != true && selectedUnit != null) {
			if (selectedUnit.GetComponent <Enemy> ().health <= 0 && selectedUnit.GetComponent<Enemy> ().Dead == true) { 
					expGain = true;

						if (expGain == true) {
							// check if experience is earned

					if (this.selectedUnit.GetComponent<Enemy> ().EnemyLevel == Level + 2) {
						Experience += Mathf.RoundToInt (this.selectedUnit.GetComponent<Enemy> ().expToGive * 1.2f);

					} else if (this.selectedUnit.GetComponent<Enemy> ().EnemyLevel == Level + 3) {
						Experience += Mathf.RoundToInt (this.selectedUnit.GetComponent<Enemy> ().expToGive * 1.3f);

					} else if (this.selectedUnit.GetComponent<Enemy> ().EnemyLevel == Level + 4) {
						Experience += Mathf.RoundToInt (this.selectedUnit.GetComponent<Enemy> ().expToGive * 1.4f);
						Experience = Mathf.RoundToInt (Experience);
							
					} else if (this.selectedUnit.GetComponent<Enemy> ().EnemyLevel == Level - 2) {
						// give less xp
						Experience += Mathf.RoundToInt (this.selectedUnit.GetComponent<Enemy> ().expToGive / 1.2f);


					} else if (this.selectedUnit.GetComponent<Enemy> ().EnemyLevel == Level - 3) {
						// give less xp
						Experience += Mathf.RoundToInt (this.selectedUnit.GetComponent<Enemy> ().expToGive / 1.3f);

					} else if (this.selectedUnit.GetComponent<Enemy> ().EnemyLevel == Level - 4) {
						// give less xp
						Experience += Mathf.RoundToInt (this.selectedUnit.GetComponent<Enemy> ().expToGive / 1.4f);
					} else if (this.selectedUnit.GetComponent<Enemy> ().EnemyLevel == Level - 5) {
						Experience += 0;
					}

							Experience += this.selectedUnit.GetComponent<Enemy> ().expToGive;
						}

							if (Experience == expfunction || Experience > expfunction) {
								bool ding = true;

								if (ding == true && Experience > expfunction) {
									Debug.Log ("Ding Level:  " + Level);
									Debug.Log (Experience + " out of " + expfunction);
									Experience = expfunction - Experience;
									Level++;
									ding = true;

								} else if (ding == true) {
									Level++;
									Experience = 0;
									Debug.Log ("Ding Level:  " + Level);
									Debug.Log (Experience + " out of " + expfunction);
									ding = true;
								}
							}
			} else {
				expGain = false;
			}
		}
	}

	public void GetHit (int enemyDamage) {
		if (selectedUnit != null) {

			/*if (this.selectedUnit.GetComponent<Enemy> ().EnemyLevel == Level - 2) {
				float DamageTaken = enemyDamage / (1.2f + this.selectedUnit.GetComponent<Enemy> ().enemyArmor);
				health = health - Mathf.RoundToInt (DamageTaken);
			} else if (this.selectedUnit.GetComponent<Enemy> ().EnemyLevel == Level - 3) {
				float DamageTaken = enemyDamage / (1.3f + this.selectedUnit.GetComponent<Enemy> ().enemyArmor);
				health = health - Mathf.RoundToInt (DamageTaken);
			} else if (this.selectedUnit.GetComponent<Enemy> ().EnemyLevel == Level - 4) {
				float DamageTaken = enemyDamage / (1.4f + this.selectedUnit.GetComponent<Enemy> ().enemyArmor);
				health = health - Mathf.RoundToInt (DamageTaken);
			}

			if (this.selectedUnit.GetComponent<Enemy> ().EnemyLevel == Level + 2) {
				float DamageTaken = enemyDamage * (1.2f + this.selectedUnit.GetComponent<Enemy> ().enemyArmor);
				health = health - Mathf.RoundToInt (DamageTaken);
			} else if (this.selectedUnit.GetComponent<Enemy> ().EnemyLevel == Level + 3) {
				float DamageTaken = enemyDamage * (1.3f + this.selectedUnit.GetComponent<Enemy> ().enemyArmor);
				health = health - Mathf.RoundToInt (DamageTaken);
			} else if (this.selectedUnit.GetComponent<Enemy> ().EnemyLevel == Level + 4) {
				float DamageTaken = enemyDamage * (1.4f + this.selectedUnit.GetComponent<Enemy> ().enemyArmor);
				health = health - Mathf.RoundToInt (DamageTaken);
			}*/
		}

		health = health - enemyDamage;
		healthBar.value = CalculateHealth();
		Debug.Log (health);

		if (health <= 0) {
			//player is dead
			isDead = true;
			Debug.Log("Player is dead");
			health = 0;
		}
	}

	void Isdead() {
		while (isDead == true) {
			float Speed = usermovement.runSpeed;

			Speed = Speed + Speed;
			canAttack = false;
			expGain = false;

			//making sure enemies cant attack or see you
			enemyScript.targetSeen = false;
			enemyScript.detectionRange = 0.0f;
			enemyScript.outofrangeTimer = 0.0f;
		}
	}
}