using UnityEngine;

public class MainObject : MonoBehaviour
{
    public float maxInteractionDistance = 5f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        if (Vector3.Distance(Camera.main.transform.position, transform.position) <= maxInteractionDistance)
        {
            GameManagerAir.Instance.StartMainSequence();
        }
        else
        {
            Debug.Log("Trop loin pour interagir !");
        }
    }
}
