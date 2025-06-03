using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{

    GameObject m_Potdepeinture;
    Material m_Material;

    Image _image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _image = GetComponent<Image>();
    }
    public void ColorSelected()
    {
        if (m_Potdepeinture != null)
        {
            m_Potdepeinture.GetComponent<MeshRenderer>().material.DOColor(_image.color, -1);
            m_Material.SetColor("Color_Texture", _image.color);
        }
    }
}
