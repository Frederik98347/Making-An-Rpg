using UnityEngine;
using System.Collections;

public class CreateNewWeapon : MonoBehaviour {

	private BaseWeapon newWeapon;

	void Start() {
		Debug.Log ("Debugging void start");
		print ("Debugging void start");

	}

	public void CreateWeapon(){
		newWeapon = new BaseWeapon ();

		ChooseWeaponType ();
		PickWeaponName ();
		newWeapon.ItemDescription = "This is a new Weapon";
		newWeapon.ItemID = Random.Range (1, 101);
	}

	private void PickWeaponName(){

		string newweapon = newWeapon.WeaponType.ToString();
		int randomName = Random.Range(1,5);
		Debug.Log(randomName + "RandomNames debug");
		switch (randomName) {

		case 1:
			Debug.Log("Case 1: RandomNames debug");
			newWeapon.ItemName = newweapon + " of the Eagle";
			break;
		case 2:
			Debug.Log("Case 2: RandomNames debug");
			newWeapon.ItemName = newweapon + " of the Boar";
			break;
		case 3:
			Debug.Log("Case 3: RandomNames debug");
			newWeapon.ItemName = newweapon + " of the Snake";
			break;
		case 4:
			Debug.Log("Case 4: RandomNames debug");
			newWeapon.ItemName = newweapon + " of the Kitten";
			break;
		}
	}

	private void ChooseWeaponType() {
		int randomTemp = Random.Range (1, 9);
		Debug.Log (randomTemp + "ChooseWeaponType Debug");
		switch (randomTemp) {

		case 1:
			Debug.Log("Case 1: ChooseEquipmentType debug");
			newWeapon.WeaponType = BaseWeapon.WeaponTypes.BOW;
			break;
		case 2:
			Debug.Log("Case 2: ChooseEquipmentType debug");
			newWeapon.WeaponType = BaseWeapon.WeaponTypes.DAGGER;
			break;
		case 3:
			Debug.Log("Case 3: ChooseEquipmentType debug");
			newWeapon.WeaponType = BaseWeapon.WeaponTypes.AXE;
			break;
		case 4:
			Debug.Log("Case 4: ChooseEquipmentType debug");
			newWeapon.WeaponType = BaseWeapon.WeaponTypes.POLEARM;
			break;
		case 5:
			Debug.Log ("Case 5: ChooseEquipmentType debug");
			newWeapon.WeaponType = BaseWeapon.WeaponTypes.SWORD;
			break;
		case 6:
			Debug.Log ("Case 5: ChooseEquipmentType debug");
			newWeapon.WeaponType = BaseWeapon.WeaponTypes.SHIELD;
			break;
		case 7:
			Debug.Log ("Case 5: ChooseEquipmentType debug");
			newWeapon.WeaponType = BaseWeapon.WeaponTypes.STAFF;
			break;
		}
	}
}
