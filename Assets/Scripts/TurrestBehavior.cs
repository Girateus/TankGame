using UnityEngine;

public class TurrestBehavior : MonoBehaviour
{
    [SerializeField] Transform _turret;
    [SerializeField] Transform _player;
    
    [SerializeField] GameObject _ammoPrefab;

    [SerializeField] float MaxAngle;
    [SerializeField] float MinAngle;
    [SerializeField] float _rotateTurret = 20f;
    [SerializeField] float _maxDistance = 15f;
    [SerializeField] float _betweenShots = 5f;
    [SerializeField] float _nextTimeToFire;

    private Time _time;
    private float Ypos;
    private float Xpos;
    private float Zpos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null)
        {
            // Tente de trouver le joueur s'il n'est pas assigné dans l'Inspector
            GameObject joueur = GameObject.FindWithTag("Player");
            if (joueur != null)
            {
                if (Time.time <= _nextTimeToFire)
                {
                    
                }
                _player = joueur.transform;
            }
        }
        
        Ypos = Mathf.Clamp(_turret.position.y, MinAngle, MaxAngle);
        Xpos = Mathf.Clamp(_turret.position.x, MinAngle, MaxAngle);
        Zpos = Mathf.Clamp(_turret.position.z, MinAngle, MaxAngle);
        
        float distance = Vector3.Distance(_turret.position, _player.position);
        if (distance <= _maxDistance)
        {
            FollowTarget();
            
        }   
    }
    
    private void FollowTarget()
    {
        Vector3 directionToTarget = new Vector3(_player.position.x - Xpos, _player.position.y, _player.position.z);
        
        Quaternion rotationDesiree = Quaternion.LookRotation(directionToTarget);
        
        _turret.rotation = Quaternion.Slerp(
            _turret.rotation,
            rotationDesiree,
            _rotateTurret * Time.deltaTime);
    }
}
