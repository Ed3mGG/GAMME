using DG.Tweening;
using UnityEngine;

//This scripts manipulates the rotation of the Caroussel textures through DOTween
public class Carousel : MonoBehaviour
{
    [SerializeField]
    float _X, _Y, _Z, _duration;
    void Start()
    {
        transform.DORotate(new Vector3(_X, _Y, _Z), _duration, RotateMode.LocalAxisAdd).SetEase(Ease.InOutSine).SetLoops(-1);
    }
}
