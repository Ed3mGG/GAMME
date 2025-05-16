using DG.Tweening;
using UnityEngine;

public class AmbianceManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_AmbianceReference;

    [SerializeField]
    Color m_colorSelected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<MeshRenderer>().material.DOColor(m_colorSelected, -1);
    }

    public void AmbiancePicker()
    {
        if (m_AmbianceReference != null)
        {
            m_colorSelected = GetComponent<MeshRenderer>().material.color;
            m_AmbianceReference.GetComponent<MeshRenderer>().material.DOColor(m_colorSelected, -1);
        }
    }
}
