using System.Collections;
using NUnit.Framework;

namespace HomeExercises
{
	public class TestData
	{
		public static IEnumerable TestCasesNumberValidation(bool isOnlyPositive)
		{
			yield return new TestCaseData("1.62", true).SetName("No_Sign_Number_True");
			yield return  new TestCaseData("1,62", true).SetName("No_Sign_Number_True_When_Coma");
			yield return new TestCaseData("+64.2", true).SetName("Positive_Sign_Number_True");
			yield return new TestCaseData("+64,2", true).SetName("Positive_Sign_Number_When_Coma_True");
			yield return new TestCaseData("-12.13", !isOnlyPositive).SetName("Negative_Sign_Number_False");
			yield return new TestCaseData("-12,13", !isOnlyPositive).SetName("Negative_Sign_Number_When_Coma_False");
		}
	}
}
