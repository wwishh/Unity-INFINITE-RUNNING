using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle_control : MonoBehaviour
{
    public obstacle_creator obstacle_script = null;
    // Start is called before the first frame update
    void Start()
    {
        obstacle_script = GameObject.Find("obstacle").GetComponent<obstacle_creator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(obstacle_script.IsGone2(gameObject))
            GameObject.Destroy(gameObject);
    }
}
