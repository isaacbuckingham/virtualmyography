using UnityEngine;

public class RotateReticle : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 1, 0);

    void Update()
    {
        transform.Rotate(0, 5, 0, Space.World);
        // Debug log to show current rotation
        Debug.Log("Current Rotation: " + transform.rotation.eulerAngles);
    }
}