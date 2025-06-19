using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Templates.MR;
using LazyFollow = UnityEngine.XR.Interaction.Toolkit.UI.LazyFollow;

public struct GoalTutorials
{
    public StepManagerUI.OnboardingGoals CurrentGoal;
    public bool Completed;

    public GoalTutorials(StepManagerUI.OnboardingGoals goal)
    {
        CurrentGoal = goal;
        Completed = false;
    }
}
    public class StepManagerUI : MonoBehaviour
    {   
        public enum OnboardingGoals
        {
            Empty,
            Passthrough,
            Boundingboxes,
        }


        [Serializable]
            class Step
            {
                [SerializeField]
                public GameObject stepObject;

                [SerializeField]
                public string buttonText;
            }
        [SerializeField]
        List<Step> m_StepList = new List<Step>();
        
        [SerializeField]
        public TextMeshProUGUI m_StepButtonTextField;
        
        [SerializeField]
        GameObject m_TutorielUIParent;

        [SerializeField]
        FadeMaterial m_FadeMaterial;

        [SerializeField]
        Toggle m_PassthroughToggle;
        
        [SerializeField]
        Toggle m_AmbianceToggle;

        [SerializeField]
        Toggle m_BoundingBoxes;

        [SerializeField]
        Button ContinueButton;
        
        [SerializeField]
        LazyFollow m_GoalPanelLazyFollow;

        [SerializeField]
        ARFeatureController m_FeatureController;

        int m_firstGoalInt, m_secondGoalInt, m_thirdGoalInt, m_secondGoalIntBis;


        Queue<GoalTutorials> m_OnboardingGoals;
        bool m_AllGoalsFinished;
        GoalTutorials m_CurrentGoal;
        int m_CurrentGoalIndex = 0;
        
        private void Start()
        {
                if (m_FeatureController == null)
        {
#if UNITY_2023_1_OR_NEWER
                    m_FeatureController = FindAnyObjectByType<ARFeatureController>();
#else
            m_FeatureController = FindObjectOfType<ARFeatureController>();
#endif
        }
            m_OnboardingGoals = new Queue<GoalTutorials>();
            var firstGoal = new GoalTutorials(OnboardingGoals.Empty);
            var secondGoal = new GoalTutorials(OnboardingGoals.Passthrough);
            var thirdGoal = new GoalTutorials(OnboardingGoals.Boundingboxes);
            var endGoal = new GoalTutorials(OnboardingGoals.Empty);

            m_firstGoalInt = 0;
            m_secondGoalInt = 0;
            m_secondGoalIntBis = 0;
            m_thirdGoalInt = 0;
            //m_endGoalInt = 0;

            m_OnboardingGoals.Enqueue(firstGoal);
            m_OnboardingGoals.Enqueue(secondGoal);
            m_OnboardingGoals.Enqueue(thirdGoal);
            m_OnboardingGoals.Enqueue(endGoal);

            m_CurrentGoal = m_OnboardingGoals.Dequeue();


            /*if (m_FadeMaterial != null)
            {
                m_FadeMaterial.FadeSkybox(false);
                m_FadeMaterial.FadeAmbiance(false);
            }*/
        }

        public void FirstGoal()
        {
            if (m_CurrentGoal.CurrentGoal == OnboardingGoals.Empty)
            {
                if (m_firstGoalInt < 1)
                {
                    m_firstGoalInt++;
                    isGoalCompleted();
                }
            }
        }

        public void SecondGoal()
        {
            if (m_CurrentGoal.CurrentGoal == OnboardingGoals.Passthrough)
            {
                if (m_secondGoalInt < 1)
                {
                    m_secondGoalInt++;
                    isGoalCompleted();
                }
            }
        }
        
        public void SecondGoalBis()
        {
            if (m_CurrentGoal.CurrentGoal == OnboardingGoals.Passthrough)
            {
                if (m_secondGoalIntBis < 1)
                {
                    m_secondGoalIntBis++;
                    isGoalCompleted();
                }
            }
        }

        public void SecondGoalCompletion()
        {
            if (m_CurrentGoal.CurrentGoal == OnboardingGoals.Passthrough)
            {
                if (m_secondGoalIntBis > 0 && m_secondGoalInt > 0)
                {
                    isGoalCompleted();
                }
            }
        }

        public void ThirdGoal()
        {
            if (m_CurrentGoal.CurrentGoal == OnboardingGoals.Boundingboxes)
            {
                if (m_thirdGoalInt < 2)
                {
                    m_thirdGoalInt++;
                    isGoalCompleted();
                }
            }
        }
        
        void LoadNextTutorial()
        {
            SceneManager.LoadScene("Tutoriel 2");
        }

        void Update()
        {
            if (!m_AllGoalsFinished)
            {
                ProcessGoals();
            }
        }

        void ProcessGoals()
        {
            if (!m_CurrentGoal.Completed)
            {
                switch (m_CurrentGoal.CurrentGoal)
                {
                    case OnboardingGoals.Empty:
                        m_GoalPanelLazyFollow.positionFollowMode = LazyFollow.PositionFollowMode.Follow;
                        break;
                    case OnboardingGoals.Passthrough:
                        m_GoalPanelLazyFollow.positionFollowMode = LazyFollow.PositionFollowMode.Follow;
                        break;
                    case OnboardingGoals.Boundingboxes:
                        m_GoalPanelLazyFollow.positionFollowMode = LazyFollow.PositionFollowMode.None;
                        break;
                }
            }
        }

        void CompleteGoal()
            {
                m_CurrentGoal.Completed = true;
                m_CurrentGoalIndex++;
                if (m_OnboardingGoals.Count > 0)
                {
                    m_CurrentGoal = m_OnboardingGoals.Dequeue();
                    m_StepList[m_CurrentGoalIndex - 1].stepObject.SetActive(false);
                    m_StepList[m_CurrentGoalIndex].stepObject.SetActive(true);
                    m_StepButtonTextField.text = m_StepList[m_CurrentGoalIndex].buttonText;
                }
                else
                {
                    m_firstGoalInt= 0;
                    m_secondGoalInt = 0;
                    m_thirdGoalInt = 0;
                    m_AllGoalsFinished = true;
                    ForceEndAllGoals();
                }

                if (m_CurrentGoal.CurrentGoal == OnboardingGoals.Passthrough)
                {
                    m_firstGoalInt = 0;
                    m_thirdGoalInt = 0;
                    m_FeatureController.ToggleBoundingBoxes(false);
                    m_FeatureController.TogglePlanes(false);
                    ContinueButton.gameObject.SetActive(false);
                    
                    if (m_PassthroughToggle != null)
                    {
                        m_PassthroughToggle.isOn = true; 
                    }
                    
                }
                else if (m_CurrentGoal.CurrentGoal == OnboardingGoals.Boundingboxes)
                {
                    m_firstGoalInt = 0;
                    m_secondGoalInt = 0;
                    m_FeatureController.ToggleBoundingBoxes(false);
                    m_FeatureController.ToggleBoundingBoxVisualization(false);
                    m_FeatureController.TogglePlanes(false);
                    ContinueButton.gameObject.SetActive(false);

                    if (m_FadeMaterial != null)
                    {
                        m_FadeMaterial.FadeSkybox(true);
                        m_FadeMaterial.FadeAmbiance(true);
                        
                        if (m_PassthroughToggle != null)
                        {
                            m_PassthroughToggle.isOn = true; 
                        }

                        if (m_AmbianceToggle != null)
                        {
                            m_AmbianceToggle.isOn = true;
                        } 
                    }
                }
            }

        public void BoxVisualization()
        {
            if (!m_FeatureController.BoundingBoxManager.isActiveAndEnabled)
            {
                m_FeatureController.ToggleBoundingBoxes(true);
                m_FeatureController.ToggleBoundingBoxVisualization(true);
            }
            else
            {
                m_FeatureController.ToggleBoundingBoxes(false);
                m_FeatureController.ToggleBoundingBoxVisualization(false);
            }
        }

            public void ForceCompleteGoal()
            {
                CompleteGoal();
            }

            public void ForceEndAllGoals()
            {
                m_TutorielUIParent.transform.localScale = Vector3.zero;

                if (m_FadeMaterial != null)
                {
                    m_FadeMaterial.FadeSkybox(true);

                    if (m_PassthroughToggle != null)
                        m_PassthroughToggle.isOn = false;
                }

                LoadNextTutorial();
            }

            public void isGoalCompleted()
            {
                ///       ----------------------------------------------       \\\
                ///       This section sets up the button if the goal is reached \\\
                if (m_CurrentGoal.CurrentGoal == OnboardingGoals.Empty)
                {
                    if (m_firstGoalInt > 0)
                    {
                        ContinueButton.gameObject.SetActive(true);
                    }
                }
                else if (m_CurrentGoal.CurrentGoal == OnboardingGoals.Passthrough)
                {
                    if (m_secondGoalInt > 0 && m_secondGoalIntBis > 0)
                    {
                        ContinueButton.gameObject.SetActive(true);
                    }
                }
                else if (m_CurrentGoal.CurrentGoal == OnboardingGoals.Boundingboxes)
                {
                    if (m_thirdGoalInt > 1)
                    {
                        ContinueButton.gameObject.SetActive(true);
                    }
                }
                else
                {
                    LoadNextTutorial();
                }
            }
    }