using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 1.5f;
    [SerializeField] private float _minHeightOffset = -0.4f;
    [SerializeField] private float _maxHeightOffset = 0.9f;
    [SerializeField] private float _pipeLifetime = 4f;
    [SerializeField] private GameObject _pipePrefab;

    public void StartSpawning()
    {
        InvokeRepeating(nameof(SpawnPipe), 0f, _spawnInterval);
    }

    private void SpawnPipe()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, Random.Range(_minHeightOffset, _maxHeightOffset));
        GameObject pipe = Instantiate(_pipePrefab, spawnPosition, Quaternion.identity);

        Destroy(pipe, _pipeLifetime);
    }
}