using UnityEngine;

[System.Serializable]
public class Buff{

    [SerializeField] Icon icon;

    [SerializeField] string buffName;
    [SerializeField] float maxduration;
    [SerializeField] float curDuration;
    [SerializeField] bool permanent = false;
   
    //Effects
    // how to tell if its a buff or a debuff?
}
