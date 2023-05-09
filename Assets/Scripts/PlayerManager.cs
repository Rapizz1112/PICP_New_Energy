using Mirror;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : NetworkBehaviour
{
    //public static PlayerManager instance;
    Database database;
    GameManager gameManager;
    ServerManager serverManager;

    [Header("Data")]
    [SyncVar] public int indexPlayer;
    public PlayerData playerData;


    [Header("UIGaneral")]
    #region
     TextMeshProUGUI finance;
     TextMeshProUGUI manpower;
     TextMeshProUGUI RD;

    public GameObject bgMoney;


    TextMeshProUGUI time;
     Slider energyOutputSlider;
     Slider fuelPriceSlider;
     Slider carbonEmissionSlider;
     Slider perOfTarget;

     public List<string> powerNames;
     List<string> powerNumberNames;
     TextMeshProUGUI[] namePower;

     Image[] energyOutputList;
     Image[] fuelPriceList;
     Image[] carbonEmissionList;
     Image[] greenEfficiencyList;


     Button[] selectButton;


     Button[] upgradeButton;
     TextMeshProUGUI[] nonUpgrade;
    #endregion

    [Header("Renewable")]
    #region
    bool renewableOpen;
     GameObject UIRenewable;

     Image renewablePowerIcon;
     TextMeshProUGUI renewablePowerName;

    public RenewablePower renewablePowerDefault;
    public  List<RenewablePower> renewablePowers;

     TextMeshProUGUI currentName;
     RenewableOutputUI currentOutput;


    public List<Button> selectButtonRenewable;
    public List<GameObject> selectBg;
    public List<RenewableUI> renewableUI;

     Image requiredRenewablIcon;
     RenewableUI requiredRenewableUI;
     int onSelect;

    public RenewableData renewableCost;

     Button confirmRenewableButton;


    public Button exitButtonSelect;
    #endregion

    [Header("UpgradeUI")]
    #region
     bool upgradeOpen;
     GameObject UIUpgrade;

     Image upgradePowerIcon;
     TextMeshProUGUI upgradePowerName;

     Image currentEn;
     Image currentFp;
     Image currentCe;
     Image currentGe;

     Image upgradeEn;
     Image upgradeFp;
     Image upgradeCe;
     Image upgradeGe;

     int costFnUpgrade;
     int costMpUpgrade;
     int costRdUpgrade;

     TextMeshProUGUI costFnUpgradeText;
     TextMeshProUGUI costMpUpgradeText;
     TextMeshProUGUI costRdUpgradeText;

     Button confirmUpgradeButton;

     Button exitButton;
    Button surrenderButton;

    #endregion
    [Header("PlayAgian")]
    #region
    public Button playAgainTimeout;


    #endregion
    public Image infoUI;

    public int towerSelect;
    public PowerType powerTypeSelect;
    bool addInSec;
    [Header("GameStatus")]
    [SyncVar] public bool gameStart;
    [SyncVar] public bool onScenePlay;
    public float countInteraction;
    public bool isPlayWithFirends;
    // Start is called before the first frame update
    public bool isCount;

    public GameObject[] finger;
    public bool checkFinger;

    [Header("Surrender")]
    public bool surrenderOpen;
    public GameObject bgSurrender;
    public Button yesSurrender;
    public Button noSurrender;
    public Button countButton;
    public TextMeshProUGUI countText;

    [SyncVar] public PlayerSide side;

    public StateMassage stateMassage;
    public bool onMoney;
    public bool massageStart;

    public int checkAddResoure;

    [Header("Massage")]
    public MassageType massageType;

    public int fingerNumber;

    public bool endGame;
    void Start()
    {
        if(isLocalPlayer)
        {
            SendDataToServer(PlayerInfo.Instance.playerSide);
        }
        database = Database.instance;
        gameManager = GameManager.instance;
        if (isServer)
        {
            serverManager = ServerManager.instance;
        }
        if (isLocalPlayer)
        {
            /*
            coinPlayer = UIManager.Instance.coinPlayer;
            shopCostUI = UIManager.Instance.shopCostUI;
            hotelCostUI = UIManager.Instance.hotelCostUI;
            officeCostUI = UIManager.Instance.officeCostUI;
            shopUpgrade = UIManager.Instance.shopUpgrade;
            hotelUpgrade = UIManager.Instance.hotelUpgrade;
            officeUpgrade = UIManager.Instance.officeUpgrade;          
            shopUpgrade.onClick.AddListener(delegate () { UpgradeTower(0); });
            hotelUpgrade.onClick.AddListener(delegate () { UpgradeTower(1); });
            officeUpgrade.onClick.AddListener(delegate () { UpgradeTower(2); });*/
            finance = UIManager.Instance.finance;
            manpower = UIManager.Instance.manpower;
            RD = UIManager.Instance.RD;
            bgMoney = UIManager.Instance.bgMoney;

            perOfTarget = UIManager.Instance.perOfTarget;

            time = UIManager.Instance.time;


            namePower = UIManager.Instance.namePower;

            energyOutputSlider = UIManager.Instance.energyOutputSlider;
            fuelPriceSlider = UIManager.Instance.fuelPriceSlider;
            carbonEmissionSlider = UIManager.Instance.carbonEmissionSlider;

            energyOutputList = UIManager.Instance.energyOutputList;
            fuelPriceList = UIManager.Instance.fuelPriceList;
            carbonEmissionList = UIManager.Instance.carbonEmissionList;
            greenEfficiencyList = UIManager.Instance.greenEfficiencyList;

            selectButton = UIManager.Instance.selectButton;

            UIRenewable = UIManager.Instance.UIRenewable;
            renewablePowerIcon = UIManager.Instance.renewablePowerIcon;
            renewablePowerName = UIManager.Instance.renewablePowerName;

            for (int i = 0; i < selectButton.Length; i++)
            {
                int j = i;
                selectButton[i].onClick.AddListener(() => SoundManager.instance.playSound(Sound.click));
                selectButton[i].onClick.AddListener(delegate () { CountReset(); });
                selectButton[i].onClick.AddListener(() => RenewableButtonSystem(j));

            }
            currentName = UIManager.Instance.currentName;
            currentOutput = UIManager.Instance.currentOutput;
            requiredRenewablIcon = UIManager.Instance.requiredRenewablIcon;
            requiredRenewableUI = UIManager.Instance.requiredRenewableUI;
            confirmRenewableButton = UIManager.Instance.confirmRenewableButton;

            confirmRenewableButton.onClick.AddListener(() => SoundManager.instance.playSound(Sound.click));
            confirmRenewableButton.onClick.AddListener(delegate () { CountReset(); });
            confirmRenewableButton.onClick.AddListener(() => ConfirmRenewable());
            exitButtonSelect = UIManager.Instance.exitButtonSelect;

            exitButtonSelect.onClick.AddListener(() => SoundManager.instance.playSound(Sound.click));
            exitButtonSelect.onClick.AddListener(delegate () { CountReset(); });
            exitButtonSelect.onClick.AddListener(delegate () { ExitSelect(); });

            upgradeButton = UIManager.Instance.upgradeButton;
            for (int i = 0; i < upgradeButton.Length; i++)
            {
                int j = i;
                upgradeButton[i].onClick.AddListener(() => SoundManager.instance.playSound(Sound.click));
                upgradeButton[i].onClick.AddListener(delegate () { CountReset(); });
                upgradeButton[i].onClick.AddListener(() => UpgradeSystemButton(j));
            }
            nonUpgrade = UIManager.Instance.nonUpgrade;

            UIUpgrade = UIManager.Instance.UIUpgrade;

            upgradePowerIcon = UIManager.Instance.upgradePowerIcon;
            upgradePowerName = UIManager.Instance.upgradePowerName;

            currentCe = UIManager.Instance.currentCe;
            currentEn = UIManager.Instance.currentEn;
            currentFp = UIManager.Instance.currentFp;
            currentGe = UIManager.Instance.currentGe;

            upgradeCe = UIManager.Instance.upgradeCe;
            upgradeEn = UIManager.Instance.upgradeEn;
            upgradeFp = UIManager.Instance.upgradeFp;
            upgradeGe = UIManager.Instance.upgradeGe;   

            costFnUpgradeText = UIManager.Instance.costFnUpgrade;
            costMpUpgradeText = UIManager.Instance.costMpUpgrade;
            costRdUpgradeText = UIManager.Instance.costRdUpgrade;

            confirmUpgradeButton = UIManager.Instance.confirmUpgradeButton;
            confirmUpgradeButton.onClick.AddListener(() => SoundManager.instance.playSound(Sound.click));
            confirmUpgradeButton.onClick.AddListener(delegate () { CountReset(); });
            confirmUpgradeButton.onClick.AddListener(() => ConfirmUpgrade());

            exitButton = UIManager.Instance.exitButton;

            exitButton.onClick.AddListener(() => SoundManager.instance.playSound(Sound.click));
            exitButton.onClick.AddListener(delegate () { CountReset(); });
            exitButton.onClick.AddListener(delegate () { ExitUpgrade(); });


            surrenderButton = UIManager.Instance.surrenderButton;

            surrenderButton.onClick.AddListener(() => SoundManager.instance.playSound(Sound.click));
            surrenderButton.onClick.AddListener(delegate () { CountReset(); });
            surrenderButton.onClick.AddListener(() => SurrenderSystem(true));

            infoUI = UIManager.Instance.infoUI;

            playAgainTimeout = UIManager.Instance.playAgainTimeout;

            bgSurrender = UIManager.Instance.bgSurrender;
            yesSurrender = UIManager.Instance.yesSurrender;

            yesSurrender.onClick.AddListener(() => SoundManager.instance.playSound(Sound.click));
            yesSurrender.onClick.AddListener(delegate () { CountReset(); });
            yesSurrender.onClick.AddListener(delegate () { YesSurrenderSystem(); });

            noSurrender = UIManager.Instance.noSurrender;

            noSurrender.onClick.AddListener(() => SoundManager.instance.playSound(Sound.click));
            noSurrender.onClick.AddListener(delegate () { CountReset(); });
            noSurrender.onClick.AddListener(() => SurrenderSystem(false));

            countButton = UIManager.Instance.countButton;
            countButton.onClick.AddListener(delegate () { CountReset(); });

            countText = UIManager.Instance.countText;
            finger = UIManager.Instance.finger;
            gameManager.RequireData();
        }
        

    }
    [Command]
    void SendDataToServer(PlayerSide playerSide)
    {
        side = playerSide;
        ServerManager.instance.CheckSidePlayer(gameObject, side);
    }
    [TargetRpc]
    public void ChangeSide(PlayerSide playerSide)
    {
        PlayerPrefs.SetInt("side", (int)playerSide);
        PlayerInfo.Instance.playerSide = (PlayerSide)PlayerPrefs.GetInt("side");
    }


    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
            UIPlayerManager.instance.playerManager = this;

            if (UIPlayerManager.instance.state == State.play)
            {
                if (!gameStart)
                {
                    SetStateMassage(StateMassage.endTo);
                    SetGameStart(true);
                    SetOnSencePlay(true);
                    CountReset();
                    endGame = false;
                    playerData.SetLG(LanguageManager.instance.language);
                } 
            }else
            {
                if(!gameManager.is2Player && gameStart) SetGameStart(false);
            }
        }
        if (playerData == null)
        {
            if (indexPlayer == 1)
            {
                playerData = gameManager.player1Data;
            }
            else if (indexPlayer == 2)
            {
                playerData = gameManager.player2Data;
            }
            return;      
        }
        if(isLocalPlayer && ((UIPlayerManager.instance.state == State.play && gameStart) || UIPlayerManager.instance.state == State.endGame))
        {
            CountInteractionSystem();
        }
        if (!gameStart) return;
        if (isServer)
        {            
            if (playerData.Sec >= 0)
            {
                /*if ((int)playerData.Sec == 30 && !addInSec)
                {
                    playerData.AddResource();
                    addInSec = true;
                }*/
                if ((int)playerData.Sec != 60 && (int)playerData.Sec % 15 == 0 && checkAddResoure != (int)playerData.Sec)
                {
                    playerData.AddResource();
                    Debug.Log("Add");
                    checkAddResoure = (int)playerData.Sec;
                }

                playerData.Sec -= Time.deltaTime;
            }
            else
            {
                /*if (addInSec) playerData.AddResource();
                addInSec = false;*/
                playerData.Sec = 60;
                playerData.Min--;
            }                   
        }
        if (isLocalPlayer)
        {
            UIShow();
            if (gameManager.is2Player || endGame) return;
            if (playerData.Min < 0)
            {
                TimeOutSystem();
                endGame = true;
            }
            if (playerData.perOfTarget >= 1)
            {
                WinClient();
                endGame = true;
            }
        }
    }
 
    public void CountInteractionSystem()
    {
        countInteraction += Time.deltaTime;
        if(countInteraction >= 80)
        {
            countButton.gameObject.SetActive(true);
            countText.text = Mathf.Floor(91 - countInteraction).ToString();
        }else
        {
            countButton.gameObject.SetActive(false);
            countText.text = "10";
        }
        if(countInteraction >= 30)
        {
            fingerNumber = FingerSystem();
            if(!checkFinger)
            {
                for(int i = 0;i < 15;i++)
                {
                    finger[i].SetActive(i == fingerNumber);
                }
                SetStateMassage(StateMassage.time);
                checkFinger = true;
            }
        }
        if (countInteraction >= 90)
        {
            countButton.gameObject.SetActive(false);
            BackToMenu();
        }
    }
    public int FingerSystem()
    {
        for (int i = 0; i < playerData.tower.Length; i++)
        {
            if(playerData.tower[i].powerType == PowerType.Coal || playerData.tower[i].powerType == PowerType.Oil)
            {
                return i;
            }
        }
        for (int i = 0; i < playerData.tower.Length; i++)
        {
            if (!playerData.tower[i].isUpgrade)
            {
                return 6 + i;
            }
        }
        float[] perArray = new float[]{playerData.perEg,playerData.perCb,playerData.perFp};
        int maxIndex = perArray.ToList().IndexOf(perArray.Min());
        return 12+maxIndex;
        //////////////////////////////////////////////////////////
        /*for (int i = 0; i < playerData.tower.Length; i++)
        {
            if (playerData.tower[i].powerType != database.correctPower[i])
            {
                return i;
            }
        }
        for (int i = 0; i < playerData.tower.Length; i++)
        {
            if (!playerData.tower[i].isUpgrade)
            {
                return 6 + i;
            }
        }*/
    }
    public void UIShow()
    {
        if (playerData == null) return;
        finance.text = playerData.finance + "";
        manpower.text = playerData.manpower + "";
        RD.text = playerData.RD + "";
        if (playerData.Min >= 0) time.text = playerData.Min.ToString("00") + ":" + Mathf.Floor(playerData.Sec).ToString("00");

        energyOutputSlider.value = playerData.energyOutput;
        fuelPriceSlider.value = playerData.fuelPrice;
        carbonEmissionSlider.value = playerData.carbonEmission;
        perOfTarget.value = playerData.perOfTarget;

        CollectData();

        if (renewableOpen)
        {
            if (renewableCost.finance != 0)
            {
                confirmRenewableButton.interactable = true;
            }
            else
            {
                confirmRenewableButton.interactable = false;
            }
        }

        //InfoMassageSystem();
        if (surrenderOpen)
        {
            bgSurrender.SetActive(true);
        }
        else
        {
            bgSurrender.SetActive(false);
        }
        
    }
    private void InfoMassageSystem()
    {
        switch(stateMassage)
        {
            case StateMassage.endTo:
                {
                    infoUI.sprite = LanguageManager.instance.language == Languages.ENG ?  database.wellDoneSprite : database.wellDoneSpriteMAS;
                    break;
                }
            case StateMassage.remember:
                {
                    infoUI.sprite = LanguageManager.instance.language == Languages.ENG ? database.rememberSprite : database.rememberSpriteMAS;
                    break;
                }
            case StateMassage.keepIt:
                {
                    infoUI.sprite = LanguageManager.instance.language == Languages.ENG ? database.keepUpSprite : database.keepUpSpriteMAS;
                    break;
                }
            case StateMassage.time:
                {
                    Sprite spriteTime = default;
                    if(fingerNumber < 12)
                    {
                        spriteTime = LanguageManager.instance.language == Languages.ENG ? database.timeSprite[0] : database.timeSpriteMAS[0];
                    }
                    else if(fingerNumber == 12)
                    {
                        spriteTime = LanguageManager.instance.language == Languages.ENG ? database.timeSprite[1] : database.timeSpriteMAS[1];
                    }
                    else if(fingerNumber == 13)
                    {
                        spriteTime = LanguageManager.instance.language == Languages.ENG ? database.timeSprite[2] : database.timeSpriteMAS[2];

                    }
                    else if(fingerNumber == 14)
                    {
                        spriteTime = LanguageManager.instance.language == Languages.ENG ? database.timeSprite[3] : database.timeSpriteMAS[3];
                    }
                    infoUI.sprite = spriteTime;
                    break;
                }
            case StateMassage.onBuy:
                {
                    break;
                }

        }

    }
    

    public void SurrenderSystem(bool value)
    {
        if(value)
        {
            SoundManager.instance.playSound(Sound.resetPopUp);
        }
        surrenderOpen = value;
    }
    public void YesSurrenderSystem()
    {
        //SurrenderButtonSystem();
        BackToMenu();
        SurrenderSystem(false);
    }
    public void CollectData()
    {

        for (int i = 0; i < energyOutputList.Length; i++)
        {
            int currentEnNum = (int)Mathf.Ceil((float)playerData.energyOutputList[i] / 25);
            int currentFpNum = (int)Mathf.Ceil((float)playerData.fuelPriceList[i] / 25);
            int currentCeNum = (int)Mathf.Ceil((float)playerData.carbonEmissionList[i] / 25);
            int currentGeNum = (int)Mathf.Ceil((float)playerData.greenEfficiencyList[i] / 25);

            energyOutputList[i].sprite = database.energySpriteList[currentEnNum];
            fuelPriceList[i].sprite = database.fuelPriceSpriteList[currentFpNum];
            carbonEmissionList[i].sprite = database.carbonEmissionSpriteList[currentCeNum];
            greenEfficiencyList[i].sprite = database.greenEfficiencySpriteList[currentGeNum];

        }


        for (int i = 0; i < playerData.tower.Length; i++)
        {
            selectButton[i].GetComponent<Image>().sprite = database.powerSprite[(int)playerData.tower[i].powerType];
            namePower[i].text = LanguageManager.instance.language == Languages.ENG ?
               database.namePowerENG[(int)playerData.tower[i].powerType] :
                database.namePowerMAS[(int)playerData.tower[i].powerType];

            powerNames[i] = LanguageManager.instance.language == Languages.ENG ?
               database.namePowerENG[(int)playerData.tower[i].powerType] :
                database.namePowerMAS[(int)playerData.tower[i].powerType];

            //powerNames[i] = playerData.tower[i].powerType.ToString();
            //namePower[i].text = playerData.tower[i].powerType.ToString();

            if (playerData.tower[i].powerType != PowerType.Oil && playerData.tower[i].powerType != PowerType.Coal && !playerData.tower[i].isUpgrade)
            {
                upgradeButton[i].gameObject.SetActive(true);
                nonUpgrade[i].gameObject.SetActive(false);
            }
            else
            {
                upgradeButton[i].gameObject.SetActive(false);
                nonUpgrade[i].gameObject.SetActive(true);
            }
        }
        foreach(string i in powerNames)
        {

        }
        for(int i = 0; i < powerNames.Count; i++)
        {

        }
    }
    public void RenewableButtonSystem(int index)
    {
        UIRenewable.SetActive(true);
        SetStateMassage(StateMassage.onBuy);
        SoundManager.instance.playSound(Sound.popUp);
        renewableOpen = true;
        towerSelect = index;
        renewablePowerIcon.sprite = database.powerSprite[(int)playerData.tower[towerSelect].powerType];
        renewablePowerName.text = LanguageManager.instance.language == Languages.ENG ?
               database.namePowerENG[(int)playerData.tower[towerSelect].powerType].ToUpper() + " PLANT" :
                "LOJI " + database.namePowerMAS[(int)playerData.tower[towerSelect].powerType].ToUpper();
        //renewablePowerName.text = playerData.tower[towerSelect].powerType.ToString().ToUpper() + " PLANT";
        //renewablePowerName.text = playerData.tower[towerSelect].powerType.ToString().ToUpper() + " LOJI";
        requiredRenewablIcon.sprite = database.powerSprite[(int)playerData.tower[towerSelect].powerType];
        //requiredRenewableUI.nameType.text = playerData.tower[towerSelect].powerType.ToString() + "\n" + "Energy";
        requiredRenewableUI.nameType.text = LanguageManager.instance.language == Languages.ENG ?
              database.namePowerENG[(int)playerData.tower[towerSelect].powerType] + "\n" + "Energy" :
               "Tenaga" + "\n" + database.namePowerMAS[(int)playerData.tower[towerSelect].powerType];
        //requiredRenewableUI.nameType.text =  "TENAGA" + "\n" + playerData.tower[towerSelect].powerType.ToString();
        HighlightSystem(towerSelect, true);

        for (int i = 0; i < playerData.tower[index].canRenewable.Count; i++)
        {
            StatusData statusData = new StatusData();
            RenewableData renewableData = new RenewableData();

            if (playerData.tower[index].canRenewable[i] != playerData.tower[index].powerType)
            {
                if (playerData.tower[index].canRenewable[i] != PowerType.Coal && playerData.tower[index].canRenewable[i] != PowerType.Oil)
                {
                    PowerType powerType = playerData.tower[index].canRenewable[i];
                    statusData.energyOutput = database.statusResource[(int)powerType].status.energyOutput;
                    statusData.fuelPrice = database.statusResource[(int)powerType].status.fuelPrice;
                    statusData.carbonEmission = database.statusResource[(int)powerType].status.carbonEmission;
                    statusData.greenEfficiency = database.statusResource[(int)powerType].status.greenEfficiency;

                    renewableData.finance = database.renewableEnergies[(int)powerType].renewable.finance;
                    renewableData.manpower = database.renewableEnergies[(int)powerType].renewable.manpower;
                    renewableData.RD = database.renewableEnergies[(int)powerType].renewable.RD;

                    int currentEnNum = (int)Mathf.Ceil((float)statusData.energyOutput / 25);
                    int currentFpNum = (int)Mathf.Ceil((float)statusData.fuelPrice / 25);
                    int currentCeNum = (int)Mathf.Ceil((float)statusData.carbonEmission / 25);
                    int currentGeNum = (int)Mathf.Ceil((float)statusData.greenEfficiency / 25);

                    //currentOutput.imageFp.sprite = database.powerSprite[currentEnNum];
                    //currentOutput.imageEn.sprite = database.powerSprite[currentFpNum];
                    //currentOutput.imageFp.sprite = database.powerSprite[currentCeNum];

                    renewablePowers.Add(new RenewablePower
                    {
                        PowerType = playerData.tower[index].canRenewable[i],
                        enSprite = database.energySpriteList[currentEnNum],
                        fpSprite = database.fuelPriceSpriteList[currentFpNum],
                        ceSprite = database.carbonEmissionSpriteList[currentCeNum],
                        geSprite = database.greenEfficiencySpriteList[currentCeNum],
                        statusData = statusData,
                        renewableData = renewableData
                    });
                    
                }          
            }
            else
            {
                PowerType powerType = playerData.tower[index].canRenewable[i];

                statusData.energyOutput = database.statusResource[(int)powerType].status.energyOutput;
                statusData.fuelPrice = database.statusResource[(int)powerType].status.fuelPrice;
                statusData.carbonEmission = database.statusResource[(int)powerType].status.carbonEmission;
                statusData.greenEfficiency = database.statusResource[(int)powerType].status.greenEfficiency;

                renewableData.finance = 0;
                renewableData.manpower = 0;
                renewableData.RD = 0;

                renewablePowerDefault.PowerType = playerData.tower[index].powerType;
                renewablePowerDefault.statusData = statusData;
                renewablePowerDefault.renewableData = renewableData;

                //currentName.text = renewablePowerDefault.PowerType.ToString();
                currentName.text = LanguageManager.instance.language == Languages.ENG ?
                    database.namePowerENG[(int)renewablePowerDefault.PowerType] :
                    database.namePowerMAS[(int)renewablePowerDefault.PowerType];


                int currentEnNum = (int)Mathf.Ceil((float)statusData.energyOutput / 25);
                int currentFpNum = (int)Mathf.Ceil((float)statusData.fuelPrice / 25);
                int currentCeNum = (int)Mathf.Ceil((float)statusData.carbonEmission / 25);
                int currentGeNum = (int)Mathf.Ceil((float)statusData.greenEfficiency / 25);

                currentOutput.imageEn.sprite = database.energySpriteList[currentEnNum];
                currentOutput.imageFp.sprite = database.fuelPriceSpriteList[currentFpNum];
                currentOutput.imageCe.sprite = database.carbonEmissionSpriteList[currentCeNum];
                currentOutput.imageGe.sprite = database.greenEfficiencySpriteList[currentGeNum];

            }
        }
        for (int i = 0; i < renewablePowers.Count; i++)
        {
            int j = i;
            GameObject Object = Instantiate(UIManager.Instance.renewablePrefabs, UIManager.Instance.renewableParent.transform);
            RenewableManager renewable = Object.GetComponent<RenewableManager>();
            selectButtonRenewable.Add(renewable.selectButtonRenewable);
            selectBg.Add(renewable.selectBg);
            renewableUI.Add(renewable.renewableUI);
            selectButtonRenewable[i].onClick.RemoveAllListeners();
            selectButtonRenewable[i].onClick.AddListener(delegate () { CountReset(); });
            selectButtonRenewable[i].onClick.AddListener(() => SoundManager.instance.playSound(Sound.click));
            selectButtonRenewable[i].onClick.AddListener(() => SelectRenewable(j));          
            //renewableUI[i].nameType.text = renewablePowers[i].PowerType.ToString();
            renewableUI[i].nameType.text = LanguageManager.instance.language == Languages.ENG ?
                database.namePowerENG[(int)renewablePowers[i].PowerType] :
                database.namePowerMAS[(int)renewablePowers[i].PowerType];
         
            renewableUI[i].renewableOutputUI.imageEn.sprite = renewablePowers[i].enSprite;
            renewableUI[i].renewableOutputUI.imageFp.sprite = renewablePowers[i].fpSprite;
            renewableUI[i].renewableOutputUI.imageCe.sprite = renewablePowers[i].ceSprite;
            renewableUI[i].renewableOutputUI.imageGe.sprite = renewablePowers[i].geSprite;
            renewableUI[i].costFn.text = renewablePowers[i].renewableData.finance + "";
            renewableUI[i].costMp.text = renewablePowers[i].renewableData.manpower + "";
            renewableUI[i].costRd.text = renewablePowers[i].renewableData.RD + "";
        }
        if(database.infoPowerSpriteList[(int)database.correctPower[towerSelect]] != null)
        {
            if(playerData.tower[towerSelect].powerType == database.correctPower[towerSelect])
            {
                infoUI.sprite = LanguageManager.instance.language == Languages.ENG ? 
                    database.resultPowerSpriteList[(int)playerData.tower[towerSelect].powerType] : 
                    database.resultPowerSpriteListMAS[(int)playerData.tower[towerSelect].powerType];
                SetStateMassageType(MassageType.A);
            }
            else
            {
                infoUI.sprite = LanguageManager.instance.language == Languages.ENG ? 
                    database.infoPowerSpriteList[(int)database.correctPower[towerSelect]] : 
                    database.infoPowerSpriteListMAS[(int)database.correctPower[towerSelect]];

                SetStateMassageType(MassageType.B);
            }
        }
    }
    public void SelectRenewable(int index)
    {
        SetStateMassage(StateMassage.onBuy);
        onSelect = index;
        powerTypeSelect = renewablePowers[index].PowerType;
        renewableCost.finance = renewablePowers[index].renewableData.finance;
        renewableCost.manpower = renewablePowers[index].renewableData.manpower;
        renewableCost.RD = renewablePowers[index].renewableData.RD;

        for (int i = 0; i < selectBg.Count; i++)
        {
            if(i == index)
            {
                selectBg[i].SetActive(true);
            }else
            {
                selectBg[i].SetActive(false);
            }
        }
        requiredRenewablIcon.sprite = database.powerSprite[(int)renewablePowers[index].PowerType];
        //requiredRenewableUI.nameType.text = renewablePowers[index].PowerType.ToString() + "\n"+"Energy";
        requiredRenewableUI.nameType.text = LanguageManager.instance.language == Languages.ENG ?
              database.namePowerENG[(int)renewablePowers[index].PowerType] + "\n" + "Energy" :
               "Tenaga" + "\n" + database.namePowerMAS[(int)renewablePowers[index].PowerType];
        requiredRenewableUI.costFn.text = renewablePowers[index].renewableData.finance + "";
        requiredRenewableUI.costMp.text = renewablePowers[index].renewableData.manpower + "";
        requiredRenewableUI.costRd.text = renewablePowers[index].renewableData.RD + "";
        if (powerTypeSelect == database.correctPower[towerSelect])
        {
            infoUI.sprite = LanguageManager.instance.language == Languages.ENG ? 
                database.resultPowerSpriteList[(int)renewablePowers[index].PowerType] : 
                database.resultPowerSpriteListMAS[(int)renewablePowers[index].PowerType];

            SetStateMassageType(MassageType.A);
        }
        else
        {
            infoUI.sprite = LanguageManager.instance.language == Languages.ENG ?
                database.infoPowerSpriteList[(int)database.correctPower[towerSelect]] : 
                database.infoPowerSpriteListMAS[(int)database.correctPower[towerSelect]];

            SetStateMassageType(MassageType.B);
        }

    }
    public void ConfirmRenewable()
    {
        bool checkResorce = playerData.finance >= renewableCost.finance && playerData.manpower >= renewableCost.manpower && playerData.RD >= renewableCost.RD && renewableCost.finance != 0;

        if (checkResorce)
        {
            playerData.tower[towerSelect].powerType = powerTypeSelect;
            playerData.tower[towerSelect].isUpgrade = false;
            ConfirmRenewableServer(towerSelect, renewableCost.finance, renewableCost.manpower, renewableCost.RD, (int)powerTypeSelect);
            ExitSelect();
            StartCoroutine("KeepitSystem");
        }
        else
        {
            if (!onMoney)
            {
                StartCoroutine("RedMoneySystem");
            }
        }
        
    }
    [Command]
    public void ConfirmRenewableServer(int index, int costFn, int costMp, int costRd,int powerTypeSelect)
    {
        CalResorce(costFn, costMp, costRd);
        playerData.tower[index].powerType = (PowerType)powerTypeSelect;
        playerData.tower[index].isUpgrade = false;
        playerData.tower[index].PlayEffectChange();
        SoundManager.instance.playSound(Sound.buildingChange);
    }
    public void ExitSelect()
    {
        StopIEMoney();
        UIRenewable.SetActive(false);
        HighlightSystem(towerSelect, false);
        renewableOpen = false;
        renewablePowerDefault = new RenewablePower();
        renewablePowers.Clear();
        renewableCost = new RenewableData();
        for(int i = 0; i < selectButtonRenewable.Count; i++)
        {
            Destroy(selectButtonRenewable[i].gameObject);
        }
        selectButtonRenewable.Clear();
        selectBg.Clear();
        renewableUI.Clear();
        Vector3 pos = UIManager.Instance.content.GetComponent<RectTransform>().position;
        UIManager.Instance.content.GetComponent<RectTransform>().position = new Vector3(pos.x,0, pos.z);

        requiredRenewableUI.nameType.text = "";
        requiredRenewableUI.costFn.text = "0";
        requiredRenewableUI.costMp.text = "0";
        requiredRenewableUI.costRd.text = "0";
        onSelect = 0;
        towerSelect = 0;
        CollectData();

    }
    #region Upgrade
    public void UpgradeSystemButton(int index)
    {
        UIUpgrade.SetActive(true);
        SetStateMassage(StateMassage.onBuy);
        SoundManager.instance.playSound(Sound.popUp);
        upgradeOpen = true;
        towerSelect = index;
        upgradePowerIcon.sprite = database.powerSprite[(int)playerData.tower[towerSelect].powerType];
        //upgradePowerName.text = playerData.tower[towerSelect].powerType.ToString().ToUpper() + " PLANT";
        upgradePowerName.text = LanguageManager.instance.language == Languages.ENG ?
            database.namePowerENG[(int)playerData.tower[towerSelect].powerType].ToUpper() + " PLANT" :
            "LOJI " + database.namePowerMAS[(int)playerData.tower[towerSelect].powerType].ToUpper();
        HighlightSystem(towerSelect, true);

        float enFloat = (float)database.statusResource[(int)(playerData.tower[index].powerType)].status.energyOutput;
        float fpFloat = (float)database.statusResource[(int)(playerData.tower[index].powerType)].status.fuelPrice;
        float ceFloat = (float)database.statusResource[(int)(playerData.tower[index].powerType)].status.carbonEmission;
        float geFloat = (float)database.statusResource[(int)(playerData.tower[index].powerType)].status.greenEfficiency;

        float upgradeEnFloat = (float)database.statusResource[(int)(playerData.tower[index].powerType)].statusUpgrade.energyOutput;
        float upgradeFpFloat = (float)database.statusResource[(int)(playerData.tower[index].powerType)].statusUpgrade.fuelPrice;
        float upgradeCeFloat = (float)database.statusResource[(int)(playerData.tower[index].powerType)].statusUpgrade.carbonEmission;
        float upgradeGeFloat = (float)database.statusResource[(int)(playerData.tower[index].powerType)].statusUpgrade.greenEfficiency;

        int currentEnNum = (int)Mathf.Ceil(enFloat / 25);
        int currentFpNum = (int)Mathf.Ceil(fpFloat / 25);
        int currentCeNum = (int)Mathf.Ceil(ceFloat / 25);
        int currentGeNum = (int)Mathf.Ceil(geFloat / 25);

        int upgradeEnNum = (int)Mathf.Ceil(upgradeEnFloat / 25);
        int upgradeFpNum = (int)Mathf.Ceil(upgradeFpFloat / 25);
        int upgradeCeNum = (int)Mathf.Ceil(upgradeCeFloat / 25);
        int upgradeGeNum = (int)Mathf.Ceil(upgradeGeFloat / 25);

        currentEn.sprite = database.energySpriteList[currentEnNum];
        currentFp.sprite = database.fuelPriceSpriteList[currentFpNum];
        currentCe.sprite = database.carbonEmissionSpriteList[currentCeNum];
        currentGe.sprite = database.greenEfficiencySpriteList[currentGeNum];

        upgradeEn.sprite = database.energySpriteList[upgradeEnNum];
        upgradeFp.sprite = database.fuelPriceSpriteList[upgradeFpNum];
        upgradeCe.sprite = database.carbonEmissionSpriteList[upgradeCeNum];
        upgradeGe.sprite = database.greenEfficiencySpriteList[upgradeGeNum];

        costFnUpgrade = database.renewableEnergies[(int)(playerData.tower[index].powerType)].renewableUpgrade.finance;
        costMpUpgrade = database.renewableEnergies[(int)(playerData.tower[index].powerType)].renewableUpgrade.manpower;
        costRdUpgrade = database.renewableEnergies[(int)(playerData.tower[index].powerType)].renewableUpgrade.RD;

        costFnUpgradeText.text = "" + costFnUpgrade;
        costMpUpgradeText.text = "" + costMpUpgrade;
        costRdUpgradeText.text = "" + costRdUpgrade;

        if (database.infoPowerSpriteList[(int)database.correctPower[towerSelect]] != null)
        {
            if (playerData.tower[towerSelect].powerType == database.correctPower[towerSelect])
            {
                infoUI.sprite = LanguageManager.instance.language == Languages.ENG ? 
                    database.resultPowerSpriteList[(int)playerData.tower[towerSelect].powerType] 
                    : database.resultPowerSpriteListMAS[(int)playerData.tower[towerSelect].powerType];

                SetStateMassageType(MassageType.A);
            }
            else
            {
                infoUI.sprite = LanguageManager.instance.language == Languages.ENG ? 
                    database.infoPowerSpriteList[(int)database.correctPower[towerSelect]] 
                    : database.infoPowerSpriteListMAS[(int)database.correctPower[towerSelect]];

                SetStateMassageType(MassageType.B);
            }
        }
    }
    public void ConfirmUpgrade()
    {
        bool checkResorce = playerData.finance >= costFnUpgrade && playerData.manpower >= costMpUpgrade && playerData.RD >= costRdUpgrade;

        if (checkResorce)
        {
            playerData.tower[towerSelect].isUpgrade = true;
            UpgradeServer(towerSelect, costFnUpgrade, costMpUpgrade, costRdUpgrade);
            ExitUpgrade();
            StartCoroutine("KeepitSystem");
        }
        else
        {
            if (!onMoney)
            {
                StartCoroutine("RedMoneySystem");
            }

        }
    }
    public void StopIEMoney()
    {
        StartCoroutine("RedMoneySystem");
        SetStateMassage(StateMassage.remember);
        bgMoney.SetActive(false);
        onMoney = false;
    }
    public void ExitUpgrade()
    {
        StopIEMoney();
        UIUpgrade.SetActive(false);
        HighlightSystem(towerSelect, false);
        upgradeOpen = false;
        costFnUpgrade = 0;
        costMpUpgrade = 0;
        costRdUpgrade = 0;
        towerSelect = 0;
        CollectData();
    }
    [Command]
    private void HighlightSystem(int index,bool open)
    {
        if(open)
        {
            for (int i = 0; i< playerData.highlight.Length;i++)
            {
                playerData.highlight[i].SetActive(i == index);
            }

        }else
        {
            for (int i = 0; i < playerData.highlight.Length; i++)
            {
                playerData.highlight[i].SetActive(false);
            }
        }
    }
    #endregion
    [Command]
    private void UpgradeServer(int index,int costFnUpgrade,int costMpUpgrade,int costRdUpgrade)
    {
        CalResorce(costFnUpgrade,costMpUpgrade,costRdUpgrade);
        playerData.tower[index].isUpgrade = true;
        playerData.tower[index].PlayEffectChange();
        SoundManager.instance.playSound(Sound.buildingChange);
    }
  
    private void CalResorce(int costFn, int costMp, int costRd)
    {
        playerData.finance -= costFn;
        playerData.manpower -= costMp;
        playerData.RD -= costRd;
    }

    public IEnumerator RedMoneySystem()
    {
        onMoney = true;
        bgMoney.SetActive(true);
        infoUI.sprite = LanguageManager.instance.language == Languages.ENG ?
            infoUI.sprite = database.moneySprite :
            infoUI.sprite = database.moneySpriteMAS;
        SetStateMassageType(MassageType.B);
        yield return new WaitForSeconds(5);
        if (!upgradeOpen && !renewableOpen) SetStateMassage(StateMassage.remember);
        bgMoney.SetActive(false);
        onMoney = false;
    }
    public IEnumerator KeepitSystem()
    {
        SetStateMassage(StateMassage.keepIt);
        yield return new WaitForSeconds(3);
        if (!upgradeOpen && !renewableOpen) SetStateMassage(StateMassage.remember);
    }
    private void SetStateMassage(StateMassage stateMassage)
    {
        this.stateMassage = stateMassage;
        switch(stateMassage)
        {
            case StateMassage.endTo:
                {
                    infoUI.sprite = LanguageManager.instance.language == Languages.ENG ?  database.wellDoneSprite : database.wellDoneSpriteMAS;
                    SetStateMassageType(MassageType.A);
                    break;
                }
            case StateMassage.remember:
                {
                    infoUI.sprite = LanguageManager.instance.language == Languages.ENG ? database.rememberSprite : database.rememberSpriteMAS;
                    SetStateMassageType(MassageType.B);
                    break;
                }
            case StateMassage.keepIt:
                {
                    infoUI.sprite = LanguageManager.instance.language == Languages.ENG ? database.keepUpSprite : database.keepUpSpriteMAS;
                    SetStateMassageType(MassageType.C);
                    break;
                }
            case StateMassage.time:
                {
                    Sprite spriteTime = default;
                    if (fingerNumber < 12)
                    {
                        spriteTime = LanguageManager.instance.language == Languages.ENG ? database.timeSprite[0] : database.timeSpriteMAS[0];
                    }
                    else if(fingerNumber == 12)
                    {
                        spriteTime = LanguageManager.instance.language == Languages.ENG ? database.timeSprite[1] : database.timeSpriteMAS[1];
                    }
                    else if(fingerNumber == 13)
                    {
                        spriteTime = LanguageManager.instance.language == Languages.ENG ? database.timeSprite[2] : database.timeSpriteMAS[2];

                    }
                    else if(fingerNumber == 14)
                    {
                        spriteTime = LanguageManager.instance.language == Languages.ENG ? database.timeSprite[3] : database.timeSpriteMAS[3];
                    }
                    infoUI.sprite = spriteTime;
                    SetStateMassageType(MassageType.C);
                    break;
                }
            case StateMassage.onBuy:
                {
                    //massageType = MassageType.B;
                    break;
                }
        }
    }
    private void SetStateMassageType(MassageType massageType)
    {
        this.massageType = massageType;
        for (int i = 0; i < UIManager.Instance.massageSet.Length; i++)
        {
            UIManager.Instance.massageSet[i].SetActive(false);
        }
        for (int i = 0; i < UIManager.Instance.massageSet.Length; i++)
        {
            UIManager.Instance.massageSet[i].SetActive(i == (int)massageType);
        }
    }

    IEnumerator CountdownCount(float time)
    {
        int count = (int)time;
        while (count < 60)
        {
            // display something...
            yield return new WaitForSeconds(1);
            count += 1;
        }

    }
    public void WinSystem()
    {
        countInteraction = 50;
        CheckExit();
        UIPlayerManager.instance.saveValue = playerData.perOfTarget;
        UIPlayerManager.instance.state = State.endGame;
    }

    public void LostSystem()
    {
        CheckExit();
        UIPlayerManager.instance.saveValue = playerData.perOfTarget;
        UIPlayerManager.instance.state = State.endGame;
    }
    [TargetRpc]
    public void Lost2Player()
    {
        playAgainTimeout.onClick.RemoveAllListeners();
        playAgainTimeout.onClick.AddListener(() => SoundManager.instance.playSound(Sound.click));
        playAgainTimeout.onClick.AddListener(delegate () { CountReset(); });
        playAgainTimeout.onClick.AddListener(delegate () { UIPlayerManager.instance.Wait(); });
        LostSystem();
    }
    [TargetRpc]
    public void Win2Player()
    {
        playAgainTimeout.onClick.RemoveAllListeners();
        playAgainTimeout.onClick.AddListener(() => SoundManager.instance.playSound(Sound.click));
        playAgainTimeout.onClick.AddListener(delegate () { CountReset(); });
        playAgainTimeout.onClick.AddListener(delegate () { UIPlayerManager.instance.Wait(); });
        WinSystem();
    }

    public void WinClient()
    {
        WinServer();
        playAgainTimeout.onClick.RemoveAllListeners();
        playAgainTimeout.onClick.AddListener(() => SoundManager.instance.playSound(Sound.click));
        playAgainTimeout.onClick.AddListener(delegate () { CountReset(); });
        playAgainTimeout.onClick.AddListener(delegate () { UIPlayerManager.instance.Playgame(); });
        WinSystem();
    }
    [Command]
    public void WinServer()
    {
        Debug.Log("WinServer");
        Invoke("FireworkDelay", 2f);
        if (serverManager.plant2.activeInHierarchy && serverManager.plant2.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            serverManager.plant2.GetComponent<Animator>().SetTrigger("Win");
        }
        if (serverManager.MASplant2.activeInHierarchy && serverManager.MASplant2.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            serverManager.MASplant2.GetComponent<Animator>().SetTrigger("Win");
        }

    }

    private void FireworkDelay()
    {
        playerData.firework.SetActive(false);
        playerData.firework.SetActive(true);
    }

    public void SurrenderButtonSystem()
    {
        ////////LostPage
        //playAgainLose.onClick.RemoveAllListeners();
        //playAgainLose.onClick.AddListener(() => SoundManager.instance.playSound(Sound.click));
        if (gameManager.is2Player)
        {
            /* gameManager.playerCheckWin(indexPlayer);*/
            ////////LostPage
            //playAgainLose.onClick.AddListener(delegate () { UIPlayerManager.instance.PlayOnPlayerInGame(); });

            //////ResetOnly
            playerData.ResetDataClient();

        }
        else
        {
            playerData.ResetClient();
            //////ResetOnly
            //playAgainLose.onClick.RemoveAllListeners();
            //playAgainLose.onClick.AddListener(delegate () { UIPlayerManager.instance.Play(); });
            //////ResetOnly
            //LostSystem();
        }
        ////////LostPage
        //LostSystem();
    }
    public void TimeOutSystem()
    {
        playAgainTimeout.onClick.RemoveAllListeners();
        playAgainTimeout.onClick.AddListener(() => SoundManager.instance.playSound(Sound.click));
        playAgainTimeout.onClick.AddListener(delegate () { CountReset(); });
        playAgainTimeout.onClick.AddListener(delegate () { UIPlayerManager.instance.Playgame(); });
        CheckExit();
        UIPlayerManager.instance.saveValue = playerData.perOfTarget;
        SoundManager.instance.playSoundServer(Sound.endGame);
        UIPlayerManager.instance.state = State.endGame;
    }
    [TargetRpc]
    public void TimeOut2PlayerClient()
    {
        playAgainTimeout.onClick.RemoveAllListeners();
        playAgainTimeout.onClick.AddListener(() => SoundManager.instance.playSound(Sound.click));
        playAgainTimeout.onClick.AddListener(delegate () { CountReset(); });
        playAgainTimeout.onClick.AddListener(delegate () { UIPlayerManager.instance.Wait(); });
        CheckExit();
        UIPlayerManager.instance.saveValue = playerData.perOfTarget;
        UIPlayerManager.instance.state = State.endGame;
    }
    public void CountReset()
    {
        countInteraction = 0;
        if(checkFinger)
        {
            for (int i = 0; i < 15; i++)
            {
                finger[i].SetActive(false);
            }
            checkFinger = false;
        }
        if (!massageStart)
        {
            SetStateMassage(StateMassage.remember);
            massageStart = true;
        }
    }
    private void CheckExit()
    {
        if (upgradeOpen)
        {
            ExitUpgrade();
        }
        if (renewableOpen)
        {
            ExitSelect();
        }
        if (surrenderOpen)
        {
            bgSurrender.SetActive(false);
            surrenderOpen = false;
        }
    }
    [Command]
    public void SetGameStart(bool value)
    {
        gameStart = value;
    }
    [Command]
    public void SetOnSencePlay(bool value)
    {
        onScenePlay = value;
    }
    [TargetRpc]
    public void DisPlayer()
    {
        NetworkClient.Disconnect();
    }
    public void BackToMenu()
    {
        CountReset();
        CheckExit();       
        UIPlayerManager.instance.BackMenu();
    }
}
[System.Serializable]
public class RenewablePower
{
    public PowerType PowerType;
    public Sprite enSprite;
    public Sprite fpSprite;
    public Sprite ceSprite;
    public Sprite geSprite;
    public StatusData statusData;
    public RenewableData renewableData;
}

public enum StateMassage
{
    endTo,
    remember,
    keepIt,
    time,
    onBuy
}
