using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface connect4AI
{
    // return: 0-5 = column to drop token
    // Param: Values in the 2D array represents: 0 = empty, 1 = player1, 2 = player2...
    int Place(int[,] board);
    void SetTeam(int teamNumber);
}