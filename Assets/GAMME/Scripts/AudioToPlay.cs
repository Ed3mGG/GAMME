using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

public class AudioToPlay : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This is our reference to the gameobject that contain the script required.")]
    AudioManager m_audioManager;


    AudioSource m_audioSource;

    [SerializeField]
    [Tooltip("Music title")]
    TMP_Text m_musicName;

    [SerializeField]
    [Tooltip("Play Pause Text")]
    TMP_Text m_PlayPause;

    [SerializeField]
    [Tooltip("Play Pause Button")]
    Button m_PlayPauseButton;


    [SerializeField]
    [Tooltip("Slider that controls the volume")]
    Slider m_Slider;

    [SerializeField] 
    Toggle m_Audio3DToggle;


    [SerializeField]
    [Tooltip("Mixer that controls the volume")]
    AudioMixer m_Mixer;

    /*[SerializeField]
    GameObject audioMenu;*/

    AudioClip[] m_audioClip;
    String[] m_audioNames;

    [SerializeField]
    [Tooltip("Insert the number that correspond that which music will be played.")]
    public int m_music;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*audioMenu.GetComponent<FollowingObject>();
        audioMenu.transform.parent = transform;*/

        // Get volume from Players pref if exist and set 3D Audio to true 
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
        


        m_audioSource = GetComponent<AudioSource>();
        if (m_audioManager != null)
        {
            for (int i = 0; i < m_audioManager.GetComponent<AudioManager>().musicRepertory.Length; i++)
            {
                m_audioClip = m_audioManager.GetComponent<AudioManager>().musicRepertory;
            }
 
            m_audioSource.clip = m_audioClip[m_music];
            

            for (int i = 0; i < m_audioManager.GetComponent<AudioManager>().musicText.Length; i++)
            {
                m_audioNames = m_audioManager.GetComponent<AudioManager>().musicText;
            }
            
            TitleMusic();
            SetAudio3D();
            ChangeButtonColor();
        }


    }

   public void PlayorPauseMusic()
    {
        if (m_audioSource.isPlaying)
        {
            m_audioSource.Pause();
            ChangeButtonColor();
        }
        else 
        {
            m_audioSource.Play();
            ChangeButtonColor();
        } 
    }

    private void ChangeButtonColor()
    {
        if (!m_audioSource.isPlaying)
        {
            ColorBlock colorBlock = m_PlayPauseButton.colors;
            colorBlock.selectedColor = new Color(0, 1, 0);
            colorBlock.highlightedColor = new Color(0, 1, 0);
            colorBlock.normalColor = new Color(0, 1, 0);
            m_PlayPauseButton.colors = colorBlock;
            m_PlayPause.text = "Jouer";
        }
        else
        {
            ColorBlock colorBlock2 = m_PlayPauseButton.colors;
            colorBlock2.selectedColor = new Color(1, 0, 0);
            colorBlock2.highlightedColor = new Color(1, 0, 0);
            colorBlock2.normalColor = new Color(1, 0, 0);
            m_PlayPauseButton.colors = colorBlock2;
            m_PlayPause.text = "Pause";
        }
    }

    public void NextMusic()
    {
        m_audioSource.Stop();
        m_music += 1;
        if (m_music > m_audioClip.Length - 1)
        {
            m_music = 0; 
            m_audioSource.clip = m_audioClip[m_music];
            m_audioSource.Play();
            TitleMusic();
        }
        else
        {
            m_audioSource.clip = m_audioClip[m_music];
            m_audioSource.Play();
            TitleMusic();
        }
        ChangeButtonColor();
    }

    public void PreviousMusic()
    {
        m_audioSource.Stop();
        m_music -= 1;
        if (m_music < 0)
        {
            m_music = m_audioClip.Length - 1;
            m_audioSource.clip = m_audioClip[m_music];
            m_audioSource.Play();
            TitleMusic();
            
        }
        else
        {
            m_audioSource.clip = m_audioClip[m_music];
            m_audioSource.Play();
            TitleMusic();
        }
        ChangeButtonColor();
    }

    public void TitleMusic()
    {

        m_musicName.text = m_audioNames[m_music].ToString();

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
        SetVolume(m_Slider.value);
    }

    public void RefreshSlider(float m_volume)
    {
        m_Slider.value = m_volume;
    }

    public void Audio3D()
    {
        if (m_audioSource.spatialBlend == 1)
        {
            m_audioSource.spatialBlend = 0;
            m_Audio3DToggle.isOn = false;
            
        }
            
        else if(m_audioSource.spatialBlend == 0)
        {
            m_audioSource.spatialBlend = 1;
            m_Audio3DToggle.isOn = true; 
            
        }
            
    }

    private void SetAudio3D ()
    {
        m_Audio3DToggle.isOn = true;
        m_audioSource.spatialBlend = 1;
    }
    
}
