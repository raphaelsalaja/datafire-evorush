using UnityEditor;
using UnityEditor.XR.Interaction.Toolkit;
[CustomEditor(typeof(SocketWithTagEditor))]
public class SocketWithTagEditor : XRSocketInteractorEditor
{
    private SerializedProperty targetTag;
    protected override void OnEnable()
    {
        base.OnEnable();
        targetTag = serializedObject.FindProperty("targetTag");
    }

    protected override void DrawProperties()
    {
        base.DrawProperties();
        EditorGUILayout.PropertyField(targetTag);
    }
}