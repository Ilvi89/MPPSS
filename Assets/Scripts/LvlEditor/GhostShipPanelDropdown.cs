using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GhostShipPanelDropdown : MonoBehaviour
{
    [SerializeField] private List<ShipType> shipTypes;
    private TMP_Dropdown _dropdown;

    private LevelManager _levelManager;

    private void Start()
    {
        _levelManager = LevelManager.Instance;
        shipTypes = _levelManager.ListOfShipData.ConvertAll(s => s.ShipType).ToList();
        
        _dropdown = GetComponent<TMP_Dropdown>();
        var options = new List<TMP_Dropdown.OptionData>();
        foreach (var shipType in shipTypes) options.Add(new TMP_Dropdown.OptionData(shipType.ToString()));
        _dropdown.options.AddRange(options);
    }

    public int GetShipData(int arg0)
    {
        return arg0;
    }
}