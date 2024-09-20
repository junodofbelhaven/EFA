/*using UnityEngine;

public class Market : MonoBehaviour
{
    public GameObject marketUI;

    void Start()
    {
        // Oyun ba�lad���nda market ekran�n� kapal� tutuyoruz
        marketUI.SetActive(false);
    }

    void Update()
    {
        // "B" tu�una bas�ld���nda market panelini a�/kapat
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleMarket();
            Debug.Log("B tu�una bas�ld�");
        }
    }

    public void ToggleMarket()
    {
        bool isActive = marketUI.activeSelf;
        marketUI.SetActive(!isActive);
        Debug.Log("Market ekran�: " + (isActive ? "Kapat�ld�" : "A��ld�"));
    }
}*/

using UnityEngine;
using UnityEngine.UI; // Button t�r�n� tan�mlamak i�in gerekli

public class Market : MonoBehaviour
{
    public GameObject marketUI;
    public Button selectAK47Button; // Butonu referans al
    public GameObject playerPrefab; // Player prefab'�n�z� buraya atay�n
    public GameObject marketPanel;
    public GameObject ak_47;


    private GameObject playerInstance;

    public int playerMoney = 3000;
    public Text moneyText;
    public int ak47price = 2700;
    public Text notificationText;//sat�n alma bildirimi(yetersiz para vs.)


    void Start()
    {
        marketUI.SetActive(false);
        ak_47.SetActive(false);
        notificationText.text = "";

        UpdateMoneyText();

        if (selectAK47Button != null)
        {
            selectAK47Button.onClick.AddListener(OnSelectAK47Clicked);
        }

        // Player prefab'�n� sahnede bulup referans al�n
        playerInstance = GameObject.FindWithTag("playerCatman"); // Oyuncu prefab'�n�z� "Player" tag'i ile buluyoruz
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            // Paneli a� veya kapat
            marketPanel.SetActive(!marketPanel.activeSelf);

            // Oyun etkile�imini durdur veya ba�lat
           // Time.timeScale = marketPanel.activeSelf ? 0 : 1;

            // Mouse imlecini serbest b�rak veya kilitle
            if (marketPanel.activeSelf)
            {
                Debug.Log("mouse g�r�n�r");
                Cursor.lockState = CursorLockMode.None;  // Mouse'u serbest b�rak
                Cursor.visible = true;  // Mouse imlecini g�r�n�r yap
            }
            else
            {
                Debug.Log("mouse kapal�");
                Cursor.lockState = CursorLockMode.Locked;  // Mouse'u kilitle
                Cursor.visible = false;  // Mouse imlecini gizle
            }
           
        }
    }

    void ToggleMarket()
    {
        bool isActive = marketUI.activeSelf;
        marketUI.SetActive(!isActive);

        if (marketUI.activeSelf)
        {
            // Market aktif oldu�unda fareyi serbest b�rak ve g�ster
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // Market kapal� oldu�unda fareyi kilitle ve gizle
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    void OnSelectAK47Clicked()
    {
        // Burada karakterin envanterine veya se�ili silah�na AK47 eklenir
        //Debug.Log("FPS-AK47 se�ildi!");
        //ak_47.SetActive(true);
        // �rnek: PlayerManager veya benzeri bir sistemle se�ili silah� ekleyin

        if (playerMoney >= ak47price)
        {
            ak_47.SetActive(true);
            playerMoney -= ak47price;//para miktar�n� dusurduk
            UpdateMoneyText();//para miktarini guncelledik
            notificationText.text = "FPS-AK47 sat�n al�nd�.";
            Debug.Log("FPS-AK47 sat�n al�nd�.");

        }
        else
        {
            notificationText.text = "yetersiz  para miktar� ";//yetersiz para miktar� olunca
            Debug.Log("yetersiz para miktar�. ");
        }

    }

    void UpdateMoneyText()
    {
        moneyText.text = "Para:  $ " + playerMoney.ToString();
    }

    void ActivateWeapon(string weaponName)
    {
        if (playerInstance == null)
        {
            Debug.LogError("Player instance not found!");
            return;
        }

        // Player prefab'�n�n i�indeki silah� bul
        Weapon[] weapons = playerInstance.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            if (weapon.gameObject.name == weaponName)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }
}

