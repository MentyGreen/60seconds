using System;
using System.Collections;
using Cinemachine;
using FMODUnity;
using Rewired;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.SecondsRemaster;
using RG.SecondsRemaster.Menu;
using UnityEngine;

// Token: 0x0200013D RID: 317
public class ThirdPersonController : MonoBehaviour
{
	// Token: 0x06000F6E RID: 3950 RVA: 0x0003FFFC File Offset: 0x0003E1FC
	private void Awake()
	{
		this._player = ReInput.players.GetPlayer(0);
		this._transform = base.transform;
		this._maxForwardVelocityPow = Mathf.Pow(this._maxForwardVelocity, 2f);
		this._rotationMultiplier = 1f;
		EPlayerInput value = (EPlayerInput)this._controlModeVariable.Value;
		this._currentlySetupControlMode = value;
		this._useSensitiveRotation = true;
		switch (value)
		{
		case EPlayerInput.KEYBOARD:
			this._currentRotationSensitivity = this._rotationSensitivityKeyboard;
			this._useSensitiveRotation = false;
			return;
		case EPlayerInput.KEYBOARD_MOUSE:
			this._currentRotationSensitivity = this._rotationSensitivityMouse;
			return;
		case EPlayerInput.GAMEPAD:
			this._currentRotationSensitivity = this._rotationSensitivityGamepad;
			break;
		case EPlayerInput.TOUCH_ANALOGUE:
		case EPlayerInput.TOUCH_DIGITAL:
			break;
		case EPlayerInput.MOUSE_ONLY:
			this._currentRotationSensitivity = this._rotationSensitivityMouse;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x000400BC File Offset: 0x0003E2BC
	private void Start()
	{
		this.SpawnCamera();
	}

	// Token: 0x06000F70 RID: 3952 RVA: 0x000400C4 File Offset: 0x0003E2C4
	private void Update()
	{
		this.CheckIfControlModeChanged();
		this.GetInputs();
		if (this._player.controllers.GetLastActiveController() is Joystick)
		{
			this.CutSmallInputs();
		}
		if (this.CanMove())
		{
			this.ControlAnimation();
		}
	}

	// Token: 0x06000F71 RID: 3953 RVA: 0x00040100 File Offset: 0x0003E300
	private void FixedUpdate()
	{
		this.RotateCharacter();
		if (this.CanMove())
		{
			this.TryToSmoothMove();
			this.MoveCharacter();
			this.LimitVelocity();
			this.ProcessDirectionChange();
			this.TryToActivateTrailParticles();
			return;
		}
		this._lastMoveBlockTimestamp = Time.time;
		this._moveLockEndTimestamp = this._lastMoveBlockTimestamp + this._moveLockTimeout;
		this.TryToResetMove(true, true);
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x00040164 File Offset: 0x0003E364
	private void OnCollisionStay(Collision other)
	{
		Rigidbody rigidbody = other.rigidbody;
		if (rigidbody == null || rigidbody.isKinematic)
		{
			return;
		}
		Vector3 position = this._transform.position;
		Vector3 normalized = (other.contacts[0].point - position).normalized;
		rigidbody.AddForce(normalized * this._pushForce, ForceMode.Impulse);
		if (this.ShouldSpawnCollisionEffect())
		{
			StatsManager.Instance.AddGlobalData("ScavengeCollision", 1);
			this._nextCollisionEffectTime = Time.realtimeSinceStartup + this._collisionEffectTimeout;
			Vector3 position2 = new Vector3(position.x + Random.Range(this._collisionEffectMinOffset.x, this._collisionEffectMaxOffset.x), position.y + Random.Range(this._collisionEffectMinOffset.y, this._collisionEffectMaxOffset.y), position.z);
			Object.Instantiate<GameObject>(this._collisionEffectPrefab, position2, Quaternion.identity).transform.SetParent(this._transform);
			AudioManager.PlaySoundAtPoint(this._scavengeCollisionSoundName, this._transform, 1f, 1f, 0f);
			DestructionEffector component = rigidbody.GetComponent<DestructionEffector>();
			if (component != null)
			{
				component.Hit(10);
			}
		}
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x000402A4 File Offset: 0x0003E4A4
	private void CheckIfControlModeChanged()
	{
		EPlayerInput value = (EPlayerInput)this._controlModeVariable.Value;
		if (this._currentlySetupControlMode != value)
		{
			this._useSensitiveRotation = true;
			switch (value)
			{
			case EPlayerInput.KEYBOARD:
				this._currentRotationSensitivity = this._rotationSensitivityKeyboard;
				this._useSensitiveRotation = false;
				break;
			case EPlayerInput.KEYBOARD_MOUSE:
				this._currentRotationSensitivity = this._rotationSensitivityMouse;
				break;
			case EPlayerInput.GAMEPAD:
				this._currentRotationSensitivity = this._rotationSensitivityGamepad;
				break;
			case EPlayerInput.MOUSE_ONLY:
				this._currentRotationSensitivity = this._rotationSensitivityMouse;
				break;
			}
			this._currentlySetupControlMode = value;
		}
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x00040333 File Offset: 0x0003E533
	private bool ShouldSpawnCollisionEffect()
	{
		return this._collisionEffectPrefab != null && this.HasInput() && this._nextCollisionEffectTime <= Time.realtimeSinceStartup && Random.Range(0, 100) < this._collisionEffectChance;
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x0004036A File Offset: 0x0003E56A
	private void SpawnCamera()
	{
		CinemachineVirtualCamera component = Object.Instantiate<GameObject>(this._thirdPersonCameraPrefab, base.transform).GetComponent<CinemachineVirtualCamera>();
		component.Follow = this._cameraLookAt;
		CinemachineBrain.SoloCamera = component;
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x00040393 File Offset: 0x0003E593
	private void GetInputs()
	{
		this._rotationHorizontal = this._player.GetAxis(14);
		this._moveHorizontal = this._player.GetAxis(2);
		this._moveVertical = this._player.GetAxis(3);
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x000403CC File Offset: 0x0003E5CC
	private void CutSmallInputs()
	{
		if (Mathf.Abs(this._moveHorizontal) < 0.41f)
		{
			this._moveHorizontal = 0f;
		}
		if (Mathf.Abs(this._moveVertical) < 0.41f)
		{
			this._moveVertical = 0f;
		}
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x00040408 File Offset: 0x0003E608
	private bool HasInput()
	{
		return !Mathf.Approximately(this._moveVertical, 0f) || !Mathf.Approximately(this._moveHorizontal, 0f);
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x00040434 File Offset: 0x0003E634
	private void TryToResetMove(bool vertical = true, bool horizontal = true)
	{
		if (vertical || horizontal)
		{
			if (vertical)
			{
				this._currentVerticalForce = 0f;
				this._moveVertical = 0f;
				this._verticalVelocity = 0f;
			}
			if (horizontal)
			{
				this._currentHorizontalForce = 0f;
				this._moveHorizontal = 0f;
				this._horizontalVelocity = 0f;
			}
			if (this._rigidbody != null)
			{
				this._rigidbody.velocity = new Vector3(horizontal ? 0f : this._rigidbody.velocity.x, 0f, vertical ? 0f : this._rigidbody.velocity.z);
			}
		}
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x000404E8 File Offset: 0x0003E6E8
	private bool TryToSmoothMove()
	{
		if (this._moveLockEndTimestamp >= Time.time)
		{
			this._smoothingMultiplier = Mathf.InverseLerp(this._lastMoveBlockTimestamp, this._moveLockEndTimestamp, Time.time);
			return true;
		}
		return false;
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x00040518 File Offset: 0x0003E718
	private void TryToLimitMove(float multiplier, bool vertical = true, bool horizontal = true)
	{
		if (vertical || horizontal)
		{
			if (vertical)
			{
				this._currentVerticalForce *= multiplier;
				this._moveVertical *= multiplier;
				this._verticalVelocity *= multiplier;
			}
			if (horizontal)
			{
				this._currentHorizontalForce *= multiplier;
				this._moveHorizontal *= multiplier;
				this._horizontalVelocity *= multiplier;
			}
			if (this._rigidbody != null)
			{
				this._rigidbody.velocity = new Vector3(horizontal ? (this._rigidbody.velocity.x * multiplier) : this._rigidbody.velocity.x, 0f, vertical ? (this._rigidbody.velocity.z * multiplier) : this._rigidbody.velocity.z);
			}
		}
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x000405F8 File Offset: 0x0003E7F8
	private void MoveCharacter()
	{
		this._currentVerticalForce = Mathf.Lerp(this._currentVerticalForce, this._verticalForce, this._verticalAcceleration * Time.fixedDeltaTime);
		this._currentHorizontalForce = Mathf.Lerp(this._currentHorizontalForce, this._horizontalForce, this._horizontalAcceleration * Time.fixedDeltaTime);
		float num = this._moveVertical * this._currentVerticalForce;
		float num2;
		if (Mathf.Abs(this._moveVertical) > 0.01f && !(this._player.controllers.GetLastActiveController() is Joystick))
		{
			if (this._moveHorizontal > 0.01f)
			{
				num2 = this._maxStrafeVelocity * Time.fixedDeltaTime;
			}
			else if (this._moveHorizontal < -0.01f)
			{
				num2 = -this._maxStrafeVelocity * Time.fixedDeltaTime;
			}
			else
			{
				num2 = 0f;
			}
		}
		else
		{
			num2 = this._moveHorizontal * this._currentHorizontalForce;
		}
		this._rigidbody.AddRelativeForce(new Vector3(num2 * this._smoothingMultiplier, 0f, num * this._smoothingMultiplier), ForceMode.VelocityChange);
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x000406FD File Offset: 0x0003E8FD
	private int GetDirection(float force)
	{
		if (Mathf.Approximately(0f, force))
		{
			return 0;
		}
		if (force > 0f)
		{
			return 1;
		}
		return -1;
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x0004071C File Offset: 0x0003E91C
	private void ProcessDirectionChange()
	{
		int direction = this.GetDirection(this._moveHorizontal);
		int direction2 = this.GetDirection(this._moveVertical);
		bool flag = false;
		bool flag2 = false;
		if (direction != this._lastHorizontalDirection)
		{
			flag = true;
		}
		if (direction2 != this._lastVerticalDirection)
		{
			flag2 = true;
		}
		if (flag || flag2)
		{
			this.TryToLimitMove(this._directionChangeVelocityChange, flag2, flag);
		}
		this._lastHorizontalDirection = direction;
		this._lastVerticalDirection = direction2;
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x00040780 File Offset: 0x0003E980
	private void ControlAnimation()
	{
		bool value = Math.Abs(this._moveHorizontal) > 0f || Math.Abs(this._moveVertical) > 0f;
		this._animator.SetBool(ThirdPersonController.Running, value);
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x000407C8 File Offset: 0x0003E9C8
	private void LimitVelocity()
	{
		bool flag = !Mathf.Approximately(this._moveVertical, 0f);
		bool flag2 = !Mathf.Approximately(this._moveHorizontal, 0f);
		if (!flag)
		{
			this._currentVerticalForce = 0f;
		}
		if (!flag2)
		{
			this._currentHorizontalForce = 0f;
		}
		Vector3 vector = this._transform.InverseTransformDirection(this._rigidbody.velocity);
		this._verticalVelocity = vector.z;
		this._horizontalVelocity = vector.x;
		if (flag2 && vector.sqrMagnitude > this._maxForwardVelocityPow)
		{
			if (this._verticalVelocity > 0f)
			{
				vector = vector.normalized;
			}
			else if (this._verticalVelocity < 0f)
			{
				vector = vector.normalized;
			}
			this._verticalVelocity = vector.z;
			this._horizontalVelocity = vector.x;
		}
		else
		{
			float max = flag ? this.GetCurrentMaxForwardVelocity() : 0f;
			float num = flag ? this.GetCurrentMaxBackwardVelocity() : 0f;
			float num2 = flag2 ? this.GetCurrentMaxStrafeVelocity() : 0f;
			this._verticalVelocity = Mathf.Clamp(this._verticalVelocity, -num, max);
			this._horizontalVelocity = Mathf.Clamp(this._horizontalVelocity, -num2, num2);
			vector = new Vector3(this._horizontalVelocity, vector.y, this._verticalVelocity);
		}
		this._rigidbody.velocity = this._transform.TransformDirection(vector);
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x00040930 File Offset: 0x0003EB30
	private void RotateCharacter()
	{
		if (this._blockMovement || this._rigidbody == null)
		{
			return;
		}
		if (PauseMenuControl.IS_AFTER_PAUSE)
		{
			if (this._rotationHorizontal != 0f)
			{
				this._afterPauseHorizontalInput = this._rotationHorizontal;
				PauseMenuControl.IS_AFTER_PAUSE = false;
				base.StartCoroutine(this.ClearAfterPauseHorizontalInputRoutine());
				return;
			}
		}
		else if (this._rotationHorizontal == this._afterPauseHorizontalInput)
		{
			return;
		}
		Quaternion rotation = this._rigidbody.rotation;
		float num = this._rotationSpeed * this._currentRotationSensitivity.Value;
		this._rotationMultiplier = ((this._controlModeVariable.Value == 3) ? this._gamepadRotationMultiplier : 1f);
		float num2 = num * this._rotationHorizontal * this._rotationMultiplier;
		if (this._useSensitiveRotation)
		{
			num2 *= Time.fixedDeltaTime;
		}
		this._rigidbody.MoveRotation(Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y + num2, rotation.eulerAngles.z));
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x00040A2B File Offset: 0x0003EC2B
	private IEnumerator ClearAfterPauseHorizontalInputRoutine()
	{
		yield return new WaitForSeconds(0.1f);
		this._afterPauseHorizontalInput = 0f;
		yield break;
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x00040A3A File Offset: 0x0003EC3A
	private bool CanMove()
	{
		return !this._animator.GetBool(ThirdPersonController.Grabbing) && !this._animator.GetBool(ThirdPersonController.Dropping) && this._rigidbody != null && !this._blockMovement;
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x00040A7C File Offset: 0x0003EC7C
	private void TryToActivateTrailParticles()
	{
		bool flag = this._verticalVelocity > this._particlesActivationMinVelocity;
		if (this._isSmokeTrailActive && !flag)
		{
			if (Mathf.Approximately(this._horizontalVelocity, 0f))
			{
				this.SetParticlesActive(false);
				return;
			}
		}
		else if (!this._isSmokeTrailActive && flag)
		{
			this.SetParticlesActive(true);
		}
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x00040AD4 File Offset: 0x0003ECD4
	private void SetParticlesActive(bool active)
	{
		for (int i = 0; i < this._trailParticles.Length; i++)
		{
			if (active)
			{
				if (!this._trailParticles[i].gameObject.activeSelf)
				{
					this._trailParticles[i].gameObject.SetActive(true);
				}
				this._trailParticles[i].Play();
			}
			else
			{
				this._trailParticles[i].Stop();
			}
		}
		this._isSmokeTrailActive = active;
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x00040B41 File Offset: 0x0003ED41
	public void SetMovementBlocked(bool block)
	{
		this._blockMovement = block;
		this.StopMovement();
	}

	// Token: 0x06000F87 RID: 3975 RVA: 0x00040B50 File Offset: 0x0003ED50
	public void StopMovement()
	{
		this._animator.SetBool(ThirdPersonController.Running, false);
		this._rigidbody.velocity = Vector3.zero;
		this.SetParticlesActive(false);
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x00040B7A File Offset: 0x0003ED7A
	public void SetMovementLimited(bool limit)
	{
		this._isMovementLimited = limit;
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x00040B83 File Offset: 0x0003ED83
	private float GetCurrentMaxForwardVelocity()
	{
		if (!this._isMovementLimited)
		{
			return this._maxForwardVelocity;
		}
		return this._maxForwardVelocityInPreGame;
	}

	// Token: 0x06000F8A RID: 3978 RVA: 0x00040B9A File Offset: 0x0003ED9A
	private float GetCurrentMaxBackwardVelocity()
	{
		if (!this._isMovementLimited)
		{
			return this._maxBackwardVelocity;
		}
		return this._maxBackwardVelocityInPreGame;
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x00040BB1 File Offset: 0x0003EDB1
	private float GetCurrentMaxStrafeVelocity()
	{
		if (!this._isMovementLimited)
		{
			return this._maxStrafeVelocity;
		}
		return this._maxStrafeVelocityInPreGame;
	}

	// Token: 0x04000952 RID: 2386
	private const int PLAYER_INDEX = 0;

	// Token: 0x04000953 RID: 2387
	private const string RUNNING_ANIM_PARAM = "running";

	// Token: 0x04000954 RID: 2388
	private const string GRABBING_ANIM_PARAM = "grabbing";

	// Token: 0x04000955 RID: 2389
	private const string DROPPING_ANIM_PARAM = "dropping";

	// Token: 0x04000956 RID: 2390
	private const float SMALL_INPUT_THRESHOLD = 0.41f;

	// Token: 0x04000957 RID: 2391
	[SerializeField]
	[Header("References")]
	private Rigidbody _rigidbody;

	// Token: 0x04000958 RID: 2392
	[SerializeField]
	private GameObject _thirdPersonCameraPrefab;

	// Token: 0x04000959 RID: 2393
	[SerializeField]
	private Transform _cameraLookAt;

	// Token: 0x0400095A RID: 2394
	[SerializeField]
	[Header("Character Controller")]
	private float _rotationSpeed;

	// Token: 0x0400095B RID: 2395
	[SerializeField]
	private float _maxForwardVelocity;

	// Token: 0x0400095C RID: 2396
	[SerializeField]
	private float _maxBackwardVelocity;

	// Token: 0x0400095D RID: 2397
	[SerializeField]
	private float _maxStrafeVelocity;

	// Token: 0x0400095E RID: 2398
	[SerializeField]
	private float _maxForwardVelocityInPreGame;

	// Token: 0x0400095F RID: 2399
	[SerializeField]
	private float _maxBackwardVelocityInPreGame;

	// Token: 0x04000960 RID: 2400
	[SerializeField]
	private float _maxStrafeVelocityInPreGame;

	// Token: 0x04000961 RID: 2401
	[SerializeField]
	private float _verticalForce = 1f;

	// Token: 0x04000962 RID: 2402
	[SerializeField]
	private float _horizontalForce = 1f;

	// Token: 0x04000963 RID: 2403
	[SerializeField]
	private float _horizontalAcceleration;

	// Token: 0x04000964 RID: 2404
	[SerializeField]
	private float _verticalAcceleration;

	// Token: 0x04000965 RID: 2405
	[SerializeField]
	private float _directionChangeVelocityChange = 0.5f;

	// Token: 0x04000966 RID: 2406
	[SerializeField]
	private float _moveLockTimeout;

	// Token: 0x04000967 RID: 2407
	[SerializeField]
	private float _pushForce;

	// Token: 0x04000968 RID: 2408
	[Header("Animations and VFX")]
	[SerializeField]
	private Animator _animator;

	// Token: 0x04000969 RID: 2409
	[SerializeField]
	private ParticleSystem[] _trailParticles;

	// Token: 0x0400096A RID: 2410
	[SerializeField]
	private float _particlesActivationMinVelocity;

	// Token: 0x0400096B RID: 2411
	[SerializeField]
	private GameObject _collisionEffectPrefab;

	// Token: 0x0400096C RID: 2412
	[SerializeField]
	[Range(0f, 100f)]
	private int _collisionEffectChance;

	// Token: 0x0400096D RID: 2413
	[SerializeField]
	private float _collisionEffectTimeout;

	// Token: 0x0400096E RID: 2414
	[SerializeField]
	private Vector3 _collisionEffectMinOffset;

	// Token: 0x0400096F RID: 2415
	[SerializeField]
	private Vector3 _collisionEffectMaxOffset;

	// Token: 0x04000970 RID: 2416
	[SerializeField]
	[EventRef]
	private string _scavengeCollisionSoundName;

	// Token: 0x04000971 RID: 2417
	[SerializeField]
	private GlobalFloatVariable _rotationSensitivityMouse;

	// Token: 0x04000972 RID: 2418
	[SerializeField]
	private GlobalFloatVariable _rotationSensitivityGamepad;

	// Token: 0x04000973 RID: 2419
	[SerializeField]
	private GlobalFloatVariable _rotationSensitivityKeyboard;

	// Token: 0x04000974 RID: 2420
	[SerializeField]
	private GlobalIntVariable _controlModeVariable;

	// Token: 0x04000975 RID: 2421
	private EPlayerInput _currentlySetupControlMode;

	// Token: 0x04000976 RID: 2422
	[SerializeField]
	private float _gamepadRotationMultiplier = 10f;

	// Token: 0x04000977 RID: 2423
	private Player _player;

	// Token: 0x04000978 RID: 2424
	private float _rotationHorizontal;

	// Token: 0x04000979 RID: 2425
	private float _moveHorizontal;

	// Token: 0x0400097A RID: 2426
	private float _moveVertical;

	// Token: 0x0400097B RID: 2427
	private Transform _transform;

	// Token: 0x0400097C RID: 2428
	private bool _blockMovement;

	// Token: 0x0400097D RID: 2429
	private bool _isMovementLimited;

	// Token: 0x0400097E RID: 2430
	private bool _useSensitiveRotation = true;

	// Token: 0x0400097F RID: 2431
	private float _rotationMultiplier = 1f;

	// Token: 0x04000980 RID: 2432
	private float _horizontalVelocity;

	// Token: 0x04000981 RID: 2433
	private float _verticalVelocity;

	// Token: 0x04000982 RID: 2434
	private float _currentVerticalForce;

	// Token: 0x04000983 RID: 2435
	private float _currentHorizontalForce;

	// Token: 0x04000984 RID: 2436
	private int _lastHorizontalDirection;

	// Token: 0x04000985 RID: 2437
	private int _lastVerticalDirection;

	// Token: 0x04000986 RID: 2438
	private float _lastMoveBlockTimestamp;

	// Token: 0x04000987 RID: 2439
	private float _moveLockEndTimestamp;

	// Token: 0x04000988 RID: 2440
	private float _smoothingMultiplier = 1f;

	// Token: 0x04000989 RID: 2441
	private bool _isSmokeTrailActive;

	// Token: 0x0400098A RID: 2442
	private static readonly int Grabbing = Animator.StringToHash("grabbing");

	// Token: 0x0400098B RID: 2443
	private static readonly int Dropping = Animator.StringToHash("dropping");

	// Token: 0x0400098C RID: 2444
	private static readonly int Running = Animator.StringToHash("running");

	// Token: 0x0400098D RID: 2445
	private float _maxForwardVelocityPow;

	// Token: 0x0400098E RID: 2446
	private float _nextCollisionEffectTime;

	// Token: 0x0400098F RID: 2447
	private GlobalFloatVariable _currentRotationSensitivity;

	// Token: 0x04000990 RID: 2448
	private float _afterPauseHorizontalInput;
}
