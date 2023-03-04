using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexGrid : Hex {

    public GameObject SetPositionPrefab;
    public GameObject SetGridBasedObject;
    public GameObject SetNothingPrefab;
    public GameObject SetBoundsPrefab;
    public GameObject SetGroundPrefab;
    public GameObject SetFloorPrefab;
    public GameObject SetCoverPrefab;
    public float hexSize;
    public int hexGridSize;
    //GameObject[,] hexGridArray;
	// Use this for initialization
	void Start () 
    {
        size = hexSize;
        gridSize = hexGridSize;
        hexPositionPrefab = SetPositionPrefab;
        GridBasedObject = SetGridBasedObject;
        NothingPrefab = SetNothingPrefab;
        BoundsPrefab = SetBoundsPrefab;
        GroundPrefab = SetGroundPrefab;
        FloorPrefab = SetFloorPrefab;
        CoverPrefab = SetCoverPrefab;
        //hexGridArray = new GameObject[gridSize, gridSize];
        HexStart();
        //loops through a nice square (roumbus) based on gridSize

        CreateStuff(); //creates a bunch of objects around the map.

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                AxialCord axialCord = new AxialCord();
                axialCord.q = (i - (int)(gridSize / 2));
                axialCord.r = (j - (int)(gridSize / 2));
                //gets a distance from center to every position in the square,
                {
                    float distance = CubeDistance(new AxialCord(0, 0), axialCord);
                    if (distance <= gridSize / 2)
                    {
                        if (distance > (gridSize / 2) - 4)
                        {
                            CreateHex(BoundsPrefab, axialCord);
                        }
                        else
                        {
                            CreateHex(GroundPrefab, axialCord);
                        }
                    }
                }
            }
        }
	}

    public void CreateStuff()
    {

        AxialCord gridBasedObjectCord = new AxialCord(10, 10);
        GameObject gridBasedObject = Instantiate(GridBasedObject, AxialToPosition(gridBasedObjectCord), transform.rotation) as GameObject;
        GridBasedObject gridBasedObjectClass = gridBasedObject.GetComponent("GridBasedObject") as GridBasedObject;
        gridBasedObjectClass.CreateGridBasedObject(gridBasedObjectCord);

        gridBasedObjectCord = new AxialCord(-10, 0);
        gridBasedObject = Instantiate(GridBasedObject, AxialToPosition(gridBasedObjectCord), transform.rotation) as GameObject;
        gridBasedObjectClass = gridBasedObject.GetComponent("GridBasedObject") as GridBasedObject;
        gridBasedObjectClass.CreateGridBasedObject(gridBasedObjectCord);

        gridBasedObjectCord = new AxialCord(-10, -10);
        gridBasedObject = Instantiate(GridBasedObject, AxialToPosition(gridBasedObjectCord), transform.rotation) as GameObject;
        gridBasedObjectClass = gridBasedObject.GetComponent("GridBasedObject") as GridBasedObject;
        gridBasedObjectClass.CreateGridBasedObject(gridBasedObjectCord);

        gridBasedObjectCord = new AxialCord(10, 0);
        gridBasedObject = Instantiate(GridBasedObject, AxialToPosition(gridBasedObjectCord), transform.rotation) as GameObject;
        gridBasedObjectClass = gridBasedObject.GetComponent("GridBasedObject") as GridBasedObject;
        gridBasedObjectClass.CreateGridBasedObject(gridBasedObjectCord);

        gridBasedObjectCord = new AxialCord(0, -10);
        gridBasedObject = Instantiate(GridBasedObject, AxialToPosition(gridBasedObjectCord), transform.rotation) as GameObject;
        gridBasedObjectClass = gridBasedObject.GetComponent("GridBasedObject") as GridBasedObject;
        gridBasedObjectClass.CreateGridBasedObject(gridBasedObjectCord);

        gridBasedObjectCord = new AxialCord(0, 10);
        gridBasedObject = Instantiate(GridBasedObject, AxialToPosition(gridBasedObjectCord), transform.rotation) as GameObject;
        gridBasedObjectClass = gridBasedObject.GetComponent("GridBasedObject") as GridBasedObject;
        gridBasedObjectClass.CreateGridBasedObject(gridBasedObjectCord);

        gridBasedObjectCord = new AxialCord(-5, -10);
        gridBasedObject = Instantiate(GridBasedObject, AxialToPosition(gridBasedObjectCord), transform.rotation) as GameObject;
        gridBasedObjectClass = gridBasedObject.GetComponent("GridBasedObject") as GridBasedObject;
        gridBasedObjectClass.CreateGridBasedCoverObject(gridBasedObjectCord);

        gridBasedObjectCord = new AxialCord(5, 10);
        gridBasedObject = Instantiate(GridBasedObject, AxialToPosition(gridBasedObjectCord), transform.rotation) as GameObject;
        gridBasedObjectClass = gridBasedObject.GetComponent("GridBasedObject") as GridBasedObject;
        gridBasedObjectClass.CreateGridBasedCoverObject(gridBasedObjectCord);

    }
}
