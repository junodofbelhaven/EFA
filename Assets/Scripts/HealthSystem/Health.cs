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

    public void initializeHealth(int startingHealth)
    {

        if (startingHealth <= 0)
        {
            startingHealth = 100;
        }

        CurrentHealth = startingHealth;
    }

}
