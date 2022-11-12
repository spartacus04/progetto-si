using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ThemeSwitcher : MonoBehaviour
{
    private PlayerMovement player;
    public bool isDesert;
    public ThemeHandler[] handlers;
    private GameObject[] transitionObjects;
    public TransitionManager transitionManager;
    public float speed;

    void Start()
    {
        player = GetComponent<PlayerMovement>();
        // Utilizzo sto schifo per prendere tutti gli oggetti che implementano l'interfaccia ThemeHandler
        var mObjs = GameObject.FindObjectsOfType<MonoBehaviour>();
        handlers = (from a in mObjs where a.GetType().GetInterfaces().Any(k => k == typeof(ThemeHandler)) select (ThemeHandler)a).ToArray();
    }

    public void fireChange() {
        // Transition
        player.canMove = false;

        transitionManager.onTransition(speed);

        Utils.setTimeout(() => {
            if(isDesert) {
                foreach(var handler in handlers) {
                    handler.onDesert();
                }
            } else {
                foreach(var handler in handlers) {
                    handler.onOcean();
                }
            }
        }, 1f/3f / speed);

        Utils.setTimeout(() => {
            player.canMove = true;
        }, 2f/3f / speed);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isDesert = !isDesert;
            fireChange();
        }
    }
}
