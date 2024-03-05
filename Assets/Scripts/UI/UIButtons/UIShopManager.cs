using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIShopManager : MonoBehaviour, IPointerClickHandler
{
    private Image image;
    [SerializeField]
    private Sprite shopIcon;

    private bool isShopButton;


    private void Awake()
    {
        image = GetComponent<Image>();
        image.sprite = shopIcon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
