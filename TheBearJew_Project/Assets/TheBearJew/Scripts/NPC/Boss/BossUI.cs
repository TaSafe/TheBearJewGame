using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public static BossUI Instance { get; private set; }

    private Slider _lifeBarSlider;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _lifeBarSlider = GetComponent<Slider>();
    }

    public Color LifeBarGetDefaultColor() => _lifeBarSlider.GetComponent<Image>().color;
    public float LifeBarGetCurrentValue() => _lifeBarSlider.value;
    public void LifeBarSetMaxValue(float value) => _lifeBarSlider.value = _lifeBarSlider.maxValue = value;
    public void LifeBarSetValue(float value) => _lifeBarSlider.value = value;
    public void LifeBarSetColor(Color color) => _lifeBarSlider.fillRect.GetComponent<Image>().color = color;
    public void LifeBarSetBackgroundColor(Color color) => _lifeBarSlider.GetComponent<Image>().color = color;
}
