using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public static bool unlock;

    public Button[] levelButtons;

    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
                levelButtons[i].interactable = false;
        }
    }

    void Update()
    {
        //if (unlock)
        //{
        //    Debug.Log("levelUnlock:     " + Fader.levelUnlock);
        //    //levels[Fader.levelUnlock - 1].SetActive(false);
        //    unlock = false;
        //}
    }
}
