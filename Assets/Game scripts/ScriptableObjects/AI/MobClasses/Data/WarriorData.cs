using UnityEngine;
using EnemyTypes;

[CreateAssetMenu(fileName ="New Warrior Data", menuName ="EnemyCreator data/Warrior")]
public class WarriorData : CharacterData
{

    // all generic enemy data for warrior class
    public WarriorWpnType wpnType;
    public WarriorDmgTypes dmgType;
    public MobClassTypeWarrior warriorClassType;
    public MobRarity mobRarity;
    public MobTypes mobType;
}