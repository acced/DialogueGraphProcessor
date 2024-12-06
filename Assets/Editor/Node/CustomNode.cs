
namespace Editor.Node
{
    using UnityEngine;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine.UIElements;

    public class CustomNode : Node
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public CustomNode(string title, string description)
        {
            Title = title;
            Description = description;

            // 设置节点的样式和内容
            title = Title;

            var label = new Label(Description);
            label.AddToClassList("node-description");
            this.Add(label);

            // 设置节点的初始样式
            this.style.width = 200;
            this.style.height = 150;
            this.style.backgroundColor = new StyleColor(Color.gray);
        }
    }

}