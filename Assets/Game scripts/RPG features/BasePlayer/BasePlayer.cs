using EnemyTypes;

public class BasePlayer {
    MobDmgTypes dmgTypes;
    private string playerName;
    string familyName;
    int playerlevel = 1;

    public string PlayerName
    {
        get
        {
            return playerName;
        }

        set
        {
            playerName = value;
        }
    }

    public string FamilyName
    {
        get
        {
            return familyName;
        }

        set
        {
            familyName = value;
        }
    }

    public int PlayerLevel
    {
        get
        {
            return playerlevel;
        }

        set
        {
            playerlevel = value;
        }
    }

    public virtual void Block(int damage, bool Frontal)
    {
        if (Frontal == true)
        {
            if (dmgTypes == MobDmgTypes.DARK || dmgTypes == MobDmgTypes.PHYSICAL || dmgTypes == MobDmgTypes.TOXIC)
            {
                //30% damage reduction on block
                damage *= (int)0.70f;
            }
        }
    }

    public virtual void Roll()
    {

    }
}