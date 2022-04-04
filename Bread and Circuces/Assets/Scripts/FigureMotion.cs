using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureMotion : MonoBehaviour
{
    private float posX;
    private float posY;
    private bool activated;
    
    public GameObject baseObject;

    void Start()
    {
        activated = false;
        posX = baseObject.transform.position.x;
        posY = baseObject.transform.position.y;
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

    void OnMouseDown()
    {
        activated = !activated;
        if(activated)
            activateFigure();
        else
            deactivateFigure();
    }

    //called when figure being activated
    void activateFigure(){
        var figureRenderer = gameObject.GetComponent<Renderer>();
        figureRenderer.material.SetColor("_Color", Color.yellow);

        var baseObjectRenderer = baseObject.transform.Find("HexBase").gameObject.GetComponent<Renderer>();
        baseObjectRenderer.material.SetColor("_Color", Color.green);
    }

    //called when figure being deactivated
    void deactivateFigure(){
        var figureRenderer = gameObject.GetComponent<Renderer>();
        figureRenderer.material.SetColor("_Color", Color.white);

        var baseObjectRenderer = baseObject.transform.Find("HexBase").gameObject.GetComponent<Renderer>();
        baseObjectRenderer.material.SetColor("_Color", Color.white);
    }

    

}
