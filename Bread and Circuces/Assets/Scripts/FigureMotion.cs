using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureMotion : MonoBehaviour
{
    private float posX;
    private float posY;
    
    public GameObject baseObject;

    void Start()
    {
    }

    void Update()
    {
        MoveFigureOnObject();
    }

    void MoveFigureOnObject()
    {
        posX = baseObject.transform.position.x;
        posY = baseObject.transform.position.y;
        moveObject();
    }

    void moveObject(){
        /*animation and shit*/
        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
