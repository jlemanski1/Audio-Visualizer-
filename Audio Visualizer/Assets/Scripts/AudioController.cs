using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioController : MonoBehaviour {
    AudioSource _audioSource;
    public static float[] samples = new float[512];     // Audio Samples: 512
    public static float[] freqBands = new float[8];     // Frequency Bands:   8
    public static float[] bandBuffer = new float[8];    // Freq Band Buffer
    private float[] _bufferSmooth = new float[8];

	void Start () {
        _audioSource = GetComponent<AudioSource>();
	}
	
	// Get new samples every frame
	void Update () {
        GetSpectrumFromSource();
        GetFrequencyBands();
        BandBuffer();
	}

    // Get samples from audio source and compress to 512 samples
    void GetSpectrumFromSource() {
        _audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    // 
    void BandBuffer() {
        for (int i = 0; i < 8; i++) {
            // Set buffer to frequency band
            if (freqBands[i] > bandBuffer[i]) {
                bandBuffer[i] = freqBands[i];
                _bufferSmooth[i] = 0.005f;
            }

            // Decrease bandbuffer by smooth amount
            if (freqBands[i] < bandBuffer[i]) {
                bandBuffer[i] -= _bufferSmooth[i];
                _bufferSmooth[i] *= 1.2f;
            }
        }
    }

    void GetFrequencyBands() {
        /* Creates the frequency bands from the samples
     * 22050 / 512 = 43 hz/sample
     * Frequencies
     * 20 - 60hz
     * 60 - 250hz
     * 500 - 2000hz
     * 2000 - 4000hz
     * 4000 - 6000hz
     * 6000 - 20000hz
     * 
     * Bands
     * 0 - 2   = 86hz
     * 1 - 4   = 172hz   : 87-258
     * 2 - 8   = 344hz   : 259-602
     * 3 - 16  = 688hz   : 603-1290
     * 4 - 32  = 1376hz  : 1291-2666
     * 5 - 64  = 2752hz  : 2667-5418
     * 6 - 128 = 5504hz  : 5419-10922
     * 7 - 256 = 11008hz : 10923-21930
     * =510
     */
        int currentSample = 0;

        for (int i = 0; i < 8; i++) {
            float average = 0;  // Avg frequency of all samples
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            // Add extra 2 samples to cover entire 512
            if (i == 7) {
                sampleCount += 2;
            }

            //Add samples to frequency band
            for (int j = 0; j < sampleCount; j++) {
                average += samples[currentSample] * (sampleCount + 1);
                currentSample++;
            }

            average /= sampleCount;
            freqBands[i] = average * 10;
        }
    }
}