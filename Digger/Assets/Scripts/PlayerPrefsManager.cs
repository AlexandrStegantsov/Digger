using UnityEngine;
using System.Collections.Generic;

public class PlayerPrefsManager : MonoBehaviour
{
    // Reset all collected ores (useful for debugging)
    public static void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    // Save the inventory data
    public static void SaveInventory(Dictionary<InventoryManager.OreType, int> oreInventory, int currentCount, int totalCapacity)
    {
        foreach (var ore in oreInventory)
        {
            PlayerPrefs.SetInt(ore.Key.ToString(), ore.Value);
        }

        PlayerPrefs.SetInt("TotalCapacity", totalCapacity);
        PlayerPrefs.SetInt("CurrentCount", currentCount);
        PlayerPrefs.Save();
    }

    // Load the inventory data
    public static void LoadInventory(out Dictionary<InventoryManager.OreType, int> oreInventory, out int currentCount, out int totalCapacity)
    {
        oreInventory = new Dictionary<InventoryManager.OreType, int>();

        foreach (InventoryManager.OreType type in System.Enum.GetValues(typeof(InventoryManager.OreType)))
        {
            oreInventory[type] = PlayerPrefs.GetInt(type.ToString(), 0);
        }

        totalCapacity = PlayerPrefs.GetInt("TotalCapacity", 5);
        currentCount = PlayerPrefs.GetInt("CurrentCount", 0);
    }

    // Save the currency (money)
    public static void SaveCurrency(int currentMoney)
    {
        PlayerPrefs.SetInt("CurrentMoney", currentMoney);
        PlayerPrefs.Save();
    }

    // Load the currency (money)
    public static int LoadCurrency()
    {
        return PlayerPrefs.GetInt("CurrentMoney", 50);
    }

    // ===== Tool Data Persistence =====

    // Save tool unlock status
    //public static void SaveToolUnlockStatus(int toolIndex, bool isUnlocked)
    //{
    //    PlayerPrefs.SetInt($"ToolUnlocked_{toolIndex}", isUnlocked ? 1 : 0);
    //    PlayerPrefs.Save();
    //}

    //// Load tool unlock status
    //public static bool LoadToolUnlockStatus(int toolIndex)
    //{
    //    return PlayerPrefs.GetInt($"ToolUnlocked_{toolIndex}", toolIndex == 0 ? 1 : 0) == 1;
    //}

    //// Save currently selected tool index
    //public static void SaveSelectedTool(int toolIndex)
    //{
    //    PlayerPrefs.SetInt("SelectedTool", toolIndex);
    //    PlayerPrefs.Save();
    //}

    //// Load currently selected tool index
    //public static int LoadSelectedTool()
    //{
    //    return PlayerPrefs.GetInt("SelectedTool", 0);
    //}
}
