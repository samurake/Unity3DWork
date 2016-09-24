// Created by samurake: All rights reserved!
// Player Controls for a ball, uses camera coordinates for optimization.
// Features W,A,S,D or arrows(up,down,left,right) movement and Jump(Space).
// Also it has some Game Management lines in it.

﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public Camera cam;
    public Text countText;
    public Text winText;
    private int count;
    public float Speed;
    public float JumpHeight = 4.0f;
    private float HeightON;
    private bool jumping = true;
    private int collidercounter = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }
    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
        if (count >= 15)
            winText.text = "You Win! Congrats!";
    }
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumping == true)
        {
            HeightON = JumpHeight;
            jumping = false;
        }
        if (jumping == true)
        {
            //move according to the camera position
            Vector3 moveHorizontal = cam.transform.right * Input.GetAxisRaw("Horizontal") * Speed;
            Vector3 moveVertical = cam.transform.forward * Input.GetAxisRaw("Vertical") * Speed;
            rb.AddForce(moveHorizontal + moveVertical);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        collidercounter++;
        if (collidercounter >= 2)
        {
            jumping = true;
            collidercounter = 0;
        }
    }
    void FixedUpdate()
    {
        rb.AddForce(0, HeightON, 0, ForceMode.Impulse);
        HeightON = 0;
    }
    //code sequence for score,trigger collider,pickup de-activator
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }
}
