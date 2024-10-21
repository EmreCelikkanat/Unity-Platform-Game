using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float speed = 1.5f;
    public Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos= new Vector3(player.position.x,player.position.y,-10f);
        transform.position = Vector3.Slerp(transform.position, pos, speed*Time.deltaTime);
    }
}
