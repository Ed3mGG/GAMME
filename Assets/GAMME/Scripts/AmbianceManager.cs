using System;
using DG.Tweening;
using UnityEngine;

public class AmbianceManager : MonoBehaviour
{
    [SerializeField]
    Color m_colorSelected;

    [Header("BoundingBox and Planes")]
    [SerializeField]
    Material[] m_EnvironmentMaterialReference;
    // 0 is Intersection Glow, 1 is PlaneMaterial, 2 is BoundingBox Cube, 3 is BoundingBox Edge Glow

    [Header("Fontaines")]
    [SerializeField]
    Material[] m_FontaineMaterialsReference;
    // 0 is Bubbles_Ambiance, 1 is VitreFresnel_Ambiance, 2 is InnerSpotlight_Ambiance, 3 is OuterSpotlight_Ambiance
    
    [Header("Others")]
    [SerializeField]
    Material[] m_OtherMaterialReference;
    // 0 is Bordure

    [Header("Background Ambiance")]
    [SerializeField]
    GameObject m_AmbianceReference;
    [SerializeField]
    Boolean m_isBackgroundColored = false;
    
    [Header("SFX Feedback")] 
    [SerializeField] Boolean m_SFXFeedback = false;
        [SerializeField] AudioClip m_SFXFeedbackClip;
        [SerializeField] AudioSource m_SFXFeedbackSource;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (m_AmbianceReference == null)
        {
            m_AmbianceReference = GameObject.FindGameObjectWithTag("AmbianceBackground");
        }
        //GetComponent<MeshRenderer>().material.DOColor(m_colorSelected, -1);
        AmbianceColorAtStart();
    }

    public void AmbiancePicker()
    {
        m_colorSelected = GetComponent<MeshRenderer>().material.color;

        EnvironmentAmbianceColor();
        ObjectsAmbianceColor();
        PlaySFXFeedback();

        if (m_isBackgroundColored)
        {
            BackgroundAmbiance();
        }
    }

    public void AmbianceColorAtStart()
    {
        if (m_FontaineMaterialsReference != null)
        {
            m_FontaineMaterialsReference[0].SetColor("_Fresnel_Color", Color.white);
            m_FontaineMaterialsReference[1].SetColor("_Fresnel_Color", Color.white);
            m_FontaineMaterialsReference[2].SetColor("_Base_Color", Color.white);
            m_FontaineMaterialsReference[3].SetColor("_Base_Color", Color.white);

            m_EnvironmentMaterialReference[1].SetColor("_TexColorTint", Color.white);
            m_EnvironmentMaterialReference[2].SetColor("_Fresnel_Color", Color.white);
            m_EnvironmentMaterialReference[3].SetColor("_Base_Color", Color.white);
            
        }

        if (m_OtherMaterialReference != null)
        {
            m_OtherMaterialReference[0].SetColor("_Base_Color", Color.white);
        }
    }

    public void ObjectsAmbianceColor()
    {
        if (m_FontaineMaterialsReference != null)
        {
            m_FontaineMaterialsReference[0].SetColor("_Fresnel_Color", m_colorSelected);
            //m_FontaineMaterialsReference[0].SetColor("_Base_Color", m_colorSelected);
            m_FontaineMaterialsReference[1].SetColor("_Fresnel_Color", m_colorSelected);
            m_FontaineMaterialsReference[2].SetColor("_Base_Color", m_colorSelected);
            m_FontaineMaterialsReference[3].SetColor("_Base_Color", m_colorSelected);
        }

        if (m_OtherMaterialReference != null)
        {
            m_OtherMaterialReference[0].SetColor("_Base_Color", m_colorSelected);
        }
    }

    public void EnvironmentAmbianceColor()
    {
        if (m_EnvironmentMaterialReference != null)
        {
            m_EnvironmentMaterialReference[0].GetColor("_Glow_Color");
            m_EnvironmentMaterialReference[0].SetColor("_Glow_Color", m_colorSelected);
            m_EnvironmentMaterialReference[1].SetColor("_TexColorTint", m_colorSelected);
            m_EnvironmentMaterialReference[2].SetColor("_Fresnel_Color", m_colorSelected);
            m_EnvironmentMaterialReference[3].SetColor("_Base_Color", m_colorSelected);
        }
    }

    public void BackgroundAmbiance()
    {
        if (m_AmbianceReference != null)
        {
            m_AmbianceReference.GetComponent<MeshRenderer>().material.DOColor(m_colorSelected, -1);
        }
    }

    public void PlaySFXFeedback()
    {
        if (m_SFXFeedback)
        {
            if (m_SFXFeedback != null && m_SFXFeedbackSource != null)
            {
                m_SFXFeedbackSource.PlayOneShot(m_SFXFeedbackClip);
            } 
        }
    }

}
