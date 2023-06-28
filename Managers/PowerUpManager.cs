using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUps;
    private int minNumber = 2;

    private static PowerUpManager _instance;
    private GameObject[] instantiated = new GameObject[3];
    public static PowerUpManager Instance { get { return _instance; } }
    private GameObject currentInstantiated = null;

    [SerializeField] private Image _powerUpIcon;
    public Image PowerUpIcon { get { return _powerUpIcon; } }

    Coroutine activeCoroutine;
    PowerUp activePowerUp;

    private void Awake()
    {
        if (_instance != null)
            Destroy(this);
        _instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < powerUps.Length; i++)
        {
            instantiated[i] = Instantiate(powerUps[i]);
            instantiated[i].SetActive(false);
        }

        StartCoroutine(InstantiatePowerUp());
    }

    public IEnumerator InstantiatePowerUp()
    {
        while(true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minNumber, minNumber * UnityEngine.Random.Range(5, 10)));
            if(currentInstantiated == null)
            {
                currentInstantiated = instantiated[UnityEngine.Random.Range(0, 3)];
                SpawnManager.Instance.Spawn<PowerUp>(currentInstantiated);
                currentInstantiated.SetActive(true);
            }
        }
    }

    private delegate void ChangeColor();
    Dictionary<Type, ChangeColor> changeColorDict = new Dictionary<Type, ChangeColor>()
    {
        {typeof(NoReloadPowerUp), () => {PowerUpManager.Instance.PowerUpIcon.color = Color.red; } },
        {typeof(FreezePowerUp), () => {PowerUpManager.Instance.PowerUpIcon.color = Color.blue; } },
        {typeof(RealoadLifePowerUp), () => {PowerUpManager.Instance.PowerUpIcon.color = Color.white; }  }
    };

    public void Collected<T>(GameObject powerup)
    {
        if(activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            activePowerUp.Stop();
        } 
        changeColorDict[typeof(T)]();

        if(typeof(T) != typeof(RealoadLifePowerUp))
        {
            activeCoroutine = StartCoroutine(CountDown(powerup));
            activePowerUp = powerup.GetComponent<PowerUp>();
        }

        currentInstantiated.SetActive(false);
        currentInstantiated = null;
    }

    private IEnumerator CountDown(GameObject powerup)
    {
        yield return new WaitForSecondsRealtime(10);
        powerup.GetComponent<PowerUp>().Stop();
        PowerUpManager.Instance.PowerUpIcon.color = Color.white;
        activeCoroutine = null;
    }
}
