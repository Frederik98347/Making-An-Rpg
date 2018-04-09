using UnityEngine;
using EnemyTypes;
using RpgTools.Save;
using RpgTools.Enemy;
namespace RpgTools.PlayerClass
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(UserMovement))]
    [RequireComponent(typeof(CharacterHealthsytem))]
    public class Player : MonoBehaviour
    {

        #region Variables
        //GUI
        [SerializeField] Interactable focus;
        CharacterHealthsytem healthsystem;
        [SerializeField] Enemy.Enemy enemyScript;
        [SerializeField] UserMovement usermovement;

        //tooltip GUI
        public State state;
        public GameObject selectedUnit;
        [SerializeField] string Playername;
        public string Playerclass;
        float health = 20f;
        [SerializeField] float HPRegain = 0.05f;

        // auto attack timer
        public bool autoAttacking = false;
        float autoAttackcurTime;
        public float autoAttackCD = 1.8f;

        //stats
        private int MinDamage = 1;
        private int MaxDamage = 4;

        //bools to check enemy location
        public bool behindEnemy = false;
        [SerializeField] bool CombatHP_regain;
        bool canAttack = false;

        private int autoattackDamage;
        private float autoattackRange = 3f;
        private float CombatCounter;
        private float CombatTime = 10f;

        [SerializeField] float attackArc = 120f;

        public int AutoAttackDamage
        {
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

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        // Use this for initialization
        void Start()
        {
            /*if (basePlayer != null)
            {
                basePlayer.PlayerName = Playername;
                basePlayer.PlayerClass.CharacterClassName = Playerclass;
            }*/

            if (focus)
            {
                AutoattackRange = focus.radius;
            }

            if (healthsystem == null)
            {
                healthsystem = GetComponent<CharacterHealthsytem>();
            }

            healthsystem.MaxHealth = this.Health;
            healthsystem.CurrentHealth = this.healthsystem.MaxHealth;
            healthsystem.healthBar.fillAmount = healthsystem.CalculateHealth();

        }

        // Update is called once per frame
        void Update()
        {
            OnMouseEnter();
            if (selectedUnit != null)
            {
                AutoAttack();
            }

            TestDamage();

            //if dead 
            Isdead(); // doesnt work

            HPregain(HPRegain, CombatHP_regain);
        }

        void OnMouseEnter()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                autoAttacking = false;
                autoAttackcurTime = 0;
                RemoveFocus();
                canAttack = false;
                print("DESELECT");
            }
            if (Input.GetMouseButtonDown(0))
            {
                SelectselectedUnit();
                int i = 0;
                i = i + 1;
                if (i == 2 && state != State.DEAD)
                {
                    autoAttacking = true;
                    i = 0;
                }


            }
            else if (Input.GetMouseButtonDown(1))
            {
                SelectselectedUnit();
                if (state != State.DEAD && selectedUnit != null)
                {
                    if (Vector3.Distance(this.transform.position, selectedUnit.transform.position) <= 20f)
                    {
                        autoAttacking = true;
                    }
                }
            }
        }

        void TestDamage()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                GetHit(Random.Range(1, 10));
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                GetHealth(Random.Range(1, 10));
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
                    state = State.OUTOFCOMBAT;
                    CombatCounter = 0;
                }
            }
        }

        void HPregain(float hpregain, bool CombatRegain)
        {

            if (state == State.OUTOFCOMBAT)
            {
                if (healthsystem.CurrentHealth < healthsystem.MaxHealth)
                {
                    healthsystem.CurrentHealth += (int)((hpregain * healthsystem.MaxHealth) * Time.deltaTime);

                    if (healthsystem.CurrentHealth >= healthsystem.MaxHealth)
                    {
                        healthsystem.CurrentHealth = healthsystem.MaxHealth;
                    }
                }
            }
            else if (state == State.COMBAT && CombatRegain)
            {
                if (healthsystem.CurrentHealth < healthsystem.MaxHealth)
                {
                    healthsystem.CurrentHealth += (int)((hpregain * healthsystem.MaxHealth) * Time.deltaTime);

                    if (healthsystem.CurrentHealth >= healthsystem.MaxHealth)
                    {
                        healthsystem.CurrentHealth = healthsystem.MaxHealth;
                    }
                }
            }
        }

        void SelectselectedUnit()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 500))
            {

                Interactable interactable = hit.collider.GetComponent<Interactable>();
                //checking if interactable is hit
                if (interactable != null)
                {
                    //set as focus
                    SetFocus(interactable);

                    if (interactable.transform.tag == "enemy")
                    {

                        if (Vector3.Distance(this.transform.position, interactable.transform.position) <= 40f)
                        {
                            selectedUnit = interactable.transform.gameObject;
                            enemyScript = this.selectedUnit.transform.GetComponent<Enemy.Enemy>();
                        }
                    }
                }
            }
        }

        void SetFocus(Interactable newFocus)
        {
            if (newFocus != focus || selectedUnit != focus)
            {
                if (focus != null)
                {
                    focus.OnDefocused();
                }
                else if (selectedUnit != null && newFocus != selectedUnit && selectedUnit != focus)
                {
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

        void AutoAttack()
        {
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

                    if (angle > attackArc)
                    {
                        //frontal
                        canAttack = false;
                        autoAttacking = true;
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

                    if (autoAttacking == true && enemyScript.state != Enemy.Enemy.State.Dead && Vector3.Distance(this.transform.position, selectedUnit.transform.position) < AutoattackRange)
                    {
                        state = State.COMBAT;
                        autoAttackcurTime += Time.deltaTime;
                        //count up

                        if (autoAttackcurTime >= autoAttackCD && canAttack == true)
                        {
                            // no cd on autoattack
                            DoAutoDamage();
                            autoAttackcurTime = 0;
                        }
                    }
                    else if (Vector3.Distance(transform.position, selectedUnit.transform.position) > AutoattackRange)
                    {
                        autoAttacking = false;
                        //canAttack = false;
                        autoAttackcurTime = 0;

                        // Check if no enemies is around and then run the script
                        GettingOutOfCombat();
                    } else if (enemyScript.state == Enemy.Enemy.State.Dead)
                    {
                        canAttack = false;
                        autoAttacking = false;
                    }
                }
            }
        }

        public void GetHit(int damage, MobDmgTypes damageType = MobDmgTypes.PHYSICAL)
        {
            if (healthsystem.IsDead == true)
            {
                //Player is dead
                state = State.DEAD;
            }
            else
            {
                state = State.COMBAT;
                this.healthsystem.GetHit(damage);
                Debug.Log(damageType);
            }
        }

        public void GetHealth(int HealthValue)
        {
            if (this.healthsystem.IsDead == true)
            {
                //Is dead
                Isdead();
                state = State.DEAD;
            }
            else
            {
                this.healthsystem.GetHealth(HealthValue);
            }
        }

        void DoAutoDamage(PlayerDmgTypes damageType = PlayerDmgTypes.PHYSICAL)
        {
            AutoAttackDamage = Random.Range(MinDamage, MaxDamage);
            enemyScript.GetHit(AutoAttackDamage, damageType = PlayerDmgTypes.PHYSICAL);
        }

        public enum State
        {
            OUTOFCOMBAT = 0,
            DEAD,
            COMBAT
        }

        void Isdead()
        {
            /*if (state == State.DEAD)
            {
                float Speed = usermovement.runSpeed;

                Speed = 0;
                canAttack = false;
                autoAttacking = false;
                if (enemyScript != null)
                {
                    //enemyScript.TargetSeen = false;
                    //enemyScript._enemyInfo.DetectionRange = 0.0f;
                    enemyScript.OutofrangeTimer = 0.0f;
                    enemyScript.state = Enemy.Enemy.State.Patrol;
                }

            }*/

            //Destroy(this.gameObject, 2.0f);
        }
    }
}