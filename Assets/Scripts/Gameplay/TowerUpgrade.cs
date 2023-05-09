using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    public PowerType powerType;
    public bool isUpgrade;
    public List<PowerType> canRenewable;

    public PowerObject powerObject;

    public MediaPlayer changePower;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!isUpgrade)
        {
            for(int i = 0; i < powerObject.powerObject.Length; i++)
            {
                if(i == (int)powerType)
                {
                    if (powerObject.powerObject[i].powerObject != null) powerObject.powerObject[i].powerObject.SetActive(true);
                    if (powerObject.powerObject[i].powerObjectUpgrade != null) powerObject.powerObject[i].powerObjectUpgrade.SetActive(false);
                }else
                {
                    if (powerObject.powerObject[i].powerObject != null) powerObject.powerObject[i].powerObject.SetActive(false);
                    if (powerObject.powerObject[i].powerObjectUpgrade != null) powerObject.powerObject[i].powerObjectUpgrade.SetActive(false);
                }
            }
        }else
        {
            for (int i = 0; i < powerObject.powerObject.Length; i++)
            {
                if (i == (int)powerType)
                {
                    if (powerObject.powerObject[i].powerObject != null) powerObject.powerObject[i].powerObject.SetActive(false);
                    if (powerObject.powerObject[i].powerObjectUpgrade != null) powerObject.powerObject[i].powerObjectUpgrade.SetActive(true);
                }
                else
                {
                    if (powerObject.powerObject[i].powerObject != null) powerObject.powerObject[i].powerObject.SetActive(false);
                    if (powerObject.powerObject[i].powerObjectUpgrade != null) powerObject.powerObject[i].powerObjectUpgrade.SetActive(false);
                }
            }
        }
    }
    public void PlayEffectChange()
    {
        changePower.Rewind(true);
        changePower.Play();
        if (!isUpgrade)
        {
            powerObject.powerObject[(int)powerType].powerObject.GetComponentInChildren<MediaPlayer>().Play();
        }
        else
        {
            powerObject.powerObject[(int)powerType].powerObjectUpgrade.GetComponentInChildren<MediaPlayer>().Play();
        }
    }
 

}
[System.Serializable]
public class PowerObject
{
    public PowerObjectSet[] powerObject;
}
[System.Serializable]
public class PowerObjectSet
{
    public GameObject powerObject;
    public GameObject powerObjectUpgrade;
}
