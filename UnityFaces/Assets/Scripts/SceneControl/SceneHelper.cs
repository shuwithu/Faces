using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SceneHelper
{

    public enum ScenarioMode { None, Neutral };

    //public static string Serialize(object obj)
    //{
    //    StringBuilder xml = new StringBuilder();
    //    XmlSerializer serializer = new XmlSerializer(obj.GetType());

    //    using (TextWriter textWriter = new StringWriter(xml))
    //    {
    //        serializer.Serialize(textWriter, obj);
    //    }

    //    return xml.ToString();
    //}

    public static T Deserialize<T>(string xml)
    {
        T obj = default(T);

        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (TextReader textReader = new StringReader(xml))
        {
            obj = (T)serializer.Deserialize(textReader);
        }

        return obj;
    }



    public static Transform FindChildTransform(Transform pRoot, string pName) { return FindChildTransform(pRoot.gameObject, pName); }
    public static Transform FindChildTransform(string pRoot, string pName) { return FindChildTransform(GameObject.Find(pRoot), pName); }
    public static Transform FindChildTransform(GameObject pRoot, string pName)
    {
        //Returns the first ocurrence of a child GameObject called pName using depth search first

        Transform pTransform = pRoot.transform;
        Transform ret;

        //Debug.Log("Finding child in " + pRoot);

        foreach (Transform trs in pTransform)
        {
            if (trs.gameObject.name == pName)
            {
                //Debug.Log(pName + " found."); 
                return trs;
            }

            else ret = FindChildTransform(trs.gameObject, pName);

            if (ret != null) return ret;
        }

        return null;
    }

    public static string getTransformPath(Transform myTransform)
    {
        if (myTransform.parent == null) return "/" + myTransform.name;

        return getTransformPath(myTransform.parent) + "/" + myTransform.name;
    }
}
