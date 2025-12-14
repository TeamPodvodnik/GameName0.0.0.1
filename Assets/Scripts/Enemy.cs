using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject[] point = new GameObject[8];
    private GameObject _Player;
    public GameObject Bullet;
    private int action, rand = 0;
    public float speed = 1.0f;
    private float _timer = 2f;
    void Start()
    {
        _Player = GameObject.Find("Player");
    }

    void Update()
    {
     /*   if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(_Player.transform.position.x, _Player.transform.position.y)) < 6f)
            action = 1;
        else action = 0; */



        if (action == 0)
        {
            if (point[rand].transform.parent != null)
                for (int i = 0; i < point.Length; i++)
                    point[i].transform.parent = null;
            if (transform.position != point[rand].transform.position)
                transform.position = Vector3.MoveTowards(transform.position, point[rand].transform.position, speed * Time.deltaTime);
            else
                rand = Random.Range(0, 8); // gпробовать разные цифры
        }
        else if (action == 1)
        {
            if (point[rand].transform.parent != null)
                for (int i = 0; i < point.Length; i++)
                    point[i].transform.parent = transform;
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(_Player.transform.position.x, _Player.transform.position.y)) < 1.4f)
                transform.position = Vector3.MoveTowards(transform.position, _Player.transform.position * -1, speed * Time.deltaTime);
            else
                transform.position = Vector3.MoveTowards(transform.position, _Player.transform.position, speed * Time.deltaTime);
            if(_timer >= 0f)
                _timer-= Time.deltaTime;
            else
            {
                _timer = 2;
                Instantiate(Bullet, transform.position, Quaternion.identity); 
            }


        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(action == 0)
            rand = Random.Range(0, 8); // тоже последнее число поокрутить
    }
}
