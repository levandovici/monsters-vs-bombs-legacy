using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapScrolling : MonoBehaviour
{
    [Range(1, 50)]
    [Header("Controllers")]
    public int panCount;
    [Header("Other Objects")]
    public GameObject panPrefab;

    private void Start() 
    {
        for(int i = 0; i < panCount; i++)
        {
            
        }
    }
}
