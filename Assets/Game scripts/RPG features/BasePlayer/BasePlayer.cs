public class BasePlayer {

    private string playerName;
    string familyName;

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