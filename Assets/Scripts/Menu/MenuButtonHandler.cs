using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuButtonHandler : MonoBehaviour
{
    private Button newGameButton;
    private Button continueButton;
    private Button settingsButton;
    private Button exitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newGameButton = GameObject.Find("NewGameButton").GetComponent<Button>();
        continueButton = GameObject.Find("ContinueGameButton").GetComponent<Button>();
        settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        exitButton.onClick.AddListener(() => Application.Quit());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
