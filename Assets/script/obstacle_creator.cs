using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class obstacle_creator : MonoBehaviour
{
    public static float OBSTACLE_WIDTH;
    public static int OBSTACLE_NUM_IN_SCREEN = 3;
    public GameObject[] obstacle_prefabs;
    public int index;
    
    
    public void CreateObstacle(Vector3 obstacle_position){
        GameObject game_object = GameObject.Instantiate(obstacle_prefabs[index]) as GameObject;
        game_object.transform.position = obstacle_position;

    }

    private struct FloorObstacle{
        public bool is_created;
        public Vector3 position;
    };

    private FloorObstacle last_obstacle;
    private player_control player = null;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player_control>();
        last_obstacle.is_created =false; 
    }

    private void CreateFloorObstacle(){
        Vector3 obstacle_position;
        OBSTACLE_WIDTH = Random.Range(7.0f,10.0f);
        int next_obstacle_type = Random.Range(0,obstacle_prefabs.Length);
        
        index = next_obstacle_type;
        if(!last_obstacle.is_created){
            obstacle_position = player.transform.position;
            obstacle_position.x += OBSTACLE_WIDTH * ((float)OBSTACLE_NUM_IN_SCREEN / 2.0f);
            obstacle_position.y = 1.0f;
            if(next_obstacle_type==2){
                obstacle_position.y+=1.5f;
            }
            else if(next_obstacle_type==3){
                obstacle_position.y-=0.5f;
            }
            
        }
        else{
            obstacle_position = last_obstacle.position;
            obstacle_position.y = 1.0f;
            if(next_obstacle_type==2){
                obstacle_position.y+=1.5f;
            }
            else if(next_obstacle_type==3){
                obstacle_position.y-=0.5f;
            }
        }

        obstacle_position.x += OBSTACLE_WIDTH;
        
        CreateObstacle(obstacle_position);
        last_obstacle.position = obstacle_position;
        last_obstacle.is_created = true;
    }
    
    // Update is called once per frame
    void Update()
    {

        float obstacle_generate_x = player.transform.position.x;

        obstacle_generate_x += OBSTACLE_WIDTH * ((float)OBSTACLE_NUM_IN_SCREEN + 1) / 2.0f;
        
        while(last_obstacle.position.x < obstacle_generate_x){
            CreateFloorObstacle();
        }    
    }

    public bool IsGone2(GameObject obstacle_object){
        bool result = false;
        
        float left_limit = player.transform.position.x - OBSTACLE_WIDTH * ((float)OBSTACLE_NUM_IN_SCREEN/2.0f);
        
        if(obstacle_object.transform.position.x < left_limit)
            result = true;

        return result;
    }

}
