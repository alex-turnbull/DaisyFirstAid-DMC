using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIControl : MonoBehaviour
{
    public Text colourThing;
    GraphicRaycaster raycaster;

    private Vector3 initMenuPos;

    public GameObject menu;

    private IEnumerator coroutine;

    void Awake()
    {
        // Get both of the components we need to do this
        raycaster = GetComponent<GraphicRaycaster>();
        initMenuPos = menu.GetComponent<RectTransform>().anchoredPosition;
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || touch.phase == TouchPhase.Began)
            {
                //Set up the new Pointer Event
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                pointerData.position = Input.GetTouch(0).position;
                raycaster.Raycast(pointerData, results);

                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    if(result.gameObject.name == "MenuButton")
                    {
                        StopAllCoroutines();
                        coroutine = openMenu();
                        StartCoroutine(coroutine);
                    }

                    if(result.gameObject.name == "Return")
                    {
                        StopAllCoroutines();
                        coroutine = closeMenu();
                        StartCoroutine(coroutine);
                    }
                    colourThing.text = $"HIT: {result.gameObject.name}";
                }
            }
        }

        if (Input.GetKeyDown("o"))
        {
            StopAllCoroutines();
            coroutine = openMenu();
            StartCoroutine(coroutine);
        }

        if (Input.GetKeyDown("p"))
        {
            StopAllCoroutines();
            coroutine = closeMenu();
            StartCoroutine(coroutine);
        }
    }

    IEnumerator openMenu()
    {
        float elapsedTime = 0;
        float waitTime = 3f;
        RectTransform menuTrans = menu.GetComponent<RectTransform>();

        while (Vector3.Distance(menuTrans.anchoredPosition, new Vector3(0,210,0)) > 0.2)
        {
            menuTrans.anchoredPosition = Vector3.Lerp(menuTrans.anchoredPosition, new Vector3(0,210,0), (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
        // Make sure we got there
        menuTrans.anchoredPosition = new Vector3(0, 210, 0);
        yield return null;
    }

    IEnumerator closeMenu()
    {
        float elapsedTime = 0;
        float waitTime = 3f;
        RectTransform menuTrans = menu.GetComponent<RectTransform>();

        while (Vector3.Distance(menuTrans.anchoredPosition, new Vector3(-540, 210, 0)) > 0.2)
        {
            menuTrans.anchoredPosition = Vector3.Lerp(menuTrans.anchoredPosition, new Vector3(-540, 210, 0), (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
        // Make sure we got there
        menuTrans.anchoredPosition = new Vector3(-540, 210, 0);
        yield return null;
    }
}