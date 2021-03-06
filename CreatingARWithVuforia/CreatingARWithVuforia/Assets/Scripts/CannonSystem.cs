﻿//This script controls the firing capabilities of the turret. It is responsible for creating the projectiles
//as well as controlling the visual and audio feedback involved in shooting

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



public class CannonSystem : MonoBehaviour 
{
	[Header("Firing Properties")]
	public float maxProjectileForce = 18000f;   //Maximum force of a projectile
	public float cooldown = 1f;

	[Header("Projectile Properties")]
	public GameObject projectilePrefab;			//The projectile to be shot

	Transform projectileSpawnTransform;         //Location where the projectiles should spawn
	bool canShoot = true;
	Animator anim;								//Reference to the animator component

	string connectionString = "mongodb://trapos:trapos1@ds259351.mlab.com:59351/museo";
    String visitantes;


    void Awake()
	{
		//Get a reference to the projectile spawn point. By providing the path to the object like this, we are making an 
		//inefficient method call more efficient
		projectileSpawnTransform = GameObject.Find("Geometry/Cockpit/Turret Elevation Pivot Point/Projectile Spawn Point").transform;

		//Get a reference to the animator component
		anim = GetComponent<Animator> ();

		
		
	}

	public void FireProjectile()
	{
		if (!canShoot)
			return;

        //animacion de disparo
		GameObject go = (GameObject)Instantiate(projectilePrefab, projectileSpawnTransform.position, projectileSpawnTransform.rotation);
		Vector3 force = projectileSpawnTransform.transform.forward * maxProjectileForce;
		go.GetComponent<Rigidbody>().AddForce(force) ;
		anim.SetTrigger ("Fire");

        //Conexion a mongoDB y seleccion de BD y coleccion
		var client = new MongoClient(connectionString);
		var server = client.GetServer(); 
		var database = server.GetDatabase("museo");
		var playercollection= database.GetCollection<BsonDocument>("visitors");
		Debug.Log ("1. ESTABLISHED CONNECTION");

        //contador de visitantes por 
        
        foreach (var document in playercollection.Find(new QueryDocument("expo", "mars turret")))
        {
            var conteo = document;
            Debug.Log("6. SELECT DOC WHERE: \n" + document);
            Debug.Log("16. COUNT DOCS: \n" + playercollection.Count(new QueryDocument("expo", "mars turret")));
            visitantes = playercollection.Count(new QueryDocument("expo", "mars turret")).ToString();
        }

        String timeStamp = DateTime.Now.ToString();
        
        

        playercollection.Insert(new BsonDocument{
			{ "expo", "mars turret" },
            { "visitante", visitantes },
            { "tiempo_interaccion", timeStamp }
		});
		Debug.Log ("2. INSERTED A DOC");
		

		canShoot = false;
		Invoke("CoolDown", cooldown);
	}

	void CoolDown()
	{
		canShoot = true;
	}
}