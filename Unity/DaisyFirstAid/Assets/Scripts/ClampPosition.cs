using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampPosition : MonoBehaviour
{
    public enum clampAxis
    {
        x,
        y
    }

    public clampAxis selectedAxis;

    public float topBound;
    public float bottomBound;

    public RectTransform rectTransform;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (selectedAxis)
        {
            case clampAxis.x:
                if(rectTransform.anchoredPosition.x < bottomBound)
                {
                    rectTransform.anchoredPosition = new Vector2(bottomBound, rectTransform.anchoredPosition.y);

                }else if(rectTransform.anchoredPosition.x > topBound)
                {
                    rectTransform.anchoredPosition = new Vector2(topBound, rectTransform.anchoredPosition.y);
                }
                break;

            case clampAxis.y:
                if (rectTransform.anchoredPosition.y < bottomBound)
                {
                    rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, bottomBound);

                }
                else if (rectTransform.anchoredPosition.y > topBound)
                {
                    rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, topBound);
                }
                break;
        }
    }
}
