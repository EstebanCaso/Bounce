using UnityEngine;

public class CameraFollowZoom : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float smoothSpeed = 5f;

    public Rigidbody2D playerRb;
    public float minZoom = 5f;
    public float maxZoom = 10f;
    public float speedToMaxZoom = 20f;  // A qu� velocidad se alcanza el zoom m�ximo

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (target == null || playerRb == null) return;

        // Movimiento de c�mara normal
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Zoom din�mico seg�n la velocidad del jugador
        float speed = playerRb.linearVelocity.magnitude;
        float t = Mathf.Clamp01(speed / speedToMaxZoom);  // Normaliza la velocidad [0,1]
        float targetZoom = Mathf.Lerp(minZoom, maxZoom, t);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, smoothSpeed * Time.deltaTime);
    }
}
