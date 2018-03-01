using UnityEditor;
using UnityEngine;

public class ItemCreator : EditorWindow
{
    [MenuItem("Rpg Tools/ItemCreator")]
    static void Init()
    {

        ItemCreator itemWindow = (ItemCreator)CreateInstance(typeof(ItemCreator));
        itemWindow.Show();
    }

    Item tempItem = null;
    Icon icon = null;
    ItemCreatorManager itemManager = null;

    void OnGUI()
    {

        if (itemManager == null)
        {

            itemManager = GameObject.FindObjectOfType<ItemCreatorManager>().GetComponent<ItemCreatorManager>();
        }

        if (tempItem)
        {

            tempItem.ItemName = EditorGUILayout.TextField("Item Name", tempItem.ItemName);
            tempItem.Description = EditorGUILayout.TextField("Item Description", tempItem.Description);
            tempItem.Image = (Texture2D)EditorGUILayout.ObjectField("Item Icon", tempItem.Image, typeof(Texture2D), false);
            tempItem.TypeOfItem = (ItemType)EditorGUILayout.EnumPopup("Item type", tempItem.TypeOfItem);

            if (tempItem.TypeOfItem == ItemType.ARMOR)
            {
                tempItem.armorType = (Armor.TypeOfArmor)EditorGUILayout.EnumPopup("Armor type", tempItem.armorType);
                tempItem.Rarity = (Item.ItemRarity)EditorGUILayout.EnumPopup("Item Rarity", tempItem.Rarity);
            } else if (tempItem.TypeOfItem == ItemType.WEAPON)
            {
                tempItem.weaponType = (Weapon.TypeOfWeapon)EditorGUILayout.EnumPopup("Weapon type", tempItem.weaponType);
                tempItem.Rarity = (Item.ItemRarity)EditorGUILayout.EnumPopup("Item Rarity", tempItem.Rarity);
            } else if (tempItem.TypeOfItem == ItemType.JEWELRY)
            {
                tempItem.jewlryType = (Jewelry.TypeOfJewelry)EditorGUILayout.EnumPopup("Jewlry type", tempItem.jewlryType);
                tempItem.Rarity = (Item.ItemRarity)EditorGUILayout.EnumPopup("Item Rarity", tempItem.Rarity);
            } else if (tempItem.TypeOfItem == ItemType.CONSUMABLE)
            {
                tempItem.potionType = (Consumable.TypeOfPotion)EditorGUILayout.EnumPopup("Potion type", tempItem.potionType);
                tempItem.isStackable = true;
                tempItem.StackSize = 10;
            } else if(tempItem.TypeOfItem == ItemType.GRIMOIRE)
            {
                tempItem.grimoireType = (Grimoire.typeOfGrimoire)EditorGUILayout.EnumPopup("Grimoire type", tempItem.grimoireType);
                tempItem.Rarity = (Item.ItemRarity)EditorGUILayout.EnumPopup("Item Rarity", tempItem.Rarity);
            }

        }

        EditorGUILayout.Space();

        if (tempItem == null)
        {

            if (GUILayout.Button("Create item"))
            {

                tempItem = CreateInstance<Item>();
            }

        }
        else if (GUILayout.Button("Create Scriptable Object"))
        {
            AssetDatabase.CreateAsset(tempItem, "Assets/resources/Items/" + tempItem.ItemName + ".asset");
            AssetDatabase.SaveAssets();
            itemManager.itemList.Add(tempItem);
            Selection.activeObject = tempItem;

            tempItem = null;
        }

        if (GUILayout.Button("Reset"))
        {
            Reset();

        }

    }
    void Reset()
    {

        if (tempItem)
        {

            tempItem.ItemName = "";
            tempItem.Image = null;
            tempItem.Description = "";

        }
    }
}