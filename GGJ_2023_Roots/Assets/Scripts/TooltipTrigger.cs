using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string info;
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(info);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
