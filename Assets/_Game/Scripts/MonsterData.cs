using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MonsterData_", menuName = "UnitData/Monster")]
public class MonsterData : ScriptableObject
{
    [Header("General Status")]
    [SerializeField] private string _name;
    [SerializeField] private MonsterType _monsterType = MonsterType.None;
    [SerializeField][Range(0, 100)] private float _changeToDropItem;
    [SerializeField][Tooltip("How far monster can see")] private float _rangeOfAwareness;

    [Header("Combat")]
    [SerializeField] private int _damage;
    [SerializeField] private int _health;


    [Header("Dialog")]
    [SerializeField][TextArea()] private string _battlecry;

    private void Awake()
    {
        if (_monsterType == MonsterType.None)
        {

        }
    }

    public string Name => _name;
    public MonsterType MonsterType => _monsterType;
}
