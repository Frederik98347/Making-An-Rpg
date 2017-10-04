using UnityEngine;
using System.Collections;

public class CreateNewEquipment : MonoBehaviour {

	private BaseEquipment newEquipment;
	public string Description = "";
	private bool IsSet = false;

	void Start() {
		CreateEquipment ();
		Debug.Log ("ItemName: " +newEquipment.ItemName);
		Debug.Log ("EquipmentType: " +newEquipment.EquipmentType);
		Debug.Log ("ItemID: " + newEquipment.ItemID.ToString());
		Debug.Log ("Agility: " + newEquipment.Agility.ToString ());
		Debug.Log ("Stamina: " + newEquipment.Stamina.ToString ());
		Debug.Log ("Intellect: " + newEquipment.Intellect.ToString ());
		Debug.Log ("Strength: " + newEquipment.Strength.ToString ());
		Debug.Log ("CritChance: " + newEquipment.CritChance.ToString ());
		Debug.Log("ItemQualit: Rare");

		if (IsSet == true) {
			Debug.Log ("ItemDescription: " +newEquipment.ItemDescription);
		}
	}

	public void CreateEquipment(){
		newEquipment = new BaseEquipment ();

		ChooseEquipmentType ();
		RandomNamesEquipment ();
		StatsByLevel ();
		if (!string.IsNullOrEmpty(Description)) {
			if (Description != null) {
				IsSet = true;
				newEquipment.ItemDescription = Description;	
			}
		}
	}

	private void RandomNamesEquipment(){

		string newequipment = newEquipment.EquipmentType.ToString();
		int randomName = Random.Range(1,5);
		switch (randomName) {

		case 1:
			newEquipment.ItemName = newequipment + " of the Eagle";
			newEquipment.ItemID = 1;
			break;
		case 2:
			newEquipment.ItemName = newequipment + " of the Boar";
			newEquipment.ItemID = 2;
			break;
		case 3:
			newEquipment.ItemName = newequipment + " of the Snake";
			newEquipment.ItemID = 3;
			break;
		case 4:
			newEquipment.ItemName = newequipment + " of the Kitten";
			newEquipment.ItemID = 4;
			break;
		}
	}

	private void ChooseEquipmentType() {
		int randomTemp = Random.Range (1, 9);
		switch (randomTemp) {

		case 1:
			newEquipment.EquipmentType = BaseEquipment.EquipmentTypes.HEAD;
			break;
		case 2:
			newEquipment.EquipmentType = BaseEquipment.EquipmentTypes.SHOULDERS;
			break;
		case 3:
			newEquipment.EquipmentType = BaseEquipment.EquipmentTypes.NECK;
			break;
		case 4:
			newEquipment.EquipmentType = BaseEquipment.EquipmentTypes.RING;
			break;
		case 5:
			newEquipment.EquipmentType = BaseEquipment.EquipmentTypes.FEET;
			break;
		case 6:
			newEquipment.EquipmentType = BaseEquipment.EquipmentTypes.LEGS;
			break;
		case 7:
			newEquipment.EquipmentType = BaseEquipment.EquipmentTypes.CLOAK;
			break;
		case 8:
			newEquipment.EquipmentType = BaseEquipment.EquipmentTypes.CHEST;
			break;
		}
	}

		private void StatsByLevel() {
		int level = GetComponent<Player> ().Level;
		//STAT RANGES
		if (level >=1 && level<=10){

			int rarity = Random.Range (1, 5);
			switch (rarity) {
			case 1:
				newEquipment.Strength = Random.Range (1, 3);
				break;
			case 2:
				newEquipment.Stamina = Random.Range (1, 4);
				break;
			case 3:
				newEquipment.Agility = Random.Range (1, 3);
				break;
			case 4:
				newEquipment.Intellect = Random.Range (1, 3);
				break;
			}
		} else if (level >10 && level<=15) {

			int rarity = Random.Range (1, 4);
			switch (rarity) {
			case 1:
				newEquipment.Strength = Random.Range (2, 5);
				newEquipment.Stamina = Random.Range (2, 6);
				break;
			case 2:
				newEquipment.Agility = Random.Range (2, 5);
				newEquipment.Stamina = Random.Range (2, 6);
				break;
			case 3:
				newEquipment.Intellect = Random.Range (2, 5);
				newEquipment.Stamina = Random.Range (2, 6);
				break;
			}
		} else if (level >15 && level<=20){
			int rarity = Random.Range (1, 4);
			if (rarity == 0) {
				newEquipment.Strength = Random.Range (3, 7);
				newEquipment.Stamina = Random.Range(4,9);
			} else if (rarity == 1) {
				newEquipment.Agility = Random.Range (3, 7);
				newEquipment.Stamina = Random.Range(4,9);
			} else if (rarity == 2) {
				newEquipment.Intellect = Random.Range (3, 7);
				newEquipment.Stamina = Random.Range(4,9);
			} else if (rarity == 3) {
				newEquipment.Stamina = Random.Range(4,9);
				newEquipment.CritChance = Random.Range(1,3);
				int ran = Random.Range (1, 4);

				switch (ran) {
				case 1: 
					newEquipment.Intellect = Random.Range (3, 6);	
					break;
				case 2:
					newEquipment.Agility = Random.Range (3, 6);
					break;
				case 3:
					newEquipment.Strength = Random.Range (3, 6);
					break;
				}
			}

		} else if (level >20 && level<=25){
			int rarity = Random.Range (0, 4);
			if (rarity == 0) {
				newEquipment.Strength = Random.Range (4, 9);
				newEquipment.Stamina = Random.Range(4,10);
			} else if (rarity == 1) {
				newEquipment.Agility = Random.Range (4, 9);
				newEquipment.Stamina = Random.Range(4,10);
			} else if (rarity == 2) {
				newEquipment.Intellect = Random.Range (4, 9);
				newEquipment.Stamina = Random.Range(4,10);
			} else if (rarity == 3) {
				newEquipment.Stamina = Random.Range(4,10);
				newEquipment.CritChance = Random.Range(2,5);
				int ran = Random.Range (1, 4);

				switch (ran) {
				case 1: 
					newEquipment.Intellect = Random.Range (4, 8);	
					break;
				case 2:
					newEquipment.Agility = Random.Range (4, 8);
					break;
				case 3:
					newEquipment.Strength = Random.Range (4, 8);
					break;
				}
			}
		} else if (level >25 && level<=30){
			int rarity = Random.Range (0, 5);
			if (rarity == 0) {
				newEquipment.Strength = Random.Range (5, 9);
				newEquipment.Stamina = Random.Range(5,9);
			} else if (rarity == 1) {
				newEquipment.Stamina = Random.Range (5, 9);
				newEquipment.Stamina = Random.Range(5,9);
			} else if (rarity == 2) {
				newEquipment.Agility = Random.Range (5, 9);
				newEquipment.Stamina = Random.Range(5,9);
			} else if (rarity == 3) {
				newEquipment.Intellect = Random.Range (5, 9);
				newEquipment.Stamina = Random.Range(5,9);
			} else if (rarity == 4) {
				newEquipment.Stamina = Random.Range(5,9);
				newEquipment.CritChance = Random.Range(3,6);
				int ran = Random.Range (1, 4);

				switch (ran) {
				case 1: 
					newEquipment.Intellect = Random.Range (4, 9);	
					break;
				case 2:
					newEquipment.Agility = Random.Range (4, 9);
					break;
				case 3:
					newEquipment.Strength = Random.Range (4, 9);
					break;
				}
			}
		} else if (level >30 && level<=33){
			int rarity = Random.Range (0, 5);
			if (rarity == 0) {
				newEquipment.Strength = Random.Range (6, 10);
				newEquipment.Stamina = Random.Range(6,10);
			} else if (rarity == 1) {
				newEquipment.Stamina = Random.Range (6, 10);
				newEquipment.Stamina = Random.Range(6,10);
			} else if (rarity == 2) {
				newEquipment.Agility = Random.Range (6, 10);
				newEquipment.Stamina = Random.Range(6,10);
			} else if (rarity == 3) {
				newEquipment.Intellect = Random.Range (6, 10);
				newEquipment.Stamina = Random.Range(6,10);
			} else if (rarity == 4) {
				newEquipment.Stamina = Random.Range(6,10);
				newEquipment.CritChance = Random.Range(4,7);
				int ran = Random.Range (1, 4);

				switch (ran) {
				case 1: 
					newEquipment.Intellect = Random.Range (6, 10);	
					break;
				case 2:
					newEquipment.Agility = Random.Range (6, 10);
					break;
				case 3:
					newEquipment.Strength = Random.Range (6, 10);
					break;
				}
			}
		} else if (level >33 && level<=36){
			int rarity = Random.Range (0, 5);
			if (rarity == 0) {
				newEquipment.Strength = Random.Range (7, 11);
				newEquipment.Stamina = Random.Range(7,11);
			} else if (rarity == 1) {
				newEquipment.Stamina = Random.Range (7, 11);
				newEquipment.Stamina = Random.Range(7,11);
			} else if (rarity == 2) {
				newEquipment.Agility = Random.Range (7, 11);
				newEquipment.Stamina = Random.Range(7,11);
			} else if (rarity == 3) {
				newEquipment.Intellect = Random.Range (7, 11);
				newEquipment.Stamina = Random.Range(7,11);
			} else if (rarity == 4) {
				newEquipment.Stamina = Random.Range(7,11);
				newEquipment.CritChance = Random.Range(4,7);
				int ran = Random.Range (1, 4);

				switch (ran) {
				case 1: 
					newEquipment.Intellect = Random.Range (7, 11);	
					break;
				case 2:
					newEquipment.Agility = Random.Range (7, 11);
					break;
				case 3:
					newEquipment.Strength = Random.Range (7, 11);
					break;
				}
			}
		} else if (level >36 && level<=40){
			int rarity = Random.Range (0, 5);
			if (rarity == 0) {
				newEquipment.Strength = Random.Range (8, 14);
				newEquipment.Stamina = Random.Range(8,14);
			} else if (rarity == 1) {
				newEquipment.Stamina = Random.Range (8, 14);
				newEquipment.Stamina = Random.Range(8, 14);
			} else if (rarity == 2) {
				newEquipment.Agility = Random.Range (8, 14);
				newEquipment.Stamina = Random.Range(8, 14);
			} else if (rarity == 3) {
				newEquipment.Intellect = Random.Range (8, 14);
				newEquipment.Stamina = Random.Range(8, 14);
			} else if (rarity == 4) {
				newEquipment.Stamina = Random.Range(8, 14);
				newEquipment.CritChance = Random.Range(4, 9);
				int ran = Random.Range (1, 4);

				switch (ran) {
				case 1: 
					newEquipment.Intellect = Random.Range (8, 14);	
					break;
				case 2:
					newEquipment.Agility = Random.Range (8, 14);
					break;
				case 3:
					newEquipment.Strength = Random.Range (8, 14);
					break;
				}
			}
		} else if (level >40 && level<=44){
			int rarity = Random.Range (0, 5);
			if (rarity == 0) {
				newEquipment.Strength = Random.Range (9, 16);
				newEquipment.Stamina = Random.Range(9, 16);
			} else if (rarity == 1) {
				newEquipment.Stamina = Random.Range (9, 16);
				newEquipment.Stamina = Random.Range(9, 16);
			} else if (rarity == 2) {
				newEquipment.Agility = Random.Range (9, 16);
				newEquipment.Stamina = Random.Range(9, 16);
			} else if (rarity == 3) {
				newEquipment.Intellect = Random.Range (9, 16);
				newEquipment.Stamina = Random.Range(9, 16);
			} else if (rarity == 4) {
				newEquipment.Stamina = Random.Range(9, 16);
				newEquipment.CritChance = Random.Range(5, 9);
				int ran = Random.Range (1, 4);

				switch (ran) {
				case 1: 
					newEquipment.Intellect = Random.Range (9, 16);	
					break;
				case 2:
					newEquipment.Agility = Random.Range (9, 16);
					break;
				case 3:
					newEquipment.Strength = Random.Range (9, 16);
					break;
				}
			}
		} else if (level >44 && level<=48){
			int rarity = Random.Range (0, 5);
			if (rarity == 0) {
				newEquipment.Strength = Random.Range (11, 17);
				newEquipment.Stamina = Random.Range(11, 17);
			} else if (rarity == 1) {
				newEquipment.Stamina = Random.Range (11, 17);
				newEquipment.Stamina = Random.Range(11, 17);
			} else if (rarity == 2) {
				newEquipment.Agility = Random.Range (11, 17);
				newEquipment.Stamina = Random.Range(11, 17);
			} else if (rarity == 3) {
				newEquipment.Intellect = Random.Range (11, 17);
				newEquipment.Stamina = Random.Range(11, 17);
			} else if (rarity == 4) {
				newEquipment.Stamina = Random.Range(11, 17);
				newEquipment.CritChance = Random.Range(11, 17);
				int ran = Random.Range (1, 4);

				switch (ran) {
				case 1: 
					newEquipment.Intellect = Random.Range (11, 17);	
					break;
				case 2:
					newEquipment.Agility = Random.Range (11, 17);
					break;
				case 3:
					newEquipment.Strength = Random.Range (11, 17);
					break;
				}
			}
		} else if (level >48 && level<50){
			int rarity = Random.Range (0, 5);
			if (rarity == 0) {
				newEquipment.Strength = Random.Range (13, 21);
				newEquipment.Stamina = Random.Range(13, 21);
			} else if (rarity == 1) {
				newEquipment.Stamina = Random.Range (13, 21);
				newEquipment.Stamina = Random.Range(13, 21);
			} else if (rarity == 2) {
				newEquipment.Agility = Random.Range (13, 21);
				newEquipment.Stamina = Random.Range(13, 21);
			} else if (rarity == 3) {
				newEquipment.Intellect = Random.Range (13, 21);
				newEquipment.Stamina = Random.Range(13, 21);
			} else if (rarity == 4) {
				newEquipment.Stamina = Random.Range(13, 21);
				newEquipment.CritChance = Random.Range(13, 21);
				int ran = Random.Range (1, 4);

				switch (ran) {
				case 1: 
					newEquipment.Intellect = Random.Range (13, 21);	
					break;
				case 2:
					newEquipment.Agility = Random.Range (13, 21);
					break;
				case 3:
					newEquipment.Strength = Random.Range (13, 21);
					break;
				}
			}
		} else if (level == 50){
			int rarity = Random.Range (0, 5);
			if (rarity == 0) {
				newEquipment.Strength = Random.Range (19, 27);
				newEquipment.Stamina = Random.Range(19, 27);
			} else if (rarity == 1) {
				newEquipment.Stamina = Random.Range (19, 27);
				newEquipment.Stamina = Random.Range(19, 27);
			} else if (rarity == 2) {
				newEquipment.Agility = Random.Range (19, 27);
				newEquipment.Stamina = Random.Range(19, 27);
			} else if (rarity == 3) {
				newEquipment.Intellect = Random.Range (19, 27);
				newEquipment.Stamina = Random.Range(19, 27);
			} else if (rarity == 4) {
				newEquipment.Stamina = Random.Range(19, 27);
				newEquipment.CritChance = Random.Range(19, 27);
				int ran = Random.Range (1, 4);

				switch (ran) {
				case 1: 
					newEquipment.Intellect = Random.Range (19, 27);	
					break;
				case 2:
					newEquipment.Agility = Random.Range (19, 27);
					break;
				case 3:
					newEquipment.Strength = Random.Range (19, 27);
					break;
				}
			}
		}
	}
}