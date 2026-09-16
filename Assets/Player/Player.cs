using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 startPoint;
    private Vector2 endPoint;
    private bool isDragging = false;
    private int jumps = 2;

    public float launchForceMultiplier = 10f;
    public float slowMotionScale = 0.2f;

    public LineRenderer lineRenderer;
    public int lineSegmentCount = 20;
    public float lineSegmentLength = 0.1f;

    private Vector2 launchVelocity;

    public Generador generadorScript;
    public Puntuacion puntuacionScript;

    public bool Lose;
    public int punch = 0;
    public int combo = 1;

    public GameObject comboPopupPrefab;
    public Canvas canvasUI;

    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = lineSegmentCount;
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.yellow;
        }

        lineRenderer.enabled = false;
        rb.linearDamping = 0;
    }

    void Update()
    {
        // ENTRADA: Mouse o Touch
        bool inputStarted = Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
        bool inputHeld = Input.GetMouseButton(0) || (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary));
        bool inputReleased = Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended);

        Vector2 inputPosition = Input.mousePosition;
        if (Input.touchCount > 0)
            inputPosition = Input.GetTouch(0).position;

        // INICIO de arrastre
        if (inputStarted && jumps > 0)
        {
            combo = 1;
            startPoint = Camera.main.ScreenToWorldPoint(inputPosition);
            isDragging = true;
            Time.timeScale = slowMotionScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            lineRenderer.enabled = true;
            puntuacionScript.OcultarCombo();
        }

        // MIENTRAS se arrastra
        if (isDragging && inputHeld)
        {
            Vector2 currentPoint = Camera.main.ScreenToWorldPoint(inputPosition);
            Vector2 direction = startPoint - currentPoint;
            Vector2 force = direction * launchForceMultiplier;
            launchVelocity = force / rb.mass;
            DrawTrajectory(transform.position, launchVelocity);
        }

        // FIN del arrastre
        if (isDragging && inputReleased)
        {
            endPoint = Camera.main.ScreenToWorldPoint(inputPosition);
            Vector2 direction = startPoint - endPoint;
            Vector2 force = direction * launchForceMultiplier;
            launchVelocity = force / rb.mass;

            rb.linearVelocity = launchVelocity;

            isDragging = false;
            jumps--;

            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
            lineRenderer.enabled = false;
        }
    }


    void DrawTrajectory(Vector2 startPosition, Vector2 initialVelocity)
    {
        float timeStep = lineSegmentLength / lineSegmentCount;
        Vector3[] points = new Vector3[lineSegmentCount];

        for (int i = 0; i < lineSegmentCount; i++)
        {
            float t = i * timeStep;
            Vector2 point = startPosition + initialVelocity * t + 0.5f * Physics2D.gravity * t * t;
            points[i] = new Vector3(point.x, point.y, 0);
        }

        lineRenderer.positionCount = lineSegmentCount;
        lineRenderer.SetPositions(points);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Block"))
        {
            jumps = 2;
            Destroy(collision.gameObject);
            generadorScript.enemyCount--;

            punch++;

            puntuacionScript.AgregarPuntos(combo);
            combo++;
            Debug.Log(combo);

            audioSource.Play();

        }

        if (collision.gameObject.CompareTag("Respawn"))
        {
            jumps = 2;
            SceneManager.LoadScene(0);
        }
    }
}