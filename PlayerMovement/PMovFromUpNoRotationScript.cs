﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

    public CharacterController controller;
    public GameObject playerBody;
    public float speed = 6f;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f){
            controller.Move(direction * speed * Time.deltaTime);
            playerBody.transform.LookAt(direction + transform.position);
        }
    }
}