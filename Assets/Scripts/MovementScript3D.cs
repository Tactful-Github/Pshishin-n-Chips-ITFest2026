using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementScript3D : MonoBehaviour
{
    public float move_speed = 5;
    public float jump_height = 10;

    private Rigidbody rb;
    private Vector2 movement;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = (Input.GetAxis("Horizontal"));
        movement.y = (Input.GetAxis("Vertical"));

    }

    private void FixedUpdate()
    {
        Vector3 TransformDirection = transform.right * movement.x + transform.forward * movement.y;
        MoveCaracter(TransformDirection);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector3(0, jump_height, 0);
        }
    }

    void MoveCaracter(Vector3 direction)
    {
        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 targetVelocity = direction * move_speed;
        rb.linearVelocity = new Vector3(targetVelocity.x, currentVelocity.y, targetVelocity.z);
    }

}
