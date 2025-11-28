using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class Menu4 : MonoBehaviour
{
    public GameObject menuCan;

    void Start()
    {
        menuCan.SetActive(false);

    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.B))
        {
            menuCan.SetActive(!menuCan.activeSelf);
        }
    }
}
