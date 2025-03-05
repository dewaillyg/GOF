using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public AudioClip noteClip;
    public float maxInteractionDistance = 2f;
    public GameObject notePrefab;
    public float minTimeBetweenNotes = 1f;

    private AudioSource audioSource;
    private static float lastNotePlayed;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (noteClip != null)
        {
            audioSource.clip = noteClip;
        }
    }

    void OnMouseDown()
    {
        if (!GameManagerAir.Instance.CanInteract() || Time.time - lastNotePlayed < minTimeBetweenNotes)
        {
            return;
        }

        if (Vector3.Distance(Camera.main.transform.position, transform.position) <= maxInteractionDistance)
        {
            lastNotePlayed = Time.time;

            if (audioSource != null)
            {
                audioSource.Play();
            }

            if (notePrefab != null)
            {
                Vector3 spawnPosition = transform.position + Vector3.up * 1f;
                Instantiate(notePrefab, spawnPosition, Quaternion.Euler(0, Random.Range(0, 360), 0));
            }

            GameManagerAir.Instance.CheckNote(noteClip);
        }
    }
}
