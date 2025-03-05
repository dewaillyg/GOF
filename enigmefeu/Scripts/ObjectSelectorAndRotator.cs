using UnityEngine;

public class ObjectSelectorAndRotator : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject selectedObject;
    private bool isObjectSelected = false;

    public float rotationSpeed = 10f;
    private bool isPlayerFrozen = false;

    

    private PlayerMovement playerMovement;

    void Start()
    {
        mainCamera = Camera.main;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isObjectSelected)
            {
                DeselectObject();
            }
            else
            {
                TrySelectObject();
            }
        }

        if (isObjectSelected && selectedObject != null)
        {
            RotateSelectedObject();
        }
    }

    void TrySelectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Mirror"))
            {
                selectedObject = hitObject;
                isObjectSelected = true;
                FreezePlayer();

                Rigidbody rb = selectedObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
            }
        }
    }

    void DeselectObject()
    {
        isObjectSelected = false;
        UnfreezePlayer();

        Rigidbody rb = selectedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
        selectedObject = null;
    }

    void RotateSelectedObject()
    {
        float rotationY = 0;
        if (Input.GetKey(KeyCode.A)) rotationY = rotationSpeed * Time.deltaTime/3;
        if (Input.GetKey(KeyCode.D)) rotationY = -rotationSpeed * Time.deltaTime/3;

        float rotationX = 0;
        if (Input.GetKey(KeyCode.W)) rotationX = rotationSpeed * Time.deltaTime/3;
        if (Input.GetKey(KeyCode.S)) rotationX = -rotationSpeed * Time.deltaTime/3;

        selectedObject.transform.Rotate(rotationX, rotationY, 0, Space.World);

        
    }

    void FreezePlayer()
    {
        if (playerMovement != null && !isPlayerFrozen)
        {
            playerMovement.enabled = false;
            isPlayerFrozen = true;
        }
    }

    void UnfreezePlayer()
    {
        if (playerMovement != null && isPlayerFrozen)
        {
            playerMovement.enabled = true;
            isPlayerFrozen = false;
        }
    }

    void OnGUI()
    {
        if (isObjectSelected)
        {
            string message = "Q : Déplacer à gauche";
            string message2 = "Z : Déplacer en haut";
            string message3 = "D : Déplacer à droite";
            string message4 = "S : Déplacer en bas ";

            GUI.Label(new Rect(10, 100, 400, 20), message2);
            GUI.Label(new Rect(10, 80, 400, 20), message);
            GUI.Label(new Rect(10, 140, 400, 20), message4);
            GUI.Label(new Rect(10, 120, 400, 20), message3);
        }
    }
}
