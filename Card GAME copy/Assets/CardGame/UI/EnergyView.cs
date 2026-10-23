using TMPro;
using UnityEngine;

// Shows the energy for the draws (take it from khaled)
public class EnergyView : MonoBehaviour
{
    [SerializeField] IntEventChannel energyChanged;
    [SerializeField] GameConfig config;
    [SerializeField] TMP_Text label;

    void OnEnable() => energyChanged.Raised += Refresh; 
    void OnDisable() => energyChanged.Raised -= Refresh;

    void Refresh(int energy) => label.text = $"{energy}/{config.EnergyPerTurn}";
}
