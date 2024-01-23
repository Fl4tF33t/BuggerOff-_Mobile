using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIShopButton : UIButtonManager
{
    private Image image;
    //[SerializeField]
    TextMeshProUGUI text;

    private void Awake()
    {
        //sets the image of the button to the frog sprite, according to the FrogSO that is attached to the button
        image = GetComponent<Image>();
        image.sprite = frogSO.visualSO.userInterface.UIShopSprite;

        //references the text component of the button
        text = GetComponentInParent<TextMeshProUGUI>();
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        //what to do when the button is clicked on the shop, i assume selection of that frog. Need to establish if not enough money, can you select a frog
    }

    protected override IEnumerator HoldDownDuration()
    {
        text.text = frogSO.visualSO.userInterface.UIShopTextInfo;
        yield return new WaitForSeconds(holdTimeDelay);
        while (isPointerDown)
        {
            //what to do when the button is held down on the shop, i assume showing the stats of that frog
            text.gameObject.SetActive(true);
            yield return null;
        }
        text.gameObject.SetActive(false);
    }
}
