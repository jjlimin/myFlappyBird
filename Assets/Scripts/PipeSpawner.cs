using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 1.5f;
    [SerializeField] private float _minHeightOffset = -0.4f;
    [SerializeField] private float _maxHeightOffset = 0.9f;
    [SerializeField] private float _spawnMargin = 1f;
    [SerializeField] private GameObject _pipePrefab;

    private void Start()
    {
        Camera cam = Camera.main;
        float halfWidth = cam.orthographicSize * cam.aspect;
        transform.position = new Vector3(
            cam.transform.position.x + halfWidth + _spawnMargin,
            transform.position.y,
            transform.position.z);
    }

    public void StartSpawning()
    {
        InvokeRepeating(nameof(SpawnPipe), 0f, _spawnInterval);
    }

    private void SpawnPipe()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, Random.Range(_minHeightOffset, _maxHeightOffset));
        Instantiate(_pipePrefab, spawnPosition, Quaternion.identity);
    }
}