using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect4AIImp : MonoBehaviour, connect4AI
{
    private int myTeam;

    public int Place(int[,] board)
    {
        System.Random r = new System.Random();
        return r.Next(7);
    }

    public void SetTeam(int teamNumber)
    {
        myTeam = teamNumber;
    }
}
