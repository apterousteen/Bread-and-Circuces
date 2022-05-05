using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum TileState
//{
//    Occupied,
//    Free, 
//    Chosen
//}

public class HexTile : MonoBehaviour
{
    //public TileState currentState;
    public int gridX;
    public int gridY;
    public bool isOccupied;
    public bool isChosen;

    private void Start()
    {
        //currentState = TileState.Free;
        isChosen = false;
        isOccupied = false;
    }

    private void Update()
    {
        if (gameObject.transform.childCount == 0)
            isOccupied = false;
        else isOccupied = true;
    }
}
