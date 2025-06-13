using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool isOpen = false;
    [SerializeField]
    private bool isLocked = true;
    [SerializeField]
    private float openAngle = 90f;
    [SerializeField]
    private int requiredPoints = 8;
    [SerializeField]
    private bool requiresBook = true;
    [SerializeField]
    private float animationSpeed = 2f;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isAnimating = false;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
    }

    void Update()
    {
        // Smooth door animation
        if (isAnimating)
        {
            Quaternion targetRotation = isOpen ? openRotation : closedRotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, animationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                transform.rotation = targetRotation;
                isAnimating = false;
            }
        }
    }

    public void Interact(PlayerBehaviour player)
    {
        if (isAnimating) return; // Prevent interaction during animation

        if (isOpen)
        {
            Debug.Log("Closing door...");
            isOpen = false;
            isAnimating = true;
        }
        else
        {
            bool canOpen = !isLocked || (player.points >= requiredPoints && (!requiresBook || player.hasBook));
            if (canOpen)
            {
                Debug.Log("Opening door...");
                isOpen = true;
                isAnimating = true;
                if (isLocked)
                {
                    isLocked = false;
                    Debug.Log("Door unlocked!");
                }
            }
            else
            {
                string message = "Door is locked! ";
                if (player.points < requiredPoints) 
                    message += $"Need {requiredPoints - player.points} more points. ";
                if (requiresBook && !player.hasBook) 
                    message += "Need the book.";
                Debug.Log(message);
            }
        }
    }
}