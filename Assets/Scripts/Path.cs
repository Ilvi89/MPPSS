using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private PathType pathType = PathType.Loop;
    private int _currentPointIndex = -1;
    public bool IsCompleted { get; private set; }
    
    public void OnDrawGizmos()
    {
        var childCount = transform.childCount;

        
        for (var i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.GetChild(i).position, 1);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(
                transform.GetChild(i).position,
                transform.GetChild(i + 1).position);
        }

        if (pathType is PathType.Loop)
        {
            Gizmos.DrawLine(
                transform.GetChild(0).position,
                transform.GetChild(childCount - 1).position);
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.GetChild(0).position, 1);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.GetChild(childCount - 1).position, 1);

        
    }

    public Transform GetNextPointTransform()
    {
        if (_currentPointIndex == transform.childCount - 1)
        {
            if (pathType is PathType.Loop)
            {
                _currentPointIndex = -1;
            }
            else
            {
                IsCompleted = true;
                return transform.GetChild(_currentPointIndex);
            }
        }

        _currentPointIndex += 1;
        return transform.GetChild(_currentPointIndex);
    }
}