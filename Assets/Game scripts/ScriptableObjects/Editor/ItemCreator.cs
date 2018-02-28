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
    ItemManager itemManager = null;

    void OnGUI()
    {

        if (itemManager == null)
        {

            itemManager = GameObject.FindObjectOfType<ItemManager>().GetComponent<ItemManager>();
        }

        if (tempItem)
        {

            tempItem.Name = EditorGUILayout.TextField("Item Name", tempItem.Name);
            tempItem.Description = EditorGUILayout.TextField("Item description", tempItem.Description);
            tempItem.TypeOfItem = (ItemType)EditorGUILayout.EnumPopup("Item type", tempItem.TypeOfItem);
            tempItem.Icon = (Texture2D)EditorGUILayout.ObjectField("Item Icon", tempItem.Icon, typeof(Texture2D), false);

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
            AssetDatabase.CreateAsset(tempItem, "Assets/resources/Items/" + tempItem.Name + ".asset");
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

            tempItem.Name = "";
            tempItem.Icon = null;
            tempItem.Description = "";

        }
    }
}