using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This script loads the selected maps
public class MapSelection : MonoBehaviour
{
    [SerializeField]
    Button m_virtualRoomButton, m_playgroundAR, m_menu, m_tutoriel;

    public void LoadVirtualRoom()
    {
        if (m_virtualRoomButton != null)
        {
            SceneManager.LoadScene("AR 2");
        }
    }

    public void LoadPlaygroundAR()
    {
        if (m_playgroundAR != null)
        {
            SceneManager.LoadScene("PlaygroundAR");
        }
    }
    
    public void LoadMenu()
    {
        if (m_menu != null)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void LoadTutoriel()
    {
        if (m_tutoriel != null)
        {
            SceneManager.LoadScene("Tutoriel 1");
        }
    }
    
    public void EndTutoriel()
    {
        if (m_tutoriel != null)
        {
            SceneManager.LoadScene("PlaygroundAR");
        }
    }
}
