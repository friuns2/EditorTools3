using System.IO;
using Microsoft.Win32;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class BuildToolsSDK : EditorWindow
{

    
    
    [MenuItem("Tools/BuildLevel PC", false)]
    public static void BuildLevel()
    {
        BuildLevel(BuildTarget.StandaloneWindows);
    }
    
    [MenuItem("Tools/BuildLevel Android", false)]
    public static void BuildLevel2()
    {
        BuildLevel(BuildTarget.Android);
    }
    public static void BuildLevel(BuildTarget target)
    {
        var d = SceneManager.GetActiveScene();
        EditorSceneManager.SaveScene(d);
        
        var assetBundleName = d.name + (target == BuildTarget.StandaloneWindows? ".unity3dwindows" :".unity3dandroid");
        var build = new AssetBundleBuild() {assetNames = new[] {d.path}, assetBundleName = assetBundleName};
        var bundlesDir = "AssetBundles";
        Directory.CreateDirectory(bundlesDir);
        // File.Delete("AssetBundles/"+assetBundleName);
        // File.Delete("AssetBundles/"+assetBundleName+".manifest");
        AssetBundleManifest man = BuildPipeline.BuildAssetBundles(bundlesDir, new[] {build}, BuildAssetBundleOptions.ForceRebuildAssetBundle, target);
        Debug.Log("build success2 " + man.name);
#if sdk
        PlayerPrefs.SetString("assetPath", Path.GetFullPath(bundlesDir +"/"+ build.assetBundleName));
        var key = Registry.CurrentUser.OpenSubKey(@"Software\Unity\UnityEditor\Phaneron\", true);
        RegistryUtilities.CopyKey(key, "Brutal Strike", Registry.CurrentUser.OpenSubKey(@"Software\Phaneron\", true));
#endif
    }
    
}

