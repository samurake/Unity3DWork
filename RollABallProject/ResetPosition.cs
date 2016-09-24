// Just a simple script to Reset the position of the target that has this script.
// Used when the target is out of the map.
// Just a simple BugFix.

using UnityEngine;
using System.Collections;

public class ResetPosition : MonoBehaviour {

    public Rigidbody rb;
    private Vector3 currentPoz;
    void Update()
    {
        currentPoz = transform.position;
        if (currentPoz.y < -10)
        {
            transform.position = new Vector3(0, 1, 0);
            rb.velocity = new Vector3(0, -10, 0);
        }
    }
}
