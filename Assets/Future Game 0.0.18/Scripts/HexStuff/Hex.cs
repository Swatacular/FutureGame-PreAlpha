using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hex : MonoBehaviour
{

    public struct HexObject
    {
        public AxialCord cord;
        public GameObject prefab;
        public HexObject(GameObject prefab, AxialCord cord)
        {
            this.cord = cord;
            this.prefab = prefab;
        }
    }
    public struct AxialCord
    {
        public float q;
        public float r;
        public override int GetHashCode()
        {
            return ((int)q * 1000000 + (int)r);
        }
        //constructor
        public AxialCord(float q, float r)
        {
            this.q = q;
            this.r = r;
        }
    }
    public struct ArrayCord
    {
        public int i;
        public int j;
    }
    public struct CubeCord
    {
        public float x;
        public float y;
        public float z;
        public CubeCord(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public static Dictionary<AxialCord, GameObject> GridDictionary = new Dictionary<AxialCord, GameObject>();
    public static float hexWidth;
    public static float hexHeight;
    public static float hexHorizDistance; //The horizontal distance between adjacent hexes
    public static float hexVertDistance;
    public static int gridSize;
    public static float size;
    public static GameObject hexPositionPrefab;
    public static GameObject GridBasedObject;
    public static GameObject NothingPrefab;
    public static GameObject BoundsPrefab;
    public static GameObject GroundPrefab;
    public static GameObject FloorPrefab;
    public static GameObject CoverPrefab;
    // Use this for initialization
    public void HexStart()
    {
        hexHeight = size * 2;
        hexVertDistance = hexHeight * 3 / 4;
        hexWidth = Mathf.Sqrt(3) / 2 * hexHeight;
        hexHorizDistance = hexWidth;
    }

    public static AxialCord ArrayToAxial(ArrayCord arrayPos)
    {
        return new AxialCord( //Im not sure if I want to use the constructor or not
            //q =
            arrayPos.i + gridSize / 2,
            //r = 
            arrayPos.j + gridSize / 2);
    }
    public ArrayCord AxialToArray(AxialCord axialCord) //at the moment it doesnt round.
    {
        ArrayCord array = new ArrayCord();
        array.i = (int)(axialCord.q - gridSize / 2);
        array.j = (int)(axialCord.r - gridSize / 2);
        return array;
    }

    //public Vector2 AxialToPosition(AxialCord axialCord) //this math is done below without the extra variables
    //{
    //    float x = (axialCord.q * hexHorizDistance) + (axialCord.r * hexHorizDistance) / 2;
    //    float y = (axialCord.r * hexVertDistance);
    //    return new Vector2(x, y);
    //}

    //---------------------------
    public static AxialCord PositionToAxial(Vector2 position) //should work, untested (seems kind of messy)
    {
        float q = (position.x * Mathf.Sqrt(3)/3 - position.y / 3) / size;
        float r = position.y * 2/3 / size;
        return AxialRound(new AxialCord(q, r));
    }
    //----------------------------
    public static Vector2 AxialToPosition(AxialCord cord)
    {
    float x = size * Mathf.Sqrt(3) * (cord.q + cord.r / 2);
    float y = size * 3/2 * cord.r;
    return new Vector2(x, y);
    }
    //----------------------------
    public static AxialCord AxialRound(AxialCord cord) //should work, untested
    {
        return CubeToAxial(CubeRound(AxialToCube(cord)));
    }
    //Takes a floating point cube cord and rounds it to the nearest int cord, where x + y + z = 0
    public static CubeCord CubeRound(CubeCord position)
    {
        float rx = Mathf.Round(position.x);
        float ry = Mathf.Round(position.y);
        float rz = Mathf.Round(position.z);

        float x_diff = Mathf.Abs(rx - position.x);
        float y_diff = Mathf.Abs(ry - position.y);
        float z_diff = Mathf.Abs(rz - position.z);

        if ((x_diff > y_diff) && (x_diff > z_diff))
        {
            rx = -ry-rz;
        }
        else if (y_diff > z_diff)
        {
            ry = -rx-rz;
        }
        else
        {
            rz = -rx-ry;
        }
        return new CubeCord(rx, ry, rz);
    }
    
    public static AxialCord CubeToAxial(CubeCord cubeCord)
    {
        AxialCord axial = new AxialCord();
        axial.q = cubeCord.x;
        axial.r = cubeCord.z;
        return axial;
    }
    public static CubeCord AxialToCube(AxialCord axialCord)
    {
        CubeCord cube = new CubeCord();
        cube.x = axialCord.q;
        cube.z = axialCord.r;
        cube.y = (-cube.x) - (cube.z);
        return cube;
    }

    public static float CubeDistance(AxialCord pointA, AxialCord pointB)
    {
        CubeCord cubePointA = AxialToCube(pointA);
        CubeCord cubePointB = AxialToCube(pointB);
        return (Mathf.Abs(cubePointA.x - cubePointB.x) + Mathf.Abs(cubePointA.y - cubePointB.y) + Mathf.Abs(cubePointA.z - cubePointB.z)) / 2;
    }

    public void CreateHex(Object prefab, AxialCord hexCord)
    {
        GameObject checkedObject = null;
        Vector2 position = AxialToPosition(hexCord);
        if (GridDictionary.TryGetValue(hexCord, out checkedObject))
        {
            Debug.Log("GridBasedObject attempted to create a hexPrefab where one already existed.");
        }
        else
        {
            GameObject hexGameObject = Instantiate(prefab, position, transform.rotation) as GameObject;
            GridDictionary.Add(hexCord, hexGameObject);
            hexGameObject.transform.SetParent(transform, true);
        }
    }
}
