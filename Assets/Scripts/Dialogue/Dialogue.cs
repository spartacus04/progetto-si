using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    [SerializeField] ScriptableDialogue NPCdialogue;
    [SerializeField] TextMeshProUGUI nome;
    [SerializeField] TextMeshProUGUI message;
    GameObject canvas;
    PlayerMovement Player;
    NPC npc;
    int i = 0;
    private void Start()
    {
        var NPC = GameObject.Find("NPC");
        npc = NPC.GetComponent<NPC>();
        var player = GameObject.Find("Player");
        Player = player.GetComponent<PlayerMovement>();
        canvas = GameObject.Find("DialogueCanvasGameobject(Clone)");
        nome.text = NPCdialogue.Nome;
        message.text = NPCdialogue.Message[i];
    }

    private void Update()
    {
        if (i >= NPCdialogue.Message.Count)
        {
            Player.canMove = true;
            npc.isActive = false;
            Destroy(canvas);


            return;
        }
    }

    
    public void UpdateMessage()
    {
        i++;
		if(i >= NPCdialogue.Message.Count) return;

        message.text = NPCdialogue.Message[i];
    }

}
