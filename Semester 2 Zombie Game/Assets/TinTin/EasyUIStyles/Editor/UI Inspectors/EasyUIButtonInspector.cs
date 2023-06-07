using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace EasyUIStyle.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(EasyUIButton))]
    public class EasyUIButtonInspector : UnityEditor.UI.ButtonEditor
    {
        private EasyUIButton _target;
        private List<string> styleList = new List<string>();
        private GUISkin skin;
        SerializedProperty styleIndex;

        private new void OnEnable()
        {
            base.OnEnable();
            skin = EasyUI_HelperFunctions.GetSkin();
        }

        private new void OnDisable()
        {
            base.OnDisable();
        }

        public override void OnInspectorGUI()
        {
            _target = (EasyUIButton)target;

            if (EasyUIComponentCore<EasyUI_ButtonStyle>.easyUI_Data == null)
                    _target.easyUICore.Initialize();

            styleList = EasyUI_HelperFunctions.GetStyles<EasyUI_ButtonStyle>();

            serializedObject.Update();
            styleIndex = serializedObject.FindProperty("styleIndex");

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Style", skin.GetStyle("StyleHeading"), GUILayout.Width(100));
            
            //this fixes the ugui display issue when a style is deleted
            //by setting the serialized value we call "on validate" and update styles
            styleIndex.intValue = EditorGUILayout.Popup(styleIndex.intValue, EasyUI_HelperFunctions.CreateStyleList(styleList));
            if (GUI.changed)
                foreach (Object obj in targets)
                {
                    ((EasyUIButton)obj).easyUICore.styleIndex = serializedObject.FindProperty("styleIndex").intValue;
                }

            EditorGUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Edit Styles", GUILayout.Width(85)))
            {
                EditorWindow.GetWindow(typeof(EasyUIStyleEditor));
                EditorWindow.GetWindow(typeof(EasyUIStyleEditor)).minSize = new Vector2(400, 320);
                {
                    EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.Button;
                    EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
                    EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, _target.easyUICore.styleIndex);
                }
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            base.OnInspectorGUI();


        }
    }
}
