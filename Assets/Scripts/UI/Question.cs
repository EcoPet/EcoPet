using UnityEngine;
using System.Collections;
using UnityEngine.UI;

class Question {

	private string questionText;
	private string worstCaseText;
	private string averageCaseText;
	private string bestCaseText;

	public Question(string questionText, string worstCaseText, string averageCaseText, string bestCaseText) {
		this.questionText = questionText;
		this.worstCaseText = worstCaseText;
		this.averageCaseText = averageCaseText;
		this.bestCaseText = bestCaseText;
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

}
