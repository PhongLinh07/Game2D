using UnityEngine;
using UnityEditor;
using System.IO;

public class SpriteExporterContext
{
    [MenuItem("Assets/Export Sprites", false, 1000)]
    static void ExportSelectedSprites()
    {
        // Lấy file sprite sheet đang chọn
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(path);

        if (sprites.Length == 0)
        {
            Debug.LogWarning("❌ File này không có sub-sprites (có chắc là Sprite Mode = Multiple chưa anh?).");
            return;
        }

        // Mở hộp thoại để chọn thư mục lưu
        string exportPath = EditorUtility.OpenFolderPanel("Chọn thư mục để export sprites", Application.dataPath, "");
        if (string.IsNullOrEmpty(exportPath))
        {
            Debug.Log("Export đã bị hủy.");
            return;
        }

        // Export từng sprite
        foreach (Object obj in sprites)
        {
            Sprite sprite = obj as Sprite;
            if (sprite != null)
            {
                Texture2D tex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
                Color[] pixels = sprite.texture.GetPixels(
                    (int)sprite.rect.x,
                    (int)sprite.rect.y,
                    (int)sprite.rect.width,
                    (int)sprite.rect.height
                );
                tex.SetPixels(pixels);
                tex.Apply();

                byte[] pngData = tex.EncodeToPNG();
                File.WriteAllBytes(Path.Combine(exportPath, sprite.name + ".png"), pngData);
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("✅ Export thành công vào: " + exportPath);
    }
}
