using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class SideChecker : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform front;
    [SerializeField] private Transform back;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private string side;

    private void Update()
    {
        Transform[] arr = new Transform[4] {front, back, left, right};
        
        arr.Min(t => Vector3.Distance(t.position, transform.position));

        for (int i = 0; i < 3; i++)
        {
            // sideWithMinDist = Vector3.Min(arr[i], arr[i+1]);
        }
        
        side = "___";
    }
}