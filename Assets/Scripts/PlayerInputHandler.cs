using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 _inputVector;
    private MovementController _movementController;

    private void Awake()
    {
        _movementController = GetComponent<MovementController>();
    }

    private void Update()
    {
        _inputVector.x = Input.GetAxis("Horizontal");
        _inputVector.y = Input.GetAxis("Vertical");
        _movementController.SetInputVector(_inputVector);
    }
}