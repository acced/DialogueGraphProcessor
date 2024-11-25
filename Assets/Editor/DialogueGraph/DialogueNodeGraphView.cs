using System.Collections.Generic;
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

            AddStyles();
            
            
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();
            ports.ForEach(port =>
            {
                if (startPort == port)
                {
                    return;
                }

                if (startPort.node == port.node)
                {
                    return;
                }

                if (startPort.direction == port.direction)
                {
                    return;
                }
                
                compatiblePorts.Add(port);

            });
            return compatiblePorts;
        }

        private DGNode CreateNode(Vector2 position)
        {
            DGNode node = new DGNode();
            node.Initialize("New Dialogue",this,position);
            node.Draw();

            return node;
        }

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale,ContentZoomer.DefaultMaxScale);
      
            
   
            
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            
            
            this.AddManipulator(CreateNodeContextualMenu());
            
           // this.AddManipulator(new ClickSelector());
        }

        private IManipulator CreateNodeContextualMenu()
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator((menuEvent =>
            {
                menuEvent.menu.AppendAction("Add Node",(action => AddElement(CreateNode(action.eventInfo.localMousePosition))));
            }));
            return contextualMenuManipulator;
        }

        private void AddStyles()
        {
            StyleSheet styleSheet =
                (StyleSheet)EditorGUIUtility.Load("Assets/Editor Default Resources/Dialogue Graph/DGDialogueGrapgViewStyles.uss");
            StyleSheet nodetyleSheet =
                (StyleSheet)EditorGUIUtility.Load("Assets/Editor Default Resources/Dialogue Graph/DGNode.uss");
            styleSheets.Add(styleSheet);
            
            
            styleSheets.Add(nodetyleSheet);
        }

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();
            
            gridBackground.StretchToParentSize();
            
            Insert(0,gridBackground);

           
        }
    }
}

