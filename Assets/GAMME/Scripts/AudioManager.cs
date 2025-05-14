using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Add your soundlist here.")]
    public AudioClip[] musicRepertory;

    [SerializeField]
    [Tooltip("Name of the musics.")]
    public String[] musicText;



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
       /* for (int i = 0; i < musicRepertory.Length; i++)
        {
            musicRepertory[i].LoadAudioData();
        }
       */
    }

    public void MenuVisibility()
    {
        if (!audioMenu.activeSelf)
            audioMenu.SetActive(true);
        else audioMenu.SetActive(false);
            
    }

    

}
