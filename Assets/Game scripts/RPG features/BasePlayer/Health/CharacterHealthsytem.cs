using UnityEngine;
using UnityEngine.UI;

public class CharacterHealthsytem : MonoBehaviour {

    float currentHealth;
    float maxHealth;
    float percentageHealth;
    string outofHP;
    string hpGained;
    string hpLost;

    [SerializeField] Slider healthBar;

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

    void Start()
    {
        MaxHealth = 20f;
        // Rests health to full on game load
        CurrentHealth = MaxHealth;

        //setting healthbar value = our healthcalculation
        healthBar.value = CalculateHealth();
    }

    void Update()
    {
        TestDamage();
    }

    void GetHit(float damageValue)
    {
        FloatingTextController.Init(true, false);
        //Deduct the damage deatlh from the character's health
        CurrentHealth -= damageValue;
        healthBar.value = CalculateHealth();
        FloatingTextController.CreateFloatingText(damageValue.ToString(), transform);
        HpLost = "Health -" + damageValue;

        PercentageHPCalc();

        //making sure health cant go below 0%

        if (CurrentHealth <= 0)
        {
            Die();
            Debug.Log("0% Health");
        } else
        {
            Debug.Log(PercentageHealth + "% Health");
        }
    }

    void GetHealth(float healValue)
    {
        FloatingTextController.Init(false, true);
        //Deduct the damage deatlh from the character's health
        CurrentHealth += healValue;
        healthBar.value = CalculateHealth();
        FloatingTextController.CreateFloatingText(healValue.ToString(), transform);
        HpGained = "Health +" + healValue;

        //Calculation %HP
        PercentageHPCalc();

        //making sure Health can go above 100%
        if (CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
            Debug.Log("100% Health");
        } else
        {
            Debug.Log(PercentageHealth + "% Health");
        }
    }

    void PercentageHPCalc()
    {

        PercentageHealth = CalculateHealth() * 100f;
    }

    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    void OutofHPCalc()
    {
        OutofHP = CurrentHealth + " / " + MaxHealth;
    }

    void Die()
    {
        CurrentHealth = 0;
    }

    void TestDamage()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetHit(6);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            GetHealth(6);
        }
    }
}