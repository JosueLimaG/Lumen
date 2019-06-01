using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    public int var;

    public void Anim()
    {
        var = Random.Range(0, 500);
    }
}
