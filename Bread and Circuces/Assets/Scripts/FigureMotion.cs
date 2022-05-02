using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureMotion : MonoBehaviour
{
    private float posX;
    private float posY;
    private bool activated;
    
    public GameObject baseObject;
    public int moveDistance;

    void Start()
    {
        baseObject = transform.parent.gameObject;
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
        transform.parent = baseObject.transform;
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
        var figureRenderer = gameObject.GetComponent<SpriteRenderer>();
        figureRenderer.material.SetColor("_Color", Color.yellow);

        var tiles = FindObjectOfType<Board>().GetTilesInRadius(transform.parent.GetComponent<HexTile>(), moveDistance);
        
        foreach(var tile in tiles)
        {
            var tileRenderer = tile.gameObject.GetComponent<SpriteRenderer>();
            tileRenderer.material.SetColor("_Color", Color.green);
        }
        //var baseObjectRenderer = baseObject.transform.gameObject.GetComponent<SpriteRenderer>();
        //baseObjectRenderer.material.SetColor("_Color", Color.green);
    }

    //called when figure being deactivated
    void deactivateFigure(){
        var figureRenderer = gameObject.GetComponent<SpriteRenderer>();
        figureRenderer.material.SetColor("_Color", Color.white);

        var tiles = FindObjectOfType<Board>().GetTilesInRadius(transform.parent.GetComponent<HexTile>(), moveDistance);

        foreach (var tile in tiles)
        {
            var tileRenderer = tile.gameObject.GetComponent<SpriteRenderer>();
            tileRenderer.material.SetColor("_Color", Color.white);
        }
        //var baseObjectRenderer = baseObject.transform.gameObject.GetComponent<SpriteRenderer>();
        //baseObjectRenderer.material.SetColor("_Color", Color.white);
    }

    

}
