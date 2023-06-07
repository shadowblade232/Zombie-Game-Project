using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EasyUIStyle
{
    [System.Serializable]
    [DisallowMultipleComponent]
    [AddComponentMenu("Easy UI/ Dropdown")]

    public class EasyUIDropdown : Dropdown
    {
        public EasyUI_DropdownStyle buttonData;
        public static EasyUI_Style_Data easyUI_Data;
        public int styleIndex = 0;

        public EasyUIComponentCore<EasyUI_DropdownStyle> easyUICore = new EasyUIComponentCore<EasyUI_DropdownStyle>();

        private delegate void UpdateStyles();
        private static event UpdateStyles updateStyles;

        new void Start()
        {
            easyUICore.Initialize();
            base.Start();
        }
#if UNITY_EDITOR

        protected new void Reset()
        {
            easyUICore.Initialize();
            base.Reset();
        }
#endif 

        protected override void OnEnable()
        {
            updateStyles += UpdateFormat;
            EasyUIComponentCore<EasyUI_DropdownStyle>.styleDeleted += easyUICore.AdjustStyleIndex;
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            updateStyles -= UpdateFormat;
            EasyUIComponentCore<EasyUI_DropdownStyle>.styleDeleted -= easyUICore.AdjustStyleIndex;
            base.OnDisable();
        }
#if UNITY_EDITOR
        protected new void OnValidate()
        {
            if (easyUI_Data == null)
                easyUI_Data = EasyUI_HelperFunctions.LoadData();

            if (easyUICore.styleIndex > 0)
            {
                easyUICore.SetStyleData();
                UpdateFormat();
            }

            base.OnValidate();
        }
#endif 
        private void UpdateFormat()
        {
            easyUICore.styleIndex = this.styleIndex;

            if (easyUICore.styleData != null)
                EasyUI_HelperFunctions.UpdateDropdownFormat(easyUICore.styleData, this);
        }

        //called from editor to force update
        public static void UpdateStyle()
        {
            updateStyles?.Invoke();
        }
    }
}
