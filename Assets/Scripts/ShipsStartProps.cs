using UnityEngine;

public class ShipsStartProps : MonoBehaviour
{
    private MovementController _movementController;

    private void Awake()
    {
        _movementController = GetComponent<MovementController>();
    }

    private void Update()
    {
        _movementController.SetInputVector(new Vector2(0, 1).normalized);
    }
}