using UnityEngine;
using System.Collections;

public class Gun_1 : MonoBehaviour
{

    public float speed; //rate at which the gun fires
    public GameObject bullet; //type of bullet the gun fires (prefab)
    public float height_Ft; //also note that height_Units = height_Ft / 3, See Height Class Comments.
    private float height_Units;
    //private Bullet_1 lastBulletScript; //I dont believe i should hardcode the type of script in this line of code, but it might work, if all the bullets are the same and the properties are changed later
    private bool isFiring; //if the commmand is issued to fire the gun
    private float count;
    private Transform GunBarrel; //position where bullets come from. (should be the same name no matter the type of gun equipped (hence caps))

    void Start()
    {
        height_Units = height_Ft / 3;
        GunBarrel = transform.Find("GunBarrel");
    }

    // FixedUpdate is called once per 0.2 seconds
    void FixedUpdate()
    {
        count += Time.deltaTime; //counts since the last bullet was fire.
    }

    public void FireBullet()
    {
        if (count >= speed)
        {
            float randomAngle2D = RandomFloat(-4.5f, 3.5f); //Get a random degree for the guns top down innaccuracy
            GameObject lastBullet = Instantiate(bullet, GunBarrel.position, (transform.rotation)) as GameObject;
            lastBullet.transform.rotation *= (Quaternion.AngleAxis(randomAngle2D, lastBullet.transform.forward)); //Im not quite sure how it works, but it takes the random degree and applies it to the bullet.
            //Debug.Log("lastBullet randomAngle2D is equal to " + randomAngle2D);
            
            Bullet_1 lastBulletScript = lastBullet.GetComponent<Bullet_1>(); 
            lastBulletScript.hitInfo.height = height_Units; //at the moment this is how a height is given to a bullet, It should be in Units, which is X(feet) / 3.
            //Debug.Log("lastBulletScript.hitInfo.height = " + height_Units);
            //other code for how unaccurate a bullet should be should stay in this class, and use common variables like Strength and weight that all guns should have, Maybe I should make a interface
            float randomHeightAngle = RandomRadian(-4, 4); //Using radians for sin/cos/tan
            lastBulletScript.hitInfo.angle3D = randomHeightAngle; //At the moment this is how a angle is given to a bullet.
            //Debug.Log("lastBulletScript.hitInfo.angle3D = " + randomHeightAngle);

            count = 0.0f;
        }
    }

    private float RandomRadian(float minDegrees, float maxDegrees)
    {
        return (Random.Range(minDegrees, maxDegrees) * Mathf.Deg2Rad);
    }
    private float RandomFloat(float min, float max)
    {
        return (Random.Range(min, max));
    }
}
