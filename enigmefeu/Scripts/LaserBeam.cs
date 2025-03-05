using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    Vector3 pos, dir;
    GameObject laserObj;
    LineRenderer laser;
    List<Vector3> laserIndices = new List<Vector3>();
    BoxCollider laserCollider;

    private bool hasHitTarget = false; 
    private bool isFrozen = false; 

    public LaserBeam(Vector3 pos, Vector3 dir, Material material)
    {
        this.laserObj = new GameObject();
        this.laserObj.name = "Laser Beam";
        this.laserObj.tag = "Laser";
        this.laser = this.laserObj.AddComponent<LineRenderer>();

        this.pos = pos;
        this.dir = dir;

        laser.startWidth = 0.1f;
        laser.endWidth = 0.1f;
        laser.material = material;
        laser.startColor = Color.blue;
        laser.endColor = Color.red;

        laserCollider = this.laserObj.AddComponent<BoxCollider>();
        laserCollider.isTrigger = true;

        CastRay(pos, dir);
    }

    public void UpdateLaserBeam(Vector3 pos, Vector3 dir)
    {
        
        if (isFrozen) return;

        this.pos = pos;
        this.dir = dir;

        laserIndices.Clear();  
        CastRay(pos, dir);      
    }

   void CastRay(Vector3 pos, Vector3 dir)
{
    laserIndices.Add(pos);
    
    Ray ray = new Ray(pos, dir);
    RaycastHit hit;
    
    while (true) 
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1))
        {
            CheckHit(hit, dir);
            break; 
        }
        else
        {
            laserIndices.Add(ray.GetPoint(1000)); 
            UpdateLaser();
            break; 
        }
    }
}


    void UpdateLaser()
    {
        laser.positionCount = laserIndices.Count;

        for (int i = 0; i < laserIndices.Count; i++)
        {
            laser.SetPosition(i, laserIndices[i]);
        }
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction)
    {
        
        if (hitInfo.collider.gameObject.tag == "Mirror")
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
            CastRay(pos, dir);
        }
        
        else if (hitInfo.collider.gameObject.tag == "Target" && !hasHitTarget)
        {
            hasHitTarget = true; 
            CylinderTarget target = hitInfo.collider.GetComponent<CylinderTarget>();
            if (target != null)
            {
                target.RegisterLaserHit();  
            }

          
            laserIndices.Add(hitInfo.point);  
            UpdateLaser();
            isFrozen = true; 
        }
        else
        {
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
    }
}
