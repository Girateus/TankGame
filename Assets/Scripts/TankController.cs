using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour
{
    [SerializeField] private float _fwdSpeed = 10f;
    [SerializeField] private float _rtSpeed = 10f;
    [SerializeField] private float _turretSpeed = 10f;
    [SerializeField] private float _cannonSpeed = 10f;
    
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private Transform _tankTurret;
    [SerializeField] private Transform _tankCannon;
    [SerializeField] private ParticleSystem _flash;
    [SerializeField] private ParticleSystem _smoke1;
    [SerializeField] private ParticleSystem _smoke2;
    
    
    private const float MIN_CANNON_ANGLE = -40f; 
    private const float MAX_CANNON_ANGLE = 40f;
    private float _moveInput = 0;
    private float _rotateInput = 0;
    private float _rotateTurret = 0;
    private float _cannonAngle = 0;
    
    private Rigidbody _rigidbody;
    private Animator _animator;
    
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * (_moveInput * _fwdSpeed * Time.deltaTime));   
        //transform.Rotate(Vector3.up * (_rotateInput * _rtSpeed * Time.deltaTime));
        Vector3 velocity = _moveInput * _fwdSpeed * transform.forward;
        _rigidbody.linearVelocity = new Vector3(velocity.x, _rigidbody.linearVelocity.y, velocity.z);
        _rigidbody.angularVelocity = _rotateInput * Mathf.Deg2Rad * _rtSpeed * transform.up;
        
        _tankTurret.Rotate(new Vector3(0,_rotateTurret * _turretSpeed * Time.deltaTime, 0));
        float angleChange = _cannonAngle * _cannonSpeed * Time.deltaTime;
        _tankCannon.Rotate(Vector3.left * angleChange, Space.Self);
        Vector3 currentLocalRotation = _tankCannon.localEulerAngles;
        float normalizedAngleX = currentLocalRotation.x;
        if (normalizedAngleX > 180)
        {
            normalizedAngleX -= 360;
        }
        float clampedAngleX = Mathf.Clamp(normalizedAngleX, MIN_CANNON_ANGLE, MAX_CANNON_ANGLE);
        _tankCannon.localEulerAngles = new Vector3(clampedAngleX, 0f, 0f);
        
        _animator.SetFloat("Velocity", _rigidbody.linearVelocity.magnitude);
    }

    public void OnMoveForward(InputAction.CallbackContext ctx)
    {
        Debug.Log("I'm moving forward : " + ctx.ReadValue<float>());
        _moveInput = ctx.ReadValue<float>();
        _smoke1.Play();
        _smoke2.Play();
    }
    
    public void OnRotate(InputAction.CallbackContext ctx)
    {
        Debug.Log("I'm a spinning : " + ctx.ReadValue<float>());
        _rotateInput = ctx.ReadValue<float>();
    }

    public void OnRotateTurret(InputAction.CallbackContext ctx)
    {
        Debug.Log("I'm a spinning me turret : " + ctx.ReadValue<float>());
        _rotateTurret = ctx.ReadValue<float>();
    }

    public void OnCannonAngle(InputAction.CallbackContext ctx)
    {
        Debug.Log("I'm a adjusting the cannon : " + ctx.ReadValue<float>());
        _cannonAngle = ctx.ReadValue<float>();
        
    }

    public void DoShooting(InputAction.CallbackContext ctx)
    {
        Debug.Log("I'ma blast ya");
        if (ctx.performed)
        {
            Instantiate(_bulletPrefab, _bulletSpawn.position, _bulletSpawn.rotation);
            _flash.Play();
            audioManager.PlaySound(audioManager.TankShoot);
        }
        
        
           
    }
}
