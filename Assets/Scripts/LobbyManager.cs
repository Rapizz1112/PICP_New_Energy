using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] TMP_InputField input;
    [SerializeField] GameObject inputField;
    public bool ipOpen;
    string ip;
    public GameObject BG;
    public Toggle rememberIP;
    public bool isPC;
    public TMP_Dropdown dropdownSide;
    // Start is called before the first frame update
    void Start()
    {
        input.text = PlayerPrefs.GetString("IP");
        ip = GetLocalIPAddress();
        if(isPC)
        {
            StartServer();
        }else
        {
            BG.SetActive(true);
        }
        rememberIP.isOn = PlayerPrefs.GetInt("toggle") == 1;
        dropdownSide.value = PlayerPrefs.GetInt("side");

    }

    // Update is called once per frame
    void Update()
    {      
        inputField.SetActive(ipOpen);
        PlayerInfo.Instance.playerSide = (PlayerSide)dropdownSide.value;
        /*if(string.IsNullOrEmpty(input.text))
        {
            playerButton.interactable = false;
        }else
        {
            playerButton.interactable = true;
        }*/
    }
    public void ApplySystem()
    {
        ipOpen = false;

        if (rememberIP.isOn)
        {
            PlayerPrefs.SetString("IP", input.text);
            PlayerPrefs.SetInt("toggle", 1);
            PlayerPrefs.SetInt("side", (int)dropdownSide.value);
        }
        else
        {
            PlayerPrefs.SetString("IP", "");
            PlayerPrefs.SetInt("toggle", 0);
            PlayerPrefs.SetInt("side", 0);
        }
    }
    public void CancelSystem()
    {
        ipOpen = false;
        input.text = PlayerPrefs.GetString("IP");
        rememberIP.isOn = PlayerPrefs.GetInt("toggle") == 1;
        dropdownSide.value = PlayerPrefs.GetInt("side");
    }
    public void IPOpenSystem()
    {
        ipOpen = !ipOpen;
    }
    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
    public void StartServer()
    {
        NetworkManager.singleton.networkAddress = ip;
        NetworkManager.singleton.StartServer();
    }
    public void StartClient()
    {
        if(string.IsNullOrEmpty(input.text))
        {
            ipOpen = true;
            return;
        }
        NetworkManager.singleton.networkAddress = input.text;
        NetworkManager.singleton.StartClient();
    }

}
