using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Body : MonoBehaviour
{
    [SerializeField]
    private BodyPart rightHand;

    [SerializeField]
    public LineRenderer cuttingLine;

    private void Start()
    {
      //  cuttingLine = gameObject.AddComponent<LineRenderer>();
        cuttingLine.positionCount = 0;
    }
}
