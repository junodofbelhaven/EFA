using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Health : MonoBehaviour
{
    [field: SerializeField]
    public int CurrentHealth { get; set; }

    public UnityEvent OnDeath, OnHit;
    //private Player player;

    private void Start()
    {
        //player = GetComponent<Player>();
        //OnDeath?.AddListener(player.DeathEffect);
        OnDeath.AddListener(DisableMovement);
        OnDeath.AddListener(RemoveFromScene);  // Karakteri sahneden kaldýrýlmasý

    }
    public void getHit(int damageValue, GameObject sender)
    {

        CurrentHealth -= damageValue;


        if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
        else
        {
            OnHit?.Invoke();
        }

    }

    // Karakteri sahneden kaldýr
    void RemoveFromScene()
    {
        Destroy(gameObject);  // Bu fonksiyonla karakter sahneden kaldýrýlacak
    }

    void DisableMovement()
    {
        PlayerMovement  movementscript = GetComponent<PlayerMovement>();

        //playerMovement scripti enabled yapýlýyor
        if (movementscript != null)
        {
            movementscript.enabled = false;
        }
        // Silah script'ini devre dýþý býrak
        Weapon weaponScript = GetComponent<Weapon>();
        if (weaponScript != null)
        {
            weaponScript.enabled = false;
        }

        // Kamera hareket script'ini devre dýþý býrak
        CameraMovement cameraScript = GetComponent<CameraMovement>();
        if (cameraScript != null)
        {
            cameraScript.enabled = false;
        }
    }

    public void initializeHealth(int startingHealth)
    {

        if (startingHealth <= 0)
        {
            startingHealth = 100;
        }

        CurrentHealth = startingHealth;
    }

}
