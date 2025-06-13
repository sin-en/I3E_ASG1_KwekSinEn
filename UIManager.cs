using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI collectiblesText;
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private GameObject congratulationsPanel;
    [SerializeField] private TextMeshProUGUI congratulationsText;
    [SerializeField] private AudioClip congratulationsSFX;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
        if (congratulationsPanel != null)
            congratulationsPanel.SetActive(false);

        if (interactionText != null)
            interactionText.gameObject.SetActive(false);
            
        UpdateScore(0);
        UpdateCollectibles(0, 8);
    }

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

        if (congratulationsSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(congratulationsSFX);
        }

        Debug.Log("All collectibles collected! Congratulations!");
    }
}