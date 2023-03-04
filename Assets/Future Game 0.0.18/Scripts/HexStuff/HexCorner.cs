using UnityEngine;
using System.Collections;

public class HexCorner : Hex {

    [HideInInspector]
    public int cornerNumber;
    private Vector2 cornerPosition;
    private Vector2 centerOfHex;

	// Use this for initialization
	public void CornerStart () 
    {
        HexStart();
        centerOfHex = transform.position;
        cornerPosition = HexCornerPos(centerOfHex, size, cornerNumber);
        transform.position = new Vector3(cornerPosition.x, cornerPosition.y);
	}

    public Vector2 HexCornerPos(Vector2 center, float size, float cornerNumber)
    {
        float angle_deg = 60 * cornerNumber + 30;
        var angle_rad = Mathf.PI / 180 * angle_deg;
        return new Vector2(center.x + size * Mathf.Cos(angle_rad),
                     center.y + size * Mathf.Sin(angle_rad));
    }
}
