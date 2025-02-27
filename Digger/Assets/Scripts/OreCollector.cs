using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OreCollector : MonoBehaviour
{
    public float interactionDistance = 3f;
    public LayerMask oreLayer;
    public Button collectButton;
    public GameObject oreInfoPanel;
    public TextMeshProUGUI oreNameText;
    public SellUIManager sellUIManager;

    private OrePickup currentOre;

    private void Start()
    {
        if (collectButton != null)
        {
            collectButton.onClick.AddListener(TryCollectOre);
        }
        else
        {
            Debug.LogWarning("No interact button assigned!");
        }

        if (oreInfoPanel != null)
        {
            oreInfoPanel.SetActive(false);
        }
    }

    private void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, interactionDistance, oreLayer, QueryTriggerInteraction.Ignore);

        currentOre = null;
        oreInfoPanel?.SetActive(false);
        collectButton.gameObject.SetActive(false); // Hide the button by default

        foreach (RaycastHit hit in hits)
        {
            if (!IsDirectlyVisible(hit.point))
            {
                continue; // Skip ores behind obstacles
            }

            OrePickup ore = hit.collider.GetComponent<OrePickup>();
            if (ore != null)
            {
                currentOre = ore;
                oreNameText.text = ore.oreType.ToString();
                oreInfoPanel.SetActive(true);
                collectButton.gameObject.SetActive(true); // Show the button when ore is detected
                break;
            }
        }
    }


    private bool IsDirectlyVisible(Vector3 targetPosition)
    {
        Vector3 origin = Camera.main.transform.position;
        Vector3 direction = (targetPosition - origin).normalized;
        float distance = Vector3.Distance(origin, targetPosition);

        if (Physics.Raycast(origin, direction, out RaycastHit obstacleHit, distance, ~oreLayer, QueryTriggerInteraction.Ignore))
        {
            // If something is hit before the ore, it's blocked
            return false;
        }
        return true;
    }


    private void TryCollectOre()
    {
        if (currentOre != null)
        {
            if (InventoryManager.Instance.AddOre(currentOre.oreType, currentOre.amount))
            {
                sellUIManager.UpdateSellPanel();

                // Instead of destroying, return to the ore pool
                currentOre.CollectOre();
            }
        }
    }
}
