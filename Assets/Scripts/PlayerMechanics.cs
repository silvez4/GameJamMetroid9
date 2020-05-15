using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && GameObject.FindGameObjectsWithTag ( "Hook" ).Length == 0){
            Instantiate(Resources.Load("Mechanics/Hook") as GameObject, transform.position, Quaternion.identity);
        }
    }
}
