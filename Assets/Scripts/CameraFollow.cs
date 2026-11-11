using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2f;
    public Transform target;
    public float yOffset = 2f;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        transform.position = Vector3.Lerp(transform.position, newPos, followSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0f, 100000), -10f);
    }
}
