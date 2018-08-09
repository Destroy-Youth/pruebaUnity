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

public class datos : MonoBehaviour {

    string connectionString = "mongodb://trapos:trapos1@ds259351.mlab.com:59351/museo";
    string visitantes;

    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var client = new MongoClient(connectionString);
        var server = client.GetServer();
        var database = server.GetDatabase("museo");
        var playercollection = database.GetCollection<BsonDocument>("visitors");
        Debug.Log("1. ESTABLISHED CONNECTION");

        string expo="";
        //fecha interaccion "07/30/2018 14:52:43"
        System.Random rnd = new System.Random();
        string dia = rnd.Next(10, 31).ToString();
        string mes = rnd.Next(1, 7).ToString();
        string ano = "2018";
        string hora = rnd.Next(12, 24).ToString();
        string min = rnd.Next(10, 60).ToString();
        string seg = rnd.Next(10, 60).ToString();
        int animal = rnd.Next(1, 4);
        string btnNAme="";

        String timeStamp = "0" + mes + "/" + dia + "/" + ano + " " + hora + ":" + min + ":" + seg;
        Debug.Log(timeStamp);

        switch (animal)
        {
            case 1:
                expo = "tigre";
                btnNAme = "cara_feliz";

                break;
            case 2:
                expo = "ballena";
                btnNAme = "cara_triste";
                break;
            case 3:
                expo = "tiburon";
                btnNAme = "cara_normal";
                break;
            case 4:
                expo = "mars turret";
                break;

        }
        Debug.Log(animal);
        Debug.Log(btnNAme);

        playercollection.Insert(new BsonDocument{
                            { "expo", expo },
                            { "visitante", "" },
                            { "tiempo_interaccion", timeStamp }
                        });
        Debug.Log("2. INSERTED A DOC");

        
        playercollection.Insert(new BsonDocument{
                            { "expo", "encuesta" },
                            { "visitante", "" },
                            { "reaccion", btnNAme},
                            { "tiempo_interaccion", timeStamp }
                        });
        Debug.Log("2. INSERTED A DOC");
        
    }
}
