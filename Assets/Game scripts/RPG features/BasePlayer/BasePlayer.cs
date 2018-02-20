public class BasePlayer : BaseCharacterClass {

    private string playerName;
    private int playerlevel;
    private BaseCharacterClass playerClass;

    public int Playerlevel
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
}