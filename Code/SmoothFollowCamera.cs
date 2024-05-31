using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    public Transform target;                // The target to follow (player)
    public AnimationCurve speedCurve;       // Animation curve to control speed
    public float startSpeed = 5f;           // Initial speed
    public float endSpeed = 10f;            // Final speed

    private void Update()
    {
        if (target == null)
            return;

        // Calculate the distance between the camera and the target
        Vector3 targetPos = target.position;
        targetPos.z = transform.position.z; // Keep the Z position constant

        float distance = Vector3.Distance(transform.position, targetPos);

        // Evaluate the animation curve to get the current speed based on the distance
        float t = Mathf.Clamp01(distance / 10f); // Normalize the distance to a range between 0 and 1
        float currentSpeed = Mathf.Lerp(startSpeed, endSpeed, speedCurve.Evaluate(t));

        // Move the camera smoothly towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPos, currentSpeed * Time.deltaTime);
    }
}
 