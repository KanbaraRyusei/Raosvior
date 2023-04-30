using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Builder
{
    private static readonly string BuildDirectoryName = "Builds";

    [MenuItem("Build/Build And Run CurrentScene", false, 1)]
    private static void BuildAndRunPhotonTutorial()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;
        BuildAndRun(currentSceneName, currentSceneName);
    }

    private static void BuildAndRun(string appName, string sceneName)
    {
        var outputDirectory = Path.Combine(BuildDirectoryName, appName);
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        var targetScene = EditorBuildSettings.scenes.First(scene => Path.GetFileNameWithoutExtension(scene.path) == sceneName);
        var buildTarget = EditorUserBuildSettings.activeBuildTarget;
        var locationPath = Path.Combine(outputDirectory, MakeApplicationFileName(appName, buildTarget));
        var buildOptions = BuildOptions.SymlinkSources | BuildOptions.AutoRunPlayer;

        var originalName = PlayerSettings.productName;
        PlayerSettings.productName = appName;
        BuildPipeline.BuildPlayer(new EditorBuildSettingsScene[] { targetScene }, locationPath, buildTarget, buildOptions);
        PlayerSettings.productName = originalName;
        AssetDatabase.SaveAssets();
    }

    private static string MakeApplicationFileName(string fileName, BuildTarget buildTarget)
    {
        switch (buildTarget)
        {
            case BuildTarget.StandaloneWindows64:
                return $"{fileName}.exe";
            case BuildTarget.StandaloneOSX:
                return $"{fileName}.app";
        }
        return fileName;
    }
}
