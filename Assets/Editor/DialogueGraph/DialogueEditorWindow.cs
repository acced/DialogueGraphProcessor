
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.DialogueGraph
{
    public  class DialogueEditorWindow : EditorWindow
    {
       
        
        [MenuItem("Window/DG/Dialogue NodeGraph")]
        public static void Open()
        {
            GetWindow<DialogueEditorWindow>("Dialogue Graph");
        }

        private void OnEnable()
        {
            AddGraphView();

            AddStyles();
            
        }

        private void AddStyles()
        {
            StyleSheet styleSheet =
                (StyleSheet)EditorGUIUtility.Load("Assets/Editor Default Resources/Dialogue Graph/DGVariables.uss");
            rootVisualElement.styleSheets.Add(styleSheet);
        }

        private void AddGraphView()
        {
            DialogueNodeGraphView graphView = new DialogueNodeGraphView();
            
            graphView.StretchToParentSize();
            
            rootVisualElement.Add(graphView);
        }
    }
}