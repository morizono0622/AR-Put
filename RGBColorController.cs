using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RGBColorController : MonoBehaviour
{
    [SerializeField]
    private Slider _redSlider;
    [SerializeField]
    private Slider _greenSlider;
    [SerializeField]
    private Slider _blueSlider;
    [SerializeField]
    private TextMeshProUGUI _redValue;
    [SerializeField]
    private TextMeshProUGUI _greenValue;
    [SerializeField]
    private TextMeshProUGUI _blueValue;
    [SerializeField]
    private Image _colorPreview;

    void Start()
    {
        // スライダーの値を初期化
        UpdateColor();
    }

    public void UpdateColor()
    {
        // スライダーの値を整数としてテキストに表示
        _redValue.text = Mathf.RoundToInt(_redSlider.value).ToString();
        _greenValue.text = Mathf.RoundToInt(_greenSlider.value).ToString();
        _blueValue.text = Mathf.RoundToInt(_blueSlider.value).ToString();

        // スライダーの値から色を作成
        Color color = new Color(_redSlider.value / 255f, _greenSlider.value / 255f, _blueSlider.value / 255f);

        // 色をプレビューに反映
        _colorPreview.color = color;
    }


    public Color GetSelectedColor()
    {
        return new Color(_redSlider.value / 255f, _greenSlider.value / 255f, _blueSlider.value / 255f);
    }
}
