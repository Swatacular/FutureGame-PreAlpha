using UnityEngine;
using System.Collections;

public class HexPosition : Hex {

    public GameObject cornerHexPrefab;
    public GameObject hexContentObject;
    //private GameObject[] arrayOfHexCorners = new GameObject[6];
    void Start()
    {
        HexStart();
        //Instantiate(cornerHexPrefab, transform.position, transform.rotation);
        //for (int i = 0; i <= 5; i++) //this creates a object for each corner
        //{
        //    arrayOfHexCorners[i] = Instantiate(cornerHexPrefab, transform.position, transform.rotation) as GameObject;
        //    HexCorner corner = arrayOfHexCorners[i].GetComponent("HexCorner") as HexCorner;
        //    corner.transform.SetParent(transform, true);
        //    corner.cornerNumber = i;
        //    corner.CornerStart();
        //}
    }
	
}
