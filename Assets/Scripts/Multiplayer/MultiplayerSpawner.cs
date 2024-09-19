using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class MultiplayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;

    // Spawn noktalar�n� ayarla
    public Transform redTeamSpawnPoint;
    public Transform blueTeamSpawnPoint;

    // UI butonlar�
    public Button redButton;
    public Button blueButton;

    // Oyuncunun tak�m�n� tutmak i�in bir de�i�ken
    private string selectedTeam;
    private bool playerStatus = false;


    [SerializeField] private CanvasGroup myGroup;

    [SerializeField] private bool fadeOut = false;

    public GameObject marketpanel;

    public void HideUI()
    {
        fadeOut = true;
    }


    void Start()
    {
        // Butonlara t�klama olaylar�n� ekle
        redButton.onClick.AddListener(() => SelectTeam("Red"));
        blueButton.onClick.AddListener(() => SelectTeam("Blue"));
    }

    private void Update()
    {
        Fade();
        //CursorZort();

        if (playerStatus && marketpanel.activeSelf ==false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Tak�m se�imini yap
    public void SelectTeam(string team)
    {
        selectedTeam = team;

        // Se�ilen tak�ma g�re oyuncuyu spawn et
        SpawnPlayer();
    }

    // Oyuncuyu tak�m�na g�re do�ru yerde spawn et
    void SpawnPlayer()
    {
        Vector3 spawnPosition = Vector3.zero;

        // Tak�ma g�re spawn pozisyonunu belirle
        if (selectedTeam == "Red")
        {
            spawnPosition = redTeamSpawnPoint.position;
        }
        else if (selectedTeam == "Blue")
        {
            spawnPosition = blueTeamSpawnPoint.position;
        }

        // PhotonNetwork.Instantiate ile player prefab�n� do�ru yerde spawn et
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);

        // UI butonlar�n� gizleyin veya devre d��� b�rak�n
        redButton.gameObject.SetActive(false);
        blueButton.gameObject.SetActive(false);

        playerStatus = true;

        
    }

    private void CursorZort()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;  
    }

    private void Fade()
    {
        if (fadeOut)
        {
            if (myGroup.alpha >= 0)
            {
                myGroup.alpha -= Time.deltaTime;
                if (myGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }
}
