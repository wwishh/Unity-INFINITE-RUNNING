using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block_creator : MonoBehaviour
{
    public GameObject block_prefab;
    // Start is called before the first frame update
    public void CreateBlock(Vector3 block_position){
        GameObject game_object = GameObject.Instantiate(block_prefab) as GameObject;
        game_object.transform.position = block_position;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
