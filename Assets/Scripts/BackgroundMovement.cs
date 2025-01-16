using UnityEngine;

public class BackgroundFollower : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public float parallaxSpeed = 0.5f; // Adjust this value for desired parallax effect

    private Vector3 previousPlayerPosition;

    void Start()
    {
        previousPlayerPosition = player.transform.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = player.transform.position - previousPlayerPosition;
        transform.position += deltaMovement * parallaxSpeed;
        previousPlayerPosition = player.transform.position;
    }
}