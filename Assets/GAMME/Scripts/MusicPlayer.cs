using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

//This script is placed in the Enceinte and displays the menu whenever the user selects (grabs) the Enceinte
public class MusicPlayer : XRBaseInteractable
{

    AudioManager m_AudioManager = AudioManager.Instance;

    
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        m_AudioManager.MenuVisibility();
    }
}
