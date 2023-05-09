using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager instance;
    public Languages language;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        language = (Languages)PlayerPrefs.GetInt("lg");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("lg",(int)language);
    }
    public void ChangeLg()
    {
        language = language == Languages.ENG ? Languages.MAS : Languages.ENG;
    }

}
public enum Languages
{
    ENG,
    MAS
}
