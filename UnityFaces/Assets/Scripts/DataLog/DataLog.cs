using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class DataLog : MonoBehaviour
{
    protected SceneStarts sceneStarts = null;
    protected StreamWriter sw = null;

    protected string filenameSuffix;
    protected string lineType;
    protected int    timesPerSecond;

    protected void Start()
    {
        EventManager.Instance.AddListener<SceneStarts>(SessionStarts);
        EventManager.Instance.AddListener<SceneEnds>(SessionEnds);
    }

    protected void WriteLine(string line)
    {
        sw.WriteLine(System.DateTime.Now.Subtract(sceneStarts.time).TotalSeconds + "," + line);
    }

    protected virtual void WriteFileHeader() {}
    protected virtual void InvokeableWriteLine() {}

    private void SessionStarts(SceneStarts ss)
    {
        if (sceneStarts != null) return;
        if (ss.IsReplay) return;

        sceneStarts = ss;

        string filename = sceneStarts.timeStamp + filenameSuffix;
        sw = File.CreateText(filename);

        WriteFileHeader();

        InvokeRepeating("InvokeableWriteLine", 0f, 1f/timesPerSecond);

        Debug.Log(filename + " created.");
    }

    private void SessionEnds(SceneEnds se)
    {
        if (sceneStarts == null) return;

        CancelInvoke();

        sw.Close();
        sceneStarts = null;
    }
}
