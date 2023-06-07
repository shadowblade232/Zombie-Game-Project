using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace EasyUIStyle.UI
{
    public class EasyUIStyleEditor : EditorWindow
    {

        [MenuItem("Tools/Easy UI Style Manager")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(EasyUIStyleEditor),true, "Easy UI Styles");
            EditorWindow.GetWindow(typeof(EasyUIStyleEditor)).minSize = new Vector2(400, 320);
        }

        //remove Lists
        List<EasyUI_ImageStyle> removeImageList = new List<EasyUI_ImageStyle>();
        List<EasyUI_TextStyle> removeTextList = new List<EasyUI_TextStyle>();
        List<EasyUI_ButtonStyle> removeButtonList = new List<EasyUI_ButtonStyle>();
        List<EasyUI_ToggleStyle> removeToggleList = new List<EasyUI_ToggleStyle>();
        List<EasyUI_InputStyle> removeInputList = new List<EasyUI_InputStyle>();
        List<EasyUI_SliderStyle> removeSliderList = new List<EasyUI_SliderStyle>();
        List<EasyUI_DropdownStyle> removeDropdownList = new List<EasyUI_DropdownStyle>();
        List<EasyUI_TMPTextStyle> removeTMProTextList = new List<EasyUI_TMPTextStyle>();
        List<EasyUI_TMPInputStyle> removeTMProInputList = new List<EasyUI_TMPInputStyle>();
        List<EasyUI_TMPDropDownStyle> removeTMProDropdownList = new List<EasyUI_TMPDropDownStyle>();

        List<MoveInfo> moveList = new List<MoveInfo>();

        //Duplicate containers    
        EasyUIStyle_Base duplicateStyle;


        public enum _type
        {
            Image,
            Text,
            TextMeshPro,
            Button,
            Toggle,
            Slider,
            InputField,
            TextMeshProInput,
            Dropdown,
            TextMeshProDropdown,
        }
        public static _type thisType; //public and static so it can be set from inspector...
        
        private static EasyUI_Style_Data easyUI_Data;
        GUISkin skin;
        Vector2 scrollPos = new Vector2();
        float toggleWidth = 50f;

        //stored for GUI sizing
        EditorWindow window;

        void OnEnable()
        {
            if (EditorGUIUtility.isProSkin)
                skin = EditorGUIUtility.Load("Assets/EasyUIStyles/Resources/EasyUI_DarkSKin.guiskin") as GUISkin;
            else
                skin = EditorGUIUtility.Load("Assets/EasyUIStyles/Resources/EasyUI_LightSkin.guiskin") as GUISkin;

            SetWindow();
            window = EditorWindow.GetWindow(typeof(EasyUIStyleEditor));

            LoadData();

        }

        void OnDisable()
        {
            UpdateAsset();
        }

        //Gets data and sets lists
        void LoadData()
        {
            easyUI_Data = AssetDatabase.LoadAssetAtPath("Assets/EasyUIStyles/Resources/EasyUIData.asset", typeof(EasyUI_Style_Data)) as EasyUI_Style_Data;

            //attempt to find data in other folder
            if (easyUI_Data == null)
            {
                string[] guids = UnityEditor.AssetDatabase.FindAssets("EasyUIData");
                if (guids.Length > 0)
                {
                    string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
                    easyUI_Data = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(EasyUI_Style_Data)) as EasyUI_Style_Data;
                }
            }

            //if still can't find it - create new version
            if (easyUI_Data == null)
            {
                EasyUI_Style_Data data = ScriptableObject.CreateInstance<EasyUI_Style_Data>();
                string assetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/EasyUIData.asset");
                Debug.Log("Couldn't Find Easy UI Data. Created new copy in Assets. Feel free to move it :) ");
                AssetDatabase.CreateAsset(data, assetPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                return;
            }
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

        void OnGUI()
        {
            //Check if data found
            // if not search again
            if (Event.current.type != EventType.Repaint)
            {
                if (easyUI_Data == null)
                {
                    LoadData();
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (skin == null)
                GetSkin();

            GUILayout.Box("Easy UI Styles - Manager", skin.GetStyle("EditorHeading"));

            EditorGUILayout.Space();
            EditorGUI.indentLevel++;

            thisType = (_type)EditorGUILayout.EnumPopup("UI Style To Edit", thisType);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Add " + thisType.ToString() + " Style"))
            {
                switch (thisType)
                {
                    case _type.Image:
                        AddImage();
                        break;
                    case _type.Text:
                        AddText();
                        break;
                    case _type.Button:
                        AddButton();
                        break;
                    case _type.Toggle:
                        AddToggle();
                        break;
                    case _type.Slider:
                        AddSlider();
                        break;
                    case _type.InputField:
                        AddInput();
                        break;
                    case _type.Dropdown:
                        AddDropDown();
                        break;
                    case _type.TextMeshPro:
                        AddTMProText();
                        break;
                    case _type.TextMeshProInput:
                        AddTMProInput();
                        break;
                    case _type.TextMeshProDropdown:
                        AddTMProDropdown();
                        break;
                }

                UpdateAsset();
            }

            if (GUILayout.Button("Minimize All"))
            {
                MaximizeAll(thisType, false);
            }
            if (GUILayout.Button("Maximize All"))
            {
                MaximizeAll(thisType, true);
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            //EditorGUILayout.LabelField("",GUILayout.Width(width),GUILayout.Height(height));

            if (thisType == _type.Image)
                DrawImageUI();
            else if (thisType == _type.Text)
                DrawTextUI();
            else if (thisType == _type.Button)
                DrawButtonUI();
            else if (thisType == _type.Toggle)
                DrawToggleUI();
            else if (thisType == _type.Slider)
                DrawSliderUI();
            else if (thisType == _type.InputField)
                DrawInputUI();
            else if (thisType == _type.Dropdown)
                DrawDropDownUI();
            //else if (thisType == _type.Settings)
            //    DrawSettingsUI();
            else if (thisType == _type.TextMeshPro)
                DrawTMProTextUI();
            else if (thisType == _type.TextMeshProInput)
                DrawTMProInputUI();
            else if (thisType == _type.TextMeshProDropdown)
                DrawTMProDropdownUI();

            //not sure why so many are needed... there is certainly a better way to do this
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.EndScrollView();

            CleanUp();

            if (easyUI_Data != null)
            {
                if (easyUI_Data.imageList.Count == 0)
                    Debug.LogError("list is empty!");
            }
            else
                LoadData();

            if (GUI.changed && easyUI_Data != null)
            {
                StyleChooser.UpdateStyle();

                if (thisType == _type.Image)
                    EasyUIImage.UpdateStyle();
                else if (thisType == _type.Text)
                    EasyUIText.UpdateStyle();
                else if (thisType == _type.Button)
                    EasyUIButton.UpdateStyle();
                else if (thisType == _type.Toggle)
                    EasyUIToggle.UpdateStyle();
                else if (thisType == _type.Slider)
                    EasyUISlider.UpdateStyle();
                else if (thisType == _type.InputField)
                    EasyUIInputField.UpdateStyle();
                else if (thisType == _type.Dropdown)
                    EasyUIDropdown.UpdateStyle();
                //else if (thisType == _type.Settings)
                //    DrawSettingsUI();
                else if (thisType == _type.TextMeshPro)
                    EasyUITMProText.UpdateStyle();
                else if (thisType == _type.TextMeshProInput)
                    EasyUITMProInput.UpdateStyle();
                else if (thisType == _type.TextMeshProDropdown)
                    EasyUITMProDropdown.UpdateStyle();
                EditorUtility.SetDirty(easyUI_Data);
            }
        }

        void AddText()
        {
            if (easyUI_Data == null)
                return;

            EasyUI_TextStyle newText = new EasyUI_TextStyle();
            easyUI_Data.textList.Add(newText);
            newText.styleName = "Text Style " + easyUI_Data.textList.Count;
            newText.displayOrder = easyUI_Data.textList.Count - 1;
        }
        void AddImage()
        {
            if (easyUI_Data == null)
                return;

            EasyUI_ImageStyle newImage = new EasyUI_ImageStyle();
            easyUI_Data.imageList.Add(newImage);
            newImage.styleName = "Image Style " + easyUI_Data.imageList.Count;
            newImage.displayOrder = easyUI_Data.imageList.Count - 1;
            newImage.previousOrder = easyUI_Data.imageList.Count - 1;
        }
        void AddButton()
        {
            if (easyUI_Data == null)
                return;

            EasyUI_ButtonStyle newButton = new EasyUI_ButtonStyle();
            easyUI_Data.buttonList.Add(newButton);
            newButton.styleName = "Button Style " + easyUI_Data.buttonList.Count;
        }
        void AddToggle()
        {
            if (easyUI_Data == null)
                return;

            EasyUI_ToggleStyle newToggle = new EasyUI_ToggleStyle();
            easyUI_Data.toggleList.Add(newToggle);
            newToggle.styleName = "Toggle Style " + easyUI_Data.toggleList.Count;
        }
        void AddSlider()
        {
            if (easyUI_Data == null)
                return;

            EasyUI_SliderStyle newSlider = new EasyUI_SliderStyle();
            easyUI_Data.sliderList.Add(newSlider);
            newSlider.styleName = "Slider Style " + easyUI_Data.sliderList.Count;
        }
        void AddInput()
        {
            if (easyUI_Data == null)
                return;

            EasyUI_InputStyle newInput = new EasyUI_InputStyle();
            easyUI_Data.inputList.Add(newInput);
            newInput.styleName = "Input Style " + easyUI_Data.inputList.Count;
        }
        void AddDropDown()
        {
            if (easyUI_Data == null)
                return;

            EasyUI_DropdownStyle newDropDown = new EasyUI_DropdownStyle();
            easyUI_Data.dropdownList.Add(newDropDown);
            newDropDown.styleName = "Dropdown Style " + easyUI_Data.dropdownList.Count;
        }

        void AddTMProText()
        {
            if (easyUI_Data == null)
                return;

            EasyUI_TMPTextStyle newTMProText = new EasyUI_TMPTextStyle();
            newTMProText.styleName = "TMP Text Style " + easyUI_Data.tmproTextList.Count;
            easyUI_Data.tmproTextList.Add(newTMProText);
        }

        void AddTMProInput()
        {
            if (easyUI_Data == null)
                return;

            EasyUI_TMPInputStyle newTMProText = new EasyUI_TMPInputStyle();
            newTMProText.styleName = "TMP Input Style " + easyUI_Data.tmproInputList.Count;

            easyUI_Data.tmproInputList.Add(newTMProText);
        }

        void AddTMProDropdown()
        {
            if (easyUI_Data == null)
                return;

            EasyUI_TMPDropDownStyle newTMProText = new EasyUI_TMPDropDownStyle();
            newTMProText.styleName = "TMP Dropdown Style " + easyUI_Data.tmproDropdownList.Count;
            easyUI_Data.tmproDropdownList.Add(newTMProText);
        }

        void DuplicateStyle(EasyUIStyle_Base _style)
        {
            if (easyUI_Data == null)
                return;

            if (_style is EasyUI_ImageStyle)
                duplicateStyle = _style.MakeCopy() as EasyUI_ImageStyle;
            else if (_style is EasyUI_TextStyle)
                duplicateStyle = _style.MakeCopy() as EasyUI_TextStyle;
            else if (_style is EasyUI_SliderStyle)
                duplicateStyle = _style.MakeCopy() as EasyUI_SliderStyle;
            else if (_style is EasyUI_TMPInputStyle)
                duplicateStyle = _style.MakeCopy() as EasyUI_TMPInputStyle;
            else if (_style is EasyUI_InputStyle)
                duplicateStyle = _style.MakeCopy() as EasyUI_InputStyle;
            else if (_style is EasyUI_ToggleStyle)
                duplicateStyle = _style.MakeCopy() as EasyUI_ToggleStyle;
            else if (_style is EasyUI_TMPDropDownStyle)
                duplicateStyle = _style.MakeCopy() as EasyUI_TMPDropDownStyle;
            else if (_style is EasyUI_DropdownStyle)
                duplicateStyle = _style.MakeCopy() as EasyUI_DropdownStyle;
            else if (_style is EasyUI_ButtonStyle)
                duplicateStyle = _style.MakeCopy() as EasyUI_ButtonStyle;
            else if (_style is EasyUI_TMPTextStyle)
                duplicateStyle = _style.MakeCopy() as EasyUI_TMPTextStyle;
        }

        //used from context menu
        public static void AddStyle(UnityEngine.UI.Text text)
        {
            //set style
            EasyUI_Style_Data _EasyUI_Data = EasyUI_HelperFunctions.LoadData();
            EasyUI_TextStyle newTextStyle = new EasyUI_TextStyle();
            newTextStyle = newTextStyle.MakeCopy(text);
            //newTextStyle.styleName = text.gameObject.name + " Style";
            _EasyUI_Data.textList.Add(newTextStyle);

            //add style chooser
            StyleChooser sc = AddStyleChooser(text.gameObject);
            sc.textStyleIndex = _EasyUI_Data.textList.Count;
        }

        public static void AddStyle(UnityEngine.UI.Image image)
        {
            //set style
            EasyUI_Style_Data _EasyUI_Data = EasyUI_HelperFunctions.LoadData();
            EasyUI_ImageStyle newImageStyle = new EasyUI_ImageStyle();
            newImageStyle = newImageStyle.MakeCopy(image);
            _EasyUI_Data.imageList.Add(newImageStyle);

            //add style chooser
            StyleChooser sc = AddStyleChooser(image.gameObject);
            sc.imageStyleIndex = _EasyUI_Data.imageList.Count;
        }

        public static void AddStyle(UnityEngine.UI.Button button)
        {
            //set style
            EasyUI_Style_Data _EasyUI_Data = EasyUI_HelperFunctions.LoadData();
            EasyUI_ButtonStyle newStyle = new EasyUI_ButtonStyle();
            newStyle = newStyle.MakeCopy(button);
            _EasyUI_Data.buttonList.Add(newStyle);

            //add style chooser
            StyleChooser sc = AddStyleChooser(button.gameObject);
            sc.buttonStyleIndex = _EasyUI_Data.buttonList.Count;
        }

        public static void AddStyle(UnityEngine.UI.Toggle toggle)
        {
            //set style
            EasyUI_Style_Data _EasyUI_Data = EasyUI_HelperFunctions.LoadData();
            EasyUI_ToggleStyle newStyle = new EasyUI_ToggleStyle();
            newStyle = newStyle.MakeCopy(toggle);
            _EasyUI_Data.toggleList.Add(newStyle);

            //add style chooser
            StyleChooser sc = AddStyleChooser(toggle.gameObject);
            sc.toggleStyleIndex = _EasyUI_Data.toggleList.Count;
        }

        public static void AddStyle(UnityEngine.UI.Slider slider)
        {
            //set style
            EasyUI_Style_Data _EasyUI_Data = EasyUI_HelperFunctions.LoadData();
            EasyUI_SliderStyle newStyle = new EasyUI_SliderStyle();
            newStyle = newStyle.MakeCopy(slider);
            _EasyUI_Data.sliderList.Add(newStyle);

            //add style chooser
            StyleChooser sc = AddStyleChooser(slider.gameObject);
            sc.sliderStyleIndex = _EasyUI_Data.sliderList.Count;
        }

        public static void AddStyle(UnityEngine.UI.InputField inputField)
        {
            //set style
            EasyUI_Style_Data _EasyUI_Data = EasyUI_HelperFunctions.LoadData();
            EasyUI_InputStyle newStyle = new EasyUI_InputStyle();
            newStyle = newStyle.MakeCopy(inputField);
            _EasyUI_Data.inputList.Add(newStyle);

            //add style chooser
            StyleChooser sc = AddStyleChooser(inputField.gameObject);
            sc.inputStyleIndex = _EasyUI_Data.inputList.Count;
        }

        public static void AddStyle(TMPro.TMP_Text text)
        {
            //set style
            EasyUI_Style_Data _EasyUI_Data = EasyUI_HelperFunctions.LoadData();
            EasyUI_TMPTextStyle newStyle = new EasyUI_TMPTextStyle();
            newStyle = newStyle.MakeCopy(text);
            _EasyUI_Data.tmproTextList.Add(newStyle);

            //add style chooser
            StyleChooser sc = AddStyleChooser(text.gameObject);
            sc.tmproTextStyleIndex = _EasyUI_Data.tmproTextList.Count;
        }

        public static void AddStyle(TMPro.TMP_InputField tmpInput)
        {
            //set style
            EasyUI_Style_Data _EasyUI_Data = EasyUI_HelperFunctions.LoadData();
            EasyUI_TMPInputStyle newStyle = new EasyUI_TMPInputStyle();
            newStyle = newStyle.MakeCopy(tmpInput);
            //newImageStyle.styleName = Image.gameObject.name + " Style";
            _EasyUI_Data.tmproInputList.Add(newStyle);

            //add style chooser
            StyleChooser sc = AddStyleChooser(tmpInput.gameObject);
            sc.tmproInputStyleIndex = _EasyUI_Data.tmproInputList.Count;
        }

        public static void AddStyle(TMPro.TMP_Dropdown tmpDropDown)
        {
            //set style
            EasyUI_Style_Data _EasyUI_Data = EasyUI_HelperFunctions.LoadData();
            EasyUI_TMPDropDownStyle newStyle = new EasyUI_TMPDropDownStyle();
            newStyle = newStyle.MakeCopy(tmpDropDown);

            _EasyUI_Data.tmproDropdownList.Add(newStyle);

            //add style chooser
            StyleChooser sc = AddStyleChooser(tmpDropDown.gameObject);
            sc.tmproDropdownStyleIndex = _EasyUI_Data.tmproDropdownList.Count;
        }

        public static void AddStyle(UnityEngine.UI.Dropdown dropDown)
        {
            //set style
            EasyUI_Style_Data _EasyUI_Data = EasyUI_HelperFunctions.LoadData();
            EasyUI_DropdownStyle newStyle = new EasyUI_DropdownStyle();
            newStyle = newStyle.MakeCopy(dropDown);

            _EasyUI_Data.dropdownList.Add(newStyle);

            //add style chooser
            StyleChooser sc = AddStyleChooser(dropDown.gameObject);
            sc.dropdownStyleIndex = _EasyUI_Data.dropdownList.Count;
        }

        public static StyleChooser AddStyleChooser(GameObject go)
        {
            if (go.GetComponent<StyleChooser>() == null)
                return go.AddComponent<StyleChooser>();
            else
                return go.GetComponent<StyleChooser>();
        }

        //Cleans up lists of UI styles
        //Needs to be done this way to avoid enumeration errors caused by GUI refresh
        void CleanUp()
        {
            if (easyUI_Data == null)
                return;

            foreach (MoveInfo moveInfo in moveList)
                MoveStyle(moveInfo._style, moveInfo.moveUp);

            foreach (EasyUI_TextStyle t in removeTextList)
            {
                EasyUIComponentCore<EasyUI_TextStyle>.StyleDeleted(t as EasyUI_TextStyle);
                easyUI_Data.textList.Remove(t);
                removeTextList.Remove(t);
                break;
            }
            foreach (EasyUI_ImageStyle i in removeImageList)
            {
                EasyUIComponentCore<EasyUI_ImageStyle>.StyleDeleted(i as EasyUI_ImageStyle);
                easyUI_Data.imageList.Remove(i);
                removeImageList.Remove(i);
                break;
            }
            foreach (EasyUI_ButtonStyle b in removeButtonList)
            {
                EasyUIComponentCore<EasyUI_ButtonStyle>.StyleDeleted(b as EasyUI_ButtonStyle);
                easyUI_Data.buttonList.Remove(b);
                removeButtonList.Remove(b);
                break;
            }
            foreach (EasyUI_ToggleStyle t in removeToggleList)
            {
                EasyUIComponentCore<EasyUI_ToggleStyle>.StyleDeleted(t as EasyUI_ToggleStyle);
                easyUI_Data.toggleList.Remove(t);
                removeToggleList.Remove(t);
                break;
            }
            foreach (EasyUI_SliderStyle s in removeSliderList)
            {
                EasyUIComponentCore<EasyUI_SliderStyle>.StyleDeleted(s as EasyUI_SliderStyle);
                easyUI_Data.sliderList.Remove(s);
                removeSliderList.Remove(s);
                break;
            }
            foreach (EasyUI_InputStyle i in removeInputList)
            {
                EasyUIComponentCore<EasyUI_InputStyle>.StyleDeleted(i as EasyUI_InputStyle);
                easyUI_Data.inputList.Remove(i);
                removeInputList.Remove(i);
                break;
            }
            foreach (EasyUI_DropdownStyle d in removeDropdownList)
            {
                EasyUIComponentCore<EasyUI_DropdownStyle>.StyleDeleted(d as EasyUI_DropdownStyle);
                easyUI_Data.dropdownList.Remove(d);
                removeDropdownList.Remove(d);
                break;
            }
            foreach (EasyUI_TMPTextStyle t in removeTMProTextList)
            {
                EasyUIComponentCore<EasyUI_TMPTextStyle>.StyleDeleted(t as EasyUI_TMPTextStyle);
                easyUI_Data.tmproTextList.Remove(t);
                removeTMProTextList.Remove(t);
                break;
            }
            foreach (EasyUI_TMPInputStyle i in removeTMProInputList)
            {
                EasyUIComponentCore<EasyUI_TMPInputStyle>.StyleDeleted(i as EasyUI_TMPInputStyle);
                easyUI_Data.tmproInputList.Remove(i);
                removeTMProInputList.Remove(i);
                break;
            }
            foreach (EasyUI_TMPDropDownStyle d in removeTMProDropdownList)
            {
                EasyUIComponentCore<EasyUI_TMPDropDownStyle>.StyleDeleted(d as EasyUI_TMPDropDownStyle);
                easyUI_Data.tmproDropdownList.Remove(d);
                removeTMProDropdownList.Remove(d);
                break;
            }

            if (duplicateStyle != null)
            {
                if (duplicateStyle is EasyUI_ImageStyle)
                    easyUI_Data.imageList.Add(duplicateStyle as EasyUI_ImageStyle);
                else if (duplicateStyle is EasyUI_TextStyle)
                    easyUI_Data.textList.Add(duplicateStyle as EasyUI_TextStyle);
                else if (duplicateStyle is EasyUI_SliderStyle)
                    easyUI_Data.sliderList.Add(duplicateStyle as EasyUI_SliderStyle);
                else if (duplicateStyle is EasyUI_TMPInputStyle)
                    easyUI_Data.tmproInputList.Add(duplicateStyle as EasyUI_TMPInputStyle);
                else if (duplicateStyle is EasyUI_InputStyle)
                    easyUI_Data.inputList.Add(duplicateStyle as EasyUI_InputStyle);
                else if (duplicateStyle is EasyUI_ToggleStyle)
                    easyUI_Data.toggleList.Add(duplicateStyle as EasyUI_ToggleStyle);
                else if (duplicateStyle is EasyUI_TMPDropDownStyle)
                    easyUI_Data.tmproDropdownList.Add(duplicateStyle as EasyUI_TMPDropDownStyle);
                else if (duplicateStyle is EasyUI_DropdownStyle)
                    easyUI_Data.dropdownList.Add(duplicateStyle as EasyUI_DropdownStyle);
                else if (duplicateStyle is EasyUI_ButtonStyle)
                    easyUI_Data.buttonList.Add(duplicateStyle as EasyUI_ButtonStyle);
                else if (duplicateStyle is EasyUI_TMPTextStyle)
                    easyUI_Data.tmproTextList.Add(duplicateStyle as EasyUI_TMPTextStyle);

                duplicateStyle = null;
            }
        }

        void UpdateAsset()
        {
            if (easyUI_Data == null || Application.isPlaying)
                return;

            EditorUtility.SetDirty(easyUI_Data);
            //AssetDatabase.SaveAssets();
            //AssetDatabase.Refresh();

            if (easyUI_Data.imageList.Count == 0)
                Debug.LogError("list is empty!");
        }

        //Allows Easy UI window to be opened from inspector
        static void SetWindow()
        {
            Style_Chooser_Inspector.SetWindow(EditorWindow.GetWindow(typeof(EasyUIStyleEditor)));
        }

        void DrawImageUI()
        {
            float startHeight = 0f;

            for (int i = 0; i < easyUI_Data.imageList.Count; i++)
            {
                easyUI_Data.imageList[i].displayOrder = i;
            }

            foreach (EasyUI_ImageStyle i in easyUI_Data.imageList)
            {
                ///Code to adjust size of box area
                float lengthOfBox;
                if (!i.maximize)
                    lengthOfBox = 30f;
                else if (i.imageType == UnityEngine.UI.Image.Type.Filled)
                    lengthOfBox = 325f;
                else
                    lengthOfBox = 255f;

#if UNITY_2019_3_OR_NEWER
                lengthOfBox *= 1.05f;
#endif

                //blank label allows scrollview to expand
                GUILayout.Label("", GUILayout.Height(lengthOfBox));
                //background shading
                GUILayout.BeginArea(new Rect(10f, startHeight, window.position.width - 30f, lengthOfBox), skin.box);
                startHeight += lengthOfBox + 10f;

                DrawTopStyleButtons(i);

                if (i.maximize)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    i.styleName = EditorGUILayout.TextField("Style Name : ", i.styleName);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();

                    //EditorGUILayout.LabelField("  Sync", GUILayout.Width(toggleWidth));
                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.indentLevel--;

                    if (GUILayout.Button("Sync", GUILayout.Width(50)))
                    {
                        i.ToggleAll(!i.syncColor);
                    }
                    EditorGUI.indentLevel++;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncColor = EditorGUILayout.Toggle(i.syncColor, GUILayout.Width(toggleWidth));
                    i.color = EditorGUILayout.ColorField("Tint", i.color);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncMaterial = EditorGUILayout.Toggle(i.syncMaterial, GUILayout.Width(toggleWidth));
                    i.material = EditorGUILayout.ObjectField("Material", i.material, typeof(Material), false) as Material;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncRaycastTarget = EditorGUILayout.Toggle(i.syncRaycastTarget, GUILayout.Width(toggleWidth));
                    i.raycastTarget = EditorGUILayout.Toggle("Raycast Target", i.raycastTarget);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncSprite = EditorGUILayout.Toggle(i.syncSprite, GUILayout.Width(toggleWidth));
                    i.sprite = EditorGUILayout.ObjectField("Image", i.sprite, typeof(Sprite), false) as Sprite;
                    EditorGUILayout.EndHorizontal();


                    EditorGUILayout.BeginHorizontal();
                    i.syncImageType = EditorGUILayout.Toggle(i.syncImageType, GUILayout.Width(toggleWidth));
                    i.imageType = (UnityEngine.UI.Image.Type)EditorGUILayout.EnumPopup("Image Type", i.imageType);
                    EditorGUILayout.EndHorizontal();

                    if (i.imageType == UnityEngine.UI.Image.Type.Filled)
                    {
                        EditorGUILayout.BeginHorizontal();
                        i.syncFillMethod = EditorGUILayout.Toggle(i.syncFillMethod, GUILayout.Width(toggleWidth));
                        i.fillMethod = (UnityEngine.UI.Image.FillMethod)EditorGUILayout.EnumPopup("Fill Method", i.fillMethod);
                        EditorGUILayout.EndHorizontal();


                        EditorGUILayout.BeginHorizontal();
                        i.syncFillMethod = EditorGUILayout.Toggle(i.syncFillMethod, GUILayout.Width(toggleWidth));
                        EditorGUI.indentLevel++;
                        switch (i.fillMethod)
                        {
                            case UnityEngine.UI.Image.FillMethod.Horizontal:
                                i.originHorz = (UnityEngine.UI.Image.OriginHorizontal)EditorGUILayout.EnumPopup("Fill Method", i.originHorz);
                                break;
                            case UnityEngine.UI.Image.FillMethod.Vertical:
                                i.originVert = (UnityEngine.UI.Image.OriginVertical)EditorGUILayout.EnumPopup("Fill Method", i.originVert);
                                break;
                            case UnityEngine.UI.Image.FillMethod.Radial90:
                                i.origin90 = (UnityEngine.UI.Image.Origin90)EditorGUILayout.EnumPopup("Fill Method", i.origin90);
                                break;
                            case UnityEngine.UI.Image.FillMethod.Radial180:
                                i.origin180 = (UnityEngine.UI.Image.Origin180)EditorGUILayout.EnumPopup("Fill Method", i.origin180);
                                break;
                            case UnityEngine.UI.Image.FillMethod.Radial360:
                                i.origin360 = (UnityEngine.UI.Image.Origin360)EditorGUILayout.EnumPopup("Fill Method", i.origin360);
                                break;
                        }
                        EditorGUI.indentLevel--;
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        i.syncFillAmount = EditorGUILayout.Toggle(i.syncFillAmount, GUILayout.Width(toggleWidth));
                        EditorGUI.indentLevel++;
                        i.fillAmount = EditorGUILayout.Slider("Fill Amount", i.fillAmount, 0f, 1f);
                        EditorGUI.indentLevel--;
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        i.syncClockwise = EditorGUILayout.Toggle(i.syncClockwise, GUILayout.Width(toggleWidth));
                        EditorGUI.indentLevel++;
                        i.clockwise = EditorGUILayout.Toggle("Clockwise", i.clockwise);
                        EditorGUI.indentLevel--;
                        EditorGUILayout.EndHorizontal();
                    }


                    EditorGUILayout.BeginHorizontal();
                    i.syncPreserveAspect = EditorGUILayout.Toggle(i.syncPreserveAspect, GUILayout.Width(toggleWidth));
                    i.preserveAspect = EditorGUILayout.Toggle("Preserve Aspect", i.preserveAspect);
                    EditorGUILayout.EndHorizontal();

                    EditorGUI.indentLevel--;
                    EditorGUILayout.Space();

                }
                GUILayout.EndArea();

            }
        }

        void DrawTextUI()
        {
            float startHeight = 10f;

            foreach (EasyUI_TextStyle t in easyUI_Data.textList)
            {
                ///Code to adjust size of box area
                float lengthOfBox;
                if (!t.maximize)
                    lengthOfBox = 30f;
                else if (!t.resizeForBestFit)
                    lengthOfBox = 350f;
                else
                    lengthOfBox = 385f;

#if UNITY_2019_3_OR_NEWER
                lengthOfBox *= 1.05f;
#endif

                //blank label allows scrollview to expand
                GUILayout.Label("", GUILayout.Height(lengthOfBox));
                //background shading
                GUILayout.BeginArea(new Rect(10f, startHeight, window.position.width - 30f, lengthOfBox), skin.box);
                startHeight += lengthOfBox + 10f;

                DrawTopStyleButtons(t);

                //Don't display if not maximized
                if (t.maximize)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.Space();
                    t.styleName = EditorGUILayout.TextField("Edit Style Name : ", t.styleName);
                    EditorGUILayout.Space();


                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.indentLevel--;
                    if (GUILayout.Button("Sync", GUILayout.Width(50)))
                    {
                        t.ToggleAll(!t.syncFont);
                    }
                    EditorGUI.indentLevel++;
                    EditorGUILayout.EndHorizontal();

                    ///t.styleName = EditorGUILayout.TextField("Style Name : ", t.styleName);

                    EditorGUILayout.BeginHorizontal();
                    t.syncFont = EditorGUILayout.Toggle(t.syncFont, GUILayout.Width(toggleWidth));
                    t.font = EditorGUILayout.ObjectField("Font", t.font, typeof(Font), false) as Font;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    t.syncFontStyle = EditorGUILayout.Toggle(t.syncFontStyle, GUILayout.Width(toggleWidth));
                    t.fontStyle = (FontStyle)EditorGUILayout.EnumPopup("Font Style", t.fontStyle);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    t.syncFontSize = EditorGUILayout.Toggle(t.syncFontSize, GUILayout.Width(toggleWidth));
                    t.fontSize = EditorGUILayout.IntField("Font Size", t.fontSize);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    t.syncLineSpacing = EditorGUILayout.Toggle(t.syncLineSpacing, GUILayout.Width(toggleWidth));
                    t.lineSpacing = EditorGUILayout.FloatField("Line Spacing", t.lineSpacing);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    t.syncRichText = EditorGUILayout.Toggle(t.syncRichText, GUILayout.Width(toggleWidth));
                    t.richText = EditorGUILayout.Toggle("Rich Text", t.richText);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    t.syncTextAnchor = EditorGUILayout.Toggle(t.syncTextAnchor, GUILayout.Width(toggleWidth));
                    t.textAnchor = (TextAnchor)EditorGUILayout.EnumPopup("Text Alignment", t.textAnchor);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    t.syncAlignByGeometry = EditorGUILayout.Toggle(t.syncAlignByGeometry, GUILayout.Width(toggleWidth));
                    t.alignByGeometry = EditorGUILayout.Toggle("Align by Geometry", t.alignByGeometry);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    t.syncHorizontalWrapMode = EditorGUILayout.Toggle(t.syncHorizontalWrapMode, GUILayout.Width(toggleWidth));
                    t.horzWrap = (HorizontalWrapMode)EditorGUILayout.EnumPopup("Horizontal Overflow", t.horzWrap);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    t.syncVerticalWrapMode = EditorGUILayout.Toggle(t.syncVerticalWrapMode, GUILayout.Width(toggleWidth));
                    t.vertWrap = (VerticalWrapMode)EditorGUILayout.EnumPopup("Vertical Overflow", t.vertWrap);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    t.syncBestFit = EditorGUILayout.Toggle(t.syncBestFit, GUILayout.Width(toggleWidth));
                    t.resizeForBestFit = EditorGUILayout.Toggle("Best Fit", t.resizeForBestFit);
                    EditorGUILayout.EndHorizontal();


                    if (t.resizeForBestFit)
                    {
                        EditorGUI.indentLevel++;
                        EditorGUI.indentLevel++;
                        EditorGUI.indentLevel++;
                        EditorGUI.indentLevel++;
                        t.minSize = EditorGUILayout.IntField("Min Size", t.minSize);
                        t.maxSize = EditorGUILayout.IntField("Max Size", t.maxSize);
                        EditorGUI.indentLevel--;
                        EditorGUI.indentLevel--;
                        EditorGUI.indentLevel--;
                        EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    t.syncTextColor = EditorGUILayout.Toggle(t.syncTextColor, GUILayout.Width(toggleWidth));
                    t.textColor = EditorGUILayout.ColorField("Text Color", t.textColor);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    t.syncTexMat = EditorGUILayout.Toggle(t.syncTexMat, GUILayout.Width(toggleWidth));
                    t.textMat = EditorGUILayout.ObjectField("Material", t.textMat, typeof(Material), false) as Material;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    t.syncRaycastTarget = EditorGUILayout.Toggle(t.syncRaycastTarget, GUILayout.Width(toggleWidth));
                    t.isRaycastTarget = EditorGUILayout.Toggle("Raycast Target", t.isRaycastTarget);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUI.indentLevel--;
                }

                GUILayout.EndArea();
            }
        }

        void DrawButtonUI()
        {
            float startHeight = 10f;

            foreach (EasyUI_ButtonStyle b in easyUI_Data.buttonList)
            {
                float lengthOfBox;
                if (!b.maximize)
                    lengthOfBox = 30f;
                else
                {
                    lengthOfBox = 245f;
                    lengthOfBox += BoxDrawAdjust(b);
                }

#if UNITY_2019_3_OR_NEWER
                lengthOfBox *= 1.08f;
#endif
                //blank label allows scrollview to expand
                GUILayout.Label("", GUILayout.Height(lengthOfBox));
                //background shading
                GUILayout.BeginArea(new Rect(10f, startHeight, window.position.width - 30f, lengthOfBox), skin.box);
                startHeight += lengthOfBox + 10f;

                DrawButtonTemplate(b);

                GUILayout.EndArea();
            }
        }

        void DrawToggleUI()
        {
            float startHeight = 10f;

            foreach (EasyUI_ToggleStyle t in easyUI_Data.toggleList)
            {

                float lengthOfBox;
                if (!t.maximize)
                    lengthOfBox = 30f;
                else
                    lengthOfBox = 280f;

                lengthOfBox += BoxDrawAdjust(t);

#if UNITY_2019_3_OR_NEWER
                lengthOfBox *= 1.1f;
#endif
                //blank label allows scrollview to expand
                GUILayout.Label("", GUILayout.Height(lengthOfBox));
                //background shading
                GUILayout.BeginArea(new Rect(10f, startHeight, window.position.width - 30f, lengthOfBox), skin.box);
                startHeight += lengthOfBox + 10f;

                DrawButtonTemplate(t);

                if (t.maximize)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.BeginHorizontal();
                    t.syncIsOn = EditorGUILayout.Toggle(t.syncIsOn, GUILayout.Width(toggleWidth));
                    t.isOn = EditorGUILayout.Toggle("Is On", t.isOn);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    t.syncToggleTransition = EditorGUILayout.Toggle(t.syncToggleTransition, GUILayout.Width(toggleWidth));
                    t.toggleTransition = (UnityEngine.UI.Toggle.ToggleTransition)EditorGUILayout.EnumPopup("Toggle Transition", t.toggleTransition);
                    EditorGUILayout.EndHorizontal();
                    EditorGUI.indentLevel--;
                }

                GUILayout.EndArea();
            }
        }

        void DrawSliderUI()
        {
            float startHeight = 10f;

            foreach (EasyUI_SliderStyle s in easyUI_Data.sliderList)
            {
                float lengthOfBox;
                if (!s.maximize)
                    lengthOfBox = 30f;
                else
                    lengthOfBox = 340f;

                lengthOfBox += BoxDrawAdjust(s);

#if UNITY_2019_3_OR_NEWER
                lengthOfBox *= 1.07f;
#endif

                //blank label allows scrollview to expand
                GUILayout.Label("", GUILayout.Height(lengthOfBox));
                //background shading
                GUILayout.BeginArea(new Rect(10f, startHeight, window.position.width - 30f, lengthOfBox), skin.box);
                startHeight += lengthOfBox + 10f;

                DrawButtonTemplate(s);

                if (s.maximize)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.BeginHorizontal();
                    s.syncDirection = EditorGUILayout.Toggle(s.syncDirection, GUILayout.Width(toggleWidth));
                    s.direction = (UnityEngine.UI.Slider.Direction)EditorGUILayout.EnumPopup("Direction", s.direction);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    s.syncDirection = EditorGUILayout.Toggle(s.syncDirection, GUILayout.Width(toggleWidth));
                    s.minValue = EditorGUILayout.FloatField("Min Value", s.minValue);

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    s.syncMaxValue = EditorGUILayout.Toggle(s.syncMaxValue, GUILayout.Width(toggleWidth));
                    s.maxValue = EditorGUILayout.FloatField("Max Value", s.maxValue);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    s.syncWholeNumbers = EditorGUILayout.Toggle(s.syncWholeNumbers, GUILayout.Width(toggleWidth));
                    s.wholeNumbers = EditorGUILayout.Toggle("Whole Numbers", s.wholeNumbers);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    s.syncValue = EditorGUILayout.Toggle(s.syncValue, GUILayout.Width(toggleWidth));
                    s.value = EditorGUILayout.Slider("Value", s.value, s.minValue, s.maxValue);
                    EditorGUILayout.EndHorizontal();
                    EditorGUI.indentLevel--;
                }

                GUILayout.EndArea();
            }
        }

        void DrawInputUI()
        {
            float startHeight = 10f;

            foreach (EasyUI_InputStyle i in easyUI_Data.inputList)
            {
                float lengthOfBox;
                if (!i.maximize)
                    lengthOfBox = 30f;
                else if (i.customCaretColor)
                    lengthOfBox = 440f;
                else
                    lengthOfBox = 425f;

                lengthOfBox += BoxDrawAdjust(i);

#if UNITY_2019_3_OR_NEWER
                lengthOfBox *= 1.09f;
#endif

                //blank label allows scrollview to expand
                GUILayout.Label("", GUILayout.Height(lengthOfBox));
                //background shading
                GUILayout.BeginArea(new Rect(10f, startHeight, window.position.width - 30f, lengthOfBox), skin.box);
                startHeight += lengthOfBox + 10f;

                DrawButtonTemplate(i);

                if (i.maximize)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.BeginHorizontal();
                    i.syncText = EditorGUILayout.Toggle(i.syncText, GUILayout.Width(toggleWidth));
                    i.text = EditorGUILayout.TextField("Text", i.text);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncCharacterLimit = EditorGUILayout.Toggle(i.syncCharacterLimit, GUILayout.Width(toggleWidth));
                    i.characterLimit = EditorGUILayout.IntField("Character Limit", i.characterLimit);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncContentType = EditorGUILayout.Toggle(i.syncContentType, GUILayout.Width(toggleWidth));
                    i.contentType = (UnityEngine.UI.InputField.ContentType)EditorGUILayout.EnumPopup("Content Type", i.contentType);
                    EditorGUILayout.EndHorizontal();

                    if (i.contentType == UnityEngine.UI.InputField.ContentType.Standard)
                    {
                        EditorGUILayout.BeginHorizontal();
                        i.syncLineType = EditorGUILayout.Toggle(i.syncLineType, GUILayout.Width(toggleWidth));
                        EditorGUI.indentLevel++;
                        i.lineType = (UnityEngine.UI.InputField.LineType)EditorGUILayout.EnumPopup("Line Type", i.lineType);
                        EditorGUI.indentLevel--;
                        EditorGUILayout.EndHorizontal();

                        //if (i.lineType != UnityEngine.UI.InputField.LineType.SingleLine)
                        //{
                        //    EditorGUILayout.BeginHorizontal();
                        //    i.syncLineLimit = EditorGUILayout.Toggle(i.syncLineLimit, GUILayout.Width(toggleWidth));
                        //    EditorGUI.indentLevel++;
                        //    i.lineLimit = EditorGUILayout.IntField("Line Limit", i.lineLimit);
                        //    EditorGUI.indentLevel--;
                        //    EditorGUILayout.EndHorizontal();
                        //}
                    }
                    else if (i.contentType == UnityEngine.UI.InputField.ContentType.Custom)
                    {
                        EditorGUILayout.BeginHorizontal();
                        i.syncLineType = EditorGUILayout.Toggle(i.syncLineType, GUILayout.Width(toggleWidth));
                        EditorGUI.indentLevel++;
                        i.lineType = (UnityEngine.UI.InputField.LineType)EditorGUILayout.EnumPopup("Line Type", i.lineType);
                        EditorGUI.indentLevel--;
                        EditorGUILayout.EndHorizontal();

                        //if (i.lineType != UnityEngine.UI.InputField.LineType.SingleLine)
                        //{
                        //    EditorGUILayout.BeginHorizontal();
                        //    i.syncLineLimit = EditorGUILayout.Toggle(i.syncLineLimit, GUILayout.Width(toggleWidth));
                        //    EditorGUI.indentLevel++;
                        //    i.lineLimit = EditorGUILayout.IntField("Line Limit", i.lineLimit);
                        //    EditorGUI.indentLevel--;
                        //    EditorGUILayout.EndHorizontal();
                        //}

                        EditorGUILayout.BeginHorizontal();
                        i.syncInputType = EditorGUILayout.Toggle(i.syncInputType, GUILayout.Width(toggleWidth));
                        EditorGUI.indentLevel++;
                        i.inputType = (UnityEngine.UI.InputField.InputType)EditorGUILayout.EnumPopup("Input Type", i.inputType);
                        EditorGUI.indentLevel--;
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        i.syncKeyboardType = EditorGUILayout.Toggle(i.syncKeyboardType, GUILayout.Width(toggleWidth));
                        EditorGUI.indentLevel++;
                        i.keyboardType = (TouchScreenKeyboardType)EditorGUILayout.EnumPopup("Keyboard Type", i.keyboardType);
                        EditorGUI.indentLevel--;
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        i.syncCharacterValidation = EditorGUILayout.Toggle(i.syncCharacterValidation, GUILayout.Width(toggleWidth));
                        EditorGUI.indentLevel++;
                        i.characterValidation = (UnityEngine.UI.InputField.CharacterValidation)EditorGUILayout.EnumPopup("Character Validation", i.characterValidation);
                        EditorGUI.indentLevel--;
                        EditorGUILayout.EndHorizontal();

                    }

                    EditorGUILayout.BeginHorizontal();
                    i.syncCaretBlinkRate = EditorGUILayout.Toggle(i.syncCaretBlinkRate, GUILayout.Width(toggleWidth));
                    i.caretBlinkRate = EditorGUILayout.Slider("Caret Blink Rate", i.caretBlinkRate, 0f, 4f);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncCaretWidth = EditorGUILayout.Toggle(i.syncCaretWidth, GUILayout.Width(toggleWidth));
                    i.caretWidth = EditorGUILayout.IntSlider("Caret Width", i.caretWidth, 1, 5);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncCustomCaretColor = EditorGUILayout.Toggle(i.syncCustomCaretColor, GUILayout.Width(toggleWidth));
                    i.customCaretColor = EditorGUILayout.Toggle("Custom Caret Color", i.customCaretColor);
                    EditorGUILayout.EndHorizontal();

                    if (i.customCaretColor)
                    {
                        EditorGUILayout.BeginHorizontal();
                        i.syncSelectionColor = EditorGUILayout.Toggle(i.syncSelectionColor, GUILayout.Width(toggleWidth));
                        i.caretColor = EditorGUILayout.ColorField("Caret Color", i.caretColor);
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.BeginHorizontal();
                    i.syncSelectionColor = EditorGUILayout.Toggle(i.syncSelectionColor, GUILayout.Width(toggleWidth));
                    i.selectionColor = EditorGUILayout.ColorField("Selection Color", i.selectionColor);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncHideMobileInput = EditorGUILayout.Toggle(i.syncHideMobileInput, GUILayout.Width(toggleWidth));
                    i.hideMobileInput = EditorGUILayout.Toggle("Hide Mobile Input", i.hideMobileInput);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncReadOnly = EditorGUILayout.Toggle(i.syncReadOnly, GUILayout.Width(toggleWidth));
                    i.readOnly = EditorGUILayout.Toggle("Read Only", i.readOnly);
                    EditorGUILayout.EndHorizontal();
                    EditorGUI.indentLevel--;
                }

                GUILayout.EndArea();
            }
        }

        void DrawDropDownUI()
        {
            float startHeight = 10f;

            foreach (EasyUI_DropdownStyle d in easyUI_Data.dropdownList)
            {
                float lengthOfBox;
                if (!d.maximize)
                    lengthOfBox = 30f;
                else
                {
                    lengthOfBox = 300f;
                    lengthOfBox += d.optionList.Count * 47f;
                }

                lengthOfBox += BoxDrawAdjust(d);

#if UNITY_2019_3_OR_NEWER
                lengthOfBox *= 1.085f;
#endif

                //blank label allows scrollview to expand
                GUILayout.Label("", GUILayout.Height(lengthOfBox));
                //background shading
                GUILayout.BeginArea(new Rect(10f, startHeight, window.position.width - 30f, lengthOfBox), skin.box);
                startHeight += lengthOfBox + 10f;

                DrawButtonTemplate(d);

                if (d.maximize)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.BeginHorizontal();
                    d.syncValue = EditorGUILayout.Toggle(d.syncValue, GUILayout.Width(toggleWidth));
                    d.value = EditorGUILayout.IntField("Value", d.value);
                    if (d.value >= d.optionList.Count)
                        d.value = d.optionList.Count - 1;
                    else if (d.value < 0)
                        d.value = 0;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    d.syncOptions = EditorGUILayout.Toggle(d.syncOptions, GUILayout.Width(toggleWidth));

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Dropdown Options");
                    if (GUILayout.Button("Add"))
                    {
                        UnityEngine.UI.Dropdown.OptionData _option = new UnityEngine.UI.Dropdown.OptionData();
                        _option.text = "Option " + (d.optionList.Count + 1);
                        d.AddOption(_option);
                    }
                    if (GUILayout.Button("Remove"))
                    {
                        d.RemoveOption(d.optionList[d.optionList.Count - 1]);
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndHorizontal();

                    foreach (UnityEngine.UI.Dropdown.OptionData _option in d.optionList)
                    {
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.Space();
                        EditorGUILayout.BeginVertical();
                        _option.text = EditorGUILayout.TextField("Option", _option.text);
                        _option.image = EditorGUILayout.ObjectField(_option.image, typeof(Sprite), false) as Sprite;
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();

                    }
                    EditorGUI.indentLevel--;
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                }

                GUILayout.EndArea();
            }
        }

        void DrawSettingsUI()
        {
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Add Chooser To:", skin.GetStyle("ColumnHeading"));
            EditorGUI.indentLevel++;
            EditorGUILayout.Space();
            if (GUILayout.Button("Text"))
            {
                AddToALL<UnityEngine.UI.Text>();
            }
            if (GUILayout.Button("Image"))
            {
                AddToALL<UnityEngine.UI.Image>();
            }
            if (GUILayout.Button("Button"))
            {
                AddToALL<UnityEngine.UI.Button>();
            }
            if (GUILayout.Button("Toggle"))
            {
                AddToALL<UnityEngine.UI.Toggle>();
            }
            if (GUILayout.Button("Slider"))
            {
                AddToALL<UnityEngine.UI.Slider>();
            }
            if (GUILayout.Button("Input Field"))
            {
                AddToALL<UnityEngine.UI.InputField>();
            }
            if (GUILayout.Button("Dropdown"))
            {
                AddToALL<UnityEngine.UI.Dropdown>();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("TMP UGUI - Text"))
            {
                AddToALL<TMPro.TextMeshProUGUI>();
            }
            if (GUILayout.Button("TMP Input Field"))
            {
                AddToALL<TMPro.TMP_InputField>();
            }
            if (GUILayout.Button("TMP Dropdown"))
            {
                AddToALL<TMPro.TMP_Dropdown>();
            }
            EditorGUILayout.Space();
            if (GUILayout.Button("All"))
            {
                AddToALL<UnityEngine.UI.Text>();
                AddToALL<UnityEngine.UI.Image>();
                AddToALL<UnityEngine.UI.Toggle>();
                AddToALL<UnityEngine.UI.Slider>();
                AddToALL<UnityEngine.UI.InputField>();
                AddToALL<UnityEngine.UI.Dropdown>();
                AddToALL<TMPro.TextMeshProUGUI>();
                AddToALL<TMPro.TMP_InputField>();
                AddToALL<TMPro.TMP_Dropdown>();
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Remove Chooser From:", skin.GetStyle("ColumnHeading"));
            EditorGUI.indentLevel++;
            EditorGUILayout.Space();
            if (GUILayout.Button("Text"))
            {
                RemoveFromALL<UnityEngine.UI.Text>();
            }
            if (GUILayout.Button("Image"))
            {
                RemoveFromALL<UnityEngine.UI.Image>();
            }
            if (GUILayout.Button("Button"))
            {
                RemoveFromALL<UnityEngine.UI.Button>();
            }
            if (GUILayout.Button("Toggle"))
            {
                RemoveFromALL<UnityEngine.UI.Toggle>();
            }
            if (GUILayout.Button("Slider"))
            {
                RemoveFromALL<UnityEngine.UI.Slider>();
            }
            if (GUILayout.Button("Input Field"))
            {
                RemoveFromALL<UnityEngine.UI.InputField>();
            }
            if (GUILayout.Button("Dropdown"))
            {
                RemoveFromALL<UnityEngine.UI.Dropdown>();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("TMP UGUI - Text"))
            {
                RemoveFromALL<TMPro.TextMeshProUGUI>();
            }
            if (GUILayout.Button("TMP Input Field"))
            {
                RemoveFromALL<TMPro.TMP_InputField>();
            }
            if (GUILayout.Button("TMP Dropdown"))
            {
                RemoveFromALL<TMPro.TMP_Dropdown>();
            }
            EditorGUILayout.Space();
            if (GUILayout.Button("All"))
            {
                RemoveFromALL<UnityEngine.UI.Text>();
                RemoveFromALL<UnityEngine.UI.Image>();
                RemoveFromALL<UnityEngine.UI.Toggle>();
                RemoveFromALL<UnityEngine.UI.Slider>();
                RemoveFromALL<UnityEngine.UI.InputField>();
                RemoveFromALL<UnityEngine.UI.Dropdown>();
                RemoveFromALL<TMPro.TextMeshProUGUI>();
                RemoveFromALL<TMPro.TMP_InputField>();
                RemoveFromALL<TMPro.TMP_Dropdown>();
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

        //some garbage going on here... but it works.
        void DrawTMProTextUI()
        {
            float startHeight = 0f;

            for (int i = 0; i < easyUI_Data.tmproTextList.Count; i++)
            {
                easyUI_Data.tmproTextList[i].displayOrder = i;
            }

            foreach (EasyUI_TMPTextStyle data in easyUI_Data.tmproTextList)
            {
                float lengthOfBox;
                if (!data.maximize)
                    lengthOfBox = 30f;
                else
                {
                    lengthOfBox = 775f;

                    if (data.enableVertextGradient)
                        lengthOfBox += 65f;
                    if (data.horizontalMapping == TMPro.TextureMappingOptions.Character)
                        lengthOfBox -= 16f;
                    if (((int)data.textAlignment & 16) == 16 || ((int)data.textAlignment & 8) == 8)
                        lengthOfBox += 16f;
                    if (data.enableAutoSize)
                        lengthOfBox += 40f;
                }

#if UNITY_2019_3_OR_NEWER
                lengthOfBox *= 1.08f;
#endif

                GUI.depth = 1;
                //blank label allows scrollview to expand
                GUILayout.Label("", GUILayout.Height(lengthOfBox));
                //background shading
                GUILayout.BeginArea(new Rect(10f, startHeight, window.position.width - 30f, lengthOfBox), skin.box);
                startHeight += lengthOfBox + 10f;

                DrawTopStyleButtons(data);

                if (data.maximize)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    data.styleName = EditorGUILayout.TextField("Style Name : ", data.styleName);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();

                    //EditorGUILayout.LabelField("  Sync", GUILayout.Width(toggleWidth));
                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.indentLevel--;
                    if (GUILayout.Button("Sync", GUILayout.Width(50)))
                    {
                        data.ToggleAll(data.syncFontAsset);
                    }
                    EditorGUI.indentLevel++;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    data.syncIsRightToLeft = EditorGUILayout.Toggle(data.syncIsRightToLeft, GUILayout.Width(toggleWidth));
                    data.isRightToLeft = EditorGUILayout.Toggle("Enable RTL Editor", data.isRightToLeft);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Main Settings", skin.FindStyle("SectionHeading"));
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    data.syncFontAsset = EditorGUILayout.Toggle(data.syncFontAsset, GUILayout.Width(toggleWidth));
                    data.fontAsset = EditorGUILayout.ObjectField("Font Asset ", data.fontAsset, typeof(TMPro.TMP_FontAsset), false) as TMPro.TMP_FontAsset;
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    //data.materialPreset = EditorGUILayout.ObjectField("Material Preset", data.materialPreset, typeof(Material), false) as Material;
                    if (data.fontAsset != null) //need to modify this so that editor is more responsive.
                    {
                        data.syncMaterialPreset = EditorGUILayout.Toggle(data.syncMaterialPreset, GUILayout.Width(toggleWidth));
                        if (data.materialPreset == null || data.materialPresets == null)
                        {
                            data.materialPresets = data.GetMaterialPresets();
                            data.materialValues = data.GetMaterialValues();
                            data.materialPreset = data.GetMaterialPreset(data.materialPresetIndex);
                        }
                        int tempIndex = EditorGUILayout.IntPopup(new GUIContent("Material Preset"), data.materialPresetIndex, data.materialPresets, data.materialValues);
                        if (tempIndex != data.materialPresetIndex) //trying to avoid searching the AssetDatabase...
                        {
                            data.materialPresetIndex = tempIndex;
                            data.materialPreset = data.GetMaterialPreset(data.materialPresetIndex);
                        }
                        if (GUILayout.Button("Refresh List", GUILayout.MaxWidth(100)))
                        {
                            data.materialPresets = data.GetMaterialPresets();
                            data.materialValues = data.GetMaterialValues();
                            data.materialPreset = data.GetMaterialPreset(data.materialPresetIndex);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    data.syncFontStyles = EditorGUILayout.Toggle(data.syncFontStyles, GUILayout.Width(toggleWidth));
                    EditorGUILayout.LabelField("Font Style", GUILayout.MaxWidth(145));
                    //Normal = 0,
                    //Bold = 1,
                    //Italic = 2,
                    //Underline = 4,
                    //LowerCase = 8,
                    //UpperCase = 16,
                    //SmallCaps = 32,
                    //Strikethrough = 64,
                    //Superscript = 128,
                    //Subscript = 256,
                    ////Highlight = 512
                    bool useBold = ((int)data.fontStyles & 1) == 1;
                    if (GUILayout.Toggle(useBold, new GUIContent("B", "Bold"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonLeft, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useBold)
                    {
                        if (((int)data.fontStyles & 1) != 1)
                            data.fontStyles += 1;
                        else
                            data.fontStyles -= 1;
                    }
                    bool useItalic = ((int)data.fontStyles & 2) == 2;
                    if (GUILayout.Toggle(useItalic, new GUIContent("I", "Italic"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonMid, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useItalic)
                    {
                        if (((int)data.fontStyles & 2) != 2)
                            data.fontStyles += 2;
                        else
                            data.fontStyles -= 2;
                    }
                    bool useUnderLine = ((int)data.fontStyles & 4) == 4;
                    if (GUILayout.Toggle(useUnderLine, new GUIContent("U", "Underline"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonMid, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useUnderLine)
                    {
                        if (((int)data.fontStyles & 4) != 4)
                            data.fontStyles += 4;
                        else
                            data.fontStyles -= 4;
                    }
                    bool useStrike = ((int)data.fontStyles & 64) == 64;
                    if (GUILayout.Toggle(useStrike, new GUIContent("S", "Strikethrough"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonRight, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useStrike)
                    {
                        if (((int)data.fontStyles & 64) != 64)
                            data.fontStyles += 64;
                        else
                            data.fontStyles -= 64;
                    }

                    bool useLower = ((int)data.fontStyles & 8) == 8;
                    if (GUILayout.Toggle(useLower, new GUIContent("ab", "Lowercase"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonLeft, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useLower)
                    {
                        if (((int)data.fontStyles & 8) != 8)
                            data.fontStyles += 8;
                        else
                            data.fontStyles -= 8;

                        //check for overlapping styles
                        if (((int)data.fontStyles & 16) == 16)
                            data.fontStyles -= 16;
                        if (((int)data.fontStyles & 32) == 32)
                            data.fontStyles -= 32;
                    }
                    bool useUpper = ((int)data.fontStyles & 16) == 16;
                    if (GUILayout.Toggle(useUpper, new GUIContent("AB", "Uppercase"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonMid, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useUpper)
                    {
                        if (((int)data.fontStyles & 16) != 16)
                            data.fontStyles += 16;
                        else
                            data.fontStyles -= 16;

                        //check for overlapping styles
                        if (((int)data.fontStyles & 8) == 8)
                            data.fontStyles -= 8;
                        if (((int)data.fontStyles & 32) == 32)
                            data.fontStyles -= 32;
                    }
                    bool useSmallCaps = ((int)data.fontStyles & 32) == 32;
                    if (GUILayout.Toggle(useSmallCaps, new GUIContent("SC", "Smallcaps"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonRight, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useSmallCaps)
                    {
                        if (((int)data.fontStyles & 32) != 32)
                            data.fontStyles += 32;
                        else
                            data.fontStyles -= 32;

                        //check for overlapping styles
                        if (((int)data.fontStyles & 8) == 8)
                            data.fontStyles -= 8;
                        if (((int)data.fontStyles & 16) == 16)
                            data.fontStyles -= 16;
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();


                    EditorGUILayout.BeginHorizontal();
                    data.syncFontSize = EditorGUILayout.Toggle(data.syncFontSize, GUILayout.Width(toggleWidth), GUILayout.ExpandWidth(false));
                    data.fontSize = EditorGUILayout.FloatField("Font Size ", data.fontSize);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    data.syncAutoSize = EditorGUILayout.Toggle(data.syncAutoSize, GUILayout.Width(toggleWidth));
                    data.enableAutoSize = EditorGUILayout.Toggle("Auto Size", data.enableAutoSize);
                    EditorGUILayout.EndHorizontal();

                    if (data.enableAutoSize)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.Space();
                        //EditorGUILayout.LabelField("Auto Size Options");
                        data.fontSizeMin = EditorGUILayout.FloatField("Min", data.fontSizeMin, GUILayout.ExpandWidth(false));
                        data.fontSizeMax = EditorGUILayout.FloatField("Max", data.fontSizeMax, GUILayout.ExpandWidth(false));
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.Space();
                        data.charWidthMaxAdjust = EditorGUILayout.FloatField("WD%", data.charWidthMaxAdjust, GUILayout.ExpandWidth(false));
                        data.lineSpacingMax = EditorGUILayout.FloatField("Line", data.lineSpacingMax, GUILayout.ExpandWidth(false));
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                    }

                    EditorGUILayout.BeginHorizontal();
                    data.syncColor = EditorGUILayout.Toggle(data.syncColor, GUILayout.Width(toggleWidth));
                    data.color = EditorGUILayout.ColorField("Color", data.color);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    data.syncEnableVertexGradient = EditorGUILayout.Toggle(data.syncEnableVertexGradient, GUILayout.Width(toggleWidth));
                    data.enableVertextGradient = EditorGUILayout.Toggle("Color Gradient", data.enableVertextGradient);
                    EditorGUILayout.EndHorizontal();

                    if (data.enableVertextGradient)
                    {
                        EditorGUILayout.BeginHorizontal();
                        data.syncColorPreset = EditorGUILayout.Toggle(data.syncColorGradient, GUILayout.Width(toggleWidth));
                        data.colorPreset = EditorGUILayout.ObjectField("Color Preset ", data.colorPreset, typeof(TMPro.TMP_ColorGradient), false) as TMPro.TMP_ColorGradient;
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        data.syncColorGradient = EditorGUILayout.Toggle(data.syncColorGradient, GUILayout.Width(toggleWidth));
                        data.colorMode = (TMPro.ColorMode)EditorGUILayout.EnumPopup("Color Mode", data.colorMode);
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.Space();

                        data.topLeft = EditorGUILayout.ColorField(data.topLeft, GUILayout.ExpandWidth(false));
                        if (data.colorMode == TMPro.ColorMode.HorizontalGradient || data.colorMode == TMPro.ColorMode.FourCornersGradient)
                            data.topRight = EditorGUILayout.ColorField(data.topRight, GUILayout.ExpandWidth(false));
                        else
                            EditorGUILayout.Space();
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.Space();
                        if (data.colorMode == TMPro.ColorMode.VerticalGradient || data.colorMode == TMPro.ColorMode.FourCornersGradient)
                            data.bottomLeft = EditorGUILayout.ColorField(data.bottomLeft, GUILayout.ExpandWidth(false));
                        if (data.colorMode == TMPro.ColorMode.FourCornersGradient)
                            data.bottomRight = EditorGUILayout.ColorField(data.bottomRight, GUILayout.ExpandWidth(false));
                        else
                            EditorGUILayout.Space();

                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    data.syncOverrideTags = EditorGUILayout.Toggle(data.syncOverrideTags, GUILayout.Width(toggleWidth));
                    data.overrideTags = EditorGUILayout.Toggle("Override Tags", data.overrideTags);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Spacing Options", skin.FindStyle("SectionHeading"));
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    data.syncCharacterSpacing = EditorGUILayout.Toggle(data.syncCharacterSpacing, GUILayout.Width(toggleWidth));
                    data.characterSpacing = EditorGUILayout.FloatField("Character Spacing", data.characterSpacing, GUILayout.ExpandWidth(false));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    data.syncWordSpacing = EditorGUILayout.Toggle(data.syncWordSpacing, GUILayout.Width(toggleWidth));
                    data.wordSpacing = EditorGUILayout.FloatField("Word Spacing", data.wordSpacing, GUILayout.ExpandWidth(false));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    data.syncLineSpacing = EditorGUILayout.Toggle(data.syncLineSpacing, GUILayout.Width(toggleWidth));
                    data.lineSpacing = EditorGUILayout.FloatField("Line Spacing", data.lineSpacing, GUILayout.ExpandWidth(false));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    data.syncParagraphSpacing = EditorGUILayout.Toggle(data.syncParagraphSpacing, GUILayout.Width(toggleWidth));
                    data.paragraphSpacing = EditorGUILayout.FloatField("Paragraph Spacing", data.paragraphSpacing, GUILayout.ExpandWidth(false));
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    data.syncTextAligment = EditorGUILayout.Toggle(data.syncTextAligment, GUILayout.Width(toggleWidth));
                    //data.fontStyles = EditorGUILayout.ObjectField(data.fontStyles, typeof(TMPro.FontStyles), false, null) as TMPro.FontStyles;
                    EditorGUILayout.LabelField("Alignment", GUILayout.MaxWidth(145));

#region Alignment
                    bool useLeft = ((int)data.textAlignment & 1) == 1; ;
                    if (GUILayout.Toggle(useLeft, new GUIContent(TMPro.EditorUtilities.TMP_UIStyleManager.alignLeft, "Left"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonLeft, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useLeft)
                    {
                        if (((int)data.textAlignment & 1) != 1)
                            data.textAlignment += 1;
                        else
                            data.textAlignment -= 1;

                        //check for overlapping styles
                        if (((int)data.textAlignment & 2) == 2)
                            data.textAlignment -= 2;
                        if (((int)data.textAlignment & 4) == 4)
                            data.textAlignment -= 4;
                        if (((int)data.textAlignment & 8) == 8)
                            data.textAlignment -= 8;
                        if (((int)data.textAlignment & 16) == 16)
                            data.textAlignment -= 16;
                        if (((int)data.textAlignment & 32) == 32)
                            data.textAlignment -= 32;
                    }
                    bool useCenter = ((int)data.textAlignment & 2) == 2;
                    if (GUILayout.Toggle(useCenter, new GUIContent(TMPro.EditorUtilities.TMP_UIStyleManager.alignCenter, "Center"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonMid, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useCenter)
                    {
                        if (((int)data.textAlignment & 2) != 2)
                            data.textAlignment += 2;
                        else
                            data.textAlignment -= 2;

                        //check for overlapping styles
                        if (((int)data.textAlignment & 1) == 1)
                            data.textAlignment -= 1;
                        if (((int)data.textAlignment & 4) == 4)
                            data.textAlignment -= 4;
                        if (((int)data.textAlignment & 8) == 8)
                            data.textAlignment -= 8;
                        if (((int)data.textAlignment & 16) == 16)
                            data.textAlignment -= 16;
                        if (((int)data.textAlignment & 32) == 32)
                            data.textAlignment -= 32;
                    }
                    bool useRight = ((int)data.textAlignment & 4) == 4;
                    if (GUILayout.Toggle(useRight, new GUIContent(TMPro.EditorUtilities.TMP_UIStyleManager.alignRight, "Right"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonMid, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useRight)
                    {
                        if (((int)data.textAlignment & 4) != 41)
                            data.textAlignment += 4;
                        else
                            data.textAlignment -= 4;

                        //check for overlapping styles
                        if (((int)data.textAlignment & 1) == 1)
                            data.textAlignment -= 1;
                        if (((int)data.textAlignment & 2) == 2)
                            data.textAlignment -= 2;
                        if (((int)data.textAlignment & 8) == 8)
                            data.textAlignment -= 8;
                        if (((int)data.textAlignment & 16) == 16)
                            data.textAlignment -= 16;
                        if (((int)data.textAlignment & 32) == 32)
                            data.textAlignment -= 32;
                    }
                    bool useJustified = ((int)data.textAlignment & 8) == 8;
                    if (GUILayout.Toggle(useJustified, new GUIContent(TMPro.EditorUtilities.TMP_UIStyleManager.alignJustified, "Justified"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonMid, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useJustified)
                    {
                        if (((int)data.textAlignment & 8) != 8)
                            data.textAlignment += 8;
                        else
                            data.textAlignment -= 8;

                        //check for overlapping styles
                        if (((int)data.textAlignment & 1) == 1)
                            data.textAlignment -= 1;
                        if (((int)data.textAlignment & 2) == 2)
                            data.textAlignment -= 2;
                        if (((int)data.textAlignment & 4) == 4)
                            data.textAlignment -= 4;
                        if (((int)data.textAlignment & 16) == 16)
                            data.textAlignment -= 16;
                        if (((int)data.textAlignment & 32) == 32)
                            data.textAlignment -= 32;
                    }
                    bool useFlush = ((int)data.textAlignment & 16) == 16;
                    if (GUILayout.Toggle(useFlush, new GUIContent(TMPro.EditorUtilities.TMP_UIStyleManager.alignFlush, "Flush"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonMid, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useFlush)
                    {
                        if (((int)data.textAlignment & 16) != 16)
                            data.textAlignment += 16;
                        else
                            data.textAlignment -= 16;

                        //check for overlapping styles
                        if (((int)data.textAlignment & 1) == 1)
                            data.textAlignment -= 1;
                        if (((int)data.textAlignment & 2) == 2)
                            data.textAlignment -= 2;
                        if (((int)data.textAlignment & 4) == 4)
                            data.textAlignment -= 4;
                        if (((int)data.textAlignment & 8) == 8)
                            data.textAlignment -= 8;
                        if (((int)data.textAlignment & 32) == 32)
                            data.textAlignment -= 32;
                    }
                    bool useGeometryCenter = ((int)data.textAlignment & 32) == 32;
                    if (GUILayout.Toggle(useGeometryCenter, new GUIContent(TMPro.EditorUtilities.TMP_UIStyleManager.alignGeoCenter, "Geometry Center"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonRight, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useGeometryCenter)
                    {
                        if (((int)data.textAlignment & 32) != 32)
                            data.textAlignment += 32;
                        else
                            data.textAlignment -= 32;

                        //check for overlapping styles
                        if (((int)data.textAlignment & 1) == 1)
                            data.textAlignment -= 1;
                        if (((int)data.textAlignment & 2) == 2)
                            data.textAlignment -= 2;
                        if (((int)data.textAlignment & 4) == 4)
                            data.textAlignment -= 4;
                        if (((int)data.textAlignment & 8) == 8)
                            data.textAlignment -= 8;
                        if (((int)data.textAlignment & 16) == 16)
                            data.textAlignment -= 16;

                    }

                    bool useTop = ((int)data.textAlignment & 256) == 256;
                    if (GUILayout.Toggle(useTop, new GUIContent(TMPro.EditorUtilities.TMP_UIStyleManager.alignTop, "Top"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonLeft, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useTop)
                    {
                        if (((int)data.textAlignment & 256) != 256)
                            data.textAlignment += 256;
                        else
                            data.textAlignment -= 256;

                        //check for overlapping styles
                        if (((int)data.textAlignment & 512) == 512)
                            data.textAlignment -= 512;
                        if (((int)data.textAlignment & 1024) == 1024)
                            data.textAlignment -= 1024;
                        if (((int)data.textAlignment & 2048) == 2048)
                            data.textAlignment -= 2048;
                        if (((int)data.textAlignment & 4096) == 4096)
                            data.textAlignment -= 4096;
                        if (((int)data.textAlignment & 8192) == 8192)
                            data.textAlignment -= 8192;
                    }
                    bool useMiddle = ((int)data.textAlignment & 512) == 512;
                    if (GUILayout.Toggle(useMiddle, new GUIContent(TMPro.EditorUtilities.TMP_UIStyleManager.alignMiddle, "Middle"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonMid, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useMiddle)
                    {
                        if (((int)data.textAlignment & 512) != 512)
                            data.textAlignment += 512;
                        else
                            data.textAlignment -= 512;

                        //check for overlapping styles
                        if (((int)data.textAlignment & 256) == 256)
                            data.textAlignment -= 256;
                        if (((int)data.textAlignment & 1024) == 1024)
                            data.textAlignment -= 1024;
                        if (((int)data.textAlignment & 2048) == 2048)
                            data.textAlignment -= 2048;
                        if (((int)data.textAlignment & 4096) == 4096)
                            data.textAlignment -= 4096;
                        if (((int)data.textAlignment & 8192) == 8192)
                            data.textAlignment -= 8192;

                    }
                    bool useBottom = ((int)data.textAlignment & 1024) == 1024;
                    if (GUILayout.Toggle(useBottom, new GUIContent(TMPro.EditorUtilities.TMP_UIStyleManager.alignBottom, "Bottom"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonMid, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useBottom)
                    {
                        if (((int)data.textAlignment & 1024) != 1024)
                            data.textAlignment += 1024;
                        else
                            data.textAlignment -= 1024;

                        if (((int)data.textAlignment & 256) == 256)
                            data.textAlignment -= 256;
                        if (((int)data.textAlignment & 512) == 512)
                            data.textAlignment -= 512;
                        if (((int)data.textAlignment & 2048) == 2048)
                            data.textAlignment -= 2048;
                        if (((int)data.textAlignment & 4096) == 4096)
                            data.textAlignment -= 4096;
                        if (((int)data.textAlignment & 8192) == 8192)
                            data.textAlignment -= 8192;
                    }
                    bool useBaseline = ((int)data.textAlignment & 2048) == 2048;
                    if (GUILayout.Toggle(useBaseline, new GUIContent(TMPro.EditorUtilities.TMP_UIStyleManager.alignBaseline, "Baseline"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonMid, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useBaseline)
                    {
                        if (((int)data.textAlignment & 2048) != 2048)
                            data.textAlignment += 2048;
                        else
                            data.textAlignment -= 2048;

                        //check for overlapping styles
                        if (((int)data.textAlignment & 256) == 256)
                            data.textAlignment -= 256;
                        if (((int)data.textAlignment & 512) == 512)
                            data.textAlignment -= 512;
                        if (((int)data.textAlignment & 1024) == 1024)
                            data.textAlignment -= 1024;
                        if (((int)data.textAlignment & 4096) == 4096)
                            data.textAlignment -= 4096;
                        if (((int)data.textAlignment & 8192) == 8192)
                            data.textAlignment -= 8192;
                    }
                    bool useMidLine = ((int)data.textAlignment & 4096) == 4096;
                    if (GUILayout.Toggle(useMidLine, new GUIContent(TMPro.EditorUtilities.TMP_UIStyleManager.alignMidline, "Midline"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonMid, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useMidLine)
                    {
                        if (((int)data.textAlignment & 4096) != 4096)
                            data.textAlignment += 4096;
                        else
                            data.textAlignment -= 4096;

                        //check for overlapping styles
                        if (((int)data.textAlignment & 256) == 256)
                            data.textAlignment -= 256;
                        if (((int)data.textAlignment & 512) == 512)
                            data.textAlignment -= 512;
                        if (((int)data.textAlignment & 1024) == 1024)
                            data.textAlignment -= 1024;
                        if (((int)data.textAlignment & 2048) == 2048)
                            data.textAlignment -= 2048;
                        if (((int)data.textAlignment & 8192) == 8192)
                            data.textAlignment -= 8192;
                    }
                    bool useCapLine = ((int)data.textAlignment & 8192) == 8192;
                    if (GUILayout.Toggle(useCapLine, new GUIContent(TMPro.EditorUtilities.TMP_UIStyleManager.alignCapline, "Capline"), TMPro.EditorUtilities.TMP_UIStyleManager.alignmentButtonRight, GUILayout.ExpandWidth(false), GUILayout.MinWidth(30), GUILayout.MinHeight(20)) != useCapLine)
                    {
                        if (((int)data.textAlignment & 8192) != 8192)
                            data.textAlignment += 8192;
                        else
                            data.textAlignment -= 8192;

                        //check for overlapping styles
                        if (((int)data.textAlignment & 256) == 256)
                            data.textAlignment -= 256;
                        if (((int)data.textAlignment & 512) == 512)
                            data.textAlignment -= 512;
                        if (((int)data.textAlignment & 1024) == 1024)
                            data.textAlignment -= 1024;
                        if (((int)data.textAlignment & 2048) == 2048)
                            data.textAlignment -= 2048;
                        if (((int)data.textAlignment & 4096) == 4096)
                            data.textAlignment -= 4096;
                    }
                    EditorGUILayout.EndHorizontal();

                    if (useFlush || useJustified)
                    {
                        EditorGUILayout.BeginHorizontal();
                        data.syncWrapMix = EditorGUILayout.Toggle(data.syncWrapMix, GUILayout.Width(toggleWidth));
                        data.wrapMix = EditorGUILayout.Slider("Wrap Mix", data.wrapMix, 0f, 1f);
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.Space();
#endregion

                    EditorGUILayout.BeginHorizontal();
                    data.syncWordWrapping = EditorGUILayout.Toggle(data.syncWordWrapping, GUILayout.Width(toggleWidth));
                    int wrapIndex = data.enableWordWrapping == true ? 1 : 0;
                    GUIContent[] wrappingOptions = { new GUIContent("Disabled"), new GUIContent("Enabled") };
                    int[] wrappingValues = new int[] { 0, 1 };
                    wrapIndex = EditorGUILayout.IntPopup(new GUIContent("Wrapping"), wrapIndex, wrappingOptions, wrappingValues);
                    data.enableWordWrapping = wrapIndex == 1;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    data.syncOverflowMode = EditorGUILayout.Toggle(data.syncOverflowMode, GUILayout.Width(toggleWidth));
                    data.overflowMode = (TMPro.TextOverflowModes)EditorGUILayout.EnumPopup("Overflow", data.overflowMode);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    data.syncHoriztonalMapping = EditorGUILayout.Toggle(data.syncHoriztonalMapping, GUILayout.Width(toggleWidth));
                    data.horizontalMapping = (TMPro.TextureMappingOptions)EditorGUILayout.EnumPopup("Horizontal Mapping", data.horizontalMapping);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    data.syncVerticalMapping = EditorGUILayout.Toggle(data.syncVerticalMapping, GUILayout.Width(toggleWidth));
                    data.verticalMapping = (TMPro.TextureMappingOptions)EditorGUILayout.EnumPopup("Vertical Mapping", data.verticalMapping);
                    EditorGUILayout.EndHorizontal();

                    if (data.horizontalMapping != TMPro.TextureMappingOptions.Character)
                    {
                        EditorGUILayout.BeginHorizontal();
                        data.syncLineOffset = EditorGUILayout.Toggle(data.syncLineOffset, GUILayout.Width(toggleWidth));
                        data.lineOffset = EditorGUILayout.FloatField("Line Offset", data.lineOffset, GUILayout.ExpandWidth(false));
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Extra Settings", skin.FindStyle("SectionHeading"));
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    data.syncMargins = EditorGUILayout.Toggle(data.syncMargins, GUILayout.Width(toggleWidth));
                    EditorGUILayout.LabelField("Margins", GUILayout.ExpandWidth(false), GUILayout.MaxWidth(130));
                    EditorGUILayout.BeginVertical();
                    data.margins.x = EditorGUILayout.FloatField("Left", data.margins.x, GUILayout.ExpandWidth(false));
                    data.margins.y = EditorGUILayout.FloatField("Top", data.margins.y, GUILayout.ExpandWidth(false));
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.BeginVertical();
                    data.margins.z = EditorGUILayout.FloatField("Right", data.margins.z, GUILayout.ExpandWidth(false));
                    data.margins.w = EditorGUILayout.FloatField("Bottom", data.margins.w, GUILayout.ExpandWidth(false));
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    data.syncGeometrySorting = EditorGUILayout.Toggle(data.syncGeometrySorting, GUILayout.Width(toggleWidth));
                    data.geometrySorting = (TMPro.VertexSortingOrder)EditorGUILayout.EnumPopup("Geometry Sorting", data.geometrySorting);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    data.syncRichText = EditorGUILayout.Toggle(data.syncRichText, GUILayout.Width(toggleWidth));
                    data.richText = EditorGUILayout.Toggle("Rich Text", data.richText);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    data.syncRaycastTarget = EditorGUILayout.Toggle(data.syncRaycastTarget, GUILayout.Width(toggleWidth));
                    data.raycastTarget = EditorGUILayout.Toggle("Raycast Target", data.raycastTarget);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    data.syncParseEscapeCharacters = EditorGUILayout.Toggle(data.syncParseEscapeCharacters, GUILayout.Width(toggleWidth));
                    data.parseEscapeCharacters = EditorGUILayout.Toggle("Parse Escape Characters", data.parseEscapeCharacters);
                    EditorGUILayout.EndHorizontal();

                    //EditorGUILayout.BeginHorizontal();
                    //data.syncVisibleDescender = EditorGUILayout.Toggle(data.syncVisibleDescender, GUILayout.Width(toggleWidth));
                    //data.visibleDescender = EditorGUILayout.Toggle("Visible Descender*", data.visibleDescender);
                    //EditorGUILayout.EndHorizontal();
                    //EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    data.syncSpritieAsset = EditorGUILayout.Toggle(data.syncSpritieAsset, GUILayout.Width(toggleWidth));
                    data.spriteAsset = EditorGUILayout.ObjectField("Sprite Asset", data.spriteAsset, typeof(TMPro.TMP_SpriteAsset), false) as TMPro.TMP_SpriteAsset;
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    data.syncKerning = EditorGUILayout.Toggle(data.syncKerning, GUILayout.Width(toggleWidth));
                    data.kerning = EditorGUILayout.Toggle("Kerning", data.kerning);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    data.syncExtraPadding = EditorGUILayout.Toggle(data.syncExtraPadding, GUILayout.Width(toggleWidth));
                    data.extraPadding = EditorGUILayout.Toggle("Extra Padding", data.extraPadding);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("*Visible Descender is not 'settable' in the TMPro code?"); //it has a getter and setter, but the setter does not assign a value to protected varaible line 980 in TMP_Text

                }
                GUILayout.EndArea();
            }
        }

        void DrawTMProInputUI()
        {
            float startHeight = 10f;

            foreach (EasyUI_TMPInputStyle i in easyUI_Data.tmproInputList)
            {
                float lengthOfBox;
                if (!i.maximize)
                    lengthOfBox = 30f;
                else
                {
                    lengthOfBox = 640f;
                    if (i.customCaretColor)
                        lengthOfBox += 15f;

                    if (i.contentType == UnityEngine.UI.InputField.ContentType.Custom)
                    {
                        lengthOfBox += 60;

                        if (i.lineType != UnityEngine.UI.InputField.LineType.SingleLine)
                            lengthOfBox += 15;
                    }

                }

                lengthOfBox += BoxDrawAdjust(i);

#if UNITY_2019_3_OR_NEWER
                lengthOfBox *= 1.08f;
#endif

                //blank label allows scrollview to expand
                GUILayout.Label("", GUILayout.Height(lengthOfBox));
                //background shading
                GUILayout.BeginArea(new Rect(10f, startHeight, window.position.width - 30f, lengthOfBox), skin.box);
                startHeight += lengthOfBox + 10f;

                DrawButtonTemplate(i);

                if (i.maximize)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.BeginHorizontal();
                    i.syncText = EditorGUILayout.Toggle(i.syncText, GUILayout.Width(toggleWidth));
                    i.text = EditorGUILayout.TextField("Text", i.text);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Input Field Settings", skin.FindStyle("SectionHeading"));
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    i.syncFontAsset = EditorGUILayout.Toggle(i.syncFontAsset, GUILayout.Width(toggleWidth));
                    i.fontAsset = EditorGUILayout.ObjectField("Font Asset ", i.fontAsset, typeof(TMPro.TMP_FontAsset), false) as TMPro.TMP_FontAsset;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncPointSize = EditorGUILayout.Toggle(i.syncPointSize, GUILayout.Width(toggleWidth));
                    i.pointSize = EditorGUILayout.FloatField("Point Size", i.pointSize, GUILayout.ExpandWidth(false));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncCharacterLimit = EditorGUILayout.Toggle(i.syncCharacterLimit, GUILayout.Width(toggleWidth));
                    i.characterLimit = EditorGUILayout.IntField("Character Limit", i.characterLimit);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncContentType = EditorGUILayout.Toggle(i.syncContentType, GUILayout.Width(toggleWidth));
                    i.contentType = (UnityEngine.UI.InputField.ContentType)EditorGUILayout.EnumPopup("Content Type", i.contentType);
                    EditorGUILayout.EndHorizontal();

                    if (i.contentType == UnityEngine.UI.InputField.ContentType.Standard)
                    {
                        EditorGUILayout.BeginHorizontal();
                        i.syncLineType = EditorGUILayout.Toggle(i.syncLineType, GUILayout.Width(toggleWidth));
                        EditorGUI.indentLevel++;
                        i.lineType = (UnityEngine.UI.InputField.LineType)EditorGUILayout.EnumPopup("Line Type", i.lineType);
                        EditorGUI.indentLevel--;
                        EditorGUILayout.EndHorizontal();

                        if (i.lineType != UnityEngine.UI.InputField.LineType.SingleLine)
                        {
                            EditorGUILayout.BeginHorizontal();
                            i.syncLineLimit = EditorGUILayout.Toggle(i.syncLineLimit, GUILayout.Width(toggleWidth));
                            EditorGUI.indentLevel++;
                            i.lineLimit = EditorGUILayout.IntField("Line Limit", i.lineLimit);
                            EditorGUI.indentLevel--;
                            EditorGUILayout.EndHorizontal();
                        }
                    }
                    else if (i.contentType == UnityEngine.UI.InputField.ContentType.Custom)
                    {
                        EditorGUILayout.BeginHorizontal();
                        i.syncLineType = EditorGUILayout.Toggle(i.syncLineType, GUILayout.Width(toggleWidth));
                        EditorGUI.indentLevel++;
                        i.lineType = (UnityEngine.UI.InputField.LineType)EditorGUILayout.EnumPopup("Line Type", i.lineType);
                        EditorGUI.indentLevel--;
                        EditorGUILayout.EndHorizontal();

                        if (i.lineType != UnityEngine.UI.InputField.LineType.SingleLine)
                        {
                            EditorGUILayout.BeginHorizontal();
                            i.syncLineLimit = EditorGUILayout.Toggle(i.syncLineLimit, GUILayout.Width(toggleWidth));
                            EditorGUI.indentLevel++;
                            i.lineLimit = EditorGUILayout.IntField("Line Limit", i.lineLimit);
                            EditorGUI.indentLevel--;
                            EditorGUILayout.EndHorizontal();
                        }

                        EditorGUILayout.BeginHorizontal();
                        i.syncInputType = EditorGUILayout.Toggle(i.syncInputType, GUILayout.Width(toggleWidth));
                        EditorGUI.indentLevel++;
                        i.inputType = (UnityEngine.UI.InputField.InputType)EditorGUILayout.EnumPopup("Input Type", i.inputType);
                        EditorGUI.indentLevel--;
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        i.syncKeyboardType = EditorGUILayout.Toggle(i.syncKeyboardType, GUILayout.Width(toggleWidth));
                        EditorGUI.indentLevel++;
                        i.keyboardType = (TouchScreenKeyboardType)EditorGUILayout.EnumPopup("Keyboard Type", i.keyboardType);
                        EditorGUI.indentLevel--;
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        i.syncCharacterValidation = EditorGUILayout.Toggle(i.syncCharacterValidation, GUILayout.Width(toggleWidth));
                        EditorGUI.indentLevel++;
                        i.tmp_characterValidation = (TMPro.TMP_InputField.CharacterValidation)EditorGUILayout.EnumPopup("Character Validation", i.tmp_characterValidation);
                        EditorGUI.indentLevel--;
                        EditorGUILayout.EndHorizontal();

                        if (i.tmp_characterValidation == TMPro.TMP_InputField.CharacterValidation.Regex)
                        {
                            EditorGUI.indentLevel++;
                            EditorGUILayout.BeginHorizontal();
                            //i.syncRegexValue = EditorGUILayout.Toggle(i.syncRegexValue, GUILayout.Width(toggleWidth));
                            //i.regexValue = EditorGUILayout.TextField("Regex Value", i.regexValue);
                            EditorGUILayout.LabelField("*Regex Value is private and can not be set here :(");
                            EditorGUI.indentLevel--;
                            EditorGUILayout.EndHorizontal();
                        }
                        else if (i.tmp_characterValidation == TMPro.TMP_InputField.CharacterValidation.CustomValidator)
                        {
                            EditorGUILayout.BeginHorizontal();
                            i.syncInputValidator = EditorGUILayout.Toggle(i.syncInputValidator, GUILayout.Width(toggleWidth));
                            EditorGUI.indentLevel++;
                            i.tmp_inputValidator = EditorGUILayout.ObjectField("Input Validator", i.tmp_inputValidator, typeof(TMPro.TMP_InputValidator), false) as TMPro.TMP_InputValidator;
                            EditorGUI.indentLevel--;
                            EditorGUILayout.EndHorizontal();
                        }
                    }

                    EditorGUILayout.BeginHorizontal();
                    i.syncCaretBlinkRate = EditorGUILayout.Toggle(i.syncCaretBlinkRate, GUILayout.Width(toggleWidth));
                    i.caretBlinkRate = EditorGUILayout.Slider("Caret Blink Rate", i.caretBlinkRate, 0f, 4f);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncCaretWidth = EditorGUILayout.Toggle(i.syncCaretWidth, GUILayout.Width(toggleWidth));
                    i.caretWidth = EditorGUILayout.IntSlider("Caret Width", i.caretWidth, 1, 5);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncCustomCaretColor = EditorGUILayout.Toggle(i.syncCustomCaretColor, GUILayout.Width(toggleWidth));
                    i.customCaretColor = EditorGUILayout.Toggle("Custom Caret Color", i.customCaretColor);
                    EditorGUILayout.EndHorizontal();

                    if (i.customCaretColor)
                    {
                        EditorGUILayout.BeginHorizontal();
                        i.syncSelectionColor = EditorGUILayout.Toggle(i.syncSelectionColor, GUILayout.Width(toggleWidth));
                        i.caretColor = EditorGUILayout.ColorField("Caret Color", i.caretColor);
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.BeginHorizontal();
                    i.syncSelectionColor = EditorGUILayout.Toggle(i.syncSelectionColor, GUILayout.Width(toggleWidth));
                    i.selectionColor = EditorGUILayout.ColorField("Selection Color", i.selectionColor);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Control Settings", skin.FindStyle("SectionHeading"));
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    i.syncOnFucusSelectAll = EditorGUILayout.Toggle(i.syncOnFucusSelectAll, GUILayout.Width(toggleWidth));
                    i.onFocusSelectAll = EditorGUILayout.Toggle("On Focus Select All", i.onFocusSelectAll, GUILayout.Width(toggleWidth));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncResetOnDeactivation = EditorGUILayout.Toggle(i.syncResetOnDeactivation, GUILayout.Width(toggleWidth));
                    i.resetOnDeactivation = EditorGUILayout.Toggle("Reset on Deactivation", i.resetOnDeactivation, GUILayout.Width(toggleWidth));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncRetoreOnEscapeKey = EditorGUILayout.Toggle(i.syncRetoreOnEscapeKey, GUILayout.Width(toggleWidth));
                    i.restoreOnEscapeKey = EditorGUILayout.Toggle("Restore on ESC Key", i.restoreOnEscapeKey, GUILayout.Width(toggleWidth));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncHideSoftKeyboard = EditorGUILayout.Toggle(i.syncHideSoftKeyboard, GUILayout.Width(toggleWidth));
                    i.hideSoftKeyboard = EditorGUILayout.Toggle("Hide Soft Keyboard", i.hideSoftKeyboard, GUILayout.Width(toggleWidth));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncHideMobileInput = EditorGUILayout.Toggle(i.syncHideMobileInput, GUILayout.Width(toggleWidth));
                    i.hideMobileInput = EditorGUILayout.Toggle("Hide Mobile Input", i.hideMobileInput);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncReadOnly = EditorGUILayout.Toggle(i.syncReadOnly, GUILayout.Width(toggleWidth));
                    i.readOnly = EditorGUILayout.Toggle("Read Only", i.readOnly);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncRichText = EditorGUILayout.Toggle(i.syncRichText, GUILayout.Width(toggleWidth));
                    i.richText = EditorGUILayout.Toggle("Rich Text", i.richText);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    i.syncAllowRichTextEditing = EditorGUILayout.Toggle(i.syncAllowRichTextEditing, GUILayout.Width(toggleWidth));
                    i.allowRichTextEditing = EditorGUILayout.Toggle("Allow Rich Text Editing", i.allowRichTextEditing);
                    EditorGUILayout.EndHorizontal();
                    EditorGUI.indentLevel--;

                }

                GUILayout.EndArea();
            }
        }

        void DrawTMProDropdownUI()
        {
            float startHeight = 10f;

            foreach (EasyUI_TMPDropDownStyle d in easyUI_Data.tmproDropdownList)
            {
                float lengthOfBox;
                if (!d.maximize)
                    lengthOfBox = 30f;
                else
                {
                    lengthOfBox = 300f;
                    lengthOfBox += d.tmp_optionList.Count * 47f;
                }

                lengthOfBox += BoxDrawAdjust(d);

#if UNITY_2019_3_OR_NEWER
                lengthOfBox *= 1.085f;
#endif

                //blank label allows scrollview to expand
                GUILayout.Label("", GUILayout.Height(lengthOfBox));
                //background shading
                GUILayout.BeginArea(new Rect(10f, startHeight, window.position.width - 30f, lengthOfBox), skin.box);
                startHeight += lengthOfBox + 10f;

                DrawButtonTemplate(d);

                if (d.maximize)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.BeginHorizontal();
                    d.syncValue = EditorGUILayout.Toggle(d.syncValue, GUILayout.Width(toggleWidth));
                    d.value = EditorGUILayout.IntField("Value", d.value);
                    if (d.value >= d.tmp_optionList.Count)
                        d.value = d.tmp_optionList.Count - 1;
                    else if (d.value < 0)
                        d.value = 0;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    d.syncOptions = EditorGUILayout.Toggle(d.syncOptions, GUILayout.Width(toggleWidth));

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Dropdown Options");
                    if (GUILayout.Button("Add"))
                    {
                        TMPro.TMP_Dropdown.OptionData _option = new TMPro.TMP_Dropdown.OptionData();
                        _option.text = "Option " + (d.tmp_optionList.Count + 1);
                        d.AddOption(_option);
                    }
                    if (GUILayout.Button("Remove"))
                    {
                        d.RemoveOption(d.tmp_optionList[d.tmp_optionList.Count - 1]);
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndHorizontal();

                    foreach (TMPro.TMP_Dropdown.OptionData _option in d.tmp_optionList)
                    {
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.Space();
                        EditorGUILayout.BeginVertical();
                        _option.text = EditorGUILayout.TextField("Option", _option.text);
                        _option.image = EditorGUILayout.ObjectField(_option.image, typeof(Sprite), false) as Sprite;
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();

                    }
                    EditorGUI.indentLevel--;
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                }

                GUILayout.EndArea();
            }
        }

        //draws the core ui pieces for the button style
        void DrawButtonTemplate(EasyUI_ButtonStyle style)
        {
            DrawTopStyleButtons(style);

            if (style.maximize)
            {
                EditorGUILayout.Space();
                style.styleName = EditorGUILayout.TextField("Style Name : ", style.styleName);
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Sync", GUILayout.Width(50)))
                {
                    style.ToggleAll(!style.syncTransition);
                }
                EditorGUI.indentLevel++;
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                style.syncIsInteractable = EditorGUILayout.Toggle(style.syncIsInteractable, GUILayout.Width(toggleWidth));
                style.isInteractable = EditorGUILayout.Toggle("Interactable", style.isInteractable);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                style.syncTransition = EditorGUILayout.Toggle(style.syncTransition, GUILayout.Width(toggleWidth));
                style.transition = (UnityEngine.UI.Selectable.Transition)EditorGUILayout.EnumPopup("Transition", style.transition);
                EditorGUILayout.EndHorizontal();

                if (style.transition == UnityEngine.UI.Selectable.Transition.ColorTint)
                {
                    EditorGUILayout.BeginHorizontal();
                    style.syncNormalColor = EditorGUILayout.Toggle(style.syncNormalColor, GUILayout.Width(toggleWidth));
                    EditorGUI.indentLevel++;
                    style.colors.normalColor = EditorGUILayout.ColorField("Normal Color", style.colors.normalColor);
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    style.syncHighlightedColor = EditorGUILayout.Toggle(style.syncHighlightedColor, GUILayout.Width(toggleWidth));
                    EditorGUI.indentLevel++;
                    style.colors.highlightedColor = EditorGUILayout.ColorField("Highlighted Color", style.colors.highlightedColor);
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    style.syncPressedColor = EditorGUILayout.Toggle(style.syncPressedColor, GUILayout.Width(toggleWidth));
                    EditorGUI.indentLevel++;
                    style.colors.pressedColor = EditorGUILayout.ColorField("Pressed Color", style.colors.pressedColor);
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    style.syncSelectedColor = EditorGUILayout.Toggle(style.syncSelectedColor, GUILayout.Width(toggleWidth));
                    EditorGUI.indentLevel++;
                    style.colors.selectedColor = EditorGUILayout.ColorField("Selected Color", style.colors.selectedColor);
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    style.syncDisabledColor = EditorGUILayout.Toggle(style.syncDisabledColor, GUILayout.Width(toggleWidth));
                    EditorGUI.indentLevel++;
                    style.colors.disabledColor = EditorGUILayout.ColorField("Disabled Color", style.colors.disabledColor);
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    style.syncColorMultiplier = EditorGUILayout.Toggle(style.syncColorMultiplier, GUILayout.Width(toggleWidth));
                    EditorGUI.indentLevel++;
                    style.colorMultiplier = EditorGUILayout.Slider("Color Multiplier", style.colorMultiplier, 1f, 5f);
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    style.syncFadeDuration = EditorGUILayout.Toggle(style.syncFadeDuration, GUILayout.Width(toggleWidth));
                    EditorGUI.indentLevel++;
                    style.fadeDuration = EditorGUILayout.FloatField("Fade Duration", style.fadeDuration);
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();
                }
                else if (style.transition == UnityEngine.UI.Selectable.Transition.SpriteSwap)
                {
                    //EditorGUILayout.BeginHorizontal();
                    //style.syncTargetGraphic = EditorGUILayout.Toggle(style.syncTargetGraphic, GUILayout.Width(toggleWidth));
                    //EditorGUI.indentLevel++;
                    //style.targetGraphic = EditorGUILayout.ObjectField("Target Graphic", style.targetGraphic, typeof(Sprite), false, GUILayout.MaxHeight(16f)) as Sprite;
                    //EditorGUI.indentLevel--;
                    //EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    style.syncHighlightedSprite = EditorGUILayout.Toggle(style.syncHighlightedSprite, GUILayout.Width(toggleWidth));
                    EditorGUI.indentLevel++;
                    style.highlightedSprite = EditorGUILayout.ObjectField("Highlighted Sprite", style.highlightedSprite, typeof(Sprite), false, GUILayout.MaxHeight(16f)) as Sprite;
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    style.syncPressedSprite = EditorGUILayout.Toggle(style.syncPressedSprite, GUILayout.Width(toggleWidth));
                    EditorGUI.indentLevel++;
                    style.pressedSprite = EditorGUILayout.ObjectField("Pressed Sprite", style.pressedSprite, typeof(Sprite), false, GUILayout.MaxHeight(16f)) as Sprite;
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    style.syncSelectedSprite = EditorGUILayout.Toggle(style.syncSelectedSprite, GUILayout.Width(toggleWidth));
                    EditorGUI.indentLevel++;
                    style.selectedSprite = EditorGUILayout.ObjectField("Selected Sprite", style.selectedSprite, typeof(Sprite), false, GUILayout.MaxHeight(16f)) as Sprite; ;
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    style.syncDisabledSprite = EditorGUILayout.Toggle(style.syncDisabledSprite, GUILayout.Width(toggleWidth));
                    EditorGUI.indentLevel++;
                    style.disabledSprite = EditorGUILayout.ObjectField("Disabled Sprite", style.disabledSprite, typeof(Sprite), false, GUILayout.MaxHeight(16f)) as Sprite; ;
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();
                }
                else if (style.transition == UnityEngine.UI.Selectable.Transition.Animation)
                {
                    EditorGUILayout.BeginHorizontal();
                    style.syncNormalTrigger = EditorGUILayout.Toggle(style.syncNormalTrigger, GUILayout.Width(toggleWidth));
                    EditorGUI.indentLevel++;
                    style.normalTrigger = EditorGUILayout.TextField("Normal Trigger ", style.normalTrigger);
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    style.syncHighlightedTrigger = EditorGUILayout.Toggle(style.syncHighlightedTrigger, GUILayout.Width(toggleWidth));
                    EditorGUI.indentLevel++;
                    style.hightlightedTrigger = EditorGUILayout.TextField("Highlighted Trigger ", style.hightlightedTrigger);
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    style.syncPressedTrigger = EditorGUILayout.Toggle(style.syncPressedTrigger, GUILayout.Width(toggleWidth));
                    EditorGUI.indentLevel++;
                    style.pressedTrigger = EditorGUILayout.TextField("Pressed Trigger ", style.pressedTrigger);
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    style.syncDisabledTrigger = EditorGUILayout.Toggle(style.syncDisabledTrigger, GUILayout.Width(toggleWidth));
                    EditorGUI.indentLevel++;
                    style.disabledTrigger = EditorGUILayout.TextField("Disabled Trigger ", style.disabledTrigger);
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();
                }


                EditorGUILayout.BeginHorizontal();
                style.syncNavigation = EditorGUILayout.Toggle(style.syncNavigation, GUILayout.Width(toggleWidth));
                style.navigation.mode = (UnityEngine.UI.Navigation.Mode)EditorGUILayout.EnumPopup("Navigation", style.navigation.mode);
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
            }


        }

        //Adds style chooser to all children of a canvas
        void AddToALL<T>() where T : Component
        {
            Object[] tempArray;
            tempArray = Resources.FindObjectsOfTypeAll(typeof(Canvas));

            foreach (Object go in tempArray)
            {
                GameObject tempGo;
                tempGo = GameObject.Find(go.name);

                //Get all children
                Transform[] childList;
                childList = tempGo.GetComponentsInChildren<Transform>();

                foreach (Transform child in childList)
                {
                    if (child.GetComponent<T>() && !child.GetComponent<StyleChooser>())
                    {
                        child.gameObject.AddComponent<StyleChooser>();
                    }
                }
            }
        }

        //Removes Style Chooser to all children of a canvas
        void RemoveFromALL<T>() where T : Component
        {
            //return object not gameobject
            Object[] tempArray;
            tempArray = Resources.FindObjectsOfTypeAll(typeof(Canvas));

            foreach (Object go in tempArray)
            {
                //Search for gameobject by name to be able to get gameobject
                GameObject tempGo;
                tempGo = GameObject.Find(go.name);

                //Get all children
                Transform[] childList;
                childList = tempGo.GetComponentsInChildren<Transform>();

                foreach (Transform child in childList)
                {
                    if (child.GetComponent<T>() && child.GetComponent<StyleChooser>())
                    {
                        DestroyImmediate(child.gameObject.GetComponent<StyleChooser>());
                    }
                }
            }
        }

        public static void MaximizeAll(_type m_type, bool max)
        {
            switch (m_type)
            {
                case _type.Image:
                    foreach (EasyUI_ImageStyle i in easyUI_Data.imageList)
                    {
                        i.maximize = max;
                    }
                    break;
                case _type.Text:
                    foreach (EasyUI_TextStyle t in easyUI_Data.textList)
                    {
                        t.maximize = max;
                    }
                    break;
                case _type.Button:
                    foreach (EasyUI_ButtonStyle b in easyUI_Data.buttonList)
                    {
                        b.maximize = max;
                    }
                    break;
                case _type.Toggle:
                    foreach (EasyUI_ToggleStyle t in easyUI_Data.toggleList)
                    {
                        t.maximize = max;
                    }
                    break;
                case _type.Slider:
                    foreach (EasyUI_SliderStyle s in easyUI_Data.sliderList)
                    {
                        s.maximize = max;
                    }
                    break;
                case _type.InputField:
                    foreach (EasyUI_InputStyle i in easyUI_Data.inputList)
                    {
                        i.maximize = max;
                    }
                    break;
                case _type.Dropdown:
                    foreach (EasyUI_DropdownStyle d in easyUI_Data.dropdownList)
                    {
                        d.maximize = max;
                    }
                    break;
                case _type.TextMeshPro:
                    foreach (EasyUI_TMPTextStyle t in easyUI_Data.tmproTextList)
                    {
                        t.maximize = max;
                    }
                    break;
                case _type.TextMeshProInput:
                    foreach (EasyUI_TMPInputStyle t in easyUI_Data.tmproInputList)
                    {
                        t.maximize = max;
                    }
                    break;
                case _type.TextMeshProDropdown:
                    foreach (EasyUI_TMPDropDownStyle t in easyUI_Data.tmproDropdownList)
                    {
                        t.maximize = max;
                    }
                    break;
            }
        }

        public static void OpenStyle(_type m_type, int index)
        {
            if (index == 0)
                return;

            switch (m_type)
            {
                case _type.Image:
                    easyUI_Data.imageList[index - 1].maximize = true;
                    break;
                case _type.Text:
                    easyUI_Data.textList[index - 1].maximize = true;

                    break;
                case _type.Button:
                    easyUI_Data.buttonList[index - 1].maximize = true;

                    break;
                case _type.Toggle:
                    easyUI_Data.toggleList[index - 1].maximize = true;

                    break;
                case _type.Slider:
                    easyUI_Data.sliderList[index - 1].maximize = true;

                    break;
                case _type.InputField:
                    easyUI_Data.inputList[index - 1].maximize = true;
                    break;
                case _type.Dropdown:
                    easyUI_Data.dropdownList[index - 1].maximize = true;
                    break;
                case _type.TextMeshPro:
                    easyUI_Data.tmproTextList[index - 1].maximize = true;
                    break;
                case _type.TextMeshProInput:
                    easyUI_Data.tmproInputList[index - 1].maximize = true;
                    break;
                case _type.TextMeshProDropdown:
                    easyUI_Data.tmproDropdownList[index - 1].maximize = true;
                    break;

            }
        }

        //Draws basic buttons for each style
        void DrawTopStyleButtons(EasyUIStyle_Base _style)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(_style.styleName.ToString(), skin.GetStyle("StyleHeading"));

            if (_style.maximize)
            {
                if (GUILayout.Button("Minimize"))
                {
                    _style.maximize = false;
                }
            }
            else
            {
                if (GUILayout.Button("Maximize"))
                {
                    _style.maximize = true;
                }
            }
            if (GUILayout.Button("Duplicate"))
            {
                if (_style is EasyUI_ImageStyle)
                    DuplicateStyle(_style as EasyUI_ImageStyle);
                else if (_style is EasyUI_TextStyle)
                    DuplicateStyle(_style as EasyUI_TextStyle);
                else if (_style is EasyUI_SliderStyle)
                    DuplicateStyle(_style as EasyUI_SliderStyle);
                else if (_style is EasyUI_TMPInputStyle)
                    DuplicateStyle(_style as EasyUI_TMPInputStyle);
                else if (_style is EasyUI_InputStyle)
                    DuplicateStyle(_style as EasyUI_InputStyle);
                else if (_style is EasyUI_ToggleStyle)
                    DuplicateStyle(_style as EasyUI_ToggleStyle);
                else if (_style is EasyUI_TMPDropDownStyle)
                    DuplicateStyle(_style as EasyUI_TMPDropDownStyle);
                else if (_style is EasyUI_DropdownStyle)
                    DuplicateStyle(_style as EasyUI_DropdownStyle);
                else if (_style is EasyUI_ButtonStyle)
                    DuplicateStyle(_style as EasyUI_ButtonStyle);
                else if (_style is EasyUI_TMPTextStyle)
                    DuplicateStyle(_style as EasyUI_TMPTextStyle);


            }
            if (GUILayout.Button("Remove"))
            {
                Debug.Log("remove button - " + _style.GetType());


                if (_style is EasyUI_ImageStyle)
                    removeImageList.Add(_style as EasyUI_ImageStyle);
                else if (_style is EasyUI_TextStyle)
                    removeTextList.Add(_style as EasyUI_TextStyle);
                else if (_style is EasyUI_SliderStyle)
                    removeSliderList.Add(_style as EasyUI_SliderStyle);
                else if (_style is EasyUI_ToggleStyle)
                    removeToggleList.Add(_style as EasyUI_ToggleStyle);
                else if (_style is EasyUI_TMPInputStyle)
                    removeTMProInputList.Add(_style as EasyUI_TMPInputStyle);
                else if (_style is EasyUI_InputStyle)
                    removeInputList.Add(_style as EasyUI_InputStyle);
                else if (_style is EasyUI_TMPDropDownStyle)
                    removeTMProDropdownList.Add(_style as EasyUI_TMPDropDownStyle);
                else if (_style is EasyUI_DropdownStyle)
                    removeDropdownList.Add(_style as EasyUI_DropdownStyle);
                else if (_style is EasyUI_ButtonStyle)
                {
                    removeButtonList.Add(_style as EasyUI_ButtonStyle);
                }
                else if (_style is EasyUI_TMPTextStyle)
                    removeTMProTextList.Add(_style as EasyUI_TMPTextStyle);


            }
            //if (GUILayout.Button("^", GUILayout.Width(20)))
            //{
            //    moveList.Add(new MoveInfo(_style, true));
            //    Debug.Log("Moving Up");
            //}
            //if (GUILayout.Button("v", GUILayout.Width(20)))
            //{
            //    moveList.Add(new MoveInfo(_style, false));
            //}
            EditorGUILayout.EndHorizontal();
        }

        void MoveStyle(EasyUIStyle_Base _style, bool moveUp)
        {

            if (_style is EasyUI_ImageStyle)
                MoveStyle(_style as EasyUI_ImageStyle, moveUp, easyUI_Data.imageList);
            else if (_style is EasyUI_TextStyle)
                MoveStyle(_style as EasyUI_ImageStyle, moveUp, easyUI_Data.imageList);
            else if (_style is EasyUI_SliderStyle)
                MoveStyle(_style as EasyUI_ImageStyle, moveUp, easyUI_Data.imageList);
            else if (_style is EasyUI_TMPInputStyle)
                MoveStyle(_style as EasyUI_ImageStyle, moveUp, easyUI_Data.imageList);
            else if (_style is EasyUI_InputStyle)
                MoveStyle(_style as EasyUI_ImageStyle, moveUp, easyUI_Data.imageList);
            else if (_style is EasyUI_ToggleStyle)
                MoveStyle(_style as EasyUI_ImageStyle, moveUp, easyUI_Data.imageList);
            else if (_style is EasyUI_TMPDropDownStyle)
                MoveStyle(_style as EasyUI_ImageStyle, moveUp, easyUI_Data.imageList);
            else if (_style is EasyUI_DropdownStyle)
                MoveStyle(_style as EasyUI_ImageStyle, moveUp, easyUI_Data.imageList);
            else if (_style is EasyUI_ButtonStyle)
                MoveStyle(_style as EasyUI_ImageStyle, moveUp, easyUI_Data.imageList);
            else if (_style is EasyUI_TMPTextStyle)
                MoveStyle(_style as EasyUI_ImageStyle, moveUp, easyUI_Data.imageList);
        }

        void MoveStyle<T>(T _style, bool moveUp, List<T> _styleList)
        {
            if (!_styleList.Contains(_style))
                return;

            int currentPos;
            int nextPos;

            currentPos = _styleList.IndexOf(_style);

            if (currentPos == 0 && moveUp)
                return;
            else if (currentPos == _styleList.Count - 1 && !moveUp)
                return;

            if (moveUp)
                nextPos = currentPos - 1;
            else
                nextPos = currentPos + 1;

            Debug.Log("Moving style from " + currentPos + " to " + nextPos);

            T style1 = _styleList[currentPos];
            T style2 = _styleList[nextPos];

            _styleList[currentPos] = style2;
            _styleList[nextPos] = style1;
        }

        class MoveInfo
        {
            public MoveInfo(EasyUIStyle_Base style, bool moveUp)
            {
                this._style = style;
                this.moveUp = moveUp;
            }
            public EasyUIStyle_Base _style;
            public bool moveUp;
        }

        //Used to adjust size of background box based on transition type
        float BoxDrawAdjust(EasyUI_ButtonStyle b)
        {
            if (b.transition == UnityEngine.UI.Selectable.Transition.None)
                return -110f;
            else if (b.transition == UnityEngine.UI.Selectable.Transition.ColorTint)
                return 0;
            else if (b.transition == UnityEngine.UI.Selectable.Transition.SpriteSwap)
                return -25f;
            else //Animation
                return -35f;
        }
    }
}
