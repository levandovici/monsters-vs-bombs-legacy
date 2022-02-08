using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float _destroy_after = 0.15f;



    private void Start()
    {
        Destroy(gameObject, _destroy_after);
    }
}
