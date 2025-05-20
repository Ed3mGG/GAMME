using DG.Tweening;
using TMPro;
using UnityEngine;

public class FollowingObject : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Object to follow.")]
    public Transform m_Target;

    [SerializeField]
    [Tooltip("Offset of target.")]
     Vector3 m_TargetOffset = Vector3.forward;

    bool m_IgnoreX, m_IgnoreZ;
    Vector3 m_TargetPosition;

    [SerializeField]
    [Tooltip("Camera")]
    private Camera m_Camera;

    [SerializeField]
    GameObject m_thisGameObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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

            newTransform.rotation = targetRotation;
            m_TargetPosition = m_Target.position + newTransform.TransformVector(m_TargetOffset);
            transform.position = m_TargetPosition;

            if ((transform.position - m_Camera.transform.position).sqrMagnitude <= 1)
            {
                m_thisGameObject.transform.DOScale(1, -1);
                m_TargetOffset.y = 2;
            } 
            else if ((transform.position - m_Camera.transform.position).sqrMagnitude >= 6)
            {
                m_thisGameObject.transform.DOScale(3, -1);
                m_TargetOffset.y = 4;
            }
            else if ((transform.position - m_Camera.transform.position).sqrMagnitude >= 12)
            {
                m_thisGameObject.transform.DOScale(5, -1);
                m_TargetOffset.y = 8;
            }
        }  
    }
}
