using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class floor : MonoBehaviour
{
   public static bool clicked=false;
    //    public void OnPointerDown(PointerEventData eventData)
    // {
    //     Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
    //     clicked=true;
    // }
    public void OnMouseClick(){

            clicked=true;
        }
}
