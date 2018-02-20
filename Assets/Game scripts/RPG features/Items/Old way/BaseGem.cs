using UnityEngine;
using System.Collections;

public class BaseGem : BaseItem {

	private int spellEffectID;
	private bool isStackable;
	private int maxStack;
	
	public int SpellEffectID{
		get{return spellEffectID;}
		set{spellEffectID = value;}
	}
	
	public bool IsStackable{
		get{return isStackable;}
		set{isStackable = value;}
	}
	
	public int MaxStack{
		get{return maxStack;}
		set{maxStack = value;}
	}
}