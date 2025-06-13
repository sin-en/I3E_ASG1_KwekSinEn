using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI collectiblesText;
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private GameObject congratulationsPanel;
    [SerializeField] private TextMeshProUGUI congratulationsText;
    [SerializeField] private AudioClip congratulationsSFX;



    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public void UpdateCollectibles(int collected, int total)
    {
        if (collectiblesText != null)
        {
            collectiblesText.text = $"Collectibles: {collected}/{total} (Remaining: {total - collected})";
        }
    }

    public void ShowInteractionPrompt(bool show, string message = "Press E to interact")
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(show);
            if (show)
            {
                interactionText.text = message;
            }
        }
    }

    public void ShowCongratulations()
    {
        if (congratulationsPanel != null)
        {
            congratulationsPanel.SetActive(true);
        }

        if (congratulationsText != null)
        {
            congratulationsText.text = "Congratulations!\nYou collected all items!";
        }

        if (congratulationsSFX != null)
        {
            AudioSource.PlayClipAtPoint(congratulationsSFX, Camera.main.transform.position);
        }

        Debug.Log("All collectibles collected! Congratulations!");
    }
}
