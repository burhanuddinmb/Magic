using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetToWin : MonoBehaviour
{
    List<PlayerMovement> players;

    //name of the scene you want to load
    public string scene;
    public Color loadToColor = Color.white;
    PlayerMovement player;
    Node thisNode;
    [SerializeField] private int levelToUnlock;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in playerObjects)
        {
            players.Add(player.GetComponent<PlayerMovement>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (players[0].currentNode == players[1].currentNode)
        {
            GoFade();
        }
    }

    public void GoFade()
    {
        WinLevel();
        Initiate.Fade(scene, loadToColor, 1.0f);
    }

    public void WinLevel()
    {
        if (PlayerPrefs.HasKey("levelReached"))
        {
            int level = PlayerPrefs.GetInt("levelReached");
            if (level > levelToUnlock)
            {
                levelToUnlock = level;
            }
        }
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        PlayerPrefs.Save();
    }
}
