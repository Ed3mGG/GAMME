using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Templates.MR;

namespace GAMME.Scripts
{
    public class TutorialManager : MonoBehaviour
    {
        public static TutorialManager Instance;
    
        [SerializeField] ObjectSpawner m_Spawner;
        [SerializeField] private TMPro.TMP_Text m_ObjectCountText;
        [SerializeField] private GameObject m_ObjectCount;
        [SerializeField] private GameObject m_NextTutorialButton;
    
        [SerializeField] private int m_ObjectSpawnedIndex;
    
        [SerializeField]
        ARFeatureController m_FeatureController;

        private bool functionCalled = false;
    
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }
    
        private void Start()
        {
            m_NextTutorialButton.SetActive(false); 
            m_ObjectCountText.SetText(m_ObjectSpawnedIndex.ToString());
        
            if (m_FeatureController == null)
            {
#if UNITY_2023_1_OR_NEWER
                m_FeatureController = FindAnyObjectByType<ARFeatureController>();
#else
            m_FeatureController = FindObjectOfType<ARFeatureController>();
#endif
            }
        
            m_FeatureController.TogglePlanes(true);
            m_FeatureController.TogglePlaneVisualization(false);
        }

        private void Update()
        {
            if (!functionCalled)
            {
                if (m_Spawner.objectHasBeenSpawned)
                {
                    functionCalled = true;
            
                    SpawnMinus();
                }
            
            }
        }

        public void isTutorialCompleted()
        {
            if (m_ObjectSpawnedIndex <= 0)
            {
                SceneManager.LoadScene("Tutoriel 3");
            }
        }

        public void ObjectToSpawnCount()
        {
            if (m_ObjectSpawnedIndex <= 0)
            {
                m_ObjectCount.gameObject.SetActive(false);
                m_NextTutorialButton.SetActive(true); 
            }
        }

        public void SpawnMinus()
        {
            m_ObjectSpawnedIndex = - 1;
            if (m_ObjectCount.gameObject.activeSelf)
            {
                m_ObjectCountText.SetText(m_ObjectSpawnedIndex.ToString());
            }
            ObjectToSpawnCount();

            //functionCalled = false;
        }
    }
}
