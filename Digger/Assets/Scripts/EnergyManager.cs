using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    private float maxEnergy = 100f;
    private float currentEnergy;
    private float energyDepletionPerDig = 5f;
    public Image energyImage;
    public GameObject energyDepletedPanel;
    public bool isEnergyDepleted = false;

    private void Start()
    {
        currentEnergy = maxEnergy;
        energyImage.fillAmount = currentEnergy / maxEnergy;
        energyDepletedPanel.SetActive(false);
    }

    public void CheckEnergy()
    {
        if (isEnergyDepleted)
        {
            energyDepletedPanel.SetActive(true); // Ensure the panel is shown if already depleted
            return;
        }
    }

    public void OnDigButtonClick()
    {
        CheckEnergy();

        if (currentEnergy > 0)
        {
            currentEnergy -= energyDepletionPerDig;
            if (currentEnergy <= 0)
            {
                currentEnergy = 0;
                isEnergyDepleted = true;
                energyDepletedPanel.SetActive(true); // Activate panel when energy runs out
            }
            energyImage.fillAmount = currentEnergy / maxEnergy;
        }
    }

    void GiveReward()
    {
        RefillEnergy();
    }


    public void RefillEnergy()
    {
        if (IsEnergyFull()) return; // Prevent refilling if already full

        currentEnergy = maxEnergy;
        energyImage.fillAmount = currentEnergy / maxEnergy;
        isEnergyDepleted = false;
        energyDepletedPanel.SetActive(false);
    }

    public bool IsEnergyFull()
    {
        return currentEnergy >= maxEnergy;
    }

    private void HideEnergyDepletedPanel()
    {
        energyDepletedPanel.SetActive(false);
    }
}
