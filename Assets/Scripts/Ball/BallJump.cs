using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class BallJump : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private ParticleSystem _finishParticle;

    private Rigidbody _rigidbody;

    private bool _gotColision;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _finishParticle = FindObjectOfType<FinishParticle>().GetComponent<ParticleSystem>();
        _gotColision = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_gotColision)
        {
            if(collision.gameObject.TryGetComponent(out PlatformSegment platformSegment))
            {
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                _gotColision = true;
                StartCoroutine(MakeCollisionInterval());
            }
            if(collision.gameObject.TryGetComponent(out FinishPlatformSegment finishPlatformSegment))
            {
                _finishParticle.Play();
                _gotColision = true;
                StartCoroutine(RestartGame());
            }
        }

    }

    private IEnumerator MakeCollisionInterval()
    {
        yield return new WaitForSeconds(0.1f);
        _gotColision = false;
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<RestartScript>().LoadGame();
    }
}
