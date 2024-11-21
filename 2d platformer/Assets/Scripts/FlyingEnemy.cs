using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
public int damage;
public float movespeed;
public float distance;
Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float currentDistance = Vector2.Distance(transform.position, target.position);
       
        if (currentDistance < distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, movespeed * Time.deltaTime);
        }
    }
}
