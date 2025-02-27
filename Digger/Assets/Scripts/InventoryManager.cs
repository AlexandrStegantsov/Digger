using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public enum OreType { Jade, Sunstone, Rosalite, Tanzanite, Sapphire }

    private int totalCapacity = 5; // Maximum inventory space
    private int currentCount = 0; // Total ores collected
    private Dictionary<OreType, int> oreInventory = new Dictionary<OreType, int>();

    [SerializeField] private TextMeshProUGUI inventoryText; // Reference to UI Text

    //public GameObject inventoryFullAlert;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Initialize ore inventory
        foreach (OreType type in System.Enum.GetValues(typeof(OreType)))
        {
            oreInventory[type] = 0;
        }

        // Load inventory from PlayerPrefs
        PlayerPrefsManager.LoadInventory(out oreInventory, out currentCount, out totalCapacity);
        UpdateInventoryUI();
    }

    public bool AddOre(OreType type, int amount)
    {
        if (currentCount + amount > totalCapacity)
        {
            //inventoryFullAlert.SetActive(true);
            Invoke("HideInventoryFullAlertPanel", 3f);
            return false;
        }

        oreInventory[type] += amount;
        currentCount += amount;

        // Save the updated inventory
        PlayerPrefsManager.SaveInventory(oreInventory, currentCount, totalCapacity);
        UpdateInventoryUI();
        return true;
    }

    private void HideInventoryFullAlertPanel()
    {
        //inventoryFullAlert.SetActive(false);
    }

    public int GetOreCount(OreType type)
    {
        if (oreInventory.ContainsKey(type))
            return oreInventory[type];

        return 0;
    }

    public void IncreaseCapacity()
    {
        totalCapacity += 5;

        // Save the updated capacity
        PlayerPrefsManager.SaveInventory(oreInventory, currentCount, totalCapacity);
        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        if (inventoryText != null)
            inventoryText.text = $"{currentCount}/{totalCapacity}";
    }

    public void ClearInventory()
    {
        // Get a list of keys before modifying the dictionary
        List<OreType> oreTypes = new List<OreType>(oreInventory.Keys);

        foreach (OreType type in oreTypes)
        {
            oreInventory[type] = 0;
        }

        currentCount = 0;

        // Save the cleared inventory
        PlayerPrefsManager.SaveInventory(oreInventory, currentCount, totalCapacity);
        UpdateInventoryUI();
    }
}
