using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    public List<string> dialogueNames = null;
    [SerializeField]
    public List<GameObject> dialogueObjects = null;

    private Stack<KeyValuePair<string, GameObject>> dialogueStack = new();
    public void Init()
    {
        dialogueNames ??= new List<string>();
        dialogueObjects ??= new List<GameObject>();
    }

    public GameObject CreateDialogue(string dialogueName)
    {

       // if(dialogueStack.Contains(dialogueName))

        if(dialogueNames.Contains(dialogueName))
        {
            int index = dialogueNames.IndexOf(dialogueName);
            var go = Instantiate(dialogueObjects[index]);
            dialogueStack.Push(new KeyValuePair<string, GameObject>(dialogueName, go));
            return go;
        }

        return null;
    }

    public void PopDialogue()
    {
        dialogueStack.Pop();
    }
}
