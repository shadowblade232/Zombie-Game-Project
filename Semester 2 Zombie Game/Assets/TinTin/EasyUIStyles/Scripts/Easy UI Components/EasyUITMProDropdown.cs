using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EasyUIStyle
{
    [RequireComponent(typeof(Image))]
    [System.Serializable]
    [DisallowMultipleComponent]
    [AddComponentMenu("Easy UI/ TMP Dropdown")]
    public class EasyUITMProDropdown : TMPro.TMP_Dropdown
    {
        public EasyUI_TMPDropDownStyle styleData;
        public static EasyUI_Style_Data easyUI_Data;
        public int styleIndex = 0;

        public EasyUIComponentCore<EasyUI_TMPDropDownStyle> easyUICore = new EasyUIComponentCore<EasyUI_TMPDropDownStyle>();

        private delegate void UpdateStyles();
        private static event UpdateStyles updateStyles;

        new void Start()
        {
            if (targetGraphic == null)
            {
                targetGraphic = this.GetComponent<Image>();
                this.GetComponent<Image>().type = Image.Type.Sliced;
            }
            easyUICore.Initialize();
            base.Start();
        }

        protected override void OnEnable()
        {
            updateStyles += UpdateFormat;
            EasyUIComponentCore<EasyUI_TMPDropDownStyle>.styleDeleted += easyUICore.AdjustStyleIndex;
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            updateStyles -= UpdateFormat;
            EasyUIComponentCore<EasyUI_TMPDropDownStyle>.styleDeleted -= easyUICore.AdjustStyleIndex;
            base.OnDisable();
        }

#if UNITY_EDITOR
        private new void Reset()
        {
            easyUICore.Initialize();
            base.Reset();
        }

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
                EasyUI_HelperFunctions.UpdateTMProDropdown(easyUICore.styleData, this);
        }

        //called from editor to force update
        public static void UpdateStyle()
        {
            updateStyles?.Invoke();
        }
    }
}
