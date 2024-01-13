using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    [SerializeField] private WaveState state = WaveState.Waiting;
    [SerializeField] private short wave = 1;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private float waitTime = 3f;
    [SerializeField] private float waveTime = 60f;
    private TextMeshProUGUI timeText;
    private TextMeshProUGUI waveText;
    private float countDown;
    private UIController uiCont;
    bool once;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        if (instance != this) Destroy(this);
    }

    private void Start()
    {
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        timeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
        waveText = GameObject.Find("WaveText").GetComponent<TextMeshProUGUI>();
        uiCont = GameObject.Find("Player").GetComponent<UIController>();
        spawner.enabled = false;
        countDown = waitTime;

    }

    void Update()
    {
        switch (state)
        {
            case WaveState.Waiting:
                timeText.text = "Shopping";
                if (once)
                {
                    uiCont.OpenShop();
                    once = false;
                }
                if (!uiCont.ShopEnabled())
                {
                    countDown = waitTime;
                    state = WaveState.CountingDown;
                    once = true;
                }
                break;

            case WaveState.CountingDown:
                if (countDown > 0)
                {
                    timeText.text = ((int)countDown).ToString();
                    countDown -= Time.deltaTime;
                    if (countDown <= 0 || Input.GetKeyDown(KeyCode.T))
                    {
                        timeText.text = "0";
                        countDown = waveTime;
                        SetSpawnerSettings();
                        state = WaveState.Fighting;
                    }
                }
                break;

            case WaveState.Fighting:
                if (countDown > 0)
                {
                    timeText.text = ((int)countDown).ToString();
                    countDown -= Time.deltaTime;
                    if (countDown <= 0 || Input.GetKeyDown(KeyCode.T))
                    {
                        timeText.text = "0";
                        countDown = 0;
                        KillAllEnemies();
                        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Coin")) Destroy(g);
                        StartCoroutine(FightToWait());
                    }
                }
                break;
        }

        #region SpawnerSet
        if (state == WaveState.Fighting && countDown != 0)
        {
            if (!spawner.enabled)
            {
                spawner.enabled = true;
                if (!spawner.state)
                {
                    spawner.state = true;
                    spawner.StartCoroutine(spawner.Spawn());
                }
            }
        }
        else
        {
            if (spawner.enabled)
            {
                spawner.state = false;
                spawner.enabled = false;
                spawner.StopAllCoroutines();
            }
        }
        #endregion
    }

    void SetSpawnerSettings()
    {
        spawner.SetNumberOfEnemies((int) (spawner.baseNumberOfEnemies * Mathf.Sqrt(wave)));
    }

    void KillAllEnemies()
    {
        foreach (Enemy e in FindObjectsOfType<Enemy>())
        {
            e.DestroyEnd();
        }
    }

    public enum WaveState
    {
        Waiting,
        CountingDown,
        Fighting
    }

    IEnumerator FightToWait()
    {
        yield return new WaitForSeconds(2f);
        state = WaveState.Waiting;
        wave++;
        waveText.text = wave.ToString();
    }
}
