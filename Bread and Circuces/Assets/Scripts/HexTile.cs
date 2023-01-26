using UnityEngine;


public class HexTile : MonoBehaviour
{
    public int gridX;
    public int gridY;
    public bool isOccupied;
    public bool isChosen;

    private void Start()
    {
        isChosen = false;
        isOccupied = false;
    }

    private void Update()
    {
        if (gameObject.transform.childCount == 0)
            isOccupied = false;
        else
        {
            isOccupied = true;
            var child = gameObject.transform.GetChild(0);
            if (child.transform.position == transform.position)
                child.transform.position += child.GetComponent<UnitInfo>().offset;
        }
    }
}
