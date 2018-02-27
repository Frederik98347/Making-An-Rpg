using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour{
    //GUI
    [SerializeField] Interactable focus;
    [SerializeField] CharacterHealthsytem healthsystem;
    [SerializeField] Enemy enemyScript;
    [SerializeField] UserMovement usermovement;
    //CreateNewCharacter character;
   // public int _characterIndex;

    //tooltip GUI
    public bool hoverOverActive;
    public string hoverName;
    public GameObject selectedUnit;

    // auto attack timer
    public bool autoAttacking = false;
    public float autoAttackcurTime;
    public float autoAttackCD = 1.8f;
    [Range(0.1f,3f)]
    public float attackRangeMulti = .8f;

    //stats
    private int MinDamage = 1;
    private int MaxDamage = 4;

    //bools to check enemy location
    public bool behindEnemy = false;
    public bool canAttack = false;

    private int autoattackDamage;
    private float autoattackRange;

    public int AutoAttackDamage {
        get { return autoattackDamage; }
        set { autoattackDamage = value; }
    }

    public float AutoattackRange
    {
        get
        {
            return autoattackRange;
        }

        set
        {
            autoattackRange = value;
        }
    }

    // Use this for initialization
    void Start() {
        if (focus)
        {
            AutoattackRange = focus.radius;
        }
    }

    // Update is called once per frame
    void Update() {

        OnMouseEnter();
        if (selectedUnit != null)
        {
            AutoAttack();
        }

        //if dead 
        //Isdead(); // doesnt work
    }

    void OnMouseEnter() {

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
	void AutoAttack() {
		if (selectedUnit != null && focus != null) {
            if (focus != selectedUnit)
            {
                focus.OnDefocused();
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

            if (canAttack == true && autoAttacking == true && enemyScript.state != Enemy.State.Dead && Vector3.Distance(this.transform.position, selectedUnit.transform.position) < AutoattackRange * attackRangeMulti)
            {
                autoAttackcurTime += Time.deltaTime;
                //count up

                if (autoAttackcurTime >= autoAttackCD)
                {
                    // no cd on autoattack
                    AutoAttackDamage = Random.Range(MinDamage, MaxDamage);
                    enemyScript.GetHit(AutoAttackDamage);
                    autoAttacking = true;
                    autoAttackcurTime = 0;
                }
            }
            else if (Vector3.Distance(this.transform.position, selectedUnit.transform.position) > AutoattackRange * attackRangeMulti)
            {
                autoAttacking = false;
                autoAttackcurTime = 0;
                Debug.Log("Out of Range");
            }
        }
	}

    /*
    public void GetHit (int enemyDamage) {

		health = health - enemyDamage;
		Debug.Log ("Playerhp: " + health);

		if (health <= 0) {
			//player is dead
			isDead = true;
			Debug.Log("Player is dead");
			health = 0;
		}
	}
    */
	/*void Isdead() {
		if (isDead == true) {
			float Speed = usermovement.runSpeed;

			Speed = (Speed + Speed)*1.2f;
			canAttack = false;

			//making sure enemies cant attack or see you
			while (isDead == true) {
				enemyScript.TargetSeen = false;
				enemyScript._enemyInfo.DetectionRange = 0.0f;
				enemyScript.OutofrangeTimer = 0.0f;
                enemyScript.state = "patrol";
                
			}

            //Destroy(this.gameObject, 2.0f);
		}
	}*/
}