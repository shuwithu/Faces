using UnityEngine;

public class DataLogEvents : DataLog
{

    new void Start()
    {
        base.Start();

        EventManager.Instance.AddListener<DataLogEvent>(WriteLineEvent);

        filenameSuffix = "_events.csv";
        lineType = "Event";
    }

    void WriteLineEvent(DataLogEvent dle)
    {
        sw.WriteLine(dle.GetString());
    }

}
