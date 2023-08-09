using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public Tooltip tooltip;
    public static TooltipSystem instance;

    private void Awake()
    {
        instance = this;
    }
    public static void Show(string info)
    {
        if (instance != null) {
        instance.tooltip.SetText(info);
        instance.tooltip.gameObject.SetActive(true);
        }
    }
    public static void Hide()
    {
        if (instance != null)
        {
            instance.tooltip.gameObject.SetActive(false);
        }
    }
}
