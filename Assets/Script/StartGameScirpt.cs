using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEditor;

public class StartGameScirpt : MonoBehaviour {

    public delegate void GameStartedClick(int w);
    public static event GameStartedClick Spawns;

    public delegate void GameStartedClicked();
    public static event GameStartedClicked ButtonClicked;
    public delegate void NextTurnClicked();
    public static event NextTurnClicked NextTrun;
    private bool autoturn;

    public void Update()
    {
        if (autoturn)
        {
            AllTurns();
            Thread.Sleep(1000);
        }
        
    }
    public void TriggerClickedEvent(int w)
    {
        if(ButtonClicked != null)
        {
            Spawns(w);
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


    private void AllTurns()
    {
        if (NextTrun != null)
        {
            if (!SpawnScript.GameWon && ControlScript.turnCount <= 41)
            {
                
                NextTrun();

            }
            else autoturn = false;
        }

    }

    public void TriggerAllTurns()
    {

        autoturn = !autoturn;
        
    }

    IEnumerator Example()
    {
        //print(Time.time);
        yield return new WaitForSeconds(1);
        
        //print(Time.time);
    }

}
