using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace Klak.Timeline
{
    [CustomEditor(typeof(AudioPreview))]
    class AudioPreviewEditor : Editor
    {
        SerializedProperty _clip;

        void OnEnable()
        {
            _clip = serializedObject.FindProperty("template.clip");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_clip);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
