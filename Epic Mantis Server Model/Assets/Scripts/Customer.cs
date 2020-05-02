using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public Line line;
    public float speed = 5f;
    public Animator animator;
    public Vector3 doorway; //The entrance to the store
    public Vector3 theVoid; //The spawn & delete point for customers
    public Renderer render;
    public Material defaultMat;
    public Material redMat; //For customers that leave early
    public Material greenMat; //For customers that successfully leave
    public Material yellowMat; //For line jumpers

    private Vector3 destination;
    public bool moving = false;
    private bool entered = false; //Customer must enter store before getting in line
    private bool leaving = false; //Customer is leaving the store
    
    void Start() // Start is called before the first frame update
    {
        //Random varaible generation will go here  
        //doorway = GameObject.FindGameObjectWithTag("Doorway").transform.position;
        //theVoid = GameObject.FindGameObjectWithTag("TheVoid").transform.position;
        this.transform.position = theVoid;
        animator.SetBool("Moving", true);
        if(!line.addCustomer(this)) //Returns false if line is full
        {
            leaving = true;
        }
    }

    void Update() //Runs every frame
    {
       float step;
        if (!entered) //Enter Store
        {
            step = speed * Time.deltaTime; // calculate distance to move
            this.transform.position = Vector3.MoveTowards(this.transform.position, doorway, step);
            if (Vector3.Distance(this.transform.position, doorway) < 0.001f) //Check if reached destination
            {
                this.transform.position = doorway;
                entered = true;
                moving = true;
                if (leaving) //Line is full better leave
                {
                    render.material = redMat;
                    destination = theVoid;
                    leaving = true;
                    moving = true;
                    this.transform.Rotate(0.0f, 180.0f, 0.0f);
                    animator.SetBool("Moving", true);
                }
                //animator.SetBool("Moving", false);
                //moving = false;
            }
        }
        else
        {
            if (moving)
            {
                step = speed * Time.deltaTime; // calculate distance to move
                this.transform.position = Vector3.MoveTowards(this.transform.position, destination, step);
                if (Vector3.Distance(this.transform.position, destination) < 0.001f) //Check if reached destination
                {
                    if (leaving)
                    {
                        if (destination == theVoid) //Customer has reached the void, their time is at an end
                        {
                            Destroy(gameObject); //REMOVE CUSTOMER
                        }
                        else
                            moveTo(theVoid);
                    }
                    else
                    { 
                    this.transform.position = destination;
                    moving = false;
                    animator.SetBool("Moving", false);
                    }
                }
            }
        }
    }

    public void moveTo(Vector3 target)
    {
        destination = target;
        moving = true;
        animator.SetBool("Moving", true);
    }


    public void leaveStore()
    {
        render.material = greenMat;
        destination = doorway;
        leaving = true;
        moving = true;
        this.transform.Rotate(0.0f,180.0f,0.0f);
        animator.SetBool("Moving", true);
    }


}
