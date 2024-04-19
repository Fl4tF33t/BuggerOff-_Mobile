using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrogChangeSelection : MonoBehaviour
{
    [SerializeField] private Sprite _selectedSprite;
    [SerializeField] private Sprite _unselectedSprite;

    private Image _selectionBackground;
    private Image _currentSelection;

    private void Start()
    {
        _currentSelection = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        //Debug.Log("Current selection: " + _currentSelection.transform.parent.parent.name);
    }

    public void OnFrogSelected(GameObject selectedFrog)
    {
        _currentSelection.sprite = _unselectedSprite;

        _currentSelection = selectedFrog.transform.parent.GetComponent<Image>();
        //Debug.Log("Current selection !!2:!! " + _currentSelection.name);

        _currentSelection.sprite = _selectedSprite;
    }
}
