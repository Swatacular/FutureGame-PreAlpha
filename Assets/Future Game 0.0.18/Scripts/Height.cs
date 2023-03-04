using UnityEngine;
using System.Collections;

public class Height : MonoBehaviour
{

    public float TopOfHeight_Ft;
    public float BottomOfHeight_Ft;
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-NOTE-=-=-=-=-=-=-=-=-=--=-=-=-=-=--=
    //Since a unit size is currently one unit wide, and I approximate a unit being 3 feet wide, then 3ft = 1unit, So I devide all numbers
    //given in feet by 3 inside the start method.
    private float TopOfHeight_Units;
    private float BottomOfHeight_Units;



    // Use this for initialization
    void Start()
    {

        TopOfHeight_Units = TopOfHeight_Ft / 3;
        BottomOfHeight_Units = BottomOfHeight_Ft / 3;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Collided(HitInfo hitInfo) 
    {
        if (hitInfo.height3D >= BottomOfHeight_Units && hitInfo.height3D <= TopOfHeight_Units)
        {
            hitInfo.collided = true;
        }
        else
        {
            hitInfo.collided = false;
        }
    }
    public void CollidedGround(HitInfo hitInfo)
    {
        if (hitInfo.height3D >= BottomOfHeight_Units && hitInfo.height3D <= TopOfHeight_Units)
        {
            hitInfo.collidedGround = true;
        }
        else
        {
            hitInfo.collidedGround = false;
        }
    }
}