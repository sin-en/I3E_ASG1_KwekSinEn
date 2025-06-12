using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int points = 0;
    public bool hasBook = false;
    public Transform respawnPoint;
    public int maxHealth = 100;
    public int currentHealth = 100;
    public Camera MainCamera;
    

    bool canInteract = false;
    CrystalBehaviour currentCrystal = null;
    DoorBehaviour currentDoor = null;

    void Update()
    {
        RaycastHit hitinfo;
        
        Debug.DrawRay(MainCamera.transform.position, MainCamera.transform.forward * 5f, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hitinfo, 5f))
        {
            Debug.Log("Hit: " + hitinfo.collider.gameObject.name);
            if (hitinfo.collider.CompareTag("Collectible"))
            {
                if (currentCrystal != null)
                {
                    currentCrystal.Unhighlight();
                }
                canInteract = true;
                currentCrystal = hitinfo.collider.GetComponent<CrystalBehaviour>();
                currentCrystal.Highlight();
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
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
            else if (other.CompareTag("Door"))
            {
                canInteract = true;
                currentDoor = other.GetComponent<DoorBehaviour>();
            }
            else if (other.CompareTag("Hazard"))
            {
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

        if (other.CompareTag("Door") && currentDoor != null)
        {
            canInteract = false;
            currentDoor = null;
        }
    }

    void OnInteract()
    {
        if (!canInteract) return;

        if (currentCrystal != null)
        {
            Debug.Log("Collecting crystal...");
            currentCrystal.Collect(this);
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
        currentHealth = maxHealth;
        transform.position = respawnPoint.position;
        Debug.Log("Player respawned at: " + respawnPoint.position);
    }
    
    public void ModifyScore(int amt)
    {
        points += amt;
        Debug.Log("Points: " + points);
    }

    public void ModifyHealth(int amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
        }
    }
}