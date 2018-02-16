﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour {
    //GUI
    public Interactable focus;
    public Slider healthBar;
    public Enemy enemyScript;
    public UserMovement usermovement;
    bool expGain = false;
    public int Level = 1;
    private int Experience = 0;
    public bool isDead = false;

    // DEFENSIVE SKILL VALUES
    /*	int armor = 0;*/
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
    public float health = 0f;

    public int Defense {
        get { return defense; }
        set { defense = value; }
    }
    public float DamageReduction = 0f;

    private int MinDamage = 1;
    private int MaxDamage = 4;

    //bools to check enemy location
    public bool behindEnemy = false;
    public bool canAttack = false;

    private int autoattackDamage;
    private float autoattackRange = 3.0f;

    public int AutoAttackDamage {
        get { return autoattackDamage; }
        set { autoattackDamage = value; }
    }
    // Use this for initialization
    void Start() {
        healthBar.value = 1f;
        health = MaxHP;
    }

    float CalculateHealth() {
        return health / MaxHP;
    }

    // Update is called once per frame
    void Update() {

        expToLevel();
        OnMouseEnter();
        if (selectedUnit != null)
        {
            AutoAttack();
        }

        if (enemyScript.Dead == true)
        {
            RemoveFocus();
        }

        //if dead 
        //Isdead(); // doesnt work
    }

    public void Attack() {
        Debug.Log("Attack");
    }

    public void OnMouseEnter() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            autoAttacking = false;
            autoAttackcurTime = 0;
            RemoveFocus();
            canAttack = false;
            print("DESELECT");
        }

        if (Input.GetMouseButton(0)) {
            SelectselectedUnit();
            int i = 0;
            i = i + 1;
            if (i == 2) {
                autoAttacking = true;
                i = 0;
            }


        } else if (Input.GetMouseButtonDown(1)) {
            SelectselectedUnit();
            autoAttacking = true;
        }
    }

    void SelectselectedUnit() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500)) {

            Interactable interactable = hit.collider.GetComponent<Interactable>();
            //checking if interactable is hit
            if (interactable != null) {
                //set as focus
                SetFocus(interactable);

                if (interactable.transform.tag == "enemy")
                {
                    selectedUnit = interactable.transform.gameObject;
                    enemyScript = this.selectedUnit.transform.gameObject.transform.GetComponent<Enemy>();
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus || selectedUnit != focus)
        {
            if (focus != null) {
                focus.OnDefocused();
            } else if (selectedUnit != null && newFocus != selectedUnit && selectedUnit != focus){
                RemoveFocus();
            }

            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
        selectedUnit = null;
        enemyScript = null;
        canAttack = false;
        autoAttacking = false;
        behindEnemy = false;
    }
	public void AutoAttack() {
		if (selectedUnit != null && focus != null) {
            if (focus != selectedUnit)
            {
                
            }
			//Vectors
			Vector3 direction = selectedUnit.transform.position - this.transform.position;
			float angle = Vector3.Angle (direction, this.transform.forward);

			if (angle > 90.0f) {

				canAttack = false;
			} else {

				canAttack = true;
			}

			if (Vector3.Dot (direction, selectedUnit.transform.forward) < 0) {
				behindEnemy = false;

			} else {
				behindEnemy = true;
			}

            if (canAttack == true && autoAttacking == true && enemyScript.Dead != true && Vector3.Distance(this.transform.position, selectedUnit.transform.position) < autoattackRange * 0.8f)
            {
                autoAttackcurTime += Time.deltaTime;
                //count up

                if (autoAttackcurTime >= autoAttackCD)
                {
                    // no cd on autoattack
                    AutoAttackDamage = Random.Range(MinDamage, MaxDamage);
                    Debug.Log("AutoAttack: " + AutoAttackDamage);
                    enemyScript.GetHit(AutoAttackDamage);
                    autoAttacking = true;
                    autoAttackcurTime = 0;
                }
            }
            else if (Vector3.Distance(this.transform.position, selectedUnit.transform.position) > autoattackRange * 2f)
            {
                autoAttacking = false;
                autoAttackcurTime = 0;
                Debug.Log("Out of Range");
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
									Level = Level+1;
									ding = true;

								} else if (ding == true && Experience == expfunction) {
									Level = Level+1;
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
		if (isDead == true) {
			float Speed = usermovement.runSpeed;

			Speed = (Speed + Speed)*1.2f;
			canAttack = false;
			expGain = false;

			//making sure enemies cant attack or see you
			while (isDead == true) {
				enemyScript.targetSeen = false;
				enemyScript.detectionRange = 0.0f;
				enemyScript.outofrangeTimer = 0.0f;
                enemyScript.state = "patrol";
                
			}

            Destroy(this.gameObject, 2.0f);
		}
	}
}