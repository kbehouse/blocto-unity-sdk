using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

// Output the build size or a failure depending on BuildPlayer.

public class BuildTest : MonoBehaviour
{
    private const string FlowScene = "Assets/Scenes/FlowScene.unity";
    private const string BuildFolder = "BuildTest";

    private static void PerformBuild(BuildTarget target, string locationPathName)
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] {FlowScene };
        buildPlayerOptions.locationPathName = locationPathName;
        buildPlayerOptions.target = target;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        string prefix = string.Format("[Blocto] Build {0} with version {1}",target.ToString(), Application.unityVersion);
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.LogFormat("{0} SUCCESS using {1} bytes", prefix, summary.totalSize);
            EditorApplication.Exit(0);
        }else if (summary.result == BuildResult.Failed)
        {
            Debug.LogFormat("{0} FAIL", prefix);
            EditorApplication.Exit(-1);
        }else{
            Debug.LogFormat("{0} FAIL, result: {1}", prefix, summary.result);
            EditorApplication.Exit(-1);
        }
        
    }

    public static void BuildIOS()
    {
        string path =  System.IO.Path.Combine(BuildFolder, "BloctoWallet-" + Application.unityVersion + "-iOS");
        PerformBuild(BuildTarget.iOS, path);
    }

    public static void BuildAndroid()
    {
        string path =  System.IO.Path.Combine(BuildFolder, "BloctoWallet-" + Application.unityVersion + "-android.apk");
        PerformBuild(BuildTarget.Android, path);
    }
}