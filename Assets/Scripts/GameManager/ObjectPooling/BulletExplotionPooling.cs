using System.Collections.Generic;
using UnityEngine;

public class BulletExplotionPooling : MonoBehaviour
{
    public static BulletExplotionPooling bulletExplotionPooling;

    [SerializeField] GameObject objectToPool;
    [SerializeField] float cuantityToPool;

    [SerializeField] List<GameObject> objectPool;

    //audio
    AudioManager audio;

    void Awake()
    {
        bulletExplotionPooling = this;

        for (int i = 0; i < cuantityToPool; i++)
        {
            GameObject tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            tmp.transform.SetParent(transform);
            objectPool.Add(tmp);
        }

        audio = GetComponent<AudioManager>();
    }

    public GameObject GetExplotion(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if(!objectPool[i].activeSelf)
            {
                objectPool[i].SetActive(true);
                objectPool[i].transform.position = position;
                objectPool[i].transform.rotation = rotation;

                audio.playAudio("hit");//reproduce el audio de la explocion
                
                return objectPool[i];
            }
        }

        GameObject tmp = Instantiate(objectToPool, position, rotation);
        objectPool.Add(tmp);
        tmp.transform.SetParent(transform);

        audio.playAudio("hit");//reproduce el audio de la explocion

        return tmp;
    }
}
