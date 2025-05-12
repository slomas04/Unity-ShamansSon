using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SettingsMenuManager : PauseSettingsManager
{
    public static SettingsMenuManager Instance { get; private set; }
    public static bool isEditing { get; private set; }

    [SerializeField] private Button exitButton;
    [SerializeField] private Button changeUserButton;

    [SerializeField] private TMP_Text userText;
    [SerializeField] private SetUsernameScript usernameScript;
    [SerializeField] private AudioClip buttonClickSound;

    protected override void Start()
    {
        base.Start();

        if (Instance) Destroy(gameObject);
        Instance = this;
        isEditing = false;

        usernameScript.gameObject.SetActive(false);

        exitButton.onClick.AddListener(() => closeSettings());
        changeUserButton.onClick.AddListener(() => changeUsername());

        setUsernameText();
    }

    private void closeSettings()
    {
        audioSource.PlayOneShot(buttonClickSound);
        isEditing = false;
        gameObject.SetActive(false);
    }

    public void setUsernameText()
    {
        userText.text = PlayerPrefs.GetString("currentUser");
    }

    private void changeUsername()
    {
        audioSource.PlayOneShot(buttonClickSound);
        usernameScript.gameObject.SetActive(true);
        isEditing = true;
    }

    public void setEditing(bool editing)
    {
        isEditing = editing;
    }
}