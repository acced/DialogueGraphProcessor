
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


    public class AdvancedEdgeManipulator: EdgeManipulator
    {
        protected override void RegisterCallbacksOnTarget()
        {
            base.RegisterCallbacksOnTarget();
            
            target.RegisterCallback<KeyDownEvent>((evt =>
            {
                var node = evt.currentTarget as GraphView;
                if (evt.ctrlKey && evt.keyCode == KeyCode.A)
                {
                    // 选择所有节点
                    SelectAllNodes(node);
                    evt.StopPropagation(); // 阻止事件继续传播
                }
                else
                {
                    // 调用基类的 OnKeyDown 方法处理其他按键事件
                    base.OnKeyDown(evt);
                }
                
            }));
        }

        private void SelectAllNodes(GraphView baseGraphView)
        {
            // 实现选择相邻节点的逻辑
            var adjacentNodes = baseGraphView.nodes
                .ToList();
        
            baseGraphView.ClearSelection();
            adjacentNodes.ForEach(n => baseGraphView.AddToSelection(n));
        }
        
    }
