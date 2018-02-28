using UnityEngine;
using UnityEditor;

public class SpellCreator : EditorWindow {
	[MenuItem("Rpg Tools/Spell Maker")]
	static void Init() {
		
		SpellCreator spellWindow = (SpellCreator)CreateInstance(typeof(SpellCreator));
		spellWindow.Show();
	}
	
	Spell tempSpell = null;
	SpellManager spellManager = null;
	
	void OnGUI() {
		
		if (spellManager == null) {
			
            spellManager = GameObject.FindObjectOfType<SpellManager>().GetComponent<SpellManager>();
		}
		
		if (tempSpell) {
			
			tempSpell.spellName = EditorGUILayout.TextField("Spell Name", tempSpell.spellName);
			tempSpell.spellDescription = EditorGUILayout.TextField("Spell Description", tempSpell.spellDescription);
			tempSpell.spellPrefab = (GameObject)EditorGUILayout.ObjectField("Spell Prefab", tempSpell.spellPrefab, typeof(GameObject), false);
			tempSpell.spellCollisionParticle = (GameObject)EditorGUILayout.ObjectField("Spell Collision Effect", tempSpell.spellCollisionParticle, typeof(GameObject), false);
			tempSpell.spellIcon = (Texture2D)EditorGUILayout.ObjectField("Spell Icon", tempSpell.spellIcon, typeof(Texture2D), false);
			tempSpell.MinRange = EditorGUILayout.IntField("MinRange", tempSpell.MinRange);
			tempSpell.MaxRange = EditorGUILayout.IntField("MaxRange", tempSpell.MaxRange);
			tempSpell.MinDamage = EditorGUILayout.IntField("MinDamage", tempSpell.MinDamage);
			tempSpell.MaxDamage = EditorGUILayout.IntField("MaxDamage", tempSpell.MaxDamage);
			tempSpell.ManaCost = EditorGUILayout.IntField("Mana Cost", tempSpell.ManaCost);
			tempSpell.EnergyCost = EditorGUILayout.IntField("Energy Cost", tempSpell.EnergyCost);
			tempSpell.RageCost = EditorGUILayout.IntField("Rage Cost", tempSpell.RageCost);
			tempSpell.projectileSpeed = EditorGUILayout.IntField("Projectile Speed", tempSpell.projectileSpeed);
			
		}
		
		EditorGUILayout.Space();
		
		if(tempSpell == null) {
			
			if(GUILayout.Button("Create Spell")) {
				
				tempSpell = CreateInstance<Spell>();
			}
				
		} else if (GUILayout.Button("Create Scriptable Object")) {
			AssetDatabase.CreateAsset(tempSpell, "Assets/resources/Spells/" + tempSpell.spellName + ".asset");
			AssetDatabase.SaveAssets();
			spellManager.spellList.Add(tempSpell);
			Selection.activeObject = tempSpell;
				
			tempSpell = null;
		}
			
			if(GUILayout.Button("Reset")) {
				Reset();
				
			}
			
		}
	void Reset (){

		if(tempSpell) {

			tempSpell.spellName = "";
			tempSpell.spellDescription = "";
			tempSpell.spellPrefab = null;
			tempSpell.spellCollisionParticle = null;
			tempSpell.spellIcon = null;
			tempSpell.ManaCost = 0;
			tempSpell.RageCost = 0;
			tempSpell.EnergyCost = 0;
			tempSpell.MinRange = 0;
			tempSpell.MaxRange = 0;
			tempSpell.MinDamage = 0;
			tempSpell.MaxDamage = 0;
			tempSpell.projectileSpeed = 0;

		}
	}		
}