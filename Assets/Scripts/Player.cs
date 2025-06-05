using UnityEngine;

public class Player : IDamageble
{
    public GameObject Airplane;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint; 
    public float bulletForce = 1;
    public Camera mainCamera;
    public GameObject Screen;

    public override void Die()
    {
        base.Die();
        Airplane.gameObject.SetActive(false);
        Screen.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || bulletSpawnPoint == null || mainCamera == null)
        {
            Debug.LogError("Не назначены bulletPrefab, bulletSpawnPoint или mainCamera!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.drag = 0;

            // Получаем луч из центра экрана
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            Vector3 shootDirection = ray.direction.normalized;

            rb.velocity = shootDirection * bulletForce;
            bullet.transform.rotation = Quaternion.LookRotation(shootDirection); // повернуть пулю по направлению
        }
    }


}