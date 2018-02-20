using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{

    //current level
    [SerializeField] int vLevel = 1;
    [SerializeField] ParticleSystem levelAnim;
    [SerializeField] TimeBeforeDis time;
    [SerializeField] Text levelText;
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
        if (VCurrExp >= VExpReq)
        {
            LvlUp();
        }
    }
    void LvlUp()
    {
        VCurrExp -= VExpReq;
        VLevel++;
        float t = Mathf.Pow(VExpMod, VLevel);
        VExpReq = (int)Mathf.Floor(VExpBase * t);

        IncreaseExp();
        LvlUpAnim();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            GainExp(5);
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
        levelAnim.Play();
        LvlUpBox();
    }

    void LvlUpBox()
    {
        levelText.text = "Level " + VLevel;
        time.Show();
    }
}