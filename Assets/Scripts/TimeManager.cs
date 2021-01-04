using UnityEngine.UI;
using UnityEngine;
using System.Collections;
public class TimeManager : MonoBehaviour
{
    public Text Timer;
    string timeNow;
    int hours;
    int minutes;
    float second;
    new GameObject[] light;
    Light[] streetLight;
    public Material[] skymat;
    public Light sun;

    void Start()
    {
        light = GameObject.FindGameObjectsWithTag("Lantern");
        streetLight = new Light[light.Length];
        for(int i = 0;i < light.Length; i++)
        {
            streetLight[i] = light[i].GetComponentInChildren<Light>();
        }
    } 

    void Update()
    {
       controlDayAndNight();
        Time();
    }

    void controlDayAndNight()
    {
        if(hours >= 18)
        {
            nightSetting();
        }
        else
        {
            sunSetting();
        }
    }

    void Time()
    {
        hours = System.DateTime.Now.Hour;
        minutes = System.DateTime.Now.Minute;
        second = System.DateTime.Now.Second;
        timeNow = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + second.ToString("00");
        Timer.text = "時間: " + timeNow;

        if (hours >= 18)
        {
            Timer.color = Color.white;
        }
        else
        {
            Timer.color = Color.black;
        }
    }

    void sunSetting()
    {
        foreach (Light light in streetLight)
        {
            light.enabled = false;
        }
        sun.colorTemperature = 6000;
        sun.intensity = 1;
        sun.color = Color.white;
        RenderSettings.skybox = skymat[0];
    }

    void nightSetting()
    {
        foreach (Light light in streetLight)
        {
            light.enabled = true;
        }
        sun.colorTemperature = 14000;
        sun.color = Color.gray;
        sun.intensity = 1;
        RenderSettings.skybox = skymat[1];
    }
}
