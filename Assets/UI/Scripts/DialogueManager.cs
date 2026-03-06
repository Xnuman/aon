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

    public GameObject CreateDialogue(string dialogueName, Canvas canvas)
    {
        if(dialogueNames.Contains(dialogueName))
        {
            int index = dialogueNames.IndexOf(dialogueName);
            var dialogueObject = Instantiate(dialogueObjects[index], canvas.transform, false);
            dialogueStack.Push(new KeyValuePair<string, GameObject>(dialogueName, dialogueObject));
            return dialogueObject;
        }

        return null;
    }

    public void PopDialogue()
    {
        GameObject dialogueToRemove = dialogueStack.Pop().Value;
        Destroy(dialogueToRemove);
    }
}
