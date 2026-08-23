using UnityEngine;

public class LogoFloat : MonoBehaviour
{
    [SerializeField] private float _amplitude = 6f;
    [SerializeField] private float _speed = 4f;

    private RectTransform _rectTransform;
    private Vector2 _startPosition;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startPosition = _rectTransform.anchoredPosition;
    }

    private void Update()
    {
        float offsetY = Mathf.Sin(Time.time * _speed) * _amplitude;
        _rectTransform.anchoredPosition = _startPosition + new Vector2(0, offsetY);
    }
}
