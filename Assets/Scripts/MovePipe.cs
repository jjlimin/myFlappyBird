using UnityEngine;

public class MovePipe : MonoBehaviour
{
    [SerializeField] private float _speed = 0.65f;
    [SerializeField] private float _destroyMargin = 1f;

    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        transform.position += Vector3.left * (_speed * Time.deltaTime);

        float leftEdge = _cam.transform.position.x - (_cam.orthographicSize * _cam.aspect);
        if (transform.position.x < leftEdge - _destroyMargin)
        {
            Destroy(gameObject);
        }
    }
}