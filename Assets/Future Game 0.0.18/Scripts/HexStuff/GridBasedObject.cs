using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridBasedObject : Hex
{
    List<HexObject> listOfHexes = new List<HexObject>();
    public AxialCord centerPosition = new AxialCord();
    // Use this for initialization

    public void CreateGridBasedObject(AxialCord CenterPosition)
    {
        centerPosition = CenterPosition;
        Debug.Log("CreateGridBasedObject was called");
		HexStart();
		listOfHexes.Add(new HexObject(BoundsPrefab, new AxialCord(0, 1)));
		listOfHexes.Add(new HexObject(BoundsPrefab, new AxialCord(1, 0)));
		listOfHexes.Add(new HexObject(BoundsPrefab, new AxialCord(-1, 1)));
		listOfHexes.Add(new HexObject(BoundsPrefab, new AxialCord(1, -1)));
		listOfHexes.Add(new HexObject(BoundsPrefab, new AxialCord(-1, 0)));
		listOfHexes.Add(new HexObject(BoundsPrefab, new AxialCord(0, -1)));
        listOfHexes.Add(new HexObject(FloorPrefab, new AxialCord(0, 0)));
            foreach (HexObject individualHex in listOfHexes)
            {
                Debug.Log("individualHex = " + individualHex);
                AxialCord individualHexCord = individualHex.cord;
                //adds the distance from the center of the grid to the center of the object, to the distance from the center of the object to the current hex's cord.
                individualHexCord.q += centerPosition.q;
                individualHexCord.r += centerPosition.r;
                CreateHex(individualHex.prefab, individualHexCord);
            }
    }
    public void CreateGridBasedCoverObject(AxialCord CenterPosition)
    {
        centerPosition = CenterPosition;
        Debug.Log("CreateGridBasedObject was called");
        HexStart();
        listOfHexes.Add(new HexObject(CoverPrefab, new AxialCord(0, 1)));
        listOfHexes.Add(new HexObject(CoverPrefab, new AxialCord(1, 0)));
        listOfHexes.Add(new HexObject(CoverPrefab, new AxialCord(-1, 1)));
        listOfHexes.Add(new HexObject(CoverPrefab, new AxialCord(1, -1)));
        listOfHexes.Add(new HexObject(CoverPrefab, new AxialCord(-1, 0)));
        listOfHexes.Add(new HexObject(CoverPrefab, new AxialCord(0, -1)));
        listOfHexes.Add(new HexObject(BoundsPrefab, new AxialCord(0, 0)));
        foreach (HexObject individualHex in listOfHexes)
        {
            Debug.Log("individualHex = " + individualHex);
            AxialCord individualHexCord = individualHex.cord;
            //adds the distance from the center of the grid to the center of the object, to the distance from the center of the object to the current hex's cord.
            individualHexCord.q += centerPosition.q;
            individualHexCord.r += centerPosition.r;
            CreateHex(individualHex.prefab, individualHexCord);
        }
    }
}
