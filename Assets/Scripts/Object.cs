using UnityEngine;

public class Object : MonoBehaviour, IHitable {
    public int hp = 100;
    public int givenExp = 5;
    public int givenHp = 0;
    
    public bool Hit(int damage) {
        hp -= damage;

        if (hp > 0) 
            return false;

        Destroy(gameObject);
        return true;
    }

    public int GetExp() {
        return givenExp;
    }
    
    public int GetHp() {
        return givenHp;
    }
}