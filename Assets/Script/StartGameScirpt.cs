using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameScirpt : MonoBehaviour {


    public delegate void GameStartedClicked();
    public static event GameStartedClicked ButtonClicked;
    public delegate void NextTurnClicked();
    public static event NextTurnClicked NextTrun;


    public void TriggerClickedEvent()
    {
        if(ButtonClicked != null)
        {
            ButtonClicked();
        }
    }

    public void TriggerNextTurn()
    {
        if(NextTrun != null)
        {
            NextTrun();
        }
    }
}
