using Steamworks;
using System.Collections;
using System.IO;
using TMPro;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExodusManager : MonoBehaviour
{
    [Header("All the references")]

    public AchievementManager achievementManager;
    public static ExodusManager Instance;
    public ChoiceManager choiceManager;
    public MapManagment managmentMap;
    public TMP_InputField field;
    public Exodus exodus;
    public Text money;
    public Image sawDust;
    public Image happines;
    public Image development;

    public Image sawDustLogo;
    public Image happinesLogo;
    public Image developmentLogo;

    public Sprite[] spritesForParams;
    public GameObject loadText;

    [Header("Character & Atributes Lists")]

    public GameObject[] allLocations;
    public GameObject[] atributes;
    public GameObject[] allCharacters;
    public string[] characterNames;
    public GameObject[] characters;
    GameObject[] storyCharacters = new GameObject[10];
    int storyCharacterId = 0;
    public GameObject[] exodusCharacters = new GameObject[10];
    int exodusCharacterId = 0;
    public GameObject currentCharacter;
    public GameObject[] paintings;
    int idOfRest = 0;
    private string[] savedRest = new string[100];
    int idOfUpgrade = 0;

    

    int idOfUpgradeObject = 0;
    private GameObject[] upgradedObjects = new GameObject[100];

    [Header("Some variables")]

    int currentDay = 0;
    int charactersToSpawn = 3;
    bool toSpawn = true;
    bool toHide = false;
    Color c;
    Color s;
    Image rend;
    Image logoRend;
    float moneyAmmount = 50;
    float sawDustAmmount = 50;
    float happinesAmmount = 50;
    float developmentAmmount = 50;

    int idOfItem = 0;

    int eventCounterChild;
    int eventIntChild;

    [Header("UI Elements")]

    public GameObject[] cum = new GameObject[100];

    public Button upgradeButtonTemplate;
    public GameObject itemTemplate;
    public Text dayCounter;

    public Button dayOverButton;

    public GameObject ui;
    public GameObject endScreen;
    public GameObject endScreenElements;
    public GameObject logo;
    public GameObject dayOverMenu;

    public GameObject someUI;
    public GameObject someButton;

    public GameObject insideAll;
    int idKok = 0;
    private string[] eventHolder = new string[999];
    private int[] eventHolderId = new int[999];

    public struct UpgradeInfo
    {
        public string name;
        public string englishName;
        public int price;
        public int id;
        public GameObject upgradeObject;
        public Button upgradeButton;
    }

    public struct Item
    {
        public GameObject gameObject;
        public Sprite itemSprite;
        public Image menu;
        public string name;
        public string englishName;
        public string description;
        public string englishDescription;
        public int cost;
        public char type;
    }

    public UpgradeInfo[] info = new UpgradeInfo[100];
    public Item[] itemList = new Item[10];
    void Awake()
    {
        loadText.SetActive(true);
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad (gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (PlayerPrefs.HasKey("LoadScene"))
        {
            
            dayOverMenu.SetActive(true);   
            Start();
            LoadAll(PlayerPrefs.GetString("LoadScene"));
            PlayerPrefs.DeleteKey("LoadScene");
        }
        loadText.SetActive(false);
    }
    public void Change(Exodus _exodus)
    {
        exodus = _exodus;
        Initialize();
    }


    private void Initialize()
    {
        moneyAmmount += exodus.moneyAmount;
        sawDustAmmount += exodus.foodAmount;
        happinesAmmount += exodus.happinesAmount;
        developmentAmmount += exodus.developmentAmount;
        ReloadUI();
        //PlayerPrefs.SetInt(exodus.nameOfExodus, exodus.exodusInt);
        toSpawn = exodus.spawnNext;
        toHide = exodus.HideCurrentCharacter;
        eventHolder[idKok] = exodus.nameOfExodus;
        eventHolderId[idKok] = exodus.exodusInt;
        idKok += 1;
        CheckEvents();
    }

    public void CleanEvent(string exod)
    {
        for (int i = 0; i < idKok; i++)
        {
            if (exod == eventHolder[i]) { 
                eventHolder[i] = null;
                eventHolderId[i] = 0;
            }
        }
    }

    public void CreateExodus(string exodus)
    {
        idKok++;
        eventHolder[idKok] = exodus;
        CheckEvents();
    }
    public void CleanEvents()
    {
        for (int i = 0; i < eventHolder.Length; i++)
        {
            eventHolder[i] = null;
            eventHolderId[i] = 0;
            idKok = 0;
        }
    }
    /// <summary>
    /// //////////////////////////////////// START
    /// </summary>
    public void Start()
    {
        ChoiceManager.optionList[0] = "noz";
        if(currentDay < 1)
        {
            NextDay();
        }
        if (PlayerPrefs.HasKey("Painting1"))
        {
            paintings[0].SetActive(true);
        }

        logoRend = logo.GetComponent<Image>();
        s = logoRend.color;
        s.a = 0f;
        logoRend.color = s;

        rend = endScreen.GetComponent<Image>();
        c = rend.color;
        c.a = 0f;
        rend.color = c;
    }
    /// <summary>
    /// ////////////////////////////////////// EXODUSES
    /// </summary>
    public void ReloadUI()
    {
        money.text = moneyAmmount.ToString();
        sawDust.fillAmount = sawDustAmmount / 100;
        happines.fillAmount = happinesAmmount / 100;
        development.fillAmount = developmentAmmount / 100;

        if (sawDustAmmount <= 30)
            sawDustLogo.sprite = spritesForParams[0];
        else if (sawDustAmmount >= 30 && sawDustAmmount <= 60)
            sawDustLogo.sprite = spritesForParams[1];
        else if (sawDustAmmount > 60)
            sawDustLogo.sprite = spritesForParams[2];

        if (happinesAmmount <= 30)
            happinesLogo.sprite = spritesForParams[3];
        else if (happinesAmmount >= 30 && sawDustAmmount <= 60)
            happinesLogo.sprite = spritesForParams[4];
        else if (happinesAmmount > 60)
            happinesLogo.sprite = spritesForParams[5];

        if (developmentAmmount <= 30)
            developmentLogo.sprite = spritesForParams[6];
        else if (developmentAmmount >= 30 && sawDustAmmount <= 60)
            developmentLogo.sprite = spritesForParams[7];
        else if (developmentAmmount > 60)
            developmentLogo.sprite = spritesForParams[8];
    }


    private void ParametrsRestore()
    {
        moneyAmmount = 1;
        sawDustAmmount = 1;
        happinesAmmount = 1;
        developmentAmmount = 1;
    }

    public void addRest(GameObject _object)
    {
        savedRest[idOfRest] = _object.name;
        idOfRest++;
    }
    private void CheckEvents()
    {
        
        
        for (int i = 0; i < eventHolder.Length; i++)
        {


            switch (eventHolder[i])
            {
                case "logo":
                    idKok++;
                    eventHolder[idKok] = "beflogo"; 
                    StartCoroutine(FadeLogo(() => { RandomCharacter(); }));
                    addRest(allLocations[1]);
                    allLocations[1].SetActive(true);
                    
                    break;
                case "beflogo":
                    allCharacters[4].SetActive(false);
                    currentCharacter.SetActive(false);
                    break;
                case "Orche":
                    allCharacters[0].SetActive(false);
                    switch (eventHolderId[i])
                    {
                        case 0:
                            break;
                        case 1:
                            allCharacters[1].SetActive(true);
                            atributes[0].SetActive(true);
                            break;
                        case 2:
                            allCharacters[1].SetActive(true);
                            atributes[1].SetActive(true);
                            break;
                    }
                    break;
                case "mainMenu":
                    StartCoroutine(FadeToMenu());
                    achievementManager.RequestStats();
                    PlayerPrefs.SetInt("Painting1", 1);
                    achievementManager.SetAchievement("NEW_ACHIEVEMENT_1_2");
                    break;
                case "tutorial1":
                    StartCoroutine(JustFade(() => { currentCharacter.SetActive(false) ;}));
                    allCharacters[3].SetActive(true);
                    allLocations[1].SetActive(true);
                    break;
                case "tutorial2":
                    StartCoroutine(JustFade(() => { allCharacters[3].SetActive(false); }));
                    currentCharacter.SetActive(true);
                    atributes[2].SetActive(false);
                    atributes[3].SetActive(true);
                    break;
                case "doll":
                    storyCharacters[storyCharacterId] = allCharacters[2];
                    storyCharacterId++;
                    break;
                case "shelter":
                    RelistUpgrades("Приют", "Orphanage", 15, allLocations[5], idOfUpgrade);
                    break;
                case "expedition":
                    switch (eventHolderId[i])
                    {
                        case 0:
                            eventCounterChild = currentDay + 1;
                            break;
                        case 1:
                            eventCounterChild = currentDay + 2;
                            break;
                        case 2:
                            eventCounterChild = currentDay + 3;
                            break;
                    }
                    
                    eventIntChild = eventHolderId[i];
                    break;
                case "ending":
                    managmentMap.enableLocation(atributes[7]);
                    toSpawn = false;
                    allCharacters[6].SetActive(true);
                    break;
                case "boloto":
                    allLocations[2].SetActive(true);
                    break;
            }

        }


        

        if (moneyAmmount < 0)
        {
            ParametrsRestore();
            StartCoroutine(Fade());
            CleanEvents();
            toSpawn = false;
        }
        if (sawDustAmmount < 0)
        {
            ParametrsRestore();
            CleanEvents();
            toSpawn = false;
        }
        if (happinesAmmount < 0)
        {
            ParametrsRestore();
            CleanEvents();
            toSpawn = false;
        }
        if (developmentAmmount < 0)
        {
            ParametrsRestore();
            CleanEvents();
            toSpawn = false;
        }


        CleanEvents();
        ReloadUI();
        
        if (toSpawn)
        {
            StartCoroutine(JustFade(() => { SpawnCharacter(); }));
            
        }
    }

    public void DownParams()
    {
        moneyAmmount -= 10;
        sawDustAmmount -= 10;
        happinesAmmount -= 10;
        developmentAmmount -= 10;
        toSpawn = false;
        ReloadUI();
        if(moneyAmmount < 0)
        {
            CheckEvents();
        }
    }

    public void UpParams()
    {
        moneyAmmount += 10;
        sawDustAmmount += 10;
        happinesAmmount += 10;
        developmentAmmount += 10;
        toSpawn = false;
        ReloadUI();
    }

    /// <summary>
    /// ///////////////////CHARACTER SPAWN
    /// </summary>

    private void StoryCharacter()
    {
        
        currentCharacter = storyCharacters[storyCharacterId];
        currentCharacter.SetActive(true);
    }

    private void ExodusCharacter()
    {
        
        currentCharacter = exodusCharacters[exodusCharacterId];
        currentCharacter.SetActive(true);
    }
    public void SpawnCharacter()
    {
        if (toHide)
            currentCharacter.SetActive(false);
        if (exodusCharacterId > 0)
        {
            exodusCharacterId--;
            ExodusCharacter();
        } else 
        if (storyCharacterId > 0)
        {
            storyCharacterId--;
            StoryCharacter();
        }
        else if (!(characters.Length <= 0) && charactersToSpawn > 1)
        {
            charactersToSpawn--;
            RandomCharacter();
        } else
        {
            dayOverButton.interactable = true;
        }
    }

    public void RandomCharacter()
    {
        int random = Random.Range(0, characters.Length);
        characters[random].SetActive(true);
        currentCharacter = characters[random];
        characters[random] = null;
        
        for (int i = random; i < characters.Length; i++)
        {
            if ((i + 1) >= characters.Length || characters[i + 1] == null)
                break;
            else
                characters[i] = characters[i + 1];
            characters[i + 1] = null;
        }
        GameObject[] tempCharacters = characters;
        characters = new GameObject[tempCharacters.Length - 1];
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] = tempCharacters[i];
        }

        for (int i = 0; i < characterNames.Length; i++)
        {
            characterNames[i] = null;
        }
        for (int i = 0; i < characters.Length; i++)
        {
            characterNames[i] = characters[i].name;
        }
    }

    /// <summary>
    /// ////////////////////ITEM SYSTEM
    /// </summary>
    
    public void menuOpen(int id)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if(itemList[i].menu !=null)
                itemList[i].menu.gameObject.SetActive(false);
        }
        itemList[id].menu.gameObject.SetActive(true);
    }

    public void addTheGet()
    {
        int ren = Random.Range(1, 10);
        adTheGet(ren.ToString(), "Bucket", "По всей видимости колодезное ведро, найденное на болоте, значительно погнуто по бокам. Внутри довольно склизкое, скорее всего находится в этих угодьях довольно долго. На дне едва различимо виднеется клеймо какого-то кузница", "The bucket you got from swamps", "Vedro", 10, 'F');
    }

    public void adTheGet(string name, string englishName, string description, string englishDescription, string imageName, int cost, char type)
    {
        addItem(idOfItem, name, englishName, description, englishDescription, imageName, cost, type);
        idOfItem++;
    }

    public void addItem(int id, string name, string englishName, string description, string englishDescription, string imageName, int cost, char type)
    {
        if (id < 5)
        {
            float buttonSpacing = 55f;
            var item = Instantiate(itemTemplate);
            itemList[id].gameObject = item;
            item.SetActive(true);

            item.transform.SetParent(itemTemplate.transform.parent);
            item.transform.localScale = Vector3.one;
            Button button = item.GetComponentInChildren<Button>();
            item.transform.localPosition = new Vector3(id * buttonSpacing, 0, 0);
            item.name = "Upgrade " + (id + 1);

            itemList[id].name = name;
            itemList[id].englishName = englishName;
            itemList[id].description = description;
            itemList[id].englishDescription = englishDescription;
            itemList[id].cost = cost;
            itemList[id].type = type;

            itemList[id].itemSprite = Resources.Load<Sprite>(imageName);
            button.GetComponent<Image>().sprite = itemList[id].itemSprite;

            button.onClick.AddListener(() =>
            {
                menuOpen(id);
            });
            var images = item.GetComponentsInChildren<Image>();
            itemList[id].menu = images[1];
            itemList[id].menu.gameObject.SetActive(false);

            var texts = itemList[id].menu.GetComponentsInChildren<Text>();
            Button sellButton = itemList[id].menu.GetComponentInChildren<Button>();
            //sellButton.interactable = false;
            sellButton.onClick.AddListener(() => {
                RelistItems(id);
                SellItem(itemList[id].cost, itemList[id].type);
            });
            if (PlayerPrefs.GetInt("_language_index") == 1)
                texts[0].text = itemList[id].name;
            else
                texts[0].text = itemList[id].englishName;
            if (PlayerPrefs.GetInt("_language_index") == 1)
                texts[1].text = itemList[id].description;
            else
                texts[1].text = itemList[id].englishDescription;

        } else
        {
            
        }
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i].gameObject != null && (i != id)) {
                if (itemList[id].name == itemList[i].name)
                {
                    Destroy(itemList[id].gameObject);
                    idOfItem--;
                }
            }
        }
    }

    public void SellItem(int cost, char type)
    {

        switch (type)
        {
            case 'M':
                moneyAmmount += cost;
                break;
            case 'F':
                sawDustAmmount += cost;
                break;
            case 'H':
                happinesAmmount += cost;
                break;
            case 'A':
                developmentAmmount += cost;
                break;
        }
        ReloadUI();
    }

    public void RelistItems(int id)
    {
        Destroy(itemList[id].gameObject);
        
        itemList[id].gameObject = null;

        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i].gameObject != null)
            {
                Destroy(itemList[i].gameObject);
            }
        }
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i].gameObject == null && (i + 1) < itemList.Length)
            {
                for (int j = i; j < itemList.Length; j++)
                {
                    if (itemList[j].gameObject != null)
                    {
                        itemList[i] = itemList[j];
                        itemList[j].gameObject = null;
                        break;
                    }
                }

            }
        }
        idOfItem = 0;
        for (int i = 0; i < itemList.Length && itemList[i].gameObject != null; i++)
        {
            adTheGet(itemList[i].name, itemList[i].englishName, itemList[i].description, itemList[i].englishDescription, itemList[i].itemSprite.name, itemList[i].cost, itemList[i].type);
        }
    }

    /// <summary>
    /// ////////////////////UPGRADE SYSTEM
    /// </summary>

    public void InsertUpgrade()
    {
        int cum = Random.Range(1, 10);
        RelistUpgrades(cum.ToString(), cum.ToString(), 10, allLocations[3], idOfUpgrade);
        //UpgradeGenerate("cum", 40, allLocations[4], idOfUpgrade);
    }

    public void RelistUpgrades(string nameOfUpgrade, string englishNameOfUpgrade, int costOfUpgrade, GameObject objectToBuy, int id)
    {
        info[id].id = id;
        info[id].name = nameOfUpgrade;
        info[id].englishName = englishNameOfUpgrade;
        info[id].price = costOfUpgrade;
        info[id].upgradeObject = objectToBuy;
        addRest(objectToBuy);
        idOfUpgrade++;

        Debug.Log(info[id].id);

        for (int i = 0; i < info.Length && info[i].upgradeButton != null; i++)
        {
            Debug.Log(info[i].upgradeButton.gameObject);
        }

        for (int i = 0; i < info.Length; i++)
        {
            if (info[i].upgradeButton != null)
            {
                Destroy(info[i].upgradeButton.gameObject);
            }
        }
        for (int i = 0; i < info.Length; i++)
        {
            if (info[i].upgradeObject == null && (i + 1) < info.Length)
            {
                for (int j = i; j < info.Length; j++)
                {
                    if(info[j].upgradeObject != null)
                    {
                        info[i] = info[j];
                        info[j].upgradeObject = null;
                        break;
                    }
                }
            }
        }
        for (int i = 0; i < info.Length && info[i].upgradeObject != null; i++)
        {
            UpgradeGenerate(info[i].name, info[i].englishName, info[i].price, info[i].upgradeObject, i);
        }
    }

    public void UpgradeGenerate(string nameOfUpgrade, string englishNameOfUpgrade, int costOfUpgrade, GameObject objectToBuy, int id)
    {
        
        int buttonSpacing = -50;

        

        Button button = Instantiate(upgradeButtonTemplate);

        
        

        button.transform.SetParent(upgradeButtonTemplate.transform.parent);
        button.transform.localScale = Vector3.one;
        button.transform.localPosition = new Vector3(0, id * buttonSpacing, 0);
        button.name = "Upgrade " + (id + 1);
        var textsOfButton = button.GetComponentsInChildren<Text>();

        if (PlayerPrefs.GetInt("_language_index") == 1)
            textsOfButton[0].text = nameOfUpgrade;
        else
            textsOfButton[0].text = englishNameOfUpgrade;

        textsOfButton[1].text = costOfUpgrade.ToString();
        button.gameObject.SetActive(true);
        button.onClick.AddListener(() =>
        {
            activateUpgrade(info[id].upgradeObject, info[id].price, info[id].upgradeButton, id);
        });
        info[id].upgradeButton = button;
        info[id].upgradeButton.gameObject.SetActive(true);
        
    }

    public void activateUpgrade(GameObject _object, int cost, Button button, int id)
    {
        if (moneyAmmount >= cost)
        {
            Destroy(info[id].upgradeButton.gameObject);
            info[id].upgradeObject = null;
            
            moneyAmmount -= cost;
            ReloadUI();
            Debug.Log(_object.name);
            _object.SetActive(true);
            
        } else
        {
            Debug.Log("Idi w piszdu syn shluhi");
        }
    }

    /// <summary>
    /// ///////////////////DAY SYSTEM
    /// </summary>

    public void DayOver()
    {
        dayOverButton.interactable = false;
        SaveAll(true);
    }
    public void UdpdateDay()
    {
        dayCounter.text = currentDay.ToString();
    }

    public void NextDay()
    {
        charactersToSpawn = 3;

        currentDay++;
        if (eventCounterChild == currentDay)
        {
            storyCharacters[storyCharacterId] = allCharacters[5];
            storyCharacterId++;
            switch (eventIntChild)
            {
                case 0:
                    atributes[4].SetActive(true);
                    adTheGet("Ведро", "Bucket", "Ведро, найденное на болоте", "The bucket you got from swamps", "Vedro", 10, 'M');
                    break;
                case 1:
                    atributes[5].SetActive(true);
                    break;
                case 2:
                    atributes[6].SetActive(true);
                    break;
            }
        }
        switch (currentDay)
        {
            default:
                StartCoroutine(JustFade(() => { SpawnCharacter(); }));
                break;
            case 1:
                currentCharacter = allCharacters[4];
                currentCharacter.SetActive(true);
                break;
        }
        
        
        dayCounter.text = currentDay.ToString();
    }

    /// <summary>
    /// ///////////////////FADING
    /// </summary>
    
    IEnumerator JustFade(System.Action action)
    {
        endScreen.SetActive(true);
        for (float f = 0.05f; f <= 1; f += 0.1f)
        {
            c = rend.color;
            c.a = f;
            rend.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        endScreen.SetActive(false);
        action();
        StartCoroutine(JustUnFade());
    }
    IEnumerator JustUnFade()
    {
        logo.SetActive(false);
        endScreen.SetActive(true);
        for (float f = 1; f >= 0; f -= 0.1f)
        {
            c = rend.color;
            c.a = f;
            rend.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        endScreen.SetActive(false);
    }

    IEnumerator Fade()
    {
        ui.SetActive(false);
        endScreen.SetActive(true);
        for(float f = 0.05f; f <= 1; f += 0.05f)
        {
            c = rend.color;
            c.a = f;
            rend.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        endScreenElements.SetActive(true);
    }

    IEnumerator FadeLogo(System.Action action)
    {
        s.a = 0f;
        logo.SetActive(true);
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            s = logoRend.color;
            s.a = f;
            logoRend.color = s;
            yield return new WaitForSeconds(0.2f);
        }
        StartCoroutine(JustFade(action));
    }
    IEnumerator FadeToMenu()
    {
        c.a = 0f;
        endScreen.SetActive(true);
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            c = rend.color;
            c.a = f;
            rend.color = c;
            yield return new WaitForSeconds(0.2f);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    IEnumerator Timer(float time, System.Action action)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        action();
    }

    /// <summary>
    /// ////////////////// SAVE & LOAD SECTION
    /// </summary>
   

    public void DelayBeforeLoad(string path)
    {
        PlayerPrefs.SetString("LoadScene", path);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void DeleteSave(string path)
    {
        File.Delete(path);
    }

    public void LoadAll(string path)
    {
        SaveData.current = (SaveData)Serialization.Load(path);
        moneyAmmount = SaveData.current.money;
        sawDustAmmount = SaveData.current.sawDust;
        happinesAmmount = SaveData.current.happines;
        developmentAmmount = SaveData.current.development;
        eventHolder = SaveData.current.eventHolderS;
        eventHolderId = SaveData.current.eventHolderIdS;
        idKok = SaveData.current.idKokS;
        characterNames = SaveData.current.characterNamesS;
        idOfRest = SaveData.current.idOfRestS;
        savedRest = SaveData.current.savedRestS;
        currentDay = SaveData.current.currentDayS;



        GameObject[] temporaryCharacter = new GameObject[100];

        var children = Resources.FindObjectsOfTypeAll<GameObject>();

        Debug.Log(SaveData.current.upgradeId);

        int m = 0;
        int n = 0;
        int l = 0;
        int o = 0;
            foreach (var child in children)
            {

                if (m < characterNames.Length)
                {
                    if (child.name == characterNames[m] && child.tag == "Character")
                    {
                        temporaryCharacter[m] = child.gameObject;
                        m++;
                    }
                }
                if(n < savedRest.Length)
                {
                    if (child.name == savedRest[n])
                    {
                        child.SetActive(true);
                        n++;
                    }
                }
                if (child.name == SaveData.current.savedCharactersS[l] && child.tag == "Character")
                {
                    child.SetActive(true);
                    l++;
                }
                if (child.name == SaveData.current.upgradeObject[o])
                {
                    RelistUpgrades(SaveData.current.upgradeName[o], SaveData.current.englishUpgradeName[o], SaveData.current.upgradePrice[o], child, idOfUpgrade);
                    o++;
                }
            }
        for (int i = 0; i < SaveData.current.numberOfItems; i++)
        {
            itemList[i].name = SaveData.current.itemName[i];
            itemList[i].englishName = SaveData.current.englishItemName[i];
            itemList[i].description = SaveData.current.itemDescription[i];
            itemList[i].englishDescription = SaveData.current.englishItemDescription[i];
            string nameOfSprite = SaveData.current.spriteName[i];
            itemList[i].cost = SaveData.current.itemCost[i];
            itemList[i].type = SaveData.current.itemSellType[i];
            adTheGet(itemList[i].name, itemList[i].englishName, itemList[i].description, itemList[i].englishDescription, nameOfSprite, itemList[i].cost, itemList[i].type);
        }

        int count = 0;
        for (int i = 0; i < temporaryCharacter.Length && temporaryCharacter[i] != null; i++)
        {
            count++;
        }
        characters = new GameObject[count];
        while (count != 0)
        {
            count--;
            characters[count] = temporaryCharacter[count];
        }

        toSpawn = false;
        CheckEvents();
        ReloadUI();
        loadText.SetActive(false);
        allCharacters[4].SetActive(false);
        dayCounter.text = currentDay.ToString();
    }

    public GameObject prefab;
    public string[] saveFiles;
    public GameObject loadArea;
    public void loadSave()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/WolfAdventuresSaves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/WolfAdventuresSaves");
        }

        saveFiles = Directory.GetFiles(Application.persistentDataPath + "/WolfAdventuresSaves");

        for (int i = 0; i < saveFiles.Length; i++)
        {
            GameObject cos = Instantiate(prefab);
            cos.transform.SetParent(loadArea.transform, false);
            cos.transform.localScale = Vector3.one;
            cos.transform.localPosition = new Vector3(0, i * (-30), 0);

            var index = i;
            cos.GetComponent<Button>().onClick.AddListener(() =>
            {
                DelayBeforeLoad(saveFiles[index]);
            });
            cos.GetComponentInChildren<Text>().text = saveFiles[index].Replace(Application.persistentDataPath + "/WolfAdventuresSaves", "");
        }
        Destroy(prefab);
    }
    public void SaveAll(bool auto)
    {
        var objCharacters = GameObject.FindGameObjectsWithTag("Character");
        for (int i = 0; i < objCharacters.Length; i++)
        {
            SaveData.current.savedCharactersS[i] = objCharacters[i].name;
        }
        
        SaveData.current.money = moneyAmmount;
        SaveData.current.sawDust = sawDustAmmount;
        SaveData.current.happines = happinesAmmount;
        SaveData.current.development = developmentAmmount;
        SaveData.current.eventHolderS = eventHolder;
        SaveData.current.eventHolderIdS = eventHolderId;
        SaveData.current.characterNamesS = characterNames;
        SaveData.current.idKokS = idKok;
        SaveData.current.savedRestS = savedRest;
        SaveData.current.idOfRestS = idOfRest;
        SaveData.current.currentDayS = currentDay;

        SaveData.current.numberOfItems = idOfItem;

        Debug.Log(idOfItem);
        for (int i = 0; i < idOfItem; i++)
        {
            Debug.Log(info[i].name);
 
                SaveData.current.itemName[i] = itemList[i].name;
                SaveData.current.englishItemName[i] = itemList[i].englishName;
                SaveData.current.itemDescription[i] = itemList[i].description;
                SaveData.current.englishItemDescription[i] = itemList[i].englishDescription;
                SaveData.current.spriteName[i] = itemList[i].itemSprite.name;
                SaveData.current.itemCost[i] = itemList[i].cost;
                SaveData.current.itemSellType[i] = itemList[i].type;
            
        }
        SaveData.current.upgradeId = idOfUpgrade;

        Debug.Log(idOfUpgrade);
        for (int i = 0; i < idOfUpgrade; i++)
        {

            Debug.Log(info[i].name);
            if (info[i].name != null)
            {
                SaveData.current.upgradeName[i] = info[i].name;
                SaveData.current.englishUpgradeName[i] = info[i].englishName;
                SaveData.current.upgradePrice[i] = info[i].price;
                SaveData.current.upgradeObject[i] = info[i].upgradeObject.name;
            }
        }
        
        if (auto)
        {
            if (PlayerPrefs.GetInt("_language_index") == 1)
                SaveSerializable("Автосейв");
            else
                SaveSerializable("AutoSave");
        }
        else
            SaveSerializable(field.text);
    }

    public void SaveSerializable(string name) {
        Serialization.Save(name, SaveData.current);
    }

}
