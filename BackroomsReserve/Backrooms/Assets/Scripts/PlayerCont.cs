using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;
    public float gravityMultiplier = 2f; // ���������� ����������
    public Camera playerCamera;

    private CharacterController characterController;
    private float verticalRotation = 0f;
    private float verticalVelocity = 0f;
    private bool isJumping = false;
    private bool isRunning = false;

    public static bool _trigger;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // ���� � ����������
        float moveForward = Input.GetAxis("Vertical");
        float moveSideways = Input.GetAxis("Horizontal");
        float movementSpeed = isRunning ? runSpeed : walkSpeed;
        moveForward *= movementSpeed * Time.deltaTime;
        moveSideways *= movementSpeed * Time.deltaTime;

        // �������� � ������� ����
        float mouseHorizontal = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseVertical = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation -= mouseVertical;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        // ��������� �������� � ������ ������
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseHorizontal);

        // �������� ���������
        Vector3 moveDirection = transform.forward * moveForward + transform.right * moveSideways;

        // ��������� ����������� ������ � ������������ ���
        if (characterController.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                isJumping = true;
                verticalVelocity = jumpForce;
            }
            else
            {
                isJumping = false;
                verticalVelocity = -0.5f;
            }
        }
        else
        {
            // ���������� ����������
            verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }

        moveDirection.y = verticalVelocity * Time.deltaTime;
        characterController.Move(moveDirection);

        // ��������� ��������� ����
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        if (isRunning)
        {
            _trigger = true; 
        }
        else _trigger = false;
    }

    
}
