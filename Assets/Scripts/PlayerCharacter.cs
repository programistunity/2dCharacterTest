using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
	public Transform PlatformCenter;

	public float CharacterGroundOffset=1.8f;
	public float GravityStrength = 9.8f;
	public float MoveForce= 3.0f;
	public float MoveMaxSpeed = 8.0f;
	public float JumpForce = 14.0f;
	public float RotationSmoothing = 10.0f;
	public LayerMask GroundLayer;

	private Rigidbody2D _rigidbody2D;

	private float _moveInput;
	private bool _isGrounded;
	private Vector2 _currentGravityDir;

	void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_rigidbody2D.gravityScale = 0;
		_rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
	}

	void Start()
	{
		InputController.Instance.InputInterface.Move += Move;
		InputController.Instance.InputInterface.Jump += Jump;
	}

	void OnDestroy()
	{
		InputController.Instance.InputInterface.Move -= Move;
		InputController.Instance.InputInterface.Jump -= Jump;
	}

	void FixedUpdate()
	{
		CheckGround();
		Gravity();
		CharacterMove();
	}

	private void Gravity()
	{
		if (PlatformCenter == null) return;

	
		var platformDirection= (PlatformCenter.position - transform.position).normalized;
		var groundPoint = Physics2D.Raycast(transform.position, platformDirection,
				Mathf.Infinity, GroundLayer);
		if (groundPoint != null)
		{
			_currentGravityDir = -groundPoint.normal;
		}
		_rigidbody2D.AddForce(_currentGravityDir * GravityStrength);

		RotateToGravity();
	}

	private void CharacterMove()
	{
		if (_isGrounded)
		{
			if (_rigidbody2D.linearVelocity.magnitude < MoveMaxSpeed)
			{
				Vector2 moveDir = transform.right * _moveInput;
				_rigidbody2D.linearVelocity += moveDir * MoveMaxSpeed * MoveForce * Time.fixedDeltaTime;
			}
		}
	}

	private void CheckGround()
	{
		var hit = Physics2D.Raycast(transform.position - transform.up * CharacterGroundOffset, -transform.up,
				Mathf.Infinity, GroundLayer);
		if (hit != null && hit.distance <= CharacterGroundOffset)
		{
			_isGrounded = true;
		}
		else
		{
			_isGrounded = false;
		}
	}

	private void RotateToGravity()
	{
		var targetAngle = Mathf.Atan2(_currentGravityDir.y, _currentGravityDir.x) * Mathf.Rad2Deg + 90f;
		var currentAngle = transform.eulerAngles.z;
		
		var smoothAngle = Mathf.LerpAngle(currentAngle, targetAngle, RotationSmoothing * Time.fixedDeltaTime);
		transform.rotation = Quaternion.Euler(0f, 0f, smoothAngle);
	}

	public void Move(float direction)
	{
		_moveInput = direction;
	}

	public void Jump()
	{
		if (_isGrounded)
		{
			_rigidbody2D.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);
		}
	}
  
}
