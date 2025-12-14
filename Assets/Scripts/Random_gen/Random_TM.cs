using UnityEngine;

public class Random_TM : MonoBehaviour
{
    public Sprite[] Platform = new Sprite[4];
    SpriteRenderer sp;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        sp.sprite = Platform[Random.Range(0, 4)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
