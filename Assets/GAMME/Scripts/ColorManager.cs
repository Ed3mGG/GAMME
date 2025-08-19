using System.Collections;
using DG.Tweening;
using UnityEngine;

//This script manages the color of the Pot de peinture
//It displays the menu whenever a Pot de peinture is spawned
//It changes the color of the object touching the Peinture to the selected color 
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
