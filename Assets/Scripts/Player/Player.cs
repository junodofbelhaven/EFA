using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{

    bool isAlive = true;

    //health condition
    public int initialHealthValue = 100;
    public int armor = 0;

    //economy
    public int currentCash = 1000;

    //components
    Health health;

    public List<GameObject> weapons = new List<GameObject>(); // Oyuncunun silah envanteri


    public void AddWeaponToInventory(GameObject weapon)
    {
        weapons.Add(weapon);
        Debug.Log("Silah envantere eklendi: " + weapon.name);
    }
    private void OnEnable()
    {
        health = GetComponent<Health>();
        health.initializeHealth(initialHealthValue);
    }

    private void OnDisable()
    {

        health.OnHit.RemoveAllListeners();
    }
    //playerTEst

    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
    }

    public void DeathEffect() => Debug.Log("Player Died");

}
