using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjectScript: MonoBehaviour {
    
    public int FrameDelay = 0;
    public Object Prefab;
    private int currentframe = 0;

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
        if (currentframe >= FrameDelay)
            Instantiate(Prefab, transform.position, new Quaternion());
        currentframe++;
    }
}
