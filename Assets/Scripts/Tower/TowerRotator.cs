using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class TowerRotator : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
                float torque = touch.deltaPosition.x * Time.deltaTime * _rotateSpeed;
                _rigidbody.AddRelativeTorque(Vector3.down * torque);
            }
        }
    }
}
