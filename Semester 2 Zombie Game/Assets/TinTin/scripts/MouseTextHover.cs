using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MouseTextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject button;
    public float width = 160f;
    public float height = 30f;
    RectTransform rt;
    private Vector2 originalSize;
    private Vector2 targetSize;

    void Start()
    {
        rt = button.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(width, height);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("enter");
        originalSize = rt.sizeDelta;
        targetSize = new Vector2(width + 50, height + 50);

        StartCoroutine(ChangeSize());
    }
 
    public void OnPointerExit(PointerEventData data)
    {
        Debug.Log("exit");
        rt.sizeDelta = new Vector2(width, height);
    }

    IEnumerator ChangeSize()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 1/*duration*/)
        {
            float t = elapsedTime / 1/*duration*/;
            rt.sizeDelta = Vector2.Lerp(originalSize, targetSize, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rt.sizeDelta = targetSize;
    }
}
