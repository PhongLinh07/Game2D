using UnityEngine;
using UnityEngine.UIElements;

public class SkillBookUI : MonoBehaviour
{
    private UIDocument uiDocument;

    void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        // ScrollView có sẵn trong UXML
        var scroll = root.Q<ScrollView>("SkillBookGrid");

        // Tạo container grid bên trong
        var gridContainer = new VisualElement();
        gridContainer.style.flexDirection = FlexDirection.Row;
        gridContainer.style.flexWrap = Wrap.Wrap;
        // gridContainer.style.justifyContent = Justify.Center; // căn giữa grid
        gridContainer.style.flexGrow = 1;

        // Clear và add container vào ScrollView
        scroll.contentContainer.Clear();
        scroll.contentContainer.Add(gridContainer);

        // Thêm 25 item
        for (int i = 0; i < 45; i++)
        {
            var item = new Button();
            item.text = (i + 1).ToString();

            // Style bằng code (khỏi cần USS)
            item.style.width = 128;
            item.style.height = 128;
            item.style.backgroundColor = new Color(0.29f, 0.56f, 0.89f);
            item.style.color = Color.white;
            item.style.unityTextAlign = TextAnchor.MiddleCenter;

            // Khoảng cách giữa các ô
            item.style.marginRight = 4;
            item.style.marginBottom = 4;

            // Event click
            item.clicked += () =>
            {
                Debug.Log($"Clicked item {item.text}");
            };

            gridContainer.Add(item);
        }
    }
}
