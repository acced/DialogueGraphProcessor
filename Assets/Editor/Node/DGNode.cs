using System;
using System.Collections.Generic;
using Editor.DialogueGraph;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Node
{

    public class DGNode : UnityEditor.Experimental.GraphView.Node
    {

        public string ID { get; set; }
        public string DialogueName { get; set; }
        public List<DGDialogueType> Choices { get; set; }
        public string Text { get; set; }
        public DGDialogueType DialogueType { get; set; }
   

        protected DialogueNodeGraphView graphView;
        private Color defaultBackgroundColor;

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Disconnect Input Ports", actionEvent => DisconnectInputPorts());
            evt.menu.AppendAction("Disconnect Output Ports", actionEvent => DisconnectOutputPorts());

            base.BuildContextualMenu(evt);
        }

        public virtual void Initialize(string nodeName, DialogueNodeGraphView dgGraphView, Vector2 position)
        {
            ID = Guid.NewGuid().ToString();

            DialogueName = nodeName;
            Choices = new List<DGDialogueType>();
            Text = "Dialogue text.";

            SetPosition(new Rect(position, Vector2.zero));

            graphView = dgGraphView;
            defaultBackgroundColor = new Color(29f / 255f, 29f / 255f, 30f / 255f);

            

            mainContainer.AddToClassList("dg-node__main-container");
            extensionContainer.AddToClassList("dg-node__extension-container");
        }

        public virtual void Draw()
        {
            /* TITLE CONTAINER */

            TextField dialogueNameTextField = new TextField()
            {
                value = DialogueName
            };

            dialogueNameTextField.AddToClassList(
                "dg-node__text-field"
            );
            dialogueNameTextField.AddToClassList(
                
                "dg-node__text-field__hidden"
            );
            dialogueNameTextField.AddToClassList(
                
                "dg-node__filename-text-field"
            );

            titleContainer.Insert(0, dialogueNameTextField);

            /* INPUT CONTAINER */

            titleContainer.Insert(0,dialogueNameTextField);
            
            Port inputPort = Port.Create<Edge>(
                Orientation.Horizontal,
                Direction.Input,
                Port.Capacity.Multi,
                typeof(bool));

            inputPort.portName = "Input";
            
            Port outputPort = Port.Create<Edge>(
                Orientation.Horizontal,
                Direction.Output,
                Port.Capacity.Multi,
                typeof(bool));

            outputPort.portName = "Output";
            
            inputContainer.Add(inputPort);
            outputContainer.Add(outputPort);

            /* EXTENSION CONTAINER */

            VisualElement customDataContainer = new VisualElement();

            customDataContainer.AddToClassList("dg-node__custom-data-container");

            Foldout textFoldout = new Foldout()
            {
                text = "Dialogue Text",
                value = true 
            };
            
            TextField textTextField= new TextField()
            {
                value = Text
            };

            textTextField.AddToClassList(
                "dg-node__quote-text-field"
            );
            textTextField.AddToClassList(
                "dg-node__text-field"
            );

            textFoldout.Add(textTextField);

            customDataContainer.Add(textFoldout);
           

            extensionContainer.Add(customDataContainer);
            
            // 确保 extensionContainer 默认可见
            extensionContainer.style.display = DisplayStyle.Flex; // 显示扩展容器
            RefreshExpandedState(); // 刷新节点的扩展状态
        }

        public void DisconnectAllPorts()
        {
            DisconnectInputPorts();
            DisconnectOutputPorts();
        }

        private void DisconnectInputPorts()
        {
            DisconnectPorts(inputContainer);
        }

        private void DisconnectOutputPorts()
        {
            DisconnectPorts(outputContainer);
        }

        private void DisconnectPorts(VisualElement container)
        {
            foreach (Port port in container.Children())
            {
                if (!port.connected)
                {
                    continue;
                }

                graphView.DeleteElements(port.connections);
            }
        }
        
     

    }
}
