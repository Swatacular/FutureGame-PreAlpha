using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq; //not quite sure why I need this for the transform.Cast; But I do.

public abstract class UnitController : MonoBehaviour {

    public float sightAngle;
    public float sightDistance;
    public float turningSpeed;
    public float movementSpeed;
    public int health;
    protected List<RaycastHit2D> sightedObjects;
    protected bool isFiring;
    protected float rotate; //should be a number between -1 to 1
    private Vector3 directionToRotate;
    private Transform[] inventory; //probably could be a list at some point
    private Gun_1 gun; //at some point this should be "Gun" and get a interface to a script (or something along those lines)
    private Transform primaryWeapon;
    public Rigidbody2D rigidbody2D;
    
	// Use this for initialization
	public void StartObject () 
    {
        inventory = gameObject.transform.Cast<Transform>().Where(c => c.gameObject.tag == "Inventory").ToArray(); //Im not sure how this works, but it takes all the child game objects, searches the Tags and adds it to an array
        foreach(Transform item in inventory) //This loop is where each items script should be loaded into the unit controller.
        {
            if (item.name.Contains("PrimaryWeapon")) //This if statement will load a specific item in the array of items (his Inventory)
            {
                primaryWeapon = item;
                gun = primaryWeapon.GetComponentInChildren<Gun_1>();
            }
        }
	}
	// Update is called once per frame from another class
	public void UpdateObject () 
    {


        directionToRotate = new Vector3(0, 0, rotate * turningSpeed); //turns a float into a vector3
        transform.Rotate(directionToRotate);
        

        UpdateSight(sightDistance, sightAngle);
        //Debug.Log("SightedObjects.Count =  " + sightedObjects.Count);
		foreach (RaycastHit2D raycast in sightedObjects) 
		{
			//Debug.Log ("Object = " + raycast.transform.gameObject.name);
		}
        if (isFiring == true)
        {
            gun.FireBullet();
        }


     }

    void HitByBullet(int Damage)
    {
        health -= Damage;
        //Debug.Log("HitByBullet, New health = " + health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //DegAngle should be 360 if I want them to see in full circle
    void UpdateSight(float Distance, float DegAngle)
    {
        //once I get alot of objects, could probably make a specific size array and use RaycastNonAlloc
        List<RaycastHit2D> newSightedObjects = new List<RaycastHit2D>();
        List<GameObject> newSightedGameObjects = new List<GameObject>();

        for (int i = 0; i < DegAngle; i++)
        {
            Vector3 newDirection = Quaternion.AngleAxis((i - DegAngle / 2), Vector3.forward) * transform.up; //should rotate the direction by a few degrees
            //THE 2 is minimum distance, DEBUG HERE ------------------------------ ALSO debug default raycast layers ------------------------------------------
			RaycastHit2D[] CurrentSightline = Physics2D.RaycastAll((transform.position + (transform.up * 2)), newDirection, Distance, (1 << 9));
            
            foreach (RaycastHit2D raycast in CurrentSightline)
            {
                if (!(newSightedGameObjects.Contains(raycast.transform.gameObject)))
                {
                    newSightedObjects.Add(raycast);
                    newSightedGameObjects.Add(raycast.transform.gameObject);
                    // WORKING HERE... DEBUG what objects it can see.
                }
            }
        }
       

        sightedObjects = newSightedObjects;
    }

    protected abstract Vector3 GetFireOrderAndPos(Vector3 target);
    protected abstract Vector3 GetMoveTargetPos(Vector3 target);

    /// <summary>
    /// Rotates toward the position.
    /// Retruns true if you are looking within 20 degrees of the target.
    /// </summary>
    /// <param name="positionToLookAt"></param>
    /// <returns></returns>
    protected bool LookAt(Vector3 positionToLookAt)
    {
        Vector3 unitPosition = transform.position;

        //calculate relative position of target from unit
        Vector3 targetRelPos = positionToLookAt - unitPosition;
        //calculate angle, in degrees, that unit should be facing to face target

        //  need to subtract 90 degrees because transform is rotated (??)
        float newAngle = (Mathf.Atan2(targetRelPos.y, targetRelPos.x) * Mathf.Rad2Deg - 90);
        //  angle with a negitive value should become positive.
        if (newAngle < 0) newAngle += 360;
        //get current angle of unit, in degrees
        float oldAngle = transform.rotation.eulerAngles.z;
        //determine whether unit should rotate right (1) or left (-1) to reach desired angle
        float angleDiff = newAngle - oldAngle;
        //calculate how many degrees off from unit the target is
        float lowestAngleDiff = Mathf.Abs(angleDiff);
        lowestAngleDiff = Mathf.Min(lowestAngleDiff, 360 - lowestAngleDiff);
        //slow/stop rotation if unit is close to desired angle
        //  > 3 degrees: full speed (1)
        //  > 0.5 degrees: 1/3 speed (0.3)
        //  < 0.5 degrees: no rotation (0)

        rotate = ((angleDiff < 180 && angleDiff >= 0) || (angleDiff < -180) ? 1 : -1);
        rotate *= (lowestAngleDiff < 3 ? (lowestAngleDiff < 0.5f ? 0 : 0.3f) : 1);


        //returns if we are looking at the target or not
        return (lowestAngleDiff < 20);
    }
    //new method sightline
    //paramaters:
    //How far you can see
    //the angle that you can see at
    //more values later
    //returns a list of objects with distance and directions

}
