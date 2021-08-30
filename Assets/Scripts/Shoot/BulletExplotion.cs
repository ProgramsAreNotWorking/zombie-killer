using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplotion : MonoBehaviour
{
    public void Destroy()//este metodo se llama en un avent animation
    {
        gameObject.SetActive(false);
    }
}
