using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReactToAudio : MonoBehaviour
{
    public float maxSize;
    public float minSize;
    public float sizeFactor;

    public const int sampleDataLength = 2048;

    private float clipLoudness;
    private float[] clipData;

    public AudioSource audioSource;

    void Awake()
    {
        clipData = new float[sampleDataLength];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        audioSource.clip.GetData(clipData, audioSource.timeSamples);
        clipLoudness = 0f;
        foreach (var sample in clipData)
        {
            clipLoudness += Mathf.Abs(sample);
        }
        clipLoudness /= sampleDataLength;
        Debug.Log(clipLoudness);
    }

    void Scale()
    {
        //gameObject;
    }
}
