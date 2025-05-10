using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SettingsMenuManager : MonoBehaviour
{
    public static SettingsMenuManager Instance { get; private set; }
    public static bool isEditing { get; private set; }

    private float[] VolumeBounds = new float[] { 0f, 100f };
    private float[] FovBounds = new float[] { 30f, 160f };
    private float[] SensBounds = new float[] { 1f, 10f };

    [SerializeField] private Button exitButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button changeUserButton;

    [SerializeField] private Scrollbar volumeBar;
    [SerializeField] private Scrollbar fovBar;
    [SerializeField] private Scrollbar sensBar;

    [SerializeField] private TMP_Text volumeText;
    [SerializeField] private TMP_Text fovText;
    [SerializeField] private TMP_Text sensText;
    [SerializeField] private TMP_Text userText;

    [SerializeField] private SetUsernameScript usernameScript;

    void Awake()
    {
        if (Instance) Destroy(gameObject);
        Instance = this;
        isEditing = false;
    }

    void Start()
    {
        usernameScript.gameObject.SetActive(false);

        initPlayerPrefs();

        exitButton.onClick.AddListener(() => closeSettings());
        saveButton.onClick.AddListener(() => savePlayerPrefs());
        changeUserButton.onClick.AddListener(() => changeUsername());

        volumeBar.onValueChanged.AddListener(delegate { setVolText(); });
        sensBar.onValueChanged.AddListener(delegate { setSensText(); });
        fovBar.onValueChanged.AddListener(delegate { setFovText(); });

        volumeBar.value = normalizeFloatToSlider(PlayerPrefs.GetFloat("Volume"), VolumeBounds);
        fovBar.value = normalizeFloatToSlider(PlayerPrefs.GetFloat("Fov"), FovBounds);
        sensBar.value = normalizeFloatToSlider(PlayerPrefs.GetFloat("Sens"), SensBounds);
        setUsernameText();
    }

    private void closeSettings()
    {
        isEditing = false;
        gameObject.SetActive(false);
    }

    private void setVolText()
    {
        volumeText.text = Math.Truncate(normalizeSlidertoFloat(volumeBar.value, VolumeBounds)).ToString();
    }

    private void setSensText()
    {
        sensText.text = (Math.Truncate(normalizeSlidertoFloat(sensBar.value, SensBounds) * 10) / 10f).ToString();
    }

    private void setFovText()
    {
        fovText.text = Math.Truncate(normalizeSlidertoFloat(fovBar.value, FovBounds)).ToString();
    }

    public void setUsernameText()
    {
        userText.text = PlayerPrefs.GetString("currentUser");
    }

    private void savePlayerPrefs()
    {
        PlayerPrefs.SetFloat("Volume", normalizeSlidertoFloat(volumeBar.value, VolumeBounds));
        PlayerPrefs.SetFloat("Sens", normalizeSlidertoFloat(sensBar.value, SensBounds));
        PlayerPrefs.SetFloat("Fov", normalizeSlidertoFloat(fovBar.value, FovBounds));
        isEditing = false;
        gameObject.SetActive(false);
    }

    private void initPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 30f);
        }
        if (!PlayerPrefs.HasKey("Fov"))
        {
            PlayerPrefs.SetFloat("Fov", 60f);
        }
        if (!PlayerPrefs.HasKey("Sens"))
        {
            PlayerPrefs.SetFloat("Sens", 7.5f);
        }
        if (!PlayerPrefs.HasKey("currentUser"))
        {
            changeUsername();
        }
    }

    private void changeUsername()
    {
        usernameScript.gameObject.SetActive(true);
        isEditing = true;
    }

    private float normalizeSlidertoFloat(float sliderValue, float[] bounds)
    {
        return bounds[0] + sliderValue * (bounds[1] - bounds[0]);
    }

    private float normalizeFloatToSlider(float floatValue, float[] bounds)
    {
        return (floatValue - bounds[0]) / (bounds[1] - bounds[0]);
    }

    public void setEditing(bool b)
    {
        isEditing = b;
    }
}