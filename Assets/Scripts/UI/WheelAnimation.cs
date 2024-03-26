using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageScrollChange : MonoBehaviour
{
    [SerializeField]
    private WheelLogic wheel;
    public FrogShopData[] frogShopData;

    // Start is called before the first frame update
    private void Awake()
    {
        //wheel = GetComponentInParent<WheelLogic>();
        frogShopData = GetComponentsInChildren<FrogShopData>(); 
        wheel.OnWheelIconChange += Wheel_OnWheelIconChange;
    }

    private void Wheel_OnWheelIconChange(int index)
    {
        //foreach (var sprite in sprites)
        //{
        //    sprite
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
