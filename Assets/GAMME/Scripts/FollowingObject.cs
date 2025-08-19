using DG.Tweening;
using UnityEngine;

//This script enables the GameObject to follow the selected target
public class FollowingObject : MonoBehaviour
{

    //[SerializeField]
    [Tooltip("Object to follow.")]
    Transform m_Target;

    //[SerializeField]
    [Tooltip("Offset of target.")]
     Vector3 m_TargetOffset = Vector3.forward;

    bool m_IgnoreX, m_IgnoreZ;
    Vector3 m_TargetPosition;

    [SerializeField]
    [Tooltip("Camera")]
    private Camera m_Camera;

    [SerializeField]
    GameObject m_thisGameObject;

    private float instance;
    
    void Start()
    {
        instance = 0;
    }

    //Checking the position and rotation of the target and sets the GO to follow those informations
    void Update()
    {
        if (instance == 0)
        {
            if (GameObject.FindGameObjectWithTag("MusicPlayer") != null)
                m_Target = GameObject.FindGameObjectWithTag("MusicPlayer")?.GetComponent<Transform>();
            instance = 1;
        }
        if (instance == 1)
        {
            if (m_Target != null)
            {
                var targetRotation = m_Target.rotation;
                var newTransform = m_Target;
                var targetEuler = targetRotation.eulerAngles;
                targetRotation = Quaternion.Euler
                (
                    m_IgnoreX ? 0f : targetEuler.x,
                    targetEuler.y,
                    m_IgnoreZ ? 0f : targetEuler.z
                );
                if ((transform.position - m_Camera.transform.position).sqrMagnitude >= 2)
                {
                    m_thisGameObject.transform.DOScale(4, -1);
                    m_TargetOffset.y = 10;
                }
                else
                {
                    m_thisGameObject.transform.DOScale(2, -1);
                    m_TargetOffset.y = 5;
                }

                newTransform.rotation = targetRotation;
                m_TargetPosition = m_Target.position + newTransform.TransformVector(m_TargetOffset);
                transform.position = m_TargetPosition;
            }
        }
    }
}
