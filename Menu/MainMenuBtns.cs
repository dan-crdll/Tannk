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
        SwitchCanvasGroup(MainMenuManager.Instance.MainScreen, MainMenuManager.Instance.SettingScreen);

        Dictionary<string, float> settings = MainMenuManager.Instance.LoadSettings();
        MainMenuManager.Instance.MusicSlider.value = settings["music"] != -1 ? settings["music"] : 1;
        MainMenuManager.Instance.SoundSlider.value = settings["sound"] != -1 ? settings["sound"] : 1;
    }

    public void CloseSettingMenu()
    {
        SwitchCanvasGroup(MainMenuManager.Instance.SettingScreen, MainMenuManager.Instance.MainScreen);

        MainMenuManager.Instance.SaveSettings();
    }

    public void OpenCreditMenu()
    {
        SwitchCanvasGroup(MainMenuManager.Instance.MainScreen, MainMenuManager.Instance.CreditScreen);
    }

    public void CloseCreditMenu()
    {
        SwitchCanvasGroup(MainMenuManager.Instance.CreditScreen, MainMenuManager.Instance.MainScreen);
    }

    public void OpenTutorialMenu()
    {
        _slideNum = 0;
        SwitchCanvasGroup(MainMenuManager.Instance.MainScreen, MainMenuManager.Instance.TutorialScreen);
    }

    public void CloseTutorialMenu()
    {
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
    private void SwitchCanvasGroup(CanvasGroup canvaToClose, CanvasGroup canvaToOpen)
    {
        ButtonClicked();
        canvaToClose.alpha = 0;
        canvaToClose.interactable = false;
        canvaToClose.blocksRaycasts = false;

        canvaToOpen.alpha = 1;
        canvaToOpen.interactable = true;
        canvaToOpen.blocksRaycasts = true;
    }

    public void NextTutorialSlide()
    {
        SwitchCanvasGroup(
                MainMenuManager.Instance.TutorialSlides[_slideNum],
                MainMenuManager.Instance.TutorialSlides[(_slideNum + 1) % 3]
            );
        _slideNum = (_slideNum + 1) % 3;
    }

    public void PreviousTutorialSlide()
    {
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
