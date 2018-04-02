using UnityEngine;
using TMPro;

public class FamilyStatus : MonoBehaviour{
    public ItemManager inventory;
    public Currency currency;
    public string WealthTooltip;
    public string SocialTooltip;

    public Item StartItem_Weapon;
    public Item StartItem_Pants;
    public Item StartItem_Chest;
    public Item StartItem_Boots;
    public Item[] StartItems;

    [SerializeField] TMP_Dropdown wealthDropDownPick;
    [SerializeField] TMP_Dropdown socialDropDownPick;

    public string MoneyName;
    public float AmountOfMoney = 0;
    public float IncomePrDay = 0;

    // Use this for initialization
    void Start () {

        if (inventory == null)
        {
            inventory = FindObjectOfType<ItemManager>();
        }
        wealthDropDownPick = GameObject.Find("Wealth").GetComponentInChildren<TMP_Dropdown>();
        socialDropDownPick = GameObject.Find("Social").GetComponentInChildren<TMP_Dropdown>();

        WealthTooltip = "The more wealth, the more money you have at the start of the game according to your social rank. But in return exp gained is reduced acordingly";
        SocialTooltip = "The lower social status, the more attributes points on level up, but you lose talents and bonuses acordingly from startup";

        if (currency != null)
        {
            MoneyName = currency.CurrencyName;
            AmountOfMoney = currency.CurrencyAmount;
            IncomePrDay = currency.IncomePrDay;
        }

        StartItems = new Item[] { StartItem_Weapon, StartItem_Pants, StartItem_Boots, StartItem_Chest };
    }

    void DropDownSocial()
    {

        if (socialDropDownPick.value == 0)
        {
            // peasant
            // pants, no weapons
            // 5% speed (movement, attackspeed & casting speed)

            ClearInventory(StartItems[0]); // weapon
            ClearInventory(StartItems[1]); // pants
            ClearInventory(StartItems[2]); // feet
            ClearInventory(StartItems[3]); // chest
            SetInventory(StartItems[1]);
            if (currency != null)
            {
                IncomePrDay = 0f;
            }
        }
        else if (socialDropDownPick.value == 1)
        {
            //farmer
            //pants && wep
            //2.5% speed

            ClearInventory(StartItems[0]); // weapon
            ClearInventory(StartItems[1]); // pants
            ClearInventory(StartItems[2]); // feet
            ClearInventory(StartItems[3]); // chest
            SetInventory(StartItems[0]); // weapon
            SetInventory(StartItems[1]); // pants

            if (currency != null)
            {
                IncomePrDay = .5f;
            }
        }
        else if (socialDropDownPick.value == 2)
        {
            //knight
            //pants & wep & chest & boots, level 1 gear though
            // 0 % speed
            ClearInventory(StartItems[0]); // weapon
            ClearInventory(StartItems[1]); // pants
            ClearInventory(StartItems[2]); // feet
            ClearInventory(StartItems[3]); // chest
            SetInventory(StartItems[0]); // weapon
            SetInventory(StartItems[1]); // pants
            SetInventory(StartItems[2]); // feet
            SetInventory(StartItems[3]); // chest

            if (currency != null)
            {
                IncomePrDay = 1f;
            }
        }
    }

    void DropDownWealth()
    {

        // check what the result of the dropdowns was
        if (wealthDropDownPick.value == 0)
        {
            // poor
            if (currency != null)
                AmountOfMoney = 5;
            // expgained increased by 5%
        }
        else if (wealthDropDownPick.value == 1)
        {
            //average
            if (currency != null)
                AmountOfMoney = 15;
            // 0 % reduced xp
        }
        else if (wealthDropDownPick.value == 2)
        {
            //rich
            if (currency != null)
                AmountOfMoney = 30;
            // 5% reduced exp gained
        }
    }

    public void SetDropDownValues()
    {
        DropDownSocial();
        DropDownWealth();
    }

    private void Update()
    {
        OnMouseEnter();
    }

    void OnMouseEnter()
    {
        //make TooltipAppear inside a box, at the right of the mouse position
        if (wealthDropDownPick)
        {
            //tooltip appear
        } else if (socialDropDownPick)
        {
            //tooltip appear
        }
    }

    void SetInventory(Item item, int amountToAdd = 1, int stackSizeLimit = 1)
    {
        // add items to inventory depending on status
        //stackable items not yet implemented in inventory
        for (int i = 0; i < amountToAdd; i++)
        {
            for (int y = 0; y < stackSizeLimit; y++)
            {
                ItemManager.instance.Add(item, amountToAdd);
            }
        }
    }

    void ClearInventory(Item item, int amountToDel = 1)
    {
        for (int i = 0; i < amountToDel; i++)
        {
            ItemManager.instance.Remove(item);
        }
    }
}