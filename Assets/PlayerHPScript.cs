using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPScript : MonoBehaviour
{
    public int hp;

    private int maxHp = 5;
    private float temp;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int x)
    {
        hp -= x;

        if (hp < 0)
            hp = 0;
    }
}
