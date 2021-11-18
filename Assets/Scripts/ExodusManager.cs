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


    public EndScript endScript;
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

    public AudioClip endingClip;
    public AudioSource mainSource;

    public Sprite[] spritesForParams;

    public GameObject endScreen;

    public GameObject collidersOfCamera;

    public GameObject cameraObj;

    [Header("Character & Atributes Lists")]

    public GameObject[] allLocations;
    public GameObject[] atributes;
    public GameObject[] allCharacters;
    public string[] characterNames;
    public GameObject[] characters;
    public GameObject[] storyCharacters = new GameObject[10];
    int storyCharacterId = 0;
    public GameObject[] exodusCharacters = new GameObject[10];
    int exodusCharacterId = 0;
    public GameObject currentCharacter;
    public GameObject[] paintings;
    int idOfRest = 0;
    private string[] savedRest = new string[100];
    int idOfUpgrade = 0;
    public GameObject[] endAtributes;

    

    int idOfUpgradeObject = 0;
    private GameObject[] upgradedObjects = new GameObject[100];

    [Header("Some variables")]

    public static int peopleAgainst = 0;
    public static int relationWithMex = 0;
    public static int moneyToMex = 0;

    int currentDay = 0;

    int charactersToSpawn = 3;
    bool toSpawn = true;
    bool toHide = false;
    Color c;
    Color s;
    Image rend;
    Image logoRend;
    float moneyAmmount = 49;
    float sawDustAmmount = 50;
    float happinesAmmount = 50;
    float developmentAmmount = 50;

    int idOfItem = 0;

    public string[] eventCounters = new string[100];
    public int[] whenToAppear = new int[100];
    public int[] eventInts = new int[100];
    int idOfEventCounter = 0;


    bool zackEvent = false;

    [Header("UI Elements")]

    public GameObject[] cum = new GameObject[100];

    public Button upgradeButtonTemplate;
    public GameObject itemTemplate;
    public Text dayCounter;

    public Button dayOverButton;

    public GameObject ui;
    public GameObject finishGame;
    public GameObject endScreenElements;
    public GameObject logo;
    public GameObject dayOverMenu;

    public GameObject someUI;
    public GameObject someButton;

    public GameObject buttonsPop;

    public GameObject insideAll;
    int idKok = 0;
    private string[] eventHolder = new string[999];
    private byte[] eventHolderId = new byte[999];

    public GameObject sticks;
    public GameObject endstick;
    public GameObject warning;
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
            sticks.SetActive(false);
            buttonsPop.SetActive(false);
            dayOverMenu.SetActive(true);   
            Start();
            LoadAll(PlayerPrefs.GetString("LoadScene"));
            PlayerPrefs.DeleteKey("LoadScene");
        }
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
        dayOverButton.interactable = false;
        
        
        if (currentDay < 1)
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
    public void removeRest(GameObject _object)
    {
        for (int i = 0; i < idOfRest; i++)
        {
           if( savedRest[i] == _object.name)
            {
                savedRest[i] = null;
            }
        }
    }


    private void CheckEvents()
    {


        for (int i = 0; i < eventHolder.Length; i++)
        {


            switch (eventHolder[i])
            {
                case "worse":
                    relationWithMex++;
                    break;
                case "badly":
                    peopleAgainst++;
                    break;
                case "logo":

                    
                    storyCharacters[storyCharacterId] = allCharacters[11];
                    storyCharacterId = 1;
                    idKok++;
                    eventHolder[idKok] = "beflogo"; 
                    StartCoroutine(FadeLogo(() => { StoryCharacter(); }));
                    addRest(allLocations[1]);
                    allLocations[1].SetActive(true);
                    addRest(allLocations[4]);
                    allLocations[4].SetActive(true);

                    addRest(allLocations[3]);
                    allLocations[3].SetActive(true);

                    addRest(allLocations[5]);
                    allLocations[5].SetActive(true);

                    allCharacters[3].SetActive(false);

                    break;
                case "beflogo":
                    allCharacters[4].SetActive(false);
                    currentCharacter.SetActive(false);
                    break;
                case "Citizen1":
                    allCharacters[14].SetActive(true);
                    addRest(allCharacters[14]);
                    switch (eventHolderId[i])
                    {
                        case 0:
                            atributes[20].SetActive(true);
                            addRest(atributes[20]);
                            break;
                        case 1:
                            atributes[21].SetActive(true);
                            addRest(atributes[21]);
                            peopleAgainst++;
                            break;
                    }
                    break;
                case "nozik":
                    ChoiceManager.optionList[ChoiceManager.optionId] = "noz";
                    ChoiceManager.optionNum[ChoiceManager.optionId] = 0;
                    ChoiceManager.optionId++;
                    break;
                case "ScaryPark":
                    allLocations[12].SetActive(true);
                    addRest(allLocations[12]);
                    storyCharacters[storyCharacterId] = allCharacters[22];
                    storyCharacterId++;
                    break;
                case "Citizen2":
                    allCharacters[15].SetActive(true);
                    addRest(allCharacters[15]);
                    switch (eventHolderId[i])
                    {
                        case 0:
                            atributes[22].SetActive(true);
                            warning.SetActive(true);
                            addRest(atributes[22]);
                            break;
                        case 1:
                            atributes[23].SetActive(true);
                            warning.SetActive(true);
                            addRest(atributes[23]);
                            peopleAgainst++;
                            break;
                    }
                    break;
                case "Citizen3":
                    adTheGet("Рецепт", "Recipe", "Для замечательной кукурузы", "For wonderful corn", "recipe", 5, 'F');
                    break;
                case "Orche":
                    allCharacters[0].SetActive(false);
                    switch (eventHolderId[i])
                    {
                        case 0:
                            peopleAgainst += 2;
                            break;

                        case 1:
                            allCharacters[1].SetActive(true);
                            addRest(allCharacters[1]);
                            atributes[0].SetActive(true);
                            addRest(atributes[0]);
                            moneyToMex++;
                            warning.SetActive(true);
                            break;
                        case 2:
                            allCharacters[1].SetActive(true);
                            addRest(allCharacters[1]);
                            atributes[1].SetActive(true);
                            addRest(atributes[1]);
                            peopleAgainst++;
                            warning.SetActive(true);
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
                    warning.SetActive(true);
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
                    RelistUpgrades("Приют", "Orphanage", 5, allLocations[8], idOfUpgrade);
                    break;
                case "helpEvent":
                    ChoiceManager.optionList[ChoiceManager.optionId] = "help";
                    ChoiceManager.optionNum[ChoiceManager.optionId] = 1;
                    ChoiceManager.optionId++;
                    break;
                
                case "expedition":
                    eventCounters[idOfEventCounter] = "child";
                    switch (eventHolderId[i])
                    {
                        case 0:
                            whenToAppear[idOfEventCounter] = currentDay + 2;
                            break;
                        case 1:
                            whenToAppear[idOfEventCounter] = currentDay + 2;
                            break;
                        case 2:
                            whenToAppear[idOfEventCounter] = currentDay + 3;
                            break;
                    }
                    
                    eventInts[idOfEventCounter] = eventHolderId[i];
                    idOfEventCounter++;
                    break;
                case "GoldStory":
                    whenToAppear[idOfEventCounter] = currentDay + 2;
                    eventCounters[idOfEventCounter] = "zack";
                    idOfEventCounter++;
                    switch (eventHolderId[i])
                    {
                        case 0:
                            adTheGet("Свистулька", "The Whistle", "Свистулька Зака — обычный по конструкции свисток, сделанный из неизвестного дерева, по ощущениям отдающим каким-то неестественным холодом, " +
                        "как будто от металла. На свистульке присутствуют трещины и сколы, из-за чего использование его по назначению невозможно, скорее всего, для владельца эта вещица имела некое другое значение. " +
                        "С боку небрежно вырезано имя Зак. ИЗбавиться от неё можно только за 30 монет", "Reduces happiness everyday while in inventory", "Svistulka", -30, 'M');
                            ChoiceManager.optionList[ChoiceManager.optionId] = "bag";
                            ChoiceManager.optionNum[ChoiceManager.optionId] = 0;
                            ChoiceManager.optionId++;
                            break;
                        case 1:
                            atributes[8].SetActive(true);
                           
                            addRest(atributes[8]);
                            ChoiceManager.optionList[ChoiceManager.optionId] = "bag";
                            ChoiceManager.optionNum[ChoiceManager.optionId] = 1;
                            ChoiceManager.optionId++;
                            break;
                    }
                    break;
                case "removeSvistulka":
                    RemoveItem("Свистулька");
                    break;
                case "giveBagBack":
                    atributes[8].SetActive(false);
                    removeRest(atributes[8]);
                    eventCounters[idOfEventCounter] = "zackBless";
                    whenToAppear[idOfEventCounter] = 0;
                    idOfEventCounter++;
                    break;
                case "doNotGiveMoney":
                    atributes[8].SetActive(false);
                    adTheGet("Свистулька", "The Whistle", "Свистулька Зака — обычный по конструкции свисток, сделанный из неизвестного дерева, по ощущениям отдающим каким-то неестественным холодом, " +
                        "как будто от металла. На свистульке присутствуют трещины и сколы, из-за чего использование его по назначению невозможно, скорее всего, для владельца эта вещица имела некое другое значение. " +
                        "С боку небрежно вырезано имя Зак. ИЗбавиться от неё можно только за 30 монет", "Reduces happiness everyday while in inventory", "Svistulka", -30, 'M');
                    break;


                case "endGood":
                    collidersOfCamera.SetActive(false);
                    managmentMap.justEnableLocation(endAtributes[2]);
                    CameraScript.moveCamera(endAtributes[0].transform.position, cameraObj);
                    endAtributes[3].SetActive(true);
                    endAtributes[11].SetActive(false);
                    endAtributes[0].SetActive(false);
                    break;

                case "ending":
                    sticks.SetActive(false);
                    endstick.SetActive(true);
                    ui.SetActive(false);
                    managmentMap.enableLocation(atributes[16]);
                    CameraScript.moveCamera(endAtributes[14].transform.position, cameraObj);
                    atributes[14].SetActive(false);
                    atributes[15].SetActive(true);
                    atributes[17].GetComponent<BoxCollider2D>().enabled = false;
                    atributes[17].GetComponent<CircleCollider2D>().enabled = false;
                    toSpawn = false;
                    allCharacters[6].SetActive(true);
                    mainSource.clip = endingClip;
                    mainSource.Play();
                    
                    ui.SetActive(false);
                    dayOverButton.gameObject.SetActive(false);
                    break;
                case "showResults":
                    finishGame.SetActive(true);
                    endScript.appearScript();
                    break;
                case "boloto":
                    allLocations[2].SetActive(true);
                    warning.SetActive(true);
                    break;
                case "TrainLanky":
                    achievementManager.RequestStats();
                    achievementManager.SetAchievement("NEW_ACHIEVEMENT_1_3");
                    break;
                case "refuseBebej":
                    StartCoroutine(JustFade(() => { allCharacters[8].SetActive(true); allCharacters[9].transform.eulerAngles += new Vector3(0, 180, 0); }));
                    
                    break;

                case "BebejLeaves":
                    allCharacters[8].SetActive(false);
                    allCharacters[9].transform.eulerAngles -= new Vector3(0, 180, 0);
                    break;
                case "GiveMoney":
                    whenToAppear[idOfEventCounter] = currentDay + 1;
                    eventCounters[idOfEventCounter] = "casino";
                    eventInts[idOfEventCounter] = eventHolderId[i];
                    switch (eventHolderId[i])
                    {
                        case 0:
                            moneyAmmount -= 2;
                            break;
                        case 1:
                            moneyAmmount -= 5;
                            break;
                        case 2:
                            moneyAmmount -= 10;
                            break;
                    }
                    idOfEventCounter++;
                    break;
                case "OrganiseRebell":
                   
                    whenToAppear[idOfEventCounter] = currentDay + 1;
                    eventCounters[idOfEventCounter] = "OrganiseRebell";
                    eventInts[idOfEventCounter] = eventHolderId[i];
                    
                    idOfEventCounter++;
                    break;
                case "orchAppeal":
                    endAtributes[11].SetActive(true);
                    endAtributes[0].SetActive(true);
                    var orches = endAtributes[0].GetComponentsInChildren<Transform>();
                    for (int j = 1; j < orches.Length; j++)
                    {
                        orches[j].eulerAngles += new Vector3(0, 180, 0);
                    }
                    managmentMap.justEnableLocation(endAtributes[2]);
                    collidersOfCamera.SetActive(false);
                    CameraScript.moveCamera(endAtributes[0].transform.position, cameraObj);
                    break;
                case "OrcheWolf":
                    var orches1 = endAtributes[0].GetComponentsInChildren<Transform>();
                    for (int j = 1; j < orches1.Length; j++)
                    {
                        orches1[j].eulerAngles -= new Vector3(0, 180, 0);
                    }
                    break;
                case "OrcheRight":
                    var orches2= endAtributes[0].GetComponentsInChildren<Transform>();
                    for (int j = 1; j < orches2.Length; j++)
                    {
                        orches2[j].eulerAngles += new Vector3(0, 180, 0);
                    }
                    break;
                case "CatchMex":
                    Debug.Log("lol");
                   StartCoroutine(JustFade(() => { endAtributes[3].SetActive(true); endAtributes[0].SetActive(false); endAtributes[1].SetActive(false); }));
                    break;
                case "mexishere":
                    
                   StartCoroutine(JustFade(() => { endAtributes[24].SetActive(true); endAtributes[3].SetActive(false); endAtributes[0].SetActive(false); endAtributes[1].SetActive(false); }));
                    break;
                case "SlayTheMex":
                    mainSource.Stop();
                    endAtributes[25].SetActive(true);
                   StartCoroutine(Timer(3, () => { StartCoroutine(JustFade(() => { mainSource.Play(); endAtributes[23].SetActive(true); CameraScript.moveCamera(endAtributes[7].transform.position, cameraObj); managmentMap.enableLocation(endAtributes[7]);  }));  }));
                    break;

                case "BadEnd":
                    
                    endAtributes[5].SetActive(true);
                    collidersOfCamera.SetActive(false);
                    CameraScript.moveCamera(endAtributes[4].transform.position, cameraObj);
                    managmentMap.justEnableLocation(endAtributes[4]);

                    //StartCoroutine(JustFade(() => { CameraScript.moveCamera(endAtributes[4].transform.position, cameraObj); }));
                    break;
                case "SaveEnd":
                    atributes[17].SetActive(false);
                    atributes[18].SetActive(true);
                    endAtributes[15].SetActive(false);
                    StartCoroutine(Timer(5, () => {
                        atributes[17].SetActive(true);
                        atributes[18].SetActive(false); CameraScript.moveCamera(endAtributes[6].transform.position, cameraObj); managmentMap.enableLocation(endAtributes[6]); }));
                    break;
                case "EscapeEnd":
                    achievementManager.SetAchievement("NEW_ACHIEVEMENT_1_12");
                    atributes[17].SetActive(false);
                    StartCoroutine(JustFade(() => { CameraScript.moveCamera(endAtributes[18].transform.position, cameraObj); managmentMap.enableLocation(endAtributes[18]); }));
                    break;
                case "Convience":
                    
                    StartCoroutine(JustFade(() => {
                        endAtributes[5].SetActive(true);
                        endAtributes[21].SetActive(false);
                        endAtributes[10].SetActive(false);
                        endAtributes[19].SetActive(true);
                    }));
                    break;
                case "StayOnKnees":

                    atributes[17].SetActive(false);
                    endAtributes[12].SetActive(true);
                    break;
                case "SlayMex":
                    achievementManager.SetAchievement("NEW_ACHIEVEMENT_1_11");
                    mainSource.Stop();
                    atributes[17].SetActive(false);
                    endAtributes[15].SetActive(true);
                    StartCoroutine(Timer(2, () => { mainSource.Play(); endAtributes[16].SetActive(true); })) ;
                    break;
                case "StayOnKnees2":
                    achievementManager.SetAchievement("NEW_ACHIEVEMENT_1_14");
                    StartCoroutine(JustFade(() => { CameraScript.moveCamera(endAtributes[13].transform.position, cameraObj); managmentMap.enableLocation(endAtributes[13]); }));
                    
                    break;
                case "SlayMex2":
                    endAtributes[15].SetActive(false);
                    achievementManager.SetAchievement("NEW_ACHIEVEMENT_1_11");
                    atributes[17].SetActive(false);
                    atributes[19].SetActive(true);
                    mainSource.Stop();
                    StartCoroutine(Timer(5, () => {
                        StartCoroutine(JustFade(() => { mainSource.Play(); endAtributes[17].SetActive(true); endAtributes[9].SetActive(false); CameraScript.moveCamera(endAtributes[7].transform.position, cameraObj); managmentMap.enableLocation(endAtributes[7]); }));
                    }));
                    break;
                case "SlayWolf":
                    achievementManager.SetAchievement("NEW_ACHIEVEMENT_1_10");
                    atributes[17].SetActive(false);
                    atributes[19].SetActive(true);
                    mainSource.Stop();
                    StartCoroutine(Timer(5, () =>{ StartCoroutine(JustFade( () => { mainSource.Play(); endAtributes[8].SetActive(true); endAtributes[9].SetActive(false); CameraScript.moveCamera(endAtributes[7].transform.position, cameraObj); managmentMap.enableLocation(endAtributes[7]); }));
                    }));
                    break;
                case "SlayWolfMex":
                    achievementManager.SetAchievement("NEW_ACHIEVEMENT_1_8");
                    atributes[17].SetActive(false);
                    atributes[19].SetActive(true);
                    mainSource.Stop();
                    StartCoroutine(Timer(5, () => {
                        StartCoroutine(JustFade(() => { mainSource.Play(); endAtributes[20].SetActive(true); CameraScript.moveCamera(endAtributes[7].transform.position, cameraObj); managmentMap.enableLocation(endAtributes[7]); }));
                    }));
                    break;
                case "WolfAndMex":
                    achievementManager.SetAchievement("NEW_ACHIEVEMENT_1_9");
                    StartCoroutine(JustFade(() => { mainSource.Play(); endAtributes[22].SetActive(true); CameraScript.moveCamera(endAtributes[7].transform.position, cameraObj); managmentMap.enableLocation(endAtributes[7]); }));
                   
                    break;
                case "MexEscape":
                    achievementManager.SetAchievement("NEW_ACHIEVEMENT_1_1");
                    StartCoroutine(JustFade(() => { mainSource.Play(); endAtributes[23].SetActive(true); CameraScript.moveCamera(endAtributes[7].transform.position, cameraObj); managmentMap.enableLocation(endAtributes[7]); }));

                    break;
                case "TalkToMex":
                    StartCoroutine(JustFade(() => { endAtributes[5].SetActive(false); endAtributes[10].SetActive(true); }));
               
                    break;
                case "goToMenu":
                    PlayerPrefs.SetInt("devMenu", 1);
                    StartCoroutine(FadeToMenu());
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
            StartCoroutine(Fade());
            CleanEvents();
            toSpawn = false;
        }
        if (happinesAmmount < 0)
        {
            ParametrsRestore();
            StartCoroutine(Fade());
            CleanEvents();
            toSpawn = false;
        }
        if (developmentAmmount < 0)
        {
            ParametrsRestore();
            StartCoroutine(Fade());
            CleanEvents();
            toSpawn = false;
        }


        CleanEvents();
        ReloadUI();

        if (toHide)
            StartCoroutine(Timer(0.8f, () => { currentCharacter.SetActive(false); }));
        if (toSpawn)
        {
            StartCoroutine(JustFade(() => { SpawnCharacter(); }));
            
        }
    }


    public void addAb(string key)
    {
        ChoiceManager.optionList[ChoiceManager.optionId] = key;
        ChoiceManager.optionNum[ChoiceManager.optionId] = 1;
        ChoiceManager.optionId++;
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
        Debug.Log(storyCharacterId);
        storyCharacterId--;

        currentCharacter = storyCharacters[storyCharacterId];
        storyCharacters[storyCharacterId] = null;
        
        currentCharacter.SetActive(true);
        
    }

    private void ExodusCharacter()
    {
        
        currentCharacter = exodusCharacters[exodusCharacterId];
        currentCharacter.SetActive(true);
    }
    public void SpawnCharacter()
    {
        
        if (exodusCharacterId > 0)
        {
            exodusCharacterId--;
            ExodusCharacter();
        } else 
        if (storyCharacterId > 0)
        {
            
            StoryCharacter();
        }
        else if (!(characters.Length <= 0) && charactersToSpawn > 1)
        {
            charactersToSpawn--;
            RandomCharacter();
        } else
        {
            dayOverButton.interactable = true;
            dayOverButton.GetComponent<Animator>().SetBool("IsDayOver", true);
        }
    }

    public void RandomCharacter()
    {
        /*var randomCharacters = Resources.FindObjectsOfTypeAll<GameObject>();
        
        for (int i = 0; i < randomCharacters.Length; i++)
        {
            if (ra == characterNames[m] && child.tag == "Character")
            {
                temporaryCharacter[m] = child.gameObject;
                Debug.Log(temporaryCharacter[m]);
                m++;
            }
        }
        */
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

    public void RemoveItem(string name)
    {
        int id = 0;
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i].itemSprite != null)
                if (itemList[i].name == name)
                    id = i;
        }
        Destroy(itemList[id].gameObject);

        ShuffleItems(id);
    }

    public void ShuffleItems(int id)
    {
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

    public void RelistItems(int id)
    {
        Destroy(itemList[id].gameObject);

        ShuffleItems(id);
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
            warning.SetActive(true);
        });
        info[id].upgradeButton = button;
        info[id].upgradeButton.gameObject.SetActive(true);
        
    }

    public void activateUpgrade(GameObject _object, int cost, Button button, int id)
    {
       
        if (moneyAmmount >= cost)
        {
            Destroy(info[id].upgradeButton.gameObject);
            addRest(_object);
            info[id].upgradeObject = null;
            for (int i = 0; i < SaveData.current.upgradeName.Length; i++)
            {
                if (info[id].name == SaveData.current.upgradeName[i])
                {
                    SaveData.current.upgradeName[i] = null;
                    SaveData.current.upgradeObject[i] = null;
                    SaveData.current.upgradePrice[i] = 0;
                    SaveData.current.englishUpgradeName[i] = null;
                    SaveData.current.upgradeId--;
                }
            }
            idOfUpgrade-- ;
            
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
        dayOverButton.GetComponent<Animator>().SetBool("IsDayOver", false);
        SaveAll(true);
    }
    public void UdpdateDay()
    {
        dayCounter.text = currentDay.ToString();
    }

    public void clearCounter(int id)
    {
        eventCounters[id] = null;
        whenToAppear[id] = 0;
        eventInts[id] = 0;
    }

    public void NextDay()
    {
        moneyAmmount++;
        ReloadUI();
        charactersToSpawn = 3;
        buttonsPop.SetActive(true);
        currentDay++;
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i].itemSprite != null)
            {
                switch (itemList[i].itemSprite.name)
                {
                    case "Svistulka":
                        happinesAmmount -= 2;
                        ReloadUI();
                        break;
                }
            }
        }

        for (int i = 0; i < idOfEventCounter; i++)
        {
            Debug.Log(eventCounters[i]);
            if (eventCounters[i] == "zackBless")
            {
                happinesAmmount += 1;
                ReloadUI();
            }

            if (whenToAppear[i] == currentDay)
            {
                switch (eventCounters[i])
                {
                case "child":
                    
                        storyCharacters[storyCharacterId] = allCharacters[5];
                        storyCharacterId++;
                        switch (eventInts[i])
                        {
                            case 0:
                                atributes[4].SetActive(true);
                                adTheGet("Реликвия", "Relic", "Предмет, данный ребенком", "Item given by child", "relic", 10, 'M');
                                achievementManager.SetAchievement("NEW_ACHIEVEMENT_1_6");
                                allCharacters[10].SetActive(false);
                                break;
                            case 1:
                                atributes[5].SetActive(true);
                                atributes[11].SetActive(false);
                                atributes[12].SetActive(true);
                                break;
                            case 2:
                                atributes[6].SetActive(true);
                                atributes[11].SetActive(false);
                                atributes[12].SetActive(true);
                                break;
                                
                        }
                        clearCounter(i);
                    
                    break;
                case "zack":
                  
                        storyCharacters[storyCharacterId] = allCharacters[7];
                        storyCharacterId++;
                    
                    break;
                case "OrganiseRebell":
                        storyCharacters[storyCharacterId] = allCharacters[21];
                        storyCharacterId++;
                        break;
                case "casino":
                        storyCharacters[storyCharacterId] = allCharacters[9];
                        storyCharacterId++;
                        int fortune = Random.RandomRange(1, 10);
                        
                        if (fortune > 7)
                        {
                            achievementManager.RequestStats();
                            achievementManager.SetAchievement("NEW_ACHIEVEMENT_1_4");
                            switch (eventInts[i])
                            {
                                case 0:
                                    moneyAmmount += 6;
                                    break;
                                case 1:
                                    moneyAmmount += 15;
                                    break;
                                case 2:
                                    moneyAmmount += 30;
                                    break;
                            }
                            atributes[10].SetActive(true);
                        }
                        else
                        {
                            achievementManager.SetAchievement("NEW_ACHIEVEMENT_1_7");
                            atributes[9].SetActive(true);
                        }
                        break;
                
                }
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
            case 14:
                Debug.Log(storyCharacterId);
                storyCharacters[storyCharacterId] = allCharacters[16];
                storyCharacterId++;
                Debug.Log(storyCharacterId);
                StartCoroutine(JustFade(() => { SpawnCharacter(); }));

                break;
            case 4:

                storyCharacters[storyCharacterId] = allCharacters[18];
                storyCharacterId++;
                StartCoroutine(JustFade(() => { SpawnCharacter(); }));
                break;
            case 3:

                
                RelistUpgrades("Башня", "Tower", 10, allLocations[10], idOfUpgrade);
                StartCoroutine(JustFade(() => { SpawnCharacter(); }));
                break;

            case 8:
                storyCharacters[storyCharacterId] = allCharacters[17];
                storyCharacterId++;
                StartCoroutine(JustFade(() => { SpawnCharacter(); }));
                break;
            case 9:
                storyCharacters[storyCharacterId] = allCharacters[20];
                storyCharacterId++;
                StartCoroutine(JustFade(() => { SpawnCharacter(); }));
                break;
            case 11:
                storyCharacters[storyCharacterId] = allCharacters[19];
                storyCharacterId++;
                StartCoroutine(JustFade(() => { SpawnCharacter(); }));
                break;
            case 5:

                //storyCharacters[storyCharacterId] = allCharacters[17];
                //storyCharacterId++;
                RelistUpgrades("Пещера", "Cave", 4, allLocations[11], idOfUpgrade);
                StartCoroutine(JustFade(() => { SpawnCharacter(); }));
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
        storyCharacterId = SaveData.current.stotyCharacterIdS;
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
        idOfUpgrade = SaveData.current.idOfUpgradeS;

        peopleAgainst = SaveData.current.peopleAgainstS;
        relationWithMex = SaveData.current.relationWithMexS;
        moneyToMex = SaveData.current.moneyToMexS;

        ChoiceManager.optionList = SaveData.current.optionListS;
        ChoiceManager.optionNum = SaveData.current.optionNumS;
        ChoiceManager.optionId = SaveData.current.optionIdS;
        

        idOfEventCounter = SaveData.current.idOfEventCounterS;
        eventCounters = SaveData.current.eventCountersS;
        whenToAppear = SaveData.current.whenToAppearS;
        eventInts = SaveData.current.eventIntsS;

        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] = null;
        }

        GameObject[] temporaryCharacter = new GameObject[100];

        var children = Resources.FindObjectsOfTypeAll<GameObject>();
        

        int m = 0;
        int n = 0;
        int l = 0;
        int o = 0;
            foreach (var child in children)
            {

            for (int i = 0; i < savedRest.Length; i++)
            {
                if (child.name == savedRest[i] && child.tag == "Item")
                {
                    child.SetActive(true);
                    
                }
            }
            Debug.Log("id:" + idOfUpgrade);
            for (int i = 0; i < idOfUpgrade; i++)
            {
                Debug.Log(upgradedObjects[i]);

                if (child.name == SaveData.current.upgradeObject[i] && child.tag == "Item")
                {
                    RelistUpgrades(SaveData.current.upgradeName[i], SaveData.current.englishUpgradeName[i], SaveData.current.upgradePrice[i], child, i);
                    
                }
            }
            
            }
        foreach (var child in children)
        {
            for (int i = 0; i < characterNames.Length && characterNames[i] != null; i++)
            {
                if (child.name == characterNames[i] && child.tag == "Character")
                {
                    characters[i] = child.gameObject;
                }
            }
        }
        int randAm = 0;
        
        for (int i = 0; i < characters.Length; i++)
        {
            if(characters[i] != null)
                randAm++;
        }

        GameObject[] randCharacters = new GameObject[randAm];

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] != null)
                randCharacters[i] = characters[i];
        }

        characters = randCharacters;

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



        toSpawn = false;
        CheckEvents();
        ReloadUI();
        allCharacters[4].SetActive(false);
        dayCounter.text = currentDay.ToString();
        buttonsPop.SetActive(false);
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
        SaveData.current.stotyCharacterIdS = storyCharacterId;
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
        SaveData.current.idOfUpgradeS = idOfUpgrade;

        SaveData.current.moneyToMexS = moneyToMex;
        SaveData.current.relationWithMexS = relationWithMex;
        SaveData.current.peopleAgainstS = peopleAgainst;

        SaveData.current.numberOfItems = idOfItem;
        SaveData.current.optionListS = ChoiceManager.optionList;
        SaveData.current.optionIdS = ChoiceManager.optionId;
        SaveData.current.optionNumS = ChoiceManager.optionNum;


        SaveData.current.idOfEventCounterS = idOfEventCounter;
        SaveData.current.eventCountersS = eventCounters;
        SaveData.current.whenToAppearS = whenToAppear;
        SaveData.current.eventIntsS = eventInts;

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
            if (info[i].upgradeObject != null)
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
