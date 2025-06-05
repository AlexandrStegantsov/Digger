using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
   [SerializeField] private GameObject Controls;
   [SerializeField] private GameObject Music;
   private Menu menu;
   private void Start()
   {
      menu = Menu.Instance;
   }

   private void NextTab()
   {
      Music.SetActive(false);
      Controls.SetActive(true);
   }

   private void PreviousTab()
   {
      Music.SetActive(true);
      Controls.SetActive(false);
   }

   private void Back()
   {
      menu.MenuOpen();
   }
}
