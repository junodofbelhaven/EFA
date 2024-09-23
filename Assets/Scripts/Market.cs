/*using UnityEngine;

public class Market : MonoBehaviour
{
    public GameObject marketUI;

    void Start()
    {
        // Oyun baþladýðýnda market ekranýný kapalý tutuyoruz
        marketUI.SetActive(false);
    }

    void Update()
    {
        // "B" tuþuna basýldýðýnda market panelini aç/kapat
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleMarket();
            Debug.Log("B tuþuna basýldý");
        }
    }

    public void ToggleMarket()
    {
        bool isActive = marketUI.activeSelf;
        marketUI.SetActive(!isActive);
        Debug.Log("Market ekraný: " + (isActive ? "Kapatýldý" : "Açýldý"));
    }
}*/

using UnityEngine;
using UnityEngine.UI; // Button türünü tanýmlamak için gerekli

public class Market : MonoBehaviour
{
    public GameObject marketUI;
    public Button selectAK47Button; // Butonu referans al
    public GameObject playerPrefab; // Player prefab'ýnýzý buraya atayýn
    public GameObject marketPanel;
    public GameObject ak_47;


    private GameObject playerInstance;

    public int playerMoney = 3000;
    public Text moneyText;
    public int ak47price = 2700;
    public Text notificationText;//satýn alma bildirimi(yetersiz para vs.)

    public bool hasWeapon; // Oyuncunun silaha sahip olup olmadigini kontrol eden degisken


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

        // Player prefab'ini sahnede bulup referans alin
        playerInstance = GameObject.FindWithTag("playerCatman"); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            // Paneli aç veya kapat
            marketPanel.SetActive(!marketPanel.activeSelf);

            // Oyun etkileþimini durdur veya baþlat
           // Time.timeScale = marketPanel.activeSelf ? 0 : 1;

            // Mouse imlecini serbest býrak veya kilitle
            if (marketPanel.activeSelf)
            {
                Debug.Log("mouse görünür");
                Cursor.lockState = CursorLockMode.None;  // Mouse'u serbest býrak
                Cursor.visible = true;  // Mouse imlecini görünür yap
            }
            else
            {
                Debug.Log("mouse kapalý");
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
            // Market aktif olduðunda fareyi serbest býrak ve göster
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // Market kapalý olduðunda fareyi kilitle ve gizle
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    void OnSelectAK47Clicked()
    {
        if (playerMoney >= ak47price)
        {
            ak_47.SetActive(true);
            playerMoney -= ak47price;//para miktarýný dusurduk
            UpdateMoneyText();//para miktarini guncelledik
            notificationText.text = "FPS-AK47 satýn alýndý.";
            Debug.Log("FPS-AK47 satýn alýndý.");

            // Weapon script'ine hasWeapon deðerini güncelle
            Weapon weaponScript = playerPrefab.GetComponentInChildren<Weapon>();
            if (weaponScript != null)
            {
                weaponScript.hasWeapon = true;
            }

        }
        else
        {
            notificationText.text = "yetersiz  para miktarý ";//yetersiz para miktarý olunca
            Debug.Log("yetersiz para miktarý. ");

           //hasWeapon = false;
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

        // Player prefab'ýnýn içindeki silahý bul
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

