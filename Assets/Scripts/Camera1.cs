using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        GameController.instance.m_playerController.sceneCamera1 = GetComponent<Camera>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
