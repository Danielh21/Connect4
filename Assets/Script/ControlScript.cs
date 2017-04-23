using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript : MonoBehaviour, connect4CTRL
{
    connect4AI playerOne;
    connect4AI playerTwo;
    private int currentTurn = 1;
    public static int turnCount;
    private int[,] board;
    SpawnScript spawner;
    public GameObject TurnButton;
    public GameObject AllTurnButton;

    void Start()
    {
        StartGameScirpt.ButtonClicked += GameStart;
        StartGameScirpt.Spawns += CreateSpawns;
        StartGameScirpt.NextTrun += NextTurn;
    }

    public bool CheckGameState()
    {
        return !SpawnScript.GameWon && turnCount<=41;
    }
    public void CreateSpawns(int width)
    {
        spawner = GameObject.Find("Spawner").GetComponent<SpawnScript>();
        spawner.CreateSpawnAblePositions(width);
    }
    public void GameStart()
    {
        if (!spawner.isSpawned()) CreateSpawns(7);
            playerOne = GameObject.Find("Player One").GetComponent<connect4AI>();
        playerTwo = GameObject.Find("Player Two").GetComponent<connect4AI>();
        
        
        playerOne.SetTeam(1);
        playerTwo.SetTeam(2);
        GameObject.Find("Start 7X6").SetActive(false);
        GameObject.Find("Start 6X6").SetActive(false);
        TurnButton.SetActive(true);
        AllTurnButton.SetActive(true);
    }

    public int[,] GetBoard()
    {
        return board;
    }

    public void NextTurn()
    {
        if (!CheckGameState())
        {
            print("Game is over... Stop doing stuff!");
            return;
        }
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
        turnCount++;
    }
}
