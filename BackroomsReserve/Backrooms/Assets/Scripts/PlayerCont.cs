using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;
    public float gravityMultiplier = 2f; // Увеличение притяжения
    public Camera playerCamera;

    private CharacterController characterController;
    private float verticalRotation = 0f;
    private float verticalVelocity = 0f;
    private bool isJumping = false;
    private bool isRunning = false;
    Animator AnimPlayer;

    public Transform TargetPos;

    public static bool _trigger;

    private void Start()
    {
        AnimPlayer = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {

        // Ввод с клавиатуры
        float moveForward = Input.GetAxis("Vertical");
        float moveSideways = Input.GetAxis("Horizontal");
        float movementSpeed = isRunning ? runSpeed : walkSpeed;
        moveForward *= movementSpeed * Time.deltaTime;
        moveSideways *= movementSpeed * Time.deltaTime;

        // Вращение с помощью мыши
        float mouseHorizontal = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseVertical = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation -= mouseVertical;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        // Применяем вращение к камере игрока
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseHorizontal);

        // Движение персонажа
        Vector3 moveDirection = transform.forward * moveForward + transform.right * moveSideways;

        // Проверяем возможность прыжка и обрабатываем его
        if (characterController.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                AnimPlayer.SetTrigger("jump");
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
            // Увеличение притяжения
            verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }

        moveDirection.y = verticalVelocity * Time.deltaTime;
        characterController.Move(moveDirection);

        // Проверяем состояние бега
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            _trigger = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            _trigger = false;
        }

        Ray desiredTargetRay = playerCamera.GetComponent<Camera>().ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        Vector3 desiredTargetPosition = desiredTargetRay.origin + desiredTargetRay.direction * 0.7f;
        TargetPos.position = desiredTargetPosition;
    }
}
