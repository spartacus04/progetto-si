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


    void CanTalk(bool InRange)
    {

        if (InRange && Input.GetKey(InteractionKey[0]))
        {
            Instantiate(UIPrefabs, player.transform.position, Quaternion.identity);
        }
        else if (Input.GetKey(InteractionKey[1]))
        {
            Destroy(UIPrefabs);

        }else
        {
            return;
        }


    }

}
