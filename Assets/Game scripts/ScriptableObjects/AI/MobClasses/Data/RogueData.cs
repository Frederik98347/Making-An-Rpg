using UnityEngine;
using EnemyTypes;

[CreateAssetMenu(fileName = "New Rogue Data", menuName = "EnemyCreator data/Rogue")]
public class RogueData : CharacterData
{
    // all generic enemy data for rogue class
    public RogueWpnType wpnType;
    public RogueDmgTypes dmgType;
    public MobClassTypeRogue rogueClassType;
    public MobRarity mobRarity;
    public MobTypes mobType;
}