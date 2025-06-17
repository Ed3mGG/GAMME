using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    
    
    [Header("Color Menu")]
    [SerializeField]
    private GameObject _ColorMenu;

    Color colorSelected;


    void Start()
    {
        
        MenuVisibility();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Color"))
        {
            if (other.gameObject.GetComponent<MeshRenderer>().material != null)
            {
                colorSelected = GetComponent<MeshRenderer>().material.GetColor("_Color");
                other.gameObject.GetComponent<MeshRenderer>().material.DOColor(colorSelected, -1);
            }
        }       
    }

    public void MenuVisibility()
    {
        if (_ColorMenu != null)
        {
            if (!_ColorMenu.activeSelf)
                _ColorMenu.SetActive(true);
            else _ColorMenu.SetActive(false);
        }
    }

    
}
