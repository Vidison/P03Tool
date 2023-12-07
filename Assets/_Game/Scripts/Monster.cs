using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private MonsterData _data;
    public MonsterData MonsterData => _data;

    private void Awake()
    {
        Debug.Log("Name: " + _data.Name);
    }
}
