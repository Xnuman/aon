using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    private Body myOwner;

    public static float cuttingError = 1.0f;

    [SerializeField]
    private SpriteRenderer mySprite = null;

    [SerializeField]
    public Vector2[] cuttingPoints;

    private int startCuttingPointIndex  = -1;
    private int currentCuttingPointIndex   = -1;
    private int endCuttingPointIndex    = -1;
    private bool reverseOrder = false;

    private void Awake()
    {
        foreach(Vector2 p in cuttingPoints)
        {
          //  p.x = 
        }
    }
    private void OnMouseDown()
    {
        if(GameController.instance.playerController.CurrentMode == PlayerController.InstrumentMode.knife)
        {

            Vector2 mousePos = new (Input.mousePosition.x, Input.mousePosition.y);


            if (currentCuttingPointIndex == endCuttingPointIndex && currentCuttingPointIndex > 0)
            {
                return;
            }

            if (startCuttingPointIndex < 0)
            {
                if (Vector2.Distance(mousePos, cuttingPoints[0]) < Vector2.Distance(mousePos, cuttingPoints[^1]))
                {
                    startCuttingPointIndex = 0;
                    endCuttingPointIndex = cuttingPoints.Length - 1;
                    reverseOrder = false;
                }
                else
                {
                    startCuttingPointIndex = cuttingPoints.Length - 1;
                    endCuttingPointIndex = 0;
                    reverseOrder = true;
                }

                currentCuttingPointIndex = startCuttingPointIndex;
            }

            Vector2 currentCuttingPoint = cuttingPoints[currentCuttingPointIndex];
            //  Vector2 ssCurrentCuttingPoint = GameController.instance.playerController.sceneCamera.WorldToScreenPoint(currentCuttingPoint);
            Vector2 mouseWorldPos = GameController.instance.playerController.sceneCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, GameController.instance.playerController.sceneCamera.GetComponent<Transform>().position.z));

            if (Vector2.Distance(mouseWorldPos, currentCuttingPoint) < cuttingError)
            {
                myOwner.cuttingLine.positionCount++;
                myOwner.cuttingLine.SetPosition(myOwner.cuttingLine.positionCount - 1, new Vector3(currentCuttingPoint.x, currentCuttingPoint.y, -0.5f));
                myOwner.cuttingLine.enabled = true;
                //   myOwner.cuttingLine.widthCurve = 0.1f;

                if (reverseOrder)
                {
                    currentCuttingPointIndex--;
                }
                else
                {
                    currentCuttingPointIndex++;
                }
            }

        }
    }
}
