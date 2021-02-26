using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    public GameObject Car;
    public Slider slider;
    public float startDis;
    public float currentDistance;
    public Transform finishLine;
    void Start()
    {
        var a = Car.transform.position.x;
        startDis = finishLine.position.x - a;
        slider.maxValue = startDis;
        slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var a = Car.transform.position.x;
        currentDistance = startDis - (finishLine.position.x - a);
        slider.value = currentDistance;
    }
}
