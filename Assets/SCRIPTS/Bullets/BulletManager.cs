using UnityEngine;
using System.Collections;

public abstract class BulletManager : MonoBehaviour {

	[Tooltip("Cadence de tir")]
	public float rateOfFire;
	[Tooltip("Force de répulsion, en 100aines")]
	public float force;
	[Tooltip("Vitesse du projectile, 1 de base")]
	public float speed;
	[Tooltip("Durée de vie du projectile (en frames)")]
	public float lifeTime;
	[Tooltip("Degats sur les ennemis")]
	public int damages;
	[Tooltip("Effet d'impact sur les ennemis, les walls etc...")]
	public GameObject impactParticles;
}
