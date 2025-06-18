using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using TMPro;
using LazyFollow = UnityEngine.XR.Interaction.Toolkit.UI.LazyFollow;

public struct Goal
{
        public StepManagerUI.OnboardingGoals CurrentGoal;
        public bool Completed;

        public Goal(StepManagerUI.OnboardingGoals goal)
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
    ARFeatureController m_FeatureController;

    [SerializeField]
    List<Step> m_StepList = new List<Step>();

    int m_1stGoalInt, m_2ndGoalInt, m_3rdGoalInt, m_endGoalInt;


    Queue<Goal> m_OnboardingGoals;
    Goal m_CurrentGoal;
    
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

        m_OnboardingGoals = new Queue<Goal>();
        var firstGoal = new Goal(OnboardingGoals.Empty);
        var secondGoal = new Goal(OnboardingGoals.Passthrough);
        var thirdGoal = new Goal(OnboardingGoals.Boundingboxes);
        var endGoal = new Goal(OnboardingGoals.Empty);

        m_firstGoalInt = 0;
        m_secondGoalInt = 0;
        m_thirdGoalInt = 0;
        m_endGoalInt = 0;

        m_OnboardingGoals.Enqueue(firstGoal);
        m_OnboardingGoals.Enqueue(secondGoal);
        m_OnboardingGoals.Enqueue(thirdGoal);
        m_OnboardingGoals.Enqueue(endGoal);

        m_CurrentGoal = m_OnboardingGoals.Dequeue();


        if (m_FadeMaterial != null)
        {
            m_FadeMaterial.FadeSkybox(false);
            m_FadeMaterial.FadeAmbiance(false);

            if (m_PassthroughToggle != null)
                m_PassthroughToggle.isOn = false;

            if (m_AmbianceToggle != null)
                m_AmbianceToggle.isOn = false;
        }
        
         ///       ----------------------------------------------       \\\
        ///       This section sets up the button if the goal is reached \\\

        if (m_CurrentGoal.CurrentGoal == OnboardingGoals.Empty)
        {
            if (ContinueButton.onClick.AddListener())
            {
                m_firstGoalInt ++;
            }
        }
        else if (m_CurrentGoal.CurrentGoal == OnboardingGoals.Passthrough)
        {
            if (m_PassthroughToggle.GetComponent<Toggle>().onValueChanged.AddListener())
            {
                m_secondGoalInt ++; 
            }
            else if (m_AmbianceToggle.GetComponent<Toggle>().onValueChanged.AddListener())
            {
                m_secondGoalInt ++;
            }
            if (m_secondGoalInt > 1)
            {
                ContinueButton.SetActive(true);
            }
        }
        else if (m_CurrentGoal.CurrentGoal == OnboardingGoals.Boundingboxes)
        {
            if (m_BoundingBoxes.GetComponent<Toggle>().onValueChanged.AddListener())
            {
                m_thirdGoalInt ++;
            }
            if (m_thirdGoalInt > 1)
            {
                ContinueButton.SetActive(true);
            }
        }
    }

    public void NextStep()
    {
        if (m_CurrentGoal.CurrentGoal == OnboardingGoals.Empty)
        {
            if (m_firstGoalInt > 0)
            {
                CompleteGoal();
            }
        }
        else if (m_CurrentGoal.CurrentGoal == OnboardingGoals.Passthrough)
        {
            if (m_secondGoalInt > 1)
            {
                CompleteGoal();
            }
        }
        else if (m_CurrentGoal.CurrentGoal == OnboardingGoals.Boundingboxes)
        {
            if (m_thirdGoalInt > 1)
            {
                CompleteGoal();
            }
        }
        else
        {
                SceneManager.LoadScene("Tutoriel 2");
        }
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
                ContinueButton.SetActive(false);

                if (m_FadeMaterial != null)
                    m_FadeMaterial.FadeSkybox(false);

                if (m_PassthroughToggle != null)
                    m_PassthroughToggle.isOn = false;     
            }
            else if (m_CurrentGoal.CurrentGoal == OnboardingGoals.Boundingboxes)
            {
                m_firstGoalInt = 0;
                m_secondGoalInt = 0;
                m_FeatureController.ToggleBoundingBoxes(false);
                m_FeatureController.TogglePlanes(false);
                ContinueButton.SetActive(false);
                
                if (m_FadeMaterial != null)
                    m_FadeMaterial.FadeSkybox(false);

                if (m_PassthroughToggle != null)
                    m_PassthroughToggle.isOn = false;  
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

            if (ContinueButton.onClick.AddListener())
            {
                SceneManager.LoadScene("Tutoriel 2");
            }
        }
}

