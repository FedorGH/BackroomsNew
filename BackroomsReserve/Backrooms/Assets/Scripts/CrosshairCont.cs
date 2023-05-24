using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairCont : MonoBehaviour
{
    public Texture2D crosshairTexture;
    public Vector2 crosshairSize = new Vector2(32, 32);

    private Rect crosshairPosition;

    private void Update()
    {
        // ������������� ������� ������� � ����� ������
        float crosshairX = (Screen.width - crosshairSize.x) / 2;
        float crosshairY = (Screen.height - crosshairSize.y) / 2;
        crosshairPosition = new Rect(crosshairX, crosshairY, crosshairSize.x, crosshairSize.y);
    }

    private void OnGUI()
    {
        // ���������� ������ �� ������
        GUI.DrawTexture(crosshairPosition, crosshairTexture);
    }
}
