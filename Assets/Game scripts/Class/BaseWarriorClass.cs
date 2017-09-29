using System.Collections;
using UnityEngine;

public class BaseWarriorClass : BaseCharacterClass {

	public void WarriorClass(){
		//More of a tanky class
		CharacterClassName = "Warrior";
		CharacterClassDescription = "";
		Stamina = 18;
		Strength = 12;
		Intellect = 5;
		Agility = 5;
	}
}
