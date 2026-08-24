using System;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;
    
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private AudioClip _scoreClip;
    
    private int _score;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _currentScoreText.text = _score.ToString();
        _highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        if (_score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", _score);
            _highScoreText.text = _score.ToString();
        }
    }

    public void UpdateScore()
    {
        _score++;
        _currentScoreText.text = _score.ToString();
        _audioSource.PlayOneShot(_scoreClip);
        UpdateHighScore();
    }
}
