using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    private bool isOpen = false;
    private bool isLocked = true;
    private int requiredPoints = 8;
    private bool requiresBook = true;
    private AudioClip openDoorSFX;
    

    public void Interact(PlayerBehaviour player)
    {
        if (isOpen)
        {
            Vector3 doorRotation = transform.eulerAngles;
            doorRotation.y -= 90f;
            transform.eulerAngles = doorRotation;
            isOpen = false;
            Debug.Log("Door closed.");
        }
        else
        {
            bool canOpen = !isLocked || (player.points >= requiredPoints && (!requiresBook || player.hasBook));

            if (canOpen)
            {
                Vector3 doorRotation = transform.eulerAngles;
                doorRotation.y += 90f;
                transform.eulerAngles = doorRotation;
                isOpen = true;
                isLocked = false; // Unlock the door after opening
                Debug.Log("Door opened.");

                AudioSource.PlayClipAtPoint(openDoorSFX, transform.position);
            }
            else
            {
                Debug.Log("Door is locked.");
                if (player.points < requiredPoints)
                {
                    Debug.Log($"Need {requiredPoints} points to open the door.");
                }

                if (requiresBook && !player.hasBook)
                {
                    Debug.Log("Book is required to open this door.");
                }
            }
        }
    }
}
