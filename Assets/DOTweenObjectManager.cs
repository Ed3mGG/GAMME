using UnityEngine;
using DG.Tweening;

public class DOTweenObjectManager : MonoBehaviour
{
    [SerializeField]
    Light selectedLight;

    [SerializeField]
    GameObject selectedGameObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // Checking if there is a game object selected, if there is, set it to active and starts the rotation
        if (selectedGameObject != null)
            RotationSystem();

        // Check if there is a light selected, if there is, enables it and starts the lighting sequence
        if (selectedLight != null)
            LightingSystem();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightingSystem()
    {
        if (selectedLight == true)
        {
            // Creating the sequence for the light !! SEQUENCE BLOCKS EVERYTHING THAT COMES AFTER IT !!
            Sequence lightSequence = DOTween.Sequence();

            lightSequence.Append(selectedLight.DOColor(Color.red, 20));
            lightSequence.Append(selectedLight.DOColor(Color.blue, 20));
            lightSequence.Insert(0, selectedLight.DOIntensity(300, 40)); // Insert to override the sequence and starts this line at the beginning ('0')
        }
    }

    public void RotationSystem()
    {
        if (selectedGameObject == true)
        {
            Sequence rotationSequence = DOTween.Sequence();
            //selectedGameObject.transform.DORotate(new Vector3(0, 240, 0), 2, RotateMode.Fast).SetLoops(-1, LoopType.Restart);
            rotationSequence.Append(selectedGameObject.transform.DORotate(new Vector3(0, 340, 0), 2, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo));
        }      
    }
}
