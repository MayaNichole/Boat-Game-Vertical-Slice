using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    public GameObject player;
    PlayerScript ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
