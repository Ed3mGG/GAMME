using DG.Tweening;
using UnityEngine;

public class AmbianceManagerLayout : MonoBehaviour
{
    [SerializeField]
    Material m_MaterialReference;

    [SerializeField]
    Color m_colorSelected;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        GetComponent<MeshRenderer>().material.DOColor(m_colorSelected, -1);
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
}
