using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public Vector2 minCamPos, maxCamPos;
    public float smoothVelocity;

    private Vector2 velocity;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref velocity.x, smoothVelocity);
            float posY = Mathf.SmoothDamp(transform.position.y, target.transform.position.y + 2, ref velocity.y, smoothVelocity);

            transform.position = new Vector3(
                Mathf.Clamp(posX, minCamPos.x, maxCamPos.x),
                Mathf.Clamp(posY, minCamPos.y, maxCamPos.y),
                transform.position.z);
        }
    }
}
