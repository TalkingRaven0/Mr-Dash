using UnityEngine;

public class GibsMovement : MonoBehaviour
{
    private Rigidbody2D[] parts;
    private static Vector2 inherit;

    private void Awake()
    {
        parts = GetComponentsInChildren<Rigidbody2D>();
    }

    private void Start()
    {
        foreach(Rigidbody2D rb in parts)
        {
            rb.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1))*3,ForceMode2D.Impulse);
            rb.AddForce(inherit, ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-50, 50));
        }
    }

    public static void inheritforce(Vector2 force)
    {
        inherit = force;
    }
}
