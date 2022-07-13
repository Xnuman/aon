using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public Camera sceneCamera;

    public Camera sceneCamera1;
    public Camera sceneCamera2;

    public enum InstrumentMode
    {
        hand,
        knife,
        needle
    };

    private InstrumentMode mode;
    private bool isCutting = false;

    public InstrumentMode CurrentMode => mode;

    public void Init()
    {
         
    }

    private void Awake()
    {
        Debug.Log("PlayerController::Awake()");
        GameController.instance.m_playerController = this;
        mode = InstrumentMode.knife;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
