using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
using RenderHeads.Media.AVProVideo;

public class PlayerData : NetworkBehaviour
{
    Database database;

    [SyncVar] public Languages languages;

    [Header("Time")]
    [SyncVar] public int Min;
    [SyncVar] public float Sec;

    [Header("Resource")]
    [SyncVar] public int finance;
    [SyncVar] public int manpower;
    [SyncVar] public int RD;

    [Header("Status")]
    [SyncVar] public int energyOutput;
    [SyncVar] public int fuelPrice;
    [SyncVar] public int carbonEmission;
    [SyncVar] public int greenEfficiency;

    public GameObject[] highlight;

    public TowerUpgrade[] tower;
    public List<PowerType> powerTypesList;
    public List<int> energyOutputList;
    public List<int> fuelPriceList;
    public List<int> carbonEmissionList;
    public List<int> greenEfficiencyList;

    public float perEg;
    public float perFp;
    public float perCb;
    public float perOfTarget;

    [Header("OillPump")]
    public bool oillPumpOpen;
    public GameObject oillDefualt;
    public GameObject oillStart;
    public GameObject oillLoop;
    public GameObject oillRevert;

    [Header("Sky")]
    public GameObject skyGray;

    [Header("Firework")]
    public GameObject firework;

    // Start is called before the first frame updat
    void Start()
    {
        database = Database.instance;
    }
 
    private void Update()
    {      
        for (int i = 0; i < tower.Length; i++)
        {
            int energyOutput;
            int fuelPrice;
            int carbonEmission;
            int greenEfficiency;
            if (!tower[i].isUpgrade)
            {
                energyOutput = database.statusResource[(int)(tower[i].powerType)].status.energyOutput;
                fuelPrice = database.statusResource[(int)(tower[i].powerType)].status.fuelPrice;
                carbonEmission = database.statusResource[(int)(tower[i].powerType)].status.carbonEmission;
                greenEfficiency = database.statusResource[(int)(tower[i].powerType)].status.greenEfficiency;
            }
            else
            {
                energyOutput = database.statusResource[(int)(tower[i].powerType)].statusUpgrade.energyOutput;
                fuelPrice = database.statusResource[(int)(tower[i].powerType)].statusUpgrade.fuelPrice;
                carbonEmission = database.statusResource[(int)(tower[i].powerType)].statusUpgrade.carbonEmission;
                greenEfficiency = database.statusResource[(int)(tower[i].powerType)].statusUpgrade.greenEfficiency;

            }
            energyOutputList[i] = energyOutput;
            fuelPriceList[i] = fuelPrice;
            carbonEmissionList[i] = carbonEmission;
            greenEfficiencyList[i] = greenEfficiency;
            powerTypesList[i] = tower[i].powerType;
        }
        energyOutput = energyOutputList.Sum();
        fuelPrice = 600 - fuelPriceList.Sum();
        carbonEmission = carbonEmissionList.Sum();
        greenEfficiency = greenEfficiencyList.Sum();
        calPerOfTarget();
        if (isServer)
        {
            OillPumpSystem();
            if (carbonEmission >= database.carbonEmissionTarget)
            {
                skyGray.SetActive(false);
            }
            else
            {
                skyGray.SetActive(true);
            }
        }
    }
    public void OillPumpSystem()
    {
        if(powerTypesList.Contains(PowerType.Hydrogen))
        {
            if(!oillPumpOpen)
            {
                OillPumpOpen();
                oillPumpOpen = true;
            }
            
        }else
        {
            if(oillPumpOpen)
            {
                oillPumpOpen = false;
            }
        }
    }
    void HandleEvent(MediaPlayer mp, MediaPlayerEvent.EventType eventType, ErrorCode code)
    {
        Debug.Log("MediaPlayer " + mp.name + " generated event: " + eventType.ToString());
        if (eventType == MediaPlayerEvent.EventType.FinishedPlaying)
        {
            OillPumpLoop();
        }
    }
    public void OillPumpOpen()
    {
        oillRevert.SetActive(false);
        oillStart.SetActive(true);
        oillStart.GetComponent<MediaPlayer>().Events.AddListener(HandleEvent);
        oillStart.GetComponent<MediaPlayer>().Rewind(true);
        oillStart.GetComponent<MediaPlayer>().Play();

    }
    public void OillPumpLoop()
    {
        oillLoop.SetActive(true);
        oillLoop.GetComponent<MediaPlayer>().Rewind(true);
        oillLoop.GetComponent<MediaPlayer>().Play();
        oillStart.SetActive(false);
    }
    public void OillPumpRevert()
    {
        oillRevert.SetActive(true);
        oillRevert.GetComponent<MediaPlayer>().Rewind(true);
        oillRevert.GetComponent<MediaPlayer>().Play();
        oillLoop.SetActive(false);
    }
    public void calPerOfTarget()
    {
        perEg = energyOutput < database.energyOutputTarget ? (float)energyOutput / (float)database.energyOutputTarget :  1;
        perFp = fuelPrice < database.fuelPriceTarget ? (float)fuelPrice / (float)database.fuelPriceTarget : 1;
        perCb = carbonEmission < database.carbonEmissionTarget ? (float)carbonEmission / (float)database.carbonEmissionTarget : 1;

        /*if (energyOutput < database.energyOutputTarget) perEg = (float)energyOutput / (float)database.energyOutputTarget;
        else perEg = 1;

        if (fuelPrice < database.fuelPriceTarget) perFp = (float)fuelPrice / (float)database.fuelPriceTarget;
        else perFp = 1;

        if (carbonEmission < database.carbonEmissionTarget) perCb = (float)carbonEmission / (float)database.carbonEmissionTarget;
        else perCb = 1;*/

        perOfTarget = (perEg + perFp + perCb) / 3;
        if (perOfTarget > 0.95 && perOfTarget < 1)
        {
            perOfTarget = 0.95f;
        }
    }
    public void SetRenewableEnergy(int index,PowerType powerType)
    {
        tower[index].powerType = powerType;
        UpgradeEnergy(index,false);
        SetRenewableEnergyServer(index,powerType);
    }
    [Command]
    public void SetRenewableEnergyServer(int index,PowerType powerType)
    {
        tower[index].powerType = powerType;
        UpgradeEnergy(index, false);
    }

    public void UpgradeEnergy(int index,bool value)
    {
        tower[index].isUpgrade = value;
    }

    public void AddResource()
    {
        finance += database.financeAdd;
        manpower += database.manpowerAdd;
        RD += database.RDAdd;
    }
    public void CallDataAgain()
    {
        CallDataAgainClientRPC();
    }
    [ClientRpc]
    public void CallDataAgainClientRPC()
    {
        
    }
    public void ResetClient()
    {
        ResetAll();
        CommandReset();
    }
    [Command(requiresAuthority = false)]
    public void CommandReset()
    {
        ResetAll();
        oillLoop.SetActive(false);
        oillStart.SetActive(false);
    }

    public void ResetDataClient()
    {
        ResetData();
        CommandResetData();
    }
    [Command(requiresAuthority = false)]
    public void CommandResetData()
    {
        ResetData();
        oillLoop.SetActive(false);
        oillStart.SetActive(false);
    }
    public void ResetAll()
    {
        Min = database.timeMinStart;
        Sec = database.timeSecStart;
        ResetData();
    }

    public void ResetData()
    {
        finance = database.financeStart;
        manpower = database.manpowerStart;
        RD = database.RDStart;

        for (int i = 0; i < tower.Length; i++)
        {
            tower[i].isUpgrade = false;
            if (i <= 3)
            {
                tower[i].powerType = PowerType.Coal;
            }
            else
            {
                tower[i].powerType = PowerType.Oil;
            }
            if (isServer)
            {
                tower[i].PlayEffectChange();
            }
        }

    }
    [Command(requiresAuthority = false)]
    public void SetLG(Languages languages)
    {
        this.languages = languages;
    }
    // Update is called once per frame

}

