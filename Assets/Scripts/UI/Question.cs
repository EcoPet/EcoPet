using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum QuestionType {
	Pollution,
	WaterLevel,
	FishPopulation
}

public enum Scenario {
	Worst,
	Average,
	Best
}

public class Question {

	private string questionText;
	private string worstCaseText;
	private string averageCaseText;
	private string bestCaseText;
	private QuestionType questionType;

	public Question(string questionText, string worstCaseText,
		string averageCaseText, string bestCaseText, QuestionType questionType) {
		this.questionText = questionText;
		this.worstCaseText = worstCaseText;
		this.averageCaseText = averageCaseText;
		this.bestCaseText = bestCaseText;
		this.questionType = questionType;
	}

	public string QuestionText {
		get { return questionText; }
		set { questionText = value; }
	}

	public string WorstCaseText {
		get { return worstCaseText; }
		set { worstCaseText = value; }
	}

	public string AverageCaseText {
		get { return averageCaseText; }
		set { averageCaseText = value; }
	}

	public string BestCaseText {
		get { return bestCaseText; }
		set { bestCaseText = value; }
	}

	public QuestionType Type {
		get { return questionType; }
	}

}
