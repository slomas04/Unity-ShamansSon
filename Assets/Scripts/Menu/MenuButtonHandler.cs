using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuButtonHandler : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private GameObject settingsMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingsMenu.SetActive(false);

        exitButton.onClick.AddListener(() => Application.Quit());
        settingsButton.onClick.AddListener(() => settingsMenu.SetActive(true));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
