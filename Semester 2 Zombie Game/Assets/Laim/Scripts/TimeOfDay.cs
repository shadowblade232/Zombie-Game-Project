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
        time = Mathf.Repeat(Time.time / timeScale, 24);
        float yRotation = -60;
        
        rotationVSun = new Vector3(time * 15, yRotation, 0);
        rotationVMoon = new Vector3(time * 15 + 180, yRotation, 0);
        rotationQ.eulerAngles = rotationVSun;
        sun.transform.rotation = rotationQ;
        rotationQ.eulerAngles = rotationVMoon;
        moon.transform.rotation = rotationQ;

        if (time > 12)
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
