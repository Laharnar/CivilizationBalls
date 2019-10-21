using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerTouchScreen : MonoBehaviour {
    public Transform ballPref;

    Camera cam;

    List<IEndOfTurnListener> end;
    public Config config;
    public GameObject uiP1, uiP2;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        end = new List<IEndOfTurnListener>();
        MonoBehaviour[] mb = GameObject.FindObjectsOfType<MonoBehaviour>();
        for (int i = 0; i < mb.Length; i++)
        {
            IEndOfTurnListener asEnd = mb[i] as IEndOfTurnListener;
            if (asEnd != null)
            {
                end.Add(asEnd);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetMouseButtonDown(0) && Input.touchCount == 0)
        {


            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            Debug.Log(hit);
            if (hit) Debug.Log(hit.transform);
            bool anyObj = EventSystem.current.IsPointerOverGameObject();
            bool notUILayer = hit && LayerMask.LayerToName(hit.transform.gameObject.layer) != "UI";
            if ((!hit && !anyObj) || (hit.transform == null && !anyObj) || (hit && notUILayer && hit.transform.tag == "Ball"))

            {
                ballPref = config.BallPrefab;
                Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition); // mouse pos
                Instantiate(ballPref, pos, new Quaternion())
                    .GetComponent<MergingLogic>().OnCreated(config.currentColor, config.currentPlayer);

                config.EndTurn();
                ballPref = config.BallPrefab;


                config.UpdateUI(uiP1, uiP2);
            }
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(touch.position), Vector2.zero);
                Debug.Log(hit);
                if (hit) Debug.Log(hit.transform);
                bool anyObj = IsPointerOverUIObject();
                bool notUILayer = hit && LayerMask.LayerToName(hit.transform.gameObject.layer) != "UI";
                if ((!hit && !anyObj) || (hit.transform == null && !anyObj) || (hit && notUILayer && hit.transform.tag == "Ball"))

                {
                    ballPref = config.BallPrefab;
                    Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition); // mouse pos
                    Instantiate(ballPref, pos, new Quaternion())
                        .GetComponent<MergingLogic>().OnCreated(config.currentColor, config.currentPlayer);

                    config.EndTurn();
                    ballPref = config.BallPrefab;


                    config.UpdateUI(uiP1, uiP2);
                }
            }
        }
    }

    private bool IsPointerOverUIObject()
    {

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
