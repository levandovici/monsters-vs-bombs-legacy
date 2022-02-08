using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private float _fall_speed = 10f;



    private void Awake()
    {
        GetComponent<SpriteRenderer>().flipX = UnityEngine.Random.Range(0, 2) == 0;
    }



    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -32f, 0f), _fall_speed * Time.deltaTime);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
