using System.Collections;
using UnityEngine;

public class BaseRogueClass : BaseCharacterClass{

	public void RogueClass(){
		//Stealthy class, cant take more damage
		CharacterClassName = "Rogue";
		CharacterClassDescription = "Is stealthy assasins forcused on working in the shadows forcing enemies to do his dirty bitting. Rogues is not able to receive a lot of damage but in return they do loads of it.";
		Stamina = 8;
		Strength = 5;
		Intellect = 12;
		Agility = 18;
	}
}
