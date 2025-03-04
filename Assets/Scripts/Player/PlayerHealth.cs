using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	//referência global do código do personagem
	public static PlayerHealth Instance {get; set;}
	
	[SerializeField]
	int max_hp;//vida máxima
	int hp;//vida atual
	
	void Awake()
	{
		//setta a referência global desse script
		if(Instance == null) Instance = this;
		//garante que só tem um dele na cena
		else Destroy(gameObject);
	}
	
	void Start()
	{
		hp = max_hp;
	}
	
    public void TakeDamage(int dmg)
	{
		hp -= dmg;
		
		if(hp > max_hp) hp = max_hp;
		else if(hp <= 0) print("morreu");
	}
}
