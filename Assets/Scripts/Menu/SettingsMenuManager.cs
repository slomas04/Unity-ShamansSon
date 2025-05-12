using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor;

public class SettingsMenuManager : PauseSettingsManager
{
    public static SettingsMenuManager Instance { get; private set; }

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

        usernameScript.gameObject.SetActive(false);

        exitButton.onClick.AddListener(() => closeSettings());
        changeUserButton.onClick.AddListener(() => changeUsername());

        setUsernameText();
        ApplyAudioSettings(); 
    }

    private void closeSettings()
    {
        audioSource.PlayOneShot(buttonClickSound);
        gameObject.SetActive(false);
        MenuButtonHandler.TabOpen = false;
        ApplyAudioSettings();
    }

    public void setUsernameText()
    {
        string currentUser = PlayerPrefs.GetString("currentUser");
        if (string.IsNullOrEmpty(currentUser))
        {
            changeUsername();
            return;
        }
        userText.text = PlayerPrefs.GetString("currentUser");
    }

    private void changeUsername()
    {
        MenuButtonHandler.TabOpen = true;
        audioSource.PlayOneShot(buttonClickSound);
        usernameScript.gameObject.SetActive(true);
    }

    public void ApplyAudioSettings()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 30f);
        AudioListener.volume = volume / 100f;
    }

}