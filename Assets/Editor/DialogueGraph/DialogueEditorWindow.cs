
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.DialogueGraph
{
    public  class DialogueEditorWindow : EditorWindow
    {
        private Font m_customFont;
        
        [MenuItem("Window/DG/Dialogue NodeGraph")]
        public static void Open()
        {
            GetWindow<DialogueEditorWindow>("Dialogue Graph");
        }

        private void OnEnable()
        {
            AddGraphView();

            AddStyles();
            
            m_customFont = AssetDatabase.LoadAssetAtPath<Font>("Assets/Editor Default Resources/Dialogue Graph/HYBlackMythTrailblazer.ttf");
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