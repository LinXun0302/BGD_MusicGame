using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{

//-----------------------------------------------
//Public
//-----------------------------------------------
    public void ChangeLinePoint(Vector3 iStartPoint, Vector3 iEndPoint)
    {
        m_StartPoint = iStartPoint;
        m_EndPoint   = iEndPoint;
        m_LineRender.SetPosition(0, m_StartPoint);
        m_LineRender.SetPosition(1, m_EndPoint);
    }

    public void OpenLineRender()
    {
        m_LineRender.enabled = false;
    }

    public void CloseLineRender()
    {
        m_LineRender.enabled = false;
    }

//-----------------------------------------------
//private
//-----------------------------------------------
    private void Awake()
    {
        m_LineRender = this.gameObject.AddComponent<LineRenderer>();

        m_LineMaterial = Resources.Load<Material>("Materials/Blue");
        m_LineRender.material = m_LineMaterial;

        m_LineRender.alignment = LineAlignment.TransformZ;
        this.gameObject.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);

        m_LineRender.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        m_LineRender.startWidth = m_LineWidth;
        m_LineRender.endWidth   = m_LineWidth;

        m_LineRender.positionCount = 2;

        m_LineRender.SetPosition(0, m_StartPoint);
        m_LineRender.SetPosition(1, m_EndPoint);
    }

//-----------------------------------------------
//Variables
//-----------------------------------------------
    private LineRenderer m_LineRender;
    private Material     m_LineMaterial;
    private Vector3 m_StartPoint = Vector3.zero;
    private Vector3 m_EndPoint   = Vector3.zero;
    private float   m_LineWidth  = 1.42f;
}
