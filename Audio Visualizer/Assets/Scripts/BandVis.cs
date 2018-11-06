using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandVis : MonoBehaviour {
    public int band;
    public float startScale, scaleMultiplier;

	

	void Update () {
        transform.localScale = new Vector3(transform.localScale.x, (AudioController.freqBands[band] * scaleMultiplier) + startScale, transform.localScale.z);
	}
}
