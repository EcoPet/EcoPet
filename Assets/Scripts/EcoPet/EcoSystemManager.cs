using UnityEngine;
using System.Collections;

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

	void Start() {
		isAdjustingWaterLevel = false;
	}

	void Update() {
		performWaterLevelLerp ();
	}

	public void AdjustFishPopulation(Scenario scenario) {
		switch (scenario) {
			case Scenario.Worst:
				print ("Lowering fish population!");
				break;
			case Scenario.Average:
				print ("Keeping fish population the same!");
				break;
			case Scenario.Best:
				print ("Increasing fish population!");
				break;
		}
	}

	public void AdjustWaterLevels(Scenario scenario) {
		switch (scenario) {
			case Scenario.Worst:
				print ("Lowering water levels!");
				prepareWaterLevelLerp (scenario);
				break;
			case Scenario.Average:
				print ("Keeping water levels the same!");
				break;
			case Scenario.Best:
				print ("Increasing the water levels!");
				prepareWaterLevelLerp (scenario);
				break;
		}
	}

	public void AdjustPollution(Scenario scenario) {
		switch (scenario) {
			case Scenario.Worst:
				print ("Increasing pollution!");
				break;
			case Scenario.Average:
				print ("Keeping pollution the same!");
				break;
			case Scenario.Best:
				print ("Decreasing pollution!");
				break;
			}
	}

	private void prepareWaterLevelLerp(Scenario scenrio) {
		int direction = 1;
		if (scenrio == Scenario.Worst) {
			direction = -1;
		}
		startPosition = ecosystem.transform.position;
		endPosition = new Vector3(ecosystem.transform.position.x,
			ecosystem.transform.position.y  + direction * waterLevelAdjustment, ecosystem.transform.position.z);
		startTime = Time.time;
		journeyLength = Vector3.Distance(startPosition, endPosition);
		isAdjustingWaterLevel = true;
	}
		
	private void performWaterLevelLerp() {
		if (isAdjustingWaterLevel) {
			float distCovered = (Time.time - startTime) * waterLevelAdjustmentSpeed;
			float fracJourney = distCovered / journeyLength;
			ecosystem.transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);
		}
		if (ecosystem.transform.position == endPosition) {
			isAdjustingWaterLevel = false;
		}
	}
		
}
