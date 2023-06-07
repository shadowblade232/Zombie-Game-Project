using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace EasyUIStyle.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(EasyUITMProText))]

    public class EasyUITMProTextInspector : TMPro.EditorUtilities.TMP_EditorPanelUI //For old versions of TMP (2.01 or less) change "TMP_EditorPanelUI" to "TMP_UIEditorPanelUI"
    {
        private EasyUITMProText _target;
        private List<string> styleList = new List<string>();
        private int styleIndex;
        private GUISkin skin;

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
            _target = (EasyUITMProText)target;

            if (EasyUIComponentCore<EasyUI_TMPTextStyle>.easyUI_Data == null)
                    _target.easyUICore.Initialize();

            styleList = EasyUI_HelperFunctions.GetStylesTMP<EasyUI_TMPTextStyle>();

            serializedObject.Update();
            SerializedProperty styleIndex = serializedObject.FindProperty("styleIndex");

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Style", skin.GetStyle("StyleHeading"), GUILayout.Width(100));
            
            styleIndex.intValue = EditorGUILayout.Popup(styleIndex.intValue, EasyUI_HelperFunctions.CreateStyleList(styleList));
            if (GUI.changed)
                foreach (Object obj in targets)
                {
                    ((EasyUITMProText)obj).easyUICore.styleIndex = serializedObject.FindProperty("styleIndex").intValue;
                }

            EditorGUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Edit Styles", GUILayout.Width(85)))
            {
                EditorWindow.GetWindow(typeof(EasyUIStyleEditor));
                EditorWindow.GetWindow(typeof(EasyUIStyleEditor)).minSize = new Vector2(400, 320);
                {
                    EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.TextMeshPro;
                    EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
                    EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, styleIndex.intValue);
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
