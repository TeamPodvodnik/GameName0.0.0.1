using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet_Slime : MonoBehaviour
{
    private GameObject _Player;
    Rigidbody2D rb;
    public float speed, timer;
    void Start()
    {
        name = "Bullet";
        _Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        Vector2 direction = Vector2.up * (_Player.transform.position.y - transform.position.y) + Vector2.right * (_Player.transform.position.x - transform.position.x) /
             (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(_Player.transform.position.x, _Player.transform.position.y)) / 3);
        rb.AddForce(direction * speed * Time.deltaTime, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime;
        if (timer <= 0)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name  == "Player") 
            Destroy(gameObject);
    }
}
