using UnityEngine;

public class ClothespinGrab : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the hand/controller
        if (other.CompareTag("Hand"))
        {
            // Set the animator parameter to true to open the clothespin
            animator.SetBool("isGrabbed", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object that exited the trigger is the hand/controller
        if (other.CompareTag("Hand"))
        {
            // Set the animator parameter to false to close the clothespin
            animator.SetBool("isGrabbed", false);
        }
    }
}
