using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface connect4CTRL
{
    bool CheckGameState();
    void NextTurn();
    void GameStart();
    int[,] GetBoard();
}