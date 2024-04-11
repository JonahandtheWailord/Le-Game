using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using static UnityEditor.Progress;

public class playercontroller : MonoBehaviour
{
    public Tilemap tilemap;
    public float moveSpeed = 6f;
    public ContactFilter2D movementFilter;
    public PlayerInput playerInput;

    public static Vector3Int position;
    public static TileBase tilebase;
    public static Collider2D interacting = null;

    public static Rigidbody2D rb;
    public static Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //track direction player is facing

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            direction = new Vector2(0, -1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            direction = new Vector2(-1, 0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            direction = new Vector2(0, 1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            direction = new Vector2(1, 0);
        }
    }
    void FixedUpdate()
    {
        // Perform movement
        Vector2 intendedMovement = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector2 velocity = intendedMovement * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + velocity);

        //Do a Raycast to detect hits/interacting obejects
        RaycastHit2D[] hits = new RaycastHit2D[1]; // Adjust the size based on your needs
        int hitCount = rb.Cast(direction, movementFilter, hits, direction.magnitude);
        interacting = hits[0].collider;

        // Check if any hits were detected
        if (hitCount > 0)
        {
            
            // Ensure the first hit is not null before accessing its collider
            if (hits[0].collider != null && hits[0].collider.gameObject != null)
            {
                //Get Position of object we're currently interacting with
                Vector3Int cellPosition = new Vector3Int(Vector3Int.zero.x + (int)(direction.x), Vector3Int.zero.y + (int)(direction.y), 0);
                position = tilemap.WorldToCell(hits[0].point) + cellPosition;

                //This is so we don't get the Tilemap error for objects not associated with tilemaps
                if (hits[0].collider.TryGetComponent<Tilemap>(out Tilemap tilemp))
                {
                    Debug.Log(position);
                    tilebase = tilemp.GetTile(position);
                }
            }
            else
            {
                interacting = null;
            }
        }

        // Handle collisions
        if (hitCount > 0)
        {
            for (int i = 0; i < hitCount; i++)
            {
                //Commented out because we don't need to handle collisions manually
                /*
                Vector2 normal = hits[i].normal;
                Vector2 direction = Vector2.Reflect(velocity.normalized, normal); // Reflect the movement direction

                // If the collision is along the X-axis, zero out the X-component of the movement direction
                
                if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
                {
                    direction.x = 0f;
                }
                // If the collision is along the Y-axis, zero out the Y-component of the movement direction
                else
                {
                    direction.y = 0f;
                }
                
                // Update the movement direction with the corrected direction
                velocity = direction * moveSpeed * Time.fixedDeltaTime;

                // Apply the corrected movement to the Rigidbody
                rb.MovePosition(rb.position + velocity);
                */
            }
        }
        //for test info in corner
        test_info.interactwith(interacting);
    }
}

    // Method to add item to inventory


