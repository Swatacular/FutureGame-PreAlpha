using UnityEngine;
using System.Collections;

public class AIController : UnitController {

    private float FireDistanceMin_Units;
    private float FireDistanceMax_Units;
    private float MoveDistanceMin_Units;
    private float MoveDistanceMax_Units;

    void Start()
    {
        FireDistanceMin_Units = FireDistanceMin_FT / 3;
        FireDistanceMax_Units = FireDistanceMax_FT / 3;
        MoveDistanceMin_Units = MoveDistanceMin_FT / 3;
        MoveDistanceMax_Units = MoveDistanceMax_FT / 3;
        framesTillStart = 100;
        StartObject();
    }

    Vector3 targetLocation;
    bool fireOrder;
    bool moveOrder;
    int frameCounter;
    int framesTillStart;


    //refactor into base class
    enum DistanceCheck { ToClose, JustRight, ToFar };
    DistanceCheck walkingDistance;
    DistanceCheck firingDistance;

    void FixedUpdate()
    {
        
        if (frameCounter > framesTillStart)
        {
            if (walkingDistance == DistanceCheck.ToFar || walkingDistance == DistanceCheck.ToClose)
            {
                framesTillStart = Random.Range(10, 25);
            }
            else framesTillStart = Random.Range(100, 250);
            moveOrder = true;
            
            frameCounter = 0;
            //Debug.Log("Starts at " + framesTillStart);
        }
        frameCounter++;


    }

    void Update ()
    {
        //get AI commands via input from mouse -- position and buttons
        //  Later this should be done automatically via a method or class.
        UpdateObject();
       
        foreach(RaycastHit2D raycastHit2D in sightedObjects)
        {
            if(raycastHit2D.transform.gameObject.name.Contains("Unit"))
            {
                targetLocation = raycastHit2D.transform.position;
            }
        }

        DistanceUpdate(targetLocation);
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //bool leftClick = Input.GetMouseButton(0);
        //bool rightClick = Input.GetMouseButton(1);
        //moveOrder = leftClick ? true : moveOrder;
        //moveOrder = rightClick ? false : moveOrder;
        //moveOrder = !rightClick && moveOrder || leftClick && rightClick;

        //if (leftClick || rightClick) targetLocation = new Vector2(mousePos.x, mousePos.y);

        if (moveOrder)
        {
            GetComponent<NavMeshAgent2D>().destination = GetNextMovePos();
            moveOrder = false;
        }
        
        isFiring = (LookAt(GetFireOrderAndPos(targetLocation)) && fireOrder == true); //should be shoot order -----------------------------------------------------------
        
    }

    public float FireDistanceMin_FT;
    public float FireDistanceMax_FT;
    public float MoveDistanceMin_FT;
    public float MoveDistanceMax_FT;
    
    private Vector3 GetNextMovePos()
    {
        Vector3 newMovePos;
        
        if (walkingDistance == DistanceCheck.JustRight) //should have a different movement for "JustRight"
        {
            newMovePos = GetMoveTargetPos(targetLocation);
        }
        else
        {
            newMovePos = GetMoveTargetPos(targetLocation);
        }

        return newMovePos;
    }

    protected override Vector3 GetMoveTargetPos(Vector3 target)
    {

        //calculate the direction of travel with this transform and the targetTransform
        Vector3 headingVector = target - transform.position;

        //calculate the distance from targetTransform
        float distance = headingVector.magnitude;

        Vector3 directionVector = headingVector / distance;

        //newtarget = targetTransfrom - distance and direction with +12/0/-12 Feet depending on if they are to close, perfect or to far.
        float walkTowardsTargetAdjustment;
        if (walkingDistance == DistanceCheck.ToClose)
        {
            walkTowardsTargetAdjustment = -4f; //9 ft
        }
        else if (walkingDistance == DistanceCheck.ToFar)
        {
            walkTowardsTargetAdjustment = 4f; //9 ft
        }
        else if (walkingDistance == DistanceCheck.JustRight)
        {
            walkTowardsTargetAdjustment = 0;
        }
        else
        {
            walkTowardsTargetAdjustment = 0;
            Debug.LogError("walkingDistance = " + walkingDistance);
        }

        int strafeAngle;
        int strafeAngle1 = Random.Range(80, 110);
        int strafeAngle2 = Random.Range(-110, -80);

        if ((Random.Range(0, 100) % 2 == 1)) strafeAngle = strafeAngle1; else strafeAngle = strafeAngle2;

        Vector3 newDirection = Quaternion.AngleAxis(strafeAngle, Vector3.forward) * directionVector; //should rotate the direction by roughly 90 degrees (strafe angle)
        Vector3 newTarget = (target - (directionVector * (distance - walkTowardsTargetAdjustment)) + (newDirection * 3));

        //if the newtarget is to close to the targetTransform
        //decide if you want to strafe (or randomly move later) and how far
        //adjust newtarget with direction right/-right the distance you want to strafe
        //return newtarget;
        return newTarget;
    }

    protected override Vector3 GetFireOrderAndPos(Vector3 target)
    {
        if (firingDistance == DistanceCheck.ToClose)
        {
            fireOrder = false; //9 ft
        }
        else if (firingDistance == DistanceCheck.ToFar)
        {
            fireOrder = false; //9 ft
        }
        else if (firingDistance == DistanceCheck.JustRight)
        {
            fireOrder = true;
        }
        else
        {
            fireOrder = false;
            Debug.LogError(transform.name + " is not a valid distance from the object where firingDistance = " + firingDistance);
        }

        return target;
    }

    private void DistanceUpdate(Vector3 target)
    {
        Vector3 headingVector = target - transform.position;

        //calculate the distance from targetTransform
        float distance = headingVector.magnitude;

        FireDistanceUpdate(distance);
        MoveDistanceUpdate(distance);
    }
    private void FireDistanceUpdate(float distance)
    {

        if (distance < FireDistanceMin_Units)
        {
            firingDistance = DistanceCheck.ToClose;
        }
        else if (distance > FireDistanceMax_Units)
        {
            firingDistance = DistanceCheck.ToFar;
        }
        else if (distance > FireDistanceMin_Units && distance < FireDistanceMax_Units)
        {
            firingDistance = DistanceCheck.JustRight;
        }
        else
        {
            Debug.LogError(transform.name + " is not a valid distance from the object where Distance = " + distance);
        }
    }
    private void MoveDistanceUpdate(float distance)
    {
        if (distance < MoveDistanceMin_Units)
        {
            walkingDistance = DistanceCheck.ToClose;
        }
        else if (distance > MoveDistanceMax_Units)
        {
            walkingDistance = DistanceCheck.ToFar;
        }
        else if (distance > MoveDistanceMin_Units && distance < MoveDistanceMax_Units)
        {
            walkingDistance = DistanceCheck.JustRight;
        }
        else
        {
            Debug.LogError(transform.name + " is not a valid distance from the object where Distance = " + distance);
        }
    }
}
