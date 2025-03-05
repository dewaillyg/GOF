using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject waterProjectilePrefab;
    public Transform projectileSpawnPoint;
    public Camera playerCamera;
    public float projectileSpeed = 10f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        if (waterProjectilePrefab != null && projectileSpawnPoint != null && playerCamera != null)
        {
            GameObject projectile = Instantiate(
                waterProjectilePrefab,
                projectileSpawnPoint.position,
                projectileSpawnPoint.rotation
            );

            projectile.tag = "Projectile";

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 shootDirection = playerCamera.transform.forward;
                rb.velocity = shootDirection * projectileSpeed;
            }
        }
        else
        {
            if (waterProjectilePrefab == null)
            {
                Debug.LogWarning("Le prefab du projectile n'est pas assigné !");
            }
            if (projectileSpawnPoint == null)
            {
                Debug.LogWarning("Le point de spawn n'est pas assigné !");
            }
            if (playerCamera == null)
            {
                Debug.LogWarning("La caméra du joueur n'est pas assignée !");
            }
        }
    }
}
