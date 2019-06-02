using UnityEngine;

public class LogicSustainedObjectActivator : MonoBehaviour
{
    public LogicBoolean isActiveBoolean;
    public GameObject controlledGameObject;

    private void Awake()
    {
        controlledGameObject.SetActive(false);
    }

    private void Update()
    {
        if (controlledGameObject.activeSelf != isActiveBoolean.Value)
        {
            controlledGameObject.SetActive(isActiveBoolean.Value);
        }
    }
}
