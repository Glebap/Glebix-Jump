using UnityEngine;

public class SpawnPlatform : Platform
{
    [SerializeField] private Ball _ball;
    [SerializeField] private Transform _spawnPoint;

    private void Awake()
    {
        _spawnPoint.position = new Vector3(_spawnPoint.position.x, transform.position.y + 1.0f, _spawnPoint.position.z);
        Instantiate(_ball, _spawnPoint.position, Quaternion.identity);
    }
}
