using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Editor.Node
{
    
    public class DGNode : UnityEditor.Experimental.GraphView.Node
    {
        public string DialogueName { get; set; }
        public List<string> Choices { get; set; }
        public string Text { get; set; }
        public DGDialogueType DialogueType { get; set; }

        public void Initialize()
        {
            DialogueName = "DialogueName";
            Choices = new List<string>();
            Text = "Dialogue text.";
        }

        public void Draw()
        {
            TextField dialogueNameTextField = new TextField()
            {
                value = DialogueName
            };
            
            titleContainer.Insert(0,dialogueNameTextField);
            
            Port inputPort = Port.Create<Edge>(
                Orientation.Horizontal,
                Direction.Input,
                Port.Capacity.Multi,
                typeof(float));

            inputPort.portName = "Input";
            
            Port outputPort = Port.Create<Edge>(
                Orientation.Horizontal,
                Direction.Input,
                Port.Capacity.Multi,
                typeof(float));

            outputPort.portName = "Output";
            
            inputContainer.Add(inputPort);
            outputContainer.Add(outputPort);

            Foldout textFoldout = new Foldout()
            {
                text = "Dialogue Text"
            };
            
            TextField textTextField= new TextField()
            {
                value = Text
            };
            
            textFoldout.Add(textTextField);
            
            contentContainer.Add(textFoldout);
            
            extensionContainer.Add(contentContainer);
        }
    }
    
}
