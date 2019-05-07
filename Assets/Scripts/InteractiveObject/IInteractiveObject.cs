using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IInteractiveObject : MonoBehaviour
{
    public virtual AudioSource Sound
    {
        get
        {
            return null;
        }
    }

    public virtual float SoundPlayTime
    {
        get
        {
            return -1;
        }
    }

    public void InteractWithSound()
    {
        if (Sound != null)
        {
            Sound.Play();
            if (SoundPlayTime > 0)
            {
                Sound.SetScheduledEndTime(AudioSettings.dspTime + SoundPlayTime);
            }
        }
        Interact();
    }

    public abstract void Interact();

    public virtual bool CanInteract()
    {
        return true;
    }
}
