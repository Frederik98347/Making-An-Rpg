using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class Experience : MonoBehaviour
{

    //current level
    [SerializeField] int vLevel;
    [SerializeField] ParticleSystem levelParticleSystem;
    [SerializeField] float levelParticlefadeTime = 1.5f;
    [SerializeField] Slider expBar;
    [SerializeField] GameObject LevelupBox;
    [SerializeField] TMP_Text levelText;
    public Text LevelFrameText;
    public TMP_Text ExpBarText;
    [SerializeField] AudioManger Audio;

    BasePlayer basePlayer;

    public bool showText = true;
    public bool isPercentageExp;
    public bool isPercentageNnumbersExp;
    //current exp amount
    [SerializeField] int vCurrExp = 0;
    //exp amount needed for lvl 1
    int vExpBase = 25;
    //exp amount Required to next levelup
    [SerializeField] int vExpReq = 30;
    //modifier that increases needed exp each level
    float vExpMod = 1.15f;

    public int VLevel
    {
        get
        {
            return vLevel;
        }

        set
        {
            vLevel = value;
        }
    }

    public int VCurrExp
    {
        get
        {
            return vCurrExp;
        }

        set
        {
            vCurrExp = value;
        }
    }

    public int VExpBase
    {
        get
        {
            return vExpBase;
        }

        set
        {
            vExpBase = value;
        }
    }

    public float VExpMod
    {
        get
        {
            return vExpMod;
        }

        set
        {
            vExpMod = value;
        }
    }

    public int VExpReq
    {
        get
        {
            return vExpReq;
        }

        set
        {
            vExpReq = value;
        }
    }

    //leveling methods
    public void GainExp(int e)
    {
        VCurrExp += e;
        expBar.value = CalculateExpBar();

        if (VCurrExp >= VExpReq)
        {
            LvlUp();
            expBar.value = CalculateExpBar();
        }
        if (ExpBarText != null){
            if (isPercentageExp)
            {
                PercentageExpbar();
            }
            else if (isPercentageNnumbersExp)
            {
                PercentageWithNumbers();
            }
            else
            {
                SetExpText(VCurrExp + " / " + VExpReq);
            }
        }
    }

    void PercentageExpbar ()
    {
        isPercentageNnumbersExp = false;
        string PercentOnly = CalculateExpBar()*100f + "%";
        SetExpText(PercentOnly);
    }

    void PercentageWithNumbers()
    {
        isPercentageExp = false;
        string PercentNnumber = VCurrExp + " / " + VExpReq + " (" + CalculateExpBar() * 100f + "%)";
        SetExpText(PercentNnumber);
    }

    void LvlUp()
    {
        VCurrExp -= VExpReq;
        VLevel++;
        SetLevelText("" + VLevel);
        float t = Mathf.Pow(VExpMod, VLevel);
        VExpReq = (int)Mathf.Floor(VExpBase * t);

        IncreaseExp();
        LvlUpAnim();
    }

    private void Start()
    {
        expBar.value = CalculateExpBar();
        SetLevelText("" + VLevel);

        if (ExpBarText != null)
        {
            ShowText();
        }

        if(basePlayer != null)
        {
            basePlayer.Playerlevel = vLevel;
        }
    }

    float CalculateExpBar()
    {
        return (float)VCurrExp / (float)VExpReq;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            GainExp(100);
        }

        if (expBar.value == 1 && VCurrExp == 0)
        {
            expBar.value = 0;
        } else if (expBar.value == 1 && VCurrExp != VExpReq)
        {
            expBar.value = VCurrExp;
        }

        ShowText();
    }

    void IncreaseExp()
    {
        if (VLevel >= 20)
        {
            VExpMod += .005f;
        } else if (VLevel >= 35)
        {
            VExpMod += .003f;
        }
        else if (VLevel >= 45)
        {
            VExpMod += .002f;
        }
    }

    void LvlUpAnim()
    {
        //objectPooler needs to go here instead
        if (LevelupBox == null)
        {
            LevelupBox = GameObject.Find("LevelUpAnim&BoX");
            levelText = GameObject.Find("LevelText").GetComponent<TMP_Text>();
        } else if (levelText == null)
        {
            levelText = GameObject.Find("LevelText").GetComponent<TMP_Text>();
        }

        LevelupBox.GetComponent<levelupboxAnim>().StartAnim();
        ParticleSystem i = Instantiate(levelParticleSystem, transform.position, transform.rotation);
        i.transform.SetParent(LevelupBox.transform);
        Destroy(i.gameObject, levelParticlefadeTime);

        LvlUpBox();
    }

    void LvlUpBox()
    {
        SetText("You Have Reached " + " Level " + VLevel + "!");
    }

    public void SetText(string Text)
    {
        //text from ui set to = string text
        // text to level box popping up
        levelText.GetComponent<TMP_Text>().text = Text;
    }

    void SetLevelText (string Text)
    {
        // Text for level frame
        LevelFrameText.text = Text;
        
    }

    void SetExpText(string text)
    {
        //exp bar text
        ExpBarText.GetComponent<TMP_Text>().text = text;
    }

    void ShowText()
    {
        if (showText == false)
        {
            // dont show text On exp bar
            SetExpText("");
        }
        else
        {
            if (isPercentageNnumbersExp == true)
            {
                PercentageWithNumbers();
            }
            else if (isPercentageExp == true)
            {
                PercentageExpbar();
            }
            else
            {
                SetExpText(VCurrExp + " / " + VExpReq);
            }
        }
    }
}