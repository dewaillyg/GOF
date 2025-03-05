using System.Collections.Generic;
using UnityEngine;

public class GameManagerAir : MonoBehaviour
{
    public static GameManagerAir Instance { get; private set; }

    [Header("Sequence Settings")]
    public List<AudioClip> melodySequence;
    public float delayBetweenNotes = 1f;

    [Header("Rune Settings")]
    public GameObject runeObject;

    private List<AudioClip> playerSequence = new List<AudioClip>();
    private bool isPlayingMainSequence = false;
    private bool canPlayerPlay = false;
    private AudioSource audioSource;
    [SerializeField] private bool hasStarted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (runeObject != null)
        {
            runeObject.SetActive(false);
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public bool CanInteract()
    {
        return canPlayerPlay && !isPlayingMainSequence;
    }

    public void StartMainSequence()
    {
        if (hasStarted) return;

        if (!isPlayingMainSequence)
        {
            isPlayingMainSequence = true;
            canPlayerPlay = false;
            playerSequence.Clear();
            StartCoroutine(PlayMainSequence());
            hasStarted = true;
        }
    }

    private System.Collections.IEnumerator PlayMainSequence()
    {
        UIManager.Instance.ShowMessage("Ecoutez la sequence...");

        foreach (AudioClip note in melodySequence)
        {
            audioSource.clip = note;
            audioSource.Play();
            yield return new WaitForSeconds(note.length + delayBetweenNotes);
        }

        isPlayingMainSequence = false;
        canPlayerPlay = true;
        UIManager.Instance.ShowMessage("À vous de jouer !");
    }

    public void CheckNote(AudioClip playedNote)
    {
        if (!canPlayerPlay) return;

        if (playerSequence.Count >= melodySequence.Count)
        {
            ResetSequence("Trop de notes ! Recommencez.");
            return;
        }

        playerSequence.Add(playedNote);
        int currentIndex = playerSequence.Count - 1;

        if (playerSequence[currentIndex] != melodySequence[currentIndex])
        {
            ResetSequence("Incorrect ! Reecoutez la sequence.");
            return;
        }

        if (playerSequence.Count == melodySequence.Count)
        {
            UIManager.Instance.ShowMessage("Bravo ! Vous avez reussi !");
            playerSequence.Clear();
            canPlayerPlay = false;
            ActivateRune();
        }
        else
        {
            UIManager.Instance.ShowMessage($"Correct ! Note {playerSequence.Count}/{melodySequence.Count}");
        }
    }

    private void ResetSequence(string message)
    {
        UIManager.Instance.ShowMessage(message);
        playerSequence.Clear();
        StartCoroutine(ResetAfterDelay(2f));
    }

    private System.Collections.IEnumerator ResetAfterDelay(float delay)
    {
        canPlayerPlay = false;
        yield return new WaitForSeconds(delay);
        hasStarted = false;
        StartMainSequence();
    }

    private void ActivateRune()
    {
        if (runeObject != null)
        {
            runeObject.SetActive(true);
        }
    }
}
