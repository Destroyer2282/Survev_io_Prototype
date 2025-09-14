using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    [SerializeField] private Transform body; // Ссылка на тело
    [SerializeField] private float rotationOffset = 90f; // Корректировка угла
    [SerializeField] private float rotationSpeed = 10f; // Скорость поворота
    [SerializeField] private Vector3 pivotOffset = new Vector3(0.2f, 0, 0); // Смещение точки вращения

    void Update()
    {
        RotateHandsTowardsMouse();
    }

    private void RotateHandsTowardsMouse()
    {
        // Получение позиции курсора в мировых координатах
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Смещение точки вращения относительно тела
        Vector3 pivotPosition = body.position + pivotOffset;

        // Расчет направления
        Vector3 direction = mousePosition - pivotPosition;
        direction.Normalize();

        // Расчет угла поворота
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Создание целевого вращения
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle + rotationOffset);

        // Плавное вращение
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        // Позиционирование рук в точке вращения
        transform.position = pivotPosition;
    }
}
