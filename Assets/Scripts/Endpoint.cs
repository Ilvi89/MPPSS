using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Endpoint : MonoBehaviour
{
    [SerializeField] private Window_QuestPointer windowQuestPointer;
    [SerializeField] private UnityEvent onPlayerEnter;

    void Start()
    {
        windowQuestPointer.Show(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            onPlayerEnter.Invoke();
        }
    }
}
