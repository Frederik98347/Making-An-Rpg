using UnityEngine;

[System.Serializable]
public class Icon{
    [SerializeField] Texture2D image;
    [SerializeField] string toolTip;
    [SerializeField] string description;

    public Texture2D Image
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