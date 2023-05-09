using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerManager : MonoBehaviour
{
    public static ServerManager instance;

    [Header("Server")]
    public GameObject player1Display;
    public GameObject player2Display;

    [Header("Player")]
    public GameObject player1;
    public GameObject player2;

    [Header("P1Display")]
    public TextMeshProUGUI timeP1;
    public Slider enSliderP1;
    public Slider cbSliderP1;
    public Slider fpSliderP1;
    public Slider perSliderP1;
    public TextMeshProUGUI[] nameEnegyP1;


    [Header("P2Display")]
    public TextMeshProUGUI timeP2;
    public Slider enSliderP2;
    public Slider cbSliderP2;
    public Slider fpSliderP2;
    public Slider perSliderP2;
    public TextMeshProUGUI[] nameEnegyP2;

    public GameObject ipAddress;

    [Header("Object")]

    public GameObject topTextObject;

    public GameObject leftPlayerObject;
    public GameObject rightPlayerObject;


    public TextMeshProUGUI playerLeft;
    public bool leftTextIsSet;
    public TextMeshProUGUI playerRight;
    public bool rightTextIsSet;

    public bool haveP1;

    public GameObject hideText;
    public GameObject plant2Parent;
    public GameObject plant2;
    public GameObject MASplant2;

    public GameObject logoPlant1;
    public GameObject logoMASplant1;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.is2Player)
        {
            if (GameManager.instance.player1Data.Min < 0 || GameManager.instance.player2Data.Min < 0)
            {
                SoundManager.instance.playSound(Sound.endGame);
                if (player1 != null) player1.GetComponent<PlayerManager>().TimeOut2PlayerClient();
                if (player2 != null) player2.GetComponent<PlayerManager>().TimeOut2PlayerClient();
                GameManager.instance.is2Player = false;
            }
            if (GameManager.instance.player1Data.perOfTarget >= 1)
            {
                if (player1 != null) player1.GetComponent<PlayerManager>().Win2Player();
                if (player2 != null) player2.GetComponent<PlayerManager>().Lost2Player();
                GameManager.instance.is2Player = false;
            }
            else if(GameManager.instance.player2Data.perOfTarget >= 1)
            {
                if (player1 != null) player1.GetComponent<PlayerManager>().Lost2Player();
                if (player2 != null) player2.GetComponent<PlayerManager>().Win2Player();
                GameManager.instance.is2Player = false;
            }
        }
        ConnectPlayer();
        UIsystem();
    }
    private void UIsystem()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            ipAddress.SetActive(!ipAddress.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            hideText.SetActive(!hideText.activeSelf);
        }
        if (GameManager.instance.player1Data.Min >= 0 && GameManager.instance.player1Data.Sec >= 0) timeP1.text = GameManager.instance.player1Data.Min.ToString("00") + ":" + Mathf.Floor(GameManager.instance.player1Data.Sec).ToString("00");
        enSliderP1.value = GameManager.instance.player1Data.energyOutput;
        fpSliderP1.value = GameManager.instance.player1Data.fuelPrice;
        cbSliderP1.value = GameManager.instance.player1Data.carbonEmission;
        perSliderP1.value = GameManager.instance.player1Data.perOfTarget;
        for(int i = 0; i < nameEnegyP1.Length; i++)
        {
            nameEnegyP1[i].text = GameManager.instance.player1Data.tower[i].powerType.ToString();
        }

        if (GameManager.instance.player2Data.Min >= 0 && GameManager.instance.player2Data.Sec >= 0) timeP2.text = GameManager.instance.player2Data.Min.ToString("00") + ":" + Mathf.Floor(GameManager.instance.player2Data.Sec).ToString("00");
        enSliderP2.value = GameManager.instance.player2Data.energyOutput;
        fpSliderP2.value = GameManager.instance.player2Data.fuelPrice;
        cbSliderP2.value = GameManager.instance.player2Data.carbonEmission;
        perSliderP2.value = GameManager.instance.player2Data.perOfTarget;
        for (int i = 0; i < nameEnegyP1.Length; i++)
        {
            nameEnegyP2[i].text = GameManager.instance.player2Data.tower[i].powerType.ToString();
        }
        if (CheckTime(GameManager.instance.player1Data, 1) || CheckTime(GameManager.instance.player2Data, 2))
        {
            SoundManager.instance.clock10S.SetActive(true);
        }
        else
        {
            SoundManager.instance.clock10S.SetActive(false);
        }
    }
    private void ConnectPlayer()
    {
        if((player1 != null && player1.GetComponent<PlayerManager>().gameStart) || (player2 != null && player2.GetComponent<PlayerManager>().gameStart))
        {                    
            GameManager.instance.playerIngame = true;
        }
        else
        {
            GameManager.instance.playerIngame = false;
            haveP1 = false;
        }
        var isOnplay = (player1 != null && player1.GetComponent<PlayerManager>().onScenePlay) || (player2 != null && player2.GetComponent<PlayerManager>().onScenePlay);
        topTextObject.SetActive(!isOnplay);
        if (logoPlant1.activeInHierarchy) logoPlant1.GetComponent<Animator>().SetBool("gameStart", isOnplay);
        if(logoMASplant1.activeInHierarchy) logoMASplant1.GetComponent<Animator>().SetBool("gameStart", isOnplay);
        plant2Parent.SetActive(isOnplay);
        if (player1 != null && player1.GetComponent<PlayerManager>().gameStart)
        {           
            leftPlayerObject.SetActive(true);
            if(!leftTextIsSet)
            {
                if (!haveP1)
                {
                    playerLeft.text = GameManager.instance.player1Data.languages == Languages.ENG ? "PLAYER 01" : "PEMAIN 01";
                    haveP1 = true;
                    leftTextIsSet = true;
                }
                else
                {
                    playerLeft.text = GameManager.instance.player1Data.languages == Languages.ENG ? "PLAYER 02" : "PEMAIN 02";

                    leftTextIsSet = true;
                }
            }           
        }
        else
        {
            //GameManager.instance.player1Data.ResetAll();
            for(int i = 0; i < GameManager.instance.player1Data.highlight.Length; i++)
            {
                GameManager.instance.player1Data.highlight[i].SetActive(false);
            }
            leftTextIsSet = false;
            leftPlayerObject.SetActive(false);
        }
        if (player2 != null && player2.GetComponent<PlayerManager>().gameStart)
        {

            rightPlayerObject.SetActive(true);
            if (!rightTextIsSet)
            {
                if (!haveP1)
                {
                    playerRight.text = GameManager.instance.player2Data.languages == Languages.ENG ? "PLAYER 01" : "PEMAIN 01";
                    haveP1 = true;
                    rightTextIsSet = true;
                }
                else
                {
                    playerRight.text = GameManager.instance.player2Data.languages == Languages.ENG ? "PLAYER 02" : "PEMAIN 02";
                    rightTextIsSet = true;
                }
            }
        }
        else
        {
            //GameManager.instance.player2Data.ResetAll();
            for (int i = 0; i < GameManager.instance.player2Data.highlight.Length; i++)
            {
                GameManager.instance.player2Data.highlight[i].SetActive(false);
            }
            rightTextIsSet = false;
            rightPlayerObject.SetActive(false);
        }

        /*if(player1 != null && player2 != null)
        {
            ipAddress.SetActive(false);
        }else
        {
            ipAddress.SetActive(true);
        }*/
    }

    public bool CheckTime(PlayerData playerData, int numPlayer)
    {
        GameObject player = null;
        switch (numPlayer)
        {
            case 1:
                {
                    if(player1 != null)
                    {
                        player = player1;
                    }
                    break;
                }
            case 2:
                {
                    if (player2 != null)
                    {
                        player = player2;
                    }
                    break;
                }
        }
        if (player != null && player.GetComponent<PlayerManager>().gameStart)
        {
            if(playerData.Min == 0 && playerData.Sec <= 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }else
        {
            return false;
        }   
    }
    public void CheckSidePlayer(GameObject player,PlayerSide playerSide)
    {
        if(playerSide == PlayerSide.Left)
        {
            if (player1 == null)
            {
                player1 = player;
                player.GetComponent<PlayerManager>().indexPlayer = 1;

            }else
            {
                if (player2 != null)
                {
                    player.GetComponent<PlayerManager>().DisPlayer();   
                    return;
                }
                player2 = player;
                player.GetComponent<PlayerManager>().ChangeSide(PlayerSide.Right);
                player.GetComponent<PlayerManager>().side = PlayerSide.Right;
                player.GetComponent<PlayerManager>().indexPlayer = 2;

            }
        }else if(playerSide == PlayerSide.Right)
        {
            if (player2 == null)
            {
                player2 = player;
                player.GetComponent<PlayerManager>().indexPlayer = 2;

            }
            else
            {
                if (player1 != null)
                {
                    player.GetComponent<PlayerManager>().DisPlayer();
                    return;
                }
                player1 = player;
                player.GetComponent<PlayerManager>().ChangeSide(PlayerSide.Left);
                player.GetComponent<PlayerManager>().side = PlayerSide.Left;
                player.GetComponent<PlayerManager>().indexPlayer = 1;

            }
        }
    }
}

