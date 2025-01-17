using UnityEngine;

public class ScreenBoundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private Rigidbody2D rigidbody2D;
    private Camera camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PreventPlayerGoingOffScreen();
    }

    private void PreventPlayerGoingOffScreen()
    {
        Vector2 screenPosition = Camera.main.ScreenToWorldPoint(transform.position);

        if ((screenPosition.x < 0 && rigidbody2D.linearVelocity.x < 0) ||
            (screenPosition.x > camera.pixelWidth && rigidbody2D.linearVelocity.x > 0))
        {
            rigidbody2D.linearVelocity = new Vector2(0, rigidbody2D.linearVelocity.y);
        }

        if ((screenPosition.y < 0 && rigidbody2D.linearVelocity.y < 0) ||
            (screenPosition.y > camera.pixelHeight && rigidbody2D.linearVelocity.y > 0))
        {
            rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, 0);
        }
    }
}
