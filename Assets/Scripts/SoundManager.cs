using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class SoundManager : NetworkBehaviour
{
    public static SoundManager instance;
    public AudioSource[] audioSource;
    public GameObject clock10S;

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
        
    }
    public void playSound(int sound)
    {
        audioSource[sound].Play();
    }
    public void playSound(Sound sound)
    {
        audioSource[(int)sound].Play();
    }

    [Command(requiresAuthority = false)]
    public void playSoundServer(Sound sound)
    {
        audioSource[(int)sound].Play();
    }
}
public enum Sound
{
    buildingChange,
    click,
    endGame,
    popUp,
    resetPopUp
}
