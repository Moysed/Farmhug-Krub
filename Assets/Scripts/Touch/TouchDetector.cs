﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetector : MonoBehaviour {

    public TouchIdentifier p_touchIdentifier;
    protected Dictionary<int, TouchIdentifier> _touchPool;
    protected int _lastIndex = 0;

    public Camera _Camera;
    public float swipeSpeed = 0.1f;

    public GameObject[] settingPanel;

    private Vector2 lastTouchPosition;

    // Use this for initialization
    protected virtual void Start()
    {
        //_Camera = this.GetComponent<Camera>();
        _touchPool = new Dictionary<int, TouchIdentifier>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);
            switch (t.phase)
            {
                case TouchPhase.Began:
                    OnTouchBegan(t);
                    break;
                case TouchPhase.Ended:
                    OnTouchEnded(t);
                    break;
                case TouchPhase.Moved:
                    OnTouchMoved(t);
                    break;
                case TouchPhase.Stationary:
                    OnTouchStay(t);
                    break;
                case TouchPhase.Canceled:
                    OnTouchCancel(t);
                    break;
            }
        }

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 currentTouchPosition = Input.GetTouch(0).position;

            if (settingPanel[0].active == false && settingPanel[1].active == false && settingPanel[2].active == false)
            {
                if (lastTouchPosition != Vector2.zero)
                {
                    Vector2 deltaPosition = currentTouchPosition - lastTouchPosition;


                    _Camera.transform.Translate(Vector3.right * -deltaPosition.x * swipeSpeed * Time.deltaTime, Space.World);
                    _Camera.transform.Translate(Vector3.up * -deltaPosition.y * swipeSpeed * Time.deltaTime, Space.World);
                }

                lastTouchPosition = currentTouchPosition;
            }
        }
       

    }

    public virtual void OnTouchBegan(Touch touch)
    {
        
        GetTouchIdentifierWithTouch(touch);

       
    }
    public virtual void OnTouchEnded(Touch touch)
    {
      
            RemoveTouchIdentifierWithTouch(touch);
    }
    public virtual void OnTouchMoved(Touch touch) {
        
            UpdateTouchIdentifier (_touchPool [touch.fingerId], touch);
        TouchRest(touch);
    }
    public virtual void OnTouchStay(Touch touch) {
       
            UpdateTouchIdentifier (_touchPool [touch.fingerId], touch);
    }
    public virtual void OnTouchCancel(Touch touch)
    {
      
            RemoveTouchIdentifierWithTouch(touch);
    }

    public Vector3 convertScreenToWorld(Vector3 pos)
    {
        if (_Camera.orthographic == false)
            print("Camera is not orthographic");

        pos = _Camera.ScreenToWorldPoint(pos);
        pos.z = 0;
        return pos;
    }

   

    public TouchIdentifier GetTouchIdentifierWithTouch(Touch touch)
    {
        //If Finded with fingerID
        if (_touchPool.ContainsKey(touch.fingerId))
        {
            UpdateTouchIdentifier(_touchPool[touch.fingerId], touch);
            return _touchPool[touch.fingerId];
        }

        //Get a new Touch.
        TouchIdentifier t = Instantiate(p_touchIdentifier);
        t.fingerId = touch.fingerId;
        t.timeCreated = Time.time;
        t.startPosition = touch.position;
        //t.parent = transform;
        t.name = "Touch " + _lastIndex;

        UpdateTouchIdentifier(t, touch);


        _lastIndex++;
        _touchPool.Add(touch.fingerId, t);

        return t;
    }

    void UpdateTouchIdentifier(TouchIdentifier touchid, Touch touch)
    {
        touchid.transform.position = convertScreenToWorld(touch.position);
        touchid.deltaPosition = touch.deltaPosition;
    }

    public void RemoveTouchIdentifierWithTouch(Touch touch)
    {
        RemoveTouchIdentifierWithTouch(touch, _touchPool);
    }

    public bool RemoveTouchIdentifierWithTouch(Touch touch, Dictionary<int, TouchIdentifier> listTouch)
    {
        if (_touchPool.ContainsKey(touch.fingerId))
        {
            DeactiveTouch(touch);
            return listTouch.Remove(touch.fingerId);
        }
        return false;
    }

    void DeactiveTouch(Touch touch)
    {
        TouchIdentifier touchId = GetTouchIdentifierWithTouch(touch);

        if (!touchId) return;

        touchId.gameObject.SetActive(false);
        touchId.fingerId = -1;
        Destroy(touchId.gameObject);
    }

    void TouchRest(Touch touch)
    {

        TouchIdentifier touchId = GetTouchIdentifierWithTouch(touch);

        touchId.gameObject.SetActive(false) ;
    }
}
