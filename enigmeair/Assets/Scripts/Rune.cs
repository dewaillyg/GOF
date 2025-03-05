using UnityEngine;

public class Rune : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            CollectRune();
        }
    }

    private void CollectRune()
    {
        Destroy(gameObject);
    }
}
