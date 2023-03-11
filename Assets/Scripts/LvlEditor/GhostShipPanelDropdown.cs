using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ShipDataEvent : UnityEvent<ShipData> {}

public class GhostShipPanelDropdown : MonoBehaviour
{
    [SerializeField] private ShipData[] shipTypes;
    private TMP_Dropdown _dropdown;

    private void Start()
    {
        
        _dropdown = GetComponent<TMP_Dropdown>();
        var options = new List<TMP_Dropdown.OptionData>();
        foreach (var shipType in shipTypes) options.Add(new TMP_Dropdown.OptionData(shipType.ShipType.ToString()));
        _dropdown.options.AddRange(options);
        _dropdown.value = 1;
    }

    public ShipData GetShipData(int i) => shipTypes[i];
    
}