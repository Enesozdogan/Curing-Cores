using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class DialogeDataNode :ScriptableObject
{
    [SerializeField]
    bool isPlayerActive = false;
    [SerializeField]
    private string dialogText;
    [SerializeField]
    private List<string> subNodes=new List<string>();
    [SerializeField]
    private Rect nodePos=new Rect(0,0,200,100);

    public bool IsPlayerActive()
    {
        return isPlayerActive;
    }
    public Rect GetRect()
    {
        return nodePos;     }
    public string GetText()
    {
        return dialogText;
    }
    public List<string> GetSubNodes()
    {
        return subNodes;
    }

    public void SetPlayerIsActive(bool isPlayer)
    {
#if UNITY_EDITOR
        Undo.RecordObject(this, "Changed Speaker");
        EditorUtility.SetDirty(this);
#endif
        isPlayerActive = isPlayer;
        
    }
#if UNITY_EDITOR
    public void SetRect(Vector2 pos)
    {
        Undo.RecordObject(this, "Drag");
        nodePos.position= pos;
        EditorUtility.SetDirty(this);
    }

    public void SetDialogText(string txt)
    {
        if (txt != dialogText)
        {
            Undo.RecordObject(this, " Dialouge Text");
            dialogText = txt;
            EditorUtility.SetDirty(this);
        }
       
    }
    public void AddSubNode(string id)
    {
        Undo.RecordObject(this, "Add child");
        subNodes.Add(id);
        EditorUtility.SetDirty(this);
    }
    public void DeleteSubNode(string id)
    {
        Undo.RecordObject(this, "Delete child");
        subNodes.Remove(id);
        EditorUtility.SetDirty(this);
    }

    


#endif
}
