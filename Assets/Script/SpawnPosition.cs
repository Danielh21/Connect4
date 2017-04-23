using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPositions : MonoBehaviour {

    /*
     * This Class holds only infomation about
     * Where Objects can spawn.
     * The x Value goes should go from 0-6 and the
     * The z goes from 0.5 The values are created in another Script!
     * 
     * */

    private int x { get; set; }
    private int z { get; set; }
    private Vector3 vector { get; set; }
    private bool occipied { get; set; }
    private Player playerThatControllsField;

    public SpawnPositions(int x, int z, Vector3 vector)
    {
        this.x = x;
        this.z = z;
        this.vector = vector;
        this.occipied = false;
        this.playerThatControllsField = Player.None;
    }



    public int getZ()
    {
        return z;
    }

    public int getX()
    {
        return x;
    }

    public bool isOccipied()
    {
        return occipied;
    }

    public void SetOccipied(bool boolean)
    {
        occipied = boolean;
    }

    public Vector3 GetVector()
    {
        return vector;
    }

    public Player getPlayerThatControllsField()
    {
        return playerThatControllsField;
    }

    public void setPlayerTwoControllsField()
    {
        playerThatControllsField = Player.P2;
    }

    public void setPlayerOneControllsField()
    {
        playerThatControllsField = Player.P1;
    }

}

public enum Player
{
    None,P1,P2,
}
