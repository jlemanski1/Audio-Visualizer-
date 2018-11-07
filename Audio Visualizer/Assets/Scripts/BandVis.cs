using UnityEngine;

public class BandVis : MonoBehaviour {
    public int band;                  // Band to visualize [0-7]
    public float startScale;          // Initial & smallest cube size
    public float scaleMultiplier;     // Amount to multply cube hieght by
    public bool _useBuffer;           // Smooth Frequency Bands or not

	// Visualize frequency band value by scaling y value every frame by applying some multiplier
    void Update() {
        if (_useBuffer) {
            transform.localScale = new Vector3(transform.localScale.x, (AudioController.bandBuffer[band]
                * scaleMultiplier) + startScale, transform.localScale.z);
        }
        else {
            transform.localScale = new Vector3(transform.localScale.x, (AudioController.freqBands[band]
                * scaleMultiplier) + startScale, transform.localScale.z);
        }
    }
}