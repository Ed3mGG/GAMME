using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class MusicPlayer : XRBaseInteractable
{

    AudioManager m_AudioManager = AudioManager.Instance;

    
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        m_AudioManager.MenuVisibility();
    }
}
