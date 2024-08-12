using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContorl : MonoBehaviour
{
    public float _speed = 5.0f;
    private float _gravity = 10.81f;
    public float _jumpHeight = 5.0f;
    private float _yVelocity;
    private CharacterController _controller;
    public Rigidbody rb;
    private bool _flip = false;
    private bool grabLedge = false;
    //private AudioSource jumpsound;
    Animator animate;
    public Joystick joystick;
    public AudioSource src;
    public AudioClip sfx1,sfx2,sfx3,sfx4;
    private void CalculateMovement(){

        //float horizontalInput = joystick.Horizontal * _speed;
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(-horizontalInput,0,0);
        Vector3 velocity = direction * _speed;

        if( horizontalInput < 0 && _flip == false){
            _flip = true;
            transform.rotation = Quaternion.Euler(transform.rotation.x, 90f, transform.rotation.z);
            src.clip = sfx2;
            src.Play();
        }
        else if(horizontalInput > 0 & _flip == true){
            _flip = false;
            transform.rotation = Quaternion.Euler(transform.rotation.x,-90f,transform.rotation.z);
            src.clip = sfx2;
            src.Play();
        }

        if(animate != null){
            animate.SetFloat("Speed",Mathf.Abs(horizontalInput));
            //Debug.Log(Mathf.Abs(horizontalInput));
        }


        if(_controller.isGrounded == true){
            //Debug.Log("Grounded");

            //if(joystick.vertical > 0.2){
            if(Input.GetButtonDown("Jump")){
                //Debug.Log("jump");
                animate.SetTrigger("Jump");
                _yVelocity = _jumpHeight;
                //jumpsound.Play();
                src.clip = sfx1;
                src.Play();
            }
        }
        else{
            _yVelocity -= _gravity * Time.deltaTime;
        }


        //if(Input.GetButtonDown("Jump")){
            //rb.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
        //}

        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }
    public void GrabLedge(Vector3 position){
        transform.position = position;
        grabLedge = true;
        animate.SetBool("GrabLedge",true);
        //animate.SetBool("Jump",false);
        animate.SetFloat("Speed",0);
        _controller.enabled = false;

        
    }

    // Start is called before the first frame update
    void Start()
    {
        //jumpsound = GetComponent<AudioSource>();
        src = GetComponent<AudioSource>();
        _controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        animate = GetComponent<Animator>();
        if(animate == null){
            Debug.LogError("Animator is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(grabLedge == false){
            CalculateMovement();
        }  
        else{
            //if(joystick.Vertical > 0.1){
            if(Input.GetKeyDown(KeyCode.E)){
                if(animate != null){
                    animate.SetTrigger("ClimbUp");
                    _controller.enabled = true;
                    src.clip = sfx3;
                    src.Play();

                }
            }
        }
        
    }
}
