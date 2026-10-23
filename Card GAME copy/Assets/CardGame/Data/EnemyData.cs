using System.Collections.Generic;
using UnityEngine;

// An enemy is data too: new enemies are new assets, not new scripts.
// Its "moves" are normal cards, so enemies reuse the exact same effect system as the player.
[CreateAssetMenu(menuName = "Cards/Enemy", fileName = "NewEnemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] string enemyName = "Enemy";
    [SerializeField] Sprite portrait;
    [SerializeField, Min(1)] int maxHealth = 30;
    [SerializeField] List<CardData> moves = new List<CardData>();

    public string EnemyName => enemyName;
    public Sprite Portrait => portrait;
    public int MaxHealth => maxHealth;

    public CardData PickMove()
    {
        if (moves == null || moves.Count == 0) return null;
        return moves[Random.Range(0, moves.Count)];
    }
}
