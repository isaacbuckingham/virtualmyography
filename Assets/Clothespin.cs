using UnityEngine;

public class Clothespin : MonoBehaviour
{
    public Transform side1; // Reference to the first side of the clothespin
    public Transform side2; // Reference to the second side of the clothespin
    public Transform grabPoint; // Reference to the grabbing point (e.g., controller/hand)

    public float maxOpenAngle = 30f; // Maximum angle the clothespin can open
    public float triggerDepth = 1f; // Maximum depth for full open

    private bool isGrabbed = false;
    private Vector3 initialGrabPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            isGrabbed = true;
            initialGrabPosition = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            isGrabbed = false;
            ResetClothespin();
        }
    }

    private void Update()
    {
        if (isGrabbed)
        {
            float currentDepth = Vector3.Distance(grabPoint.position, initialGrabPosition);
            float depthRatio = Mathf.Clamp01(currentDepth / triggerDepth);
            float angle = maxOpenAngle * depthRatio;
            AdjustClothespin(angle);
        }
    }

    private void AdjustClothespin(float angle)
    {
        side1.localRotation = Quaternion.Euler(0, 0, angle);
        side2.localRotation = Quaternion.Euler(0, 0, -angle);
    }

    private void ResetClothespin()
    {
        side1.localRotation = Quaternion.identity;
        side2.localRotation = Quaternion.identity;
    }
}
