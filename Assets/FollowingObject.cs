using UnityEngine;

public class FollowingObject : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Object to follow.")]
    public Transform m_Target;

    [SerializeField]
    [Tooltip("Offset of target.")]
     Vector3 m_TargetOffset = Vector3.forward;

    bool m_IgnoreX;
    Vector3 m_TargetPosition;

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
                targetEuler.z
            );

            newTransform.rotation = targetRotation;
            m_TargetPosition = m_Target.position + newTransform.TransformVector(m_TargetOffset);
            transform.position = m_TargetPosition;
        }  
    }
}
