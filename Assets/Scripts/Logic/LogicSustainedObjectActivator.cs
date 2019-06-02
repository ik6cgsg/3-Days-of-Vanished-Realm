using UnityEngine;

public class LogicSustainedObjectActivator : MonoBehaviour
{
    public LogicBoolean isActiveBoolean;
    public GameObject controlledGameObject;

    private bool isActive;

    private void Awake()
    {
        controlledGameObject.SetActive(false);
    }

    private void Update()
    {
        if (isActive != isActiveBoolean.Value)
        {
            isActive = isActiveBoolean.Value;
            controlledGameObject.SetActive(isActive);
        }
    }
}
