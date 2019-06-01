using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;
    private Vector3 targetPos;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        rb = target.GetComponent<Rigidbody>();
        transform.parent = null;
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
    }

    void LateUpdate()
    {
        if (target == null)
            target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        
        targetPos = target.position + (DirFromAngle(target.eulerAngles.y) * 2) + new Vector3(0, 10, 0);
        float targetVelocity = Vector3.Distance(new Vector3(), rb.velocity) / 10;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, (Time.deltaTime * 25) - targetVelocity);
    }

    public Vector3 DirFromAngle(float angleInDegree)
    {
        return new Vector3(Mathf.Sin(angleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegree * Mathf.Deg2Rad));
    }
}