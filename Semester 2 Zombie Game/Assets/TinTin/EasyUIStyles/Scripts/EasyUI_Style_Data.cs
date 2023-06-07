using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EasyUIStyle
{
    [CreateAssetMenu(fileName = "EasyUIData", menuName = "Easy UI Style Data Container", order = 1010)]
    [System.Serializable]
    public class EasyUI_Style_Data : ScriptableObject
    {
        [SerializeField]
        public List<EasyUI_ImageStyle> imageList = new List<EasyUI_ImageStyle>();
        [SerializeField]
        public List<EasyUI_TextStyle> textList = new List<EasyUI_TextStyle>();
        [SerializeField]
        public List<EasyUI_ButtonStyle> buttonList = new List<EasyUI_ButtonStyle>();
        [SerializeField]
        public List<EasyUI_ToggleStyle> toggleList = new List<EasyUI_ToggleStyle>();
        [SerializeField]
        public List<EasyUI_SliderStyle> sliderList = new List<EasyUI_SliderStyle>();
        [SerializeField]
        public List<EasyUI_InputStyle> inputList = new List<EasyUI_InputStyle>();
        [SerializeField]
        public List<EasyUI_DropdownStyle> dropdownList = new List<EasyUI_DropdownStyle>();
        [SerializeField]
        public List<EasyUI_TMPTextStyle> tmproTextList = new List<EasyUI_TMPTextStyle>();
        [SerializeField]
        public List<EasyUI_TMPInputStyle> tmproInputList = new List<EasyUI_TMPInputStyle>();
        [SerializeField]
        public List<EasyUI_TMPDropDownStyle> tmproDropdownList = new List<EasyUI_TMPDropDownStyle>();

        public List<T> ReturnListOfType<T>() where T : EasyUIStyle_Base
        {
            if (imageList.Count > 0 && imageList[0] is T)
                return imageList as List<T>;
            else if (textList.Count > 0 && textList[0] is T)
                return textList as List<T>;
            else if (buttonList.Count > 0 && buttonList[0] is T)
                return buttonList as List<T>;
            else if (toggleList.Count > 0 && toggleList[0] is T)
                return toggleList as List<T>;
            else if (sliderList.Count > 0 && sliderList[0] is T)
                return sliderList as List<T>;
            else if (inputList.Count > 0 && inputList[0] is T)
                return inputList as List<T>;
            else if (dropdownList.Count > 0 && dropdownList[0] is T)
                return dropdownList as List<T>;
            if (tmproInputList.Count > 0 && tmproInputList[0] is T)
                return tmproInputList as List<T>;
            else if (tmproDropdownList.Count > 0 && tmproDropdownList[0] is T)
                return tmproDropdownList as List<T>;
            else if (tmproTextList.Count > 0 && tmproTextList[0] is T)
                return tmproTextList as List<T>;
            else
            {
                Debug.Log("No List found");
                return null;
            }
        }

        public List<T> ReturnListOfTypeTMP<T>() where T : EasyUIStyle_Base
        {
            if (tmproInputList.Count > 0 && tmproInputList[0] is T)
                return tmproInputList as List<T>;
            else if (tmproDropdownList.Count > 0 && tmproDropdownList[0] is T)
                return tmproDropdownList as List<T>;
            else if (tmproTextList.Count > 0 && tmproTextList[0] is T)
                return tmproTextList as List<T>;
            else
            {
                Debug.Log("No List found");
                return null;
            }
        }

    }
}

