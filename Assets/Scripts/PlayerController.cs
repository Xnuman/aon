using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public Camera sceneCamera;

    public enum InstrumentMode
    {
        hand,
        knife,
        needle
    };

    private InstrumentMode mode;

    public InstrumentMode CurrentMode => mode;

    public void Init()
    {
         
    }

    private void Awake()
    {
        Debug.Log("PlayerController::Awake()");
        GameController.instance.playerController = this;
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
