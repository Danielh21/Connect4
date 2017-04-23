using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnScript : MonoBehaviour {

    public Transform prefab;
    public SpawnPositions[,] Sphere2dArray;
    public static bool GameWon = false;
    private int width;
    private bool spawned=false;

    // Use this for initialization
    void Start () {
        EventCanvasManager.OnClicked += InstantiateSpehre;
        //CreateSpawnAblePositions(int x);
        
	}

    public bool isSpawned()
    {
        return spawned;
    }

    public void CreateSpawnAblePositions(int w)
    {
        /*The x Value goes should go from 0 - 6 and the
        *The z goes from 0 -5 The values are created in another Script!
        *
        **/
        width = w;
        float basex = -60;
        float basez = 5;
        Sphere2dArray = new SpawnPositions[w, 6];
        for (int x = 0; x < w; x++)
        {
            for (int z = 0;z < 6; z++)
            {
                Vector3 vector = new Vector3(basex + (20 *x) , 1.0F ,basez + (10*z));
                SpawnPositions spawnPosition = new SpawnPositions(x, z, vector);
                Sphere2dArray[x, z] = spawnPosition;
            }
        }
        spawned = true;

    }

    internal int[,] GetBoardAsArray()
    {
        // Returns the Sphere2dArray with 0 as Player.None, 1 as  Player.P1, and 2 as Player.P2
        int[,] array = new int[width, 6];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < 6; z++)
            {
                Player p = Sphere2dArray[x, z].getPlayerThatControllsField();
                int pos = 0;
                if (p == Player.P1) pos = 1;
                if (p == Player.P2) pos = 2;
                array[x, z] = pos;
            }

        }

        return array;
    }

    private int getSpawnPositionForRow(int row)
    {

        for (int i = 0; i < 6; i++)
        {
            if(Sphere2dArray[row,i].getPlayerThatControllsField() == Player.None)
            {
                return i;
            }
        }
        throw new Exception("Out of Bounds");
    }


    public void InstantiateSpehre(int playerNumber, int row)
    {
        int collum = getSpawnPositionForRow(row);

        if (playerNumber == 1) Sphere2dArray[row, collum].setPlayerOneControllsField();
        if (playerNumber == 2) Sphere2dArray[row, collum].setPlayerTwoControllsField();

        Transform  sphere = Instantiate(prefab, Sphere2dArray[row,collum].GetVector(), Quaternion.identity);
        Color sphereColor = Color.red;
        if (playerNumber == 1)
        {
            sphereColor = Color.yellow;
        }
        sphere.GetComponent<MeshRenderer>().material.SetColor("_Color", sphereColor);

        Player p;
        if (playerNumber == 2) p = Player.P2;
        else p = Player.P1;

       if(HasPlayerWon(row, collum, p))
        {
            
            Debug.Log("Player: " + playerNumber + " Has Won!");
            SpawnScript.GameWon = true;
        }


    }

    private bool HasPlayerWon(int row, int collum, Player player)
    {
        if (CheckFirstDirecton(row, collum, player, 0, 1)) return true; // Checks Veritcal
        if (CheckFirstDirecton(row, collum, player, 1, 0)) return true; // Checks Horizontal
        if (CheckFirstDirecton(row, collum, player, 1, 1)) return true; // Checks Obliquely Right Up and Left Down
        if (CheckFirstDirecton(row, collum, player, 1, -1)) return true; // Checks Obliquely Left Up and Right Down 
        else return false;
    }

    private bool CheckFirstDirecton(int row, int collum, Player player, int horizontal, int vertical)
    {
        int count = 0;
        try
        {
            if (Sphere2dArray[row + horizontal, collum + vertical].getPlayerThatControllsField() == player) count++;
            else
            {
                return CheckSecondDirection(row, collum, player, horizontal, vertical, count);
            }

            if (Sphere2dArray[row + (horizontal * 2), collum + (vertical * 2)].getPlayerThatControllsField() == player) count++;
            else
            {
                return CheckSecondDirection(row, collum, player, horizontal, vertical, count);
            }

            {
                if (Sphere2dArray[row + (horizontal * 3), collum + (vertical * 3)].getPlayerThatControllsField() == player) count++;
                else
                {
                    return CheckSecondDirection(row, collum, player, horizontal, vertical, count);
                }
            }
        }
        catch (IndexOutOfRangeException e)
        {
            return CheckSecondDirection(row, collum, player, horizontal, vertical, count);
        }

        return count >= 3;
    }

    private bool CheckSecondDirection(int row, int collum, Player player, int horizontal, int vertical, int count)
    {
        try
        {
            if (Sphere2dArray[row - horizontal, collum - vertical].getPlayerThatControllsField() == player) count++;
            else
            {
                return count >= 3;
            }

            if (Sphere2dArray[row - (horizontal * 2), collum - (vertical * 2)].getPlayerThatControllsField() == player) count++;
            else
            {
                return count >= 3;
            }

            {
                if (Sphere2dArray[row - (horizontal * 3), collum - (vertical * 3)].getPlayerThatControllsField() == player) count++;
                else
                {
                    return count >= 3;
                }
            }
        }
        catch (IndexOutOfRangeException e)
        {
            return count >= 3;
        }

        return count >= 3;
    }
    
}
