using UnityEngine;
using UnityEngine.InputSystem;

public class FlyBehavior : MonoBehaviour
{
    [SerializeField] private float _velocity = 1.5f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private AudioClip _flapClip;
    [SerializeField] private AudioClip _collisionClip;
    
    private Rigidbody2D _rb;
    private float _startingGravityScale;
    private AudioSource _audioSource;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _startingGravityScale = _rb.gravityScale;
        _rb.gravityScale = 0f;
    }

    private void Update()
    {
        if (GameManager.instance.State == GameState.GameOver) return;
        
        bool pressedThisFrame =
            (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) ||
            (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame);

        if (pressedThisFrame)
        {
            if (GameManager.instance.State == GameState.NotStarted)
            {
                GameManager.instance.StartGame();
                _rb.gravityScale = _startingGravityScale;
            }

            _rb.linearVelocity = Vector2.up * _velocity;
            _audioSource.PlayOneShot(_flapClip);
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, _rb.linearVelocity.y * _rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _audioSource.PlayOneShot(_collisionClip);
        CameraShake.instance.Shake();
        GameManager.instance.GameOver();
    }
}