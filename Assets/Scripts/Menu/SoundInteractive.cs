using UnityEngine;

public class SoundInteractive : IInteractiveObject
{
    public override void Interact()
    {
        AudioListener.volume = 1 - AudioListener.volume;
    }
}
