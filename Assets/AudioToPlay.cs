using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class AudioToPlay : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This is our reference to the gameobject that contain the script required.")]
    AudioManager audioManager;

    AudioSource audioSource;

    /*[SerializeField]
    GameObject audioMenu;*/

    AudioClip audioClip;

    [SerializeField]
    [Tooltip("Insert the number that correspond that which music will be played.")]
    public int m_music;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*audioMenu.GetComponent<FollowingObject>();
        audioMenu.transform.parent = transform;*/

        audioSource = GetComponent<AudioSource>();
        if (audioManager != null)
        {
            audioClip = audioManager.GetComponent<AudioManager>().music[m_music];
            audioSource.clip = audioClip;
        }
            
    }

   public void PlayorPauseMusic()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        else audioSource.Play();
    }

}
