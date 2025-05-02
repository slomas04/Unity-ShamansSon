using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuManager : MonoBehaviour
{
    private float[] VolumeBounds = new float[]{0f,100f};
    private float[] FovBounds = new float[]{30f,160f};
    private float[] SensBounds = new float[]{1f,10f};

    private Button exitButton;
    private Button saveButton;

    private Scrollbar volumeBar;
    private Scrollbar fovBar;
    private Scrollbar sensBar;

    private TMP_Text volumeText;
    private TMP_Text fovText;
    private TMP_Text sensText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initPlayerPrefs();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        saveButton = GameObject.Find("SaveButton").GetComponent<Button>();

        volumeBar = GameObject.Find("VolumeScrollbar").GetComponent<Scrollbar>();
        fovBar = GameObject.Find("FovScrollbar").GetComponent<Scrollbar>();
        sensBar = GameObject.Find("SensScrollbar").GetComponent<Scrollbar>();

        volumeText = GameObject.Find("VolumeValue").GetComponent<TMP_Text>();
        fovText = GameObject.Find("FovValue").GetComponent<TMP_Text>();
        sensText = GameObject.Find("SensValue").GetComponent<TMP_Text>();

        exitButton.onClick.AddListener(() => gameObject.SetActive(false));
        saveButton.onClick.AddListener(() => savePlayerPrefs());

        volumeBar.onValueChanged.AddListener(delegate {setVolText();});
        sensBar.onValueChanged.AddListener(delegate {setSensText();});
        fovBar.onValueChanged.AddListener(delegate {setFovText();});

        volumeBar.value = normalizeFloatToSlider(PlayerPrefs.GetFloat("Volume"), VolumeBounds);
        fovBar.value = normalizeFloatToSlider(PlayerPrefs.GetFloat("Fov"), FovBounds);
        sensBar.value = normalizeFloatToSlider(PlayerPrefs.GetFloat("Sens"), SensBounds);

        
    }

    private void setVolText(){
        volumeText.text = normalizeSlidertoFloat(volumeBar.value, VolumeBounds).ToString();
    }

    private void setSensText(){
        sensText.text = normalizeSlidertoFloat(sensBar.value, SensBounds).ToString();
    }

    private void setFovText(){
        fovText.text = normalizeSlidertoFloat(fovBar.value, FovBounds).ToString();
    }

    private void savePlayerPrefs(){
        PlayerPrefs.SetFloat("Volume", normalizeSlidertoFloat(volumeBar.value, VolumeBounds));
        PlayerPrefs.SetFloat("Sens", normalizeSlidertoFloat(sensBar.value, SensBounds));
        PlayerPrefs.SetFloat("Fov", normalizeSlidertoFloat(fovBar.value, FovBounds));
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
    }

    private float normalizeSlidertoFloat(float sliderValue, float[] bounds){
        return bounds[0] + sliderValue * (bounds[1] - bounds[0]); 
    }

    private float normalizeFloatToSlider(float floatValue, float[] bounds){
        return (floatValue - bounds[0]) / (bounds[1] - bounds[0]);
    }
}
