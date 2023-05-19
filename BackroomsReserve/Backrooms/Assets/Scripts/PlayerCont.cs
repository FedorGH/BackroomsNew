using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float mouseSensitivity = 2f;
    public Camera playerCamera;

    private CharacterController characterController;
    private float verticalRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // �������� ��������� ���� � ���������� ��� � ������ ������
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // ���� � ����������
        float moveForward = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        float moveSideways = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

        // �������� � ������� ����
        float mouseHorizontal = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseVertical = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation -= mouseVertical;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // ������������ ���� �������� ����� � ����

        // ��������� �������� � ������ ������
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseHorizontal);

        // �������� ���������
        Vector3 moveDirection = transform.forward * moveForward + transform.right * moveSideways;
        characterController.Move(moveDirection);
    }
}
