using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="New Dialouge",menuName ="Create Dialouge")]
public class DialogScript_Base : ScriptableObject,ISerializationCallbackReceiver
{
    [SerializeField]
    private string dialogName;
    [NonSerialized]
    Vector2 creationOffset = new Vector2(250, 0);
    [NonSerialized]
    private Dictionary<string,DialogeDataNode> nodeDict=new Dictionary<string,DialogeDataNode>();
    
    public List<DialogeDataNode> dialogNodes=new List<DialogeDataNode>();


    private void Awake()
    {

        OnValidate();
    }
    private void OnValidate()
    {
        nodeDict.Clear();
        foreach(DialogeDataNode node in dialogNodes)
        {
            nodeDict[node.name] = node;
        }
    }

    public IEnumerable<DialogeDataNode> ReturnDialogNodes()
    {
        return dialogNodes;
    }

    public IEnumerable<DialogeDataNode> GetChilds(DialogeDataNode parentDialog)
    {
        List<DialogeDataNode> returnList=new List<DialogeDataNode>();
        foreach(string id in parentDialog.GetSubNodes())
        {
            if (nodeDict.ContainsKey(id))
            {
                returnList.Add(nodeDict[id]);
            }
        }
        return returnList;
    }
#if UNITY_EDITOR
    public void CreateDialogNode(DialogeDataNode parentNode)
    {
        DialogeDataNode newNode = InstantiateNode(parentNode);
        Undo.RegisterCreatedObjectUndo(newNode, "Created New Node");
        Undo.RecordObject(this, "Creating Node");
        AddNode(newNode);
    }

    private void AddNode(DialogeDataNode newNode)
    {
        dialogNodes.Add(newNode);
        OnValidate();
    }

    private  DialogeDataNode InstantiateNode(DialogeDataNode parentNode)
    {
        DialogeDataNode newNode = CreateInstance<DialogeDataNode>();
        newNode.name = Guid.NewGuid().ToString();

        if (parentNode != null)
        {
            parentNode.AddSubNode(newNode.name);
            newNode.SetPlayerIsActive(!parentNode.IsPlayerActive());
            newNode.SetRect(parentNode.GetRect().position + creationOffset);
        }

        return newNode;
    }

    public void DeleteDialogNode(DialogeDataNode nodeToDelete)
    {
        Undo.RecordObject(this, "Deleting Node");
        dialogNodes.Remove(nodeToDelete);
        
        OnValidate();
        foreach (DialogeDataNode node in ReturnDialogNodes())
        {
            node.DeleteSubNode(nodeToDelete.name);
        }
        Undo.DestroyObjectImmediate(nodeToDelete);
    }
#endif
    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        if (dialogNodes.Count == 0)
        {
            DialogeDataNode newNode = InstantiateNode(null);
            AddNode(newNode);
        }
        if (AssetDatabase.GetAssetPath(this) != "")
        {
            foreach(DialogeDataNode node in ReturnDialogNodes())
            {
                if (AssetDatabase.GetAssetPath(node) == "")
                    AssetDatabase.AddObjectToAsset(node, this);
            }
        }
#endif
    }

    public void OnAfterDeserialize()
    {
      
    }

    public IEnumerable<DialogeDataNode> FetchNPCNodes(DialogeDataNode currNode)
    {
        foreach(var node in GetChilds(currNode))
        {
            if (node.IsPlayerActive())
            {
                yield return node;
            }
        }
    }

    public IEnumerable<DialogeDataNode> FetchPlayerNodes(DialogeDataNode currNode)
    {
        foreach (var node in GetChilds(currNode))
        {
            if (!node.IsPlayerActive())
            {
                yield return node;
            }
        }
    }
}
