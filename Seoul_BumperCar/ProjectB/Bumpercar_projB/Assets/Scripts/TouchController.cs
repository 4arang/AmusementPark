using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchController : MonoBehaviour, Interface_Item
{
    [SerializeField] private Slider powerGauge;
    [SerializeField] private GameObject carHitEffect;

    [Header("reference from car")]
    public float gaugeRef;  //filling gauge speed
    public float speedRef;  //running speed
    public float powerRef;  //collision power
    public float weight; //car wieght

    [Header("Basic info")]
    public float power;
    public float orgPos;

    [Header("Item Skill")]
    private bool fixPosition=false;

    private bool touchable = true;
    private bool moveAllowed;
    private bool powerHandled;
    private bool directionHandled;

    Collider col;
    Rigidbody rb;
    PhysicMaterial physicMat;


    private void Start()
    {
        gaugeRef = 20;
        speedRef = 20;
        powerRef = 0.1f;
        weight = 2000;

        col = GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>();
        physicMat = col.material;

        rb.mass = weight;
        powerGauge.maxValue = 100;
        powerGauge.gameObject.SetActive(false);
        
    }
    void FixedUpdate()
    {
        if (Input.touchCount>0 &&touchable)
        {
            Touch touch = Input.GetTouch(0);
           // float touchPositionX = touch.position.x; // Camera.main.ScreenToWorldPoint(touch.position).x;

            if(touch.phase == TouchPhase.Began)
            {
                orgPos = touch.position.x; //initialize
                power = 0;
                powerHandled = true;
                directionHandled = true;
                powerGauge.gameObject.SetActive(true);
                powerGauge.value = 0;
            }
            if (touch.phase == TouchPhase.Stationary ||touch.phase == TouchPhase.Moved ) //speed control || direction control
            {
                if (powerHandled)
                {
                    power += Time.deltaTime*gaugeRef;
                    if (power > 100)
                    {
                        power = 100;
                    }
                    powerGauge.value = power;

                }
                if (directionHandled)
                {
                    float rotate = touch.position.x-orgPos;
                   // Debug.Log("touchpointX " + touch.position.x);
                   // Debug.Log("rotate " + rotate);
                    transform.rotation = Quaternion.AngleAxis(rotate, Vector3.up);
                }
               //Debug.Log("where to go " + transform.forward);
            }
  
            if (touch.phase == TouchPhase.Ended)
            {
                touchable = false;
                powerHandled = false;
                directionHandled = false;
                powerGauge.gameObject.SetActive(false);
                moveAllowed = true;
                Debug.Log("Power "+ power);
            }
        }
        if (moveAllowed)
        {
            float moveFrame = power * Time.deltaTime;
            moveForward(moveFrame);
           // Debug.Log("moveFrame " + moveFrame);
        }
        Debug.Log("current speed " + rb.velocity.magnitude);

        if (rb.velocity.magnitude < 0.125f) touchable = true;
    }

    private void moveForward(float speed)
    {
        speed *= speedRef;
        rb.velocity = new Vector3(transform.forward.x*speed,0, transform.forward.z*speed);


        //Vector3 dir = Vector3.RotateTowards(transform.forward, )
        //transform.Translate( dir*speed);
        //Debug.Log("normal "+transform.forward);
        //Debug.Log("speed " +transform.forward * speed);

    }

    private void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.CompareTag("Car"))
        {
            moveAllowed = false;
            Vector3 collisionPos = (collision.transform.position+transform.position)*0.5f;
            Instantiate(carHitEffect, collisionPos, Quaternion.identity);
            rb.AddExplosionForce(power*powerRef, collisionPos, powerRef*100, 0, ForceMode.Force);  
            if(fixPosition)
            {
                rb.MovePosition(transform.position);
            }
        }
    }

    public void GotItem(Item.ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case Item.ItemType.FixPosition: fixPosition=true; break;
            case Item.ItemType.AddLife: break;
            case Item.ItemType.AccelGauge: break;
            case Item.ItemType.ExplodeEnemy: break;
            case Item.ItemType.DismissEffect: break;
            case Item.ItemType.DismissDebuff: break;
            case Item.ItemType.GetShield: break;
            case Item.ItemType.ReduceImpulse: break;
        }
    }


}
