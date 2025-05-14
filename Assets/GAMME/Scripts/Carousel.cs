using DG.Tweening;
using UnityEngine;

public class Carousel : MonoBehaviour
{
    [SerializeField]
    float _X, _Y, _Z, _duration;
    void Start()
    {
        transform.DORotate(new Vector3(_X, _Y, _Z), _duration, RotateMode.LocalAxisAdd).SetEase(Ease.InOutSine).SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
