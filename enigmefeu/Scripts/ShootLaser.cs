using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public Material material;
    private LaserBeam beam;
    private bool canActivateLaser = false;
    private bool isLaserActive = false;
    public float activationDistance = 50f;

    private GameObject player;
    
    public static int activeLaserCount = 0; 
    public static int maxLaserCount = 4; 

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance <= activationDistance)
            {
                canActivateLaser = true;

                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (isLaserActive)
                    {
                        DestroyLaser();
                    }
                    else
                    {
                        ActivateLaser();
                    }
                }

                
                if (isLaserActive && beam != null)
                {
                    beam.UpdateLaserBeam(transform.position, transform.right);
                }
            }
            else
            {
                canActivateLaser = false;
            }
        }
    }

    void ActivateLaser()
    {
        if (activeLaserCount < maxLaserCount)
        {
            beam = new LaserBeam(transform.position, transform.right, material);
            isLaserActive = true;
            activeLaserCount++;
            Debug.Log("Laser activé. Nombre de lasers actifs : " + activeLaserCount);
        }
        else
        {
            Debug.Log("Nombre maximum de lasers atteint !");
        }
    }

    void DestroyLaser()
    {
        Destroy(GameObject.Find("Laser Beam"));
        isLaserActive = false;
        activeLaserCount--; 
        Debug.Log("Laser désactivé. Nombre de lasers actifs : " + activeLaserCount);
    }

    void OnGUI()
    {
        if (canActivateLaser)
        {
            string message = isLaserActive ? "E pour désactiver le laser" : "E pour activer le  laser";
            GUI.Label(new Rect(10, 40, 250, 20), message);
        }
    }
}
