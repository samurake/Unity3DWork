// Just a simple line used to create a rotating efect on a desired target.
// It uses unity built-in timer to do that. Created for pick-ups.

using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}
}
