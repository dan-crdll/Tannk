using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayManager : MonoBehaviour
{
    [SerializeField] Image[] hearts;

    private static OverlayManager _instance;
    public static OverlayManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null)
            Destroy(this);
        _instance = this;
    }

    public void RemoveHeart()
    {
        for(int i = 2; i >= 0; i--)
        {
            if (hearts[i].gameObject.activeSelf)
            {
                hearts[i].gameObject.SetActive(false);
                break;
            }
        }
    }

    public void ReloadHearts()
    {
        foreach(Image heart in hearts) { heart.gameObject.SetActive(true);}
    }
}
