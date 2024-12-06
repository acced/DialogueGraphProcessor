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

        private string DialogueImagePath;
   

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
            topContainer.AddToClassList("dg-node__top-container");
            titleContainer.AddToClassList("dg-node__title-container");
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
                
                "dg-node__filename-text-field"
            );
            
            titleContainer.Insert(0, dialogueNameTextField);
            
            /* Under TITLE CONTAINER */
            VisualElement imageDataContainer = new VisualElement();
            imageDataContainer.AddToClassList("dg-node__image-container");
            Image image = new Image()
            {
                name = "ResourceImage",
                // 明确设置可交互
                pickingMode = PickingMode.Position  
            };
            image.pickingMode = PickingMode.Position;
            // 设置默认图片
            image.image = AssetDatabase.LoadAssetAtPath<Texture2D>(
                "Assets/Editor Default Resources/Dialogue Asset/Mina_final(0A25).png"
            );
            image.RegisterCallback<MouseDownEvent>(evt =>
            {
                // 阻止事件冒泡到GraphView
                if (evt.button == 0) // 左键点击
                {
                    evt.StopPropagation();
                    ShowImageSelectorDialog(image);
                }
            });
            image.AddToClassList("dg-node__image-selector");
            
           
            imageDataContainer.Add(image); 
            TextField menuTextField = new TextField()
            {
                value = "菜单文本",
                 
            };
            menuTextField.multiline = true;
            imageDataContainer.Add(menuTextField); 
            
            menuTextField.AddToClassList("dg-node__menu-text-field");
            mainContainer.Insert(1,imageDataContainer);
            
            
            
          
            /* INPUT CONTAINER */
            
            titleContainer.Insert(0,dialogueNameTextField);
            // Add the image to the title container
           
            
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
            
            TextField commandTextField = new TextField()
            {
                value = "命令文本",
            };
            commandTextField.AddToClassList(
                "dg-node__command-text-field"
            );
            
            
            customDataContainer.Add(commandTextField);
            
            Foldout textFoldout = new Foldout()
            {
                text = "Dialogue Text",
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
            RefreshExpandedState(); // 刷新节点的扩展状态
        }
        
        // Implement the method to show the image selection dialog and update the image
        private void ShowImageSelectorDialog(Image image)
        {
            // Example: Open a file picker or resource selection dialog
            // You could use a custom window to load resources or use Unity's FileDialog API if available
            string selectedImagePath = OpenFilePicker(); // This is just a placeholder method
    
            if (!string.IsNullOrEmpty(selectedImagePath))
            {
                // Load the selected image and update the Image element
                Texture2D selectedImage = LoadTextureFromPath(selectedImagePath);
                image.image = selectedImage;
            }
        }

// Placeholder function to open file picker (implement as per your needs)
        private string OpenFilePicker()
        {
            // This could be a custom dialog or integration with a resource selection system
            // In Unity, you could use `UnityEditor.EditorUtility.OpenFilePanel()` in the editor
            return UnityEditor.EditorUtility.OpenFilePanel("Select Image", "", "png,jpg,jpeg");
        }

// Placeholder function to load texture from file path
        private Texture2D LoadTextureFromPath(string path)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes); // Load the image data into a Texture2D
            return texture;
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
