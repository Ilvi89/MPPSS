using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerSpeed : MonoBehaviour
{
    private TMP_Text _speedField;
    [SerializeField] private MovementController movementController;

    private void Start()
    {
        _speedField = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _speedField.text = movementController.CurrentSpeed + "knots";
    }
}
