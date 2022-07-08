using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control : MonoBehaviour
{
    public static float ACCELERATION = 10.0f;
    public static float SPEED_MIN = 4.0f;
    public static float SPEED_MAX;
    public static float JUMP_HEIGHT_MAX = 2.5f;
    public static float JUMP_POWER_REDUCE = 0.3f;
    public static int bullet_count = 5;
    public static int item_count = 0;

    public GameObject player;
    public Transform bullet;
    public float bullet_power = 600.0f;
    public AudioClip bullet_sound;
    public AudioClip gameover_sound;
    public AudioClip jump_sound;
    public AudioClip item_sound;

    public Animator animator;

    public float playtime;
    public float checktime;
    


    public enum STEP{
        NONE = -1,
        RUN = 0,
        JUMP,
        NUM,
    };

    public STEP step = STEP.NONE;
    public STEP next_step = STEP.NONE;

    public float step_timer = 0.0f;
    private bool is_landed = false;
    public static bool is_collided = false;
    private bool is_key_released = false;


    void Awake(){
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        next_step = STEP.RUN;
    }

    private void CheckLanded(){
        is_landed = false;

        do{
            Vector3 current_position = transform.position;
            Vector3 down_position = current_position + Vector3.down * 0.3f;

            RaycastHit hit;
            if(!Physics.Linecast(current_position, down_position, out hit))
                break;
            if(step == STEP.JUMP){
                if(step_timer < Time.deltaTime * 3.0f)
                    break;
            }

            is_landed = true;
        }while(false);
    }


    void Update()
    {
        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        CheckLanded();
        if(!is_collided){
            playtime += Time.deltaTime;

            if(Input.GetButtonDown("Fire1")&&bullet_count>0){
                GameObject spawn_point = GameObject.Find("sp_bullet");
                Transform prefab_bullet = Instantiate(bullet, spawn_point.transform.position, spawn_point.transform.rotation);
                prefab_bullet.GetComponent<Rigidbody>().AddForce(spawn_point.transform.forward * bullet_power);
                AudioSource.PlayClipAtPoint(bullet_sound, this.transform.position);
                bullet_count--;
            }

            if(Input.GetButtonDown("Fire2")&&item_count>0){
                bullet_count += 5;
                item_count--;
            }



            checktime += Time.deltaTime;

            if(checktime <= 20.0f){
                SPEED_MAX = 5.0f;
            }
            else if(checktime <= 40.0f){
                SPEED_MAX = 5.5f;
            }
            else if(checktime <= 60.0f){
                SPEED_MAX = 6.0f;
            }
            else if(checktime <= 80.0f){
                SPEED_MAX = 7.0f;
            }
            else{
                SPEED_MAX = 8.0f;
            }



            step_timer += Time.deltaTime;

            if(next_step == STEP.NONE){
                switch(step){
                    case STEP.RUN:
                        if(!is_landed){

                        }
                        else if(Input.GetMouseButtonDown(0))
                            next_step = STEP.JUMP;
                        break;
                    case STEP.JUMP:
                        if(is_landed)
                            next_step = STEP.RUN;
                            animator.SetBool("isJumping", false);
                        break;
                }
            }

            while(next_step != STEP.NONE){
                step = next_step;
                next_step = STEP.NONE;

                switch(step){
                    case STEP.JUMP:
                        AudioSource.PlayClipAtPoint(jump_sound, this.transform.position);
                        velocity.y = Mathf.Sqrt(2.0f * 9.8f * JUMP_HEIGHT_MAX);
                        animator.SetBool("isJumping", true);
                        is_key_released = false;
                        
                        break;
                }

                step_timer = 0.0f;
            }

            switch(step){
                case STEP.RUN:
                    velocity.x += ACCELERATION * Time.deltaTime; 
                    if(Mathf.Abs(velocity.x) > SPEED_MAX)
                        velocity.x = SPEED_MAX;
                    break;
                
                case STEP.JUMP:
                    do{
                        if(!Input.GetMouseButtonUp(0))
                            break;
                        if(is_key_released)
                            break;
                        if(velocity.y <= 0.0f)
                            break;

                        velocity.y *= JUMP_POWER_REDUCE; 
                        is_key_released =true;
                    }while(false);
                    break;

            }
            GetComponent<Rigidbody>().velocity = velocity;
        }
    }


    void OnCollisionEnter(Collision other) {
        if(other.transform.tag=="obstacle"){
            AudioSource.PlayClipAtPoint(gameover_sound, this.transform.position);
            is_collided = true;
        }
    }

    void OnTriggerEnter (Collider other){
        if(other.gameObject.tag == "item"){
            AudioSource.PlayClipAtPoint(item_sound, this.transform.position);
            Destroy(other.gameObject);
            item_count++;
        }
    }

    void OnGUI(){
        GUI.skin.label.fontSize = 20;
        GUI.Label(new Rect(81, 60, 300, 300), "PLAY TIME : " + playtime.ToString());
        GUI.Label(new Rect(81, 100, 300, 300), "SPEED_MAX : " + SPEED_MAX.ToString());
        GUI.Label(new Rect(81, 140, 300, 300), "BULLET : " + bullet_count.ToString());
        GUI.Label(new Rect(81, 180, 300, 300), "ITEM : " + item_count.ToString());      
    }

}
