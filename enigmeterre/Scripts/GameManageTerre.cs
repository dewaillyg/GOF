using UnityEngine;
using TMPro;

public class GameManagerTerre : MonoBehaviour
{
    public static GameManagerTerre Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshPro hintText3D;
    public GameObject[] targetPrefabs;
    public Transform[] spawnPoints;
    
    public GameObject rewardPrefab;
    public Transform rewardSpawnPoint;
    
    public GameObject rune; 

    private int[] targetOrder;
    private int currentScore = 0;
    private int currentTargetIndex = 0;
    private GameObject currentTarget;
    private GameObject[] spawnedTargets;
    private int totalTargetsToHit;

    private string[] hintMessages =
    {
        "Trouvez la cible près du grand rocher.",
        "Cherchez une cible proche de l'arbre tordu.",
        "Avancez vers la cible près du petit pont.",
        "Regardez autour de la fontaine.",
        "Une cible est cachée près du mur en ruine.",
        "La cible suivante est près du banc en bois.",
        "Vous la trouverez proche de la cascade.",
        "Cherchez à côté du vieux lampadaire.",
        "Regarder en hauteur.",
        "La dernière cible est près de l'entrée principale."
    };

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        targetOrder = new int[targetPrefabs.Length];
        for (int i = 0; i < targetOrder.Length; i++)
        {
            targetOrder[i] = i;
        }

        totalTargetsToHit = targetPrefabs.Length;
        spawnedTargets = new GameObject[spawnPoints.Length];
        SpawnNewTarget();
        UpdateScoreUI();
        UpdateHintText3D();
        
        // Cache la rune au début
        if (rune != null)
        {
            rune.SetActive(false);
        }
    }

    public void TargetHit(int targetId)
    {
        if (targetId == currentTargetIndex)
        {
            Debug.Log("Bonne cible touchée !");
            currentScore++;
            currentTargetIndex++;

            if (currentScore == totalTargetsToHit)
            {
                Debug.Log("Bravo ! Toutes les cibles ont été touchées !");
                if (currentTarget != null)
                {
                    Destroy(currentTarget);
                    currentTarget = null;
                }
                ShowReward();
                UpdateHintText3D("Félicitations ! Vous avez terminé.");
                
                // Rendre la rune visible
                if (rune != null)
                {
                    rune.SetActive(true);
                    Debug.Log("Rune activée : " + rune.activeSelf);
                }
                else
                {
                    Debug.LogError("La référence à la rune est NULL !");
                }
             }
            else
            {
                SpawnNewTarget();
                UpdateHintText3D();
            }
        }
        else
        {
            Debug.Log("Mauvaise cible touchée. Réinitialisation du jeu !");
            ResetGame();
        }

        UpdateScoreUI();
    }

    void ShowReward()
    {
       void ShowReward()
{
    Debug.Log("ShowReward() appelé");

    // Vérifier si la rune existe déjà
    if (rune != null)
    {
        rune.SetActive(true);
        Debug.Log("Rune activée.");
        return; // On quitte la fonction pour éviter un doublon
    }

    if (rewardPrefab != null && rewardSpawnPoint != null)
    {
        rune = Instantiate(rewardPrefab, rewardSpawnPoint.position, Quaternion.identity);
        Debug.Log("Rune instanciée !");
    }
    else
    {
        Debug.LogError("La récompense ou le point de spawn n'est pas configuré !");
    }
}

    }

    void SpawnNewTarget()
    {
        if (currentTarget != null)
        {
            Destroy(currentTarget);
        }

        if (currentTargetIndex < totalTargetsToHit)
        {
            Transform spawnPoint = spawnPoints[currentTargetIndex];
            if (spawnedTargets[currentTargetIndex] == null)
            {
                GameObject target = Instantiate(targetPrefabs[currentTargetIndex], spawnPoint.position, Quaternion.identity);
                spawnedTargets[currentTargetIndex] = target;
                target.GetComponent<Target>().targetId = currentTargetIndex;
                currentTarget = target;
            }
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score : " + currentScore + " / " + totalTargetsToHit;
        }
    }

    void UpdateHintText3D(string customMessage = "")
    {
        if (hintText3D != null)
        {
            if (string.IsNullOrEmpty(customMessage))
            {
                hintText3D.text = "Indice : " + hintMessages[currentTargetIndex];
            }
            else
            {
                hintText3D.text = customMessage;
            }
        }
    }

    public void ResetGame()
    {
        Debug.Log("Réinitialisation du jeu !");
        currentScore = 0;
        currentTargetIndex = 0;

        foreach (GameObject target in spawnedTargets)
        {
            if (target != null)
            {
                Destroy(target);
            }
        }

        for (int i = 0; i < spawnedTargets.Length; i++)
        {
            spawnedTargets[i] = null;
        }

        SpawnNewTarget();
        UpdateScoreUI();
        UpdateHintText3D();
        
        // Cacher la rune à nouveau si le jeu est reset
        if (rune != null)
        {
            rune.SetActive(false);
        }
    }
}
