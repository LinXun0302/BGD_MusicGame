using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public List<InputData> TouchUpdate()
    {
        List<InputData> aInputDataList = new List<InputData>();
        if (Input.touchCount <= 0)
        {
            aInputDataList = KeyBroadInputUpdate();
        }
        else
        {
            InputData aInputData = new InputData();
            for (int index = 0; index < Input.touchCount; index++)
            {
                Touch aTouch = Input.touches[index];
                aInputData.TouchID = aTouch.fingerId;
                switch (Input.touches[index].phase)
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
        if (Physics.Raycast(ray, out hit,100.0f))
        {

        }
        return 0;
    }

    private List<InputData> KeyBroadInputUpdate()
    {
        List<InputData> aInputDataList = new List<InputData>();
        InputData aInputData;
        if (KeyTouch(KeyCode.D) != TouchTypes.None)
        {
            aInputData = new InputData();
            aInputData.TouchType = KeyTouch(KeyCode.D);
            aInputData.TouchTrackIndex = 0;
        }
        if (KeyTouch(KeyCode.F) != TouchTypes.None)
        {
            aInputData = new InputData();
            aInputData.TouchType = KeyTouch(KeyCode.F);
            aInputData.TouchTrackIndex = 1;
        }
        if (KeyTouch(KeyCode.G) != TouchTypes.None)
        {
            aInputData = new InputData();
            aInputData.TouchType = KeyTouch(KeyCode.G);
            aInputData.TouchTrackIndex = 2;
        }
        if (KeyTouch(KeyCode.H) != TouchTypes.None)
        {
            aInputData = new InputData();
            aInputData.TouchType = KeyTouch(KeyCode.H);
            aInputData.TouchTrackIndex = 3;
        }
        if (KeyTouch(KeyCode.J) != TouchTypes.None)
        {
            aInputData = new InputData();
            aInputData.TouchType = KeyTouch(KeyCode.J);
            aInputData.TouchTrackIndex = 4;
        }
        if (KeyTouch(KeyCode.K) != TouchTypes.None)
        {
            aInputData = new InputData();
            aInputData.TouchType = KeyTouch(KeyCode.K);
            aInputData.TouchTrackIndex = 5;
        }
        if (KeyTouch(KeyCode.F) != TouchTypes.None)
        {
            aInputData = new InputData();
            aInputData.TouchType = KeyTouch(KeyCode.L);
            aInputData.TouchTrackIndex = 6;
        }
        return aInputDataList;
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
