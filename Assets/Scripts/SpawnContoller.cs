using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnContoller : MonoBehaviour{
	public GameObject key;
	public GameObject cure;

	public Transform[] keySpawnPoints;
	public Transform[] cureSpawnPoints;

	public List<GameObject> instantiatedObjects = new List<GameObject>();

	private void Awake(){
		spawnItems(26, key, keySpawnPoints);
		spawnItems(16, cure, cureSpawnPoints);
	}

	void spawnItems(int numberOfItems, GameObject spawnObject, Transform[] spawnPoint){
		List<int> positions = randomPositions(spawnPoint.Length, numberOfItems);

		GameController gameController = FindObjectOfType<GameController>();

		for(int i = 0; i < numberOfItems; i++){
			GameObject instantiatedObject = Instantiate(spawnObject, spawnPoint[positions[i]].position, spawnPoint[positions[i]].rotation);

			if(instantiatedObject.GetComponent<Item>().key)
				instantiatedObjects.Add(instantiatedObject);
		}

		for(int i = 0; i < 26;i++){
			Item item = instantiatedObjects[i].GetComponent<Item>();
			if(i < 5){
			    instantiatedObjects[i].GetComponent<MeshRenderer>().material = gameController.keyColors[0];
				item.keyColor = "red";
			}
			else if(i < 10){
				instantiatedObjects[i].GetComponent<MeshRenderer>().material = gameController.keyColors[1];
				item.keyColor = "green";
			}
			else if(i < 15){
				instantiatedObjects[i].GetComponent<MeshRenderer>().material = gameController.keyColors[2];
				item.keyColor = "pink";
			}
			else if(i < 20){
				instantiatedObjects[i].GetComponent<MeshRenderer>().material = gameController.keyColors[3];
				item.keyColor = "purple";
			}
			else if(i < 26){
				instantiatedObjects[i].GetComponent<MeshRenderer>().material = gameController.keyColors[4];
				item.keyColor = "blue";
			}
		}
	}

	List<int> randomPositions(int length, int numberOfItems){
		List<int> positions = new List<int>();
		int number;

		for (int i = 0; i < numberOfItems; i++){
			do{
				number = Random.Range(0, length);
			}while (positions.Contains(number));
			positions.Add(number);
		}

		return positions;
	}
}
