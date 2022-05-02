using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileState
{
    Occupied,
    Free, 
    Chosen
}

public class HexTile : MonoBehaviour
{
    public TileState currentState;
    public int gridX;
    public int gridY;

    private void Update()
    {
        if (transform.childCount == 0)
            currentState = TileState.Free;
        else currentState = TileState.Occupied;
    }
}
