using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestionManager : MonoBehaviour {

	public Text questionText;
	public Text worstCaseText;
	public Text averageCaseText;
	public Text bestCaseText;

	private Question currentQuestion;
	private List<Question> questionsList;
	private int currentQuestionIndex = -1;
	private EcoSystemManager ecosystemManager;

	// Use this for initialization
	void Start () {
		questionsList = new List<Question> ();
		ecosystemManager = transform.GetComponent<EcoSystemManager> ();
		LoadQuestions ();
		PrepareFirstQuestion ();
	}

	public void OnWorstCaseClick() {
		print ("Worst case!");
		performEcosystemActions (Scenario.Worst);
		PrepareNextQuestion ();
	}

	public void OnAverageCaseClick() {
		print ("Average case!");
		performEcosystemActions (Scenario.Average);
		PrepareNextQuestion ();
	}

	public void OnBestCaseClick() {
		print ("Best case!");
		performEcosystemActions (Scenario.Best);
		PrepareNextQuestion ();
	}

	private void performEcosystemActions(Scenario scenario) {
		switch (currentQuestion.Type) {
			case QuestionType.FishPopulation:
				ecosystemManager.AdjustFishPopulation (scenario);
				break;
			case QuestionType.Pollution:
				ecosystemManager.AdjustPollution (scenario);
				break;
			case QuestionType.WaterLevel:
				ecosystemManager.AdjustWaterLevels (scenario);
				break;
		}
	}

	private void LoadQuestions() {
		// Fish population Q/A
		questionsList.Add (new Question(
			"Do you eat sustainable sourced fish?",
			"All the time",
			"Sometimes",
			"Never",
			QuestionType.FishPopulation));

		questionsList.Add (new Question(
			"How often do you go fishing?",
			"All the time",
			"Sometimes",
			"Never",
			QuestionType.FishPopulation));

		questionsList.Add (new Question(
			"How often do you eat salmon?",
			"Every day",
			"Once a week",
			"Once a month",
			QuestionType.FishPopulation));

		questionsList.Add (new Question(
			"How many of your meals normally contain seafood?",
			"Most of them",
			"Some of them",
			"None of them",
			QuestionType.FishPopulation));



		// Water Levels Q/A
		questionsList.Add (new Question(
			"How many times do you run the washing machine/dish washer every week?",
			"Over 14",
			"7-14",
			"0-6",
			QuestionType.WaterLevel));

		questionsList.Add (new Question(
			"How long do you leave the water running for when you brush your teeth?",
			"The whole time",
			"About half the time",
			"I turn it off immediately",
			QuestionType.WaterLevel));


		// Pollution Q/A
		questionsList.Add (new Question(
			"How many packed goods did you consume over the past 2 days?",
			"More than 8",
			"3-8",
			"Less than 3",
			QuestionType.Pollution));

		questionsList.Add (new Question(
			"Do you ever spit your gum out on the road?",
			"Always!",
			"Whenever I cannot stand the taste of flavorless gum in my mouth",
			"Never",
			QuestionType.Pollution));

		questionsList.Add (new Question(
			"How many non-recyclable products do you use every day?",
			"More than 6",
			"2-5",
			"Less than 2",
			QuestionType.Pollution));

		questionsList.Add (new Question(
			"How many packaged goods did you use today?",
			"More than 10",
			"5-9",
			"1-4",
			QuestionType.Pollution));
		
	}

	private void PrepareFirstQuestion() {
		currentQuestionIndex = Random.Range (0, questionsList.Count);
		LoadQuestion ();
	}

	private void PrepareNextQuestion() {
		int randomNumber = currentQuestionIndex;
		while (currentQuestionIndex == randomNumber) {
			randomNumber = Random.Range (0, questionsList.Count);
		}
		currentQuestionIndex = randomNumber;
		LoadQuestion ();
	}

	private void LoadQuestion() {
		currentQuestion = questionsList[currentQuestionIndex];
		questionText.text = currentQuestion.QuestionText;
		worstCaseText.text = currentQuestion.WorstCaseText;
		averageCaseText.text = currentQuestion.AverageCaseText;
		bestCaseText.text = currentQuestion.BestCaseText;
	}
}
