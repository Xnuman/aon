using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Body : MonoBehaviour
{
    [Header("Body Parts")]
    //[SerializeField] private BodyPart m_head;
    //[SerializeField] private BodyPart m_rHand;
    //[SerializeField] private BodyPart m_lHand;
    //[SerializeField] private BodyPart m_rFoot;
    //[SerializeField] private BodyPart m_lFoot;
    public BodyPart[] m_Parts;

    [SerializeField]
    public LineRenderer cuttingLine;

    private PlayerController g_PlayerController   = null;
    private BodyController   g_BodyController     = null;

    public void Init()
    {
        g_PlayerController = GameController.instance.m_playerController;
        g_BodyController   = GameController.instance.m_bodyController;

        foreach(BodyPart bp in m_Parts)
        {
            AttachBodyPart(bp);
        }
    }

    public void DetachBodyPart(ref BodyPart a_bodyPart)
    {
        a_bodyPart.onMouseDown -= OnBodyPartClicked;
        a_bodyPart = null;
    }

    public void AttachBodyPart(in BodyPart a_bodyPart)
    {
        a_bodyPart.onMouseDown += OnBodyPartClicked;
    }

    public void OnBodyPartClicked(BodyPart a_bodyPart)
    {
        g_BodyController.StartCutting(a_bodyPart);

        if (g_PlayerController.CurrentMode == PlayerController.InstrumentMode.knife)
        {

            //    if (currentCuttingPointIndex == endCuttingPointIndex && currentCuttingPointIndex >= 0)
            //    {
            //        return;
            //    }

            //    Vector2 mousePos = new(Input.mousePosition.x, Input.mousePosition.y);
            //    Camera sceneCam = GameController.instance.playerController.sceneCamera1;
            //    Vector3 sceneCamPos = sceneCam.GetComponent<Transform>().position;

            //    Vector2 mouseWorldPos = sceneCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, sceneCamPos.z));

            //    if (startCuttingPointIndex < 0)
            //    {

            //        Vector2 firstCuttinglineWorldPos = myCuttingLine.transform.TransformPoint(m_cuttingPoints[0]);
            //        Vector2 lastCuttingLineWorldPos = myCuttingLine.transform.TransformPoint(m_cuttingPoints[^1]);

            //        if(Vector2.Distance(mouseWorldPos, firstCuttinglineWorldPos) < Vector2.Distance(mouseWorldPos, lastCuttingLineWorldPos))
            //        {
            //            startCuttingPointIndex = 0;
            //            endCuttingPointIndex = m_cuttingPoints.Length - 1;
            //            reverseOrder = false;
            //        }
            //        else
            //        {
            //            startCuttingPointIndex = m_cuttingPoints.Length - 1;
            //            endCuttingPointIndex = 0;
            //            reverseOrder = true;
            //        }

            //        currentCuttingPointIndex = startCuttingPointIndex;
            //    }

            //    Vector2 currentCuttingPoint = m_cuttingPoints[currentCuttingPointIndex];
            //    Vector2 currentCuttingPointWorldPos = myCuttingLine.transform.TransformPoint(currentCuttingPoint);
            //    //  Vector2 ssCurrentCuttingPoint = GameController.instance.playerController.sceneCamera.WorldToScreenPoint(currentCuttingPoint);


            //    if (Vector2.Distance(mouseWorldPos, currentCuttingPointWorldPos) < cuttingError)
            //    {
            //        myOwner.cuttingLine.positionCount++;
            //        myOwner.cuttingLine.transform.position = gameObject.transform.position;
            //        myOwner.cuttingLine.SetPosition(myOwner.cuttingLine.positionCount - 1, new Vector3(currentCuttingPoint.x, currentCuttingPoint.y, 1.0f));
            //        myOwner.cuttingLine.enabled = true;
            //        //   myOwner.cuttingLine.widthCurve = 0.1f;

            //        if (reverseOrder)
            //        {
            //            currentCuttingPointIndex--;
            //        }
            //        else
            //        {
            //            currentCuttingPointIndex++;
            //        }
            //    }

            //}
        }
    }
    private void Start()
    {
        cuttingLine.positionCount = 0;
    }
}
