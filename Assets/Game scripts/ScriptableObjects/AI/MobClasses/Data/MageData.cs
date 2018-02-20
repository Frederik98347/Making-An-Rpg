using UnityEngine;
using EnemyTypes;

[CreateAssetMenu(fileName = "New Mage Data", menuName = "EnemyCreator data/Mage")]
public class MageData : CharacterData
{
    // all generic enemy data for mage class
    public MageWpnType wpnType;
    public MageDmgTypes dmgType;
    public MobClassTypeMage mageClassType;
    public MobRarity mobRarity;
    public MobTypes mobType;
}