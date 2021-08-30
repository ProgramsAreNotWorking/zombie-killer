using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject explotion;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        BulletExplotionPooling.bulletExplotionPooling.GetExplotion(transform.position, Quaternion.identity);
        Destroy();
    }

    #region //public Funtion
    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    public void Destroy(float time)
    {
        StartCoroutine(CorrutineDestroy(time));
    }

    IEnumerator CorrutineDestroy(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        gameObject.SetActive(false);
    }
    #endregion
}
