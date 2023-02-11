using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class SpeedChecker : MonoBehaviour
{
    [SerializeField] private Path _path;
    private bool _end = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start " + _path.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (_end is false)
        {
            if (_path.IsCompleted == true)
            {
                Debug.Log("End " + _path.name);
            }
        }
        
    }
}
