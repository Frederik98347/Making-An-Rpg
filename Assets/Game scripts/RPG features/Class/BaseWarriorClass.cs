using System.Collections;
using UnityEngine;

public class BaseWarriorClass : BaseCharacterClass {

	public void WarriorClass(){
		//More of a tanky class
		CharacterClassName = "Warrior";
		CharacterClassDescription = "Brutal Heavy hitter whos also able to withstand damage toe to toe.";
		Stamina = 18;
		Strength = 13;
		Intellect = 5;
		Agility = 5;
	}
}
