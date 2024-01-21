using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogConnector : MonoBehaviour
{

 
    DialogScript_Base currDialog;
    DialogeDataNode currNode;
    public event Action OnUpdateRequired;
    public string speakerName;
    public bool isInDialog;
    bool isPlayerTalking;
    int index=0;
   

    public void IniaiteDialog(DialogScript_Base dialog)
    {
        currDialog = dialog;
        currNode = currDialog.dialogNodes[0];
        OnUpdateRequired?.Invoke();
    }
    public bool ShowDialog()
    {
        if (currDialog == null || !isInDialog)
        {
            return false;
        }
        return true;
    }
    public bool IsPlayerTalking()
    {
        return isPlayerTalking;
    }
    public string GetDialogText()
    {
        string txt = currNode.GetText();
        return txt;
    }
    public void ProgressDialog()
    {
        int count = currDialog.FetchPlayerNodes(currNode).Count();
        if (count> 0)
        {
            isPlayerTalking= true;
            OnUpdateRequired?.Invoke();
        }
        else
        {
            isPlayerTalking= false;
            DialogeDataNode[] nodes = currDialog.FetchNPCNodes(currNode).ToArray();
       
            index =UnityEngine.Random.Range(0, nodes.Length);
            currNode = nodes[index];
            OnUpdateRequired?.Invoke();
        }
               
        
       
        
        
    }
    public void FinishDialog()
    {
        OnUpdateRequired?.Invoke();
    }
    public void ChooseDialog(DialogeDataNode pickedNode) 
    {
        currNode= pickedNode;
        
        isPlayerTalking = false;
        ProgressDialog();
    }
    public bool CanProgress()
    {
        if(currDialog.GetChilds(currNode).ToArray().Length == 0)
        {
            return false;
        }
        return true;
        
    }
    public IEnumerable<DialogeDataNode> FetchChoiceList()
    {

        return currDialog.GetChilds(currNode);  
    
       
    }
}
