using UnityEngine;

public class RatMovement : MonoBehaviour
{
    // Скорость движения вперёд
    public float moveSpeed = 5f;
    // Скорость поворота (градусов в секунду)
    public float rotationSpeed = 90f;
    // Минимальное и максимальное время до смены направления
    public float minTimer = 2f;
    public float maxTimer = 5f;

    private Rigidbody rb;
    // Целевая ротация, к которой будем поворачиваться
    private Vector3 _targetDirection;
    // Таймер для смены направления
    private float timer;

    void Start()
    {
        _targetDirection = Vector3.left; 
        rb = GetComponent<Rigidbody>();
        timer = Random.Range(minTimer, maxTimer);
    }

    void Update()
    {
        // Уменьшаем таймер
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            _targetDirection = -_targetDirection;
            timer = Random.Range(minTimer, maxTimer);
        }

        //// Плавно поворачиваемся к целевой ориентации
        //Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        //rb.MoveRotation(newRotation);

        // Двигаемся только вперёд согласно текущей ориентации
        // Сохраняем вертикальную составляющую скорости (если, например, гравитация действует)
        var velocity = moveSpeed * Time.deltaTime * _targetDirection;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;
    }
}