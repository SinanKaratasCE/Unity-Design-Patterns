using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utils
{

    public class IsPointerOverUI : MonoBehaviour
    {
        private int UILayer;


        private void Start()
        {
            UILayer = LayerMask.NameToLayer("UI");
        }

        //This method return 'true' if mouse pointer/touch is over any UI element. Including TextMesh because of this it is not useful for game with TextMesh
        // private bool IsPointerOverUIObject()
        // {
        //     PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        //     eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //     List<RaycastResult> results = new List<RaycastResult>();
        //     EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        //     return results.Count > 0;
        // }

        //This Method return 'true' if mouse pointer/touch is over any UI element. Excluding TextMesh
        public bool IsPointerOverUIElement()
        {
            return IsPointerOverUIElement(GetEventSystemRaycastResults());
        }


        private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaycastResults)
        {
            for (int index = 0; index < eventSystemRaycastResults.Count; index++)
            {
                RaycastResult curRaycastResult = eventSystemRaycastResults[index];
                if (curRaycastResult.gameObject.layer == UILayer)
                    return true;
            }

            return false;
        }


        //Gets all event system raycast results of current mouse or touch position.
        private List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);
            return raycastResults;
        }
 
    }

}