using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace EasyUIStyle.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(StyleChooser))]
    public class Style_Chooser_Inspector : Editor
    {

        private string[] styles;
        private List<string> styleList = new List<string>();
        private StyleChooser _chooserScript;
        private GUISkin skin;

        //Used to allow multiple object editing
        SerializedProperty imageStyleIndex;
        SerializedProperty textStyleIndex;
        SerializedProperty buttonStyleIndex;
        SerializedProperty sliderStyleIndex;
        SerializedProperty inputStyleIndex;
        SerializedProperty toggleStyleIndex;
        SerializedProperty dropdownStyleIndex;
        SerializedProperty tmproTextStyleIndex;
        SerializedProperty tmproInputStyleIndex;
        SerializedProperty tmproDropdownStyleIndex;

        //stores editor window
        static EditorWindow ctsWindow = null;

        void OnEnable()
        {
            GetSkin();

            imageStyleIndex = serializedObject.FindProperty("imageStyleIndex");
            textStyleIndex = serializedObject.FindProperty("textStyleIndex");
            buttonStyleIndex = serializedObject.FindProperty("buttonStyleIndex");
            sliderStyleIndex = serializedObject.FindProperty("sliderStyleIndex");
            inputStyleIndex = serializedObject.FindProperty("inputStyleIndex");
            toggleStyleIndex = serializedObject.FindProperty("toggleStyleIndex");
            dropdownStyleIndex = serializedObject.FindProperty("dropdownStyleIndex");
            tmproTextStyleIndex = serializedObject.FindProperty("tmproTextStyleIndex");
            tmproInputStyleIndex = serializedObject.FindProperty("tmproInputStyleIndex");
            tmproDropdownStyleIndex = serializedObject.FindProperty("tmproDropdownStyleIndex");

            if (ctsWindow == null)
                ctsWindow = null;//dummy line to lose error
        }

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                GUILayout.Label("Sorry, can't Edit In Play Mode");
                return;
            }

            serializedObject.Update();

            if (_chooserScript == null)
                _chooserScript = (StyleChooser)target;

            if (_chooserScript.easyUI_Data == null)
            {
                _chooserScript.easyUI_Data = EasyUI_HelperFunctions.LoadData();
                GUILayout.Label("Didn't find the data...?");
                return;
            }

            if (skin == null)
                GetSkin();

            GUILayout.BeginHorizontal();

            GUILayout.Label("Easy UI Style Chooser", skin.GetStyle("InspectorHeading"));
            if (GUILayout.Button("Edit \n Styles", GUILayout.Width(75)))
            {
                EditorWindow.GetWindow(typeof(EasyUIStyleEditor));
                EditorWindow.GetWindow(typeof(EasyUIStyleEditor)).minSize = new Vector2(400, 320);

                if (_chooserScript.hasButton)
                {
                    EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.Button;
                    EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
                    EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, buttonStyleIndex.intValue);
                }
                else if (_chooserScript.hasImage)
                {
                    EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.Image;
                    EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
                    EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, imageStyleIndex.intValue);
                }
                else if (_chooserScript.hasText)
                {
                    EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.Text;
                    EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
                    EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, textStyleIndex.intValue);
                }
                else if (_chooserScript.hasInput)
                {
                    EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.InputField;
                    EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
                    EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, inputStyleIndex.intValue);
                }
                else if (_chooserScript.hasSlider)
                {
                    EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.Slider;
                    EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
                    EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, sliderStyleIndex.intValue);
                }
                else if (_chooserScript.hasToggle)
                {
                    EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.Toggle;
                    EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
                    EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, toggleStyleIndex.intValue);
                }
                else if (_chooserScript.hasDropdown)
                {
                    EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.Dropdown;
                    EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
                    EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, dropdownStyleIndex.intValue);
                }
                else if (_chooserScript.hasTMProText)
                {
                    EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.TextMeshPro;
                    EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
                    EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, tmproTextStyleIndex.intValue);
                }

            }
            GUILayout.EndHorizontal();



            EditorGUI.indentLevel++;
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (_chooserScript.hasImage)
                EditorGUILayout.LabelField("Image Style", skin.GetStyle("StyleHeading"), GUILayout.Width(100));
            else if (_chooserScript.hasText)
                EditorGUILayout.LabelField("Text Style", skin.GetStyle("StyleHeading"), GUILayout.Width(100));
            else if (_chooserScript.hasTMProText)
                EditorGUILayout.LabelField("TMPro Text Style", skin.GetStyle("StyleHeading"), GUILayout.Width(100));

            //reset style number
            styleList.Clear();

            //populate styles
            if (_chooserScript.hasImage)
            {
                for (int i = 0; i < _chooserScript.easyUI_Data.imageList.Count; i++)
                {
                    styleList.Add(_chooserScript.easyUI_Data.imageList[i].styleName);
                }
            }
            else if (_chooserScript.hasText)
            {
                for (int i = 0; i < _chooserScript.easyUI_Data.textList.Count; i++)
                {
                    styleList.Add(_chooserScript.easyUI_Data.textList[i].styleName);
                }
            }
            else if (_chooserScript.hasTMProText)
            {
                for (int i = 0; i < _chooserScript.easyUI_Data.tmproTextList.Count; i++)
                {
                    styleList.Add(_chooserScript.easyUI_Data.tmproTextList[i].styleName);
                }
            }

            styles = CreateStyleList(styleList);

            //Choose Style
            if (_chooserScript.hasImage)
            {
                imageStyleIndex.intValue = EditorGUILayout.Popup(imageStyleIndex.intValue, styles);
            }
            else if (_chooserScript.hasText)
            {
                textStyleIndex.intValue = EditorGUILayout.Popup(textStyleIndex.intValue, styles);
            }
            else if (_chooserScript.hasTMProText)
            {
                tmproTextStyleIndex.intValue = EditorGUILayout.Popup(tmproTextStyleIndex.intValue, styles);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            //Secondary Style
            if (_chooserScript.hasButton)
                EditorGUILayout.LabelField("Button Style", skin.GetStyle("StyleHeading"), GUILayout.Width(100));
            else if (_chooserScript.hasToggle)
                EditorGUILayout.LabelField("Toggle Style", skin.GetStyle("StyleHeading"), GUILayout.Width(100));
            else if (_chooserScript.hasSlider)
                EditorGUILayout.LabelField("Slider Style", skin.GetStyle("StyleHeading"), GUILayout.Width(100));
            else if (_chooserScript.hasInput)
                EditorGUILayout.LabelField("Input Style", skin.GetStyle("StyleHeading"), GUILayout.Width(100));
            else if (_chooserScript.hasDropdown)
                EditorGUILayout.LabelField("Dropdown Style", skin.GetStyle("StyleHeading"), GUILayout.Width(100));
            else if (_chooserScript.hasTMProInput)
                EditorGUILayout.LabelField("TMP Input Style", skin.GetStyle("StyleHeading"), GUILayout.Width(100));
            else if (_chooserScript.hasTMProDropdown)
                EditorGUILayout.LabelField("TMP Dropdown", skin.GetStyle("StyleHeading"), GUILayout.Width(100));


            styleList.Clear();
            //populate secondary style
            if (_chooserScript.hasButton)
            {
                for (int i = 0; i < _chooserScript.easyUI_Data.buttonList.Count; i++)
                {
                    styleList.Add(_chooserScript.easyUI_Data.buttonList[i].styleName);
                }
            }
            else if (_chooserScript.hasToggle)
            {
                for (int i = 0; i < _chooserScript.easyUI_Data.toggleList.Count; i++)
                {
                    styleList.Add(_chooserScript.easyUI_Data.toggleList[i].styleName);
                }
            }
            else if (_chooserScript.hasSlider)
            {
                for (int i = 0; i < _chooserScript.easyUI_Data.sliderList.Count; i++)
                {
                    styleList.Add(_chooserScript.easyUI_Data.sliderList[i].styleName);
                }
            }
            else if (_chooserScript.hasInput)
            {
                for (int i = 0; i < _chooserScript.easyUI_Data.inputList.Count; i++)
                {
                    styleList.Add(_chooserScript.easyUI_Data.inputList[i].styleName);
                }
            }
            else if (_chooserScript.hasDropdown)
            {
                for (int i = 0; i < _chooserScript.easyUI_Data.dropdownList.Count; i++)
                {
                    styleList.Add(_chooserScript.easyUI_Data.dropdownList[i].styleName);
                }
            }
            else if (_chooserScript.hasTMProInput)
            {
                for (int i = 0; i < _chooserScript.easyUI_Data.tmproInputList.Count; i++)
                {
                    styleList.Add(_chooserScript.easyUI_Data.tmproInputList[i].styleName);
                }
            }
            else if (_chooserScript.hasTMProDropdown)
            {
                for (int i = 0; i < _chooserScript.easyUI_Data.tmproDropdownList.Count; i++)
                {
                    styleList.Add(_chooserScript.easyUI_Data.tmproDropdownList[i].styleName);
                }
            }

            styles = CreateStyleList(styleList);

            //Choose secondary style
            if (_chooserScript.hasButton)
            {
                buttonStyleIndex.intValue = EditorGUILayout.Popup(buttonStyleIndex.intValue, styles);
            }
            else if (_chooserScript.hasToggle)
            {
                toggleStyleIndex.intValue = EditorGUILayout.Popup(toggleStyleIndex.intValue, styles);
            }
            else if (_chooserScript.hasSlider)
            {
                sliderStyleIndex.intValue = EditorGUILayout.Popup(sliderStyleIndex.intValue, styles);
            }
            else if (_chooserScript.hasInput)
            {
                inputStyleIndex.intValue = EditorGUILayout.Popup(inputStyleIndex.intValue, styles);
            }
            else if (_chooserScript.hasDropdown)
            {
                dropdownStyleIndex.intValue = EditorGUILayout.Popup(dropdownStyleIndex.intValue, styles);
            }
            else if (_chooserScript.hasTMProInput)
            {
                tmproInputStyleIndex.intValue = EditorGUILayout.Popup(tmproInputStyleIndex.intValue, styles);
            }
            else if (_chooserScript.hasTMProDropdown)
            {
                tmproDropdownStyleIndex.intValue = EditorGUILayout.Popup(tmproDropdownStyleIndex.intValue, styles);
            }

            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;

            //Ensures style is implemented with inspector toggle
            if (GUI.changed)
            {
                if (_chooserScript.easyUI_Data != null)
                    EditorUtility.SetDirty(_chooserScript.easyUI_Data);
            }


            serializedObject.ApplyModifiedProperties();
            //Used to Debug;
            //DrawDefaultInspector();
        }

        private void GetSkin()
        {
            if (EditorGUIUtility.isProSkin)
                skin = EditorGUIUtility.Load("Assets/EasyUIStyles/Resources/EasyUI_DarkSKin.guiskin") as GUISkin;
            else
                skin = EditorGUIUtility.Load("Assets/EasyUIStyles/Resources/EasyUI_LightSkin.guiskin") as GUISkin;

            //attempt to find data in other folder
            if (skin == null)
            {
                string[] guids;
                if (EditorGUIUtility.isProSkin)
                    guids = UnityEditor.AssetDatabase.FindAssets("EasyUI_DarkSKin");
                else
                    guids = UnityEditor.AssetDatabase.FindAssets("EasyUI_LightSkin");

                if (guids.Length > 0)
                {
                    string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);

                    if (EditorGUIUtility.isProSkin)
                        skin = EditorGUIUtility.Load(path) as GUISkin;
                    else
                        skin = EditorGUIUtility.Load(path) as GUISkin;
                }
            }
        }

        void ClearToggles(int index, List<bool> onThisList)
        {
            for (int i = 0; i < onThisList.Count; i++)
            {
                if (i != index)
                    onThisList[i] = false;
            }
        }

        //create list of styles for display in inspector
        string[] CreateStyleList(List<string> _styles)
        {
            string[] stylesArray = new string[styleList.Count + 1];

            stylesArray[0] = "--- None ---";

            for (int i = 0; i < _styles.Count; i++)
            {
                stylesArray[i + 1] = (i + 1).ToString() + ". " + _styles[i];
            }

            return stylesArray;
        }

        //called from editor window
        //allows inspector to open editor window from button
        public static void SetWindow(EditorWindow win)
        {
            ctsWindow = win;
        }
    }
}
