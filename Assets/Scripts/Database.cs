using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Database : MonoBehaviour
{
    public static Database instance;
    public int timeMinStart;
    public int timeSecStart;
    public int financeStart;
    public int manpowerStart;
    public int RDStart;

    public int financeAdd;
    public int manpowerAdd;
    public int RDAdd;

    public int energyOutputTarget;
    public int fuelPriceTarget;
    public int carbonEmissionTarget;

    public string[] namePowerENG;
    public string[] namePowerMAS;

    public PowerType[] correctPower;

    public StatusResource[] statusResource;
    public RenewableEnergy[] renewableEnergies;

    public Sprite[] powerSprite;

    public Sprite[] energySpriteList;
    public Sprite[] fuelPriceSpriteList;
    public Sprite[] carbonEmissionSpriteList;
    public Sprite[] greenEfficiencySpriteList;

    [Header("Info")]
    //public Sprite defaultPowerSprite;
    public Sprite keepUpSprite;
    public Sprite moneySprite;
    public Sprite[] timeSprite;
    public Sprite rememberSprite;
    public Sprite wellDoneSprite;
    public Sprite[] infoPowerSpriteList;
    public Sprite[] resultPowerSpriteList;

    [Header("InfoMAS")]
    //public Sprite defaultPowerSpriteMAS;
    public Sprite keepUpSpriteMAS;
    public Sprite moneySpriteMAS;
    public Sprite[] timeSpriteMAS;
    public Sprite rememberSpriteMAS;
    public Sprite wellDoneSpriteMAS;
    public Sprite[] infoPowerSpriteListMAS;
    public Sprite[] resultPowerSpriteListMAS;


    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class StatusResource
{
    public string name;
    public StatusData status;
    public StatusData statusUpgrade;
}
[System.Serializable]
public class StatusData
{
    public int energyOutput;
    public int fuelPrice;
    public int carbonEmission;
    public int greenEfficiency;
}
[System.Serializable]
public class RenewableEnergy
{
    public string name;
    public RenewableData renewable;
    public RenewableData renewableUpgrade;
}
[System.Serializable]
public class RenewableData
{
    public int finance;
    public int manpower;
    public int RD;
}
public enum PowerType
{
    Solar,
    Wind,
    Hydroelectric,
    Geothermal,
    BioEnergy,
    Hydrogen,
    Gas,
    Coal,
    Oil,
}
