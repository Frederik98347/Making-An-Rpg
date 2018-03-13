using UnityEngine;

public class CharacterData : ScriptableObject {
    //character data class
    //generic all classes

    public GameObject charModel;
    new string name;
    int maxHealth;
    int currentHealth;
    int critchance;
    int agility;
    int intellect;
    int stamina;
    int strength;
    int haste;
    int defence;
    int resistance;
    int endurance;
    float movementspeed;
    float walkingspeed;
    int level;
    float attackrange;
    float attackspeed;

    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public int MaxHealth
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

    public int CurrentHealth
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

    public int Critchance
    {
        get
        {
            return critchance;
        }

        set
        {
            critchance = value;
        }
    }

    public int Agility
    {
        get
        {
            return agility;
        }

        set
        {
            agility = value;
        }
    }

    public int Intellect
    {
        get
        {
            return intellect;
        }

        set
        {
            intellect = value;
        }
    }

    public int Stamina
    {
        get
        {
            return stamina;
        }

        set
        {
            stamina = value;
        }
    }

    public int Strength
    {
        get
        {
            return strength;
        }

        set
        {
            strength = value;
        }
    }

    public int Defence
    {
        get
        {
            return defence;
        }

        set
        {
            defence = value;
        }
    }

    public int Resistance
    {
        get
        {
            return resistance;
        }

        set
        {
            resistance = value;
        }
    }

    public int Endurance
    {
        get
        {
            return endurance;
        }

        set
        {
            endurance = value;
        }
    }

    public float Movementspeed
    {
        get
        {
            return movementspeed;
        }

        set
        {
            movementspeed = value;
        }
    }

    public float Walkingspeed
    {
        get
        {
            return walkingspeed;
        }

        set
        {
            walkingspeed = value;
        }
    }

    public float Attackrange
    {
        get
        {
            return attackrange;
        }

        set
        {
            attackrange = value;
        }
    }

    public float Attackspeed
    {
        get
        {
            return attackspeed;
        }

        set
        {
            attackspeed = value;
        }
    }
}