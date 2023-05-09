using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIPlayerManager : MonoBehaviour
{
    public static UIPlayerManager instance;
    public State state;
    public StateStart stateStart;
    public StateStartObject stateStartObject;
    public Slider slider3;

    public GameObject playPanal;
    public GameObject videoInfoPanal;
    // Start is called before the first frame update
    public GameObject BGImage;
    public GameObject startPanel;
    public GameObject P1Button;
    public GameObject P2Button;
    public GameObject helloPanel;
    public GameObject helloLeftPanel;
    public GameObject helloRightPanel;
    public GameObject toPanel;
    public GameObject timeoutPanel;
    public GameObject videoBG;

    public GameObject timeout01;
    public GameObject timeout02;
    public GameObject timeout03;

    public float saveValue;

    public PlayerManager playerManager;

    [Header("toSystem")]

    public Sprite[] toSprite;
    public Sprite[] toSpriteMAS;
    public MassageType[] toMassage;
    public GameObject[] massageSet;
    public Image toImage;
    public int toCount;
    float countNextTo;
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
        switch (state)
        {
            case State.start:
                {
                    playPanal.SetActive(false);
                    BGImage.SetActive(true);
                    videoInfoPanal.SetActive(false);

                    videoBG.SetActive(true);
                    videoBG.GetComponent<VideoPlayer>().Play();

                    startPanel.SetActive(true);
                    helloPanel.SetActive(false);
                    toPanel.SetActive(false);
                    timeoutPanel.SetActive(false);
                    StartFunction();
                    break;
                }
            case State.hello:
                {
                    playPanal.SetActive(false);
                    BGImage.SetActive(true);
                    videoInfoPanal.SetActive(false);

                    startPanel.SetActive(false);
                    helloPanel.SetActive(true);
                    if(PlayerInfo.Instance.playerSide == PlayerSide.Left)
                    {
                        helloLeftPanel.SetActive(true);
                        helloRightPanel.SetActive(false);
                    }else
                    {
                        helloLeftPanel.SetActive(false);
                        helloRightPanel.SetActive(true);
                    }
                    toPanel.SetActive(false);
                    timeoutPanel.SetActive(false);
                    StartFunction();
                    break;
                }
            case State.tutorial:
                {
                    if (countNextTo > 5)
                    {
                        toCount++;
                        countNextTo = 0;
                    }
                    if (toCount > toSprite.Length-1)
                    {
                        ToEnd();
                        return;
                    }
                    playPanal.SetActive(false);
                    videoInfoPanal.SetActive(true);

                    BGImage.SetActive(true);
                    startPanel.SetActive(false);
                    helloPanel.SetActive(false);
                    toPanel.SetActive(true);
                    countNextTo += Time.deltaTime;         
                    if(LanguageManager.instance.language == Languages.ENG)
                    {
                        toImage.sprite = toSprite[toCount];
                    }else
                    {
                        toImage.sprite = toSpriteMAS[toCount];
                    }
                    for (int i = 0; i < massageSet.Length; i++)
                    {
                        massageSet[i].SetActive((int)toMassage[toCount] == i);
                    }
                    timeoutPanel.SetActive(false);
                    StartFunction();
                    break;
                }
            case State.endGame:
                {
                    playPanal.SetActive(true);
                    BGImage.SetActive(false);
                    videoInfoPanal.SetActive(true);

                    videoBG.SetActive(false);

                    startPanel.SetActive(false);
                    helloPanel.SetActive(false);
                    toPanel.SetActive(false);
                    timeoutPanel.SetActive(true);
                    if(saveValue >= 1)
                    {
                        timeout01.SetActive(false);
                        timeout02.SetActive(false);
                        timeout03.SetActive(true);
                    }
                    else if (saveValue >= 0.5f)
                    {
                        timeout01.SetActive(false);
                        timeout02.SetActive(true);
                        timeout03.SetActive(false);
                        
                    }
                    else
                    {
                        timeout01.SetActive(true);
                        timeout02.SetActive(false);
                        timeout03.SetActive(false);

                    }

                    break;
                }
            case State.play:
                {
                    playPanal.SetActive(true);
                    videoBG.SetActive(false);
                    videoInfoPanal.SetActive(true);

                    BGImage.SetActive(false);
                    startPanel.SetActive(false);
                    helloPanel.SetActive(false);
                    toPanel.SetActive(false);
                    timeoutPanel.SetActive(false);
                    break;
                }
        }

        slider3.value = saveValue;
    }

    private void StartFunction()
    {
        switch (stateStart)
        {
            /*case StateStart.start:
                {
                    stateStartObject.start.SetActive(true);
                    stateStartObject.player.SetActive(false);
                    stateStartObject.wait.SetActive(false);
                    break;
                }*/
            case StateStart.player:
                {
                    P1Button.GetComponent<Button>().interactable = !GameManager.instance.Waitplayer;

                    P2Button.GetComponent<Button>().interactable = !GameManager.instance.playerIngame;

                    //stateStartObject.start.SetActive(false);
                    stateStartObject.player.SetActive(true);
                    stateStartObject.wait.SetActive(false);
                    break;
                }
            case StateStart.wait:
                {                  
                    //stateStartObject.start.SetActive(false);
                    stateStartObject.player.SetActive(false);
                    stateStartObject.wait.SetActive(true);
                    break;
                }
        }
    }

    public void TapScreen()
    {
        stateStart = StateStart.player;
    }
    public void Play()
    {
        state = State.hello;
    }
    public void Playgame()
    {
        playerManager.playerData.ResetClient();
        state = State.play;
        //PlayerManager.instance.SetGameStart(true);
    }
    public void Wait()
    {
        if(state != State.start)
        {
            state = State.start;
        }
        stateStart = StateStart.wait;
        GameManager.instance.SetPlayerWait(playerManager.indexPlayer);
    }
    public void Cencle()
    {
        stateStart = StateStart.player;
        GameManager.instance.ResetPlayerWait();
    }

    public void SaveData(float value)
    {
        saveValue = value;
    }
    public void StateTo()
    {
        countNextTo = 0;
        toCount = 0;
        state = State.tutorial;
    }
    public void ToEnd()
    {
        countNextTo = 0;
        toCount = 0;
        Playgame();
    }
    public void BackMenu()
    {
        playerManager.playerData.ResetClient();
        playerManager.SetOnSencePlay(false);
        if (GameManager.instance.is2Player)
        {
            GameManager.instance.Check2Player(false);
        }
        state = State.start;
        stateStart = StateStart.player;
        //stateStart = StateStart.start;

    }
    public void PlayOnPlayerInGame()
    {
        playerManager.playerData.ResetDataClient();
        state = State.play;
    }
}
public enum State
{
    start,
    endGame,
    play,
    tutorial,
    hello
}
public enum StateStart
{
    //start,
    player,
    wait,
}
[System.Serializable]
public class StateStartObject
{
    //public GameObject start;
    public GameObject player;
    public GameObject wait;
}
[System.Serializable]
public enum MassageType
{
    A,
    B,
    C
}