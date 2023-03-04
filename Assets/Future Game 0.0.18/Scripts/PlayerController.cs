using UnityEngine;
using System.Collections;

public class PlayerController : UnitController {

    public string fireButton;
    public string HorizInput;
    public string VertInput;
    Vector3 mousePos;
    private float up; //should be a number between -1 and 1
    private float right; //should be a number between -1 and 1

    protected override Vector3 GetFireOrderAndPos(Vector3 target)
    {
        
        return mousePos;
    }

    // Use this for initialization
    void Start () {

        StartObject();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
	
	// Update is called once per frame
	void Update () {

        UpdateObject();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rigidbody2D.velocity = new Vector2(0, up * movementSpeed);
        rigidbody2D.velocity += new Vector2(right * movementSpeed, 0);
        //NEED TO REWORK for angles.. at the moment you move twice as fast at an angle.


        up = Input.GetAxis(VertInput);
        right = Input.GetAxis(HorizInput);


        if (LookAt(GetFireOrderAndPos(mousePos)) && Input.GetButton(fireButton))
        {
            isFiring = true;
        }
        else isFiring = false;
	}
    
    //could use this method and the pathfinding asset 
    protected override Vector3 GetMoveTargetPos(Vector3 target)
    {
        return target;
    }
}
