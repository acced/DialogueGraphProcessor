
namespace Editor.Node
{
    using UnityEngine;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine.UIElements;

    public class CustomGraphView : GraphView
    {
        public CustomGraphView()
        {
            // 启用点击选择操作
            this.AddManipulator(new AdvancedEdgeManipulator());

            // 设置背景颜色
            this.style.backgroundColor = new StyleColor(Color.white);

            // 添加自定义节点
            AddNode("Node 1", "This is node 1");
            AddNode("Node 2", "This is node 2");
        }
        
        

        // 添加一个自定义节点到 GraphView
        private void AddNode(string title, string description)
        {
            var node = new CustomNode(title, description);
            node.SetPosition(new Rect(100, 100, 200, 150));
            node.RegisterCallback<MouseUpEvent>(OnNodeClicked);
            this.AddElement(node);
        }

        // 处理节点点击事件
        private void OnNodeClicked(MouseUpEvent evt)
        {
            var clickedNode = evt.currentTarget as CustomNode;
            if (clickedNode != null)
            {
                Debug.Log($"Node clicked: {clickedNode.Title}");
                clickedNode.style.backgroundColor = new StyleColor(Color.green); // 改变颜色以示选中

                // 在这里可以更新UI显示选中节点的详细信息
                DisplayNodeDetails(clickedNode);
            }
        }

        // 显示选中节点的详细信息
        private void DisplayNodeDetails(CustomNode node)
        {
            Debug.Log($"Title: {node.Title}");
            Debug.Log($"Description: {node.Description}");
        }
    }

}