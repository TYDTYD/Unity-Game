using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Transform trans;
    private Transform Transform;
    // Start is called before the first frame update
    void Start()
    {
        Transform = GetComponent<Transform>();
        trans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform.position = new Vector3(trans.position.x,trans.position.y+2,-10);
    }
}