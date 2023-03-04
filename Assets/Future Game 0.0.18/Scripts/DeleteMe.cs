using UnityEngine;
using System.Collections;

public class DeleteMe : MonoBehaviour
{

    public int FrameDelay = 0;
    private int currentFrame = 0;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (currentFrame >= FrameDelay) DestroyMe();
        currentFrame++;
    }
    private void DestroyMe()
    {
        DestroyObject(gameObject);
    }
}