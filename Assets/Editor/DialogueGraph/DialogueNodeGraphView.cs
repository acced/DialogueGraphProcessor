using Editor.Node;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.DialogueGraph
{
    public class DialogueNodeGraphView : GraphView
    {

        public DialogueNodeGraphView()
        {
            AddManipulators();
            
            AddGridBackground();

            CreateNode();

            AddStyles();
            
            
        }

        private void CreateNode()
        {
            DGNode node = new DGNode();
            node.Initialize();
            node.Draw();
            node.SetPosition(new Rect(100, 200, 100, 150)); // 设置初始位置和大小
            DGNode node1 = new DGNode();
            node1.SetPosition(new Rect(300, 200, 100, 150)); // 设置初始位置和大小
            AddElement(node1);
            node1.Initialize();
            node1.Draw();
            AddElement(node);
            AddElement(node1);
        }

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale,ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new ClickSelector());
        }

        private void AddStyles()
        {
            StyleSheet styleSheet =
                (StyleSheet)EditorGUIUtility.Load("Assets/Editor Default Resources/Dialogue Graph/DGDialogueGrapgViewStyles.uss");
            styleSheets.Add(styleSheet);
        }

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();
            
            gridBackground.StretchToParentSize();
            
            Insert(0,gridBackground);

           
        }
    }
}

