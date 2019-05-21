using UnityEngine;

public class BrazierPuzzleController : ISavableObject
{
    public TorchInteractiveController[] braziers;
    public GameObject[] lights;
    public IInteractiveObject platform;

    public bool Solved
    {
        private set;
        get;
    }

    public override void Save()
    {
        SaveBool("Solved", Solved);
    }

    public override void Load()
    {
        Solved = LoadBool("Solved");
    }

    void Awake()
    {
        foreach (GameObject l in lights)
        {
            l.SetActive(false);
        }
        Solved = false;
    }

    void Update()
    {
        if (!Solved && AllLit())
        {
            platform.InteractWithSound();
            Solved = true;
        }
    }

    private bool AllLit()
    {
        bool res = true;
        for (int i = 0; i < braziers.Length; i++)
        {
            if (!braziers[i].IsLit)
            {
                res = false;
                continue;
            }
            lights[i].SetActive(true);
        }
        return res;
    }
}
