using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetTrigger : MonoBehaviour
{
    private const int WINDOW_SIZE = 40;
    private const int NUM_SEGMENTS = 2;
    private const int SEGMENT_SIZE = WINDOW_SIZE / NUM_SEGMENTS;

    public int LowLimit = 30;
    public int HighLimit = 130;

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
        EvaluateModel();
    }

    private void EvaluateModel()
    {
        if (sensorData.Count < WINDOW_SIZE)
        {
            triggerDown = false;
            return;
        }

        float[] means = new float[NUM_SEGMENTS];
        float[] maximums = new float[NUM_SEGMENTS];
        float[] minimums = new float[NUM_SEGMENTS];

        Vector3 baseline = sensorData[sensorData.Count - 1];

        for (int i = 0; i < NUM_SEGMENTS; i++)
        {
            int segmentStart = SEGMENT_SIZE * i;
            ComputeOffsets(segmentStart, baseline);

            means[i] = ComputeAverage();
            maximums[i] = ComputeMaximum();
            minimums[i] = ComputeMinimum();
        }

        float min1 = minimums[0];
        float max2 = maximums[1];

        if (min1 < LowLimit && max2 > HighLimit)
        {
            sensorData.Clear();
            triggerDown = true;
        }
    }

    private void ComputeOffsets(int start, Vector3 baseline)
    {
        for (int i = 0; i < SEGMENT_SIZE; i++)
        {
            Vector3 point = sensorData[start + i];
            Vector3 offset = new Vector3(point.x - baseline.x, point.y - baseline.y, point.z - baseline.z);
            offsets[i] = offset.magnitude;
        }
    }

    private float ComputeAverage()
    {
        float sum = 0;
        foreach (float offset in offsets)
        {
            sum += offset;
        }

        return sum / offsets.Length;
    }

    private float ComputeMaximum()
    {
        float max = float.MinValue;
        foreach (float offset in offsets)
        {
            max = Mathf.Max(offset, max);
        }

        return max;
    }

    private float ComputeMinimum()
    {
        float min = float.MaxValue;
        foreach (float offset in offsets)
        {
            min = Mathf.Min(offset, min);
        }

        return min;
    }
}
