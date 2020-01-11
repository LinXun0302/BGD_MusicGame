using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TouchTypes
{
    TouchDown,
    TouchHold,
    TouchRelease,
    Slide,
    None
}

public class InputData
{
    public TouchTypes TouchType = TouchTypes.None;
    public int TouchID          = -1;
    public int TouchTrackIndex  = -1;
}

public class InputManager : Singleton<InputManager>
{
    public List<InputData> TouchUpdate()
    {
        List<InputData> aInputDataList = new List<InputData>();
        if (Input.touchCount <= 0)
        {
            InputData aInputData = new InputData();
            if (Input.GetMouseButtonDown(0))
            {
                aInputData.TouchType = TouchTypes.TouchDown;
                aInputData.TouchTrackIndex = DetectTouchTrack(Input.mousePosition);
                aInputData.TouchID = 0;
            }
            else if(Input.GetMouseButton(0))
            {
                aInputData.TouchType = TouchTypes.TouchHold;
                aInputData.TouchTrackIndex = DetectTouchTrack(Input.mousePosition);
                aInputData.TouchID = 0;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                aInputData.TouchType = TouchTypes.TouchRelease;
                aInputData.TouchTrackIndex = DetectTouchTrack(Input.mousePosition);
                aInputData.TouchID = 0;
            }
            if (aInputData.TouchTrackIndex != -1)
            {
                aInputDataList.Add(aInputData);
            }
        }
        else
        {
            InputData aInputData = new InputData();
            for (int index = 0; index < Input.touchCount; index++)
            {
                Touch aTouch = Input.GetTouch(index);
                aInputData.TouchID = aTouch.fingerId;
                aInputData.TouchTrackIndex = DetectTouchTrack(aTouch.position);
                switch (aTouch.phase)
                {
                    case TouchPhase.Began:
                        aInputData.TouchType = TouchTypes.TouchDown;
                        break;
                    case TouchPhase.Moved:
                        aInputData.TouchType = TouchTypes.TouchHold;
                        break;
                    case TouchPhase.Stationary:
                        aInputData.TouchType = TouchTypes.TouchHold;
                        break;
                    case TouchPhase.Ended:
                        aInputData.TouchType = TouchTypes.TouchRelease;
                        break;
                }
            }
            aInputDataList.Add(aInputData);
        }
        return aInputDataList;
    }

    private int DetectTouchTrack(Vector2 iTouchPosition)
    {
        //Camera need fixed
        Ray ray = Camera.main.ScreenPointToRay(iTouchPosition);
        RaycastHit hit;
        int aTrackIndex = -1;
        if (Physics.Raycast(ray, out hit,100.0f))
        {
            switch (hit.transform.name)
            {
                case "Track1":
                    aTrackIndex = 0;
                    break;
                case "Track2":
                    aTrackIndex = 1;
                    break;
                case "Track3":
                    aTrackIndex = 2;
                    break;
                case "Track4":
                    aTrackIndex = 3;
                    break;
                case "Track5":
                    aTrackIndex = 4;
                    break;
                case "Track6":
                    aTrackIndex = 5;
                    break;
                case "Track7":
                    aTrackIndex = 6;
                    break;
            }
        }
        return aTrackIndex;
    }

    private TouchTypes KeyTouch(KeyCode iKeyCode)
    {
        TouchTypes aTouchTypes = TouchTypes.None;
        if (Input.GetKeyDown(iKeyCode)) {
            aTouchTypes = TouchTypes.TouchDown;
        }
        else if(Input.GetKey(iKeyCode))
        {
            aTouchTypes = TouchTypes.TouchHold;
        }
        else if (Input.GetKeyUp(iKeyCode))
        {
            aTouchTypes = TouchTypes.TouchRelease;
        }
        return aTouchTypes;
    }
}
