public class BasePlayer {
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
}