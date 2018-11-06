using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleVis : MonoBehaviour {
    public GameObject cube; // Cube Prefab
    GameObject[] _cubes = new GameObject[512];
	public float maxScale;
    public int radius;

	void Start () {
        // Instantiate 512 cubes (1/sample) in a ring
        for (int i = 0; i < 512; i++) {
            GameObject _instanceCube = (GameObject)Instantiate(cube);
            _instanceCube.transform.position = this.transform.position;
            _instanceCube.transform.parent = this.transform;
            _instanceCube.name = "Sample Cube " + i;
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0); // 512/360 = 0.703125
            _instanceCube.transform.position = Vector3.forward * radius;
            _cubes[i] = _instanceCube;
        }
	}
	
	// Scale Cubes with Sample to test visualization
	void Update () {
        for (int i = 0; i < 512; i++) {
            if (cube != null) {
                _cubes[i].transform.localScale = new Vector3(1, (AudioController.samples[i] * maxScale) + 2, 1);

            }
        }
	}
}
