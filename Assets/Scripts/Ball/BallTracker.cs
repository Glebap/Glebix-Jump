 using UnityEngine;

public class BallTracker : MonoBehaviour
{
    [SerializeField] private Vector3 _directionOffset;
    [SerializeField] private float _length;
    private Ball _ball;
    private Beam _beam;
    private TowerBuilder _towerBuilder;
    private SpawnPlatform _spawnPlatform;
    private Vector3 _cameraPosition;
    private Vector3 _minimumBallPosition;
    private Vector3 beamPosition;

    private float _startCameraPositionY;

    private void Start()
    {
        _ball = FindObjectOfType<Ball>();
        _beam = FindObjectOfType<Beam>();

        beamPosition = _beam.transform.position;
        _minimumBallPosition = _ball.transform.position;
        
        TrackBall();
    }

    private void Update()
    {
        if (_ball.transform.position.y < _minimumBallPosition.y)
        {
            TrackBall();
            _minimumBallPosition = _ball.transform.position;
        }
    }

    private void TrackBall()
    {
        beamPosition.y = _ball.transform.position.y;
        _cameraPosition = _ball.transform.position;

        Vector3 direction = (beamPosition - _ball.transform.position).normalized + _directionOffset;

        _cameraPosition -= direction * _length;
        transform.position = _cameraPosition;
        transform.LookAt(_ball.transform);
    }
}
