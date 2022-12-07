using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public struct WaveData
{
    public GameObject[] enemy;
    public Transform[] spawnPoint;
}

public class LevelManager : MonoBehaviour
{
    public Camera mainCamera;

    [SerializeField] private List<WaveData> waves;

    [SerializeField] private List<SquadBrain> allyBrains;
    [SerializeField] private Transform house;

    [SerializeField] private Button btn_play;
    [SerializeField] private Button btn_pause;
    [SerializeField] private Button btn_restart;
    [SerializeField] private Button btn_exit;

    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject looseText;

    private int currentWawe = 0;
    private int waveCount = 0;
    private int allyCount = 0;

    private bool isGameEndedAlready = false;
    private void Awake()
    {
        _default = this;
    }

    private void Start()
    {
        allyCount = allyBrains.Count;
        foreach (SquadBrain brain in allyBrains)
        {
            brain.partyDestroyed.AddListener(AllyDied);
        }
        Time.timeScale = 0f;
        CallNextWave();
    }
    private void AllyDied()
    {
        allyCount--;
        if (allyCount <= 0)
        {
            EndGame(false);
        }
    }

    private void EnemyDied()
    {
        waveCount--;
        if (waveCount <= 0)
        {
            if (currentWawe < waves.Count)
            {
                CallNextWave();
            }
            else
            {
                EndGame(true);
            }
        }
        Debug.Log(waveCount);
    }

    private void CallNextWave()
    {
        if (currentWawe == waves.Count)
            EndGame(true);
        else
        {
            WaveData tempWD = waves[currentWawe];
            for (int i = 0; i < tempWD.enemy.Length; i++)
            {
                GameObject tempEnemy = Instantiate(tempWD.enemy[i], tempWD.spawnPoint[i].position, tempWD.spawnPoint[i].rotation);
                tempEnemy.GetComponent<BrainRootRef>().GetConnectedSB().partyDestroyed.AddListener(EnemyDied);
                waveCount++;
            }
            currentWawe++;
        }
    }

    public void EndGame(bool isWin)
    {
        if (isGameEndedAlready)
            return;
        Debug.LogWarning("Game ended with state: " + isWin);

        if (isWin)
            winText.SetActive(true);
        else
            looseText.SetActive(true);
        isGameEndedAlready = true;
    }

    public Vector3 AskForHousePos()
    {
        return house.position;
    }
    public void StartGame()
    {
        btn_play.gameObject.SetActive(false);
        btn_pause.gameObject.SetActive(true);
        btn_restart.gameObject.SetActive(true);
        btn_exit.gameObject.SetActive(true);

        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        btn_play.gameObject.SetActive(true);
        btn_pause.gameObject.SetActive(false);
        btn_restart.gameObject.SetActive(false);
        btn_exit.gameObject.SetActive(false);

        Time.timeScale = 0f;
    }
    #region Singleton
    private static LevelManager _default;
    public static LevelManager Default => _default;
    #endregion
}
