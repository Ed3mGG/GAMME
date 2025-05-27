using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;


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

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
        TitleMusic();
        SetAudio3D();
        ChangeButtonColor();
    }

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

    public void TitleMusic()
    {

        m_MusicNameText.SetText(m_MusicDataList[m_MusicIndex].musicName);

    }

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

    public void SetVolumeFromSlider()
    {
        SetVolume(m_SliderVolume.value);
    }

    public void RefreshSlider(float m_volume)
    {
        m_SliderVolume.value = m_volume;
    }

    public void Audio3D()
    {
        if (m_AudioSource.spatialBlend == 1)
        {
            m_AudioSource.spatialBlend = 0;
            m_Audio3DToggle.isOn = false;

        }

        else if (m_AudioSource.spatialBlend == 0)
        {
            m_AudioSource.spatialBlend = 1;
            m_Audio3DToggle.isOn = true;

        }

    }

    private void SetAudio3D()
    {
        m_Audio3DToggle.isOn = true;
        m_AudioSource.spatialBlend = 1;
    }


}
