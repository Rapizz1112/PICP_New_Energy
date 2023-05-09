using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class UIManager : NetworkBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI ipAddress;

    public TextMeshProUGUI finance;
    public TextMeshProUGUI manpower;
    public TextMeshProUGUI RD;

    public GameObject bgMoney;


    public TextMeshProUGUI time;

    public Slider energyOutputSlider;
    public Slider fuelPriceSlider;
    public Slider carbonEmissionSlider;
    public Slider perOfTarget;

    public TextMeshProUGUI[] namePower;

    public Image[] energyOutputList;
    public Image[] fuelPriceList;
    public Image[] carbonEmissionList;
    public Image[] greenEfficiencyList;

    [Header("SelectButton")]
    public Button[] selectButton;


    [Header("UpgradeButton")]
    public Button[] upgradeButton;
    public TextMeshProUGUI[] nonUpgrade;


    [Header("SelectUI")]
    public GameObject UIRenewable;

    public Image renewablePowerIcon;
    public TextMeshProUGUI renewablePowerName;

    public TextMeshProUGUI costFnSelect;
    public TextMeshProUGUI costMpSelect;
    public TextMeshProUGUI costRdSelect;


    public TextMeshProUGUI currentName;
    public RenewableOutputUI currentOutput;

    public GameObject renewableParent;  
    public GameObject renewablePrefabs;
    public GameObject content;

    public Image requiredRenewablIcon;
    public RenewableUI requiredRenewableUI;

    public Button confirmRenewableButton;

    public Button exitButtonSelect;

    [Header("UpgradeUI")]
    public GameObject UIUpgrade;

    public Image upgradePowerIcon;
    public TextMeshProUGUI upgradePowerName;

    public Image currentEn;
    public Image currentFp;
    public Image currentCe;
    public Image currentGe;

    public Image upgradeEn;
    public Image upgradeFp;
    public Image upgradeCe;
    public Image upgradeGe;

    public TextMeshProUGUI costFnUpgrade;
    public TextMeshProUGUI costMpUpgrade;
    public TextMeshProUGUI costRdUpgrade;

    public Button confirmUpgradeButton;

    public Button exitButton;
    public Button surrenderButton;

    public Image infoUI;


    [Header("PlayAgian")]
    public Button playAgainWin;
    public Button playAgainLose;
    public Button playAgainTimeout;

    [Header("Surrender")]
    public GameObject bgSurrender;
    public Button yesSurrender;
    public Button noSurrender;

    public Button countButton;
    public TextMeshProUGUI countText;

    [Header("Massage")]
    public GameObject[] massageSet;
    public GameObject[] finger;

    // Start is called before the first frame update
    private void Awake()
    {        
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(NetworkManager.singleton != null)
        {
            ipAddress.text = "IP Address : " + NetworkManager.singleton.networkAddress;
        }
    }
}
[System.Serializable]
public class RenewableUI
{
    public TextMeshProUGUI nameType;
    public RenewableOutputUI renewableOutputUI;
    public TextMeshProUGUI costFn;
    public TextMeshProUGUI costMp;
    public TextMeshProUGUI costRd;
}
[System.Serializable]
public class RenewableOutputUI
{
    public Image imageEn;
    public Image imageFp;
    public Image imageCe;
    public Image imageGe;
}
