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
    int i = 0;
    private void Start()
    {
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
            Destroy(canvas);
          
            return;
        }
    }

    
    public void UpdateMessage()
    {
        
        i++;
        message.text = NPCdialogue.Message[i];
       
    }

}
