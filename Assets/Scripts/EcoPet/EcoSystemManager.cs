using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EcoSystemManager : MonoBehaviour {

	public GameObject ecosystem;
	public float waterLevelAdjustment;
	public float waterLevelAdjustmentSpeed;

	private GameObject waterLevel;
	private bool isAdjustingWaterLevel;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float startTime;
	private float journeyLength;
	private Transform pollutionCollection;
	private Transform fishClusterCollection;

	private static string PollutionCollectionGameObjectName = "PollutionCollection";
	private static string FishCollectionGameObjectName = "FishClusters";

	void Start() {
		isAdjustingWaterLevel = false;
		pollutionCollection = ecosystem.transform.Find (PollutionCollectionGameObjectName);
		fishClusterCollection = ecosystem.transform.Find (FishCollectionGameObjectName);
	}

	void Update() {
		PerformWaterLevelLerp ();
	}

	public void AdjustFishPopulation(Scenario scenario) {
		switch (scenario) {
			case Scenario.Worst:
				print ("Lowering fish population!");
				DecreaseFishPopulation ();
				break;
			case Scenario.Average:
				print ("Keeping fish population the same!");
				break;
			case Scenario.Best:
				print ("Increasing fish population!");
				IncreaseFishPopulation ();
				break;
		}
	}

	public void AdjustWaterLevels(Scenario scenario) {
		switch (scenario) {
			case Scenario.Worst:
				print ("Lowering water levels!");
				PrepareWaterLevelLerp (scenario);
				break;
			case Scenario.Average:
				print ("Keeping water levels the same!");
				break;
			case Scenario.Best:
				print ("Increasing the water levels!");
				PrepareWaterLevelLerp (scenario);
				break;
		}
	}

	public void AdjustPollution(Scenario scenario) {
		switch (scenario) {
			case Scenario.Worst:
				print ("Increasing pollution!");
				IncreasePollution ();
				break;
			case Scenario.Average:
				print ("Keeping pollution the same!");
				break;
			case Scenario.Best:
				print ("Decreasing pollution!");
				DecreasePollution ();
				break;
		}
	}

	public void IncreaseFishPopulation() {
		List<GameObject> inactiveFishClusterList = GetInactiveFishClusterGameObjectList ();
		EnableRandomGameObjects (ref inactiveFishClusterList);
	}

	public void DecreaseFishPopulation() {
		List<GameObject> activeFishClusterList = GetActiveFishClusterGameObjectList ();
		DisableRandomGameObjects (ref activeFishClusterList);
	}

	public void IncreasePollution() {
		List<GameObject> inactivePollutionList = GetInctivePollutionGameObjectList();
		EnableRandomGameObjects (ref inactivePollutionList);
	}

	public void DecreasePollution() {
		List<GameObject> activePollutionList = GetActivePollutionGameObjectList();
		DisableRandomGameObjects (ref activePollutionList);
	}

	private List<GameObject> GetInactiveFishClusterGameObjectList() {
		List<GameObject> inactiveFishClusterList = new List<GameObject> ();
		foreach (Transform fishClusterTransform in fishClusterCollection) {
			GameObject currentPollutionObject = fishClusterTransform.gameObject;
			if (!currentPollutionObject.activeSelf) {
				inactiveFishClusterList.Add (currentPollutionObject);
			}
		}
		return inactiveFishClusterList;
	}

	private List<GameObject> GetActiveFishClusterGameObjectList() {
		List<GameObject> activeFishClusterList = new List<GameObject> ();
		foreach (Transform fishClusterTransform in fishClusterCollection) {
			GameObject currentPollutionObject = fishClusterTransform.gameObject;
			if (currentPollutionObject.activeSelf) {
				activeFishClusterList.Add (currentPollutionObject);
			}
		}
		return activeFishClusterList;
	}

	private void PrepareWaterLevelLerp(Scenario scenrio) {
		int direction = 1;
		bool pausePollutionFloating = true;

		if (scenrio == Scenario.Worst) {
			direction = -1;
		}
		startPosition = ecosystem.transform.position;
		endPosition = new Vector3(ecosystem.transform.position.x,
			ecosystem.transform.position.y  + direction * waterLevelAdjustment, ecosystem.transform.position.z);
		startTime = Time.time;
		journeyLength = Vector3.Distance(startPosition, endPosition);
		isAdjustingWaterLevel = true;
		AdjustPollutionWithWaterLevel (pausePollutionFloating);
	}

	private void PerformWaterLevelLerp() {
		if (isAdjustingWaterLevel) {
			float distCovered = (Time.time - startTime) * waterLevelAdjustmentSpeed;
			float fracJourney = distCovered / journeyLength;
			ecosystem.transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);
		}
		if (ecosystem.transform.position == endPosition) {
			isAdjustingWaterLevel = false;
			bool pausePollutionFloating = false;
			AdjustPollutionWithWaterLevel (pausePollutionFloating);
		}
	}

	private void AdjustPollutionWithWaterLevel(bool pausePollutionFloating) {
		foreach (Transform child in pollutionCollection) {
			PollutionFloating script = child.gameObject.GetComponent<PollutionFloating> ();
			if (pausePollutionFloating) {
				script.PauseLerping ();
				continue;
			}
			script.ResumeLerping ();
		}
	}

	private List<GameObject> GetInctivePollutionGameObjectList() {
		List<GameObject> inactivePollutionList = new List<GameObject> ();
		foreach (Transform pollutionTransform in pollutionCollection) {
			GameObject currentPollutionObject = pollutionTransform.gameObject;
			if (!currentPollutionObject.activeSelf) {
				inactivePollutionList.Add (currentPollutionObject);
			}
		}
		return inactivePollutionList;
	}

	private List<GameObject> GetActivePollutionGameObjectList() {
		List<GameObject> activePollutionList = new List<GameObject> ();
		foreach (Transform pollutionTransform in pollutionCollection) {
			GameObject currentPollutionObject = pollutionTransform.gameObject;
			if (currentPollutionObject.activeSelf) {
				activePollutionList.Add (currentPollutionObject);
			}
		}
		return activePollutionList;
	}

	private void DisableRandomGameObjects(ref List<GameObject> gameObjectList) {
		for (int count = 0; count < 2 && count < gameObjectList.Count; count++) {
			int randomPollutionIndex = Random.Range (0, gameObjectList.Count);
			gameObjectList [randomPollutionIndex].SetActive (false);
		}
	}

	private void EnableRandomGameObjects(ref List<GameObject> gameObjectList) {
		for (int count = 0; count < 2 && count < gameObjectList.Count; count++) {
			int randomPollutionIndex = Random.Range (0, gameObjectList.Count);
			gameObjectList [randomPollutionIndex].SetActive (true);
		}
	}
}
