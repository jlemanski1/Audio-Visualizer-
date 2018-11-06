using UnityEngine;

public class SampleVis : MonoBehaviour {
    public GameObject cube;                     // Cube Prefab
    GameObject[] _cubes = new GameObject[512];  // Array of instantiated cubes
    public float maxScale;                      // Max scale of cubes
    public int radius;                          // Radius from centre to spawn cubes


    // Instantiate 512 cubes (1/sample) in a ring
    void Start() {
        for (int i = 0; i < 512; i++) {
            GameObject _instanceCube = (GameObject)Instantiate(cube);       // Instantiate cube
            _instanceCube.transform.position = this.transform.position;     // Set instanced cube pos to this pos
            _instanceCube.transform.parent = this.transform;                // Set instanced cube parent
            _instanceCube.name = "Sample Cube " + i;                        // Set handy name
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0); // 512/360 = 0.703125
            _instanceCube.transform.position = Vector3.forward * radius;    // Rotate around centre at a set radius
            _cubes[i] = _instanceCube;                                      // Add instanced cube to array at current sample index
        }
    }


    // Scale Cubes by current sample value to visualize samples
    void Update() {

        for (int i = 0; i < 512; i++) {
            if (cube != null) {
                _cubes[i].transform.localScale = new Vector3(1, (AudioController.samples[i] * maxScale) + 2, 1);

            }
        }
    }
}