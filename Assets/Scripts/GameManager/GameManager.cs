using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState;
    public GameObject winPanel;
    public int level;

    private void Awake()
    {
        instance = this;
        Debug.Log(level);
    }

    public void LoadLevel()
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
    }

    private IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(2);
        level++;
        SceneManager.LoadScene(level);
    }
}

public enum GameState
{
    Play,
    Win,
    ChangeLevel
}
