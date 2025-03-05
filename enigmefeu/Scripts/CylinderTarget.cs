using UnityEngine;

public class CylinderTarget : MonoBehaviour
{
    public Color targetColor = Color.green; 
    private Renderer rend;
    private bool isTargetHit = false;

    private int laserHitCount = 0; 
    public int requiredLaserHits = 4; 
    public ElevatorPlatform platform; 

    void Start()
    {
        rend = GetComponent<Renderer>(); 
    }

    public void RegisterLaserHit()
{
    if (!isTargetHit)
    {
        laserHitCount++; 

        Debug.Log("Laser touché ! Compteur : " + laserHitCount);  

        if (laserHitCount >= requiredLaserHits)
        {
            isTargetHit = true; 
            ChangeColor();
            platform?.ActivatePlatform(); 
        }
    }
}


    private void ChangeColor()
    {
        rend.material.color = targetColor;
        Debug.Log("Cylindre activé par " + laserHitCount + " lasers.");
    }

    
}
