using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    Rigidbody ballBody;
    SphereCollider ballCollider;
    Vector3 previousPlayerPosition;

    void Start()
    {
        ballBody = GetComponent<Rigidbody>();
        ballCollider = GetComponent<SphereCollider>();

        // Find spilleren én gang, så vi kan lagre dens startposition
        GameObject player = GameObject.FindGameObjectWithTag("PlayerFree");
        if (player != null)
        {
            previousPlayerPosition = player.transform.position;
        }
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("PlayerFree");
        if (player != null)
        {
            Collider playerCollider = player.GetComponent<Collider>();

            if (playerCollider != null && ballCollider.bounds.Intersects(playerCollider.bounds))
            {
                // Beregn spillerens bevægelsesretning baseret på forrige position
                Vector3 playerMovementDirection = (player.transform.position - previousPlayerPosition).normalized;

                // Hvis spilleren ikke bevæger sig, giv en standard skub-retning
                if (playerMovementDirection == Vector3.zero)
                {
                    playerMovementDirection = (ballBody.position - player.transform.position).normalized;
                }

                // Tilføj kraft i spillerens bevægelsesretning
                ballBody.AddForce(playerMovementDirection * 10f, ForceMode.Impulse);
            }

            // Opdater spillerens tidligere position til næste frame
            previousPlayerPosition = player.transform.position;
        }
    }
}
