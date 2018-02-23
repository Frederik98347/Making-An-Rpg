using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// CharacterHealthSystem that both does Text, Ui sliders and health calculations
/// </summary>
public class CharacterHealthsytem : MonoBehaviour {

    float currentHealth;
    float maxHealth;
    float percentageHealth;
    string outofHP;
    string hpGained;
    string hpLost;
    bool isDead;

    public TMP_Text HpBarText;
    [SerializeField] bool isPercentageHp;
    [SerializeField] bool isPercentageNnumbers;

    [SerializeField] Slider healthBar;
    
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
        MaxHealth = 20f;
        // Rests health to full on game load
        CurrentHealth = MaxHealth;

        //setting healthbar value = our healthcalculation
        healthBar.value = CalculateHealth();

        if (HpBarText != null)
        {
            if (isPercentageHp)
            {
                PercentageHPCalc();
            }
            else if (isPercentageNnumbers)
            {
                PercentageWithNumbers();
            }
            else
            {
                OutofHPCalc();
            }
        }
    }

    void Update()
    {
        TestDamage();
    }

    public void GetHit(float damageValue)
    {
        //Deduct the damage deatlh from the character's health
        CombatTextManager.Instance.CreateText(gameObject.transform.position, true, false, damageValue.ToString());

        CurrentHealth -= damageValue;
        healthBar.value = CalculateHealth();
        //HpLost = "Health -" + damageValue;

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

        //making sure health cant go below 0%

        if (CurrentHealth <= 0)
        {
            Die();
        } else
        {
            IsDead = false;
        }
    }

    public void GetHealth(float healValue)
    {
        //Deduct the damage deatlh from the character's health
        CombatTextManager.Instance.CreateText(gameObject.transform.position, false, true, healValue.ToString());

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

        CurrentHealth += healValue;
        healthBar.value = CalculateHealth();
        //HpGained = "Health +" + healValue;

        //Calculation %HP
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

        //making sure Health can go above 100%
        if (CurrentHealth >= MaxHealth)
        {
            IsDead = false;
            CurrentHealth = MaxHealth;
            healthBar.value = 1;

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
        } else
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
        } else
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
        } else
        {
            isPercentageNnumbers = false;
            string PercentageHealth = CalculateHealth() * 100f + "%" + " Dead";
            SetHpText(PercentageHealth);
        }
    }

    float CalculateHealth()
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
        } else
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

    void Die()
    {
        CurrentHealth = 0;
        healthBar.value = CurrentHealth;
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

    void TestDamage()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetHit(Random.Range(1f,10f));
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            GetHealth(Random.Range(1f, 10f));
        }
    }
}