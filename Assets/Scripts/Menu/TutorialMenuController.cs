using UnityEngine;
using UnityEngine.UI;

public class TutorialMenuController : MonoBehaviour
{
    [SerializeField] private GameObject storyTab;
    [SerializeField] private GameObject revolverControlsTab;
    [SerializeField] private GameObject generalControlsTab;
    [SerializeField] private Button storyButton;
    [SerializeField] private Button revolverControlsButton;
    [SerializeField] private Button generalControlsButton;
    [SerializeField] private Button hideButton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSound;

    private void Start()
    {
        storyButton.onClick.AddListener(ShowStoryTab);
        revolverControlsButton.onClick.AddListener(ShowRevolverControlsTab);
        generalControlsButton.onClick.AddListener(ShowGeneralControlsTab);
        hideButton.onClick.AddListener(HideMenuPanel);

        gameObject.SetActive(false);
    }

    public void HideMenuPanel()
    {
        audioSource.PlayOneShot(buttonClickSound);
        gameObject.SetActive(false);
    }

    private void ShowStoryTab()
    {
        audioSource.PlayOneShot(buttonClickSound);
        storyTab.SetActive(true);
        revolverControlsTab.SetActive(false);
        generalControlsTab.SetActive(false);
    }

    private void ShowRevolverControlsTab()
    {
        audioSource.PlayOneShot(buttonClickSound);
        storyTab.SetActive(false);
        revolverControlsTab.SetActive(true);
        generalControlsTab.SetActive(false);
    }

    private void ShowGeneralControlsTab()
    {
        audioSource.PlayOneShot(buttonClickSound);
        storyTab.SetActive(false);
        revolverControlsTab.SetActive(false);
        generalControlsTab.SetActive(true);
    }
}