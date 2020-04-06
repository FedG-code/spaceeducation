using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickTech : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        switch(eventData.button)
        {
            case PointerEventData.InputButton.Left:
                GetComponent<TechUI>().clickedAdd();
                break;

            case PointerEventData.InputButton.Right:
                GetComponent<TechUI>().clickedRemove();
                break;
        }
    }
}
