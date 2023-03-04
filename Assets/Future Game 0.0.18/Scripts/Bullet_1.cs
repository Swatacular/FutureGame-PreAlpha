using UnityEngine;
using System.Collections;

public class HitInfo
{
    public float height; //the starting height of the bullet.
    public bool collided; //If the bullet hit pretty much anything else
    public bool collidedGround; //If the bullet hit the ground
    public Vector2 originVector2; //Location the bullet was shot from
    public float angle3D; //in radians
    public float height3D; //Current height updated in FixedUpdate
}
public class Bullet_1 : MonoBehaviour
{
    public HitInfo hitInfo = new HitInfo();
    public float Health;
    public float Speed;
    public float Damage;
    private Rigidbody2D rigidbody2D;

    // Use this for initialization
    void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = (transform.up * Speed);
        transform.localScale = new Vector3 (1,(Speed / 8),1);
        hitInfo.originVector2 = rigidbody2D.position;
        //Also should get a angle from gun or gunbarrel.
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        Vector2 hitOriginVector2 = hitInfo.originVector2;
        float distance2D_H = Vector2.Distance(hitOriginVector2, transform.position);
        hitInfo.height3D = (hitInfo.height + (Mathf.Tan(hitInfo.angle3D) * distance2D_H));
        if (Health <= 0)
        {
            DestroyObject(gameObject);
        }
        if (hitInfo.height3D <= 0)
        {
            Instantiate(Resources.Load("Prefabs\\Spark_1"), transform.position, new Quaternion());
            DestroyObject(gameObject);
        }
        //should update height based on angle given.
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        otherCollider.gameObject.SendMessage("Collided", hitInfo, SendMessageOptions.DontRequireReceiver);
        //Debug.Log("If hitInfo == true was called.");
        if (hitInfo.collided == true)
        {
            //Debug.Log("hitInfo returned true.");
            Health -= 0.2f; //TEMP.. This number should depend on the object it collided with or its own speed/puncturing values.
            
            if (otherCollider.gameObject.tag.Contains("Unit")) //sends a message to a object (if its a unit) so that it knows it was hit and reduces health
            {
                //Debug.Log("SendMessageUpwards(HitByBullet) was called");
                otherCollider.gameObject.SendMessageUpwards("HitByBullet", Damage);

                Instantiate(Resources.Load("Prefabs\\Hit Mark_X"), transform.position, new Quaternion());

            } else
            {
                Instantiate(Resources.Load("Prefabs\\Spark_1"), transform.position, new Quaternion());
            }
        }
    }
}
//This is how finding the height works.. (it is now located inside the FixedUpdate Method of the bullet)
////Vector2 hitVector2 = hitInfo.originVector2;

//float hitX = hitOriginVector2.x; //Getting some easier to understand names
//float hitY = hitOriginVector2.y;
////Debug.Log("hitOriginVector2.x = " + hitX + " and hitOriginVector2.y " + hitY);
//float thisX = transform.position.x; //Same here.
//float thisY = transform.position.y;
////Debug.Log("transform.position.x = " + thisX + " and transform.position.y " + thisY);

//float oppositeSqrd = hitX - thisX; //Subtracting to get a triangle that should be the distance in one direction
//float adjacent = hitY - thisY; //^^ same but the second angle

//float hypotenuseSquared = ((oppositeSqrd * oppositeSqrd) + (adjacent * adjacent));
//float distance2D_H = Mathf.Sqrt(hypotenuseSquared);
//float distance2D_Hsqrd = 
//    (((transform.position.x - originVector2.x)
//    * (transform.position.x - originVector2.x))
//    + ((transform.position.y - originVector2.y)
//    * (transform.position.y - originVector2.y)));
//float distance2D_H = Mathf.Sqrt(
//    ((transform.position.x - originVector2.x)
//    * (transform.position.x - originVector2.x))
//    + ((transform.position.y - originVector2.y)
//    * (transform.position.y - originVector2.y)));
//when I switch from looking top down, to looking at the bullet from the side, the hypotenuse becomes the adjacent side of the height triangle
//this is all done for me in Vector2.Distance(a,b);

////float distance2D_H = Vector2.Distance(hitVector2, transform.position);

//remember Sohcahtoa (Sine(angle) = Opposite / Hypo, etc) //Also remember opposite2 + adjacent2 = hypotenuse2
////float height3D_O = (hitInfo.height + (Mathf.Tan(hitInfo.angle3D) * distance2D_H)); //Tan(angle) = Opposite / Adjacent.. Solve for Opposite.. (which when looking at it from the side, is the height

//Debug.Log("The height3D_O of the collided Object is " + height3D_O);
//Debug.Log("The distance2D_H of the collided Object is " + distance2D_H);
//Debug.Log("The hitInfo.angle3D is equal to " + hitInfo.angle3D);
//Debug.Log("Mathf.Tan(hitInfo.angle3D) is equal to " + (Mathf.Tan(hitInfo.angle3D)));
//Debug.Log("TopOfHeight_Units is equal to " + TopOfHeight_Units);
//Debug.Log("BottomOfHeight_Units is equal to " + BottomOfHeight_Units);