using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour
{
    [SerializeField]
    Button m_virtualRoomButton, m_playgroundAR, m_menu;

    public void LoadVirtualRoom()
    {
        if (m_virtualRoomButton != null)
        {
            SceneManager.LoadScene("VirtualRoom");
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
}
