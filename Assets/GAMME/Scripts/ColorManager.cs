using DG.Tweening;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField]
    Color colorSelected;

    /*[SerializeField]
    private GameObject item;*/
    void Start()
    {
        GetComponent<MeshRenderer>().material.DOColor(colorSelected, -1);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Color"))
        {
            if (other.gameObject.GetComponent<MeshRenderer>().material != null)
            {
                other.gameObject.GetComponent<MeshRenderer>().material.DOColor(colorSelected, -1);
            }
        }
        
    }

    /*public void ChangeColor()
    {
        item.GetComponent<MeshRenderer>().material.DOColor(colorSelected, -1);
    }*/

    
}
