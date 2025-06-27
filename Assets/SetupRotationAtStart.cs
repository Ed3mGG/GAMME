using System.Collections;
using UnityEngine;

public class SetupRotationAtStart : MonoBehaviour
{

    [SerializeField] private Camera _Camera;
    [SerializeField] private GameObject _Setup;
    [SerializeField] private float _delayBeforeRotation;
    
    private Vector3 _SetupPosition;
    
    Coroutine coroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _SetupPosition =  _Setup.transform.position;
        if (_Camera != null)
        {
            StartCoroutine(DelayBeforeRotation(_delayBeforeRotation));
        }
    }

    private IEnumerator DelayBeforeRotation(float delay)
    {
        yield return new WaitForSeconds(delay);
        _Setup.transform.position = _Camera.transform.position + _Camera.transform.forward;
        var angles = _Setup.transform.eulerAngles;
        angles.y = _Camera.transform.rotation.eulerAngles.y;
        _Setup.transform.eulerAngles = angles;
        var vector3 = _Setup.transform.position;
        vector3.y = _SetupPosition.y;
        _Setup.transform.position = vector3;
    }
}
