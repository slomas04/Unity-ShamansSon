using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SettingsMenuManager : MonoBehaviour
{
    public static SettingsMenuManager Instance {get; private set;}
    public static bool isEditing {get; private set;}
    
    private float[] VolumeBounds = new float[]{0f,100f};
    private float[] FovBounds = new float[]{30f,160f};
    private float[] SensBounds = new float[]{1f,10f};

    private Button exitButton;
    private Button saveButton;
    private Button changeUserButton;

    private Scrollbar volumeBar;
    private Scrollbar fovBar;
    private Scrollbar sensBar;

    private TMP_Text volumeText;
    private TMP_Text fovText;
    private TMP_Text sensText;
    private TMP_Text userText;

    private SetUsernameScript usernameScript;

    void Awake()
    {
        if (Instance) Destroy(gameObject);
        Instance = this;
        isEditing = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        usernameScript = GameObject.Find("SetUserCanvas").GetComponent<SetUsernameScript>();
        usernameScript.gameObject.SetActive(false);

        initPlayerPrefs();
        exitButton = GameObject.Find("ExitSettingsButton").GetComponent<Button>();
        saveButton = GameObject.Find("SaveButton").GetComponent<Button>();
        changeUserButton = GameObject.Find("ChangeUserButton").GetComponent<Button>();

        volumeBar = GameObject.Find("VolumeScrollbar").GetComponent<Scrollbar>();
        fovBar = GameObject.Find("FovScrollbar").GetComponent<Scrollbar>();
        sensBar = GameObject.Find("SensScrollbar").GetComponent<Scrollbar>();

        volumeText = GameObject.Find("VolumeValue").GetComponent<TMP_Text>();
        fovText = GameObject.Find("FovValue").GetComponent<TMP_Text>();
        sensText = GameObject.Find("SensValue").GetComponent<TMP_Text>();
        userText = GameObject.Find("CurrentUserValue").GetComponent<TMP_Text>();

        exitButton.onClick.AddListener(() => closeSettings());
        saveButton.onClick.AddListener(() => savePlayerPrefs());
        changeUserButton.onClick.AddListener(() => changeUsername());

        volumeBar.onValueChanged.AddListener(delegate {setVolText();});
        sensBar.onValueChanged.AddListener(delegate {setSensText();});
        fovBar.onValueChanged.AddListener(delegate {setFovText();});

        volumeBar.value = normalizeFloatToSlider(PlayerPrefs.GetFloat("Volume"), VolumeBounds);
        fovBar.value = normalizeFloatToSlider(PlayerPrefs.GetFloat("Fov"), FovBounds);
        sensBar.value = normalizeFloatToSlider(PlayerPrefs.GetFloat("Sens"), SensBounds);
        setUsernameText();

        
    }

    private void closeSettings(){
        isEditing = false;
        gameObject.SetActive(false);
    }

    private void setVolText(){
        volumeText.text = Math.Truncate(normalizeSlidertoFloat(volumeBar.value, VolumeBounds)).ToString();
    }

    private void setSensText(){
        sensText.text = (Math.Truncate(normalizeSlidertoFloat(sensBar.value, SensBounds)*10)/10f).ToString();
    }

    private void setFovText(){
        fovText.text = Math.Truncate(normalizeSlidertoFloat(fovBar.value, FovBounds)).ToString();
    }

    public void setUsernameText(){
        userText.text = PlayerPrefs.GetString("currentUser");
    }

    private void savePlayerPrefs(){
        PlayerPrefs.SetFloat("Volume", normalizeSlidertoFloat(volumeBar.value, VolumeBounds));
        PlayerPrefs.SetFloat("Sens", normalizeSlidertoFloat(sensBar.value, SensBounds));
        PlayerPrefs.SetFloat("Fov", normalizeSlidertoFloat(fovBar.value, FovBounds));
        isEditing = false;
        gameObject.SetActive(false);
    }

    private void initPlayerPrefs(){
        if(!PlayerPrefs.HasKey("Volume")){
            PlayerPrefs.SetFloat("Volume", 30f);
        }
        if(!PlayerPrefs.HasKey("Fov")){
            PlayerPrefs.SetFloat("Fov", 60f);
        }
        if(!PlayerPrefs.HasKey("Sens")){
            PlayerPrefs.SetFloat("Sens", 7.5f);
        }
        if(!PlayerPrefs.HasKey("currentUser")){
            changeUsername();
        }
    }

    private void changeUsername(){
        usernameScript.gameObject.SetActive(true);
        isEditing = true;
    }

    private float normalizeSlidertoFloat(float sliderValue, float[] bounds){
        return bounds[0] + sliderValue * (bounds[1] - bounds[0]); 
    }

    private float normalizeFloatToSlider(float floatValue, float[] bounds){
        return (floatValue - bounds[0]) / (bounds[1] - bounds[0]);
    }

    public void setEditing(bool b){
        isEditing = b;
    }
}
