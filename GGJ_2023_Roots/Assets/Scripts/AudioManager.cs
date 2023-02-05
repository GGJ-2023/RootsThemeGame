using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioSource placeSource;
    //public AudioClip placeClip;
    public AudioSource destroySource;
    //public AudioClip destroyClip;
    //public AudioSource menus;

    private void Awake()
    {
        instance = this;


        placeSource.GetComponent<AudioSource>();
        destroySource.GetComponent<AudioSource>();


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceSound()
    {        
        placeSource.Play();
    }
    public void DestroySound()
    {
        destroySource.Play();
    }

    public void MenuClick()
    {

    }
}
