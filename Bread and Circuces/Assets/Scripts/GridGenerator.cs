using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject gridObject;
    public int gridSizeX = 10;
    public int gridSizeY = 10;

    private float dx = 0.86f;
    private float dy = 0.74f;

    void Start()
    {
        var spawnStartPosition = gridObject.transform.position;
        for(int i = 0; i < gridSizeX; ++i)
        {
            for(int j = 0; j < gridSizeY; ++j)
            {
                var xcomponent = dx * i;
                if(j % 2 != 0) //continue;
                    xcomponent = (dx * i) - 0.43f;

                var ycomponent = -dy * j;

                Instantiate(gridObject, spawnStartPosition 
                                        + new Vector3(xcomponent, ycomponent, 0), gridObject.transform.rotation);
            }
        }
    }

    void Update()
    {
        
    }
}
