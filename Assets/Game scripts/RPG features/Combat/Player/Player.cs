using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour{

    #region Variables
    //GUI
    [SerializeField] Interactable focus;
    CharacterHealthsytem healthsystem;
    [SerializeField] Enemy enemyScript;
    [SerializeField] UserMovement usermovement;
    BasePlayer basePlayer;
    //CreateNewCharacter character;
    // public int _characterIndex;

    //tooltip GUI
    public State state;
    public GameObject selectedUnit;
    public string Playername;
    public string Playerclass;
    float health = 20f;

    // auto attack timer
    public bool autoAttacking = false;
    float autoAttackcurTime;
    public float autoAttackCD = 1.8f;

    //stats
    private int MinDamage = 1;
    private int MaxDamage = 4;

    //bools to check enemy location
    public bool behindEnemy = false;
    bool canAttack = false;

    private int autoattackDamage;
    private float autoattackRange = 3f;
    private float CombatCounter;
    private float CombatTime = 10f;

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
    #endregion

    // Use this for initialization
    void Start() {
        if (basePlayer != null)
        {
            basePlayer.PlayerName = Playername;
            basePlayer.PlayerClass.CharacterClassName = Playerclass;
        }

        if (focus)
        {
            AutoattackRange = focus.radius;
        }

        if (healthsystem == null)
        {
            healthsystem = GetComponent<CharacterHealthsytem>();
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
        Isdead(); // doesnt work
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
            if (i == 2 && state != State.DEAD) {
                autoAttacking = true;
                i = 0;
            }


        } else if (Input.GetMouseButtonDown(1)) {
            SelectselectedUnit();
            if (state != State.DEAD)
            {
                autoAttacking = true;
            }
        }
    }

    void GettingOutOfCombat()
    {
        if (state == State.COMBAT)
        {
            if (CombatCounter < CombatTime)
            {
                //count up
                CombatCounter += Time.deltaTime;

            }
            else
            {
                state = State.Alive;
                CombatCounter = 0;
                HPregain();
            }
        }
    }

    void HPregain()
    {
        if (state == State.Alive)
        {
            if (healthsystem.CurrentHealth < healthsystem.MaxHealth)
            {
                healthsystem.CurrentHealth += (int)(0.05f * healthsystem.MaxHealth) * Time.deltaTime;

                if (healthsystem.CurrentHealth >= healthsystem.MaxHealth)
                {
                    healthsystem.CurrentHealth = healthsystem.MaxHealth;
                }
            }
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
        if (state != State.DEAD)
        {
            if (selectedUnit != null && focus != null)
            {
                if (focus != selectedUnit)
                {
                    focus.OnDefocused();
                }
                //Vectors
                Vector3 direction = selectedUnit.transform.position - this.transform.position;
                float angle = Vector3.Angle(direction, this.transform.forward);

                if (angle > 90.0f)
                {

                    canAttack = false;
                }
                else
                {

                    canAttack = true;
                }

                if (Vector3.Dot(direction, selectedUnit.transform.forward) < 0)
                {
                    behindEnemy = false;

                }
                else
                {
                    behindEnemy = true;
                }

                if (canAttack == true && autoAttacking == true && enemyScript.state != Enemy.State.Dead && Vector3.Distance(this.transform.position, selectedUnit.transform.position) < AutoattackRange)
                {
                    state = State.COMBAT;
                    autoAttackcurTime += Time.deltaTime;
                    //count up

                    if (autoAttackcurTime >= autoAttackCD)
                    {
                        // no cd on autoattack
                        DoAutoDamage();
                        autoAttackcurTime = 0;
                    }
                }
                else if (Vector3.Distance(transform.position, selectedUnit.transform.position) > AutoattackRange)
                {
                    autoAttacking = false;
                    canAttack = false;
                    autoAttackcurTime = 0;

                    // Check if no enemies is around and then run the script
                    GettingOutOfCombat();
                }
            }
        }
	}

    public void GetHit(int damage)
    {
        if (healthsystem.IsDead == true)
        {
            //Player is dead
            state = State.DEAD;
        } else
        {
            healthsystem.GetHit(damage);
        }
    }

    void DoAutoDamage()
    {
        AutoAttackDamage = Random.Range(MinDamage, MaxDamage);
        enemyScript.GetHit(AutoAttackDamage);
    }

    public enum State
    {
        Alive = 0,
        DEAD,
        COMBAT
    }

    void Isdead() {
		if (state == State.DEAD) {
			float Speed = usermovement.runSpeed;

			Speed = (Speed + Speed)*1.2f;
			canAttack = false;
            autoAttacking = false;
            if (enemyScript != null)
            {
                enemyScript.TargetSeen = false;
                enemyScript._enemyInfo.DetectionRange = 0.0f;
                enemyScript.OutofrangeTimer = 0.0f;
                enemyScript.state = Enemy.State.Patrol;
            }

		}

        //Destroy(this.gameObject, 2.0f);
    }
}