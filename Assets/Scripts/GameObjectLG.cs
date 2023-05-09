using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectLG : MonoBehaviour
{
    public GameObject[] gameObjects;
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
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].SetActive((int)playerData.languages == i);
            }
        }
        else
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].SetActive((int)LanguageManager.instance.language == i);
            }
        }
    }
}
