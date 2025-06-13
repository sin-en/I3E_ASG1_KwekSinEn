using UnityEngine;
using TMPro;
public class PlayerBehaviour : MonoBehaviour
{
    public int points = 0;
    public bool hasBook = false;
    public Transform respawnPoint;
    public Camera MainCamera;
    public UIManager uiManager;
    private bool canInteract = false;
    private CrystalBehaviour currentCrystal = null;
    private BookBehaviour currentBook = null;
    private DoorBehaviour currentDoor = null;
    private int collectiblesCollected = 0;
    private int totalCollectibles = 8;

    void Start()
    {
        if (uiManager == null)
        {
            uiManager = FindFirstObjectByType<UIManager>();
        }
        if (uiManager != null)
        {
            uiManager.UpdateScore(points);
            uiManager.UpdateCollectibles(collectiblesCollected, totalCollectibles);
        }
    }
    void Update()
    {
        HandleInput();
        RaycastHit hitinfo;

        Debug.DrawRay(MainCamera.transform.position, MainCamera.transform.forward * 5f, Color.red);
        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hitinfo, 5f))
        {
            Debug.Log("Hit: " + hitinfo.collider.gameObject.name);
            if (hitinfo.collider.CompareTag("Collectible"))
            {
                if (currentCrystal != null && currentCrystal != hitinfo.collider.GetComponent<CrystalBehaviour>())
                {
                    currentCrystal.Unhighlight();
                }
                canInteract = true;
                currentCrystal = hitinfo.collider.GetComponent<CrystalBehaviour>();
                currentCrystal.Highlight();
            }
            else if (hitinfo.collider.CompareTag("Book"))
            {
                currentBook = hitinfo.collider.GetComponent<BookBehaviour>();
                canInteract = true;
            }
            else if (hitinfo.collider.CompareTag("Door"))
            {
                canInteract = true;
                currentDoor = hitinfo.collider.GetComponent<DoorBehaviour>();
            }
            else
            {
                canInteract = false;
                if (currentCrystal != null)
                {
                    currentCrystal.Unhighlight();
                    currentCrystal = null;
                }
                currentBook = null;
                currentDoor = null; 
                canInteract = false;
            }
        }
        if (uiManager != null)
        {
            uiManager.ShowInteractionPrompt(canInteract);
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            OnInteract();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger with: " + other.gameObject.name);

        if (other.CompareTag("Collectible"))
        {
            canInteract = true;
            currentCrystal = other.GetComponent<CrystalBehaviour>();
        }
        else if (other.CompareTag("Book"))
        {
            canInteract = true;
            currentBook = other.GetComponent<BookBehaviour>();
        }
        else if (other.CompareTag("Door"))
        {
            canInteract = true;
            currentDoor = other.GetComponent<DoorBehaviour>();
        }
        else if (other.CompareTag("Hazard"))
        {
            Debug.Log("Player in hazard  - instant death!");
            DieAndRespawn();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Collectible") && currentCrystal != null)
        {
            canInteract = false;
            currentCrystal = null;
        }
        if (other.CompareTag("Book") && currentBook != null)
        {
            canInteract = false;
            currentBook = null;
        }
        if (other.CompareTag("Door") && currentDoor != null)
        {
            canInteract = false;
            currentDoor = null;
        }
    }

    void OnInteract()
    {

        if (currentCrystal != null)
        {
            Debug.Log("Collecting crystal...");
            currentCrystal.Collect(this);
        }
        if (currentBook != null)
        {
            Debug.Log("Collecting book...");
            currentBook.Collect(this);
            canInteract = false;
            currentBook = null;
        }
        if (currentDoor != null)
        {
            Debug.Log("Opening door...");
            currentDoor.Interact(this);
        }
    }
    void DieAndRespawn()
    {
        Debug.Log("Player died. Respawning...");
        transform.position = respawnPoint.position;
        Debug.Log("Player respawned at: " + respawnPoint.position);
    }
    public void ModifyScore(int amt)
    {
        points += amt;
        Debug.Log("Points: " + points);
        if (uiManager != null)
        {
            uiManager.UpdateScore(points);
        }
    }
    public void CollectibleCollected()
    {
        collectiblesCollected++;
        if (uiManager != null)
        {
            uiManager.UpdateCollectibles(collectiblesCollected, totalCollectibles);
        }
        if (collectiblesCollected >= totalCollectibles)
            {
                uiManager.ShowCongratulations();
            }
    }
}