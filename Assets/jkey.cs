using UnityEngine;

public class KeyPressLogger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("The J key was pressed!");
        }
    }
}