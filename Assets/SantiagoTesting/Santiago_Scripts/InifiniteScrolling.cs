using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InifiniteScrolling : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _viewportTransform;
    [SerializeField] private RectTransform _contentPanelTransform;
    [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;

    [SerializeField] private RectTransform[] _ItemsList;
    // Start is called before the first frame update
    void Start()
    {
        int itemsToAdd = Mathf.CeilToInt( _viewportTransform.rect.height / _ItemsList[0].rect.height+_verticalLayoutGroup.spacing);

        for (int i = 0; i < itemsToAdd; i++)
        {
            int num = _ItemsList.Length - i - 1;
            while (num < 0)
            {
                num += _ItemsList.Length;
            }
            RectTransform newItem = Instantiate(_ItemsList[num], _contentPanelTransform);
            newItem.gameObject.SetActive(true);
        }
        _contentPanelTransform.localPosition = new Vector3(_contentPanelTransform.localPosition.x, ((0 - _ItemsList[0].rect.height + _verticalLayoutGroup.spacing) * itemsToAdd), _contentPanelTransform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(_contentPanelTransform.localPosition.y > 0)
        {
            Canvas.ForceUpdateCanvases();
            _contentPanelTransform.localPosition -= new Vector3(0, _ItemsList.Length * (_ItemsList[0].rect.height+_verticalLayoutGroup.spacing), 0);
        }

        if (_contentPanelTransform.localPosition.y < 0 - (_ItemsList.Length * (_ItemsList[0].rect.height+_verticalLayoutGroup.spacing)))
        {
            Canvas.ForceUpdateCanvases();
            _contentPanelTransform.localPosition += new Vector3(0, _ItemsList.Length * (_ItemsList[0].rect.height + _verticalLayoutGroup.spacing), 0);
        }
        */
    }
}
