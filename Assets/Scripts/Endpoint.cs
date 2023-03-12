using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Endpoint : MonoBehaviour
{
    [SerializeField] public Window_QuestPointer windowQuestPointer;
    [SerializeField] public UnityEvent onPlayerEnter;

    void Start()
    {
        windowQuestPointer ??= FindObjectOfType<Window_QuestPointer>();
        windowQuestPointer.Show(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            onPlayerEnter?.Invoke();
        }
    }
}
