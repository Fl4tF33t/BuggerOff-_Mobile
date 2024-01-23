using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonManager : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerMoveHandler, IPointerEnterHandler
{
    //instead of game object, this will be the scriptable object taht shows all the info of that frog
    [SerializeField] 
    protected FrogSO frogSO;

    //variables used to show info when holding down on a button fro a specified time
    protected bool isPointerDown;
    private Coroutine holdDownCoroutine;
    [SerializeField]
    protected float holdTimeDelay;

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        //This method is used to select a UI element
        //This method is where you implement the functionalionality of the button
        Debug.Log("Click");
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        //This method is used when the user presses down on the UI element
        //You can use a form of TimeScale to determine what is shown after the user presses down
        Debug.Log("Down");

        //Clears out any pre-existing coroutine
        if(holdDownCoroutine != null)
        {
            StopCoroutine(holdDownCoroutine);
        }

        //Sets the hold down is true, and starts a new coroutine
        isPointerDown = true;
        holdDownCoroutine = StartCoroutine(HoldDownDuration());
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        //This method is used when the user releases the UI element
        //You can use this method to reset the Ui element to its original state
        Debug.Log("Up");

        isPointerDown = false;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        //This method is used when the user enters the UI element
        //You can use this on PC when a mouse hovers over the UI element
        Debug.Log("Enter");
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        //This method is used when the user exits the UI element
        //You can use this method to reset the Ui element to its original state
        Debug.Log("Exit");

        isPointerDown = false;
    }

    public virtual void OnPointerMove(PointerEventData eventData)
    {
        //This method is used when the user moves the mouse over the UI element
        //You can use this method to implement a drag and drop functionality
        Debug.Log("Move");
    }

    protected virtual IEnumerator HoldDownDuration()
    {
        yield return new WaitForSeconds(holdTimeDelay);
        while(isPointerDown)
        {
            Debug.Log(frogSO.visualSO.userInterface.UIShopTextInfo);
            yield return null;
        }
        //what to do when it is no longer true, here you can turn the visibility off
    }
}
