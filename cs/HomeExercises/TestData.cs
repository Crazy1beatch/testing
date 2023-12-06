using System.Collections.Generic;
using NUnit.Framework;

namespace HomeExercises
{
	public class TestData
	{
		public static IEnumerable<TestCaseData> TestCasesNumberValidatorThrows_On_Creation()
		{
			yield return new TestCaseData(-1, 2).SetName("Negative_Precision");
			yield return  new TestCaseData(0, 2).SetName("Zero_Precision");
			yield return new TestCaseData(1, -1).SetName("Negative_Scale");
			yield return  new TestCaseData(2, 3).SetName("Scale_Grater_Than_Precision");
			yield return  new TestCaseData(2, 2).SetName("Scale_Equals_To_Precision");
		}

		public static IEnumerable<TestCaseData> TestCasesNumberValidation(bool isOnlyPositive)
		{
			yield return new TestCaseData("1.62", true).SetName("No_Sign_Number_True");
			yield return  new TestCaseData("1,62", true).SetName("No_Sign_Number_True_When_Coma");
			yield return new TestCaseData("+64.2", true).SetName("Positive_Sign_Number_True");
			yield return new TestCaseData("+64,2", true).SetName("Positive_Sign_Number_When_Coma_True");
			yield return new TestCaseData("-12.13", !isOnlyPositive).SetName("Negative_Sign_Number_False");
			yield return new TestCaseData("-12,13", !isOnlyPositive).SetName("Negative_Sign_Number_When_Coma_False");
		}

		public static IEnumerable<TestCaseData> TestCasesNumberValidatesFalse()
		{
			yield return new TestCaseData("a.sd").SetName("Not_A_Number");
			yield return new TestCaseData("").SetName("Empty_String");
			yield return new TestCaseData(null).SetName("Null_String");
			yield return new TestCaseData("?0.1").SetName("Unexpected_Symbol");
			yield return new TestCaseData("10.").SetName("No_Frac_Part");
			yield return new TestCaseData(".12").SetName("No_Digit_Part");
			yield return new TestCaseData(" 0").SetName("Space_Before_Number");
			yield return new TestCaseData("0 ").SetName("Space_After_Number");
			yield return new TestCaseData("0.,1").SetName("Too_Much_Different_Separators");
			yield return new TestCaseData("0..1").SetName("Too_Much_Dot_Separators");
			yield return new TestCaseData("0,,1").SetName("Too_Much_Coma_Separators");
			yield return new TestCaseData("0.1,3").SetName("Too_Much_Separators_Different_Places");
			yield return new TestCaseData("++0.1").SetName("Too_Much_Signs");
			yield return new TestCaseData("0.112").SetName("Frac_Is_Bigger_Than_Scale");
			yield return new TestCaseData("1110.11").SetName("Number_Is_Bigger_Than_Precision");
			yield return new TestCaseData("0x01").SetName("Hex_Number");
			yield return new TestCaseData("aboba").SetName("Random_String");
			yield return new TestCaseData("110/11").SetName("Invalid_Separator_\"/\"");
			yield return new TestCaseData("110;11").SetName("Invalid_Separator_\";\"");
			yield return new TestCaseData("23\n23").SetName("Invalid_Separator_\"\\n\"");
			yield return new TestCaseData(" ").SetName("Space_String");
			yield return new TestCaseData("0+.0").SetName("Sign_Wrong_Position");
			yield return new TestCaseData("0.0+").SetName("Sign_After_Number");
			yield return new TestCaseData("\n").SetName("New_Line_String");
			yield return new TestCaseData("\t").SetName("Tab_String");
		} 
	}
}
