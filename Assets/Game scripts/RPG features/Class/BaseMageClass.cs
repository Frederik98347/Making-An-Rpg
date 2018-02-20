public class BaseMageClass : BaseCharacterClass{

	public void MageClass(){
		// can take more damage, casts spells and able to avoid some damage.
		CharacterClassName = "Mage";
		CharacterClassDescription = "A smart Wizard focussed on dealing damage with magic and excelling in ranged combat. Mages has the highest intellect of all classes but suffers from a low amount of strenght";
		Stamina = 12;
		Strength = 3;
		Intellect = 17;
		Agility = 9;

        //secoundary stats
        Critchance = 3;
        Defense = 3;
    }
}