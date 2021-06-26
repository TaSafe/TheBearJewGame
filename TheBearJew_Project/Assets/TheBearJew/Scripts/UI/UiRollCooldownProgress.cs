using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiRollCooldownProgress : MonoBehaviour
{
    private Slider _slider;

    void Awake() => _slider = GetComponent<Slider>();

    void Update()
    {
        transform.position = new Vector3(
            Player.Instance.transform.position.x, 
            transform.position.y,
            Player.Instance.transform.position.z);
    }
    public void SetMaxValue(float max) => _slider.maxValue = max;
    public void SlideValue(float value) => _slider.value = value;
}
