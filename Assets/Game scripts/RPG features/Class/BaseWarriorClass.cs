public class BaseWarriorClass : BaseCharacterClass {

	public void WarriorClass(){
		//More of a tanky class
		CharacterClassName = "Warrior";
		CharacterClassDescription = "Brutal Heavy hitter whos also able to withstand damage toe to toe with hardhitting enemies. Expect in close quarters, but also suffers from low intellect and agility";
		Stamina = 18;
		Strength = 14;
		Intellect = 6;
		Agility = 4;

        //secoundary stats
        Critchance = 4;
        Defense = 5;
    }
}
