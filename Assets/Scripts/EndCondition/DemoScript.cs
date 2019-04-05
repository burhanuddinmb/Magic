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
        WinLevel();
        Initiate.Fade(scene, loadToColor, 1.0f); 
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
        SaveData.SaveLevelInfo(levelToUnlock - 1, 2.0f);
    }
}
