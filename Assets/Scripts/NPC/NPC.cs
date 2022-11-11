using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    
    [Header("Player")]
    [SerializeField] GameObject player;
#nullable enable
    PlayerMovement? Player;
#nullable disable
    [Header("Distance")]
    [SerializeField]float distance;
    [SerializeField]float distanceRange;

    [Header("Prefabs")]
    [SerializeField] GameObject UIPrefabs;

    [Header("Commands")]
    [SerializeField] List<KeyCode> InteractionKey;

    void Start()
    {
        isActive = false;
        //cache player gameobject di scena
        player = GameObject.Find("Player");
        //controllo se c'è il player o meno e mi serve per limitare i movimenti grazie al bool all interno dello script del movimento
        if (player != null)
        {
            Player = player.GetComponent<PlayerMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetectDistance();

    }

    void DetectDistance()
    {
        distance = Vector2.Distance(player.transform.position,transform.position);

        if(distanceRange > distance)
        {
            CanTalk(true);
        }
        else
        {
            CanTalk(false);
           
        }
        
    }

   public bool isActive;
    void CanTalk(bool InRange)
    {
        
        GameObject canvas;
        if (InRange && Input.GetKeyDown(InteractionKey[0]) && !isActive)
        {
            Player.canMove = false;
            Instantiate(UIPrefabs, player.transform.position, Quaternion.identity);
            isActive = true;
            
		}
        canvas = GameObject.Find("DialogueCanvasGameobject(Clone)");


        if (Input.GetKey(InteractionKey[1]) && isActive)
        {
            Player.canMove = true;
            isActive = false;
            Destroy(canvas);

        }
        

    }

}
