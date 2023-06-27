using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CanvasSwitcher
{
    public static void SwitchCanvasGroup(CanvasGroup canvaToClose, CanvasGroup canvaToOpen)
    {
        canvaToClose.alpha = 0;
        canvaToClose.interactable = false;
        canvaToClose.blocksRaycasts = false;

        canvaToOpen.alpha = 1;
        canvaToOpen.interactable = true;
        canvaToOpen.blocksRaycasts = true;
    }

    public static void ToggleCanvasGroup(CanvasGroup canvaToToggle, bool open)
    {
        canvaToToggle.alpha = open ? 1 : 0;
        canvaToToggle.interactable = open;
        canvaToToggle.blocksRaycasts = open;
    }
}
