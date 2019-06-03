using UnityEngine;

public class SoundInteractive : IInteractiveObject
{
    public Texture2D soundOnTexture;
    public Texture2D soundOffTexture;

    private static bool soundOn = true;

    private void Awake()
    {
        GetComponent<Renderer>().material.SetTexture("_MainTex", soundOn ? soundOnTexture : soundOffTexture);
    }

    public override void Interact()
    {
        AudioListener.volume = 1 - AudioListener.volume;

        soundOn = !soundOn;

        GetComponent<Renderer>().material.SetTexture("_MainTex", soundOn ? soundOnTexture : soundOffTexture);
    }
}
