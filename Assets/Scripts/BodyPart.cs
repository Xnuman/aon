using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    private Body myOwner;

    public Action<BodyPart> onMouseDown; 

    public static float cuttingError = 3.0f;

    [SerializeField]
    private SpriteRenderer m_Sprite = null;

    private LineRenderer m_CuttingLine;

    private Vector3[] m_cuttingPointsPosLocal;
    private Vector3[] m_cuttingPointsPosWorld;

    private int startCuttingPointIndex  = -1;
    private int currentCuttingPointIndex   = -1;
    private int endCuttingPointIndex    = -1;
    private bool reverseOrder = false;

    private void Awake()
    {
    }

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        m_CuttingLine = GetComponent<LineRenderer>();

        m_cuttingPointsPosLocal = new Vector3[m_CuttingLine.positionCount];
        m_cuttingPointsPosWorld = new Vector3[m_CuttingLine.positionCount];

        m_CuttingLine.GetPositions(m_cuttingPointsPosLocal);
        
        for(int i = 0; i < m_cuttingPointsPosLocal.Length; ++i)
        {
            m_cuttingPointsPosWorld[i] = transform.TransformPoint(m_cuttingPointsPosLocal[i]);
        }
    }

    public Vector3[] GetCuttingLinePosLocal => m_cuttingPointsPosLocal;
    public Vector3[] GetCuttingLinePosWorld => m_cuttingPointsPosWorld;
    private void OnMouseDown()
    {
        onMouseDown?.Invoke(this);
    }
}
