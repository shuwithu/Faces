using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


// comment on avatar control
public class AvatarControl : MonoBehaviour
{
    void Start()
    {
        EventManager.Instance.AddListener<SceneStarts>(AvatarStart);
        EventManager.Instance.AddListener<SceneEnds>(AvatarStop);

        Debug.Log(this.name + " initialized.");
    }

    void Update()
    {
    }

    void AvatarStart(SceneStarts ss)
    {
    }

    void AvatarStop(SceneEnds se)
    {
    }

}
