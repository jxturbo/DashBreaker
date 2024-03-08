using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public Slider volumeSlider;
    public TMPro.TMP_Dropdown resolutionDropdown;
    int currentResolutionIndex = 0;

    private const string ResolutionPrefKey = "ResolutionIndex";
    private const string VolumePrefKey = "Volume";
    private const string FullscreenPrefKey = "IsFullscreen";

    void Start()
    {
        // Resets current resolution
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        
        // Stores all possible resolutions for the current PC into a string
        List<string> options = new List<string>();
        
        // Records all available screen resolutions
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            // Used to check for the current resolution setting by matching the current setting with the setting currently being 
            // parsed into the array
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Adds the various resolution options to the options menu
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt(ResolutionPrefKey, currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();

        // Set initial volume
        float initialVolume = PlayerPrefs.GetFloat(VolumePrefKey, 0.75f);
        volumeSlider.value = initialVolume;
        SetVolume(initialVolume);

        // Set initial fullscreen mode
        bool initialFullscreen = PlayerPrefs.GetInt(FullscreenPrefKey, 1) == 1;
        Screen.fullScreen = initialFullscreen;
    }

    // Sets resolution
    public void SetResolution(int resolutionIndex)
    {
        // Sets resolution to whatever setting has been chosen by using its position in the array
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(ResolutionPrefKey, resolutionIndex);
        
    }

    // Sets volume of the game using a slider and extracting its fill percentage
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat(VolumePrefKey, volume);
    }

    // Toggles fullscreen in-game
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt(FullscreenPrefKey, isFullScreen ? 1 : 0);
    }
}
