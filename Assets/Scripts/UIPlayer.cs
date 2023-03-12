using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    private TMP_Text _speedField;
    [SerializeField] private TMP_Text _dirField;
    [SerializeField] public MovementController movementController;
    [SerializeField] private Image[] steps = new Image[4];

    private void Start()
    {
        _speedField = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (movementController is null)
        {
            return;
        }
        _speedField.text = movementController.CurrentSpeed + "knots";
        _dirField.text = movementController.CurrentDirection() + "°";
        StepsPaint(movementController.SpeedStep);


    }

    private void StepsPaint(int movementControllerSpeedStep)
    {
        for (int i = movementControllerSpeedStep; i < 4; i++)
        {
            steps[i].color = Color.white;
        }
        for (int i = 0; i <= movementControllerSpeedStep; i++)
        {
            steps[i].color = Color.black;
        }
    }
}
