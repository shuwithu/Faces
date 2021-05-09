using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent { }

public class SceneStarts : GameEvent
{
    private float startTime;
    public float StartTime
    {
        get { return startTime; }
        set { startTime = value; }
    }
    private SceneHelper.ScenarioMode sceneMode;
    public SceneHelper.ScenarioMode SceneMode
    {
        get { return sceneMode; }
        set { sceneMode = value; }
    }
    private bool isReplay;
    public bool IsReplay{
        get { return isReplay; }
        set { isReplay = value; }}

    public DateTime time;
    public DateTime timeUtc;
    public string timeStamp;
    public double unixTime;


    public SceneStarts()
    {
        sceneMode = SceneHelper.ScenarioMode.None;
        isReplay = false;
    }

    public SceneStarts(SceneHelper.ScenarioMode sm, bool isRep = false)
    {
        sceneMode = sm;
        isReplay = isRep;

        startTime = Time.time;
        time = DateTime.Now; //this is a struct with current date and time
        timeUtc = DateTime.UtcNow;
        timeStamp = time.ToString("yyyy.MM.dd.HH.mm.ss");
        unixTime = timeUtc.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
    }
}
public class SceneStartsRequest : GameEvent
{
    public SceneHelper.ScenarioMode sceneMode;
    public bool isReplay;

    public SceneStartsRequest(SceneHelper.ScenarioMode sm, bool r)
    {
        sceneMode = sm;
        isReplay = r;
    }
}
public class SceneEnds : GameEvent
{
    public bool replay;

    public SceneEnds(bool r = false)
    {
        replay = r;
    }
}
public class SceneEndsRequest : GameEvent 
{
    public bool replay;

    public SceneEndsRequest(bool r = false)
    {
        replay = r;
    }
}
public abstract class SceneFades : GameEvent
{
    public float fadeTime;

    public SceneFades()
    {
        fadeTime = 2.0f;
    }
}
public class SceneFadesIn : SceneFades
{
    public SceneFadesIn(float ft)
    {
        fadeTime = ft;
    }
}
public class SceneFadesOut : SceneFades
{
    public SceneFadesOut(float ft)
    {
        fadeTime = ft;
    }
}
public class BroadcastSceneLength : GameEvent
{
    public float sceneLength;

    public BroadcastSceneLength(float bsl)
    {
        sceneLength = bsl;
    }
}
public class DataLogEvent : GameEvent 
{
    public double startTime;
    public double endTime;
    public double duration;
    public string noun;
    public string verb;
    public string dObject;
    public string iObject;
    public string oComplement1;
    public string oComplement2;

    public DataLogEvent(string noun)
    {
    }

    public string GetString()
    {
        return startTime + "," + endTime + "," + duration + "," + noun + "," + verb + "," + dObject + "," + iObject + "," + oComplement1 + "," + oComplement2;
    }
}

