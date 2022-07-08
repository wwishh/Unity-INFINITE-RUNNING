using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class item_creator : MonoBehaviour
{
    public static float ITEM_WIDTH;
    public static int ITEM_NUM_IN_SCREEN = 3;
    public GameObject item_prefabs;

      
    public void CreateItem(Vector3 item_position){
        GameObject game_object = GameObject.Instantiate(item_prefabs) as GameObject;
        game_object.transform.position = item_position;

    }

    private struct FloorItem{
        public bool is_created;
        public Vector3 position;
    };

    private FloorItem last_item;
    private player_control player = null;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player_control>();
        last_item.is_created =false; 
    }

    private void CreateFloorItem(){
        Vector3 item_position;
        ITEM_WIDTH = Random.Range(30.0f,45.0f);

        if(!last_item.is_created){
            item_position = player.transform.position;
            item_position.x += ITEM_WIDTH * ((float)ITEM_NUM_IN_SCREEN / 2.0f);
            item_position.y = 1.0f;  
        }
        else{
            item_position = last_item.position;
            item_position.y = 1.0f;
        }

        item_position.x += ITEM_WIDTH;
        
        CreateItem(item_position);
        last_item.position = item_position;
        last_item.is_created = true;
    }
    
    // Update is called once per frame
    void Update()
    {

        float item_generate_x = player.transform.position.x;

        item_generate_x += ITEM_WIDTH * ((float)ITEM_NUM_IN_SCREEN + 1) / 2.0f;
        
        while(last_item.position.x < item_generate_x){
            CreateFloorItem();
        }    
    }

    public bool IsGone3(GameObject item_object){
        bool result = false;
        
        float left_limit = player.transform.position.x - ITEM_WIDTH * ((float)ITEM_NUM_IN_SCREEN/2.0f);
        
        if(item_object.transform.position.x < left_limit)
            result = true;

        return result;
    }

}
