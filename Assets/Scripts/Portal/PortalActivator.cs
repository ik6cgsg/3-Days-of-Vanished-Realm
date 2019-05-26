using UnityEngine;

public class PortalActivator : IInteractiveObject
{
    public AudioSource soundRef = null;
    public BasePortal baseRef;

    public override AudioSource Sound
    {
        get
        {
            return soundRef;
        }
    }

    public override void Save()
    {
        SaveBool("isActive", baseRef.isActive);
    }

    public override void Load()
    {
        baseRef.SetActive(LoadBool("isActive"));
    }

    public override void Interact()
    {
        baseRef.SetActive(!baseRef.isActive);
    }
}
