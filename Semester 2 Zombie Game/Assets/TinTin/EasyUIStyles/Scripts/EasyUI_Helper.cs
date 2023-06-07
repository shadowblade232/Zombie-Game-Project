using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;


namespace EasyUIStyle
{
    [System.Serializable]
    public class EasyUIStyle_Base
    {
        public int displayOrder = 0;
        public int previousOrder = 0;

        public string styleName = "New Style";
        public bool maximize = true;

        public virtual void ToggleAll(bool _isOn)
        { }

        public virtual EasyUIStyle_Base MakeCopy()
        {
            return null;
        }
    }

    [System.Serializable]
    public class EasyUI_ImageStyle : EasyUIStyle_Base
    {
        public Color color = new Color(0f, 0f, 0f, 1f);
        public Sprite sprite;
        public Material material;
        public bool raycastTarget = true;
        public bool preserveAspect = false;
        public Image.Type imageType;
        public bool fillCenter = true;

        //Filled Type
        public Image.FillMethod fillMethod = Image.FillMethod.Radial360;
        public Image.Origin360 origin360 = Image.Origin360.Bottom;
        public Image.Origin180 origin180 = Image.Origin180.Bottom;
        public Image.Origin90 origin90 = Image.Origin90.BottomLeft;
        public Image.OriginHorizontal originHorz = Image.OriginHorizontal.Left;
        public Image.OriginVertical originVert = Image.OriginVertical.Bottom;
        public float fillAmount = 1;
        public bool clockwise = true;

        //track syncing
        public bool syncColor = true;
        public bool syncSprite = true;
        public bool syncMaterial = true;
        public bool syncRaycastTarget = true;
        public bool syncPreserveAspect = true;
        public bool syncImageType = true;
        public bool syncFillCenter = true;
        public bool syncFillMethod = true;
        public bool syncOrigin360 = true;
        public bool syncOrigin180 = true;
        public bool syncOrigin90 = true;
        public bool syncOriginHorzontal = true;
        public bool syncOriginVertical = true;
        public bool syncFillAmount = true;
        public bool syncClockwise = true;

        public override void ToggleAll(bool _isOn)
        {
            syncColor = _isOn;
            syncSprite = _isOn;
            syncMaterial = _isOn;
            syncRaycastTarget = _isOn;
            syncPreserveAspect = _isOn;
            syncImageType = _isOn;
            syncFillCenter = _isOn;
            syncFillMethod = _isOn;
            syncOrigin360 = _isOn;
            syncOrigin180 = _isOn;
            syncOrigin90 = _isOn;
            syncOriginHorzontal = _isOn;
            syncOriginVertical = _isOn;
            syncFillAmount = _isOn;
            syncClockwise = _isOn;
        }

        public override EasyUIStyle_Base MakeCopy()
        {
            EasyUI_ImageStyle _style = new EasyUI_ImageStyle();
            _style.styleName = "New Image Style";

            _style.color = this.color;
            _style.sprite = this.sprite;
            _style.material = this.material;
            _style.raycastTarget = this.raycastTarget;
            _style.imageType = this.imageType;
            _style.fillCenter = this.fillCenter;
            _style.fillMethod = this.fillMethod;
            _style.origin360 = this.origin360;
            _style.origin180 = this.origin180;
            _style.origin90 = this.origin90;
            _style.originHorz = this.originHorz;
            _style.originVert = this.originVert;
            _style.fillAmount = this.fillAmount;
            _style.clockwise = this.clockwise;

            _style.syncColor = this.syncColor;
            _style.syncSprite = this.syncSprite;
            _style.syncMaterial = this.syncMaterial;
            _style.syncRaycastTarget = this.syncRaycastTarget;
            _style.syncPreserveAspect = this.syncPreserveAspect;
            _style.syncImageType = this.syncImageType;
            _style.syncFillCenter = this.syncFillCenter;
            _style.syncFillMethod = this.syncFillMethod;
            _style.syncOrigin360 = this.syncOrigin360;
            _style.syncOrigin180 = this.syncOrigin180;
            _style.syncOrigin90 = this.syncOrigin90;
            _style.syncOriginHorzontal = this.syncOriginHorzontal;
            _style.syncOriginVertical = this.syncOriginVertical;
            _style.syncFillAmount = this.syncFillAmount;
            _style.syncClockwise = this.syncClockwise;

            return _style;
        }

        public EasyUI_ImageStyle MakeCopy(Image image)
        {
            EasyUI_ImageStyle _style = new EasyUI_ImageStyle();
            _style.styleName = image.gameObject.name + " style";
            _style.color = image.color;
            _style.sprite = image.sprite;
            _style.material = image.material;
            _style.raycastTarget = image.raycastTarget;
            _style.imageType = image.type;
            _style.fillCenter = image.fillCenter;
            _style.fillMethod = image.fillMethod;
            _style.origin360 = (Image.Origin360)image.fillOrigin;
            _style.origin180 = (Image.Origin180)image.fillOrigin;
            _style.origin90 = (Image.Origin90)image.fillOrigin;
            _style.originHorz = (Image.OriginHorizontal)image.fillOrigin;
            _style.originVert = (Image.OriginVertical)image.fillOrigin;
            _style.fillAmount = image.fillAmount;
            _style.clockwise = image.fillClockwise;

            return _style;
        }
    }

    [System.Serializable]
    public class EasyUI_TextStyle : EasyUIStyle_Base
    {
        public Font font;
        public FontStyle fontStyle = FontStyle.Normal;
        public int fontSize = 14;
        public float lineSpacing = 1;
        public bool richText = false;
        public Color textColor = Color.black;
        public Material textMat;
        public bool alignByGeometry = false;
        public TextAnchor textAnchor = TextAnchor.UpperLeft;
        public HorizontalWrapMode horzWrap = HorizontalWrapMode.Overflow;
        public VerticalWrapMode vertWrap = VerticalWrapMode.Truncate;
        public bool resizeForBestFit;
        public int minSize = 10;
        public int maxSize = 40;
        public bool isRaycastTarget = true;

        public bool syncFont = true;
        public bool syncFontStyle = true;
        public bool syncFontSize = true;
        public bool syncLineSpacing= true;
        public bool syncRichText= true;
        public bool syncTextColor = true;
        public bool syncTexMat = true;
        public bool syncTextAnchor = true;
        public bool syncAlignByGeometry = true;
        public bool syncHorizontalWrapMode = true;
        public bool syncVerticalWrapMode = true;
        public bool syncBestFit = true;
        public bool syncRaycastTarget = true;

        public override void ToggleAll(bool _isOn)
        {
            syncFont = _isOn;
            syncFontStyle = _isOn;
            syncFontSize = _isOn;
            syncTextColor = _isOn;
            syncTexMat = _isOn;
            syncTextAnchor = _isOn;
            syncHorizontalWrapMode = _isOn;
            syncVerticalWrapMode = _isOn;
            syncBestFit = _isOn;
            syncRaycastTarget = _isOn;
        }

        public override EasyUIStyle_Base MakeCopy()
        {
            EasyUI_TextStyle _style = new EasyUI_TextStyle();
            _style.styleName = "New Text Style";

            _style.font = this.font;
            _style.fontStyle = this.fontStyle;
            _style.fontSize = this.fontSize;
            _style.lineSpacing = this.lineSpacing;
            _style.richText = this.richText;
            _style.textColor = this.textColor;
            _style.textMat = this.textMat;
            _style.alignByGeometry = this.alignByGeometry;
            _style.textAnchor = this.textAnchor;
            _style.horzWrap = this.horzWrap;
            _style.vertWrap = this.vertWrap;
            _style.resizeForBestFit = this.resizeForBestFit;
            _style.minSize = this.minSize;
            _style.maxSize = this.maxSize;
            _style.isRaycastTarget = this.isRaycastTarget;

            _style.syncFont = this.syncFont;
            _style.syncFontStyle = this.syncFontStyle;
            _style.syncFontSize = this.syncFontSize;
            _style.syncTextColor = this.syncTextColor;
            _style.syncTexMat = this.syncTexMat;
            _style.syncTextAnchor = this.syncTextAnchor;
            _style.syncHorizontalWrapMode = this.syncHorizontalWrapMode;
            _style.syncVerticalWrapMode = this.syncVerticalWrapMode;
            _style.syncBestFit = this.syncBestFit;
            _style.syncRaycastTarget = this.syncRaycastTarget;

            return _style;
        }

        public EasyUI_TextStyle MakeCopy(Text text)
        {
            EasyUI_TextStyle _style = new EasyUI_TextStyle();
            _style.styleName = text.name + " style";
            _style.font = text.font;
            _style.fontStyle = text.fontStyle;
            _style.fontSize = text.fontSize;
            _style.textColor = text.color;
            _style.textMat = text.material;
            _style.textAnchor = text.alignment;
            _style.horzWrap = text.horizontalOverflow;
            _style.vertWrap = text.verticalOverflow;
            _style.resizeForBestFit = text.resizeTextForBestFit;
            _style.minSize = text.resizeTextMinSize;
            _style.maxSize = text.resizeTextMaxSize;
            _style.isRaycastTarget = text.raycastTarget;

            //_style.syncFont = text.syncFont;
            //_style.syncFontStyle = text.syncFontStyle;
            //_style.syncFontSize = text.syncFontSize;
            //_style.syncTextColor = text.syncTextColor;
            //_style.syncTexMat = text.syncTexMat;
            //_style.syncTextAnchor = text.syncTextAnchor;
            //_style.syncHorizontalWrapMode = text.syncHorizontalWrapMode;
            //_style.syncVerticalWrapMode = text.syncVerticalWrapMode;
            //_style.syncBestFit = text.syncBestFit;
            //_style.syncRaycastTarget = text.syncRaycastTarget;

            return _style;
        }
    }

    [System.Serializable]
    public class EasyUI_ButtonStyle : EasyUIStyle_Base
    {
        public bool isInteractable = true;
        public Selectable.Transition transition = Selectable.Transition.ColorTint;
        public Graphic graphicTarget;
        public ColorBlock colors = ColorBlock.defaultColorBlock;
        public float colorMultiplier = 1f;
        public float fadeDuration = 0.1f;
        public Navigation navigation = new Navigation();

        public bool syncIsInteractable = true;
        public bool syncTransition = true;
        public bool syncNormalColor = true;
        public bool syncHighlightedColor = true;
        public bool syncPressedColor = true;
        public bool syncSelectedColor = true; //add this line
        public bool syncDisabledColor = true;
        public bool syncColorMultiplier = true;
        public bool syncFadeDuration = true;
        public bool syncNavigation = true;

        //Sprite Swap Transition
        public Sprite targetGraphic;
        public Sprite highlightedSprite;
        public Sprite pressedSprite;
        public Sprite selectedSprite;
        public Sprite disabledSprite;

        public bool syncTargetGraphic = true;
        public bool syncHighlightedSprite = true;
        public bool syncPressedSprite = true;
        public bool syncSelectedSprite = true;
        public bool syncDisabledSprite = true;

        //Animation Transition
        public string normalTrigger = "Normal";
        public string hightlightedTrigger = "Highlighted";
        public string pressedTrigger = "Pressed";
        public string disabledTrigger = "Disabled";

        public bool syncNormalTrigger = true;
        public bool syncHighlightedTrigger = true;
        public bool syncPressedTrigger = true;
        public bool syncDisabledTrigger = true;

        public EasyUI_ButtonStyle()
        {
            navigation.mode = Navigation.Mode.Automatic;
        }

        public override void ToggleAll(bool _isOn)
        {
            syncIsInteractable = _isOn;
            syncTransition = _isOn;
            syncNormalColor = _isOn;
            syncHighlightedColor = _isOn;
            syncPressedColor = _isOn;
            syncDisabledColor = _isOn;
            syncColorMultiplier = _isOn;
            syncFadeDuration = _isOn;
            syncNavigation = _isOn;
        }

        public override EasyUIStyle_Base MakeCopy()
        {
            EasyUI_ButtonStyle _style = new EasyUI_ButtonStyle();
            _style.styleName = "New Button Style";

            _style.isInteractable = this.isInteractable;
            _style.transition = this.transition;
            _style.colors = this.colors;
            _style.navigation = this.navigation;

            _style.syncIsInteractable = this.syncIsInteractable;
            _style.syncTransition = this.syncTransition;
            _style.syncNormalColor = this.syncNormalColor;
            _style.syncHighlightedColor = this.syncHighlightedColor;
            _style.syncPressedColor = this.syncPressedColor;
            _style.syncDisabledColor = this.syncDisabledColor;
            _style.syncColorMultiplier = this.syncColorMultiplier;
            _style.syncFadeDuration = this.syncFadeDuration;
            _style.syncNavigation = this.syncNavigation;

            return _style;
        }

        public EasyUI_ButtonStyle MakeCopy(Button button)
        {
            EasyUI_ButtonStyle _style = new EasyUI_ButtonStyle();
            _style.styleName = button.name + " style";

            _style.isInteractable = button.interactable;
            _style.transition = button.transition;
            _style.colors = button.colors;
            _style.navigation = button.navigation;
            return _style;
        }
    }

    [System.Serializable]
    public class EasyUI_ToggleStyle : EasyUI_ButtonStyle
    {
        public bool isOn;
        public Toggle.ToggleTransition toggleTransition = Toggle.ToggleTransition.Fade;

        public bool syncIsOn = true;
        public bool syncToggleTransition = true;

        public override void ToggleAll(bool _isOn)
        {
            base.ToggleAll(_isOn);
            syncIsOn = _isOn;
            syncToggleTransition = _isOn;
        }

        public override EasyUIStyle_Base MakeCopy()
        {
            EasyUI_ToggleStyle _style = new EasyUI_ToggleStyle();
            _style.styleName = "New Toggle Style";

            _style.isInteractable = this.isInteractable;
            _style.transition = this.transition;
            _style.colors = this.colors;
            _style.navigation = this.navigation;

            _style.isOn = this.isOn;
            _style.toggleTransition = this.toggleTransition;

            _style.syncIsInteractable = this.syncIsInteractable;
            _style.syncTransition = this.syncTransition;
            _style.syncNormalColor = this.syncNormalColor;
            _style.syncHighlightedColor = this.syncHighlightedColor;
            _style.syncPressedColor = this.syncPressedColor;
            _style.syncDisabledColor = this.syncDisabledColor;
            _style.syncColorMultiplier = this.syncColorMultiplier;
            _style.syncFadeDuration = this.syncFadeDuration;
            _style.syncNavigation = this.syncNavigation;

            _style.syncIsOn = this.syncIsOn;
            _style.syncToggleTransition = this.syncToggleTransition;

            return _style;
        }

        public EasyUI_ToggleStyle MakeCopy(Toggle toggle)
        {
            EasyUI_ToggleStyle _style = new EasyUI_ToggleStyle();
            _style.styleName = toggle.name + " style";

            _style.isInteractable = toggle.interactable;
            _style.transition = toggle.transition;
            _style.colors = toggle.colors;
            _style.navigation = toggle.navigation;

            _style.isOn = toggle.isOn;
            _style.toggleTransition = toggle.toggleTransition;

            return _style;
        }
    }

    [System.Serializable]
    public class EasyUI_SliderStyle : EasyUI_ButtonStyle
    {
        public Slider.Direction direction = Slider.Direction.LeftToRight;
        public float minValue = 0f;
        public float maxValue = 1f;
        public bool wholeNumbers = false;
        public float value = 0.5f;

        public bool syncDirection = true;
        public bool syncMinValue = true;
        public bool syncMaxValue = true;
        public bool syncWholeNumbers = true;
        public bool syncValue = false;

        public override void ToggleAll(bool _isOn)
        {
            base.ToggleAll(_isOn);
            syncDirection = _isOn;
            syncMinValue = _isOn;
            syncMaxValue = _isOn;
            syncWholeNumbers = _isOn;
            syncValue = _isOn;
        }

        public override EasyUIStyle_Base MakeCopy()
        {
            EasyUI_SliderStyle _style = new EasyUI_SliderStyle();
            _style.styleName = "New Slider Style";

            _style.isInteractable = this.isInteractable;
            _style.transition = this.transition;
            _style.colors = this.colors;
            _style.navigation = this.navigation;

            _style.direction = this.direction;
            _style.minValue = this.minValue;
            _style.maxValue = this.maxValue;
            _style.wholeNumbers = this.wholeNumbers;
            _style.value = this.value;

            _style.syncIsInteractable = this.syncIsInteractable;
            _style.syncTransition = this.syncTransition;
            _style.syncNormalColor = this.syncNormalColor;
            _style.syncHighlightedColor = this.syncHighlightedColor;
            _style.syncPressedColor = this.syncPressedColor;
            _style.syncDisabledColor = this.syncDisabledColor;
            _style.syncColorMultiplier = this.syncColorMultiplier;
            _style.syncFadeDuration = this.syncFadeDuration;
            _style.syncNavigation = this.syncNavigation;

            _style.syncDirection = this.syncDirection;
            _style.syncMinValue = this.syncMinValue;
            _style.syncMaxValue = this.syncMaxValue;
            _style.syncWholeNumbers = this.syncWholeNumbers;

            return _style;
        }

        public EasyUI_SliderStyle MakeCopy(Slider slider)
        {
            EasyUI_SliderStyle _style = new EasyUI_SliderStyle();
            _style.styleName = slider.name + " style";

            _style.isInteractable = slider.interactable;
            _style.transition = slider.transition;
            _style.colors = slider.colors;
            _style.navigation = slider.navigation;

            _style.direction = slider.direction;
            _style.minValue = slider.minValue;
            _style.maxValue = slider.maxValue;
            _style.wholeNumbers = slider.wholeNumbers;
            _style.value = slider.value;

            return _style;
        }
    }

    [System.Serializable]
    public class EasyUI_InputStyle : EasyUI_ButtonStyle
    {
        public string text = "";
        public int characterLimit = 0;
        public InputField.ContentType contentType = InputField.ContentType.Standard;
        public InputField.LineType lineType = InputField.LineType.SingleLine;
        public InputField.InputType inputType = InputField.InputType.Standard;
        public TouchScreenKeyboardType keyboardType = TouchScreenKeyboardType.NumberPad;
        public InputField.CharacterValidation characterValidation = InputField.CharacterValidation.Integer;
        public string regexValue = "";

        [Range(0, 4)]
        public float caretBlinkRate = 1f;
        [Range(1, 5)]
        public int caretWidth = 1;
        public bool customCaretColor = false;
        public Color caretColor = new Color(168 / 255, 206 / 255, 1, 192 / 255);
        public Color selectionColor = new Color(168 / 255, 206 / 255, 1, 192 / 255);
        public bool hideMobileInput = false;
        public bool readOnly = false;

        public bool syncText = true;
        public bool syncCharacterLimit = true;
        public bool syncContentType = true;
        public bool syncLineType = true;
        public bool syncInputType = true;
        public bool syncKeyboardType = true;
        public bool syncCharacterValidation = true;

        public bool syncCaretBlinkRate = true;
        public bool syncCaretWidth = true;
        public bool syncCustomCaretColor = true;
        public bool syncCaretColor = true;
        public bool syncSelectionColor = true;
        public bool syncHideMobileInput = true;
        public bool syncReadOnly = true;

        public override void ToggleAll(bool _isOn)
        {
            base.ToggleAll(_isOn);

            syncText = _isOn;
            syncCharacterLimit = _isOn;
            syncContentType = _isOn;
            syncContentType = _isOn;
            syncLineType = _isOn;
            syncCaretBlinkRate = _isOn;
            syncCaretWidth = _isOn;
            syncCustomCaretColor = _isOn;
            syncCaretColor = _isOn;
            syncSelectionColor = _isOn;
            syncHideMobileInput = _isOn;
            syncReadOnly = _isOn;
            syncInputType = _isOn;
            syncKeyboardType = _isOn;
            syncCharacterValidation = _isOn;
        }

        public override EasyUIStyle_Base MakeCopy()
        {
            EasyUI_InputStyle _style = new EasyUI_InputStyle();
            _style.styleName = "New Input Field Style";

            _style.isInteractable = this.isInteractable;
            _style.transition = this.transition;
            _style.colors = this.colors;
            _style.navigation = this.navigation;

            _style.text = this.text;
            _style.characterLimit = this.characterLimit;
            _style.contentType = this.contentType;
            _style.lineType = this.lineType;
            _style.inputType = this.inputType;
            _style.keyboardType = this.keyboardType;
            _style.characterValidation = this.characterValidation;

            _style.caretBlinkRate = this.caretBlinkRate;
            _style.caretWidth = this.caretWidth;
            _style.customCaretColor = this.customCaretColor;
            _style.caretColor = this.caretColor;
            _style.selectionColor = this.selectionColor;
            _style.hideMobileInput = this.hideMobileInput;
            _style.readOnly = this.readOnly;

            _style.syncIsInteractable = this.syncIsInteractable;
            _style.syncTransition = this.syncTransition;
            _style.syncNormalColor = this.syncNormalColor;
            _style.syncHighlightedColor = this.syncHighlightedColor;
            _style.syncPressedColor = this.syncPressedColor;
            _style.syncDisabledColor = this.syncDisabledColor;
            _style.syncColorMultiplier = this.syncColorMultiplier;
            _style.syncFadeDuration = this.syncFadeDuration;
            _style.syncNavigation = this.syncNavigation;

            _style.syncText = this.syncText;
            _style.syncCharacterLimit = this.syncCharacterLimit;
            _style.syncContentType = this.syncContentType;
            _style.syncLineType = this.syncLineType;
            _style.syncInputType = this.syncInputType;
            _style.syncKeyboardType = this.syncKeyboardType;
            _style.syncCharacterValidation = this.syncCharacterValidation;

            _style.syncCaretBlinkRate = this.syncCaretBlinkRate;
            _style.syncCaretWidth = this.syncCaretWidth;
            _style.syncCustomCaretColor = this.syncCustomCaretColor;
            _style.syncCaretColor = this.syncCaretColor;
            _style.syncSelectionColor = this.syncSelectionColor;
            _style.syncHideMobileInput = this.syncHideMobileInput;
            _style.syncHideMobileInput = this.syncHideMobileInput;
            _style.syncReadOnly = this.syncReadOnly;

            return _style;
        }

        public EasyUI_InputStyle MakeCopy(InputField inputField)
        {
            EasyUI_InputStyle _style = new EasyUI_InputStyle();
            _style.styleName = inputField.name + " style";

            _style.isInteractable = inputField.interactable;
            _style.transition = inputField.transition;
            _style.colors = inputField.colors;
            _style.navigation = inputField.navigation;

            _style.text = inputField.text;
            _style.characterLimit = inputField.characterLimit;
            _style.contentType = inputField.contentType;
            _style.lineType = inputField.lineType;
            _style.inputType = inputField.inputType;
            _style.keyboardType = inputField.keyboardType;
            _style.characterValidation = inputField.characterValidation;
            _style.caretBlinkRate = inputField.caretBlinkRate;
            _style.caretWidth = inputField.caretWidth;
            _style.customCaretColor = inputField.customCaretColor;
            _style.caretColor = inputField.caretColor;
            _style.selectionColor = inputField.selectionColor;
            _style.hideMobileInput = inputField.shouldHideMobileInput;
            _style.readOnly = inputField.readOnly;

            return _style;
        }
    }

    [System.Serializable]
    public class EasyUI_DropdownStyle : EasyUI_ButtonStyle
    {
        public int value = 0;
        public List<Dropdown.OptionData> optionList = new List<Dropdown.OptionData>();

        public bool syncValue = true;
        public bool syncOptions = false;

        public override void ToggleAll(bool _isOn)
        {
            base.ToggleAll(_isOn);

            syncValue = _isOn;
            syncOptions = _isOn;
        }

        public override EasyUIStyle_Base MakeCopy()
        {
            EasyUI_DropdownStyle _style = new EasyUI_DropdownStyle();
            _style.styleName = "New Dropdown Style";
            _style.isInteractable = this.isInteractable;
            _style.transition = this.transition;
            _style.colors = this.colors;
            _style.navigation = this.navigation;

            _style.value = this.value;
            _style.optionList = new List<Dropdown.OptionData>();

            foreach (Dropdown.OptionData _option in this.optionList)
            {
                Dropdown.OptionData newOption = new Dropdown.OptionData();
                newOption.image = _option.image;
                newOption.text = _option.text;
                _style.optionList.Add(newOption);
            }

            _style.syncIsInteractable = this.syncIsInteractable;
            _style.syncTransition = this.syncTransition;
            _style.syncNormalColor = this.syncNormalColor;
            _style.syncHighlightedColor = this.syncHighlightedColor;
            _style.syncPressedColor = this.syncPressedColor;
            _style.syncDisabledColor = this.syncDisabledColor;
            _style.syncColorMultiplier = this.syncColorMultiplier;
            _style.syncFadeDuration = this.syncFadeDuration;
            _style.syncNavigation = this.syncNavigation;

            _style.syncValue = this.syncValue;
            _style.syncOptions = this.syncOptions;

            return _style;
        }

        public EasyUI_DropdownStyle MakeCopy(Dropdown dropDown)
        {
            EasyUI_DropdownStyle _style = new EasyUI_DropdownStyle();
            _style.styleName = dropDown.name + " style";

            _style.isInteractable = dropDown.interactable;
            _style.transition = dropDown.transition;
            _style.colors = dropDown.colors;
            _style.navigation = dropDown.navigation;

            _style.value = dropDown.value;
            _style.optionList = new List<Dropdown.OptionData>();

            foreach (Dropdown.OptionData _option in dropDown.options)
            {
                Dropdown.OptionData newOption = new Dropdown.OptionData();
                newOption.image = _option.image;
                newOption.text = _option.text;
                _style.optionList.Add(newOption);
            }

            return _style;
        }

        public void AddOption(Dropdown.OptionData _option)
        {
            optionList.Add(_option);
        }

        public void RemoveOption(Dropdown.OptionData _option)
        {
            optionList.Remove(_option);
        }



    }

    [System.Serializable]
    public class EasyUI_TMPTextStyle : EasyUIStyle_Base
    {

        //things I want sync'd
        public TMPro.TMP_FontAsset fontAsset;
        public bool syncFontAsset = true;

        public Material materialPreset;
        public bool syncMaterialPreset = true;
        public int materialPresetIndex = 0;
        //used to help reduce the calles to the AssetDatabase - line 1444 EasyUIStyles
        public GUIContent[] materialPresets;
        public int[] materialValues = new int[] { 0 };

        public bool isRightToLeft = false;
        public bool syncIsRightToLeft = true;

        public FontStyles fontStyles = FontStyles.Normal;
        public bool syncFontStyles = true;

        public Color color = Color.white;

        public TMPro.TextAlignmentOptions textAlignment = TextAlignmentOptions.BaselineLeft;
        public bool syncTextAligment = true;
        public float wrapMix = 0.4f;
        public bool syncWrapMix = true;

        public float fontSize = 25;
        public bool syncFontSize = true;

        public TMPro.FontWeight fontWeight = FontWeight.Regular;
        public bool syncFontWeight = true;

        public bool enableAutoSize = false;
        public bool syncAutoSize = true;

        public float fontSizeMax = 50;
        public bool syncFontSizeMax = true;
        public float fontSizeMin = 15;
        public bool syncFontSizeMin = true;

        public float charWidthMaxAdjust = 0;
        public bool syncCharWidthMaxAdjust = true;
        public float lineSpacingMax = 0;
        public bool syncLineSpacingMax = true;

        public Color fontColor = Color.white;
        public bool syncColor = true;

        public bool enableVertextGradient = false;
        public bool syncEnableVertexGradient = true;

        public TMP_ColorGradient colorPreset;
        public bool syncColorPreset = true;

        public TMPro.ColorMode colorMode = ColorMode.FourCornersGradient;
        public Color topLeft = Color.white;
        public Color topRight = Color.white;
        public Color bottomLeft = Color.white;
        public Color bottomRight = Color.white;
        public bool syncColorGradient = true;

        public bool overrideTags = false;
        public bool syncOverrideTags = true;

        public float characterSpacing = 0;
        public bool syncCharacterSpacing = true;
        public float wordSpacing = 0;
        public bool syncWordSpacing = true;
        public float lineSpacing = 0;
        public bool syncLineSpacing = true;
        public float paragraphSpacing = 0;
        public bool syncParagraphSpacing = true;

        public bool enableWordWrapping = true;
        public bool syncWordWrapping = true;

        public TextOverflowModes overflowMode = TextOverflowModes.Overflow;
        public bool syncOverflowMode = true;

        public TextureMappingOptions horizontalMapping = TextureMappingOptions.Character;
        public bool syncHoriztonalMapping = true;
        public TextureMappingOptions verticalMapping = TextureMappingOptions.Character;
        public bool syncVerticalMapping = true;

        public float lineOffset = 0f;
        public bool syncLineOffset = true;

        public Vector4 margins = Vector4.zero;
        public bool syncMargins = true;

        public VertexSortingOrder geometrySorting = VertexSortingOrder.Normal;
        public bool syncGeometrySorting = true;

        public bool richText = true;
        public bool syncRichText = true;

        public bool raycastTarget = true;
        public bool syncRaycastTarget = true;

        public bool parseEscapeCharacters = true;
        public bool syncParseEscapeCharacters = true;

        public bool visibleDescender = true;
        public bool syncVisibleDescender = true;

        public TMP_SpriteAsset spriteAsset;
        public bool syncSpritieAsset = true;

        public bool kerning = true;
        public bool syncKerning = true;

        public bool extraPadding = false;
        public bool syncExtraPadding = true;

        public override void ToggleAll(bool _isOn)
        {
            syncFontAsset = !_isOn;
            syncMaterialPreset = !_isOn;
            syncIsRightToLeft = !_isOn;
            syncFontStyles = !_isOn;
            syncFontWeight = !_isOn;
            syncFontSize = !_isOn;
            syncTextAligment = !_isOn;
            syncAutoSize = !_isOn;
            syncFontSizeMax = !_isOn;
            syncFontSizeMin = !_isOn;
            syncCharWidthMaxAdjust = !_isOn;
            syncLineSpacingMax = !_isOn;
            syncColor = !_isOn;
            syncEnableVertexGradient = !_isOn;
            syncColorPreset = !_isOn;
            syncColorGradient = !_isOn;
            syncOverrideTags = !_isOn;
            syncCharacterSpacing = !_isOn;
            syncWordSpacing = !_isOn;
            syncLineSpacing = !_isOn;
            syncParagraphSpacing = !_isOn;
            syncWordWrapping = !_isOn;
            syncOverflowMode = !_isOn;
            syncHoriztonalMapping = !_isOn;
            syncVerticalMapping = !_isOn;
            syncLineOffset = !_isOn;
            syncMargins = !_isOn;
            syncGeometrySorting = !_isOn;
            syncRichText = !_isOn;
            syncRaycastTarget = !_isOn;
            syncParseEscapeCharacters = !_isOn;
            syncVisibleDescender = !_isOn;
            syncSpritieAsset = !_isOn;
            syncKerning = !_isOn;
            syncExtraPadding = !_isOn;

        }

        public GUIContent[] GetMaterialPresets()
        {

            if (fontAsset == null)
                return null;

            Material[] m_MaterialPresets = null;
#if UNITY_EDITOR
            m_MaterialPresets = TMPro.EditorUtilities.TMP_EditorUtility.FindMaterialReferences(fontAsset);
#endif
            GUIContent[] m_MaterialPresetNames = new GUIContent[m_MaterialPresets.Length];

            for (int i = 0; i < m_MaterialPresetNames.Length; i++)
            {
                m_MaterialPresetNames[i] = new GUIContent(m_MaterialPresets[i].name);
            }

            return m_MaterialPresetNames;
        }

        public Material GetMaterialPreset(int index)
        {
            Material material = null;
#if UNITY_EDITOR
            material = TMPro.EditorUtilities.TMP_EditorUtility.FindMaterialReferences(fontAsset)[index];
#endif
            return material;
        }

        public int[] GetMaterialValues()
        {
            int[] values = new int[GetMaterialPresets().Length];

            for (int i = 0; i < GetMaterialPresets().Length; i++)
                values[i] = i;

            return values;
        }

        public override EasyUIStyle_Base MakeCopy()
        {
            EasyUI_TMPTextStyle _style = new EasyUI_TMPTextStyle();
            _style.styleName = "New TMP Text Style";

            _style.fontAsset = this.fontAsset;
            _style.syncFontAsset = this.syncFontAsset;
            _style.materialPreset = this.materialPreset;
            _style.syncMaterialPreset = this.syncMaterialPreset;
            _style.materialPresetIndex = this.materialPresetIndex;
            _style.syncMaterialPreset = this.syncMaterialPreset;
            _style.isRightToLeft = this.isRightToLeft;
            _style.syncIsRightToLeft = this.syncIsRightToLeft;
            _style.fontStyles = this.fontStyles;
            _style.syncFontStyles = this.syncFontStyles;
            _style.color = this.color;
            _style.syncColor = this.syncColor;
            _style.textAlignment = this.textAlignment;
            _style.syncTextAligment = this.syncTextAligment;
            _style.wrapMix = this.wrapMix;
            _style.syncWrapMix = this.syncWrapMix;
            _style.fontSize = this.fontSize;
            _style.syncFontSize = this.syncFontSize;
            _style.enableAutoSize = this.enableAutoSize;
            _style.fontSizeMin = this.fontSizeMin;
            _style.syncFontSizeMin = this.syncFontSizeMin;
            _style.fontSizeMax = this.fontSizeMax;
            _style.syncFontSizeMax = this.syncFontSizeMax;
            _style.charWidthMaxAdjust = this.charWidthMaxAdjust;
            _style.syncCharWidthMaxAdjust = this.syncCharWidthMaxAdjust;
            _style.lineSpacingMax = this.lineSpacingMax;
            _style.syncLineSpacingMax = this.syncLineSpacingMax;
            _style.enableVertextGradient = this.enableVertextGradient;
            _style.syncEnableVertexGradient = this.syncEnableVertexGradient;
            _style.colorPreset = this.colorPreset;
            _style.syncColorPreset = this.syncColorPreset;
            _style.colorMode = this.colorMode;
            _style.syncColorGradient = this.syncColorGradient;
            _style.topLeft = this.topLeft;
            _style.topRight = this.topRight;
            _style.bottomLeft = this.bottomLeft;
            _style.bottomRight = this.bottomRight;
            _style.overrideTags = this.overrideTags;
            _style.syncOverrideTags = this.syncOverrideTags;
            _style.characterSpacing = this.characterSpacing;
            _style.syncCharacterSpacing = this.syncCharacterSpacing;
            _style.wordSpacing = this.wordSpacing;
            _style.syncWordSpacing = this.syncWordSpacing;
            _style.lineSpacing = this.lineSpacing;
            _style.syncLineSpacing = this.syncLineSpacing;
            _style.paragraphSpacing = this.paragraphSpacing;
            _style.syncParagraphSpacing = this.syncParagraphSpacing;
            _style.enableWordWrapping = this.enableWordWrapping;
            _style.syncWordWrapping = this.syncWordWrapping;
            _style.overflowMode = this.overflowMode;
            _style.syncOverflowMode = this.syncOverflowMode;
            _style.horizontalMapping = this.horizontalMapping;
            _style.syncHoriztonalMapping = this.syncHoriztonalMapping;
            _style.verticalMapping = this.verticalMapping;
            _style.syncVerticalMapping = this.syncVerticalMapping;
            _style.lineOffset = this.lineOffset;
            _style.syncLineOffset = this.syncLineOffset;
            _style.margins = this.margins;
            _style.syncMargins = this.syncMargins;
            _style.geometrySorting = this.geometrySorting;
            _style.syncGeometrySorting = this.syncGeometrySorting;
            _style.richText = this.richText;
            _style.syncRichText = this.syncRichText;
            _style.raycastTarget = this.raycastTarget;
            _style.syncRaycastTarget = this.syncRaycastTarget;
            _style.parseEscapeCharacters = this.parseEscapeCharacters;
            _style.syncParseEscapeCharacters = this.syncParseEscapeCharacters;
            _style.visibleDescender = this.visibleDescender;
            _style.syncVisibleDescender = this.syncVisibleDescender;
            _style.spriteAsset = this.spriteAsset;
            _style.syncSpritieAsset = this.spriteAsset;
            _style.kerning = this.kerning;
            _style.syncKerning = this.syncKerning;
            _style.extraPadding = this.extraPadding;
            _style.syncExtraPadding = this.syncExtraPadding;

            return _style;
        }

        public EasyUI_TMPTextStyle MakeCopy(TMPro.TMP_Text tmpText)
        {
            EasyUI_TMPTextStyle _style = new EasyUI_TMPTextStyle();
            _style.styleName = tmpText.name + " style";


            _style.fontAsset = tmpText.font;
            _style.materialPreset = tmpText.material;
            //_style.materialPresetIndex = tmpText.mat
            _style.isRightToLeft = tmpText.isRightToLeftText;
            _style.fontStyles = tmpText.fontStyle;
            _style.color = tmpText.color;
            _style.textAlignment = tmpText.alignment;
            _style.wrapMix = tmpText.wordWrappingRatios;
            _style.fontSize = tmpText.fontSize;
            _style.enableAutoSize = tmpText.enableAutoSizing;
            _style.fontSizeMin = tmpText.fontSizeMin;
            _style.fontSizeMax = tmpText.fontSizeMax;
            _style.charWidthMaxAdjust = tmpText.characterWidthAdjustment;
            _style.lineSpacingMax = tmpText.lineSpacingAdjustment;
            _style.enableVertextGradient = tmpText.enableVertexGradient;
            _style.colorPreset = tmpText.colorGradientPreset;
            _style.topLeft = tmpText.colorGradient.topLeft;
            _style.topRight = tmpText.colorGradient.topRight;
            _style.bottomLeft = tmpText.colorGradient.bottomLeft;
            _style.bottomRight = tmpText.colorGradient.bottomRight;
            _style.overrideTags = tmpText.overrideColorTags;
            _style.characterSpacing = tmpText.characterSpacing;
            _style.wordSpacing = tmpText.wordSpacing;
            _style.lineSpacing = tmpText.lineSpacing;
            _style.paragraphSpacing = tmpText.paragraphSpacing;
            _style.enableWordWrapping = tmpText.enableWordWrapping;
            _style.overflowMode = tmpText.overflowMode;
            _style.horizontalMapping = tmpText.horizontalMapping;
            _style.verticalMapping = tmpText.verticalMapping;
            _style.lineOffset = tmpText.mappingUvLineOffset;
            _style.margins = tmpText.margin;
            _style.geometrySorting = tmpText.geometrySortingOrder;
            _style.richText = tmpText.richText;
            _style.raycastTarget = tmpText.raycastTarget;
            _style.parseEscapeCharacters = tmpText.parseCtrlCharacters;
            //_style.visibleDescender = tmpText.visibleDescender;
            _style.spriteAsset = tmpText.spriteAsset;
            _style.kerning = tmpText.enableKerning;
            _style.extraPadding = tmpText.extraPadding;

            return _style;
        }
    }

    [System.Serializable]
    public class EasyUI_TMPInputStyle : EasyUI_InputStyle
    {
        public TMPro.TMP_FontAsset fontAsset;
        public bool syncFontAsset = true;

        public float pointSize = 14;
        public bool syncPointSize = true;

        //control settings
        public bool onFocusSelectAll = true;
        public bool syncOnFucusSelectAll = true;

        public bool resetOnDeactivation = true;
        public bool syncResetOnDeactivation = true;

        public bool restoreOnEscapeKey = true;
        public bool syncRetoreOnEscapeKey = true;

        public bool hideSoftKeyboard = false;
        public bool syncHideSoftKeyboard = true;

        public bool richText = true;
        public bool syncRichText = true;

        public bool allowRichTextEditing = true;
        public bool syncAllowRichTextEditing = true;

        public TMP_InputField.CharacterValidation tmp_characterValidation = TMP_InputField.CharacterValidation.Integer;
        public bool syncRegexValue = true;

        public TMP_InputValidator tmp_inputValidator;
        public bool syncInputValidator = true;

        public int lineLimit = 0;
        public bool syncLineLimit = true;

        public override EasyUIStyle_Base MakeCopy()
        {
            EasyUI_TMPInputStyle _style = new EasyUI_TMPInputStyle();
            _style.styleName = "New TMP Input Field Style";

            _style.isInteractable = this.isInteractable;
            _style.transition = this.transition;
            _style.colors = this.colors;
            _style.navigation = this.navigation;

            _style.text = this.text;
            _style.characterLimit = this.characterLimit;
            _style.contentType = this.contentType;
            _style.lineType = this.lineType;
            _style.inputType = this.inputType;
            _style.keyboardType = this.keyboardType;
            _style.characterValidation = this.characterValidation;

            _style.caretBlinkRate = this.caretBlinkRate;
            _style.caretWidth = this.caretWidth;
            _style.customCaretColor = this.customCaretColor;
            _style.caretColor = this.caretColor;
            _style.selectionColor = this.selectionColor;
            _style.hideMobileInput = this.hideMobileInput;
            _style.readOnly = this.readOnly;

            _style.syncIsInteractable = this.syncIsInteractable;
            _style.syncTransition = this.syncTransition;
            _style.syncNormalColor = this.syncNormalColor;
            _style.syncHighlightedColor = this.syncHighlightedColor;
            _style.syncPressedColor = this.syncPressedColor;
            _style.syncDisabledColor = this.syncDisabledColor;
            _style.syncColorMultiplier = this.syncColorMultiplier;
            _style.syncFadeDuration = this.syncFadeDuration;
            _style.syncNavigation = this.syncNavigation;

            _style.syncText = this.syncText;
            _style.syncCharacterLimit = this.syncCharacterLimit;
            _style.syncContentType = this.syncContentType;
            _style.syncLineType = this.syncLineType;
            _style.syncInputType = this.syncInputType;
            _style.syncKeyboardType = this.syncKeyboardType;
            _style.syncCharacterValidation = this.syncCharacterValidation;

            _style.syncCaretBlinkRate = this.syncCaretBlinkRate;
            _style.syncCaretWidth = this.syncCaretWidth;
            _style.syncCustomCaretColor = this.syncCustomCaretColor;
            _style.syncCaretColor = this.syncCaretColor;
            _style.syncSelectionColor = this.syncSelectionColor;
            _style.syncHideMobileInput = this.syncHideMobileInput;
            _style.syncHideMobileInput = this.syncHideMobileInput;
            _style.syncReadOnly = this.syncReadOnly;

            return _style;
        }

        public EasyUI_TMPInputStyle MakeCopy(TMPro.TMP_InputField inputField)
        {
            EasyUI_TMPInputStyle _style = new EasyUI_TMPInputStyle();
            _style.styleName = inputField.name + " style";

            _style.isInteractable = inputField.interactable;
            _style.transition = inputField.transition;
            _style.colors = inputField.colors;
            _style.navigation = inputField.navigation;

            _style.text = inputField.text;
            _style.characterLimit = inputField.characterLimit;
            _style.contentType = (UnityEngine.UI.InputField.ContentType)inputField.contentType;
            _style.lineType = (UnityEngine.UI.InputField.LineType)inputField.lineType;
            _style.inputType = (UnityEngine.UI.InputField.InputType)inputField.inputType;
            _style.keyboardType = inputField.keyboardType;
            _style.tmp_characterValidation = inputField.characterValidation;
            _style.caretBlinkRate = inputField.caretBlinkRate;
            _style.caretWidth = inputField.caretWidth;
            _style.customCaretColor = inputField.customCaretColor;
            _style.caretColor = inputField.caretColor;
            _style.selectionColor = inputField.selectionColor;
            _style.hideMobileInput = inputField.shouldHideMobileInput;
            _style.readOnly = inputField.readOnly;

            return _style;
        }

        public override void ToggleAll(bool _isOn)
        {
            base.ToggleAll(_isOn);
            syncFontAsset = _isOn;
            syncPointSize = _isOn;
            syncOnFucusSelectAll = _isOn;
            syncRetoreOnEscapeKey = _isOn;
            syncRichText = _isOn;
            syncAllowRichTextEditing = _isOn;
            syncRegexValue = _isOn;
            syncInputValidator = _isOn;
            syncLineLimit = _isOn;
            syncResetOnDeactivation = _isOn;
            syncHideSoftKeyboard = _isOn;
        }
    }

    [System.Serializable]
    public class EasyUI_TMPDropDownStyle : EasyUI_DropdownStyle
    {
        public List<TMP_Dropdown.OptionData> tmp_optionList = new List<TMP_Dropdown.OptionData>();

        public void AddOption(TMP_Dropdown.OptionData _option)
        {
            tmp_optionList.Add(_option);
        }

        public void RemoveOption(TMP_Dropdown.OptionData _option)
        {
            tmp_optionList.Remove(_option);
        }

        public override EasyUIStyle_Base MakeCopy()
        {
            EasyUI_TMPDropDownStyle _style = new EasyUI_TMPDropDownStyle();
            _style.styleName = "New TMP Dropdown Style";

            _style.isInteractable = this.isInteractable;
            _style.transition = this.transition;
            _style.colors = this.colors;
            _style.navigation = this.navigation;

            _style.value = this.value;
            _style.optionList = new List<Dropdown.OptionData>();

            foreach (Dropdown.OptionData _option in this.optionList)
            {
                Dropdown.OptionData newOption = new Dropdown.OptionData();
                newOption.image = _option.image;
                newOption.text = _option.text;
                _style.optionList.Add(newOption);
            }

            _style.syncIsInteractable = this.syncIsInteractable;
            _style.syncTransition = this.syncTransition;
            _style.syncNormalColor = this.syncNormalColor;
            _style.syncHighlightedColor = this.syncHighlightedColor;
            _style.syncPressedColor = this.syncPressedColor;
            _style.syncDisabledColor = this.syncDisabledColor;
            _style.syncColorMultiplier = this.syncColorMultiplier;
            _style.syncFadeDuration = this.syncFadeDuration;
            _style.syncNavigation = this.syncNavigation;

            _style.syncValue = this.syncValue;
            _style.syncOptions = this.syncOptions;

            return _style;
        }

        public EasyUI_TMPDropDownStyle MakeCopy(TMP_Dropdown dropDown)
        {
            EasyUI_TMPDropDownStyle _style = new EasyUI_TMPDropDownStyle();
            _style.styleName = dropDown.name + " style";

            _style.isInteractable = dropDown.interactable;
            _style.transition = dropDown.transition;
            _style.colors = dropDown.colors;
            _style.navigation = dropDown.navigation;

            _style.value = dropDown.value;
            _style.optionList = new List<Dropdown.OptionData>();

            foreach (TMP_Dropdown.OptionData _option in dropDown.options)
            {
                TMP_Dropdown.OptionData newOption = new TMP_Dropdown.OptionData();
                newOption.image = _option.image;
                newOption.text = _option.text;
                _style.tmp_optionList.Add(newOption);
            }

            return _style;
        }

        public override void ToggleAll(bool _isOn)
        {
            base.ToggleAll(_isOn);
        }
    }

    //helper functions used by various Easy UI Style classes
    public class EasyUI_HelperFunctions
    {
        public static EasyUI_Style_Data LoadData()
        {
            EasyUI_Style_Data easyUI_Data = null;
#if UNITY_EDITOR

            easyUI_Data = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Plugins/EasyUIStyles/Resources/EasyUIData.asset", typeof(EasyUI_Style_Data)) as EasyUI_Style_Data;

            //attempt to find data in other folder
            if (easyUI_Data == null)
            {
                Debug.Log("Couldn't find data at path");

                string[] guids = UnityEditor.AssetDatabase.FindAssets("EasyUIData.asset");

                if (guids.Length > 0)
                {
                    string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
                    easyUI_Data = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(EasyUI_Style_Data)) as EasyUI_Style_Data;
                }
            }
#endif
            return easyUI_Data;
        }

        public static GUISkin GetSkin()
        {
            GUISkin skin = null;
#if UNITY_EDITOR
            if (UnityEditor.EditorGUIUtility.isProSkin)
                skin = UnityEditor.EditorGUIUtility.Load("Assets/EasyUIStyles/Resources/EasyUI_DarkSKin.guiskin") as GUISkin;
            else
                skin = UnityEditor.EditorGUIUtility.Load("Assets/EasyUIStyles/Resources/EasyUI_LightSkin.guiskin") as GUISkin;

            //attempt to find data in other folder
            if (skin == null)
            {
                string[] guids;
                if (UnityEditor.EditorGUIUtility.isProSkin)
                    guids = UnityEditor.AssetDatabase.FindAssets("EasyUI_DarkSKin");
                else
                    guids = UnityEditor.AssetDatabase.FindAssets("EasyUI_LightSkin");

                if (guids.Length > 0)
                {
                    string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);

                    if (UnityEditor.EditorGUIUtility.isProSkin)
                        skin = UnityEditor.EditorGUIUtility.Load(path) as GUISkin;
                    else
                        skin = UnityEditor.EditorGUIUtility.Load(path) as GUISkin;
                }
            }
#endif
            return skin;
        }

        public static string[] CreateStyleList(List<string> _styles)
        {
            string[] stylesArray = new string[_styles.Count + 1];

            stylesArray[0] = "--- None ---";

            for (int i = 0; i < _styles.Count; i++)
            {
                stylesArray[i + 1] = (i + 1).ToString() + ". " + _styles[i];
            }

            return stylesArray;
        }

        public static List<string> GetStyles<T>() where T : EasyUIStyle_Base
        {
            List<string> styleList = new List<string>();

            if(EasyUIComponentCore<T>.easyUI_Data == null)
            {
                Debug.Log("No Data");
                return null;
            }

            List<T> styles = EasyUIComponentCore<T>.easyUI_Data.ReturnListOfType<T>();
            if (styles == null || styles.Count == 0)
                return styleList;

            for (int i = 0; i < styles.Count; i++)
            {
                styleList.Add(styles[i].styleName);
            }

            return styleList;
        }

        public static List<string> GetStylesTMP<T>() where T : EasyUIStyle_Base
        {
            List<string> styleList = new List<string>();

            if (EasyUIComponentCore<T>.easyUI_Data == null)
            {
                Debug.Log("No Data");
                return null;
            }

            List<T> styles = EasyUIComponentCore<T>.easyUI_Data.ReturnListOfTypeTMP<T>();
            if (styles == null || styles.Count == 0)
                return styleList;

            for (int i = 0; i < styles.Count; i++)
            {
                styleList.Add(styles[i].styleName);
            }

            return styleList;
        }

        public static void UpdateTextFormat(EasyUI_TextStyle thisTextStyle, Text text)
        {
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
            if (thisTextStyle.syncLineSpacing)
                text.lineSpacing = thisTextStyle.lineSpacing;
            if (thisTextStyle.syncRichText)
                text.supportRichText = thisTextStyle.richText;
            if (thisTextStyle.syncVerticalWrapMode)
                text.verticalOverflow = thisTextStyle.vertWrap;
            if (thisTextStyle.syncHorizontalWrapMode)
                text.horizontalOverflow = thisTextStyle.horzWrap;
            if (thisTextStyle.syncAlignByGeometry)
                text.alignByGeometry = thisTextStyle.alignByGeometry;
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

        public static void UpdateImageFormat(EasyUI_ImageStyle thisImageStyle, Image image)
        {
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

        public static void UpdateButtonCore(EasyUI_ButtonStyle style, Selectable uiObject)
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
            if (style.syncSelectedColor) //add this and the next line
                cb.selectedColor = style.colors.selectedColor;
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
            if (style.syncSelectedSprite)
                ss.selectedSprite = style.selectedSprite;
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

        public static void UpdateToggleFormat(EasyUI_ToggleStyle thisToggleStyle, Toggle toggle)
        {
            if (thisToggleStyle == null)
                return;

            UpdateButtonCore(thisToggleStyle, toggle);

            if (thisToggleStyle.syncIsOn)
                toggle.isOn = thisToggleStyle.isOn;
            if (thisToggleStyle.syncToggleTransition)
                toggle.toggleTransition = thisToggleStyle.toggleTransition;
        }

        public static void UpdateSliderFormat(EasyUI_SliderStyle thisSliderStyle, Slider slider)
        {
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

        public static void UpdateInputFormat(EasyUI_InputStyle thisInputStyle, InputField input)
        {
            if (thisInputStyle == null)
                return;

            UpdateButtonCore(thisInputStyle, input);

            if (thisInputStyle.syncText)
                input.text = thisInputStyle.text;
            if (thisInputStyle.syncCharacterLimit)
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

        public static void UpdateDropdownFormat(EasyUI_DropdownStyle thisDropdownStyle, Dropdown dropdown)
        {
            if (thisDropdownStyle == null)
                return;

            UpdateButtonCore(thisDropdownStyle, dropdown);

            if (thisDropdownStyle.syncValue)
                dropdown.value = thisDropdownStyle.value;
            if (thisDropdownStyle.syncOptions)
                dropdown.options = thisDropdownStyle.optionList;
        }

        public static void UpdateTMProTextFormat(EasyUI_TMPTextStyle thisTMProTextStyle, TextMeshProUGUI tmproText)
        {
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

            if (thisTMProTextStyle.syncColorGradient && thisTMProTextStyle.enableVertextGradient)
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

        public static void UpdateTMProInput(EasyUI_TMPInputStyle thisTMProInputStyle, TMP_InputField tmproInput)
        {
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

        public static void UpdateTMProDropdown(EasyUI_TMPDropDownStyle thisTMProDropDownStyle, EasyUITMProDropdown tmproDropdown)
        {
            if (thisTMProDropDownStyle == null)
                return;

            UpdateButtonCore(thisTMProDropDownStyle, tmproDropdown);

            if (thisTMProDropDownStyle.syncValue)
                tmproDropdown.value = thisTMProDropDownStyle.value;
            if (thisTMProDropDownStyle.syncOptions)
                tmproDropdown.options = thisTMProDropDownStyle.tmp_optionList;
        }
    }
}

