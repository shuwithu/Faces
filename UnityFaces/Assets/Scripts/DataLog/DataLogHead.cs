using UnityEngine;

public class DataLogHead : DataLog
{
    public GameObject cameraObject;
    public GameObject leftHand;
    public GameObject rightHand;

    new void Start()
    {
        base.Start();

        filenameSuffix = "_tracking.csv";
        lineType = "Head";
        timesPerSecond = 120;
    }

    protected override void WriteFileHeader()
    {
        WriteLine("LineType,CameraTX,CameraTY,CameraTZ,CameraRX,CameraRY,CameraRZ," +
                  "LeftTX,LeftTY,LeftTZ,LeftRX,LeftRY,LeftRZ," +
                  "RightTX,RightTY,RightTZ,RightRX,RightRY,RightRZ,");
    }

    protected new void InvokeableWriteLine()
    {
        WriteLine(lineType + "," +
                  cameraObject.transform.position.x + "," +
                  cameraObject.transform.position.y + "," +
                  cameraObject.transform.position.z + "," +
                  cameraObject.transform.rotation.eulerAngles.x + "," +
                  cameraObject.transform.rotation.eulerAngles.y + "," +
                  cameraObject.transform.rotation.eulerAngles.z + "," +
                  leftHand.transform.position.x + "," +
                  leftHand.transform.position.y + "," +
                  leftHand.transform.position.z + "," +
                  leftHand.transform.rotation.eulerAngles.x + "," +
                  leftHand.transform.rotation.eulerAngles.y + "," +
                  leftHand.transform.rotation.eulerAngles.z + "," +
                  rightHand.transform.position.x + "," +
                  rightHand.transform.position.y + "," +
                  rightHand.transform.position.z + "," +
                  rightHand.transform.rotation.eulerAngles.x + "," +
                  rightHand.transform.rotation.eulerAngles.y + "," +
                  rightHand.transform.rotation.eulerAngles.z);
    }


}
