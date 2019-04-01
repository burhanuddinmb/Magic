using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayers : MonoBehaviour
{
    [SerializeField] GameObject playerRed;
    [SerializeField] GameObject playerBlue;

    PlayerMovement red;
    PlayerMovement blue;


    // Start is called before the first frame update
    void Start()
    {
        if (playerBlue)
        {
            blue = playerBlue.GetComponent<PlayerMovement>();
            if (playerRed)
                blue.isSelected = false;
        }
        if (playerRed)
        {
            red = playerRed.GetComponent<PlayerMovement>();
            if (playerBlue)
                red.isSelected = false;
        }
    }

    public void SelectRed()
    {
        if (!red.isSelected && blue.isSelected)
        {
            blue.isSelected = false;
        }
        red.isSelected = !red.isSelected;
    }


    public void SelectBlue()
    {
        if (!blue.isSelected && red.isSelected)
        {
            red.isSelected = false;
        }
        blue.isSelected = !blue.isSelected;
    }
}
