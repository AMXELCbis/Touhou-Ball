using System.Collections;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Networking;
using SimpleJSON;

public class JsonLoadforTest : MonoBehaviour
{
  public static void SaveJsonFile(string filename, JSONObject data)
  {
    string Path = Application.dataPath + filename;

    File.WriteAllText(Path, data.ToString());
  }

  public static JSONObject LoadJsonFile(string filename)
  {
    string LoadData;
    string Path = Application.dataPath + filename;

    if (!File.Exists(Path))
    {
      return null;
    }

    LoadData = File.ReadAllText(Path);

    if (LoadData.Length > 0)
    {
      JSONObject listForMessages = (JSONObject)JSON.Parse(LoadData);
      return listForMessages;
    }

    return null;
  }

  public static class JsonHelper
  {
    public static T[] FromJson<T>(string json)
    {
      Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
      return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
      Wrapper<T> wrapper = new Wrapper<T>();
      wrapper.Items = array;
      return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
      Wrapper<T> wrapper = new Wrapper<T>();
      wrapper.Items = array;
      return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
      public T[] Items;
    }
  }

  IEnumerator LoadTxt(string path)
  {
    // 或者是讀取檔案系統 var url = @"http://C:\example.txt";
    Debug.Log("Start loading content from path: " + path);
    var url = path;
    var request = UnityWebRequest.Get(url);
    var download = new DownloadHandlerBuffer();
    request.downloadHandler = download;
    yield return request.SendWebRequest();
    Debug.Log("download text: " + download.text);
  }

}