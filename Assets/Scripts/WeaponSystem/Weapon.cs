using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Weapon : MonoBehaviour
{

    public Camera playerCamera;

    //shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    float shootingDelay = 0.2f;

    //burst
    public int burstBulletsLeft;
        
    //bullet properties
    public WeaponType weaponType;
    PhotonView photonView;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletPrefabLifetime = 3f;

     // baslangictaki toplam mermi sayisi
    //public int currentAmmo ;     // Su  an kullanilabilir mermi sayisi
    public TextMeshProUGUI currentAmmoText; // Soldaki mermi (mevcut mermi)
    public TextMeshProUGUI totalAmmoText;  // Sagdaki mermi (yedek mermi)

    //public int totalAMmo = 60;//yedek mermiler dahil tam mermi sayisi
    public int totalAmmo = 60; 
    public int currentAmmo = 30;// mevcut mermi sayisi
    public int maxAmmoPerMaganize = 30;//her sarjordeki mermi sayisi

    public bool hasWeapon; // Oyuncunun silaha sahip olup olmadýðýný kontrol eden deðiþken

    public Market market;



    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;
    //JustForTest
    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = weaponType.BulletPerBurstShot;
    }

    private void Start()
    {
        if (market == null)
        {
            market = FindObjectOfType<Market>();  // Eðer market atanmadýysa, sahnede otomatik bul
        }
        photonView = GetComponent<PhotonView>();
        UpdateAmmoUI();
    }

    // Update is called once per frame
    void Update()
    {
        //if (currentShootingMode == ShootingMode.Auto)
        //{
        //    isShooting = Input.GetKey(KeyCode.Mouse0);
        //}
        //else if ((currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst))
        //{
        //    isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        //}

        //if (readyToShoot && isShooting)
        //{
        //    burstBulletsLeft = weaponType.BulletPerBurstShot;
        //    FireWeapon(); 
        //}


        Debug.Log("hasWeapon deðeri: " + hasWeapon);  // Bu mesaj, hasWeapon'ýn doðru olup olmadýðýný kontrol eder.

        // Eðer silah satýn alýnmadýysa, ateþ etmeyi engelle
        if (!hasWeapon)
        {
            Debug.Log("Silah alýnmadý, ateþ edilemiyor.");
            return;  // hasWeapon false ise ateþ etme
        }

        if (currentShootingMode == ShootingMode.Auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (readyToShoot && isShooting)
        {
            burstBulletsLeft = weaponType.BulletPerBurstShot;
            FireWeapon();
        }

    }

    //private void FireWeapon()
    //{
    //    if (photonView.IsMine){
    //    readyToShoot = false;
    //    Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

    //    //instantiate the bullet
    //    GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

    //    //adjusting direction
    //    bullet.transform.forward = shootingDirection;

    //    //shoot the bullet
    //    bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

    //    //destroy bullet after some time
    //    StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifetime));

    //    // checking if we are done shooting
    //    if (allowReset)
    //    {
    //        Invoke("ResetShot", shootingDelay);
    //        allowReset = false;
    //    }

    //        // burst mode
    //        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
    //        {
    //            burstBulletsLeft--;
    //            Invoke("FireWeapon", shootingDelay);
    //        }
    //    }

    //}

    private void FireWeapon()
    {
        if (photonView.IsMine && currentAmmo > 0)
        {
            readyToShoot = false;
            Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.transform.forward = shootingDirection;
            bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
            StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifetime));

            currentAmmo--;  // Mevcut mermiyi düþür
            UpdateAmmoUI(); // UI'da mermiyi güncelle

            if (currentAmmo == 0 && totalAmmo > 0)  // Eðer mevcut mermi biterse
            {
                Reload();  // Yedek mermiden þarjör doldur
            }

            if (allowReset)
            {
                Invoke("ResetShot", shootingDelay);
                allowReset = false;
            }

            if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
            {
                burstBulletsLeft--;
                Invoke("FireWeapon", shootingDelay);
            }
        }

        // Eðer mevcut mermi biterse 1 saniye bekle
        if (currentAmmo == 0)
        {
            StartCoroutine(WaitBeforeNextShot());
        }
    }
    private IEnumerator WaitBeforeNextShot()
    {
        yield return new WaitForSeconds(1f);  // 1 saniye bekle
        readyToShoot = true;  // Tekrar ateþ etmeye izin ver
    }



    void Reload()
    {
        // Þarjörü yeniden doldur
        int ammoToLoad = Mathf.Min(maxAmmoPerMaganize, totalAmmo);
        currentAmmo = ammoToLoad;
        totalAmmo -= ammoToLoad;
        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        currentAmmoText.text = currentAmmo.ToString();
        totalAmmoText.text = totalAmmo.ToString();
    }


    private void swapWeaponMode()
    {
        /*weapon swap function that operates with if conditions based on
        boolean operators that implemented in weaponType class.*/
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    private Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray,out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        
        float x = UnityEngine.Random.Range(-weaponType.BulletSpread, weaponType.BulletSpread);
        float y = UnityEngine.Random.Range(-weaponType.BulletSpread, weaponType.BulletSpread);

        return direction + new Vector3(x, y, 0);

    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float bulletPrefabLifetime)
    {
        yield return new WaitForSeconds(bulletPrefabLifetime);
        Destroy(bullet);
    }
}
