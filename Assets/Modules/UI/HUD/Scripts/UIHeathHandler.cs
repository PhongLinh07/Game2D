using UnityEngine;
using UnityEngine.UIElements;

public class UIHeathHandler : MonoBehaviour
{
    public static UIHeathHandler Instance { get; private set; }
    private VisualElement m_HPBar, m_MPBar;
    private Label m_HPText, m_MPText;
    private float m_maxHP, m_maxMP;
    private LogicCharacter _logicCharacter;

    public Button btn_Open;
    private void Awake()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_HPBar = uiDocument.rootVisualElement.Q<VisualElement>("HP");
        m_HPText = m_HPBar.Q<Label>("Description");
        m_MPBar = uiDocument.rootVisualElement.Q<VisualElement>("MP");
        m_MPText = m_MPBar.Q<Label>("Description");

        btn_Open = uiDocument.rootVisualElement.Q<Button>("Btn_Open");

        Instance = this;
        
        Bootstrapper.Instance.eventWhenCloneCharacter += Init;
    }

    public void Init(LogicCharacter logicCharacter)
    {
        Bootstrapper.Instance.eventWhenCloneCharacter -= Init;

        _logicCharacter = logicCharacter;

        if (logicCharacter == null) Debug.Log("Null");

        m_maxHP = _logicCharacter.Data.attributes[EAttribute.Hp].value;
        m_maxMP = _logicCharacter.Data.attributes[EAttribute.Mana].value;

        logicCharacter.OnStatsChanged += TakeDamage;
        logicCharacter.OnStatsChanged += UseSKill;

        TakeDamage();
        UseSKill();
    }
    private void TakeDamage()
    {
        m_HPBar.style.width = Length.Percent(Mathf.Clamp(_logicCharacter.Data.attributes[EAttribute.Hp].currValue / m_maxHP, 0, m_maxHP)*100);
        m_HPText.text = $"{_logicCharacter.Data.attributes[EAttribute.Hp].currValue}/{m_maxHP}";
    }
     
     
    private void UseSKill()
    {
        m_MPBar.style.width = Length.Percent(Mathf.Clamp(_logicCharacter.Data.attributes[EAttribute.Mana].currValue / m_maxMP, 0, m_maxMP)*100);
        m_MPText.text = $"{_logicCharacter.Data.attributes[EAttribute.Mana].currValue}/{m_maxMP}";
    }
 
}