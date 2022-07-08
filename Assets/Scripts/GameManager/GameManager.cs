using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState;
    public GameObject winPanel;
    public TMP_Text coinRewardText;
    public int level;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Data data = DataManager.Load();
        Debug.Log(data);
        if (data == null)
        {
            level = 1;
            return;
        }
        else
        {
            level = data.level;
        }
    }

    public void LoadLevel()
    {
        StartCoroutine(ChangeLevel());
    }

    public void NextLevel()
    {
        SetGameState(GameState.ChangeLevel);
    }

    public void SetGameState(GameState state)
    {
        this.gameState = state;

        switch (state)
        {
            case GameState.Play:
                break;
            case GameState.Win:
                StartCoroutine(WinLevel());
                break;
            case GameState.ChangeLevel:
                level++;
                DataManager.Save(new Data(level));
                StartCoroutine(ChangeLevel());
                break;
            default:
                Debug.LogWarning("Not valid game state");
                break;
        }
    }

    private IEnumerator WinLevel()
    {
        yield return new WaitForSeconds(2);
        winPanel.SetActive(true);
        coinRewardText.text = PlayerMovement.instance.stackCount.ToString();
    }

    private IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(level);
    }
}

public enum GameState
{
    Play,
    Win,
    ChangeLevel
}
