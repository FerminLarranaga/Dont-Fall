using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenuController : MonoBehaviour
{

    public Button ResolutionButton;
    public Text ResolutionLabel, GraphicsLabel;
    public DropdownController ResolutionDD, GraphicsDD;
    public Slider VolumeSlider;
    public Toggle FullScreenToggle;

    private Resolution[] resolutions;
    private List<string> cleanResolutionOptions = new List<string>();
    private List<Resolution> CleanResolutions = new List<Resolution>();
    private Vector2 currentResolution;

    private void Start()
    {
        FullScreenToggle.isOn = Screen.fullScreen;

        VolumeSlider.value = PlayerPrefs.GetFloat("Volume");

        HandleGraphicsLabel(QualitySettings.GetQualityLevel());

        resolutions = Screen.resolutions;

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }


        int currentIndexBtn = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            bool isRepeated = false;
            foreach (string cleanOption in cleanResolutionOptions)
            {
                if (options[i] == cleanOption)
                {
                    isRepeated = true;
                }
            }

            if (!isRepeated)
            {
                float resolutionDiv = (float.Parse(options[i].Split('x')[0]) / float.Parse(options[i].Split('x')[1]));

                if (resolutionDiv > 1.76f && resolutionDiv < 1.78f)
                {
                    cleanResolutionOptions.Add(options[i]);
                    CleanResolutions.Add(resolutions[i]);

                    Button newOption = Instantiate(ResolutionButton, ResolutionButton.transform.parent);
                    newOption.name = currentIndexBtn.ToString();
                    TextMeshProUGUI texto = newOption.GetComponentInChildren<TextMeshProUGUI>();
                    texto.text = options[i];

                    currentIndexBtn++;
                }
            }
        }

        Destroy(ResolutionButton.gameObject);

        if (PlayerPrefs.GetString("CurrentResolution") == "")
        {
            ResolutionLabel.text = options[currentResolutionIndex];

            currentResolution = new Vector2(resolutions[currentResolutionIndex].width,
                                            resolutions[currentResolutionIndex].height);
        }
        else
        {
            string currentResString = PlayerPrefs.GetString("CurrentResolution");
            ResolutionLabel.text = currentResString;
            float xCurrentRes = float.Parse((currentResString.Split('x')[0]));
            float yCurrentRes = float.Parse((currentResString.Split('x')[1]));

            currentResolution = new Vector2(xCurrentRes, yCurrentRes);
        }

        Resolution MaxResolution = CleanResolutions[CleanResolutions.ToArray().Length - 1];

        if (currentResolution.x != MaxResolution.width &&
            currentResolution.y != MaxResolution.height)
        {
            FullScreenToggle.interactable = false;
        }
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
    }

    public void SetQuality(int indexQuality)
    {
        QualitySettings.SetQualityLevel(indexQuality, true);
        HandleGraphicsLabel(indexQuality);
        GraphicsDD.SendMessage("HandleDD");
    }

    void HandleGraphicsLabel(int indexQuality)
    {
        if (indexQuality == 0)
            GraphicsLabel.text = "Low";
        else if (indexQuality == 1)
            GraphicsLabel.text = "Medium";
        else
            GraphicsLabel.text = "High";
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(Button ResolutionBtn)
    {
        Resolution resolution = CleanResolutions[int.Parse(ResolutionBtn.name)];
        Resolution MaxResolution = CleanResolutions[CleanResolutions.ToArray().Length - 1];

        if (resolution.width != MaxResolution.width &&
            resolution.height != MaxResolution.height)
        {
            Screen.SetResolution(resolution.width, resolution.height, false);
            FullScreenToggle.isOn = false;
            FullScreenToggle.interactable = false;
        } else
        {
            Screen.SetResolution(resolution.width, resolution.height, true);
            FullScreenToggle.isOn = true;
            FullScreenToggle.interactable = true;
        }

        PlayerPrefs.SetString("CurrentResolution", cleanResolutionOptions[int.Parse(ResolutionBtn.name)]);

        ResolutionLabel.text = cleanResolutionOptions[int.Parse(ResolutionBtn.name)];
        ResolutionDD.SendMessage("HandleDD");

    }
}
