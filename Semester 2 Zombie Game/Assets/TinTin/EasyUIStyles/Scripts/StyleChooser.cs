using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace EasyUIStyle
{
    [ExecuteInEditMode]
    [System.Serializable]
    [DisallowMultipleComponent]
    public class StyleChooser : MonoBehaviour
    {
        public bool hasText;
        public bool hasImage;
        public bool hasButton;
        public bool hasToggle;
        public bool hasSlider;
        public bool hasInput;
        public bool hasDropdown;
        public bool hasTMProText;
        public bool hasTMProInput;
        public bool hasTMProDropdown;

        public Text text;
        public Image image;
        public Button button;
        public Toggle toggle;
        public Slider slider;
        public InputField input;
        public Dropdown dropdown;
        public TextMeshProUGUI tmproText;
        public TMP_InputField tmproInput;
        public TMP_Dropdown tmproDropdown;

        public int imageStyleIndex;
        public int textStyleIndex;
        public int buttonStyleIndex;
        public int toggleStyleIndex;
        public int sliderStyleIndex;
        public int inputStyleIndex;
        public int dropdownStyleIndex;
        public int tmproTextStyleIndex;
        public int tmproInputStyleIndex;
        public int tmproDropdownStyleIndex;

        private EasyUI_TextStyle thisTextStyle;
        private EasyUI_ImageStyle thisImageStyle;
        private EasyUI_ButtonStyle thisButtonStyle;
        private EasyUI_ToggleStyle thisToggleStyle;
        private EasyUI_SliderStyle thisSliderStyle;
        private EasyUI_InputStyle thisInputStyle;
        private EasyUI_DropdownStyle thisDropdownStyle;
        private EasyUI_TMPTextStyle thisTMProTextStyle;
        private EasyUI_TMPInputStyle thisTMProInputStyle;
        private EasyUI_TMPDropDownStyle thisTMProDropDownStyle;

        public EasyUI_Style_Data easyUI_Data;

        private delegate void UpdateStyles();
        private static event UpdateStyles updateStyles;

        void Start()
        {

            Initialize();
        }

        private void Reset()
        {
            Initialize();
        }

        private void OnEnable()
        {
            updateStyles += ForceUpdate;
        }

        private void OnDisable()
        {
            updateStyles -= ForceUpdate;
        }

        //called from editor window when a style is changed
        public static void UpdateStyle()
        {
            updateStyles?.Invoke();
        }

        public void Initialize()
        {
            //only runs in edit mode
            if (Application.isPlaying)
                this.enabled = false;


            //Grab scriptable object
            easyUI_Data = EasyUI_HelperFunctions.LoadData();

            //Use to check type of UI element and later for changing style
            text = this.GetComponent<Text>();
            image = this.GetComponent<Image>();
            button = this.GetComponent<Button>();
            toggle = this.GetComponent<Toggle>();
            slider = this.GetComponent<Slider>();
            input = this.GetComponent<InputField>();
            dropdown = this.GetComponent<Dropdown>();
            tmproText = this.GetComponent<TextMeshProUGUI>();
            tmproInput = this.GetComponent<TMP_InputField>();
            tmproDropdown = this.GetComponent<TMP_Dropdown>();

            if (text != null)
                hasText = true;
            else
                hasText = false;
            if (image != null)
                hasImage = true;
            else
                hasImage = false;
            if (button != null)
                hasButton = true;
            else
                hasButton = false;
            if (slider != null)
                hasSlider = true;
            else
                hasSlider = false;
            if (input != null)
                hasInput = true;
            else
                hasInput = false;
            if (toggle != null)
                hasToggle = true;
            else
                hasToggle = false;
            if (dropdown != null)
                hasDropdown = true;
            else
                hasDropdown = false;
            if (tmproText != null)
                hasTMProText = true;
            else
                hasTMProText = false;
            if (tmproInput != null)
                hasTMProInput = true;
            else
                hasTMProInput = false;
            if (tmproDropdown != null)
                hasTMProDropdown = true;
            else
                hasTMProDropdown = false;
        }

        private void OnValidate()
        {
            UpdateStyle();
        }

        // Update is called once per frame
        //void Update()
        //{
        //    if (easyUI_Data != null)
        //        ForceUpdate();
        //    else
        //        LoadData();
        //}

        public void ForceUpdate()
        {
            //Debug.Log("Doing Force Update");

            if (easyUI_Data == null)
            {
                easyUI_Data = EasyUI_HelperFunctions.LoadData();
                return;
            }

            if (hasText)
                UpdateTextFormat();
            else if (hasImage)
                UpdateImageFormat();
            else if (hasTMProText)
                UpdateTMProTextFormat();

            if (hasButton)
                UpdateButtonFormat();
            else if (hasSlider)
                UpdateSliderFormat();
            else if (hasToggle)
                UpdateToggleFormat();
            else if (hasInput)
                UpdateInputFormat();
            else if (hasDropdown)
                UpdateDropdownFormat();
            else if (hasTMProDropdown)
                UpdateTMProDropdown();
            else if (hasTMProInput)
                UpdateTMProInput();

            if (easyUI_Data == null)
                return;
        }

        public void LoadData()
        {
#if UNITY_EDITOR
            easyUI_Data = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Plugins/EasyUIStyles/Resources/EasyUIData.asset", typeof(EasyUI_Style_Data)) as EasyUI_Style_Data;

            //attempt to find data in other folder
            if (easyUI_Data == null)
            {
                string[] guids = UnityEditor.AssetDatabase.FindAssets("EasyUIData.asset");
                if (guids.Length > 0)
                {
                    string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
                    easyUI_Data = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(EasyUI_Style_Data)) as EasyUI_Style_Data;
                }
            }
#endif
        }

        public void UpdateTextFormat()
        {
            if (easyUI_Data.textList.Count == 0)
                return;

            if (textStyleIndex == 0)
                return;

            if (textStyleIndex > easyUI_Data.textList.Count)
            {
                thisTextStyle = easyUI_Data.textList[0];
                return;
            }

            thisTextStyle = easyUI_Data.textList[textStyleIndex - 1];

            if (thisTextStyle == null)
                return;

            if (thisTextStyle.syncFont)
                text.font = thisTextStyle.font;
            if (thisTextStyle.syncFontSize)
                text.fontSize = thisTextStyle.fontSize;
            if (thisTextStyle.syncFontStyle)
                text.fontStyle = thisTextStyle.fontStyle;
            if (thisTextStyle.syncTextAnchor)
                text.alignment = thisTextStyle.textAnchor;
            if (thisTextStyle.syncVerticalWrapMode)
                text.verticalOverflow = thisTextStyle.vertWrap;
            if (thisTextStyle.syncHorizontalWrapMode)
                text.horizontalOverflow = thisTextStyle.horzWrap;
            if (thisTextStyle.syncBestFit)
            {
                text.resizeTextForBestFit = thisTextStyle.resizeForBestFit;
                text.resizeTextMinSize = thisTextStyle.minSize;
                text.resizeTextMaxSize = thisTextStyle.maxSize;
            }
            if (thisTextStyle.syncTextColor)
                text.color = thisTextStyle.textColor;
            if (thisTextStyle.syncTexMat)
                text.material = thisTextStyle.textMat;
            if (thisTextStyle.syncRaycastTarget)
                text.raycastTarget = thisTextStyle.isRaycastTarget;
        }

        public void UpdateImageFormat()
        {
            if (easyUI_Data.imageList.Count == 0)
                return;

            if (imageStyleIndex == 0)
                return;

            if (imageStyleIndex > easyUI_Data.imageList.Count)
            {
                thisImageStyle = easyUI_Data.imageList[0];
                return;
            }
            thisImageStyle = easyUI_Data.imageList[imageStyleIndex - 1];

            if (thisImageStyle == null)
                return;

            if (thisImageStyle.syncColor)
                image.color = thisImageStyle.color;
            if (thisImageStyle.syncMaterial)
                image.material = thisImageStyle.material;
            if (thisImageStyle.syncSprite)
                image.sprite = thisImageStyle.sprite;
            if (thisImageStyle.syncRaycastTarget)
                image.raycastTarget = thisImageStyle.raycastTarget;
            if (thisImageStyle.syncImageType)
                image.type = thisImageStyle.imageType;

            if (thisImageStyle.imageType == UnityEngine.UI.Image.Type.Filled)
            {
                if (thisImageStyle.syncFillMethod)
                    image.fillMethod = thisImageStyle.fillMethod;

                switch (thisImageStyle.fillMethod)
                {
                    case UnityEngine.UI.Image.FillMethod.Horizontal:
                        if (thisImageStyle.syncOriginHorzontal)
                            image.fillOrigin = (int)thisImageStyle.originHorz;
                        break;
                    case UnityEngine.UI.Image.FillMethod.Vertical:
                        if (thisImageStyle.syncOriginVertical)
                            image.fillOrigin = (int)thisImageStyle.originVert;
                        break;
                    case UnityEngine.UI.Image.FillMethod.Radial90:
                        if (thisImageStyle.syncOrigin90)
                            image.fillOrigin = (int)thisImageStyle.origin90;
                        break;
                    case UnityEngine.UI.Image.FillMethod.Radial180:
                        if (thisImageStyle.syncOrigin180)
                            image.fillOrigin = (int)thisImageStyle.origin180;
                        break;
                    case UnityEngine.UI.Image.FillMethod.Radial360:
                        if (thisImageStyle.syncOrigin360)
                            image.fillOrigin = (int)thisImageStyle.origin360;
                        break;
                }
                if (thisImageStyle.syncClockwise)
                    image.fillClockwise = thisImageStyle.clockwise;
                if (thisImageStyle.syncFillAmount)
                    image.fillAmount = thisImageStyle.fillAmount;
            }
            else if (thisImageStyle.syncFillCenter)
                image.fillCenter = thisImageStyle.fillCenter;

            if (thisImageStyle.syncPreserveAspect)
                image.preserveAspect = thisImageStyle.preserveAspect;
        }

        void UpdateButtonFormat()
        {
            if (easyUI_Data.buttonList.Count == 0)
                return;

            if (buttonStyleIndex == 0)
                return;

            if (buttonStyleIndex > easyUI_Data.buttonList.Count)
            {
                thisButtonStyle = easyUI_Data.buttonList[0];
                return;
            }
            thisButtonStyle = easyUI_Data.buttonList[buttonStyleIndex - 1];

            if (thisButtonStyle == null)
                return;

            UpdateButtonCore(thisButtonStyle, button);
        }

        void UpdateToggleFormat()
        {
            if (easyUI_Data.toggleList.Count == 0)
                return;

            if (toggleStyleIndex == 0)
                return;

            if (toggleStyleIndex > easyUI_Data.toggleList.Count)
            {
                thisToggleStyle = easyUI_Data.toggleList[0];
                return;
            }
            thisToggleStyle = easyUI_Data.toggleList[toggleStyleIndex - 1];

            if (thisToggleStyle == null)
                return;

            UpdateButtonCore(thisToggleStyle, toggle);

            if (thisToggleStyle.syncIsOn)
                toggle.isOn = thisToggleStyle.isOn;
            if (thisToggleStyle.syncToggleTransition)
                toggle.toggleTransition = thisToggleStyle.toggleTransition;
        }

        void UpdateSliderFormat()
        {
            if (easyUI_Data.sliderList.Count == 0)
                return;

            if (sliderStyleIndex == 0)
                return;

            if (sliderStyleIndex > easyUI_Data.sliderList.Count)
            {
                thisSliderStyle = easyUI_Data.sliderList[0];
                return;
            }
            thisSliderStyle = easyUI_Data.sliderList[sliderStyleIndex - 1];

            if (thisSliderStyle == null)
                return;

            UpdateButtonCore(thisSliderStyle, slider);

            if (thisSliderStyle.syncDirection)
                slider.direction = thisSliderStyle.direction;
            if (thisSliderStyle.syncMinValue)
                slider.minValue = thisSliderStyle.minValue;
            if (thisSliderStyle.syncMinValue)
                slider.maxValue = thisSliderStyle.maxValue;
            if (thisSliderStyle.syncWholeNumbers)
                slider.wholeNumbers = thisSliderStyle.wholeNumbers;
            if (thisSliderStyle.syncValue)
                slider.value = thisSliderStyle.value;
        }

        void UpdateInputFormat()
        {
            if (easyUI_Data.inputList.Count == 0)
                return;

            if (inputStyleIndex == 0)
                return;

            if (inputStyleIndex > easyUI_Data.inputList.Count)
            {
                thisInputStyle = easyUI_Data.inputList[0];
                return;
            }
            thisInputStyle = easyUI_Data.inputList[inputStyleIndex - 1];

            if (thisInputStyle == null)
                return;

            UpdateButtonCore(thisInputStyle, input);

            if (thisInputStyle.syncText)
                input.text = thisInputStyle.text;
            if (this.thisInputStyle.syncCharacterLimit)
                input.characterLimit = thisInputStyle.characterLimit;
            if (thisInputStyle.syncContentType)
                input.contentType = thisInputStyle.contentType;
            if (thisInputStyle.syncLineType)
                input.lineType = thisInputStyle.lineType;

            if (thisInputStyle.syncInputType)
                input.inputType = thisInputStyle.inputType;
            if (thisInputStyle.syncKeyboardType)
                input.keyboardType = thisInputStyle.keyboardType;
            if (thisInputStyle.syncCharacterValidation)
                input.characterValidation = thisInputStyle.characterValidation;


            if (thisInputStyle.syncCaretBlinkRate)
                input.caretBlinkRate = thisInputStyle.caretBlinkRate;
            if (thisInputStyle.syncCaretWidth)
                input.caretWidth = thisInputStyle.caretWidth;
            if (thisInputStyle.syncCustomCaretColor)
                input.customCaretColor = thisInputStyle.customCaretColor;
            if (thisInputStyle.syncCaretColor)
                input.caretColor = thisInputStyle.caretColor;
            if (thisInputStyle.syncSelectionColor)
                input.selectionColor = thisInputStyle.selectionColor;
            if (thisInputStyle.syncHideMobileInput)
                input.shouldHideMobileInput = thisInputStyle.hideMobileInput;
            if (thisInputStyle.syncReadOnly)
                input.readOnly = thisInputStyle.readOnly;
        }

        void UpdateTMProTextFormat()
        {
            if (easyUI_Data.tmproTextList.Count == 0)
                return;

            if (tmproTextStyleIndex == 0)
                return;

            if (tmproTextStyleIndex > easyUI_Data.tmproTextList.Count)
            {
                thisTMProTextStyle = easyUI_Data.tmproTextList[0];
                return;
            }
            thisTMProTextStyle = easyUI_Data.tmproTextList[tmproTextStyleIndex - 1];

            if (thisTMProTextStyle == null)
                return;


            if (thisTMProTextStyle.syncIsRightToLeft)
                tmproText.isRightToLeftText = thisTMProTextStyle.isRightToLeft;

            //font asset
            if (thisTMProTextStyle.syncFontAsset)
            {
                tmproText.font = thisTMProTextStyle.fontAsset;
                tmproText.UpdateFontAsset();
            }

            if (thisTMProTextStyle.syncMaterialPreset && thisTMProTextStyle.fontAsset != null && thisTMProTextStyle.materialPreset != null)
                tmproText.fontSharedMaterial = thisTMProTextStyle.materialPreset;

            //tmproText.fontSharedMaterial = thisTMProTextStyle.GetMaterialPreset(thisTMProTextStyle.materialPresetIndex);

            //tmproText.fontSharedMaterial = thisTMProTextStyle.materialPreset;

            //font style
            if (thisTMProTextStyle.syncFontStyles)
                tmproText.fontStyle = thisTMProTextStyle.fontStyles;

            //font size
            if (thisTMProTextStyle.syncFontSize)
                tmproText.fontSize = thisTMProTextStyle.fontSize;

            //auto sizing
            if (thisTMProTextStyle.syncAutoSize)
                tmproText.enableAutoSizing = thisTMProTextStyle.enableAutoSize;
            if (thisTMProTextStyle.syncFontSizeMax && thisTMProTextStyle.syncAutoSize)
                tmproText.fontSizeMax = thisTMProTextStyle.fontSizeMax;
            if (thisTMProTextStyle.syncFontSizeMin && thisTMProTextStyle.syncAutoSize)
                tmproText.fontSizeMin = thisTMProTextStyle.fontSizeMin;
            if (thisTMProTextStyle.syncCharWidthMaxAdjust && thisTMProTextStyle.syncAutoSize)
                tmproText.characterWidthAdjustment = thisTMProTextStyle.charWidthMaxAdjust;
            if (thisTMProTextStyle.syncLineSpacingMax && thisTMProTextStyle.syncAutoSize)
                tmproText.lineSpacingAdjustment = thisTMProTextStyle.lineSpacingMax;

            //color
            if (thisTMProTextStyle.syncColor)
                tmproText.color = thisTMProTextStyle.color;

            if (thisTMProTextStyle.syncColorGradient && this.thisTMProTextStyle.enableVertextGradient)
            {
                VertexGradient vg = new VertexGradient();

                //tmproText.colorGradient.colorMode = thisTMProTextStyle.colorMode;
                vg.topLeft = thisTMProTextStyle.topLeft;
                vg.topRight = thisTMProTextStyle.topRight;
                vg.bottomLeft = thisTMProTextStyle.bottomLeft;
                vg.bottomRight = thisTMProTextStyle.bottomRight;
                tmproText.colorGradient = vg;
            }

            //spacing
            if (thisTMProTextStyle.syncCharacterSpacing)
                tmproText.characterSpacing = thisTMProTextStyle.characterSpacing;
            if (thisTMProTextStyle.syncWordSpacing)
                tmproText.wordSpacing = thisTMProTextStyle.wordSpacing;
            if (thisTMProTextStyle.syncLineSpacing)
                tmproText.lineSpacing = thisTMProTextStyle.lineSpacing;
            if (thisTMProTextStyle.syncParagraphSpacing)
                tmproText.paragraphSpacing = thisTMProTextStyle.paragraphSpacing;

            //word wrapping
            if (thisTMProTextStyle.syncWordWrapping)
                tmproText.enableWordWrapping = thisTMProTextStyle.enableWordWrapping;

            //overflow
            if (thisTMProTextStyle.syncOverflowMode)
                tmproText.overflowMode = thisTMProTextStyle.overflowMode;

            //horiztonal and vertical mapping
            if (thisTMProTextStyle.syncHoriztonalMapping)
                tmproText.horizontalMapping = thisTMProTextStyle.horizontalMapping;
            if (thisTMProTextStyle.syncVerticalMapping)
                tmproText.verticalMapping = thisTMProTextStyle.verticalMapping;
            if (thisTMProTextStyle.syncLineOffset)
                tmproText.mappingUvLineOffset = thisTMProTextStyle.lineOffset;

            //alignment
            if (thisTMProTextStyle.syncTextAligment)
                tmproText.alignment = thisTMProTextStyle.textAlignment;

            //override tags
            if (thisTMProTextStyle.syncOverrideTags)
                tmproText.overrideColorTags = thisTMProTextStyle.overrideTags;

            //extra settings
            if (thisTMProTextStyle.syncMargins)
                tmproText.margin = thisTMProTextStyle.margins;
            if (thisTMProTextStyle.syncGeometrySorting)
                tmproText.geometrySortingOrder = thisTMProTextStyle.geometrySorting;
            if (thisTMProTextStyle.syncRichText)
                tmproText.richText = thisTMProTextStyle.richText;
            if (thisTMProTextStyle.syncRaycastTarget)
                tmproText.raycastTarget = thisTMProTextStyle.raycastTarget;
            if (thisTMProTextStyle.syncParseEscapeCharacters)
                tmproText.parseCtrlCharacters = thisTMProTextStyle.parseEscapeCharacters;

            if (thisTMProTextStyle.syncVisibleDescender)
            {
                tmproText.useMaxVisibleDescender = thisTMProTextStyle.visibleDescender;

            }

            if (thisTMProTextStyle.syncSpritieAsset)
                tmproText.spriteAsset = thisTMProTextStyle.spriteAsset;
            if (thisTMProTextStyle.syncKerning)
                tmproText.enableKerning = thisTMProTextStyle.kerning;
            if (thisTMProTextStyle.syncExtraPadding)
                tmproText.extraPadding = thisTMProTextStyle.extraPadding;

            tmproText.SetAllDirty();
        }

        void UpdateTMProInput()
        {
            if (easyUI_Data.tmproInputList.Count == 0)
                return;

            if (tmproInputStyleIndex == 0)
                return;
            if (tmproInputStyleIndex > easyUI_Data.tmproInputList.Count)
            {
                thisTMProInputStyle = easyUI_Data.tmproInputList[0];
                return;
            }
            thisTMProInputStyle = easyUI_Data.tmproInputList[tmproInputStyleIndex - 1];

            if (thisTMProInputStyle == null)
                return;

            UpdateButtonCore(thisTMProInputStyle, tmproInput);

            if (thisTMProInputStyle.syncText)
                tmproInput.text = thisTMProInputStyle.text;
            if (thisTMProInputStyle.syncCharacterLimit)
                tmproInput.characterLimit = thisTMProInputStyle.characterLimit;
            //enums are defined the same... So why do extra work? A bit hacky I suppose.
            if (thisTMProInputStyle.syncContentType)
                tmproInput.contentType = (TMP_InputField.ContentType)thisTMProInputStyle.contentType;
            if (thisTMProInputStyle.syncLineType)
                tmproInput.lineType = (TMP_InputField.LineType)thisTMProInputStyle.lineType;
            if (thisTMProInputStyle.syncLineLimit)
                tmproInput.lineLimit = thisTMProInputStyle.lineLimit;
            if (thisTMProInputStyle.syncInputType)
                tmproInput.inputType = (TMP_InputField.InputType)thisTMProInputStyle.inputType;
            if (thisTMProInputStyle.syncKeyboardType)
                tmproInput.keyboardType = thisTMProInputStyle.keyboardType;
            if (thisTMProInputStyle.syncCharacterValidation)
                tmproInput.characterValidation = (TMP_InputField.CharacterValidation)thisTMProInputStyle.tmp_characterValidation;
            //if (thisTMProInputStyle.syncRegexValue)
            //    tmproInput.regexValue = thisTMProInputStyle.regexValue;

            if (thisTMProInputStyle.syncCaretBlinkRate)
                tmproInput.caretBlinkRate = thisTMProInputStyle.caretBlinkRate;
            if (thisTMProInputStyle.syncCaretWidth)
                tmproInput.caretWidth = thisTMProInputStyle.caretWidth;
            if (thisTMProInputStyle.syncCustomCaretColor)
                tmproInput.customCaretColor = thisTMProInputStyle.customCaretColor;
            if (thisTMProInputStyle.syncCaretColor)
                tmproInput.caretColor = thisTMProInputStyle.caretColor;
            if (thisTMProInputStyle.syncSelectionColor)
                tmproInput.selectionColor = thisTMProInputStyle.selectionColor;
            if (thisTMProInputStyle.syncHideMobileInput)
                tmproInput.shouldHideMobileInput = thisTMProInputStyle.hideMobileInput;
            if (thisTMProInputStyle.syncReadOnly)
                tmproInput.readOnly = thisTMProInputStyle.readOnly;

            //tmp specific bits
            if (thisTMProInputStyle.syncFontAsset && thisTMProInputStyle.fontAsset != null)
            {
                tmproInput.fontAsset = thisTMProInputStyle.fontAsset;
                //tmproInput.UpdateFontAsset();
            }

            if (thisTMProInputStyle.syncPointSize)
                tmproInput.pointSize = thisTMProInputStyle.pointSize;
            if (thisTMProInputStyle.syncOnFucusSelectAll)
                tmproInput.onFocusSelectAll = thisTMProInputStyle.onFocusSelectAll;
            if (thisTMProInputStyle.syncResetOnDeactivation)
                tmproInput.resetOnDeActivation = thisTMProInputStyle.resetOnDeactivation;
            if (thisTMProInputStyle.syncRetoreOnEscapeKey)
                tmproInput.restoreOriginalTextOnEscape = thisTMProInputStyle.restoreOnEscapeKey;
            if (thisTMProInputStyle.syncHideSoftKeyboard)
                tmproInput.shouldHideSoftKeyboard = thisTMProInputStyle.hideSoftKeyboard;
            if (thisTMProInputStyle.richText)
                tmproInput.richText = thisTMProInputStyle.richText;
            if (thisTMProInputStyle.syncAllowRichTextEditing)
                tmproInput.isRichTextEditingAllowed = thisTMProInputStyle.allowRichTextEditing;
        }

        void UpdateTMProDropdown()
        {
            if (easyUI_Data.tmproDropdownList.Count == 0)
                return;

            if (tmproDropdownStyleIndex == 0)
                return;
            if (tmproDropdownStyleIndex > easyUI_Data.tmproDropdownList.Count)
            {
                thisTMProDropDownStyle = easyUI_Data.tmproDropdownList[0];
                return;
            }
            thisTMProDropDownStyle = easyUI_Data.tmproDropdownList[tmproDropdownStyleIndex - 1];

            if (thisTMProDropDownStyle == null)
                return;

            UpdateButtonCore(thisTMProDropDownStyle, tmproDropdown);

            if (thisTMProDropDownStyle.syncValue)
                tmproDropdown.value = thisTMProDropDownStyle.value;
            if (thisTMProDropDownStyle.syncOptions)
                tmproDropdown.options = thisTMProDropDownStyle.tmp_optionList;
        }

        void UpdateDropdownFormat()
        {
            if (easyUI_Data.dropdownList.Count == 0)
                return;

            if (dropdownStyleIndex == 0)
                return;

            if (dropdownStyleIndex > easyUI_Data.dropdownList.Count)
            {
                thisDropdownStyle = easyUI_Data.dropdownList[0];
                return;
            }
            thisDropdownStyle = easyUI_Data.dropdownList[dropdownStyleIndex - 1];

            if (thisDropdownStyle == null)
                return;

            UpdateButtonCore(thisDropdownStyle, dropdown);

            if (thisDropdownStyle.syncValue)
                dropdown.value = thisDropdownStyle.value;
            if (thisDropdownStyle.syncOptions)
                dropdown.options = thisDropdownStyle.optionList;
        }

        void UpdateButtonCore(EasyUI_ButtonStyle style, Selectable uiObject)
        {

            if (style.syncIsInteractable)
                uiObject.interactable = style.isInteractable;
            if (style.syncTransition)
                uiObject.transition = style.transition;

            ColorBlock cb = new ColorBlock();
            cb = uiObject.colors;
            if (style.syncNormalColor)
                cb.normalColor = style.colors.normalColor;
            if (style.syncHighlightedColor)
                cb.highlightedColor = style.colors.highlightedColor;
            if (style.syncPressedColor)
                cb.pressedColor = style.colors.pressedColor;
            if (style.syncDisabledColor)
                cb.disabledColor = style.colors.disabledColor;
            if (style.syncColorMultiplier)
                cb.colorMultiplier = style.colorMultiplier;
            if (style.syncFadeDuration)
                cb.fadeDuration = style.fadeDuration;
            uiObject.colors = cb;
            if (style.syncNavigation)
                uiObject.navigation = style.navigation;

            SpriteState ss = new SpriteState();
            ss = uiObject.spriteState;

            if (style.syncHighlightedSprite)
                ss.highlightedSprite = style.highlightedSprite;
            if (style.syncPressedSprite)
                ss.pressedSprite = style.pressedSprite;
            if (style.syncDisabledSprite)
                ss.disabledSprite = style.disabledSprite;

            uiObject.spriteState = ss;

            AnimationTriggers at = new AnimationTriggers();
            at = uiObject.animationTriggers;

            if (style.syncNormalTrigger)
                at.normalTrigger = style.normalTrigger;
            if (style.syncHighlightedTrigger)
                at.highlightedTrigger = style.hightlightedTrigger;
            if (style.syncPressedTrigger)
                at.pressedTrigger = style.pressedTrigger;
            if (style.syncDisabledTrigger)
                at.disabledTrigger = style.disabledTrigger;

            uiObject.animationTriggers = at;

        }
    }
}
