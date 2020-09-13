using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class BootstrapLoader
{
   private const string BootstrapScenePath = "Assets/_Project/Scenes/Bootstrap.unity";
      
   static BootstrapLoader()
   {
      var bootstrapScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(BootstrapScenePath);
      EditorSceneManager.playModeStartScene = bootstrapScene;
   }
}