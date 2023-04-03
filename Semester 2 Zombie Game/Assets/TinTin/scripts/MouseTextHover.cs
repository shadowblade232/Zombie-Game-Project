using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MouseTextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isPointerOver = false;

    public GameObject button;
    public TextMeshProUGUI buttonText;
    RectTransform rt;
    private Vector2 originalSize;
    private Vector2 targetSize;
    private Vector2 currentSize;
    public float sizeRatio;
    public float sizeDuration;
    private float originalTextSize;

    void Start()
    {
        rt = button.GetComponent<RectTransform>();
        originalSize = rt.sizeDelta;
        originalTextSize = buttonText.fontSize;
    }

    // Update is called once per frame
    void Update()
    {
        currentSize = rt.sizeDelta;
        sizeRatio = currentSize.x / originalSize.x;

        buttonText.fontSize = originalTextSize * sizeRatio;

        if(isPointerOver){
            rt.sizeDelta = new Vector2(originalSize.x + 50, originalSize.y + 50);
        } else{
            rt.sizeDelta = originalSize;
        }
    }

    //public void OnPointerEnter(PointerEventData data)
    //{
    //    Debug.Log("enter");
    //    targetSize = new Vector2(originalSize.x + 50, originalSize.y + 50);
    //
    //    // Stop any running coroutine before starting a new one
    //    StopAllCoroutines();
    //
    //    StartCoroutine(enlarge());
    //}
    //
    //public void OnPointerExit(PointerEventData data)
    //{
    //    Debug.Log("exit");
    //    currentSize = rt.sizeDelta;
    //
    //    // Stop any running coroutine before starting a new one
    //    StopAllCoroutines();
    //
    //    StartCoroutine(decreaseSize());
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
    }

    IEnumerator enlarge()
    {
        float elapsedTime = 0f;

        while (elapsedTime < sizeDuration)
        {
            float t = elapsedTime / sizeDuration;
            rt.sizeDelta = Vector2.Lerp(originalSize, targetSize, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rt.sizeDelta = targetSize;
    }

    IEnumerator decreaseSize()
    {
        float elapsedTime = 0f;

        while (elapsedTime < sizeDuration)
        {
            float t = elapsedTime / sizeDuration;
            rt.sizeDelta = Vector2.Lerp(currentSize, originalSize, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rt.sizeDelta = targetSize;
    }
}
