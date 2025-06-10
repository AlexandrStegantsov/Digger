using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : IDamageble 
{
    public GameObject Airplane;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint; 
    public float bulletForce = 1;
    public Camera mainCamera;
    public GameObject Screen;
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private float missileLaunchForce = 20f;
    public TargetHighlighter missileHighlighter;
    private Transform target;
    public int MaxAmmo = 100;
    public int CurrentAmmo = 100;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private List<WeaponUpgrade> currentUpgrades = new();

    
    
    public override void Die()
    {
        base.Die();
        Airplane.gameObject.SetActive(false);
        Screen.SetActive(true);
    }
    private int GetUpgradeLevel(WeaponType type)
    {
        var upgrade = currentUpgrades.Find(u => u.weaponType == type);
        return upgrade != null ? upgrade.level : 0;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        { 
            FireMissile();
        }
    }

    
    private void FixedUpdate()
    {
        SetTarget(missileHighlighter.currentTarget);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private IEnumerator Reload()
    {
        ammoText.text = "Reloading...";
        yield return new WaitForSeconds(2f);
        CurrentAmmo = MaxAmmo;
        ammoText.text = CurrentAmmo.ToString() + "/" + MaxAmmo.ToString();
    }
    private void Shoot()
    {
        if (CurrentAmmo == 0)
        {
            StartCoroutine("Reload");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.drag = 0;
            AmmoDecrease();

            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            Vector3 shootDirection = ray.direction.normalized;

            float forceMultiplier = 1f;
            int level = GetUpgradeLevel(WeaponType.MachineGun);
            if (level == 2) forceMultiplier += 0.1f;
            if (level == 3) bulletForce *= 1.15f;
            if (level == 4) {/* уменьшить разброс — логика в другом месте */}

            rb.velocity = shootDirection * bulletForce * forceMultiplier;
            bullet.transform.rotation = Quaternion.LookRotation(shootDirection);
        }
    }


    private void AmmoDecrease()
    {
        CurrentAmmo--;
        ammoText.text = CurrentAmmo.ToString() + "/" + MaxAmmo.ToString();
    }
    public void FireMissile()
    {
        if (target == null) return;

        GameObject missile = Instantiate(missilePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = missile.GetComponent<Rigidbody>();

        float force = missileLaunchForce;
        int level = GetUpgradeLevel(WeaponType.HomingRocket);
        if (level == 3) force *= 1.2f;
        rb.velocity = bulletSpawnPoint.forward * force;

        HomingMissilee homing = missile.GetComponent<HomingMissilee>();
        homing.SetTarget(target);

        if (level == 5)
        {
            // двойной залп
            StartCoroutine(LaunchSecondMissile());
        }
    }

    private IEnumerator LaunchSecondMissile()
    {
        yield return new WaitForSeconds(0.25f);
        FireMissile();
    }


}