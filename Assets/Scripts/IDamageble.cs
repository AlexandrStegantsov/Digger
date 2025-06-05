using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDamageble : MonoBehaviour
{
   public int currentHp;
   public int maxHp;

   private void Start()
   {
      currentHp = maxHp;
   }

   public void TakeDamage(int damage)
   {
      currentHp -= damage;
      if (currentHp <= 0)
      {
         currentHp = 0;
         Die();
      }
   }

   public virtual void Die()
   {
      Destroy(gameObject);
      Debug.LogError("Destroyed");
   }
}
