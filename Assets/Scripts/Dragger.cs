//Dragger Input
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dragger : MonoBehaviour
{
    //Sets the Tag up within the game which game objects can be assigned to.
    //It is the use of this Tag that determins within the game which items can be dragged and which can not.
    public const string DRAGGABLE_TAG = "Draggable";

    private bool dragging = false;
    private Vector2 originalPosition;
    private Transform objectToDrag;
    private Text objectToDragImage;

    List<RaycastResult> hitObjects = new List<RaycastResult>();

   

    void Update()
    {
        //Checks whether the primary button (Left button) on the mouse is being pressed down.
        if (Input.GetMouseButtonDown(0))
        {
            objectToDrag = GetDraggableTransformUnderMouse();

            //Only if the object has been identified as draggable with the class get the text and allow it to be moved
            //by assigning the dragging bool to true.
            if (objectToDrag != null)
            {
                dragging = true; 

                objectToDrag.SetAsLastSibling();

                originalPosition = objectToDrag.position;
                objectToDragImage = objectToDrag.GetComponent<Text>();
                objectToDragImage.raycastTarget = false;
            }
        }

        //Checks the status of the dragging bool, and if True sets the drag object to the mouse's position in the game. 
        if (dragging)
        {
            objectToDrag.position = Input.mousePosition;
        }

        //Checks whether the primary button (Left button) on the mouse is NOT being pressed down.
        if (Input.GetMouseButtonUp(0))
        {
            //Only if the object has been identified as draggable within the class will it check the status of the object
            //below. If the object below has the draggable Tag it set the dragged object's position to the static object position.
            //If the user drags the object to somewhere else in the game not including the draggable Tag the object returns to 
            //it's starting position. The dragging bool is the reset to false so the mouse and dragged object are no longer connected.
            if (objectToDrag != null)
            {
                var objectToReplace = GetDraggableTransformUnderMouse();

                if (objectToReplace != null) 
                {
                    objectToDrag.position = objectToReplace.position;
                    objectToDrag.SetParent(objectToReplace);
                }
                else
                {
                    objectToDrag.position = originalPosition;
                }

                objectToDragImage.raycastTarget = true;
                objectToDrag = null;
            }

            dragging = false;
        }
    }

    //This checks the clicked objects's data within the game and the mouse's data. 
    //If the raycast is unable to identify the object in the game it returns Null if not the gameObject is brought to the
    //top of the Raycast.
    private GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;
        
        return hitObjects.First().gameObject;
    }

    //This checks whether the clicked object has been found within the raycast, and if it contains the draggable tag.
    //Only if both these criteria are met will the object be set to moveable, otherwise Null prevents it from moving forward in the 
    //next script.
    private Transform GetDraggableTransformUnderMouse()
    {
        var clickedObject = GetObjectUnderMouse();

        // get top level object hit
        if (clickedObject != null && clickedObject.tag == DRAGGABLE_TAG)
        {
            return clickedObject.transform;
        }

        return null;
    }

    
}