using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public bool cantMove;
    public Pausa pauseMenu;

    [SerializeField]private Transform posicionObjeto;
    public Transform PosicionObjeto { get { return posicionObjeto; } }
    public bool CantMove { set { cantMove = value; } }
    public float movementSpeed;
    public Vector2 sensitivity;
    public new Transform camera;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.None;
       
        cantMove = false;
    }

    private void UpdateMovement()
    {
        
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 velocity = Vector3.zero;

        if (hor != 0 || ver != 0)
        {
            Vector3 direction = (transform.forward * ver + transform.right * hor).normalized;
            velocity = direction * movementSpeed;
        }

        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;
    }

    private void UpdateMouseLook()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        if (hor != 0)
        {
            transform.Rotate(0, hor * sensitivity.x, 0);
        }

        if (ver != 0)
        {
            Vector3 rotation = camera.localEulerAngles;
            rotation.x = (rotation.x - ver * sensitivity.y + 360) % 360;
            if (rotation.x > 80 && rotation.x < 180) {
                rotation.x = 80; 
             } else if (rotation.x < 280 && rotation.x > 180)
            {
                rotation.x = 280;
            }

            camera.localEulerAngles = rotation;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        if (!cantMove)
        {
            UpdateMovement();
            UpdateMouseLook();
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

       
    }

    public void ActivarCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void DesactivarCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
