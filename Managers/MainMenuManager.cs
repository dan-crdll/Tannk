using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private static MainMenuManager _instace;
    public static MainMenuManager Instance { get { return _instace; } }

    [SerializeField] CanvasGroup _mainScreen;
    [SerializeField] CanvasGroup _settingScreen;
    [SerializeField] CanvasGroup _creditScreen;
    [SerializeField] CanvasGroup _tutorialScreen;
    [SerializeField] Slider _soundSlider;
    [SerializeField] Slider _musicSlider;
    [SerializeField] AudioSource _bgMusicSource;
    [SerializeField] AudioSource _soundEffectSource;
    [SerializeField] TextMeshProUGUI _scoreText;

    [SerializeField] CanvasGroup[] _tutorialSlides;
    public CanvasGroup MainScreen { get { return _mainScreen; }  }
    public CanvasGroup SettingScreen { get { return _settingScreen; } }
    public CanvasGroup CreditScreen { get { return _creditScreen; } }
    public CanvasGroup TutorialScreen { get { return _tutorialScreen; } }
    public Slider SoundSlider { get { return _soundSlider; } }
    public Slider MusicSlider { get { return _musicSlider; } }
    public AudioSource BackgroundMusicSource { get { return _bgMusicSource; } }
    public AudioSource SoundEffectSource { get { return _soundEffectSource; } }
    public TextMeshProUGUI ScoreText { get { return _scoreText; } }
    public CanvasGroup[] TutorialSlides { get { return _tutorialSlides; } }

    private void Awake()
    {
        if (_instace != null)
            Destroy(this);
        _instace = this;
    }

    private void Start()
    {
        Dictionary<string, float> settings = MainMenuManager.Instance.LoadSettings();
        BackgroundMusicSource.volume = settings["music"] != -1 ? settings["music"] : 1;
        SoundEffectSource.volume = settings["sound"] != -1 ? settings["sound"] : 1;

        int[] scores = ScoreManager.Instance.Scores;
        string text = "";

        for(int i = 0; i < scores.Length; i++)
        {
            if (scores[i] == -1)
            {
                if (i == 0)
                    text += "Ancora nulla...";
                break;
            }
            text += (i + 1) + " - " + scores[i]; 
        }
        ScoreText.text = text;
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("music", MusicSlider.value);
        PlayerPrefs.SetFloat("sound", SoundSlider.value);

        BackgroundMusicSource.volume = MusicSlider.value;
        SoundEffectSource.volume = SoundSlider.value;

        PlayerPrefs.Save();
    }

    public Dictionary<string, float> LoadSettings()
    {
        Dictionary<string, float> settings = new Dictionary<string, float>()
        {
            {"music", PlayerPrefs.GetFloat("music", -1) },
            {"sound", PlayerPrefs.GetFloat("sound", -1) }
        };

        return settings;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
