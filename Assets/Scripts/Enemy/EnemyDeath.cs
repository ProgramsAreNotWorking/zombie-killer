using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public void Destroy()//este metodo se llama en un event animation
    {
        gameObject.SetActive(false);
    }
}
