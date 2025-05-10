using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetUsernameScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField textInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textInput.onSubmit.AddListener(delegate { submitUsername(textInput); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void submitUsername(TMP_InputField s){
        PlayerPrefs.SetString("currentUser", s.text);
        SettingsMenuManager.Instance.setEditing(false);
        SettingsMenuManager.Instance.setUsernameText();
        gameObject.SetActive(false);
    }
}
