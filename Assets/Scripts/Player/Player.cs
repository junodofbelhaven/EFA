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
    public int currentCash = 0;

    //components
    Health health;
    private void OnEnable()
    {
        health = GetComponent<Health>();
        health.initializeHealth(initialHealthValue);
    }

    private void OnDisable()
    {

        health.OnHit.RemoveAllListeners();
    }


    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
    }

    public void DeathEffect() => Debug.Log("Player Died");

}
