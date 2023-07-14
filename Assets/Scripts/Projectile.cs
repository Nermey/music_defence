using UnityEngine;


public enum projectileType
{
    bomb, accord, note
};

public class Projectile : MonoBehaviour
{
    [SerializeField]
    int attackDamage;

    [SerializeField]
    projectileType pType;

    public int AttackDamage
    {
        get 
        { 
            return attackDamage; 
        }
    }

    public projectileType PType
    {
        get
        {
            return pType;
        }
    }
}
