using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    [SerializeField] ScriptableDialogue NPCdialogue;
    [SerializeField] TextMeshProUGUI nome;
    [SerializeField] TextMeshProUGUI message;

    int i = 0;
    private void Start()
    {
        nome.text = NPCdialogue.Nome;
        message.text = NPCdialogue.Message[i];
    }

    private void Update()
    {
        
    }

    
    public void UpdateMessage()
    {
        if(i > NPCdialogue.Message.Count)
        {
            return;
        }
        i++;
        message.text = NPCdialogue.Message[i];
    }

}
