using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 1. Added this for TextMeshPro support

public class MovementScriptWithCameraMovement : MonoBehaviour
{
    public float move_speed = 5;

    private Rigidbody rb;
    private Vector2 movement;
    private SceneChange currentCubicle;
    private Animator anim;

    [Header("UI Settings")]
    public GameObject interactPrompt; // 2. Drag your Text object here in the Inspector

    [Header("Audio Settings")]
    public AudioSource sfxSource;   // Drag the AudioSource component here
    public AudioClip footstepSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        
        // Hide prompt at the start
        if (interactPrompt != null)
            interactPrompt.SetActive(false);
    }

    public void PlayFootstep() 
    {
        if (sfxSource != null && footstepSound != null)
        {
            sfxSource.pitch = Random.Range(0.8f, 1.2f);
            sfxSource.PlayOneShot(footstepSound);
        }
    }

    void Update()
    {
        movement.x = (Input.GetAxis("Vertical"));
        movement.y = (Input.GetAxis("Horizontal"));

        // 3. Logic for the 'F' key
        if (interactPrompt != null && interactPrompt.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
               if (currentCubicle != null)
                {
                    currentCubicle.ActivateCubicle(); // RUN THE SCRIPT ON THE CUBICLE
                }// Do something here (e.g., start a mini-game or dialogue)
            }
        }

        if (anim != null)
    {
        // We send the 'Horizontal' input to MoveX
        // and the 'Vertical' input to MoveY
        anim.SetFloat("horizontal", movement.y); 
        anim.SetFloat("vertical", movement.x);

        // This 'Speed' tells the animator if we are moving at all
        float speed = new Vector2(movement.x, movement.y).magnitude;

        if (speed < 0.01f)
    {
        sfxSource.Stop(); // This kills any currently playing footstep/scuff
    }

        anim.SetFloat("speed", speed);
    }
    



    }

    private void FixedUpdate()
    {
        Vector3 TransformDirection = transform.right * movement.x + transform.forward * movement.y;
        MoveCaracter(TransformDirection);
    }

    void MoveCaracter(Vector3 direction)
    {
        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 targetVelocity = direction * move_speed;
        // Kept your specific velocity logic
        rb.linearVelocity = new Vector3(targetVelocity.x, currentVelocity.y, -targetVelocity.z);
    }

   private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("CubicleInteract"))
        {
            // Try to get the script
            SceneChange logic = other.GetComponent<SceneChange>();

            // ONLY show prompt and save reference if the script actually exists
            if (logic != null)
            {
                currentCubicle = logic;
                if (interactPrompt != null) interactPrompt.SetActive(true);
                Debug.Log("Entered Zone and found CubicleLogic!");
            }
            else
            {
                Debug.LogWarning("Object tagged CubicleInteract is missing the CubicleLogic script!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CubicleInteract"))
        {
            if (interactPrompt != null) interactPrompt.SetActive(false);

            currentCubicle = null; // Important to clear this!
            Debug.Log("Exited 3D Zone!");
        }
    }
}