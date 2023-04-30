using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Cysharp.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class JsonUtils
{
    /// <summary>
    /// ResoucesファイルからJsonファイル"複数"を読み込むジェネリック関数
    /// </summary>
    /// <typeparam name="T">読み込みたいクラス</typeparam>
    /// <param name="path">読み込みたいデータのパス</param>
    /// <returns>リスト型のデータ</returns>
    public static T[] LoadJsonsFromResources<T>(string path, bool debug = false)
    {
        object[] objects = Resources.LoadAll(path);
        List<string> jsonStrs = new List<string>();
        List<T> datas = new List<T>();

        for (int i = 0; i < objects.Length; i++)
        {
            jsonStrs.Add(objects[i].ToString());
            datas.Add(JsonUtility.FromJson<T>(jsonStrs[i]));
        }

        if (debug == true)
        {
            Debug.Log("リソースから読み込んだJsonファイルの内容");
            jsonStrs.ForEach(x => Debug.Log(x));
            Debug.Log($"パスAssets/Resources/{path}");
        }

        return datas.ToArray();
    }

    /// <summary>
    /// ResoucesファイルからJsonファイルを読み込む関数
    /// </summary>
    /// <typeparam name="T">読み込みたいクラス</typeparam>
    /// <param name="path">読み込みたいデータのパス</param>
    /// <returns>読み込んだデータ</returns>
    public static T LoadJsonFromResources<T>(string path)
    {
        var jsonStr = Resources.Load(path).ToString();

        Debug.Log($"リソースから読み込んだJsonファイルの内容{jsonStr}\nパス Assets/Resources/{path}");

        return JsonUtility.FromJson<T>(jsonStr);
    }

    public static async UniTask<T> LoadJsonFromResoucesAsync<T>(string path)
    {
        var jsonObj = await Resources.LoadAsync(path);

        var jsonStr = jsonObj.ToString();

        Debug.Log($"[非同期]リソースから読み込んだJsonファイルの内容{jsonStr}\nパス Assets/Resources/{path}");

        return JsonUtility.FromJson<T>(jsonStr);
    }

    /// <summary>
    /// 指定されたパスからJsonファイルを読み込む関数
    /// </summary>
    /// <typeparam name="T">読み込みたいクラス</typeparam>
    /// <param name="path">読み込みたいデータのパス</param>
    /// <returns>読み込んだデータ</returns>
    public static T LoadJson<T>(string path)
    {
        using var reader = new StreamReader(path, System.Text.Encoding.GetEncoding("UTF-8"));

        var jsonStr = reader.ReadToEnd();
        reader.Close();

        Debug.Log($"指定されたパスのファイル読み込んだJsonファイルの内容{jsonStr}\nパス{path}");

        return JsonUtility.FromJson<T>(jsonStr);
    }

    /// <summary>
    /// 指定されたパスから非同期にJsonファイルを読み込む関数
    /// </summary>
    /// <typeparam name="T">読み込みたいクラス</typeparam>
    /// <param name="path">読み込みたいデータのパス</param>
    /// <returns>読み込んだデータ</returns>
    public static async UniTask<T> LoadJsonAsync<T>(string path)
    {
        using var reader = new StreamReader(path, System.Text.Encoding.GetEncoding("UTF-8"));

        var jsonStr = await reader.ReadToEndAsync();
        reader.Close();

        Debug.Log($"[非同期]指定されたパスのファイルから読み込んだJsonファイルの内容{jsonStr}\nパス{path}");

        return JsonUtility.FromJson<T>(jsonStr);
    }

    /// <summary>
    /// Jsonファイルを作る関数
    /// </summary>
    /// <param name="data">作りたいデータ</param>
    /// <param name="path">読み込みたいデータのパス</param>
    public static void CreateJson<T>(T data, string path)
    {
        using var writer = new StreamWriter(path);

        var jsonStr = JsonUtility.ToJson(data);

        writer.Write(jsonStr);
        writer.Flush();
        writer.Close();

        Debug.Log($"作成したJsonファイルの内容{jsonStr}\nパス{path}");

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }

    /// <summary>
    /// Jsonファイルを非同期で作る関数
    /// </summary>
    /// <param name="data">作りたいデータ</param>
    /// <param name="path">読み込みたいデータのパス</param>
    public static async UniTask CreateJsonAsync<T>(T data, string path)
    {
        using var writer = new StreamWriter(path);

        var jsonStr = JsonUtility.ToJson(data);

        await writer.WriteAsync(jsonStr);
        writer.Flush();
        writer.Close();

        Debug.Log($"[非同期]作成したJsonファイルの内容{jsonStr}\nパス{path}");

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
