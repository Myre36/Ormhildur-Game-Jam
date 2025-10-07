using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(BackgroundLayerOrdering))]
public class BackgroundLayerOrderingEditor : Editor
{
    private BackgroundLayerOrdering script;

    private void OnEnable()
    {
        script = (BackgroundLayerOrdering)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Sort Background Elements"))
        {
            script.SortBackgroundElements();
        }
    }
}
