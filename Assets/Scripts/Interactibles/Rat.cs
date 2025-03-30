using Assets.Scripts.Interactions;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Highlightable))]
public class Rat : MonoBehaviour
{
    // Минимальное и максимальное время движения в одном направлении
    public float minTime = 1.0f;
    public float maxTime = 3.0f;

    // Скорость горизонтального движения
    public float minSpeed = 1f;
    public float maxSpeed = 3f;

    public float minJump = 3f;
    public float maxJump = 8f;

    // Вероятность подпрыгивания (от 0 до 1)
    public float jumpProbability = 0.2f;

    // Скорость поворота модели (в градусах в секунду)
    public float rotationSpeed = 180f;

    // Базовая скорость для анимации (нормальная скорость движения)
    public float baseAnimationSpeed = 2f;

    public GameObject MeatPrefab;

    private Rigidbody rb;
    private Animator animator;
    private Vector3 currentDirection;
    private float timer;
    private float _speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        SetNewDirection();
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    void SetNewDirection()
    {
        Vector2 randomDir2D = Random.insideUnitCircle.normalized;
        currentDirection = new Vector3(randomDir2D.x, 0, randomDir2D.y);

        timer = Random.Range(minTime, maxTime);
        _speed = Random.Range(minSpeed, maxSpeed);

        if (IsGrounded() && Random.value < jumpProbability)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, Random.Range(minJump, maxJump), rb.linearVelocity.z);
        }
    }

    void FixedUpdate()
    {
        Vector3 horizontalVelocity = currentDirection * _speed;
        rb.linearVelocity = new Vector3(horizontalVelocity.x, rb.linearVelocity.y, horizontalVelocity.z);

        Quaternion targetRotation = Quaternion.LookRotation(currentDirection, Vector3.up);
        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));

        // Привязка глобальной скорости аниматора к скорости движения
        if (animator != null)
        {
            animator.speed = _speed / baseAnimationSpeed;
        }

        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
        {
            SetNewDirection();
        }
    }

    public void TakeHit()
    {
        EffectManager.Instance.PlaySfx(EffectManager.Instance.RatDeathAudio)
            .PlayParticles(EffectManager.Instance.RatDeathParticle, transform.position);
        Instantiate(MeatPrefab,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }

}
