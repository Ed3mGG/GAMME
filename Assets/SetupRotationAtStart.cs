using UnityEngine;

public class SetupRotationAtStart : MonoBehaviour
{

    [SerializeField] private Camera m_Camera;
    [SerializeField] private Vector3 m_Rotation;
    [SerializeField] private Vector3 m_Setup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (m_Camera != null)
        {
            //m_SetupTransform.position = m_Camera.transform.position;
            
        }
    }
}
