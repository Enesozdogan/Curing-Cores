using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class DialougeEditor : EditorWindow
{
    Vector2 scrolPos;
    [NonSerialized]
    DialogeDataNode pickedNode=null;
    DialogScript_Base currDialog;
    [NonSerialized]
    GUIStyle nodeGraphics;
    [NonSerialized]
    GUIStyle playerNodeGraphics;
    [NonSerialized]
    DialogeDataNode creationNode=null,deletionNode=null,linkParentNode=null;
    [NonSerialized]
    bool isDragging = false;
    [NonSerialized]
    Vector2 canvasOffset;
    Vector2 mouseDragOffset;
    [MenuItem("Window/Dialogue Editor")]
  

    public static void OpenEditor()
    {
        GetWindow(typeof(DialougeEditor),false,"Dialoge Editor");
        
    }
    public static void OpenEditor(DialogScript_Base dialogue)
    {
        DialougeEditor window = GetWindow<DialougeEditor>(false, "Dialogue Editor");
        window.currDialog = dialogue;
    }
    [OnOpenAssetAttribute(1)]
    public static bool OnOpenAsset(int id,int line)
    {
        DialogScript_Base dialog = EditorUtility.InstanceIDToObject(id) as DialogScript_Base;
        if (dialog != null)
        {
            Debug.Log(id);
            Debug.Log(line);

            OpenEditor(dialog);
            return true;
        }
        return false;
    }
    private void OnEnable()
    {
        Selection.selectionChanged += OnCurrDialogChanged;
        nodeGraphics=new GUIStyle();
        nodeGraphics.normal.background = EditorGUIUtility.Load("node5") as Texture2D;
        nodeGraphics.fontStyle = FontStyle.Bold;
        nodeGraphics.padding=new RectOffset(20,20,20,20); 
        nodeGraphics.border=new RectOffset(12,12,12,12);
        
        playerNodeGraphics = new GUIStyle();
        playerNodeGraphics.normal.background = EditorGUIUtility.Load("node2") as Texture2D;
        playerNodeGraphics.fontStyle = FontStyle.Bold;
        playerNodeGraphics.padding = new RectOffset(20, 20, 20, 20);
        playerNodeGraphics.border = new RectOffset(12, 12, 12, 12);
    }
    //aktif diyalog sinif secimi
    private void OnCurrDialogChanged()
    {
        DialogScript_Base newDialog=Selection.activeObject as DialogScript_Base;
        if(newDialog != null)
        {
            currDialog=newDialog;
            Repaint();
        }
    }

    private void OnGUI()
    {
        if(currDialog== null)
        {
            EditorGUILayout.LabelField("No dialog");
        }
        else
        {
            HandlePickEvents();
            scrolPos=EditorGUILayout.BeginScrollView(scrolPos);
            GUILayoutUtility.GetRect(4000, 4000);
            foreach (DialogeDataNode node in currDialog.ReturnDialogNodes())
            {
                DrawNode(node);
            }
            foreach (DialogeDataNode node in currDialog.ReturnDialogNodes())
            {
                DrawLines(node);
            }
            EditorGUILayout.EndScrollView();
            if (creationNode != null)
            {
                
                currDialog.CreateDialogNode(creationNode);
                creationNode = null;
            }
            if(deletionNode!= null)
            {
                
                currDialog.DeleteDialogNode(deletionNode);
                creationNode = null;
            }
        }
        
     
    }

    private void DrawLines(DialogeDataNode node)
    {
        Vector3 startPos=new Vector3(node.GetRect().xMax,node.GetRect().center.y);
        foreach(DialogeDataNode childNode in currDialog.GetChilds(node))
        {
            Vector3 endPos=new Vector3(childNode.GetRect().xMin,childNode.GetRect().center.y);
            Vector3 contOffset=endPos- startPos;
            contOffset.y = 0;
            contOffset.x *= 0.8f;
            Handles.DrawBezier(startPos, endPos, startPos+contOffset,endPos-contOffset,Color.grey,null,5f);
        }
    }

    //Asagidaki fonksiyonda fare aksiyonlari takip edilerek secilen dugum hangisi kontrol edilir ve pickedNode dugumune atanir.
    private void HandlePickEvents()
    {
        if (Event.current.type == EventType.ValidateCommand && Event.current.commandName == "UndoRedoPerformed")
        {
            GUI.changed = true;
        }
        if (Event.current.type==EventType.MouseDown && pickedNode == null)
        {
            pickedNode = GetPickedNode(Event.current.mousePosition+scrolPos);
            if (pickedNode == null)
            {
                isDragging = true;
                if (Event.current.alt)
                {
                    canvasOffset = Event.current.mousePosition + scrolPos;
                    Selection.activeObject = currDialog;
                }
                
             
            }
            else
            {
                mouseDragOffset = pickedNode.GetRect().position - Event.current.mousePosition;
                Selection.activeObject = pickedNode;
               
            }
        }
       else if(Event.current.type == EventType.MouseDrag && pickedNode != null)
        {
            
            pickedNode.SetRect(Event.current.mousePosition+mouseDragOffset);
            Repaint();
        }
       else if(Event.current.type == EventType.MouseDrag && isDragging)
        {
            scrolPos=canvasOffset-Event.current.mousePosition;
            Repaint();
        }
       else if(Event.current.type == EventType.MouseUp && pickedNode != null)
        {
            pickedNode = null;
        }
       else if(Event.current.type == EventType.MouseUp && isDragging)
        {
            isDragging = false;
        }
    }
    //Asagidaki fonksiyonda fare pozisyonunda bulunan dugum dondurulur.
    private DialogeDataNode GetPickedNode(Vector2 pos)
    {
        DialogeDataNode pickedFinalNode = null;
        foreach (DialogeDataNode node in currDialog.ReturnDialogNodes())
        {
            if (node.GetRect().Contains(pos))
            {
                pickedFinalNode= node;
            }
        }
        return pickedFinalNode;
    }
    //Dugum cizim islemleri
    private void DrawNode(DialogeDataNode node)
    {
        GUIStyle style = nodeGraphics;
        if (node.IsPlayerActive()) style = playerNodeGraphics;
        GUILayout.BeginArea(node.GetRect(), style);
        node.SetDialogText(EditorGUILayout.TextField(node.GetText()));
        

        GUILayout.BeginHorizontal();
        LinkButtons(node);
        if (GUILayout.Button("+"))
        {
            creationNode = node;
        }
        if (GUILayout.Button("-"))
        {
            deletionNode = node;
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    private void LinkButtons(DialogeDataNode node)
    {
        if (linkParentNode == null)
        {
            if (GUILayout.Button("Link"))
            {
                linkParentNode = node; 
            }
        }
        else if(linkParentNode == node)
        {
            if (GUILayout.Button("Cancel"))
            {
                linkParentNode = null;
            }
        }
        else if (linkParentNode.GetSubNodes().Contains(node.name))
        {
            if (GUILayout.Button("Unlink"))
            {
                
                linkParentNode.DeleteSubNode(node.name);
                linkParentNode = null; 
            }
        }
        else
        {
            if(GUILayout.Button("Sub Link"))
            {
               
                linkParentNode.AddSubNode(node.name);
                linkParentNode= null;
            }
        }
    }
}
