using System.Collections;
using UnityEngine;

public class JetEnemyAI : MonoBehaviour
{
    public Transform player;
    public float speed = 50f;
    public float rotationSpeed = 2f;
    public float distanceToChangeTactic = 200f;
    public float maneuverRadius = 100f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;
    public float shootDistance = 100f;
    public float aimAngle = 30f;

    private float fireCooldown;
    private Vector3 currentTarget;
    private float changeTacticCooldown = 3f;
    private float tacticTimer = 0f;

    private enum Tactic { Behind, Side, Front }
    private Tactic currentTactic;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PickNewTactic();
    }

    void Update()
    {
        if (player == null) return;

        tacticTimer -= Time.deltaTime;
        if (tacticTimer <= 0)
        {
            PickNewTactic();
        }

        MoveToTarget();
        HandleShooting();
    }

    void PickNewTactic()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > distanceToChangeTactic)
        {
            currentTactic = Tactic.Behind;
        }
        else
        {
            currentTactic = (Tactic)Random.Range(0, 3);
        }

        switch (currentTactic)
        {
            case Tactic.Behind:
                currentTarget = player.position - player.forward * 50f;
                break;
            case Tactic.Side:
                Vector3 sideOffset = player.right * (Random.value > 0.5f ? 1 : -1) * maneuverRadius;
                currentTarget = player.position + sideOffset;
                break;
            case Tactic.Front:
                currentTarget = player.position + player.forward * 50f;
                break;
        }

        tacticTimer = changeTacticCooldown;
    }

    void MoveToTarget()
    {
        Vector3 direction = (currentTarget - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void HandleShooting()
    {
        if (firePoint == null || bulletPrefab == null) return;

        fireCooldown -= Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (distanceToPlayer <= shootDistance && angleToPlayer <= aimAngle && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = BulletPool.Instance.GetBullet();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * 1000f;
        }

        // Вернуть пулю через 5 секунд
        StartCoroutine(ReturnBulletAfterDelay(bullet, 5f));
    }

    private IEnumerator ReturnBulletAfterDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        BulletPool.Instance.ReturnBullet(bullet);
    }

}
