using UnityEngine;
public class SoundInteractive : IInteractiveObject
{
    public  override void Interact()
    {
        AudioListener.pause=true;
    }
}
