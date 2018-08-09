using UnityEngine;
using UnityEngine.Events;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

public class animTiger : MonoBehaviour {

    string connectionString = "mongodb://trapos:trapos1@ds259351.mlab.com:59351/museo";
    string visitantes;

    public Animator anim;
    public AudioClip[] aClips;
    public AudioSource myAudioSource;
    string btnNAme;

    // Use this for initialization
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit))
            {
                btnNAme = Hit.transform.name;
                switch (btnNAme)
                {
                    case "btnText_SoundTiger":
                        anim.Play("sound");
                        
                        myAudioSource.clip = aClips[0];
                        myAudioSource.Play();
                        break;

                    case "btnText_NarTiger":
                        anim.Play("run");
                        myAudioSource.clip = aClips[1];
                        myAudioSource.Play();

                        //Conexion a mongoDB y seleccion de BD y coleccion
                        var client = new MongoClient(connectionString);
                        var server = client.GetServer();
                        var database = server.GetDatabase("museo");
                        var playercollection = database.GetCollection<BsonDocument>("visitors");
                        Debug.Log("1. ESTABLISHED CONNECTION");

                        

                        String timeStamp = DateTime.Now.ToString();
                        playercollection.Insert(new BsonDocument{
                            { "expo", "tigre" },
                            { "visitante", "" },
                            { "tiempo_interaccion", timeStamp }
                        });
                        Debug.Log("2. INSERTED A DOC");

                        break;

                    default:
                        break;
                }
            }
            else
            {
                anim.Play("idle");
            }
        }
    }
}
