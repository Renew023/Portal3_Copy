using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InteractableGrabbable : MonoBehaviour, IInteractable
{
    private bool _isHeld = false;
    private Rigidbody _rb;
    private Transform _holder;

    [Header("Grab Settings")]
    [SerializeField] private float followSpeed = 20f;
    [SerializeField] private float releasePushStrength = 0.5f;

    [Header("Layer Change Settings")]
    [SerializeField] private int heldLayer;

    private int _originalLayer;
    private Quaternion _rotationOffset;
    private float _holdDistance; // 유지 거리

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.freezeRotation = true;
        _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    public void StartGrab(Transform holder, float holdDistance)
    {
        _holder = holder;
        _isHeld = true;
        _holdDistance = holdDistance;

        _originalLayer = gameObject.layer;
        gameObject.layer = heldLayer;

        _rotationOffset = Quaternion.Inverse(_holder.rotation) * transform.rotation;

        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.angularVelocity = Vector3.zero;
    }

    public void StopGrab()
    {
        _isHeld = false;
        _rb.constraints = RigidbodyConstraints.None;

        gameObject.layer = _originalLayer;

        if (_holder != null)
        {
            Vector3 pushDir = (_holder.forward + _holder.up * 0.5f).normalized;
            _rb.velocity += pushDir * releasePushStrength;
        }

        _holder = null;
    }

    private void FixedUpdate()
    {
        if (_isHeld && _holder != null)
        {
            Vector3 targetPos = _holder.position + _holder.forward * _holdDistance;
            Quaternion targetRot = _holder.rotation * _rotationOffset;

            Vector3 dir = targetPos - transform.position;
            _rb.velocity = dir * followSpeed;
            _rb.angularVelocity = Vector3.zero;

            Quaternion smoothedRot = Quaternion.Lerp(_rb.rotation, targetRot, Time.fixedDeltaTime * followSpeed);
            _rb.MoveRotation(smoothedRot);
        }
    }

    public bool IsHeld => _isHeld;

    public void Interact() { } // 기본 Interact는 비워둠

    public bool CanShowUI() => !_isHeld;
}
