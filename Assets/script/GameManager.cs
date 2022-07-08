using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject EndPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player_control.is_collided){
            EndPanel.SetActive(true);
        }
    }

    public void Restart(){
        SceneManager.LoadScene("Main");
        EndPanel.SetActive(false);
        player_control.is_collided = false;
        player_control.bullet_count = 5;
        player_control.item_count = 0;
    } 
}
