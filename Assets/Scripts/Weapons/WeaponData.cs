using UnityEngine;

[System.Serializable]
public struct AttackType
{
    public string type;
    public int damage;
    public float attackSpeed;
    //public float range;
}

[System.Serializable]
public struct WeaponLevel
{
    public string levelName;
    public AttackType[] attacks;
}

[CreateAssetMenu(fileName = "NewWeapon", menuName = "ZeldaLike/Weapon")]
public class WeaponData : ScriptableObject
{
    public string baseName;
    [TextArea] public string description;

    public WeaponLevel[] levels;
}