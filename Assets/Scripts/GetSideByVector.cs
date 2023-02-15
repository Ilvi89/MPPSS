using UnityEngine;

public class GetSideByVector : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _directionAngle;

    [SerializeField] private Side side;

    private void Update()
    {
        _direction = (target.position - transform.position).normalized;
        _directionAngle = Quaternion.FromToRotation(transform.up, _direction).eulerAngles.z;
        side = GetSide(_directionAngle);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1);
        Gizmos.DrawRay(transform.position, transform.up * 3);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(target.position, 1);
        Gizmos.DrawRay(target.position, target.up * 3);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, _direction * 3);
    }

    private Side GetSide(float angle)
    {
        return angle switch
        {
            (>= 45 and < 135) => Side.Left,
            (>= 135 and < 225) => Side.Back,
            (>= 225 and < 315) => Side.Right,
            _ => Side.Front
        };
    }

    private enum Side
    {
        Front,
        Back,
        Left,
        Right
    }
}