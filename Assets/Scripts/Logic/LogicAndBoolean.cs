using UnityEngine;

public class LogicAndBoolean : LogicBoolean
{
    public LogicBoolean[] logicBooleans;

    void Update()
    {
        foreach (LogicBoolean logicBoolean in logicBooleans)
        {
            if (!logicBoolean.Value)
            {
                Value = false;
                return;
            }
        }

        Value = true;
    }
}
