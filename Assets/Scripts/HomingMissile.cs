using UnityEngine;

public class HomingMissilee : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float rotateSpeed = 200f;
    [SerializeField] private float lifeTime = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime); 
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 rotateAmount = Vector3.Cross(transform.forward, direction);

        rb.angularVelocity = rotateAmount * rotateSpeed * Mathf.Deg2Rad;
        rb.velocity = transform.forward * speed;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damagable") )
        {
           
            other.GetComponent<IDamageble>().Die();
        }

      
        Destroy(gameObject);
    }
}