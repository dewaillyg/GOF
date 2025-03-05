using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public GameObject[] emplacementPoints;  
    public GameObject[] cylinders;  

    private GameObject selectedCylinder = null;

    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Cylinder"))
                {
                    selectedCylinder = hit.transform.gameObject;
                }
                else if (hit.transform.CompareTag("Emplacement") && selectedCylinder != null)
                {
                   
                    selectedCylinder.transform.position = hit.transform.position;
                    selectedCylinder = null;
                }
            }
        }
    }
}
