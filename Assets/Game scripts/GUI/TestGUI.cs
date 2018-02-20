using UnityEngine;

public class TestGUI : MonoBehaviour {
private BaseCharacterClass class1 = new BaseMageClass();
private BaseCharacterClass class2 = new BaseWarriorClass();
private BaseCharacterClass class3 = new BaseRogueClass();
 
 void OnGUI(){
  GUILayout.Label (" " + class1.CharacterClassName);
  GUILayout.Label (" "+class1.CharacterClassDescription);
  GUILayout.Label (" " + class1.Intellect.ToString());
  GUILayout.Label (class2.CharacterClassDescription);
  GUILayout.Label (" " +class2.Strength.ToString());
  GUILayout.Label (class3.CharacterClassName);
  GUILayout.Label (class3.CharacterClassDescription);
        GUILayout.Label(" " + class3.Agility.ToString());
    }
}