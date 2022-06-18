using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 1000f;
    public float xsens = 700f;
    public Transform body;
    public Transform buddy;
    float xrotation = 0f;
    public Animator animator; 
    public bool inmenu = true;
    public Image crosshair1, crosshair2; 
    public TMP_Text menutext1, menutext2;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // myAnimation = GetComponent<Animation>();
        animator.SetBool("inmenu", true); inmenu = true;
    }

    void Update()
    {
        if (inmenu) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                animator.SetBool("inmenu", false);
                inmenu = false;
                buddy.position = new Vector3(0, 0, 0);
                buddy.rotation = Quaternion.Euler(0, 0, 0);
                menutext1.text = ""; menutext2.text = "";
                crosshair1.enabled = true; crosshair2.enabled = true;
            }
        }

        else {

        float mousex = Input.GetAxis("Mouse X") * xsens * Time.deltaTime;
        float mousey = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        
        xrotation -= mousey;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xrotation, 0f, 0f);
        body.Rotate(Vector3.up * mousex);
        //if (Input.GetKeyDown(KeyCode.LeftAlt)) {buddy.position = new Vector3(buddy.position.x, buddy.position.y-0.5f, buddy.position.z);}
        //if (Input.GetKeyUp(KeyCode.LeftAlt)) {buddy.position = new Vector3(buddy.position.x, buddy.position.y+0.5f, buddy.position.z);}
        }
    }
}
