using System.Collections.Generic;
using UnityEngine;

public class Rpg_functions
{

    [SerializeField] Icon icon;
    [SerializeField] List<Buff> buffs;
    [SerializeField] Transform transform;
    [SerializeField] bool isPlayer;
    [SerializeField] bool isEnemy;
    [SerializeField] string name;

    public void LevelUp()
    {
        if (isPlayer == true)
        {
            //transform.GetComponent<Player>().ExpToLevel();
        }
    }

    public Icon ThumbNail
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

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

    public void AddBuff(Buff b)
    {
        buffs.Add(b);
    }

    public void IsPlayer(bool _isPlayer)
    {
        _isPlayer = isPlayer;
        if (this.transform.tag == "Player")
        {
            isPlayer = true;
        } else
        {
            isPlayer = false;
        }
    }

    public void IsEnemy(bool _isEnemy)
    {
        _isEnemy = isEnemy;
        if (isPlayer == false)
        {
            isEnemy = true;
        } else
        {
            isEnemy = false;
        }
    }
}