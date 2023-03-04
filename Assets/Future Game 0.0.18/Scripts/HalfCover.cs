using UnityEngine;
using System.Collections;

public class HalfCover : MonoBehaviour {

    public float bulletBypassChance;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //if (otherCollider.gameObject.tag.Contains("Bullet"))
        //{
        //    if (randomBoolean())
        //    {
        //        Destroy(otherCollider.gameObject);
        //    }
        //}
    }

    //private bool randomBoolean()
    //{
    //    if (Random.value >= bulletBypassChance)
    //    {
    //        return true;
    //    }
    //    return false;
    //}
}
