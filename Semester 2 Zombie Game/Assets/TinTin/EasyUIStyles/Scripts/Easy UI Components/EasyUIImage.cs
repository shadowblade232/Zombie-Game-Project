using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EasyUIStyle
{
    [System.Serializable]
    [DisallowMultipleComponent]
    [AddComponentMenu("Easy UI/ Image")]

    public class EasyUIImage : Image
    {
        public EasyUI_ImageStyle imageData;
        public static EasyUI_Style_Data easyUI_Data;

        [SerializeField]
        public new Material material;
        public int styleIndex = 0;

        public EasyUIComponentCore<EasyUI_ImageStyle> easyUICore = new EasyUIComponentCore<EasyUI_ImageStyle>();

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
            EasyUIComponentCore<EasyUI_ImageStyle>.styleDeleted += easyUICore.AdjustStyleIndex;
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            updateStyles -= UpdateFormat;
            EasyUIComponentCore<EasyUI_ImageStyle>.styleDeleted -= easyUICore.AdjustStyleIndex;
            base.OnDisable();
        }

#if UNITY_EDITOR
        private new void Reset()
        {
            easyUICore.Initialize();
            SetAllDirty();
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

            SetAllDirty();
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
        private void UpdateFormat()
        {
            easyUICore.styleIndex = this.styleIndex;

            if (easyUICore.styleData != null)
                EasyUI_HelperFunctions.UpdateImageFormat(easyUICore.styleData, this);
            else
                Debug.Log("No data found");
        }

        //called from editor to force update
        public static void UpdateStyle()
        {
            Debug.Log("Updating Image Style");
            updateStyles?.Invoke();
        }

    }
}
