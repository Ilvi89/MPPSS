using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private float driftFactor = 0.1f;
    [SerializeField] private float maxSpeed = 10;
    [SerializeField] private float turnFactor = 2f;
    [SerializeField] private float rotationAngle;
    [SerializeField] private bool instaStart;
    [SerializeField] private int speedStep = 1;

    private float _accelerationInput;

    private float _delay;
    private bool _pressed;
    private Rigidbody2D _rigidbody2D;
    private float _rotationAngle;
    private float _sterlingInput;

    private float[] SpeedByStep => new[] {-(maxSpeed / 2), 0, maxSpeed / 2, maxSpeed};

    public int SpeedStep
    {
        get => speedStep;
        set => speedStep = value;
    }

    public float CurrentSpeed => SpeedByStep[speedStep];
    
    public float CurrentDirection()
    {
        var dir = 360 - transform.eulerAngles.z;
        return dir > 0 ? dir : dir * -2;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.SetRotation(transform.rotation);
    }

    private void Start()
    {
        speedStep = instaStart ? 2 : 1;
        _rotationAngle = rotationAngle != 0 ? rotationAngle : transform.eulerAngles.z;
    }

    private void FixedUpdate()
    {
        Move();
        KillOrthogonalVelocity();
        ApplySterling();
    }

    private void ApplySterling()
    {
        _rotationAngle -= _sterlingInput * turnFactor;
        _rigidbody2D.MoveRotation(_rotationAngle);
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

    private void Move()
    {
        if (_accelerationInput != 0)
        {
            ResetDelay();
            if (!_pressed)
            {
                _delay = 1f;
                _pressed = true;
                SpeedStep = Mathf.Clamp(SpeedStep + Mathf.RoundToInt(_accelerationInput), 0, 3);
            }
        }

        _rigidbody2D.MovePosition(
            _rigidbody2D.position + (Vector2) transform.up *
            (Time.fixedDeltaTime * CurrentSpeed * 1.85f * 0.30f * 0.5f * .5f));
    }

    private void ResetDelay()
    {
        if (_pressed && _delay > 0) _delay -= Time.deltaTime;
        if (_delay < 0)
        {
            _delay = 0;
            _pressed = false;
        }
    }
}