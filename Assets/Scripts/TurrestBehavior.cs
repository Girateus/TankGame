using System;
using System.Collections;
using UnityEngine;

public class TurrestBehavior : MonoBehaviour
{
    [SerializeField] private float _detectionRange;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _dps;
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private Transform _turretCannon;
    [SerializeField] private float _lerpCompensation;
    [SerializeField] private LineRenderer _laser;
    [SerializeField] private Transform _laserPoint;
    
    [SerializeField] private LayerMask _layers;
    
    private bool _playerDetected = false;
    AudioManager audioManager;

    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!_playerPosition)
        {
            _playerPosition = GameObject.FindWithTag("Tank").transform;
        }
        
        _laser.enabled = false;
       
    }
    
    // Update is called once per frame
    void Update()
    {
     //_turretCannon.LookAt(_playerPosition);
     
     if (_playerPosition != null )
     {
         Vector3 playerDirection = _playerPosition.position - _turretCannon.position;
         if (playerDirection.magnitude < _detectionRange)
         {
             if (_playerDetected == false)
             {
                 StartCoroutine(ShootSequence_co());
                 _playerDetected = true;
             }
             _turretCannon.rotation = Quaternion.Lerp(_turretCannon.rotation, Quaternion.LookRotation(playerDirection), _lerpCompensation * Time.deltaTime);  
         }
         else
         {
             StopAllCoroutines();
             //StopCoroutine(ShootSequence_co());
             _laser.enabled = false;
             _playerDetected = false;
             _turretCannon.rotation = Quaternion.Lerp(_turretCannon.rotation, Quaternion.LookRotation(Vector3.forward), _lerpCompensation * Time.deltaTime);
         }
         
     }
     else
     {
         StopAllCoroutines();
         _laser.enabled = false;
     }
     
     if (_laser.enabled)
     {
         _laser.SetPosition(0, _laserPoint.position);
         _laser.SetPosition(1, _playerPosition.position);
     }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_turretCannon.position, _detectionRange);
    }

    private void DoLaserShoot()
    {
        if (Physics.Raycast(_turretCannon.position, _turretCannon.forward, out RaycastHit hit, Mathf.Infinity, _layers))
        {
            Debug.Log("hit something" + hit.collider.gameObject.name);
           // if (hit.collider.CompareTag("Tank"))
           if (hit.collider.gameObject.TryGetComponent(out DamageTaker damageTaker))
            {
                damageTaker.TakeDamage(_dps * Time.deltaTime);
                
                _laser.enabled = true;
                _laser.SetPosition(0,_laserPoint.position);
                _laser.SetPosition(1, hit.point);
                audioManager.PlaySound(audioManager.TurretsShoot);
                
                Debug.DrawRay(_turretCannon.position, _turretCannon.forward * 100, Color.green, 0.25f);
            }
            else
            {
               _laser.enabled = false;
            }

            //if (hit.collider.gameObject.TryGetComponent(out TankController tank))
            //{
            //    Debug.DrawRay(_turretCannon.position, _turretCannon.forward * 100, Color.green);
            //}
        }
        else
        {
            Debug.DrawRay(_turretCannon.position, _turretCannon.forward * 100, Color.red, 0.25f);
            _laser.enabled = false;
        }
    }
    
    private IEnumerator ShootSequence_co()
    {
        do
        {
            DoLaserShoot();
            yield return new WaitForSeconds(_fireRate);
        } while (true);


    }
}
