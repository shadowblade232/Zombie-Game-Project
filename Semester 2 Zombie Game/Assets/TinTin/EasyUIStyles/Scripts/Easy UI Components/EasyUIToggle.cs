using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EasyUIStyle
{
    [System.Serializable]
    [DisallowMultipleComponent]
    [AddComponentMenu("Easy UI/ Toggle")]

    public class EasyUIToggle : Toggle
    {
        public EasyUI_ToggleStyle buttonData;
        public static EasyUI_Style_Data easyUI_Data;
        public int styleIndex = 0;

        public EasyUIComponentCore<EasyUI_ToggleStyle> easyUICore = new EasyUIComponentCore<EasyUI_ToggleStyle>();

        private delegate void UpdateStyles();
        private static event UpdateStyles updateStyles;

        new void Start()
        {
            easyUICore.Initialize();
            base.Start();
        }


        protected override void OnEnable()
        {
            updateStyles += UpdateFormat;
            EasyUIComponentCore<EasyUI_ToggleStyle>.styleDeleted += easyUICore.AdjustStyleIndex;
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            updateStyles -= UpdateFormat;
            EasyUIComponentCore<EasyUI_ToggleStyle>.styleDeleted -= easyUICore.AdjustStyleIndex;
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
                EasyUI_HelperFunctions.UpdateToggleFormat(easyUICore.styleData, this);
        }

        //called from editor to force update
        public static void UpdateStyle()
        {
            updateStyles?.Invoke();
        }
    }
}
