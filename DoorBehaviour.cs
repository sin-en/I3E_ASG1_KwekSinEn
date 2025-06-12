using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    private bool isOpen = false;

    public void Interact(PlayerBehaviour player)
    {
        if (isOpen)
        {
            Debug.Log("Closing door...");
            Vector3 doorRotation = transform.rotation.eulerAngles;
            doorRotation.y -= 90f;
            transform.eulerAngles = doorRotation;
            isOpen = false;
        }
        else if (player.points >= 8 && player.hasBook)
        {
            Debug.Log("Opening door...");
            Vector3 doorRotation = transform.rotation.eulerAngles;
            doorRotation.y += 90f;
            transform.eulerAngles = doorRotation;
            isOpen = true;
        }
        else
        {
            Debug.Log("You need at least 8 points and the book to open the door.");
        }
    }
}
