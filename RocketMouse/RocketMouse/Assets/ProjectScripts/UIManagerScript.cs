using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    [SerializeField] private Animator startBtnAnimator;
    [SerializeField] private Animator settingsBtnAnimator;
    [SerializeField] private Animator settingsDlgAnimator;
    [SerializeField] private Animator slidePanelAnimator;
    [SerializeField] private Animator gearImgAnimator;
    [SerializeField] private AudioSource mainCameraVolume;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle volumeToggle;
    private void Start()
    {
        RectTransform transformSlidePanel = slidePanelAnimator.transform as RectTransform;
        Vector2 pos = transformSlidePanel.anchoredPosition;
        pos.y -= transformSlidePanel.rect.height;
        transformSlidePanel.anchoredPosition = pos;
        mainCameraVolume.volume = PlayerPrefs.GetFloat("musicVolume");
        if (PlayerPrefs.GetInt("volumeToggle") == 1)
            volumeToggle.isOn = true;
        else volumeToggle.isOn = false;
        volumeSlider.value = PlayerPrefs.GetFloat("volumeSlider");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("RocketMouse");
    }
    public void OpenSettings()
    {
        startBtnAnimator.SetBool("isHidden", true);
        settingsBtnAnimator.SetBool("isHidden", true);
        settingsDlgAnimator.enabled = true;
        settingsDlgAnimator.SetBool("isHidden", false);
    }
    public void closeSettings()
    {
        startBtnAnimator.SetBool("isHidden", false);
        settingsBtnAnimator.SetBool("isHidden", false);
        settingsDlgAnimator.SetBool("isHidden", true);
    }
    public void ToggleSlidePanel()
    {
        slidePanelAnimator.enabled = true;
        gearImgAnimator.enabled = true;
        bool isHidden = slidePanelAnimator.GetBool("isHidden");
        slidePanelAnimator.SetBool("isHidden", !isHidden);
        gearImgAnimator.SetBool("isHidden", !isHidden);
    }
    public void SaveProgress()
    {
        PlayerPrefs.SetFloat("musicVolume", mainCameraVolume.volume);
        if (volumeToggle.isOn)
            PlayerPrefs.SetInt("volumeToggle", 1);
        else PlayerPrefs.SetInt("volumeToggle", 0);
        PlayerPrefs.SetFloat("volumeSlider", volumeSlider.value);
    }
 
}
