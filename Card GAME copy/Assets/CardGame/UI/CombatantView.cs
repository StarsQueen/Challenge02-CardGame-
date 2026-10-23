using TMPro;
using UnityEngine;
using UnityEngine.UI;

// If this doesnt work I'mma cry actually
public class CombatantView : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text blockText;
    [SerializeField] Image healthFill;   
    [SerializeField] Image portrait;

    ICombatant combatant;

    public void Bind(ICombatant newCombatant, Sprite art = null)
    {
        Unbind();
        combatant = newCombatant;
        combatant.Changed += Refresh;

        if (portrait != null && art != null) portrait.sprite = art;
        Refresh();
    }

    void OnDestroy() => Unbind();

    void Unbind()
    {
        if (combatant != null) combatant.Changed -= Refresh;
        combatant = null;
    }

    void Refresh()
    {
        if (combatant == null) return;

        if (nameText != null) nameText.text = combatant.DisplayName; //Name
        if (healthText != null) healthText.text = $"{combatant.CurrentHealth}/{combatant.MaxHealth}"; //Health
        if (healthFill != null) healthFill.fillAmount = (float)combatant.CurrentHealth / combatant.MaxHealth; //Filler
        if (blockText != null) blockText.text = combatant.Block > 0 ? $"Block {combatant.Block}" : ""; 
    }
}
