using UnityEngine;

public static class FileUtils
{
    public const string SAVE_DATA_JSON_FILE_NAME = "SaveData.json";

    /// <summary>
    /// 書き込み可能なディレクトリのパスを返す
    /// ファイルの保存はこのディレクトリの直下ではなく、サブディレクトリを作成して保存する事を推奨します
    /// </summary>
    /// <returns>プラットフォームごとの書き込み可能なディレクトリのパス</returns>
    public static string GetWritableDirectoryPath()
    {
        // Androidの場合、Application.persistentDataPathでは外部から読み出せる場所に保存されてしまうため
        // アプリをアンインストールしてもファイルが残ってしまう
        // ここではアプリ専用領域に保存するようにする
#if !UNITY_EDITOR && UNITY_ANDROID
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            using (var getFilesDir = currentActivity.Call<AndroidJavaObject>("getFilesDir"))
            {
                return getFilesDir.Call<string>("getCanonicalPath");
            }
#else
        return Application.persistentDataPath;
#endif
    }
}
