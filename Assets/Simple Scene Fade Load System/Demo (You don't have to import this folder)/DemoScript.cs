using UnityEngine;
using System.Collections;

public class DemoScript : MonoBehaviour
{
    //name of the scene you want to load
    public string scene;
	public Color loadToColor = Color.white;
    PlayerMovement player;
    Node thisNode;
    [SerializeField]private int levelToUnlock;

   // [SerializeField] private LevelSelector levelSelector;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        thisNode = GetComponent<Node>();
    }
    public void GoFade()
    {
        Initiate.Fade(scene, loadToColor, 1.0f); 
        WinLevel();
        
    }

    private void Update()
    {
        if (player.currentNode == thisNode)
        {
            GoFade();
            
        }
    }
    public void WinLevel()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
    }
}
