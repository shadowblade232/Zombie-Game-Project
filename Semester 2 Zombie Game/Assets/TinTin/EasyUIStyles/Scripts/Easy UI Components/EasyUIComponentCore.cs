using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUIStyle;

public class EasyUIComponentCore<T> where T : EasyUIStyle_Base
{
    private T _styleData;
    public T styleData
    {
        get
        {
            if (_styleData == null)
                SetStyleData();
            return _styleData;
        }
    }
    private static EasyUI_Style_Data _easyUI_data;
    public static EasyUI_Style_Data easyUI_Data
    {
        get
        {
            if(_easyUI_data == null)
                _easyUI_data = EasyUI_HelperFunctions.LoadData();
            return _easyUI_data;
        }
    }
    public int styleIndex = 0;

    public delegate void StyleDeletedEvent(int style);
    public static event StyleDeletedEvent styleDeleted;

    public void Initialize()
    {
        //Grab scriptable object with style data
        if (easyUI_Data == null)
            _easyUI_data = EasyUI_HelperFunctions.LoadData();
    }

    public static void StyleDeleted(T deletedStyle)
    {
        for (int i = 0; i < easyUI_Data.ReturnListOfType<T>().Count; i++)
        {
            if (easyUI_Data.ReturnListOfType<T>()[i] == deletedStyle)
            {
                styleDeleted?.Invoke(i + 1);
                return;
            }
        }
    }

    public void AdjustStyleIndex(int deletedStyle)
    {
        if (this.styleIndex < deletedStyle)
            return;
        else if (deletedStyle == this.styleIndex)
            this.styleIndex = 0;
        else
            this.styleIndex--;
    }

    public void SetStyleData(int index)
    {
        styleIndex = index;
        SetStyleData();
    }

    public void SetStyleData()
    {
        List<T> dataList = easyUI_Data.ReturnListOfType<T>();
        if (dataList.Count >= styleIndex && styleIndex != 0)
            SetStyleData(dataList[styleIndex - 1]); //minus 1 to account for first blank --none-- style
    }

    private void SetStyleData(T data)
    {
        _styleData = data; 
    }
}
