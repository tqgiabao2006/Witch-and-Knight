using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public bool player1Active = true;
    private void Start()
    {
        player1.SetActive(false);
        player1.transform.SetParent(player2.transform);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
           
            SwitchPlayer();
        }
       
        
    }

    public void SwitchPlayer()
    {
        if(player1Active)
        {
            player1.transform.SetParent(p: null);
            player2.SetActive(false);
            player1.SetActive(true);
            player2.transform.SetParent(player1.transform);
            player1Active = false;
        }
        else
        {
            player2.transform.SetParent(p: null);
            player1.SetActive(false);
            player2.SetActive(true);
            player1.transform.SetParent(player2.transform);

            player1Active =  true;
        }
    }
}
