using UnityEngine;
using System.Collections;

public class TestGUI : MonoBehaviour {
private BaseCharacterClass class1 = new BaseMageClass{};
private BaseCharacterClass class2 = new BaseWarriorClass{};
private BaseCharacterClass class3 = new BaseRogueClass{};
public TextMesh text;
 
 void OnGUI(){
  GUILayout.Label (class1.CharacterClassName);
  GUILayout.Label (class1.CharacterClassDescription);
  GUILayout.Label (class2.CharacterClassName);
  GUILayout.Label (class2.CharacterClassDescription);
  GUILayout.Label (class3.CharacterClassName);
  GUILayout.Label (class3.CharacterClassDescription);
 }
}