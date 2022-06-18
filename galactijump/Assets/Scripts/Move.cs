using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 3;
    public float gravity = -16;
    public float jumpheight = 4;

    public Transform groundCheck;
    public  float groundDistance = 0.4f;
    public LayerMask groundmask;

    bool isGrounded;
    Vector3 velocity;

    public float dfov = 75; public float sfov = 90;
    float gox = 0, goy = 1, goz = 0;

    private AudioSource source;
    public Text win;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded){
            velocity.y = Mathf.Sqrt(jumpheight*-2f*gravity);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) {speed=6;}
        if (Input.GetKeyUp(KeyCode.LeftShift)) {speed=3;}
        // if (Input.GetKeyDown(KeyCode.LeftAlt)) {speed = 1;}
        // if (Input.GetKeyUp(KeyCode.LeftAlt)) {speed = 3;}

        Vector3 move = transform.right * x + transform.forward *z;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); 

    }
    void LateUpdate() {
        if (transform.position.y <= -5) {
            transform.position = new Vector3(gox,goy,goz);
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.name.StartsWith("checkpoint")) {
            gox = hit.transform.position.x;
            goy = hit.transform.position.y + 1;
            goz = hit.transform.position.z;
            Destroy(hit.gameObject);
            source.Play();
        }
        if (hit.gameObject.name.StartsWith("winner")) {
            Destroy(hit.gameObject);
            source.Play();
            win.text = "You win!";
        }
    }
}
