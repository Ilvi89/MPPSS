using UnityEngine;
using UnityEngine.Internal;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    [Header("Ship settings")] [Range(0f, 1f)] [SerializeField]
    private float driftFactor = 0.1f;

    [SerializeField] private float accelerationFactor = 5f;
    [SerializeField] private float turnFactor = 2f;
    [SerializeField] private float rotationAngle;
    [SerializeField] private bool instaStart = false;


    private float _accelerationInput;
    private Rigidbody2D _rigidbody2D;
    private float _rotationAngle;
    private float _sterlingInput;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.SetRotation(transform.rotation);
    }

    private void Start()
    {
        _rotationAngle = rotationAngle != 0 ? rotationAngle : transform.eulerAngles.z;
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySterling();
    }

    private void ApplySterling()
    {
        _rotationAngle -= _sterlingInput * turnFactor;
        _rigidbody2D.MoveRotation(_rotationAngle);
    }

    private void ApplyEngineForce()
    {
        Vector2 engineForceVector = transform.up * (_accelerationInput * accelerationFactor);
        _rigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(_rigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(_rigidbody2D.velocity, transform.right);

        _rigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        _sterlingInput = inputVector.x;
        _accelerationInput = instaStart ? 1 : inputVector.y;
    }
}