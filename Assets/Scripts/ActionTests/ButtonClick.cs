using TMPro;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buttonText;
    
    private int _clickCount;

    public void OnClick()
    {
        _clickCount++;
        _buttonText.text = _clickCount.ToString();
    }
}