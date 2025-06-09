using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class WeaponScrollController : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform[] items;
    public Image highlight;
    public int visibleCount = 3;
    public float itemHeight = 120f;
    private Menu menu;
    private PlayerActions inputActions;
    private int selectedIndex = 0;

    void Start()
    {
        UpdateSelection();
    }
    private void OnEnable()
    {
        inputActions = new PlayerActions();
        inputActions.Enable();

        menu = Menu.Instance;
        
        inputActions.UI.Down.performed += ctx => ScrollNext();
        inputActions.UI.Up.performed += ctx => ScrollPrevious();
    }
    /*void Update()
    {
        if (Gamepad.current == null) return;

        if (Gamepad.current.dpad.down.wasPressedThisFrame)
        {
            ChangeSelection(1);
        }
        else if (Gamepad.current.dpad.up.wasPressedThisFrame)
        {
            ChangeSelection(-1);
        }
    }*/

    private void ScrollNext()
    {
        ChangeSelection(1);
    }

    private void ScrollPrevious()
    {
        ChangeSelection(-1);
    }
    void ChangeSelection(int delta)
    {
        selectedIndex = Mathf.Clamp(selectedIndex + delta, 0, items.Length - 1);
        UpdateSelection();
    }

    void UpdateSelection()
    {
        // Центрирование по объекту
        float scrollPos = (float)selectedIndex / (items.Length - visibleCount);
        scrollRect.verticalNormalizedPosition = 1f - Mathf.Clamp01(scrollPos);

        // Подсветка (если есть highlight Image)
        //highlight.transform.position = items[selectedIndex].transform.position;
    }
}