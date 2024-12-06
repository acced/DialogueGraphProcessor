
using UnityEngine.UIElements;

namespace Editor.Node
{
    using UnityEngine;
    using UnityEditor;

    public class GraphEditorWindow : EditorWindow
    {
        [MenuItem("Window/Graph Editor")]
        public static void ShowExample()
        {
            GraphEditorWindow wnd = GetWindow<GraphEditorWindow>();
            wnd.titleContent = new GUIContent("Graph Editor");
        }

        private void OnEnable()
        {
            var graphView = new CustomGraphView();
            graphView.StretchToParentSize(); // 填充父容器
            rootVisualElement.Add(graphView); // 将图形视图添加到窗口的根元素中
        }
    }

}