using System.Collections;
using UnityEngine;

public class BaseMageClass : BaseCharacterClass{

	public void MageClass(){
		//More of a tanky class
		CharacterClassName = "Mage";
		CharacterClassDescription = "A smart Wizard focussed on dealing damage with magic.";
		Stamina = 13;
		Strength = 3;
		Intellect = 15;
		Agility = 8;
	}
}