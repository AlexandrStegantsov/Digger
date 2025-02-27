using UnityEngine;
using System.Collections;

public class OrePickup : MonoBehaviour
{
    public InventoryManager.OreType oreType;
    public int amount = 1;

    private SphereCollider detectionCollider;
    private Rigidbody rb;
    private bool hasBeenActivated = false;
    private string oreID;
    private BoxCollider boxCollider;
    private Vector3 initialPosition; // Store initial spawn position

    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        //rb.isKinematic = true; // Start with physics disabled
        //rb.useGravity = false;

        //detectionCollider = GetComponent<SphereCollider>();
        //if (detectionCollider == null)
        //{
        //    detectionCollider = gameObject.AddComponent<SphereCollider>();
        //    detectionCollider.isTrigger = true;
        //}

        //boxCollider = GetComponent<BoxCollider>();

        //boxCollider.enabled = false; // Disable physical collision until activation
        //detectionCollider.radius = 0.3f;

        //initialPosition = transform.position; // Store the original underground position

        //Vector3 roundedPos = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
        //oreID = $"Ore_{roundedPos.x}_{roundedPos.y}_{roundedPos.z}";

        //if (PlayerPrefs.HasKey(oreID))
        //{
        //    Destroy(gameObject);
        //}
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!hasBeenActivated)
    //    {
    //        ActivateOre();
    //    }
    //}

    private void ActivateOre()
    {
        Debug.Log("Ore detected, bringing above terrain.");

        // Move ore slightly above its spawn position
        float liftAmount = 0.5f; // Adjust how much it rises
        transform.position = new Vector3(initialPosition.x, initialPosition.y + liftAmount, initialPosition.z);

        // Enable physics for a natural fall/rest effect
        rb.isKinematic = false;
        rb.useGravity = true;

        // Disable detection so it doesn’t trigger again
        detectionCollider.enabled = false;

        // Enable collision so it behaves like a real object
        if (boxCollider != null)
        {
            boxCollider.enabled = true;
        }

        hasBeenActivated = true;

        //PlayerPrefs.SetInt(oreID, 1);
        //PlayerPrefs.Save();
    }

    public void CollectOre()
    {
        //AudioManager.Instance.PlaySFX("interact");
        //rb.velocity = Vector3.zero;
        //rb.angularVelocity = Vector3.zero;
        //rb.isKinematic = true;
        //rb.useGravity = false;
        //GetComponent<Collider>().isTrigger = true;

        //hasBeenActivated = false;

        Destroy(gameObject);
    }

    //public void SetDetectionRadius(float radius)
    //{
    //    if (!hasBeenActivated && detectionCollider != null)
    //    {
    //        detectionCollider.radius = radius;
    //    }
    //}
}

