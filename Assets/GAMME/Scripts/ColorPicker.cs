using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    [Header("Color")]
    [SerializeField]
    GameObject m_Peinture;
    [SerializeField]
    GameObject m_ImageToColor;

    [Header("Color Menu")]
    [SerializeField]
    private GameObject _ColorMenu;

    private Material _potDePeinture;

    Image _button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _button = GetComponent<Image>();
        _potDePeinture = m_Peinture.GetComponent<MeshRenderer>().material;
    }
    public void ColorSelected()
    {
        if (m_Peinture != null)
        {
            _potDePeinture.SetColor("_Color", _button.color);
            _potDePeinture.DOColor(_button.color, -1);
            m_ImageToColor.GetComponent<Image>().DOColor(_button.color, -1);
        }
    }

    public void CloseMenu()
    {
        if (_ColorMenu != null)
        {
            if (_ColorMenu.activeSelf)
                _ColorMenu.SetActive(false);
        }
    }
   
}
