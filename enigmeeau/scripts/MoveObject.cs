using UnityEngine;

public class DestroyOnKey : MonoBehaviour
{
    public float activationDistance = 3f; // Distance d'activation
    public Transform player; // Référence au joueur

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning(gameObject.name + " : Aucun joueur assigné !");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Debug.Log(gameObject.name + " : Distance au joueur = " + distanceToPlayer);

        if (distanceToPlayer <= activationDistance && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(gameObject.name + " : Mur détruit !");
            Destroy(gameObject);
        }
    }
}
