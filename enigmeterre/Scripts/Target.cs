using UnityEngine;

public class Target : MonoBehaviour
{
    public int targetId;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Cible " + targetId + " touchée par le projectile !");
            GameManagerTerre.Instance.TargetHit(targetId);
        }
    }
}
