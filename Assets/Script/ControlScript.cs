using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript : MonoBehaviour, connect4CTRL
{
    connect4AI playerOne;
    connect4AI playerTwo;
    private int currentTurn = 1;
    private int[,] board;
    SpawnScript spawner;
    public GameObject TurnButton;

    void Start()
    {
        StartGameScirpt.ButtonClicked += GameStart;
        StartGameScirpt.NextTrun += NextTurn;
    }

    public bool CheckGameState()
    {
        throw new NotImplementedException();
    }

    public void GameStart()
    {
        playerOne = GameObject.Find("Player One").GetComponent<connect4AI>();
        playerTwo = GameObject.Find("Player Two").GetComponent<connect4AI>();
        spawner = GameObject.Find("Spawner").GetComponent<SpawnScript>();
        playerOne.SetTeam(1);
        playerTwo.SetTeam(2);
        GameObject.Find("Start").SetActive(false);
        TurnButton.SetActive(true);
    }

    public int[,] GetBoard()
    {
        throw new NotImplementedException();
    }

    public void NextTurn()
    {
        int place = -1;
        board = spawner.GetBoardAsArray();
        if(currentTurn == 1)
        {
           place =  playerOne.Place(board);
        }
        else
        {
           place =  playerTwo.Place(board);
        }
        spawner.InstantiateSpehre(currentTurn, place);
        if (currentTurn == 1) currentTurn = 2;
        else currentTurn = 1;
    }
}
