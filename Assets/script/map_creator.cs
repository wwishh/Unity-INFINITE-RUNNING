using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class map_creator : MonoBehaviour
{
    public static float BLOCK_WIDTH = 1.0f;
    public static float BLOCK_HEIGHT = 0.2f;
    public static int BLOCK_NUM_IN_SCREEN = 24;


    private struct FloorBlock{
        public bool is_created;
        public Vector3 position;
    };

    private FloorBlock last_block;
    private player_control player = null;
    private block_creator block;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player_control>();
        last_block.is_created =false;
        block = gameObject.GetComponent<block_creator>();    
    }

    private void CreateFloorBlock(){
        Vector3 block_position;
        
        
        if(!last_block.is_created){
            block_position = player.transform.position;
            block_position.x -= BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
            block_position.y = 0.0f;
        }
        else{
            block_position = last_block.position;
        }

        block_position.x += BLOCK_WIDTH;

        
        block.CreateBlock(block_position);
        last_block.position = block_position;
        last_block.is_created = true;
    }
    
    void Update()
    {

        float block_generate_x = player.transform.position.x;

        block_generate_x += BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN + 1) / 2.0f;
        
        while(last_block.position.x < block_generate_x){
            CreateFloorBlock();
        }    
    }

    public bool IsGone(GameObject block_object){
        bool result = false;
        
        float left_limit = player.transform.position.x - BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN/2.0f);
        
        if(block_object.transform.position.x < left_limit)
            result = true;

        return result;
    }

}
