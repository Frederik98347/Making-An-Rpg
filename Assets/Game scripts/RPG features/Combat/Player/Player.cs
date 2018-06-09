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

        public Animator anim;

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
        public bool frontEnemy = false;
        public bool CombatHP_regain;
        public bool canAttack = false;

        private int autoattackDamage;
        private float autoattackRange = 3f;
        private float CombatCounter;
        private float CombatTime = 10f;

        [SerializeField] float attackArc = 120f;
        private float time;

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

            state = State.OUTOFCOMBAT;
            anim = GetComponent<Animator>();
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

            HPregain(HPRegain, CombatHP_regain);
            if (canAttack == false)
            {
                anim.SetBool("isAttacking", false);
            } else if(autoAttacking == false)
            {
                anim.SetBool("isAttacking", false);
            }

            GettingOutOfCombat();
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

        //working now
        void GettingOutOfCombat()
        {
            //changed something here lately, canAttack == false
            if (state == State.COMBAT && canAttack == false)
            {
                //count up
                CombatCounter += Time.deltaTime;
                if (CombatCounter >= CombatTime)
                {
                    state = State.OUTOFCOMBAT;
                    CombatCounter = 0;
                }
            }
        }

        //Working now
        void HPregain(float hpregain, bool CombatRegain)
        {
            if (state == State.OUTOFCOMBAT)
            {
                time += Time.deltaTime;
                if (time >= 5.0f)
                {
                    if (healthsystem.CurrentHealth < healthsystem.MaxHealth)
                    {
                        //do that every 5 sec until full hp
                        healthsystem.GetHealth((int)(hpregain * healthsystem.MaxHealth)); // 0.05f*maxhp = 5% of maxhp to regain
                        time = 0.0f;
                        Debug.Log("Healing");
                    }
                }
            }
            else if (state == State.COMBAT && CombatRegain == true)
            {
                time += Time.deltaTime;
                if (time >= 5.0f)
                {

                    if (healthsystem.CurrentHealth < healthsystem.MaxHealth)
                    {
                        //do that every 5 sec until full hp
                        healthsystem.GetHealth((int)(hpregain * healthsystem.MaxHealth)); // 0.05f*maxhp = 5% of maxhp to regain
                        time = 0.0f;
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
                        autoAttacking = true;
                        canAttack = true;

                        frontEnemy = true;
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
                            attackAnimation();
                            DoAutoDamage();

                            AbilityIconEffects IconEffects = GetComponent<AbilityIconEffects>(); // getting component of icon effects
                            IconEffects.AttackEffect(); // calling attackeffect

                            autoAttackcurTime = 0;
                        }
                    }
                    else if (Vector3.Distance(transform.position, selectedUnit.transform.position) > AutoattackRange)
                    {
                        autoAttacking = false;
                        //canAttack = false;
                        autoAttackcurTime = 0;

                    } else if (enemyScript.state == Enemy.Enemy.State.Dead)
                    {
                        canAttack = false;
                        autoAttacking = false;

                        //call loot script here
                    }
                }
            }
        }

        void attackAnimation()
        {
            anim.SetBool("isAttacking", true);
            anim.SetBool("isBlocking", false);
            anim.SetBool("isRolling", false);
        }

        public void GetHit(int damage, MobDmgTypes damageType = MobDmgTypes.PHYSICAL)
        {
            if (healthsystem.IsDead == true)
            {
                //Player is dead
                state = State.DEAD;
                Isdead();
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

        public void DoAutoDamage(PlayerDmgTypes damageType = PlayerDmgTypes.PHYSICAL)
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

        //not working
        void Isdead()
        {
            if (state == State.DEAD)
            {
                anim.SetBool("isIdle", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isBlocking", false);
                anim.SetBool("isDying", true);
                anim.SetBool("isRolling", false);
                Debug.Log("Dead mf'er");
            }
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