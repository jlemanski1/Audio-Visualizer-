using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioController : MonoBehaviour {
    AudioSource _audioSource;
    public static float[] samples = new float[512];   // Audio Samples: 512


	void Start () {
        _audioSource = GetComponent<AudioSource>();
	}
	
	// Get new samples every frame
	void Update () {
        GetSpectrumFromSource();
	}

    // Get samples from audio source and compress to 512 samples
    void GetSpectrumFromSource() {
        _audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }
}
