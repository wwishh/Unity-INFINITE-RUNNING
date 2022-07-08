using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_action : MonoBehaviour
{
    public AudioClip collision_sound;
    public Transform explosion_effect;

    void OnTriggerEnter (Collider other){
        if(other.gameObject.tag == "obstacle"){
            Instantiate(explosion_effect, this.transform.position, this.transform.rotation);
            AudioSource.PlayClipAtPoint(collision_sound, this.transform.position);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
