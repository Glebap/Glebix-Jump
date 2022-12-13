using System.Collections;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private int _levelCount;
    [SerializeField] private float _additionalScale;
    [SerializeField] private GameObject _beam;
    [SerializeField] private SpawnPlatform _spawnPlatform;
    [SerializeField] private FinishPlatform _finishPlatform;
    [SerializeField] private Platform[] _platform;

    private float _startAndFinishAdditionalLocalScale = 0.5f;

    public float BeamScaleY => _levelCount / 2.0f + _startAndFinishAdditionalLocalScale + _additionalScale/2.0f;


    private void Awake()
    {
        Build();
    }

    private void Build()
    {
        GameObject beam = Instantiate(_beam, transform);
        beam.transform.localScale = new Vector3(1, BeamScaleY, 1);

        Vector3 spawnPosition = beam.transform.position;
        spawnPosition.y += beam.transform.localScale.y - _additionalScale;

        SpawnPlatform(_spawnPlatform, ref spawnPosition, beam.transform);

        for(int i = 0; i < _levelCount; i++)
        {
            SpawnPlatform(_platform[Random.Range(0, _platform.Length)], ref spawnPosition, beam.transform);
        }

        SpawnPlatform(_finishPlatform, ref spawnPosition, beam.transform);
    }

    private void SpawnPlatform(Platform platform, ref Vector3 spawnPosition, Transform parent)
    {
        var newPlatform = Instantiate(platform, spawnPosition, Quaternion.Euler(0, Random.Range(0,360), 0), parent);
        Vector3 platformScale = newPlatform.transform.localScale;
        platformScale.y = newPlatform.transform.localScale.y / BeamScaleY;
        newPlatform.transform.localScale = platformScale;
        spawnPosition.y -= 1;
    }
}
