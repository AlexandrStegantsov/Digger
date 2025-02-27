using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShovelBehavior : MonoBehaviour
{
    public Button digButton; // Assign the UI button in the Inspector

    public Vector3 originalPos;
    public Vector3 digPos;
    private bool isDigging = false;

    private void Start()
    {
        // Ensure shovel starts at original position
        transform.localPosition = originalPos;

        // Check if the button is assigned
        if (digButton != null)
        {
            digButton.onClick.AddListener(OnDigButtonClicked);
        }
        else
        {
            Debug.LogWarning("Dig button not assigned in the Inspector!");
        }
    }

    private void OnDigButtonClicked()
    {
        if (!isDigging)
        {
            StartCoroutine(PerformDig());
        }
    }

    private IEnumerator PerformDig()
    {
        isDigging = true;

        float duration = 0.2f; // Time for each movement
        yield return StartCoroutine(MoveShovel(digPos, duration)); // Move to dig position
        yield return StartCoroutine(MoveShovel(originalPos, duration)); // Move back

        isDigging = false;
    }

    private IEnumerator MoveShovel(Vector3 targetPos, float time)
    {
        Vector3 startPos = transform.localPosition;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.localPosition = Vector3.Lerp(startPos, targetPos, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPos; // Ensure exact position
    }
}


