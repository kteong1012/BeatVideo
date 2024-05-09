using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelEditorWindow : EditorWindow
{
    [MenuItem("Tools/关卡编辑器")]
    public static void OpenWindow()
    {
        var window = GetWindow<LevelEditorWindow>();
        window.titleContent = new GUIContent("关卡编辑器");
    }

    private GraphView _graphView;

    private void CreateGUI()
    {
        var root = rootVisualElement;

        _graphView = new LevelEditorView
        {
            name = "GraphView"
        };
        _graphView.StretchToParentSize();
        root.Add(_graphView);
    }
}
