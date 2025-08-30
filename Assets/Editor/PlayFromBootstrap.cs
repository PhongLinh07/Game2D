#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class PlayFromBootstrap
{

    static PlayFromBootstrap()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        switch (state)
        {
            case PlayModeStateChange.ExitingEditMode:
                
                // Nếu đang ở scene khác thì mở Bootstrap
                if (EditorSceneManager.GetActiveScene().name != "Bootstrap")
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                    EditorSceneManager.OpenScene("Assets/Scenes/Bootstrap.unity");
                }
                break;

            case PlayModeStateChange.EnteredEditMode:
                // Sau khi thoát Play, load lại scene trước đó
                EditorSceneManager.OpenScene("Assets/Scenes/GamePlay.unity");
                break;
        }
    }
}
#endif
