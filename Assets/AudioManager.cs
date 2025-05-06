using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Add your soundlist here.")]
    public AudioClip[] music;

    [SerializeField]
    [Tooltip("This is the menu that will appear when you poke an object.")]
    GameObject m_audioMenu;
    public GameObject audioMenu
    {
        get => m_audioMenu;
        set => m_audioMenu = value;
    }

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

       
    }

    public void MenuVisibility()
    {
        if (!audioMenu.activeSelf)
            audioMenu.SetActive(true);
        else audioMenu.SetActive(false);
            
    }

    

}
