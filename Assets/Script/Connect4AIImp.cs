using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect4AIImp : MonoBehaviour, connect4AI
{
    private int myTeam;

    public int Place(int[,] board)
    {
        if (board[3, 3] == 0) return 3;
        System.Random r = new System.Random();
        int rtn = r.Next(7);
        while (board[rtn, board.GetLength(1)-1]!=0) rtn = r.Next(7);
        return rtn;
    }

    public void SetTeam(int teamNumber)
    {
        myTeam = teamNumber;
    }
}
