using UnityEngine;
using EnemyTypes;

public class AIConfig : ScriptableObject
{
    [SerializeField] int enemyHP;
    [SerializeField] int enemyMaxHealth;
    [SerializeField] int enemyLevel;
    [SerializeField] string enemyName;
    [SerializeField] int enemyDefense;
    [SerializeField] int agility;
    [SerializeField] int intellect;
    [SerializeField] int stamina;
    [SerializeField] int strength;
    [SerializeField] int haste;
    [SerializeField] int endurance;
    [SerializeField] int enemyResistance;
    [SerializeField] int exptogive;
    [SerializeField] int minAutoDamage;
    [SerializeField] int maxAutoDamage;

    [SerializeField] float attackSpeed;
    [SerializeField] float attackRange;
    [SerializeField] float movementSpeed;
    [SerializeField] float walkingSpeed;
    [SerializeField] float detectionRange;
    [SerializeField] float behind_detectionRange;
    [SerializeField] float outofrangeTimer;
    [SerializeField] Texture2D enemyIcon;
    [SerializeField] string toolTip;

    // ai abilities
    [SerializeField] GameObject abilityPrefab_1;
    [SerializeField] GameObject abilityPrefab_2;
    [SerializeField] GameObject abilityPrefab_3;
    [SerializeField] GameObject abilityPrefab_4;

    public string EnemyName
    {
        get
        {
            return enemyName;
        }

        set
        {
            enemyName = value;
        }
    }

    public int EnemyDefense
    {
        get
        {
            return enemyDefense;
        }

        set
        {
            enemyDefense = value;
        }
    }

    public int Exptogive
    {
        get
        {
            return exptogive;
        }

        set
        {
            exptogive = value;
        }
    }

    public int MinAutoDamage
    {
        get
        {
            return minAutoDamage;
        }

        set
        {
            minAutoDamage = value;
        }
    }

    public int MaxAutoDamage
    {
        get
        {
            return maxAutoDamage;
        }

        set
        {
            maxAutoDamage = value;
        }
    }

    public float AttackSpeed
    {
        get
        {
            return attackSpeed;
        }

        set
        {
            attackSpeed = value;
        }
    }

    public float AttackRange
    {
        get
        {
            return attackRange;
        }

        set
        {
            attackRange = value;
        }
    }

    public float MovementSpeed
    {
        get
        {
            return movementSpeed;
        }

        set
        {
            movementSpeed = value;
        }
    }

    public float WalkingSpeed
    {
        get
        {
            return walkingSpeed;
        }

        set
        {
            walkingSpeed = value;
        }
    }

    public float DetectionRange
    {
        get
        {
            return detectionRange;
        }

        set
        {
            detectionRange = value;
        }
    }

    public GameObject AbilityPrefab_1
    {
        get
        {
            return abilityPrefab_1;
        }

        set
        {
            abilityPrefab_1 = value;
        }
    }

    public GameObject AbilityPrefab_2
    {
        get
        {
            return abilityPrefab_2;
        }

        set
        {
            abilityPrefab_2 = value;
        }
    }

    public GameObject AbilityPrefab_3
    {
        get
        {
            return abilityPrefab_3;
        }

        set
        {
            abilityPrefab_3 = value;
        }
    }

    public GameObject AbilityPrefab_4
    {
        get
        {
            return abilityPrefab_4;
        }

        set
        {
            abilityPrefab_4 = value;
        }
    }

    public float OutofrangeTimer
    {
        get
        {
            return outofrangeTimer;
        }

        set
        {
            outofrangeTimer = value;
        }
    }

    public float Behind_detectionRange
    {
        get
        {
            return behind_detectionRange;
        }

        set
        {
            behind_detectionRange = value;
        }
    }

    public Texture2D EnemyIcon
    {
        get
        {
            return enemyIcon;
        }

        set
        {
            enemyIcon = value;
        }
    }

    public string ToolTip
    {
        get
        {
            return toolTip;
        }

        set
        {
            toolTip = value;
        }
    }

    public int EnemyResistance
    {
        get
        {
            return enemyResistance;
        }

        set
        {
            enemyResistance = value;
        }
    }

    public int EnemyHP
    {
        get
        {
            return enemyHP;
        }

        set
        {
            enemyHP = value;
        }
    }

    public int EnemyMaxHealth
    {
        get
        {
            return enemyMaxHealth;
        }

        set
        {
            enemyMaxHealth = value;
        }
    }

    public int EnemyLevel
    {
        get
        {
            return enemyLevel;
        }

        set
        {
            enemyLevel = value;
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

    public int Haste
    {
        get
        {
            return haste;
        }

        set
        {
            haste = value;
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

    // enums
    public MobRarity mobRarity;
    public MobDmgTypes dmgType;
    public MobTypes mobType;
    public MobClass mobClass;

    //warrior
    public WarriorWpnType warWpnType;
    public WarriorDmgTypes warDmgType;
    public MobClassTypeWarrior warriorClassType;

    //rogue
    public RogueWpnType rogueWpnType;
    public RogueDmgTypes rogueDmgType;
    public MobClassTypeRogue rogueClassType;

    //mage
    public MageWpnType mageWpnType;
    public MageDmgTypes mageDmgType;
    public MobClassTypeMage mageClassType;
}