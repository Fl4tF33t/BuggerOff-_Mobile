using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
public class ClickFrogWheel : MonoBehaviour, IPointerClickHandler
{

    public event Action OnClickButton;

    private Button kPasa;

    private void Awake()
    {
        kPasa = GetComponent<Button>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickButton?.Invoke();
        
    }

    public void Selection() 
    {
        Debug.Log("dick");
    }

    // Start is called before the first frame update
    void Start()
    {
        kPasa.onClick.AddListener(Selection);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
