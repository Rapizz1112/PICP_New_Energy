using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    public GameObject serverDisplay;
    public GameObject uiPlayerStart;

    public PlayerData player1Data;
    public PlayerData player2Data;


    [Header("WaitPlayer")]
    [SyncVar] public bool player1Wiat;
    [SyncVar] public bool player2Wiat;


    [SyncVar] public bool Waitplayer;
    [SyncVar] public bool playerIngame;

    [SyncVar] public bool is2Player;

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
        if (isServer)
        {
            serverDisplay.SetActive(true);
            if(player1Wiat || player2Wiat)
            {
                Waitplayer = true;
                if(player1Wiat && player2Wiat)
                {
                    PlayerPlay();
                    Debug.Log("PlayerPlay");
                    is2Player = true;
                    player1Wiat = false;
                    player2Wiat = false;
                    Waitplayer = false;
                }
            }

        }
        else 
        {
            uiPlayerStart.SetActive(true);
        }
    }
    [Command(requiresAuthority = false)]
    public void Check2Player(bool value)
    {
        if(is2Player != value)
        {
            is2Player = value;
        }
    }

    [Command(requiresAuthority = false)]
    public void SetPlayerWait(int index)
    {
        is2Player = false;
        if (index == 1)
        {
            player1Wiat = true;
        }
        else if (index == 2)
        {
            player2Wiat = true;
        }
    }
    [Command(requiresAuthority = false)]
    public void SetPlayerIngame(bool value)
    {
        is2Player = value;
    }

    [Command(requiresAuthority = false)]
    public void ResetPlayerWait()
    {
        player1Wiat = false;
        player2Wiat = false;
        Waitplayer = false;
    }
    [ClientRpc]
    public void PlayerPlay()
    {
        NetworkClient.localPlayer.GetComponent<PlayerManager>().isPlayWithFirends = true;
        UIPlayerManager.instance.Playgame();
    }

    [Command(requiresAuthority = false)]
    public void RequireData()
    {
        player1Data.CallDataAgain();
        player2Data.CallDataAgain();
    }
    public void DisconnectPlayer()
    {
        NetworkManager.singleton.StopClient();
    }   
    public void DisconnectServer()
    {
        NetworkManager.singleton.StopHost();
    }
    public void LGSystem()
    {
        LanguageManager.instance.ChangeLg();
    }
}
