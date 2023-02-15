using UnityEngine;

public class ShowDistance : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private Transform target;

    private void FixedUpdate()
    {
        distance = Vector2.Distance(target.position, transform.position);
    }
}