using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class NPC : MonoBehaviour, Interactable, ThemeHandler
{
    public string animDesert;
    public string animOcean;
    public Texture icon;
    public RawImage iconImage;
    public string[] textOcean;
    public string[] textDesert;
    public float textSpeed;
    public GameObject canvas;
    public TextMeshProUGUI text;

    
    private Animator animator;
    private SpriteRenderer sr;
    private PlayerMovement pm;
    private bool isDialogueOpen = false;

    private List<string> messages;
    private int index = 0;
    private bool isWriting = false;

    public void onDesert()
    {
        sr.enabled = animDesert.Length != 0;

        if(animDesert.Length != 0) {
            animator.SetTrigger(animDesert);
        }
        
        messages = textDesert.ToList();
    }

    public void onOcean()
    {
        sr.enabled = animOcean.Length != 0;

        if(animOcean.Length != 0) {
            animator.SetTrigger(animOcean);
        }

        messages = textOcean.ToList();
    }

    public void onInteract(GameObject player)
    {
        pm = player.GetComponent<PlayerMovement>();

        if(!isDialogueOpen) {
            isDialogueOpen = true;

            pm.canMove = false;
            canvas.SetActive(true);
            index = -1;
        }

        StopAllCoroutines();

        if(isWriting) {
            text.text = messages[index];
            isWriting = false;
        }
        else {
            index++;
            NextSentence();
        }
    }

    void NextSentence() {
        if(index < messages.Count) {
            text.text = "";
            StartCoroutine(WriteSentence());
        }
        else {
            text.text = "";
            index = 0;
            isDialogueOpen = false;
            pm.canMove = true;
            canvas.SetActive(false);
        }
    }

    IEnumerator WriteSentence() {
        isWriting = true;

        foreach(char letter in messages[index].ToCharArray()) {
            text.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        isWriting = false;
    }

    public void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        iconImage.texture = icon;

        messages = ThemeSwitcher.isDesert ? textDesert.ToList() : textOcean.ToList();
    }
}
