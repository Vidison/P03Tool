using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "RoleData_", menuName = "UnitData/Role")]
public class RoleData : MonoBehaviour
{

    [Header("General Status")]
    [SerializeField] private string _name;
    [SerializeField] private RoleType _roleType = RoleType.None;
    [SerializeField] private Transform _roleToFollow;
    [SerializeField] private Transform _roleFollower;
    [SerializeField][Range(0, 100)] private float _chanceToDropLoot;
    [SerializeField][Tooltip("How far monster can see")] private float _rangeOfAwareness;

    public NavMeshAgent _follow;

    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationspeed;


    [Header("Dialog")]
    [SerializeField][TextArea()] private string _approach;

    private void Awake()
    {

    }

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
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, _rotationspeed * Time.deltaTime);
            }
        }
        if (_roleType == RoleType.Enemy)
        {
            float dist = Vector3.Distance(_roleToFollow.position, _roleFollower.position);
            Debug.Log("Distance to other: " + dist);
            if (dist < _rangeOfAwareness + 5 && dist > _rangeOfAwareness)
            {
                transform.LookAt(_roleToFollow);
            } else if (dist < _rangeOfAwareness) {
                _follow.SetDestination(_roleToFollow.position);
            }

            
            //transform.position = Vector3.MoveTowards(this.transform.position, _roleToFollow.position, _speed * Time.deltaTime);
        }
    }

    public string Name => _name;
    public RoleType RoleType => _roleType;
}
