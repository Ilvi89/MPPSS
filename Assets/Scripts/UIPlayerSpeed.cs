using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerSpeed : MonoBehaviour
{
    private TMP_Text _speedField;
    [SerializeField] private MovementController movementController;
    [SerializeField] private Image[] steps = new Image[4];

    private void Start()
    {
        _speedField = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _speedField.text = movementController.CurrentSpeed + "knots";

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
