using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Settings : MonoBehaviour
{
   [SerializeField] private GameObject Controls;
   [SerializeField] private GameObject Music;
   private Menu menu;
   private PlayerActions inputActions;
   private void OnEnable()
   {
      inputActions = new PlayerActions();
      menu = Menu.Instance;
      inputActions.Enable();
      inputActions.UI.Next.performed += NextTab;
      inputActions.UI.Back.performed += PreviousTab;
   }
   private void OnDisable()
   {
      inputActions.UI.Next.performed -= NextTab;
      inputActions.UI.Back.performed -= PreviousTab;
      inputActions.Disable();
   }
   private void NextTab(InputAction.CallbackContext context)
   {
      Music.SetActive(false);
      Controls.SetActive(true);
   }

   private void PreviousTab(InputAction.CallbackContext context)
   {
      Music.SetActive(true);
      Controls.SetActive(false);
   }

   private void Back()
   {
      menu.MenuOpen();
   }
   
}
