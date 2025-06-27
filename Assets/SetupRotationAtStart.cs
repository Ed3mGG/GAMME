using System.Collections;
using UnityEngine;

public class SetupRotationAtStart : MonoBehaviour
{

    [SerializeField] private Camera _Camera;
    [SerializeField] private GameObject _Setup;
    [SerializeField] private float _delayBeforeRotation;
    
    private Vector3 _SetupPosition;
    
    Coroutine coroutine;
    
    void Start()
    {
        _SetupPosition =  _Setup.transform.position;
        if (_Camera != null)
        {
            StartCoroutine(DelayBeforeRotation(_delayBeforeRotation));
        }
    }
//This script rotates the GameObject "Setup" to it faces the direction of the MainCamera after the delay (can be set in the Editor "Delay Before Rotation")
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
