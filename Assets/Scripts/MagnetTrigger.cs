using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetTrigger : MonoBehaviour
{
    private const int WINDOW_SIZE = 40;
    private const int NUM_SEGMENTS = 2;
    private const int SEGMENT_SIZE = WINDOW_SIZE / NUM_SEGMENTS;
    private const int T1 = 30, T2 = 130;

    private List<Vector3> sensorData;
    private float[] offsets;
    private static bool triggerDown;

    public static bool TriggerDown
    {
        get
        {
            return triggerDown;
        }
    }

    void Awake()
    {
        sensorData = new List<Vector3>(WINDOW_SIZE);
        offsets = new float[SEGMENT_SIZE];
    }

    void OnEnable()
    {
        sensorData.Clear();
        Input.compass.enabled = true;
    }

    void OnDisable()
    {
        Input.compass.enabled = false;
    }

    void Update()
    {
        Vector3 currentVector = Input.compass.rawVector;
        if (currentVector.x == 0 && currentVector.y == 0 && currentVector.z == 0)
        {
            return;
        }

        if (sensorData.Count >= WINDOW_SIZE)
        {
            sensorData.RemoveAt(0);
        }

        sensorData.Add(currentVector);
    }

    private void EvaluateModel()
    {
        if (sensorData.Count < WINDOW_SIZE)
        {
            triggerDown = false;
            return;
        }

        float[] means = new float[2];
        float[] maximums = new float[2];
        float[] minimums = new float[2];

        Vector3 baseline = sensorData[sensorData.Count - 1];

        for (int i = 0; i < NUM_SEGMENTS; i++)
        {
            int segmentStart = 20 * i;
            offsets = ComputeOffsets(segmentStart, baseline);

            means[i] = ComputeMean(offsets);
            maximums[i] = ComputeMaximum(offsets);
            minimums[i] = ComputeMinimum(offsets);
        }

        float min1 = minimums[0];
        float max2 = maximums[1];

        if (min1 < T1 && max2 > T2)
        {
            sensorData.Clear();
            triggerDown = true;
        }
    }

    private float[] ComputeOffsets(int start, Vector3 baseline)
    {
        for (int i = 0; i < SEGMENT_SIZE; i++)
        {
            Vector3 point = sensorData[start + i];
            Vector3 o = new Vector3(point.x - baseline.x, point.y - baseline.y, point.z - baseline.z);
            offsets[i] = o.magnitude;
        }

        return offsets;
    }

    private float ComputeMean(float[] offsets)
    {
        float sum = 0;
        foreach (float o in offsets)
        {
            sum += o;
        }

        return sum / offsets.Length;
    }

    private float ComputeMaximum(float[] offsets)
    {
        float max = float.MinValue;
        foreach (float o in offsets)
        {
            max = Mathf.Max(o, max);
        }

        return max;
    }

    private float ComputeMinimum(float[] offsets)
    {
        float min = float.MaxValue;
        foreach (float o in offsets)
        {
            min = Mathf.Min(o, min);
        }

        return min;
    }
}
