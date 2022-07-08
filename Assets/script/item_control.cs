using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_control : MonoBehaviour
{
    public item_creator item_script = null;
    // Start is called before the first frame update
    void Start()
    {
        item_script = GameObject.Find("item").GetComponent<item_creator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(item_script.IsGone3(gameObject))
            GameObject.Destroy(gameObject);
    }
}
