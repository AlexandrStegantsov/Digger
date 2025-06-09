using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private PlayerActions inputActions;

    [Header("Tabs")]
    [SerializeField] private GameObject Upgrade;
    [SerializeField] private GameObject ShopTab;

    [Header("UI")]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    [SerializeField] private float elementHeight = 100f;
    [SerializeField] private int visibleCount = 3;

    private int currentIndex = 0;
    [SerializeField] private Button[] weaponButtons;

    private void OnEnable()
    {
        inputActions = new PlayerActions();
        inputActions.Enable();

        inputActions.UI.Next.performed += NextTab;
        inputActions.UI.Back.performed += PreviousTab;
        inputActions.UI.Down.performed += ctx => MoveSelection(1);
        inputActions.UI.Up.performed += ctx => MoveSelection(-1);

        // Найти все кнопки
        

        // Установить первую как выбранную
        if (weaponButtons.Length > 0)
        {
            currentIndex = 0;
            EventSystem.current.SetSelectedGameObject(weaponButtons[0].gameObject);
        }
    }

    private void OnDisable()
    {
        inputActions.UI.Next.performed -= NextTab;
        inputActions.UI.Back.performed -= PreviousTab;
        inputActions.UI.Down.performed -= ctx => MoveSelection(1);
        inputActions.UI.Up.performed -= ctx => MoveSelection(-1);
        inputActions.Disable();
    }

    private void NextTab(InputAction.CallbackContext context)
    {
        Upgrade.SetActive(true);
        ShopTab.SetActive(false);
    }

    private void PreviousTab(InputAction.CallbackContext context)
    {
        Upgrade.SetActive(false);
        ShopTab.SetActive(true);
    }

    private void MoveSelection(int direction)
    {
        if (weaponButtons.Length == 0) return;

        int newIndex = Mathf.Clamp(currentIndex + direction, 0, weaponButtons.Length - 1);
        if (newIndex != currentIndex)
        {
            currentIndex = newIndex;
            EventSystem.current.SetSelectedGameObject(weaponButtons[currentIndex].gameObject);
            ScrollIfNeeded(currentIndex);
        }
    }

    private void ScrollIfNeeded(int index)
    {
        float totalHeight = content.rect.height - scrollRect.viewport.rect.height;
        float targetPos = index * elementHeight;
        float normalized = 1f - Mathf.Clamp01(targetPos / totalHeight);
        scrollRect.verticalNormalizedPosition = normalized;
    }
}
