using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoomNodeGraphEditor : EditorWindow {
    private GUIStyle _roomNodeStyle;
    
    // Editor layout values
    private const float NodeWidth = 160f;
    private const float NodeHeight = 75f;
    private const int NodePadding = 25;
    private const int NodeBorder = 12;
    
    // Unity window configuration
    [MenuItem("Room Node Graph Editor", menuItem = "Window/Dungeon Editor/Room Node Graph Editor")]
    private static void OpenWindow() {
        GetWindow<RoomNodeGraphEditor>("Room Node Graph Editor");
    }

    private void OnEnable() {
        // Defines the style and its values
        _roomNodeStyle = new GUIStyle();
        _roomNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
        _roomNodeStyle.normal.textColor = Color.white;
        _roomNodeStyle.padding = new RectOffset(NodePadding, NodePadding, NodePadding, NodePadding);
        _roomNodeStyle.border = new RectOffset(NodeBorder,NodeBorder,NodeBorder,NodeBorder);

    }
    
    private void OnGUI() {
        // Draws the Editor GUI
        GUILayout.BeginArea(new Rect(new Vector2(100f, 100f), new Vector2(NodeWidth, NodeHeight)), _roomNodeStyle);
        EditorGUILayout.LabelField("Node 1");
        GUILayout.EndArea();
        
        GUILayout.BeginArea(new Rect(new Vector2(300f, 300f), new Vector2(NodeWidth, NodeHeight)), _roomNodeStyle);
        EditorGUILayout.LabelField("Node 2");
        GUILayout.EndArea();
    }
}
