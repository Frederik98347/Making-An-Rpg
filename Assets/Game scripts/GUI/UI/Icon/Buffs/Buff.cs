using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buff{

    private List<Buff> buffs;
    [SerializeField] Icon icon;

    [SerializeField] string buffName;
    [SerializeField] float maxduration;
    [SerializeField] float curDuration;
    [SerializeField] bool permanent = false;

    //Effects
    // how to tell if its a buff or a debuff?

    public List<Buff> Buffs
    {
        get
        {
            return buffs;
        }

        set
        {
            buffs = value;
        }
    }

    public string BuffName
    {
        get
        {
            return buffName;
        }

        set
        {
            buffName = value;
        }
    }

    public float Maxduration
    {
        get
        {
            return maxduration;
        }

        set
        {
            maxduration = value;
        }
    }

    public float CurDuration
    {
        get
        {
            return curDuration;
        }

        set
        {
            curDuration = value;
        }
    }

    public bool Permanent
    {
        get
        {
            return permanent;
        }

        set
        {
            permanent = value;
        }
    }

    public void AddBuff(Buff b)
    {
        buffs.Add(b);
    }

    public void RemoveBuff(Buff b, float t)
    {
        t -= Time.deltaTime;

        if (t <= 0)
        {
            buffs.Remove(b);
        } else if (Permanent)
        {
            buffs.Remove(b);
        }
    }
}