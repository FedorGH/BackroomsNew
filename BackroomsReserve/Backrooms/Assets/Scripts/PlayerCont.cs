using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float runSpeed = 6f;
    public float crouchSpeed = 2f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;
    public float gravityMultiplier = 2f; // Увеличение притяжения
    public float crouchHeight = 1f;
    public Camera playerCamera;

    private CharacterController characterController;
    private float verticalRotation = 0f;
    private float verticalVelocity = 0f;
    [HideInInspector] public bool isRunning = false;
    private bool isCrouching = false; // Переменная состояния крадения
    private float originalControllerHeight;
    Animator AnimPlayer;

    private void Start()
    {
        AnimPlayer = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        originalControllerHeight = characterController.height;
    }

    private void Update()
    {

        float moveForward = Input.GetAxis("Vertical");
        float moveSideways = Input.GetAxis("Horizontal");
        float movementSpeed = isRunning ? runSpeed : (isCrouching ? crouchSpeed : walkSpeed);
        moveForward *= movementSpeed * Time.deltaTime;
        moveSideways *= movementSpeed * Time.deltaTime;
        
        // Ввод с клавиатуры


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
            if (Input.GetButtonDown("Jump"))
            {
                AnimPlayer.SetTrigger("jump");
                verticalVelocity = jumpForce;
            }
            else
            {
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && isCrouching == false)
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCrouch();
        }
    }

    private void ToggleCrouch()
    {
        isCrouching = !isCrouching; // Инвертируем состояние крадения

        if (isCrouching)
        {
            characterController.height = crouchHeight; // Изменяем высоту CharacterController в положении крадения
        }
        else
        {
            characterController.height = originalControllerHeight; // Возвращаем оригинальную высоту CharacterController
        }
    }
}
