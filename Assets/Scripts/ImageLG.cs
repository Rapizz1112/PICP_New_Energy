using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLG : MonoBehaviour
{
    public Sprite[] sprites;
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
            if ((int)playerData.languages >= sprites.Length || sprites[(int)playerData.languages] == null)
            {
                Debug.LogError("Null " + gameObject.name);
                GetComponent<Image>().sprite = null;
                return;
            }
            GetComponent<Image>().sprite = sprites[(int)playerData.languages];
        }
        else
        {
            if ((int)LanguageManager.instance.language >= sprites.Length || sprites[(int)LanguageManager.instance.language] == null)
            {
                Debug.LogError("Null " + gameObject.name);
                GetComponent<Image>().sprite = null;
                return;
            }
            GetComponent<Image>().sprite = sprites[(int)LanguageManager.instance.language];
        }        
    }
}
