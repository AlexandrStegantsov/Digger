using System.Collections;
using UnityEngine;

public class JetBullet : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage = 100;
    private BoxCollider boxCollider;

    private void Start()
    {
        Destroy(gameObject, lifetime);
         boxCollider = GetComponent<BoxCollider>();
        // StartCoroutine("Trigger");
    }

    private IEnumerator Trigger()
    {
        boxCollider.isTrigger = false;
        yield return new WaitForSeconds(0.1f);
        boxCollider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damagable") )
        {
           
           other.GetComponent<IDamageble>().TakeDamage(damage);
        }

      
        Destroy(gameObject);
    }
}