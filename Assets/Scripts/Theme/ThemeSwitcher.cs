using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ThemeSwitcher : MonoBehaviour
{
    private PlayerMovement player;
    public static bool isDesert = true;
    private GameObject[] transitionObjects;
    public TransitionManager transitionManager;
    public float speed;

    void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    public void fireChange() {
        // Utilizzo sto schifo per prendere tutti gli oggetti che implementano l'interfaccia ThemeHandler
        var mObjs = GameObject.FindObjectsOfType<MonoBehaviour>();
        var handlers = (from a in mObjs where a.GetType().GetInterfaces().Any(k => k == typeof(ThemeHandler)) select (ThemeHandler)a).ToList();

        // Transition
        player.canMove = false;

        transitionManager.onTransition(speed);

        Utils.setTimeout(() => {
            if(isDesert) {
                handlers.ForEach(e => {
                    e.onDesert();
                });
            } else {
                handlers.ForEach(e => {
                    e.onOcean();
                });
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
