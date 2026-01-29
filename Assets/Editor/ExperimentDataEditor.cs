using System;
using ARLaboratory.Scriptable_Object;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ARLaboratory
{
    [CustomEditor(typeof(ExperimentData))]
    public class ExperimentDataEditor : Editor
    {
        #region SerializedProperties
        SerializedProperty _experimentTimeline;
        SerializedProperty _overwriteCameraPosition;
        SerializedProperty _startCameraPosition;
        SerializedProperty _header; 
        SerializedProperty _description;
        SerializedProperty _webAudio;
        SerializedProperty _videoUrl;
        SerializedProperty _imageLinks;
        SerializedProperty _reagents;
        SerializedProperty _equipment;

        private bool _experimentTextFields = false;
        private bool _experimentWebLinks = false;
        #endregion

        private void OnEnable()
        {
            _experimentTimeline = serializedObject.FindProperty("_experimentTimeline");
            _overwriteCameraPosition = serializedObject.FindProperty("_overwriteCameraPosition");
            _startCameraPosition = serializedObject.FindProperty("_startCameraPosition");
            _header = serializedObject.FindProperty("_header");
            _description = serializedObject.FindProperty("_description");
            _webAudio = serializedObject.FindProperty("_webAudio");
            _videoUrl = serializedObject.FindProperty("_videoUrl");
            _imageLinks = serializedObject.FindProperty("_imageLinks");
            _reagents = serializedObject.FindProperty("_reagents");
            _equipment = serializedObject.FindProperty("_equipment");
        }

        public override void OnInspectorGUI()
        {
            ExperimentData experimentData = (ExperimentData)target;
            
            serializedObject.Update();

            EditorGUILayout.PropertyField(_experimentTimeline);
            
            EditorGUILayout.PropertyField(_overwriteCameraPosition);
            
            if (experimentData.OverwriteCameraPosition)
            {
                EditorGUILayout.PropertyField(_startCameraPosition);
            }

            _experimentTextFields = EditorGUILayout.BeginFoldoutHeaderGroup(_experimentTextFields, "Text Fields");
            if (_experimentTextFields)
            {
                EditorGUILayout.PropertyField(_header);
                EditorGUILayout.PropertyField(_description);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            
            _experimentWebLinks = EditorGUILayout.BeginFoldoutHeaderGroup(_experimentWebLinks, "Web Links");
            if (_experimentWebLinks)
            {
                EditorGUILayout.PropertyField(_webAudio);
                EditorGUILayout.PropertyField(_videoUrl);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            
            EditorGUILayout.PropertyField(_imageLinks);
            
            EditorGUILayout.PropertyField(_equipment);
            EditorGUILayout.PropertyField(_reagents);
            

            serializedObject.ApplyModifiedProperties();
        }
    }
}
