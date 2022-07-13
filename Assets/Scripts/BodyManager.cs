using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    public static BodyController instance;

    private Body m_body;

    const int NULL_INDEX = -1;

    private int m_cutLineStart = NULL_INDEX;
    private int m_cutLineCur   = NULL_INDEX;
    private int m_cutLineEnd   = NULL_INDEX;

    public void Init()
    {
        instance = this;
    }
    public void AddBody(in Body a_body)
    {
        m_body = a_body;

        foreach(BodyPart bp in a_body.m_Parts)
        {

        }
    }

    public void StartCutting(in BodyPart bp)
    {

    }

}
