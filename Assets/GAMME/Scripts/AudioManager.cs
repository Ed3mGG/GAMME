using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

//This script is the Audio manager
//It is where you're managing the sounds that are played in the Enceinte
//It also manages the Audio menu

//This structure holds the informations of all musics you have
[Serializable]
public struct MusicData
{
    public string musicName;
    public AudioClip audioClip;
}


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Musics")]
    [SerializeField]
    private List<MusicData> m_MusicDataList = new List<MusicData>();
    
    private AudioSource m_AudioSource;
    private int m_MusicIndex = 0;


    [Header("UI")]
    [SerializeField]
    [Tooltip("This is the menu that will appear when you poke an object.")]
    GameObject m_audioMenu;
    [SerializeField]
    [Tooltip("Music title")]
    TMP_Text m_MusicNameText;
    [SerializeField]
    [Tooltip("Play Pause Text")]
    TMP_Text m_PlayPauseText;
    [SerializeField]
    [Tooltip("Play Pause Button")]
    Button m_PlayPauseButton;
    [SerializeField]
    [Tooltip("Slider that controls the volume")]
    Slider m_SliderVolume;
    [SerializeField]
    Toggle m_Audio3DToggle;
    [SerializeField]
    [Tooltip("Mixer that controls the volume")]
    AudioMixer m_Mixer;

    

    public GameObject audioMenu
    {
        get => m_audioMenu;
        set => m_audioMenu = value;
    }

    //Instantiating this component to acces it anywhere and making sure there is only one Audiomanager
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    
    //Setting the volume to max and calling the other methods for the audio menu
    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
        TitleMusic();
        SetAudio3D();
        ChangeButtonColor();
    }

    //This method gets the audiosource of the enceinte in the scene and sets the clip of the selected music
    //It also activates or deactivates the menu depending on whether the user selects or deselects the enceinte
    public void MenuVisibility()
    {
        if (m_AudioSource == null)
        {
            m_AudioSource = GameObject.FindGameObjectWithTag("MusicPlayer")?.GetComponent<AudioSource>();
            m_AudioSource.clip = m_MusicDataList[m_MusicIndex].audioClip;
        }
        
        if (!audioMenu.activeSelf)
            audioMenu.SetActive(true);
        else audioMenu.SetActive(false);      
    }

    //Playing or pausing the menu
    public void PlayorPauseMusic()
    {
        if (m_AudioSource.isPlaying)
        {
            m_AudioSource.Pause();
            ChangeButtonColor();
        }
        else
        {
            m_AudioSource.Play();
            ChangeButtonColor();
        }
    }

    //This method changes the color and text of the "PlayPauseButton"
    private void ChangeButtonColor()
    {
        if (!m_AudioSource.isPlaying)
        {
            ColorBlock colorBlock = m_PlayPauseButton.colors;
            colorBlock.selectedColor = new Color(0, 1, 0);
            colorBlock.highlightedColor = new Color(0, 1, 0);
            colorBlock.normalColor = new Color(0, 1, 0);
            m_PlayPauseButton.colors = colorBlock;
            m_PlayPauseText.SetText("Jouer");
        }
        else
        {
            ColorBlock colorBlock2 = m_PlayPauseButton.colors;
            colorBlock2.selectedColor = new Color(1, 0, 0);
            colorBlock2.highlightedColor = new Color(1, 0, 0);
            colorBlock2.normalColor = new Color(1, 0, 0);
            m_PlayPauseButton.colors = colorBlock2;
            m_PlayPauseText.SetText("Pause");
        }
    }

    //If the next button is pressed, the +1 music is selected if it exists and played
    //Or it goes to the 1st music of the repertory and plays it
    public void NextMusic()
    {
        m_AudioSource.Stop();
        m_MusicIndex += 1;
        if (m_MusicIndex > m_MusicDataList.Count - 1)
        {
            m_MusicIndex = 0;
            m_AudioSource.clip = m_MusicDataList[m_MusicIndex].audioClip;
            m_AudioSource.Play();
            TitleMusic();
        }
        else
        {
            m_AudioSource.clip = m_MusicDataList[m_MusicIndex].audioClip;
            m_AudioSource.Play();
            TitleMusic();
        }
        ChangeButtonColor();
    }

    ////If the next button is pressed, the -1 music is selected if it exists and played
    //Or it goes to the last music of the repertory and plays it
    public void PreviousMusic()
    {
        m_AudioSource.Stop();
        m_MusicIndex -= 1;
        if (m_MusicIndex < 0)
        {
            m_MusicIndex = m_MusicDataList.Count - 1;
            m_AudioSource.clip = m_MusicDataList[m_MusicIndex].audioClip;
            m_AudioSource.Play();
            TitleMusic();

        }
        else
        {
            m_AudioSource.clip = m_MusicDataList[m_MusicIndex].audioClip;
            m_AudioSource.Play();
            TitleMusic();
        }
        ChangeButtonColor();
    }

    //This method simply displays the name of the currently selected music
    public void TitleMusic()
    {

        m_MusicNameText.SetText(m_MusicDataList[m_MusicIndex].musicName);

    }

    //This method gets the value of the slider, sends it to the audiomixer and saves the value to the PlayerPrefs
    public void SetVolume(float m_volume)
    {
        if (m_volume < 1)
        {
            m_volume = .001f;
        }

        RefreshSlider(m_volume);
        PlayerPrefs.SetFloat("SavedMasterVolume", m_volume);
        m_Mixer.SetFloat("MasterVolume", Mathf.Log10(m_volume / 100) * 20f);
    }

    //This method 
    public void SetVolumeFromSlider()
    {
        SetVolume(m_SliderVolume.value);
    }

    public void RefreshSlider(float m_volume)
    {
        m_SliderVolume.value = m_volume;
    }

    //This method enables the 3D audio
    //which lets the user manipulates the spatialization of the music through the Enceinte
    public void Audio3D()
    {
        if (m_AudioSource.spatialBlend == 1)
        {
            m_AudioSource.spatialBlend = 0;
            //m_Audio3DToggle.isOn = false;

        }

        else if (m_AudioSource.spatialBlend == 0)
        {
            m_AudioSource.spatialBlend = 1;
            //m_Audio3DToggle.isOn = true;

        }

    }

    //This method enables the 3D audio
    //which lets the user manipulates the spatialization of the music through the Enceinte
    //Sets the toggle to true
    private void SetAudio3D()
    {
        m_Audio3DToggle.isOn = true;
        m_AudioSource.spatialBlend = 1;
    }


}
