
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SkillBook : AContainer<SkillCfgItem>
{  
    public DetailsSkill details;
    public static SkillBook Instance;


    [SerializeField]
    private LogicCharacter _logicCharacter;


    private VisualElement Icon;
    private Label Information;
    private Label Description;


    private void Awake()
    {
        Instance = this;
        Bootstrapper.Instance.eventWhenCloneCharacter += Init;
    }
    private void Init(LogicCharacter logicCharacter)
    {
        Bootstrapper.Instance.eventWhenCloneCharacter -= Init;
        _logicCharacter = logicCharacter;

        Init();
        UpdateContainer();
    }
    public override void Init()
    {
        base.Init();
        _logicCharacter = LogicCharacter.Instance;
        slotUIs.Clear();

        uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        // ScrollView có sẵn trong UXML
        var scroll = root.Q<VisualElement>("Btn_Skill").Q<ScrollView>("ScrollView");
        


        // Tạo container grid bên trong
        var gridContainer = new VisualElement();
        gridContainer.style.flexDirection = FlexDirection.Row;
        gridContainer.style.flexWrap = Wrap.Wrap;
        // gridContainer.style.justifyContent = Justify.Center; // căn giữa grid
        gridContainer.style.flexGrow = 1;

        // Clear và add container vào ScrollView
        scroll.contentContainer.Clear();
        scroll.contentContainer.Add(gridContainer);


        foreach (var skill in _logicCharacter.Data.SkillsLearned)
        {
            var newSlot = new SkillSlotUI(slotTemplate);
            newSlot.SetData(skill.Value);

            slotUIs[skill.Key] = newSlot;
            gridContainer.Add(newSlot.Root);
        }

        var detail = root.Q<VisualElement>("Btn_Skill").Q<VisualElement>("Detail");
        Icon = detail.Q<VisualElement>("Icon");
        Information = detail.Q<Label>("Information");
        Description = detail.Q<Label>("Description");
    }

    
    public override void UpdateContainer()
    {
        SkillSlotUI s;
        SkillCfgItem i;
        foreach (var slot in slotUIs)
        {
            s = (SkillSlotUI)slot.Value;
            i = s.dataOfSlot;

            s.Equip(_logicCharacter.Data.SkillsEquipped.ContainsKey(i.id));
        }
    }
    public override void OnClick(int id) 
    {
        base.OnClick(id);
        UpdateDetail(_logicCharacter.Data.SkillsLearned[id]);
    }

    private void UpdateDetail(SkillCfgItem skill)
    {
      
        Icon.style.backgroundImage = new StyleBackground(skill.Icon);
        Information.text = skill.Name;
        Description.text = skill.Description;
    }

}
