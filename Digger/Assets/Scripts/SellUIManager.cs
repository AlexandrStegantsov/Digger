using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellUIManager : MonoBehaviour
{
    public GameObject oreEntryTemplate; // Template for ore text (disabled by default)
    public Transform oreListParent; // Parent container for ore entries
    public TextMeshProUGUI totalText; // Text for total earnings
    public Button sellButton; // Sell button reference

    private Dictionary<InventoryManager.OreType, int> orePrices = new Dictionary<InventoryManager.OreType, int>()
    {
        { InventoryManager.OreType.Jade, 5 },
        { InventoryManager.OreType.Sunstone, 15 },
        { InventoryManager.OreType.Rosalite, 30 },
        { InventoryManager.OreType.Tanzanite, 50 },
        { InventoryManager.OreType.Sapphire, 100 },
    };

    private int totalEarnings = 0;

    private void Start()
    {
        if (sellButton != null)
            sellButton.onClick.AddListener(SellOres);

        UpdateSellPanel();
    }

    public void UpdateSellPanel()
    {
        Debug.Log("Updating Sell Panel...");

        // Clear previous entries
        foreach (Transform child in oreListParent)
        {
            if (child.gameObject != oreEntryTemplate)
                Destroy(child.gameObject);
        }

        totalEarnings = 0;

        // Prepare formatted sell panel text
        List<string> oreEntries = new List<string>();

        foreach (InventoryManager.OreType ore in System.Enum.GetValues(typeof(InventoryManager.OreType)))
        {
            int count = InventoryManager.Instance.GetOreCount(ore);
            if (count > 0)
            {
                int price = orePrices[ore];
                int total = count * price;
                totalEarnings += total;

                // Format each row dynamically
                string formattedEntry = $"{ore}\t\t{count}   x   {price} $     =     {total} $";
                oreEntries.Add(formattedEntry);
            }
        }

        // Add each formatted entry to the UI
        foreach (string entry in oreEntries)
        {
            GameObject newEntry = Instantiate(oreEntryTemplate, oreListParent);
            newEntry.SetActive(true);
            newEntry.GetComponent<TextMeshProUGUI>().text = entry;
        }

        // Update total earnings
        totalText.text = $"Total    =   {totalEarnings}$";
    }

    private void SellOres()
    {
        if (totalEarnings > 0)
        {
            //AudioManager.Instance.PlaySFX("sell");
            // Add earnings to central money system
            CurrencyManager.Instance.AddMoney(totalEarnings);

            // After the panel is updated, clear inventory
            InventoryManager.Instance.ClearInventory();

            // Refresh Sell Panel to display updated info
            UpdateSellPanel();
        }
    }

}
