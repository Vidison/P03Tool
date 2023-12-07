using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;
using Unity.VisualScripting;


public class RoleData : MonoBehaviour
{

    [Header("General Status")]
    [SerializeField] private string _name;
    [SerializeField] private RoleType _roleType = RoleType.None;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _enemy;
    [SerializeField][Range(0, 100)] private float _chanceToDropLoot;
    [SerializeField][Tooltip("How far monster can see")] private float _rangeOfAwareness;

    
    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    public NavMeshAgent _follower;


    [Header("Ranged")] 
    public GameObject enemyBullet;
    public Transform spawnPoint;
    
    [SerializeField] private float timer = 5;
    private float bulletTime;
    public float _shootSpeed;

    [Header("Dialog")]
    [SerializeField][TextArea()] private string approach;

    private void Update()
    {
        if (_roleType == RoleType.Player)
        {
            float horizontalMove = Input.GetAxis("Horizontal");
            float verticalMove = Input.GetAxis("Vertical");
            Vector3 moveDirection = new Vector3(horizontalMove, 0, verticalMove);

            moveDirection.Normalize();
            float magnitude = moveDirection.magnitude;
            magnitude = Mathf.Clamp01(magnitude);

            transform.Translate(moveDirection * _speed * Time.deltaTime, Space.World);
            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotate = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, _rotationSpeed * Time.deltaTime);
            }
        }
        if (_roleType == RoleType.Melee)
        {
            float dist = Vector3.Distance(_player.position, _enemy.position);
            Debug.Log("Distance to other: " + dist);
            if (dist < _rangeOfAwareness + 5 && dist > _rangeOfAwareness + 0.5)
            {
                _follower.ResetPath();
                transform.LookAt(_player);
            } 
            if (dist < _rangeOfAwareness) {
                
                _follower.SetDestination(_player.position);
              
            }
            if (dist > _rangeOfAwareness + 5)
            {
                _follower.isStopped = true;
               
            }
        }
        if (_roleType == RoleType.Ranged)
        {
            
            float distGun = Vector3.Distance(_player.position, _enemy.position);
            Debug.Log("Distance to other: Ranged " + distGun);
            transform.LookAt(_player);
            

            if (distGun < _rangeOfAwareness)
            {
          
                ShootAtPlayer();

            }
            
        }
    }

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

            bulletTime = timer;
         
            GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
            Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
            bulletRig.AddForce(bulletRig.transform.forward * _shootSpeed);
            Destroy(bulletObj, 5);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _rangeOfAwareness);
    }
 
}
