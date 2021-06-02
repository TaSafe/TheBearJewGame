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

    public void LifeBarSetMaxValue(float value) => _lifeBarSlider.value = _lifeBarSlider.maxValue = value;
    public void LifeBarSetValue(float value) => _lifeBarSlider.value = value;

}
