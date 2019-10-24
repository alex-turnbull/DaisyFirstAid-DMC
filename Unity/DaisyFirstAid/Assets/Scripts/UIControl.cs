using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIControl : MonoBehaviour
{
    public GameObject pagesHolder;
    [SerializeField]
    private List<GameObject> pages = new List<GameObject>();
    public List<GameObject> pageHistory = new List<GameObject>();
    private int currentPageIndex = 0;
    private int previousPageIndex = 0;
    public GameObject currentPage;

    public Text colourThing;
    GraphicRaycaster raycaster;

    private Vector3 initMenuPos;

    public GameObject menu;
    public GameObject menuBack;

    private IEnumerator coroutine;
    private bool menuOpen = false;

    private bool moving = false;

    void Awake()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        initMenuPos = menu.GetComponent<RectTransform>().anchoredPosition;
    }

    private void Start()
    {
        for (int i = 0; i < pagesHolder.transform.childCount; i++)
        {
            pages.Add(pagesHolder.transform.GetChild(i).gameObject);
            pagesHolder.transform.GetChild(i).gameObject.GetComponent<pageRef>().pageIndex = i;
        }
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || touch.phase == TouchPhase.Ended)
            {
                //Set up the new Pointer Event
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                pointerData.position = Input.GetTouch(0).position;
                raycaster.Raycast(pointerData, results);

                moving = pointerData.IsScrolling();
                //if (good = true) work; 


                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    //if (result.gameObject.name == "MenuButton")
                    //{
                    //    StopAllCoroutines();
                    //    coroutine = openMenu();
                    //    StartCoroutine(coroutine);
                    //}

                    //if (result.gameObject.name == "Return" || results[0].gameObject.name == "menuBack")
                    //{
                    //    StopAllCoroutines();
                    //    coroutine = closeMenu();
                    //    StartCoroutine(coroutine);
                    //}

                    //if (result.gameObject.name == "ReturnButton")
                    //{
                    //    previousPage();
                    //}

                    //if (result.gameObject.tag == "PageButton")
                    //{
                    //    if (result.gameObject.GetComponent<pageToOpen>() != null)
                    //    {
                   //         changePage(result.gameObject.GetComponent<pageToOpen>().pageRef);
                    //    }
                    //}

                    colourThing.text = $"HIT: {result.gameObject.name}";
                }              
            }
        }


        //--------------------------------DEBUGGING UNITY--------------------------------
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            changePage(pagesHolder.transform.GetChild(1).gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            changePage(pagesHolder.transform.GetChild(2).gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            changePage(pagesHolder.transform.GetChild(3).gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            changePage(pagesHolder.transform.GetChild(4).gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            changePage(pagesHolder.transform.GetChild(5).gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            previousPage();
        }
        //--------------------------------------------------------------------------------

        //ANDROID BACK BUTTON
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            previousPage();
        }
    }

    IEnumerator openMenu()
    {
        menuOpen = true;
        menuBack.SetActive(true);
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
        menuOpen = false;
        menuBack.SetActive(false);
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

    public void changePage(GameObject pageObject)
    {
        if (!moving)
        {
            pageHistory.Add(currentPage);
            currentPage.SetActive(false);
            currentPage = pageObject;
            currentPage.SetActive(true);
        }
        


        //previousPageIndex = currentPageIndex;
        //currentPageIndex = pageIndex;

        //pagesHolder.transform.GetChild(previousPageIndex).gameObject.SetActive(false);
        //pagesHolder.transform.GetChild(currentPageIndex).gameObject.SetActive(true);
    }

    public void previousPage()
    {
        if(pageHistory.Count != 0)
        {
            currentPage.SetActive(false);
            currentPage = pageHistory[pageHistory.Count - 1];
            pageHistory.RemoveAt(pageHistory.Count - 1);
            currentPage.SetActive(true);
        }        
    }

    public void btn_openMenu()
    {
        StopAllCoroutines();
        coroutine = openMenu();
        StartCoroutine(coroutine);
    }

    public void btn_closeMenu()
    {
        StopAllCoroutines();
        coroutine = closeMenu();
        StartCoroutine(coroutine);
    }
}