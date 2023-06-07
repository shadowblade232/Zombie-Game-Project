using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using EasyUIStyle;
using EasyUIStyle.UI;
using System;
using TMPro;
using Object = System.Object;
using System.Collections.Generic;

public class ContextMenuAdditions {

    [MenuItem("CONTEXT/Text/Replace with TMP Text")]
    static void ReplaceWithTMP(MenuCommand command)
    {
        Text text = (Text)command.context;
        GameObject go = text.gameObject;
        string textValue = text.text;
        UnityEngine.GameObject.DestroyImmediate(text);
        go.AddComponent<TMPro.TextMeshProUGUI>().text = textValue;

        StyleChooser sc = go.GetComponent<StyleChooser>();
        if (sc != null)
            sc.Initialize();
    }

    [MenuItem("CONTEXT/TMP_Text/Replace with UGUI Text")]
    static void ReplaceWithText(MenuCommand command)
    {
        TMPro.TMP_Text text = (TMPro.TMP_Text)command.context;
        GameObject go = text.gameObject;
        string textValue = text.text;
        UnityEngine.GameObject.DestroyImmediate(text);
        go.AddComponent<UnityEngine.UI.Text>().text = textValue;

        StyleChooser sc = go.GetComponent<StyleChooser>();
        if (sc != null)
            sc.Initialize();
    }

    //[MenuItem("CONTEXT/Text/Add Easy UI Style")]
    //[MenuItem("CONTEXT/Button/Add Easy UI Style")]
    //[MenuItem("CONTEXT/Image/Add Easy UI Style")]
    //[MenuItem("CONTEXT/Toggle/Add Easy UI Style")]
    //[MenuItem("CONTEXT/Slider/Add Easy UI Style")]
    //[MenuItem("CONTEXT/Dropdown/Add Easy UI Style")]
    //[MenuItem("CONTEXT/InputField/Add Easy UI Style")]
    //[MenuItem("CONTEXT/TMP_Text/Add Easy UI Style")]
    //[MenuItem("CONTEXT/TMP_InputField/Add Easy UI Style")]
    //[MenuItem("CONTEXT/TMP_Dropdown/Add Easy UI Style")]
    //static void AddButtonStyle(MenuCommand command)
    //{
    //    if (command.context.GetType() == typeof(Text))
    //    {
    //        Text text = (Text)command.context;
    //        if (!HasStyleChooser(text.gameObject) || GetIndex(command) == text.GetComponent<StyleChooser>().textStyleIndex)
    //            EasyUIStyleEditor.AddStyle(text);
    //        else
    //        {
    //            OpenEditorWindow();
    //            EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.Text;
    //            EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
    //            EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, text.GetComponent<StyleChooser>().textStyleIndex);
    //        }
    //    }
    //    else if (command.context.GetType() == typeof(Button))
    //    {
    //        Button button = (Button)command.context;
    //        if (!HasStyleChooser(button.gameObject) || GetIndex(command) == button.GetComponent<StyleChooser>().buttonStyleIndex)
    //            EasyUIStyleEditor.AddStyle(button);
    //        else
    //        {
    //            OpenEditorWindow();
    //            EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.Button;
    //            EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
    //            EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, button.GetComponent<StyleChooser>().buttonStyleIndex);
    //        }
    //    }
    //    else if (command.context.GetType() == typeof(Image))
    //    {
    //        Image image = (Image)command.context;
    //        if (!HasStyleChooser(image.gameObject) || GetIndex(command) == image.GetComponent<StyleChooser>().imageStyleIndex)
    //            EasyUIStyleEditor.AddStyle(image);
    //        else
    //        {
    //            OpenEditorWindow();
    //            EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.Image;
    //            EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
    //            EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, image.GetComponent<StyleChooser>().imageStyleIndex);
    //        }
    //    }
    //    else if (command.context.GetType() == typeof(Toggle))
    //    {
    //        Toggle toggle = (Toggle)command.context;
    //        if (!HasStyleChooser(toggle.gameObject) || toggle.GetComponent<StyleChooser>().toggleStyleIndex == 0)
    //            EasyUIStyleEditor.AddStyle(toggle);
    //        else
    //        {
    //            OpenEditorWindow();
    //            EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.Toggle;
    //            EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
    //            EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, toggle.GetComponent<StyleChooser>().toggleStyleIndex);
    //        }
    //    }
    //    else if (command.context.GetType() == typeof(Slider))
    //    {
    //        Slider slider = (Slider)command.context;
    //        if (!HasStyleChooser(slider.gameObject) || GetIndex(command) == slider.GetComponent<StyleChooser>().sliderStyleIndex)
    //            EasyUIStyleEditor.AddStyle(slider);
    //        else
    //        {
    //            OpenEditorWindow();
    //            EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.Slider;
    //            EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
    //            EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, slider.GetComponent<StyleChooser>().sliderStyleIndex);
    //        }
    //    }
    //    else if (command.context.GetType() == typeof(TMPro.TMP_Dropdown))
    //    {
    //        TMPro.TMP_Dropdown dropDown = (TMPro.TMP_Dropdown)command.context;
    //        if (!HasStyleChooser(dropDown.gameObject) || GetIndex(command) == dropDown.GetComponent<StyleChooser>().tmproDropdownStyleIndex)
    //            EasyUIStyleEditor.AddStyle(dropDown);
    //        else
    //        {
    //            OpenEditorWindow();
    //            EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.TextMeshProDropdown;
    //            EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
    //            EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, dropDown.GetComponent<StyleChooser>().tmproDropdownStyleIndex);
    //        }

    //        Debug.Log("Copying tmp dropdown");
    //    }
    //    else if (command.context.GetType() == typeof(Dropdown))
    //    {
    //        Dropdown dropDown = (Dropdown)command.context;
    //        if (!HasStyleChooser(dropDown.gameObject) || GetIndex(command) == dropDown.GetComponent<StyleChooser>().dropdownStyleIndex)
    //            EasyUIStyleEditor.AddStyle(dropDown);
    //        else
    //        {
    //            OpenEditorWindow();
    //            EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.Dropdown;
    //            EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
    //            EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, dropDown.GetComponent<StyleChooser>().dropdownStyleIndex);
    //        }
    //    }
    //    else if (command.context.GetType() == typeof(TMPro.TMP_InputField))
    //    {
    //        TMPro.TMP_InputField inputField = (TMPro.TMP_InputField)command.context;
    //        if (!HasStyleChooser(inputField.gameObject) || GetIndex(command) == inputField.GetComponent<StyleChooser>().tmproInputStyleIndex )
    //            EasyUIStyleEditor.AddStyle(inputField);
    //        else
    //        {
    //            OpenEditorWindow();
    //            EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.TextMeshProInput;
    //            EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
    //            EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, GetIndex(command));
    //        }
    //    }
    //    else if (command.context.GetType() == typeof(InputField))
    //    {
    //        InputField inputField = (InputField)command.context;
    //        if (!HasStyleChooser(inputField.gameObject) || GetIndex(command) == inputField.GetComponent<StyleChooser>().inputStyleIndex)
    //            EasyUIStyleEditor.AddStyle(inputField);
    //        else
    //        {
    //            OpenEditorWindow();
    //            EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.InputField;
    //            EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
    //            EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, GetIndex(command));
    //        }
    //    }
    //    else if (command.context.GetType() == typeof(TMPro.TextMeshProUGUI))
    //    {
    //        TMPro.TMP_Text tmpText = (TMPro.TMP_Text)command.context;
    //        if (!HasStyleChooser(tmpText.gameObject) || GetIndex(command) == tmpText.GetComponent<StyleChooser>().tmproTextStyleIndex)
    //            EasyUIStyleEditor.AddStyle(tmpText);
    //        else
    //        {
    //            OpenEditorWindow();
    //            EasyUIStyleEditor.thisType = EasyUIStyleEditor._type.TextMeshPro;
    //            EasyUIStyleEditor.MaximizeAll(EasyUIStyleEditor.thisType, false);
    //            EasyUIStyleEditor.OpenStyle(EasyUIStyleEditor.thisType, GetIndex(command));
    //        }

    //    }
    //}

    [MenuItem("CONTEXT/Canvas/Convert All to Easy UI Styles")]
    private static void ConvertCanvas(MenuCommand command)
    {
        if(command.context.GetType() == typeof(Canvas))
        {
            Canvas canvas = (Canvas)command.context;
            Undo.RecordObject(canvas, "Convert Canvas to EUIS");

            Image[] imageList = canvas.gameObject.GetComponentsInChildren<Image>();
            foreach (Image image in imageList)
            {
                RemoveStyleChooser(image.gameObject);
                ReplaceImage(image);
            }

            Text[] textList = canvas.gameObject.GetComponentsInChildren<Text>();
            foreach (Text text in textList)
            {
                RemoveStyleChooser(text.gameObject);
                ReplaceText(text);
            }

            Button[] buttonList = canvas.gameObject.GetComponentsInChildren<Button>();
            foreach (Button button in buttonList)
            {
                RemoveStyleChooser(button.gameObject);
                ConvertButton(button);
            }

            Toggle[] toggleList = canvas.gameObject.GetComponentsInChildren<Toggle>();
            foreach (Toggle toggle in toggleList)
            {
                RemoveStyleChooser(toggle.gameObject);
                ConvertToggle(toggle);
            }

            Slider[] sliderList = canvas.gameObject.GetComponentsInChildren<Slider>();
            foreach (Slider slider in sliderList)
            {
                RemoveStyleChooser(slider.gameObject);
                ConvertSlider(slider);
            }

            Dropdown[] dropdownList = canvas.gameObject.GetComponentsInChildren<Dropdown>();
            foreach (Dropdown dropdown in dropdownList)
            {
                RemoveStyleChooser(dropdown.gameObject);
                ConvertDropdown(dropdown);
            }

            TMP_InputField[] tmpInputList = canvas.gameObject.GetComponentsInChildren<TMP_InputField>();
            foreach (TMP_InputField inputField in tmpInputList)
            {
                RemoveStyleChooser(inputField.gameObject);
                ConvertTMPInputField(inputField);
            }

            TMP_Text[] tmpTextList = canvas.gameObject.GetComponentsInChildren<TMP_Text>();
            foreach (TMP_Text text in tmpTextList)
            {
                RemoveStyleChooser(text.gameObject);
                ConvertTMPText(text);
            }

            InputField[] inputList = canvas.gameObject.GetComponentsInChildren<InputField>();
            foreach (InputField input in inputList)
            {
                RemoveStyleChooser(input.gameObject);
                ConvertInputField(input);
            }
        }
    }

    private static void RemoveStyleChooser(GameObject gameObject)
    {
        StyleChooser sc = gameObject.GetComponent<StyleChooser>();
        if (sc != null)
            GameObject.DestroyImmediate(sc);
    }


    [MenuItem("CONTEXT/Text/Convert To Easy UI Styles")]
    [MenuItem("CONTEXT/Button/Convert To Easy UI Styles")]
    [MenuItem("CONTEXT/Image/Convert To Easy UI Styles")]
    [MenuItem("CONTEXT/Toggle/Convert To Easy UI Styles")]
    [MenuItem("CONTEXT/Slider/Convert To Easy UI Styles")]
    [MenuItem("CONTEXT/Dropdown/Convert To Easy UI Styles")]
    [MenuItem("CONTEXT/InputField/Convert To Easy UI Styles")]
    [MenuItem("CONTEXT/TMP_Text/Convert To Easy UI Styles")]
    [MenuItem("CONTEXT/TMP_InputField/Convert To Easy UI Styles")]
    static void ConvertToEUI(MenuCommand command)
    {
        if (command.context.GetType() == typeof(Text))
        {
            Text text = (Text)command.context;
            ReplaceText(text);
        }
        else if (command.context.GetType() == typeof(Button))
        {
            Button button = (Button)command.context;
            ReplaceButton(button);
        }
        else if (command.context.GetType() == typeof(Image))
        {
            Image image = (Image)command.context;
            ReplaceImage(image);
        }
        else if (command.context.GetType() == typeof(Toggle))
        {
            Toggle toggle = (Toggle)command.context;
            ReplaceToggle(toggle);
        }
        else if (command.context.GetType() == typeof(Slider))
        {
            Slider slider = (Slider)command.context;
            ReplaceSlider(slider);
        }
        else if (command.context.GetType() == typeof(TMPro.TMP_Dropdown))
        {
            TMPro.TMP_Dropdown dropDown = (TMPro.TMP_Dropdown)command.context;
            ReplaceTMPDropdown(dropDown);
        }
        else if (command.context.GetType() == typeof(Dropdown))
        {
            Dropdown dropDown = (Dropdown)command.context;
            ReplaceDropdown(dropDown);
        }
        else if (command.context.GetType() == typeof(TMPro.TMP_InputField))
        {
            TMPro.TMP_InputField inputField = (TMPro.TMP_InputField)command.context;
            ReplaceTMPInputField(inputField);
        }
        else if (command.context.GetType() == typeof(InputField))
        {
            InputField inputField = (InputField)command.context;
            ReplaceInputField(inputField);
        }
        else if (command.context.GetType() == typeof(TMPro.TextMeshProUGUI))
        {
            TMPro.TMP_Text tmpText = (TMPro.TMP_Text)command.context;
            ReplaceTextTMP(tmpText);
        }
    }

    [MenuItem("CONTEXT/Button/Convert Object To Easy UI Styles")]
    [MenuItem("CONTEXT/Toggle/Convert Object To Easy UI Styles")]
    [MenuItem("CONTEXT/Slider/Convert Object To Easy UI Styles")]
    [MenuItem("CONTEXT/Dropdown/Convert Object To Easy UI Styles")]
    [MenuItem("CONTEXT/InputField/Convert Object To Easy UI Styles")]
    [MenuItem("CONTEXT/TMP_InputField/Convert Object To Easy UI Styles")]
    //[MenuItem("CONTEXT/TMP_Dropdown/Convert Object To Easy UI Styles")]
    static void ConvertObjectToEUI(MenuCommand command)
    {
        if (command.context.GetType() == typeof(Button))
        {
            Button button = (Button)command.context;
            ConvertButton(button);
        }
        else if (command.context.GetType() == typeof(Toggle))
        {
            Toggle toggle = (Toggle)command.context;
            ConvertToggle(toggle);
        }
        else if (command.context.GetType() == typeof(Slider))
        {
            Slider slider = (Slider)command.context;
            ConvertSlider(slider);
        }
        else if (command.context.GetType() == typeof(TMPro.TMP_Dropdown))
        {
            TMPro.TMP_Dropdown dropDown = (TMPro.TMP_Dropdown)command.context;
            ConvertTMPDropdown(dropDown);

        }
        else if (command.context.GetType() == typeof(Dropdown))
        {
            Dropdown dropDown = (Dropdown)command.context;
            ConvertDropdown(dropDown);

        }
        else if (command.context.GetType() == typeof(TMPro.TMP_InputField))
        {
            TMPro.TMP_InputField inputField = (TMPro.TMP_InputField)command.context;
            ConvertTMPInputField(inputField);
        }
        else if (command.context.GetType() == typeof(InputField))
        {
            InputField inputField = (InputField)command.context;
            ConvertInputField(inputField);
        }
        else if (command.context.GetType() == typeof(TMPro.TextMeshProUGUI))
        {
            TMPro.TMP_Text tmpText = (TMPro.TMP_Text)command.context;
            ConvertTMPText(tmpText);
        }
    }

    private static void ConvertButton(Button button)
    {
        EasyUIMenuOptions.ReplaceImages(button.gameObject);
        EasyUIMenuOptions.ReplaceText(button.gameObject);
        ReplaceButton(button);
    }


    private static void ConvertToggle(Toggle toggle)
    {
        ToggleGroup tg = toggle.group;

        EasyUIMenuOptions.ReplaceImages(toggle.gameObject);
        EasyUIMenuOptions.ReplaceText(toggle.gameObject);
        EasyUIToggle newToggle = ReplaceToggle(toggle);
        newToggle.group = tg;

        foreach (Transform child in newToggle.transform)
        {
            if (child.name == "Background")
            {
                UnityEngine.Object.DestroyImmediate(child.GetComponent<Image>());
                Image image = child.gameObject.AddComponent<EasyUIImage>();
                newToggle.targetGraphic = image;
                newToggle.isOn = true;

                GameObject _go = child.Find("Checkmark").gameObject;
                UnityEngine.Object.DestroyImmediate(_go.GetComponent<Image>());

                EasyUIImage _image = _go.gameObject.AddComponent<EasyUIImage>();
                newToggle.graphic = _image;

            }
            else if (child.name == "Label")
            {
                UnityEngine.Object.DestroyImmediate(child.GetComponent<Text>());
                child.gameObject.AddComponent<EasyUIText>().text = "Easy UI Toggle";
            }
        }
    }

    private static void ConvertSlider(Slider slider)
    {
        EasyUIMenuOptions.ReplaceImages(slider.gameObject);
        EasyUIMenuOptions.ReplaceText(slider.gameObject);
        EasyUISlider newSlider = ReplaceSlider(slider);

        //don't like doing it this way, but didn't seem to work otherwise...
        foreach (Image image in newSlider.GetComponentsInChildren<Image>())
        {
            if (image.name == "Handle")
            {
                newSlider.targetGraphic = image;
                break;
            }
        }
    }

    private static void ConvertTMPDropdown(TMPro.TMP_Dropdown dropDown)
    {
        dropDown.transform.Find("Template").gameObject.SetActive(true);
        EasyUIMenuOptions.ReplaceImages(dropDown.gameObject);
        EasyUIMenuOptions.ReplaceText(dropDown.gameObject);
        EasyUITMProDropdown newDropDown = ReplaceTMPDropdown(dropDown);
        newDropDown.targetGraphic = newDropDown.gameObject.GetComponent<Image>();
        newDropDown.gameObject.GetComponent<Image>().type = Image.Type.Sliced;
        newDropDown.transform.Find("Template").gameObject.SetActive(false);
    }

    private static void ConvertDropdown(Dropdown dropDown)
    {
        dropDown.transform.Find("Template").gameObject.SetActive(true);
        EasyUIMenuOptions.ReplaceImages(dropDown.gameObject);
        EasyUIMenuOptions.ReplaceText(dropDown.gameObject);

        Transform item = dropDown.gameObject.GetComponentInChildren<Toggle>().transform;
        UnityEngine.Object.DestroyImmediate(item.GetComponent<Toggle>());
        EasyUIToggle toggle = item.gameObject.AddComponent<EasyUIToggle>();


        EasyUIDropdown newDropdown = ReplaceDropdown(dropDown);
        foreach (Image image in newDropdown.gameObject.GetComponentsInChildren<Image>())
        {
            if (image.name == "Item Background")
            {
                toggle.targetGraphic = image;
            }
        }
        newDropdown.GetComponent<Image>().type = Image.Type.Sliced;
        newDropdown.targetGraphic = newDropdown.gameObject.GetComponent<Image>();
        newDropdown.transform.Find("Template").gameObject.SetActive(false);
    }

    private static void ConvertTMPInputField(TMPro.TMP_InputField inputField)
    {
        EasyUIMenuOptions.ReplaceImages(inputField.gameObject);
        EasyUIMenuOptions.ReplaceText(inputField.gameObject);
        EasyUITMProInput newInput = ReplaceTMPInputField(inputField);
        newInput.gameObject.GetComponent<Image>().type = Image.Type.Sliced;
        newInput.targetGraphic = newInput.gameObject.GetComponent<Image>();
    }

    private static void ConvertInputField(InputField inputField)
    {
        EasyUIMenuOptions.ReplaceImages(inputField.gameObject);
        EasyUIMenuOptions.ReplaceText(inputField.gameObject);
        ReplaceInputField(inputField);
    }

    private static void ConvertTMPText(TMPro.TMP_Text tmpText)
    {
        ReplaceTextTMP(tmpText);
    }

    private static int GetIndex(MenuCommand command)
    {
        if (command.context.GetType() == typeof(Text))
        {
            Text text = (Text)command.context;
            return text.GetComponent<StyleChooser>().textStyleIndex;
        }
        else if (command.context.GetType() == typeof(Button))
        {
            Button button = (Button)command.context;
            return button.GetComponent<StyleChooser>().buttonStyleIndex;
        }
        else if (command.context.GetType() == typeof(Image))
        {
            Image image = (Image)command.context;
            return image.GetComponent<StyleChooser>().imageStyleIndex;

        }
        else if (command.context.GetType() == typeof(Toggle))
        {
            Toggle toggle = (Toggle)command.context;
            return toggle.GetComponent<StyleChooser>().toggleStyleIndex;
        }
        else if (command.context.GetType() == typeof(Slider))
        {
            Slider slider = (Slider)command.context;
            return slider.GetComponent<StyleChooser>().sliderStyleIndex;

        }
        else if (command.context.GetType() == typeof(TMPro.TMP_Dropdown))
        {
            TMPro.TMP_Dropdown dropDown = (TMPro.TMP_Dropdown)command.context;
            return dropDown.GetComponent<StyleChooser>().tmproDropdownStyleIndex;
        }
        else if (command.context.GetType() == typeof(Dropdown))
        {
            Dropdown dropDown = (Dropdown)command.context;
            return dropDown.GetComponent<StyleChooser>().dropdownStyleIndex;

        }
        else if (command.context.GetType() == typeof(TMPro.TMP_InputField))
        {
            TMPro.TMP_InputField inputField = (TMPro.TMP_InputField)command.context;
            return inputField.GetComponent<StyleChooser>().tmproInputStyleIndex;
        }
        else if (command.context.GetType() == typeof(InputField))
        {
            InputField inputField = (InputField)command.context;
            return inputField.GetComponent<StyleChooser>().inputStyleIndex;
        }
        else if (command.context.GetType() == typeof(TMPro.TextMeshProUGUI))
        {
            TMPro.TMP_Text tmpText = (TMPro.TMP_Text)command.context;
            return tmpText.GetComponent<StyleChooser>().tmproTextStyleIndex;
        }
        else
            return 0;
    }

    private static bool HasStyleChooser(GameObject go)
    {
        if (go.GetComponent<StyleChooser>() == null)
            return false;
        else
            return true;
    }

    private static EasyUIText ReplaceText(Text text)
    {
        GameObject go = text.gameObject;
        string _text = text.text;
        UnityEngine.Object.DestroyImmediate(text);
        EasyUIText newText = go.AddComponent<EasyUIText>();
        newText.text = _text;

        return newText;
    }

    private static EasyUIButton ReplaceButton(Button button)
    {
        GameObject go = button.gameObject;
        Graphic target = button.targetGraphic;
        if (target == null)
            target = button.GetComponent<Image>();

        Button.ButtonClickedEvent onClick = button.onClick;

        UnityEngine.Object.DestroyImmediate(button);
        EasyUIButton newButton = go.AddComponent<EasyUIButton>();
        if(target != null)
            newButton.targetGraphic = target;
        newButton.onClick = onClick;

        return newButton;
    }

    private static EasyUIImage ReplaceImage(Image image)
    {
        GameObject go = image.gameObject;
        UnityEngine.Object.DestroyImmediate(image);
        return go.AddComponent<EasyUIImage>();
    }

    private static EasyUIToggle ReplaceToggle(Toggle toggle)
    {
        GameObject go = toggle.gameObject;
        Graphic graphicTarget = toggle.targetGraphic;
        Graphic graphic = toggle.graphic;

        string toggleLabel = go.GetComponentInChildren<Text>().text;

        Toggle.ToggleEvent onValueChanged = toggle.onValueChanged;

        UnityEngine.Object.DestroyImmediate(toggle);
        EasyUIToggle newToggle = go.AddComponent<EasyUIToggle>();
        newToggle.targetGraphic = graphicTarget;
        newToggle.graphic = graphic;
        newToggle.onValueChanged = onValueChanged;
        newToggle.GetComponentInChildren<Text>().text = toggleLabel;

        return newToggle;
    }

    private static EasyUISlider ReplaceSlider(Slider slider)
    {
        GameObject go = slider.gameObject;
        Graphic graphicTarget = slider.targetGraphic;
        RectTransform handleRect = slider.handleRect;
        RectTransform fillRect = slider.fillRect;

        Slider.SliderEvent onValueChanged = slider.onValueChanged;

        UnityEngine.Object.DestroyImmediate(slider);
        EasyUISlider newSlider = go.AddComponent<EasyUISlider>();
        newSlider.targetGraphic = graphicTarget;
        newSlider.handleRect = handleRect;
        newSlider.fillRect = fillRect;
        newSlider.onValueChanged = onValueChanged;

        return newSlider;
    }

    private static EasyUIDropdown ReplaceDropdown(Dropdown dropDown)
    {
        GameObject go = dropDown.gameObject;
        Graphic graphic = dropDown.targetGraphic;
        RectTransform template = dropDown.template;
        Text captionText = dropDown.captionText;
        Text itemText = dropDown.itemText;
        List<Dropdown.OptionData> options = dropDown.options;

        Dropdown.DropdownEvent onValueChanged = dropDown.onValueChanged;

        UnityEngine.Object.DestroyImmediate(dropDown);
        EasyUIDropdown newDropdown = go.AddComponent<EasyUIDropdown>();
        newDropdown.targetGraphic = graphic;
        newDropdown.template = template;
        newDropdown.AddOptions(options);

        Text[] textList = newDropdown.gameObject.GetComponentsInChildren<Text>();
        foreach (Text _text in textList)
        {
            if (_text.name == "Item Label")
                newDropdown.itemText = _text;
            else if (_text.name == "Label")
                newDropdown.captionText = _text;
        }

        newDropdown.onValueChanged = onValueChanged;

        return newDropdown;
    }

    private static EasyUITMProDropdown ReplaceTMPDropdown(TMP_Dropdown dropDown)
    {
        GameObject go = dropDown.gameObject;
        Graphic graphic = dropDown.targetGraphic;
        RectTransform template = dropDown.template;
        TMPro.TMP_Text captionText = dropDown.captionText;
        TMPro.TMP_Text itemText = dropDown.itemText;
        List<TMPro.TMP_Dropdown.OptionData> options = dropDown.options;

        TMP_Dropdown.DropdownEvent onValueChanged = dropDown.onValueChanged;

        UnityEngine.Object.DestroyImmediate(dropDown);
        EasyUITMProDropdown newDropdown = go.AddComponent<EasyUITMProDropdown>();
        newDropdown.targetGraphic = graphic;
        newDropdown.template = template;
        newDropdown.AddOptions(options);

        TMP_Text[] textList = newDropdown.gameObject.GetComponentsInChildren<TMP_Text>();
        foreach (TMPro.TMP_Text _text in textList)
        {
            if (_text.name == "Item Label")
                newDropdown.itemText = _text;
            else if (_text.name == "Label")
                newDropdown.captionText = _text;   
        }

        newDropdown.onValueChanged = onValueChanged;
        return newDropdown;
    }

    private static EasyUITMProInput ReplaceTMPInputField(TMP_InputField inputField)
    {
        GameObject go = inputField.gameObject;
        Graphic graphicTarget = inputField.targetGraphic;
        RectTransform textViewport = inputField.textViewport;
        string text = inputField.text;

        TMP_InputField.OnChangeEvent onValueChanged = inputField.onValueChanged;
        TMP_InputField.SubmitEvent onEnd = inputField.onEndEdit;
        TMP_InputField.SelectionEvent onSelect = inputField.onSelect;
        TMP_InputField.SelectionEvent onDeselect = inputField.onDeselect;

        UnityEngine.Object.DestroyImmediate(inputField);
        EasyUITMProInput newInputField = go.AddComponent<EasyUITMProInput>();
        newInputField.targetGraphic = graphicTarget;
        newInputField.textViewport = textViewport;
        newInputField.text = text;

        TMP_Text[] textList = newInputField.gameObject.GetComponentsInChildren<TMP_Text>();
        foreach (TMPro.TMP_Text _text in textList)
        {
            if (_text.name == "Text")
                newInputField.textComponent = _text;
        }

        newInputField.onValueChanged = onValueChanged;
        newInputField.onEndEdit = onEnd;
        newInputField.onSelect = onSelect;
        newInputField.onDeselect = onDeselect;

        return newInputField;
    }

    private static EasyUIInputField ReplaceInputField(InputField inputField)
    {
        GameObject go = inputField.gameObject;
        Graphic graphicTarget = inputField.targetGraphic;
        Text textComponent = inputField.textComponent;
        Graphic placeHolder = inputField.placeholder;
        string text = inputField.text;

        InputField.OnChangeEvent onValueChanged = inputField.onValueChanged;
        InputField.SubmitEvent onSubmit = inputField.onSubmit; //added
        InputField.EndEditEvent onEdit = inputField.onEndEdit; //changed

        UnityEngine.Object.DestroyImmediate(inputField);
        EasyUIInputField newInputField = go.AddComponent<EasyUIInputField>();
        newInputField.targetGraphic = graphicTarget;
        newInputField.placeholder = placeHolder;
        newInputField.textComponent = textComponent;
        newInputField.text = text;

        newInputField.onValueChanged = onValueChanged;
        newInputField.onSubmit = onSubmit; //added
        newInputField.onEndEdit = onEdit; //changed

        return newInputField;
    }

    private static TMP_Text ReplaceTextTMP(TMPro.TMP_Text text)
    {
        GameObject go = text.gameObject;
        string _text = text.text;

        UnityEngine.Object.DestroyImmediate(text);
        TMP_Text newText = go.AddComponent<EasyUITMProText>();
        newText.text = _text;

        return newText;
    }

    //[MenuItem("CONTEXT/EasyUIText/Remove Easy UI Styles")]
    //static void RemoveEUIStyle(MenuCommand command)
    //{
    //    if (command.context.GetType() == typeof(EasyUIText))
    //    {
    //        EasyUIText text = (EasyUIText)command.context;
    //        ChangeTextComponent<EasyUIText,Text>(text);
    //    }

    //}
    //private static Tnew ChangeTextComponent<Tcurrent, Tnew>(Tcurrent text) where Tcurrent : Text where Tnew :Text
    //{
    //    GameObject go = text.gameObject;
    //    string _text = text.text;
    //    UnityEngine.Object.DestroyImmediate(text);
    //    Tnew newText = go.AddComponent<Tnew>();
    //    newText.text = _text;

    //    return newText;
    //}

    private static void OpenEditorWindow()
    {
        EditorWindow.GetWindow(typeof(EasyUIStyleEditor));
        EditorWindow.GetWindow(typeof(EasyUIStyleEditor)).minSize = new Vector2(400, 320);
    }
}
