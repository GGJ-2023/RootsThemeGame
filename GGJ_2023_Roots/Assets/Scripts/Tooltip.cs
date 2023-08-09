using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Tooltip : MonoBehaviour
{

    private Tooltip instance;

   //[SerializeField]
   //private Camera uiCamera;
   // [SerializeField]
    //private RectTransform canvasTransform;
    public TextMeshProUGUI tooltipText;
    public RectTransform bgTransform;

    public LayoutElement layout;
    public int chrWrapLimit;



    private void Awake()
    {
        //gameObject.SetActive(false);
        instance = this;

        bgTransform = transform.Find("t_background").GetComponent<RectTransform>();
        tooltipText = transform.Find("t_text").GetComponent<TextMeshProUGUI>();

        ShowTooltip("text here");
        SetText("HelloWorld?");
    }
    private void Start()
    {
        
        this.gameObject.SetActive(false);
    }

    public void Update()
    {

        int tooltiplength = tooltipText.text.Length;

        layout.enabled = (tooltiplength > chrWrapLimit) ? true : false;
        //Vector2 localPoint;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        //bgTransform.anchoredPosition = Input.mousePosition / canvasTransform.localScale.x;
       // transform.localPosition = localPoint;
    }
        public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
    public void ShowTooltip(string tooltipString)
    {
        SetText(tooltipString);
        gameObject.SetActive(true);
    }
    public void SetText(string info)
    {
        gameObject.SetActive(true);
        
        //tooltipText.SetText(tooltipString);
        tooltipText.ForceMeshUpdate();
        Vector2 textPaddingSize = new Vector2(8, 8);
        Vector2 bgSize = tooltipText.GetRenderedValues(false);
        bgTransform.sizeDelta = bgSize;
        

        tooltipText.text = info;
    }




    public static void ShowTooltip_Static(System.Func<string> func)
    {
        //Instance.ShowTooltip();
    }
}
