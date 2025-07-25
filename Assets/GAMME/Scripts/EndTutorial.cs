using System;
using System.Collections;
using UnityEngine;

public class EndTutorial : MonoBehaviour
{
    Coroutine coroutine;
    
    [Header("Menu to display")]
    [SerializeField] private GameObject m_Menu;
    [SerializeField] private float m_DelayBeforeDisplay;

    private void Start()
    {
        m_Menu.SetActive(false);
        StartCoroutine(DisplayMenu(m_DelayBeforeDisplay));
    }

    private IEnumerator DisplayMenu(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        m_Menu.SetActive(true);
    }
}
