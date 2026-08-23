using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool HasGameStarted { get; private set; }

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
        if (HasGameStarted) return;

        HasGameStarted = true;
        _startScreenCanvas.SetActive(false);
        _scoreCanvas.SetActive(true);
        _pipeSpawner.StartSpawning();
    }

    public void GameOver()
    {
        _gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}