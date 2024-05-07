using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
public class ClickFrogWheel : MonoBehaviour, IPointerClickHandler
{
    public FrogSO frogSO;
    public event Action OnClickButton;
    public Action ChangeInfo;
    private Image img;
    public bool isopen = false;

    private Button kPasa;

    private void Awake()
    {
        kPasa = GetComponent<Button>();
        img = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickButton?.Invoke();
        
    }

    public void Selection() 
    {
        ChangeFrogUIManager.Instance.frogWheelSelection = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        kPasa.onClick.AddListener(Selection);
        ChangeInfo = () => img.sprite = frogSO.visualSO.userInterface.UIShopSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
