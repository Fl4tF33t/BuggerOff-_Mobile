using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonManager : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerMoveHandler, IPointerEnterHandler
{
    //instead of game object, this will be the scriptable object taht shows all the info of that frog
    public FrogSO frogSO;

    private bool isPointerDown;
    private Coroutine holdDownCoroutine;
    [SerializeField]
    private float holdTimeDelay;

    public void OnPointerClick(PointerEventData eventData)
    {
        //This method is used to select a UI element
        //This method is where you implement the functionalionality of the button
        Debug.Log("Click");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //This method is used when the user presses down on the UI element
        //You can use a form of TimeScale to determine what is shown after the user presses down
        Debug.Log("Down");

        if(holdDownCoroutine != null)
        {
            StopCoroutine(holdDownCoroutine);
        }

        isPointerDown = true;

        holdDownCoroutine = StartCoroutine(HoldDownDuration());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //This method is used when the user releases the UI element
        //You can use this method to reset the Ui element to its original state
        Debug.Log("Up");

        isPointerDown = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //This method is used when the user enters the UI element
        //You can use this on PC when a mouse hovers over the UI element
        Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //This method is used when the user exits the UI element
        //You can use this method to reset the Ui element to its original state
        Debug.Log("Exit");
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        //This method is used when the user moves the mouse over the UI element
        //You can use this method to implement a drag and drop functionality
        Debug.Log("Move");
    }

    private IEnumerator HoldDownDuration()
    {
        yield return new WaitForSeconds(holdTimeDelay);
        if(isPointerDown)
        {
            Debug.Log("show hold down info");
        }
    }
}
