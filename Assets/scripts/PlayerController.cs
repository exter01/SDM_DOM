﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Sprite player_left, player_right, player_front, player_rear;

    public GameObject Player;
    public GameObject Kral;
    public GameObject Hojdacka;
    public GameObject Save_Diskette;
    //public GameObject Listok;
    public static float runSpeed;
    public static float jumpSpeed;
    private float moveX, moveY;
    private Rigidbody2D body2D;
    private Vector3 pozicia;
    bool vprekazke = true;

    // Start is called before the first frame update
    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
        runSpeed = 600; //rychlost hraca
        pozicia = body2D.position;

    }

    void PlayerSpriteChange()//zmena srpite pri pohybe hraca
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Player.GetComponent<SpriteRenderer>().sprite = player_rear;
            //body2D.velocity = new Vector2(-1, body2D.velocity.x) * runSpeed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Player.GetComponent<SpriteRenderer>().sprite = player_left;
            //.velocity = new Vector2(-1, body2D.velocity.y) * runSpeed;
            //body2D.velocity = Vector2.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Player.GetComponent<SpriteRenderer>().sprite = player_right;
            //body2D.velocity = new Vector2(1, body2D.velocity.y) * runSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Player.GetComponent<SpriteRenderer>().sprite = player_front;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (vprekazke == false)
        {
            pozicia = body2D.position;
        }
        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * runSpeed * Time.deltaTime;
        body2D.MovePosition(body2D.transform.position + tempVect);
        //body2D.MovePosition(wantedPosition);
    }

    void Pohyb()//stary pohyb
    {
        //moveX = Input.GetAxis("Horizontal") * Time.deltaTime * runSpeed;
        //moveY = Input.GetAxis("Vertical") * Time.deltaTime * runSpeed;
        //jumpY = Input.GetAxis("Jump") * Time.deltaTime * jumpSpeed;
        //transform.Translate(moveX, moveY, 0f);
        //Debug.Log("4");
    }

    void FixedUpdate()
    {
        //nic
    }

    void kontrola_hranic()//kontrola hranic hry
    {
        if (transform.position.x <= -840)//lavy okraj
        {
            transform.position = new Vector3(-840, transform.position.y, transform.position.z);
        }
        if (transform.position.x >= 834)//pravy okraj
        {
            transform.position = new Vector3(834, transform.position.y, transform.position.z);
        }
        if (transform.position.y >= 90)//horny okraj
        {
            transform.position = new Vector3(transform.position.x, 90, transform.position.z);
        }
        if (transform.position.y <= -414)//spodny okraj
        {
            transform.position = new Vector3(transform.position.x, -414, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSpriteChange(); //menime sprite hraca pri pohybe WASD
        Pohyb(); //pohyb hraca
        kontrola_hranic(); //kontrola hranic hry
        //Debug.Log(transform.position); //zapnutie pozicii v debugu
    }

    void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.gameObject == Hojdacka)
        {
            body2D.position = pozicia;
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject == Hojdacka)
        {
            vprekazke = false;
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject == Kral)
        {
            Debug.Log("The Kral is touched");
            Destroy(obj.gameObject);
            Hra_param.vysvetlovanie_show = true;
        }

        if (obj.gameObject == Hojdacka)
        {
            Debug.Log("The Hojdacka is touched");
            vprekazke = true;
            body2D.position = pozicia;
        }

        if (obj.gameObject == Save_Diskette)
        {
            Debug.Log("The Save_Diskette is touched");
            Hra_param.touch_save_diskette = true;
            //body2D.position = pozicia;
        }

        if (obj.gameObject.CompareTag("Listok"))
        {
            Debug.Log("The Listok is touched");
            Destroy(obj.gameObject);
            Hra_param.listok += 1;
        }
    }
}
