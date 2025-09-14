using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    [SerializeField] private Transform body; // ������ �� ����
    [SerializeField] private float rotationOffset = 90f; // ������������� ����
    [SerializeField] private float rotationSpeed = 10f; // �������� ��������
    [SerializeField] private Vector3 pivotOffset = new Vector3(0.2f, 0, 0); // �������� ����� ��������

    void Update()
    {
        RotateHandsTowardsMouse();
    }

    private void RotateHandsTowardsMouse()
    {
        // ��������� ������� ������� � ������� �����������
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // �������� ����� �������� ������������ ����
        Vector3 pivotPosition = body.position + pivotOffset;

        // ������ �����������
        Vector3 direction = mousePosition - pivotPosition;
        direction.Normalize();

        // ������ ���� ��������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // �������� �������� ��������
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle + rotationOffset);

        // ������� ��������
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        // ���������������� ��� � ����� ��������
        transform.position = pivotPosition;
    }
}
