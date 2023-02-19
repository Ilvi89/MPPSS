using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SceneModeDropdown : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private UnityEvent onValueChanged;
    private TMP_Dropdown _mDropdown;


    private void Awake()
    {
        if (_levelManager == null) _levelManager = LevelManager.Instance;

        _mDropdown = GetComponent<TMP_Dropdown>();
        _mDropdown.onValueChanged.AddListener(_ => { DropdownValueChanged(_mDropdown); });

        Debug.Log(_levelManager.lvlMode);
    }


    private void DropdownValueChanged(TMP_Dropdown change)
    {
        if (_levelManager == null)
            _levelManager = LevelManager.Instance;
        _levelManager.SetMode(change.value);
    }
}