using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 5.0f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    public Camera myCamera;

    public Animator anim;

    public int health = 100;
    private float damageTimer = 0f;
    public float takeHitRadius = 1.5f;
    Transform target;

    private float healthBarLength;
    private int maxHealth = 100;
    

    Vector3 newPosition;
    Vector3 adjustedNewPosition;
    float moveTime;

    bool movementInprog = false;

    void Start()
    {
        newPosition = transform.position;
        adjustedNewPosition = transform.position;
        healthBarLength = Screen.width / 2;
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        MoveWithMouse();


        //WASD Movement
        if (direction.magnitude > 0.1f)
        {
            //Disable Lerp Attempt
            transform.position = Vector3.Lerp(transform.position, transform.position, 0);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            anim.SetBool("Moving", true);

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Moving", false);
        }

        Run();

        AttackEnemy();

    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, healthBarLength, 20), "Health    " + health + "/" + maxHealth);
    }


        void Run()
        {
        //Running
        if (Input.GetKeyDown(KeyCode.LeftShift))
            {
            anim.SetBool("Running", true);
            speed = 5f;
            }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
            anim.SetBool("Running", false);
            speed = 2f;
            }
        }

        
    

    void AttackEnemy()
    {
        //Attack the enemy by clicking
        if (Input.GetMouseButton(0))
        {

            RaycastHit hit;
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    print("Tag is enemy.");
                    // Destroy(hit.transform.gameObject);
                    hit.transform.gameObject.GetComponent<EnemyBehavior>().TakeDamage();
                }
            }
        }
    }

    //Take Damage from the Enemy
    public void TakeDamage()
    {
    if (damageTimer <= 0)
    {
        health -= 10;
        damageTimer = 3.0f;
        anim.SetBool("TakeHit", true);
    }

    }


void MoveWithMouse()
    {
        //Move player to position on playing field by clicking on a spot
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (myCamera == null) print("Null Camera");
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Ground")
                {
                    movementInprog = true;
                    anim.SetBool("Moving", true);
                    newPosition = hit.point;
                    print("new position" + newPosition.x + " " + newPosition.y + " " + newPosition.z );
                    adjustedNewPosition = new Vector3(newPosition.x, transform.position.y, newPosition.z);
                    //      transform.position = newPosition;

                }
            }


        }
        if (movementInprog == true)
        {
            moveTime += Time.deltaTime /500;
            transform.position = Vector3.Lerp(transform.position, adjustedNewPosition, moveTime);
            if (Approximately(transform.position, adjustedNewPosition, 0.1f))
            {
                movementInprog = false;
                anim.SetBool("Moving", false);
            }
            anim.SetBool("Moving", false);
        }
    }

    public bool Approximately(Vector3 myvector, Vector3 othervector, float percentage)
    {
        var dx = myvector.x - othervector.x;

        if (Mathf.Abs(dx) > myvector.x * percentage)
        {
            return false;
        }

        var dz = myvector.z - othervector.z;
       // print("dz " + dz);
        return Mathf.Abs(dz) < myvector.z * percentage;



    }

    //Take Damage when colliding with Enemy
    void OnTriggerEnter(Collider myCollider)
    {
        TakeDamage();
    }

void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, takeHitRadius);
    }
}

