using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{

    //current level
    [SerializeField] int vLevel = 1;
    [SerializeField] ParticleSystem levelParticleSystem;
    [SerializeField] Slider expBar;
    [SerializeField] GameObject LevelupBox;
    public TMP_Text levelText;
    public TMP_Text ExpBarText;

    public bool isPercentageExp;
    public bool isPercentageNnumbersExp;
    //current exp amount
    [SerializeField] int vCurrExp = 0;
    //exp amount needed for lvl 1
    int vExpBase = 27;
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
        float t = Mathf.Pow(VExpMod, VLevel);
        VExpReq = (int)Mathf.Floor(VExpBase * t);

        IncreaseExp();
        LevelupBox.SetActive(true);
        LvlUpAnim();
    }

    private void Start()
    {
        LevelupBox.SetActive(false);
        expBar.value = CalculateExpBar();

        if (ExpBarText != null)
        {
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

    float CalculateExpBar()
    {
        return (float)VCurrExp / (float)VExpReq;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            GainExp(5);
        }

        if (expBar.value == 1 && VCurrExp == 0)
        {
            expBar.value = 0;
        } else if (expBar.value == 1 && VCurrExp != VExpReq)
        {
            expBar.value = VCurrExp;
        }
    }

    void IncreaseExp()
    {
        if (VLevel >= 20)
        {
            VExpBase = VExpBase-3 + VExpReq;
            VExpReq = VExpReq-1 + VExpBase;
            VExpMod = 1.20f;
        } else if (VLevel >= 35)
        {
            VExpBase = VExpReq-2 + VExpBase;
            VExpReq = VExpBase + VExpReq;
            VExpMod = 1.25f;
        }
        else if (VLevel >= 45)
        {
            VExpBase = VExpBase + VExpBase;
            VExpReq = VExpReq + VExpReq;
            VExpMod = 1.30f;
        }
    }

    void LvlUpAnim()
    {
        //objectPooler needs to go here instead
        Instantiate(levelParticleSystem, transform.position, transform.rotation);
        LvlUpBox();
    }

    void LvlUpBox()
    {

        SetText("Level " + VLevel + "!");
    }

    public void SetText(string Text)
    {
        //text from ui set to = string text
        levelText.GetComponent<TMP_Text>().text = Text;
    }

    void SetExpText(string text)
    {
        ExpBarText.GetComponent<TMP_Text>().text = text;
    }
}