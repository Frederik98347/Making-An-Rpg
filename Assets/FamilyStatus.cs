using UnityEngine;
using TMPro;

public class FamilyStatus : MonoBehaviour {
    public ItemManager inventory;
    public Currency currency;
    public string WealthTooltip;
    public string SocialTooltip;
    float socialRankMath;
    float wealthRankMath;

    public float Money = 0;
    public float Income;
    public string MoneyName = "Gold Coins";

    public Item StartItem_Weapon;
    public Item StartItem_Pants;
    public Item StartItem_Chest;
    public Item StartItem_Boots;

    [SerializeField] TMP_Dropdown wealthDropDownPick;
    [SerializeField] TMP_Dropdown socialDropDownPick;

    // Use this for initialization
    void Start () {
        if (currency != null)
        {
            Money = currency.CurrencyAmount;
            MoneyName = currency.CurrencyName;
        }

        if (inventory == null)
        {
            inventory = FindObjectOfType<ItemManager>();
        }

        wealthDropDownPick = GameObject.Find("Wealth").GetComponentInChildren<TMP_Dropdown>();
        socialDropDownPick = GameObject.Find("Social").GetComponentInChildren<TMP_Dropdown>();

        WealthTooltip = "The more wealth, the more money you have at the start of the game according to your social rank. But in return exp gained is reduced acordingly";
        SocialTooltip = "The lower social status, the more attributes points on level up, but you lose talents and bonuses acordingly from startup";
    }

    public void DropDowns()
    {
        // check what the result of the dropdowns was
        if (wealthDropDownPick.value == 0)
        {
            // poor
            Money = 5;
            // expgained increased by 5%
        }
        else if (wealthDropDownPick.value == 1)
        {
            //average
            Money = 15;
            // 0 % reduced xp
        }
        else if (wealthDropDownPick.value == 2)
        {
            //rich
            Money = 30;
            // 5% reduced exp gained
        }

        if (socialDropDownPick.value == 0)
        {
            // peasant
            // pants, no weapons
            // 5% speed (movement, attackspeed & casting speed)

            inventory.Remove(StartItem_Weapon);
            inventory.Remove(StartItem_Boots);
            inventory.Remove(StartItem_Pants);
            inventory.Remove(StartItem_Chest);

            SetInventory(StartItem_Pants);

            Income = 0f;
        }
        else if (socialDropDownPick.value == 1)
        {
            //farmer
            //pants && wep
            //2.5% speed

            inventory.Remove(StartItem_Weapon);
            inventory.Remove(StartItem_Boots);
            inventory.Remove(StartItem_Pants);
            inventory.Remove(StartItem_Chest);

            SetInventory(StartItem_Weapon);
            SetInventory(StartItem_Pants);

            Income = .5f;
        }
        else if (socialDropDownPick.value == 2)
        {
            //knight
            //pants & wep & chest & boots, level 1 gear though
            // 0 % speed

            inventory.Remove(StartItem_Weapon);
            inventory.Remove(StartItem_Boots);
            inventory.Remove(StartItem_Pants);
            inventory.Remove(StartItem_Chest);

            SetInventory(StartItem_Weapon);
            SetInventory(StartItem_Pants);
            SetInventory(StartItem_Chest);
            SetInventory(StartItem_Boots);

            Income = 1f;
        }

        SetStatus();
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

    void SetCurrency(string currencyName, float currencyAmount, float currencyIncome)
    {
        if (currency != null)
        {
            currencyName = currency.CurrencyName;
            currencyAmount = currency.CurrencyAmount;
            currencyIncome = currency.IncomePrDay;
        }
    }

    void SetInventory(Item item, int amountToAdd = 1)
    {
        // add items to inventory depending on status
        //stackable items not yet implemented in inventory
        inventory.Add(item, amountToAdd);
    }

    void SetStatus()
    {
        SetCurrency(MoneyName, Money, Income);
    }
}