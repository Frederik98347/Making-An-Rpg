using UnityEngine;
using UnityEditor;
using EnemyTypes;

public class EnemyCreator : EditorWindow {
	[MenuItem("Rpg Tools/EnemyCreator")]
	static void Init() {

		EnemyCreator enemyWindow = (EnemyCreator)CreateInstance(typeof(EnemyCreator));
		enemyWindow.Show();
	}

	AIConfig tempEnemy = null;
	EnemyManager enemyManager = null;

    void OnGUI() {

        if (enemyManager == null)
        {

            //enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
            enemyManager = FindObjectOfType<EnemyManager>().GetComponent<EnemyManager>();
        }

        if (tempEnemy)
        {
            tempEnemy.EnemyName = EditorGUILayout.TextField("Enemy Name", tempEnemy.EnemyName);
            tempEnemy.EnemyHP = EditorGUILayout.IntField("Health", tempEnemy.EnemyHP);
            tempEnemy.EnemyLevel = EditorGUILayout.IntField("Level", tempEnemy.EnemyLevel);
            tempEnemy.MinAutoDamage = EditorGUILayout.IntField("MinAutoDamage", tempEnemy.MinAutoDamage);
            tempEnemy.MaxAutoDamage = EditorGUILayout.IntField("MaxAutoDamage", tempEnemy.MaxAutoDamage);
            tempEnemy.AttackSpeed = EditorGUILayout.FloatField("AttackSpeed", tempEnemy.AttackSpeed);
            tempEnemy.AttackRange = EditorGUILayout.FloatField("Attack Range", tempEnemy.AttackRange);
            tempEnemy.EnemyDefense = EditorGUILayout.IntField("Enemy Defense", tempEnemy.EnemyDefense);
            EditorGUILayout.Space();
            tempEnemy.MovementSpeed = EditorGUILayout.FloatField("MovementSpeed", tempEnemy.MovementSpeed);
            tempEnemy.DetectionRange = EditorGUILayout.FloatField("DetectionRange", tempEnemy.DetectionRange);
            tempEnemy.Exptogive = EditorGUILayout.IntField("Exp to give", tempEnemy.Exptogive);
            tempEnemy.EnemyResistance = EditorGUILayout.IntField("Resistances to magic", tempEnemy.EnemyResistance);
            tempEnemy.EnemyIcon = (Texture2D)EditorGUILayout.ObjectField("Enemy Icon", tempEnemy.EnemyIcon, typeof(Texture2D), false);
            tempEnemy.ToolTip = EditorGUILayout.TextField("Tooltip", tempEnemy.ToolTip);
            GUILayout.BeginVertical();
            tempEnemy.AbilityPrefab_1 = (GameObject)EditorGUILayout.ObjectField("Ability Prefab", tempEnemy.AbilityPrefab_1, typeof(GameObject), false);
            tempEnemy.AbilityPrefab_2 = (GameObject)EditorGUILayout.ObjectField("Ability Prefab", tempEnemy.AbilityPrefab_2, typeof(GameObject), false);
            tempEnemy.AbilityPrefab_3 = (GameObject)EditorGUILayout.ObjectField("Ability Prefab", tempEnemy.AbilityPrefab_3, typeof(GameObject), false);
            tempEnemy.AbilityPrefab_4 = (GameObject)EditorGUILayout.ObjectField("Ability Prefab", tempEnemy.AbilityPrefab_4, typeof(GameObject), false);
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Enemy Class selection");
            tempEnemy.mobClass = (MobClass)EditorGUILayout.EnumPopup(tempEnemy.mobClass);
            GUILayout.Label("Mob rarity");
            tempEnemy.mobRarity = (MobRarity)EditorGUILayout.EnumPopup(tempEnemy.mobRarity);
            GUILayout.Label("Mob Types");
            tempEnemy.mobType = (MobTypes)EditorGUILayout.EnumPopup(tempEnemy.mobType);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            if (tempEnemy.mobClass == MobClass.WARRIOR)
            {
                GUILayout.Label("Select type of Warrior");
                tempEnemy.warriorClassType = (MobClassTypeWarrior)EditorGUILayout.EnumPopup(tempEnemy.warriorClassType);
                GUILayout.Label("Select Type of damage");
                tempEnemy.warDmgType = (WarriorDmgTypes)EditorGUILayout.EnumPopup(tempEnemy.warDmgType);
                tempEnemy.warDmgType = WarriorDmgTypes.PHYSICAL;
                tempEnemy.warriorClassType = MobClassTypeWarrior.BERSKER;
                //disabling other classes attributes
                tempEnemy.rogueClassType = MobClassTypeRogue.NONE;
                tempEnemy.rogueDmgType = RogueDmgTypes.NONE;
                tempEnemy.rogueWpnType = RogueWpnType.NONE;
                tempEnemy.mageClassType = MobClassTypeMage.NONE;
                tempEnemy.mageDmgType = MageDmgTypes.NONE;
                tempEnemy.mageWpnType = MageWpnType.NONE;
                tempEnemy.dmgType = MobDmgTypes.NONE;
            } else if (tempEnemy.mobClass == MobClass.ROGUE)
            {
                GUILayout.Label("Select type of Rogue");
                tempEnemy.rogueClassType = (MobClassTypeRogue)EditorGUILayout.EnumPopup(tempEnemy.rogueClassType);
                GUILayout.Label("Select Type of damage");
                tempEnemy.rogueDmgType = (RogueDmgTypes)EditorGUILayout.EnumPopup(tempEnemy.rogueDmgType);

                // setting so its not auto on none
                tempEnemy.rogueDmgType = RogueDmgTypes.TOXIN;
                tempEnemy.rogueWpnType = RogueWpnType.DAGGERS;
                //disabling other classes attributes
                tempEnemy.warriorClassType = MobClassTypeWarrior.NONE;
                tempEnemy.warDmgType = WarriorDmgTypes.NONE;
                tempEnemy.warWpnType = WarriorWpnType.NONE;
                tempEnemy.mageClassType = MobClassTypeMage.NONE;
                tempEnemy.mageDmgType = MageDmgTypes.NONE;
                tempEnemy.mageWpnType = MageWpnType.NONE;
                tempEnemy.dmgType = MobDmgTypes.NONE;
            } else if (tempEnemy.mobClass == MobClass.MAGE)
            {
                GUILayout.Label("Select type of Mage");
                tempEnemy.mageClassType = (MobClassTypeMage)EditorGUILayout.EnumPopup(tempEnemy.mageClassType);
                GUILayout.Label("Select Type of damage");
                tempEnemy.mageDmgType = (MageDmgTypes)EditorGUILayout.EnumPopup(tempEnemy.mageDmgType);
                tempEnemy.mageDmgType = MageDmgTypes.FROST;
                tempEnemy.mageClassType = MobClassTypeMage.HEAVYAOE;
                //disabling other classes attributes
                tempEnemy.warriorClassType = MobClassTypeWarrior.NONE;
                tempEnemy.warDmgType = WarriorDmgTypes.NONE;
                tempEnemy.warWpnType = WarriorWpnType.NONE;
                tempEnemy.rogueClassType = MobClassTypeRogue.NONE;
                tempEnemy.rogueDmgType = RogueDmgTypes.NONE;
                tempEnemy.rogueWpnType = RogueWpnType.NONE;
                tempEnemy.dmgType = MobDmgTypes.NONE;
            } else 
            {
                GUILayout.Label("Damage Type");
                tempEnemy.dmgType = (MobDmgTypes)EditorGUILayout.EnumPopup(tempEnemy.dmgType);
                tempEnemy.dmgType = MobDmgTypes.PHYSICAL;
                tempEnemy.warriorClassType = MobClassTypeWarrior.NONE;
                tempEnemy.warDmgType = WarriorDmgTypes.NONE;
                tempEnemy.warWpnType = WarriorWpnType.NONE;
                tempEnemy.rogueClassType = MobClassTypeRogue.NONE;
                tempEnemy.rogueDmgType = RogueDmgTypes.NONE;
                tempEnemy.rogueWpnType = RogueWpnType.NONE;
                tempEnemy.mageClassType = MobClassTypeMage.NONE;
                tempEnemy.mageDmgType = MageDmgTypes.NONE;
                tempEnemy.mageWpnType = MageWpnType.NONE;
                tempEnemy.mobClass = MobClass.NONE;
                tempEnemy.dmgType = MobDmgTypes.NONE;
            }
            GUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();

        if (tempEnemy == null)
        {

            if (GUILayout.Button("Create Enemy"))
            {

                tempEnemy = CreateInstance<AIConfig>();
            }

        }
        else if (GUILayout.Button("Create Scriptable Object"))
        {
            if (tempEnemy.EnemyName != null)
            {
                AssetDatabase.CreateAsset(tempEnemy, "Assets/Resources/Enemies/" + tempEnemy.EnemyName + ".asset");
                AssetDatabase.SaveAssets();
                enemyManager.enemyList.Add(tempEnemy);
                Selection.activeObject = tempEnemy;

                tempEnemy = null;
            }
        }

        if (GUILayout.Button("Reset"))
        {
            Reset();
        }
    }

    void Reset()
    {

        if (tempEnemy)
        {

            tempEnemy.EnemyName = "";
            tempEnemy.EnemyHP = 0;
            tempEnemy.EnemyLevel = 0;
            tempEnemy.EnemyIcon = null;
            tempEnemy.ToolTip = null;
            tempEnemy.MinAutoDamage = 0;
            tempEnemy.MaxAutoDamage = 0;
            tempEnemy.Exptogive = 0;
            tempEnemy.AttackSpeed = 0f;
            tempEnemy.AttackRange = 0f;
            tempEnemy.MovementSpeed = 0f;
            tempEnemy.DetectionRange = 0f;
            tempEnemy.OutofrangeTimer = 0f;
            tempEnemy.EnemyDefense = 0;
            tempEnemy.AbilityPrefab_1 = null;
            tempEnemy.AbilityPrefab_2 = null;
            tempEnemy.AbilityPrefab_3 = null;
            tempEnemy.AbilityPrefab_4 = null;
            tempEnemy.EnemyResistance = 0;
            tempEnemy.warriorClassType = MobClassTypeWarrior.NONE;
            tempEnemy.warDmgType = WarriorDmgTypes.NONE;
            tempEnemy.warWpnType = WarriorWpnType.NONE;
            tempEnemy.rogueClassType = MobClassTypeRogue.NONE;
            tempEnemy.rogueDmgType = RogueDmgTypes.NONE;
            tempEnemy.rogueWpnType = RogueWpnType.NONE;
            tempEnemy.mageClassType = MobClassTypeMage.NONE;
            tempEnemy.mageDmgType = MageDmgTypes.NONE;
            tempEnemy.mageWpnType = MageWpnType.NONE;
            tempEnemy.mobClass = MobClass.NONE;
            tempEnemy.dmgType = MobDmgTypes.NONE;
            tempEnemy.mobRarity = MobRarity.COMMON;
            tempEnemy.mobType = MobTypes.MELEE;
        }
    }
}