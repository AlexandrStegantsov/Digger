using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TargetUI : MonoBehaviour
{
    public TargetHighlighter highlighter;
    public GameObject indicatorUI; 
    public Camera mainCamera;
    public bool isTargeting = false;
    [SerializeField] private Image _image;

    private void Start()
    {
       
        _image.color = Color.green;
    }

    void Update()
    {
        if (highlighter.currentTarget != null)
        {
            indicatorUI.SetActive(true);
            if (!isTargeting)
            {
                StartCoroutine("Indicator");
            }
            Vector3 screenPos = mainCamera.WorldToScreenPoint(highlighter.currentTarget.position);
            indicatorUI.transform.position = screenPos;
        }
        else
        {
            _image.color = Color.green;
            indicatorUI.SetActive(false);
            isTargeting = false;
            
        }
    }

    private IEnumerator Indicator()
    {
        _image.color = Color.green;
        isTargeting = true;
        yield return new WaitForSecondsRealtime(2f);
        _image.color = Color.red;
        yield return new WaitForSeconds(5f);
        highlighter.currentTarget = null;
    }
}