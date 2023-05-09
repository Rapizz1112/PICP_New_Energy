using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextLG : MonoBehaviour
{
    public string[] strings;
    public bool isServer;
    public PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            if ((int)playerData.languages >= strings.Length || strings[(int)playerData.languages] == null)
            {
                Debug.LogError("Null " + gameObject.name);
                GetComponent<TextMeshProUGUI>().text = "ERROR";
                return;
            }
            string output = strings[(int)playerData.languages].Replace("\\n", "\n");
            GetComponent<TextMeshProUGUI>().text = output;
        }
        else
        {
            if ((int)LanguageManager.instance.language >= strings.Length || strings[(int)LanguageManager.instance.language] == null)
            {
                Debug.LogError("Null " + gameObject.name);
                GetComponent<TextMeshProUGUI>().text = "ERROR";
                return;
            }
            string output = strings[(int)LanguageManager.instance.language].Replace("\\n", "\n");
            GetComponent<TextMeshProUGUI>().text = output;
        }
        
    }
}
