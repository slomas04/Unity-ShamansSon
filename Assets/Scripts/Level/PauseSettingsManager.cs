using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PauseSettingsManager : MonoBehaviour
{
    protected float[] VolumeBounds = new float[] { 0f, 100f };
    protected float[] FovBounds = new float[] { 30f, 160f };
    protected float[] SensBounds = new float[] { 1f, 10f };

    [SerializeField] protected Button saveButton;

    [SerializeField] protected Scrollbar volumeBar;
    [SerializeField] protected Scrollbar fovBar;
    [SerializeField] protected Scrollbar sensBar;

    [SerializeField] protected TMP_Text volumeText;
    [SerializeField] protected TMP_Text fovText;
    [SerializeField] protected TMP_Text sensText;

    [SerializeField] protected AudioSource audioSource;
    [SerializeField] private AudioClip saveSound;

    protected virtual void Start()
    {
        initPlayerPrefs();

        saveButton.onClick.AddListener(() => savePlayerPrefs());

        volumeBar.onValueChanged.AddListener(delegate { setVolText(); });
        sensBar.onValueChanged.AddListener(delegate { setSensText(); });
        fovBar.onValueChanged.AddListener(delegate { setFovText(); });

        volumeBar.value = normalizeFloatToSlider(PlayerPrefs.GetFloat("Volume"), VolumeBounds);
        fovBar.value = normalizeFloatToSlider(PlayerPrefs.GetFloat("Fov"), FovBounds);
        sensBar.value = normalizeFloatToSlider(PlayerPrefs.GetFloat("Sens"), SensBounds);
    }

    protected void setVolText()
    {
        volumeText.text = Math.Truncate(normalizeSlidertoFloat(volumeBar.value, VolumeBounds)).ToString();
    }

    protected void setSensText()
    {
        sensText.text = (Math.Truncate(normalizeSlidertoFloat(sensBar.value, SensBounds) * 10) / 10f).ToString();
    }

    protected void setFovText()
    {
        fovText.text = Math.Truncate(normalizeSlidertoFloat(fovBar.value, FovBounds)).ToString();
    }

    protected void savePlayerPrefs()
    {
        audioSource.PlayOneShot(saveSound);
        PlayerPrefs.SetFloat("Volume", normalizeSlidertoFloat(volumeBar.value, VolumeBounds));
        PlayerPrefs.SetFloat("Sens", normalizeSlidertoFloat(sensBar.value, SensBounds));
        PlayerPrefs.SetFloat("Fov", normalizeSlidertoFloat(fovBar.value, FovBounds));
    }

    protected void initPlayerPrefs()
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
    }

    protected float normalizeSlidertoFloat(float sliderValue, float[] bounds)
    {
        return bounds[0] + sliderValue * (bounds[1] - bounds[0]);
    }

    protected float normalizeFloatToSlider(float floatValue, float[] bounds)
    {
        return (floatValue - bounds[0]) / (bounds[1] - bounds[0]);
    }
}