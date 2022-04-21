using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(checkIfObjWasClicked())
        {
            print("sss");
        }
    }

    bool checkIfObjWasClicked()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            //print(hit.transform.name);
            //print(transform.name);

            if(hit.transform.name == transform.name)
                return true;
        }
        return false;
    }
}
