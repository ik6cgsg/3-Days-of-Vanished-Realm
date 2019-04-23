using UnityEngine;

public class BrazierPuzzleController : MonoBehaviour
{
    public TorchInteractiveController[] braziers;
    public GameObject[] lights;
    public IInteractiveObject platform;

    public bool Solved
    {
        private set;
        get;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject l in lights)
        {
            l.SetActive(false);
        }
        Solved = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Solved && AllLit())
        {
            platform.Interact();
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
