public class BasePlayer {

    private string playerName;
    string familyName;
    private BaseCharacterClass playerClass;

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
    public int PlayerLevel;

    public BaseCharacterClass PlayerClass
    {
        get
        {
            return playerClass;
        }

        set
        {
            playerClass = value;
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
}