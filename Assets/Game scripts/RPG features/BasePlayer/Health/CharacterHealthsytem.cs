using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// CharacterHealthSystem that both does Text, Ui sliders and health calculations
/// </summary>
namespace RpgTools
{
    public class CharacterHealthsytem : MonoBehaviour
    {
        public bool isPlayer;
        public bool isEnemy;
        [SerializeField] float currentHealth;
        [SerializeField] float maxHealth;
        float percentageHealth;
        string outofHP;
        string hpGained;
        string hpLost;
        bool isDead;
        public bool showText = true;

        public TMP_Text HpBarText;
        [SerializeField] bool isPercentageHp;
        [SerializeField] bool isPercentageNnumbers;

        public Image healthBar;
        public Slider TargetBar;

        #region Init
        public float CurrentHealth
        {
            get
            {
                return currentHealth;
            }

            set
            {
                currentHealth = value;
            }
        }

        public float MaxHealth
        {
            get
            {
                return maxHealth;
            }

            set
            {
                maxHealth = value;
            }
        }

        public float PercentageHealth
        {
            get
            {
                return percentageHealth;
            }

            set
            {
                percentageHealth = value;
            }
        }

        public string OutofHP
        {
            get
            {
                return outofHP;
            }

            set
            {
                outofHP = value;
            }
        }

        public string HpLost
        {
            get
            {
                return hpLost;
            }

            set
            {
                hpLost = value;
            }
        }

        public string HpGained
        {
            get
            {
                return hpGained;
            }

            set
            {
                hpGained = value;
            }
        }

        public bool IsDead
        {
            get
            {
                return isDead;
            }

            set
            {
                isDead = value;
            }
        }
        #endregion // All private variables init into public 

        void Start()
        {
            if (isEnemy == true)
            {
                MaxHealth = transform.GetComponent<Enemy.Enemy>().MaxHealth;
                CurrentHealth = MaxHealth;
                TargetBar.value = CalculateHealth();
            } else if(isPlayer == true)
            {
                MaxHealth = transform.GetComponent<PlayerClass.Player>().Health;
                CurrentHealth = MaxHealth;
                healthBar.fillAmount = CalculateHealth();
            }

            if (HpBarText != null)
            {
                ShowText();

            }
        }

        void Update()
        {
            if (HpBarText != null)
            {
                ShowText();

            }
        }

        public void GetHit(int damageValue)
        {
            //Deduct the damage deatlh from the character's health
            CombatTextManager.Instance.CreateText(transform.position, true, false, false, false, damageValue.ToString());

            CurrentHealth -= damageValue;

            if (isEnemy == true)
            {
                MaxHealth = transform.GetComponent<Enemy.Enemy>().MaxHealth;
                TargetBar.value = CalculateHealth();
            }
            else if (isPlayer == true)
            {
                MaxHealth = transform.GetComponent<PlayerClass.Player>().Health;
                healthBar.fillAmount = CalculateHealth();
            }

            //making sure health cant go below 0%

            if (CurrentHealth <= 0)
            {
                Die();
                IsDead = true;
            }
            else
            {
                IsDead = false;
            }
        }

        public void GetHealth(int healValue)
        {
            if (!isDead)
                //Deduct the damage deatlh from the character's health
                CombatTextManager.Instance.CreateText(transform.position, false, true, false, false, healValue.ToString());

            CurrentHealth += healValue;
            if (isEnemy == true)
            {
                TargetBar.value = CalculateHealth();
            }
            else if (isPlayer == true)
            {
                healthBar.fillAmount = CalculateHealth();
            }
            //HpGained = "Health +" + healValue;

            //making sure Health can go above 100%
            if (CurrentHealth >= MaxHealth)
            {
                IsDead = false;
                CurrentHealth = MaxHealth;
                if (isEnemy == true)
                {
                    TargetBar.value = CalculateHealth();
                }
                else if (isPlayer == true)
                {
                    healthBar.fillAmount = CalculateHealth();
                }

                if (HpBarText != null)
                {
                    ShowText();
                }
            }
            else
            {
                IsDead = false;
            }
        }

        void PercentageWithNumbers()
        {
            if (IsDead != true)
            {
                isPercentageHp = false;
                string PercentNnumber = CurrentHealth + " / " + MaxHealth + " (" + CalculateHealth() * 100f + "%)";
                SetHpText(PercentNnumber);
            }
            else
            {
                isPercentageHp = false;
                string PercentNnumber = CurrentHealth + " / " + MaxHealth + " (" + CalculateHealth() * 100f + "% Dead)";
                SetHpText(PercentNnumber);
            }
        }

        void PercentageHPCalc()
        {
            if (IsDead != true)
            {
                isPercentageNnumbers = false;
                string PercentageHealth = CalculateHealth() * 100f + "%";
                SetHpText(PercentageHealth);
            }
            else
            {
                isPercentageNnumbers = false;
                string PercentageHealth = CalculateHealth() * 100f + "%" + " Dead";
                SetHpText(PercentageHealth);
            }
        }

        public float CalculateHealth()
        {
            return CurrentHealth / MaxHealth;
        }

        void OutofHPCalc()
        {
            if (IsDead != true)
            {
                isPercentageNnumbers = false;
                isPercentageHp = false;
                OutofHP = CurrentHealth + " / " + MaxHealth;
                SetHpText(OutofHP);
            }
            else
            {
                isPercentageNnumbers = false;
                isPercentageHp = false;
                OutofHP = CurrentHealth + " / " + MaxHealth + " Dead";
                SetHpText(OutofHP);
            }
        }

        void SetHpText(string text)
        {
            HpBarText.GetComponent<TMP_Text>().text = text;
        }

        void ShowText()
        {
            if (showText == false)
            {
                // dont show text On HP bar
                SetHpText("");
            }
            else
            {
                if (isPercentageNnumbers == true)
                {
                    PercentageWithNumbers();
                }
                else if (isPercentageHp == true)
                {
                    PercentageHPCalc();
                }
                else
                {
                    OutofHPCalc();
                }
            }
        }

        void Die()
        {
            CurrentHealth = 0;
            if (isEnemy == true)
            {
                TargetBar.value = CalculateHealth();
            }
            else if (isPlayer == true)
            {
                healthBar.fillAmount = CalculateHealth();
            }
            IsDead = true;
            if (HpBarText != null)
            {
                if (isPercentageHp == true)
                {
                    PercentageHPCalc();
                }
                else if (isPercentageNnumbers == true)
                {
                    PercentageWithNumbers();
                }
                else
                {
                    OutofHPCalc();
                }
            }
        }
    }
}