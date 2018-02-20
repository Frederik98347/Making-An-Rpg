using UnityEngine;

public class CharacterData : ScriptableObject {
    //character data class
    //generic all classes

    [SerializeField] GameObject prefab;
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    [SerializeField] int critchance;
    [SerializeField] string Name;
    [SerializeField] int agility;
    [SerializeField] int intellect;
    [SerializeField] int stamina;
    [SerializeField] int strength;
    [SerializeField] int haste;
    [SerializeField] int defense;
    [SerializeField] int resistance;
    [SerializeField] int endurance;
    [SerializeField] float movementspeed;
    [SerializeField] float walkingspeed;
    [SerializeField] int level;
    [SerializeField] float attackrange;
    [SerializeField] float attackspeed;

}
