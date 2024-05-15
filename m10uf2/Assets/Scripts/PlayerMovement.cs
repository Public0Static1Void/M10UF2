using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float camSpeed;

    private float rot = 0;

    private Transform camera;

    private CharacterController ch;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camera = GetComponentInChildren<Camera>().transform;

        //ch = GetComponent<CharacterController>();
        rb = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * camSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * camSpeed;

        rot -= mouseY;
        rot = Mathf.Clamp(rot, -85, 75);

        camera.transform.localRotation = Quaternion.Euler(rot, 0, 0);

        transform.Rotate(transform.up * mouseX);

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (xInput == 0 && yInput == 0)
        {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            return;
        }

        Vector3 dir = transform.forward * yInput + transform.right * xInput;
        //transform.Translate(dir * speed * Time.deltaTime, Space.World);
        rb.MovePosition(transform.position + dir * speed);
    }
}