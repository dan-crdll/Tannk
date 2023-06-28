using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBtns : MonoBehaviour
{
    private int _slideNum = 0;
    //Handler pressione tasto quit
    public void CloseGame()
    {
        ButtonClicked();
        MainMenuManager.Instance.CloseGame();
    }

    public void OpenSettingMenu()
    {
        ButtonClicked();
        CanvasSwitcher.
        SwitchCanvasGroup(MainMenuManager.Instance.MainScreen, MainMenuManager.Instance.SettingScreen);

        MainMenuManager.Instance.StartCoroutine(MainMenuManager.Instance.UpdateVolume());

        Dictionary<string, float> settings = MainMenuManager.Instance.LoadSettings();
        MainMenuManager.Instance.MusicSlider.value = settings["music"] != -1 ? settings["music"] : 1;
        MainMenuManager.Instance.SoundSlider.value = settings["sound"] != -1 ? settings["sound"] : 1;
    }

    public void CloseSettingMenu()
    {
        ButtonClicked();
        CanvasSwitcher.
        SwitchCanvasGroup(MainMenuManager.Instance.SettingScreen, MainMenuManager.Instance.MainScreen);

        MainMenuManager.Instance.StopCoroutine(MainMenuManager.Instance.UpdateVolume());

        MainMenuManager.Instance.SaveSettings();
    }

    public void OpenCreditMenu()
    {
        ButtonClicked();
        CanvasSwitcher.
        SwitchCanvasGroup(MainMenuManager.Instance.MainScreen, MainMenuManager.Instance.CreditScreen);
    }

    public void CloseCreditMenu()
    {
        ButtonClicked();
        CanvasSwitcher.
        SwitchCanvasGroup(MainMenuManager.Instance.CreditScreen, MainMenuManager.Instance.MainScreen);
    }

    public void OpenTutorialMenu()
    {
        ButtonClicked();
        _slideNum = 0;
        CanvasSwitcher.
        SwitchCanvasGroup(MainMenuManager.Instance.MainScreen, MainMenuManager.Instance.TutorialScreen);
    }

    public void CloseTutorialMenu()
    {
        ButtonClicked();
        CanvasSwitcher.
        SwitchCanvasGroup(MainMenuManager.Instance.TutorialSlides[_slideNum], MainMenuManager.Instance.TutorialSlides[0]);

        CanvasSwitcher.
        SwitchCanvasGroup(MainMenuManager.Instance.TutorialScreen, MainMenuManager.Instance.MainScreen);
    }

    public void ResetSettings()
    {
        ButtonClicked();
        MainMenuManager.Instance.SoundSlider.value = 1;
        MainMenuManager.Instance.MusicSlider.value = 1;
    }

    public void Play()
    {
        ButtonClicked();
        MainMenuManager.Instance.LoadGame();
    }

    /**
     * Funzione per switchare l'apertura dei menu (impostazioni, crediti...) senza caricare 
     * nuove scene
     */

    public void NextTutorialSlide()
    {
        ButtonClicked();
        CanvasSwitcher.
        SwitchCanvasGroup(
                MainMenuManager.Instance.TutorialSlides[_slideNum],
                MainMenuManager.Instance.TutorialSlides[(_slideNum + 1) % 3]
            );
        _slideNum = (_slideNum + 1) % 3;
    }

    public void PreviousTutorialSlide()
    {
        ButtonClicked();
        CanvasSwitcher.
        SwitchCanvasGroup(
                MainMenuManager.Instance.TutorialSlides[_slideNum],
                MainMenuManager.Instance.TutorialSlides[_slideNum - 1 < 0 ? 2 : _slideNum - 1]
            );
        _slideNum = _slideNum - 1 < 0 ? 2 : _slideNum - 1;
    }

    private void ButtonClicked()
    {
        MainMenuManager.Instance.SoundEffectSource.Play();
    }
}
