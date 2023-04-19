using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;

public class TimeOfDay : MonoBehaviour
{
    public GameObject sun;
    public GameObject moon;

    public float timeScale = 60 * 20;
    public float time;
    public float timeOffset = 0;

    Quaternion rotationQ;

    Vector3 rotationVSun;
    Vector3 rotationVMoon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        time = -0.5f * Mathf.Cos(Mathf.PI * Time.time / timeScale) + 0.5f;

        if ((0.5f * Mathf.Sin(Mathf.PI * Time.time / timeScale) * (Mathf.PI / timeScale)) < 0)
        {
            time = -time + 1;
        }

        float yRotation = -60;
        float zRotation = 40;

        time += timeOffset;
        rotationVSun = new Vector3(time * 360, yRotation, zRotation);
        rotationVMoon = new Vector3(time * 360 + 180, yRotation, zRotation);

        rotationQ.eulerAngles = rotationVSun;
        sun.transform.rotation = rotationQ;
        rotationQ.eulerAngles = rotationVMoon;
        moon.transform.rotation = rotationQ;

        if (time > 0.5)
        {
            moon.GetComponent<Light>().shadows = LightShadows.Soft;
            sun.GetComponent<Light>().shadows = LightShadows.None;
        }

        else
        {
            sun.GetComponent<Light>().shadows = LightShadows.Soft;
            moon.GetComponent<Light>().shadows = LightShadows.None;
        }
    }
}
