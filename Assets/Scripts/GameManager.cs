using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { NotStarted, Playing, GameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState State { get; private set; } = GameState.NotStarted;

    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _startScreenCanvas;
    [SerializeField] private GameObject _scoreCanvas;
    [SerializeField] private PipeSpawner _pipeSpawner;
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        if (State != GameState.NotStarted) return;

        State = GameState.Playing;
        _startScreenCanvas.SetActive(false);
        _scoreCanvas.SetActive(true);
        _pipeSpawner.StartSpawning();
    }

    public void GameOver()
    {
        State = GameState.GameOver;
        _gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}