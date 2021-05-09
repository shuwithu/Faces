using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour 
{
    SceneStarts sceneStarts;

    static GameObject l;

    private void OnEnable()
    {

        if (l == null)
        {
            l = this.gameObject;
            //Debug.Log("Initializing permanent object " + this.gameObject.name);
        }
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        EventManager.Instance.AddListener<SceneStarts>(SessionStarts);
        EventManager.Instance.AddListener<SceneEnds>(SessionEnds);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && sceneStarts == null) EventManager.Instance.TriggerEvent(new SceneStarts(SceneHelper.ScenarioMode.None, false));
        if (Input.GetKeyDown(KeyCode.Q) && sceneStarts != null) EventManager.Instance.TriggerEvent(new SceneEnds());
        if (Input.GetKeyDown(KeyCode.Escape) && sceneStarts != null) EventManager.Instance.TriggerEvent(new SceneEnds());

    }

    void SessionStarts(SceneStarts ss)
    {
        if (sceneStarts != null) return;

        sceneStarts = ss;
        Debug.Log("Scenario starts.");
    }

    void SessionEnds(SceneEnds se)
    {
        if (sceneStarts == null) return;

        sceneStarts = null;
        Debug.Log("Scenario ends.");
    }
}
