using DG.Tweening;
using UnityEngine;

public class AmbianceManagerLayout : MonoBehaviour
{
    [SerializeField]
    Material m_MaterialReference;

    [SerializeField]
    Color m_colorSelected;

    [SerializeField]
    Material[] m_FontaineMaterials;
    // 0 is Bubbles, 1 is VitreFresnel, 2 is InnerSpotlight_Ambiance, 3 is OuterSpotlight_Ambiance



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        GetComponent<MeshRenderer>().material.DOColor(m_colorSelected, -1);
        FontaineAmbianceColorAtStart();
    }

    public void AmbiancePicker()
    {
        if (m_MaterialReference != null)
        {

            m_MaterialReference.GetColor("_Glow_Color");
            m_colorSelected = GetComponent<MeshRenderer>().material.color;
            m_MaterialReference.SetColor("_Glow_Color", m_colorSelected);
        }
    }

    public void FontaineAmbianceColorAtStart()
    {
        m_FontaineMaterials[0].SetColor("_Fresnel_Color", Color.white);
        m_FontaineMaterials[1].SetColor("_Fresnel_Color", Color.white);
        m_FontaineMaterials[2].SetColor("_Base_Color", Color.white);
        //m_FontaineMaterials[2].SetFloat("_Emissive_Intensity", 3);
        m_FontaineMaterials[3].SetColor("_Base_Color", Color.white);
        //m_FontaineMaterials[3].SetFloat("_Emissive_Intensity", 1);

    }

    public void FontaineAmbianceColor()
    {
        m_FontaineMaterials[0].SetColor("_Fresnel_Color", m_colorSelected);
        m_FontaineMaterials[1].SetColor("_Fresnel_Color", m_colorSelected);
        m_FontaineMaterials[2].SetColor("_Base_Color", m_colorSelected);
        //m_FontaineMaterials[2].SetFloat("_Emissive_Intensity", 3);
        m_FontaineMaterials[3].SetColor("_Base_Color", m_colorSelected);
        //m_FontaineMaterials[3].SetFloat("_Emissive_Intensity", 1);

    }

}
