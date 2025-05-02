using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuButtonHandler : MonoBehaviour
{
    private Button newGameButton;
    private Button continueButton;
    private Button settingsButton;
    private Button exitButton;

    private GameObject settingsMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingsMenu = GameObject.Find("SettingsMenu");
        settingsMenu.SetActive(false);

        newGameButton = GameObject.Find("NewGameButton").GetComponent<Button>();
        continueButton = GameObject.Find("ContinueGameButton").GetComponent<Button>();
        settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        exitButton.onClick.AddListener(() => Application.Quit());
        settingsButton.onClick.AddListener(() => settingsMenu.SetActive(true));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
