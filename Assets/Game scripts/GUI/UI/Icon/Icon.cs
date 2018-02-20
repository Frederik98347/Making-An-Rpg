using UnityEngine;

[System.Serializable]
public class Icon{
    [SerializeField] Sprite image;
    [SerializeField] string toolTip;
    [SerializeField] string description;

    public Sprite Image
    {
        get { return image; }
        set { image = value; }
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

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }
}
