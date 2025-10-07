using UnityEngine;
using UnityEngine.UIElements;


public class Stats : MonoBehaviour
{
    public static Stats Instance;
    [SerializeField] private UIDocument uiDocument;

    private Button closeButton;
    private VisualElement Root;
    private VisualElement[] btn_Tab = new VisualElement[3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instance = this;
        uiDocument = GetComponent<UIDocument>();
        Root = uiDocument.rootVisualElement;
        closeButton = Root.Q<Button>("Close");
        Root.style.display = DisplayStyle.None;
        closeButton.clicked += CloseThis;

        btn_Tab[0] = Root.Q<VisualElement>("Btn_Attribute"); btn_Tab[0].RegisterCallback<ClickEvent>(evt => OpenTab(0));
        btn_Tab[1] = Root.Q<VisualElement>("Btn_Inventory"); btn_Tab[1].RegisterCallback<ClickEvent>(evt => OpenTab(1));
        btn_Tab[2] = Root.Q<VisualElement>("Btn_Skill"); btn_Tab[2].RegisterCallback<ClickEvent>(evt => OpenTab(2));

    }

    private void Start()
    {
        
        UIHeathHandler.Instance.btn_Open.clicked += OpenThis;

        for (int i = 0; i < btn_Tab.Length; i++)
        {
            btn_Tab[i].Q<VisualElement>("Content").style.display = DisplayStyle.None; // chỉ hiện tab được chọn
            btn_Tab[i].Q<VisualElement>("Highlight").style.display = DisplayStyle.None; // chỉ hiện tab được chọn
        }

        btn_Tab[0].Q<VisualElement>("Content").style.display = DisplayStyle.Flex; // chỉ hiện tab được chọn
        btn_Tab[0].Q<VisualElement>("Highlight").style.display = DisplayStyle.Flex; // chỉ hiện tab được chọn
    }

    private void OpenThis()
    {
        Debug.Log("Open Stats UI");

        Root.style.display = DisplayStyle.Flex;
    }
    public void CloseThis()
    {   
        Debug.Log("Close Stats UI");
        Root.style.display = DisplayStyle.None;
    }

    // Hàm để mở tab theo chỉ số (index)
    public void OpenTab(int index)
    {
        for (int i = 0; i < btn_Tab.Length; i++)
        {
            btn_Tab[i].Q<VisualElement>("Content").style.display = (i == index ? DisplayStyle.Flex : DisplayStyle.None); // chỉ hiện tab được chọn
            btn_Tab[i].Q<VisualElement>("Highlight").style.display = (i == index ? DisplayStyle.Flex : DisplayStyle.None); // chỉ hiện tab được chọn
        }
    }
}
