using UnityEngine;

public class GetSideByVector : MonoBehaviour
{
    [SerializeField] public Transform player;

    private Vector3 _direction;
    private float _directionAngle;

    private Transform _target;

    private void Update()
    {
        _direction = (player.position - _target.position).normalized;
        _directionAngle = Quaternion.FromToRotation(_target.up, _direction).eulerAngles.z;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_target.position, 1);
        Gizmos.DrawRay(_target.position, _target.up * 3);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(player.position, 1);
        Gizmos.DrawRay(player.position, player.up * 3);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_target.position, _direction * 3);
    }

    public void SetTarget(Transform t)
    {
        _target = t;
    }

    public ShipSide GetSide()
    {
        return _directionAngle switch
        {
            (>= 45 and < 135) => ShipSide.Left,
            (>= 135 and < 225) => ShipSide.Back,
            (>= 225 and < 315) => ShipSide.Right,
            _ => ShipSide.Front
        };
    }
}