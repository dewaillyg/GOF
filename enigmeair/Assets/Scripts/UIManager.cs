using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TMP_Text messageTMPText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowMessage(string message)
    {
        if (messageTMPText != null)
            messageTMPText.text = message;
    }

    public void HideMessage()
    {
        if (messageTMPText != null)
            messageTMPText.text = "";
    }
}
