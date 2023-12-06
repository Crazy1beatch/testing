using System.Collections.Generic;
using NUnit.Framework;

namespace HomeExercises
{
	public class TestData
	{
		public static IEnumerable<TestCaseData> TestCasesNumberValidatorThrows_On_Creation()
		{
			yield return new TestCaseData(-1, 2).SetName("Negative_Precision");
			yield return new TestCaseData(0, 2).SetName("Zero_Precision");
			yield return new TestCaseData(1, -1).SetName("Negative_Scale");
			yield return new TestCaseData(2, 3).SetName("Scale_Grater_Than_Precision");
			yield return new TestCaseData(2, 2).SetName("Scale_Equals_To_Precision");
		}

		public static IEnumerable<TestCaseData> TestCasesNumberValidation()
		{
			foreach (var caseValidator in new (NumberValidator validator, string Category, bool isOnlyPositive)[]
			         {
				         (new NumberValidator(5, 2, true), "Case_OnlyPositive_True", true),
				         (new NumberValidator(5, 2), "Case_OnlyPositive_False", false)
			         })
			{
				yield return new TestCaseData("1.62", caseValidator.validator, true)
					.SetName($"$No_Sign_Number_True_{caseValidator.Category}");
				yield return new TestCaseData("1,62", caseValidator.validator, true)
					.SetName($"No_Sign_Number_True_When_Coma_{caseValidator.Category}");
				yield return new TestCaseData("+64.2", caseValidator.validator, true)
					.SetName($"Positive_Sign_Number_True_{caseValidator.Category}");
				yield return new TestCaseData("+64,2", caseValidator.validator, true)
					.SetName($"Positive_Sign_Number_When_Coma_True_{caseValidator.Category}");
				yield return new TestCaseData("-12.13", caseValidator.validator, !caseValidator.isOnlyPositive)
					.SetName($"Negative_Sign_Number_{!caseValidator.isOnlyPositive}_{caseValidator.Category}");
				yield return new TestCaseData("-12,13", caseValidator.validator, !caseValidator.isOnlyPositive)
					.SetName($"Negative_Sign_Number_When_Coma_{!caseValidator.isOnlyPositive}" +
					         $"_{caseValidator.Category}");
			}
		}

		public static IEnumerable<TestCaseData> TestCasesNumberValidatesFalse()
		{
			foreach (var caseValidator in new (NumberValidator validator, string Category)[]
			         {
				         (new NumberValidator(5, 2, true), "Case_OnlyPositive_True"),
				         (new NumberValidator(5, 2), "Case_OnlyPositive_False")
			         })
			{
				yield return new TestCaseData("a.sd", caseValidator.validator)
					.SetName($"Not_A_Number_{caseValidator.Category}");
				yield return new TestCaseData("", caseValidator.validator)
					.SetName($"Empty_String_{caseValidator.Category}");
				yield return new TestCaseData(null, caseValidator.validator)
					.SetName($"Null_String_{caseValidator.Category}");
				yield return new TestCaseData("?0.1", caseValidator.validator)
					.SetName($"$Unexpected_Symbol_{caseValidator.Category}");
				yield return new TestCaseData("10.", caseValidator.validator)
					.SetName($"No_Frac_Part_{caseValidator.Category}");
				yield return new TestCaseData(".12", caseValidator.validator)
					.SetName($"No_Digit_Part_{caseValidator.Category}");
				yield return new TestCaseData(" 0", caseValidator.validator)
					.SetName($"Space_Before_Number_{caseValidator.Category}");
				yield return new TestCaseData("0 ", caseValidator.validator)
					.SetName($"Space_After_Number_{caseValidator.Category}");
				yield return new TestCaseData("0.,1", caseValidator.validator)
					.SetName($"Too_Much_Different_Separators_{caseValidator.Category}");
				yield return new TestCaseData("0..1", caseValidator.validator)
					.SetName($"Too_Much_Dot_Separators_{caseValidator.Category}");
				yield return new TestCaseData("0,,1", caseValidator.validator)
					.SetName($"Too_Much_Coma_Separators_{caseValidator.Category}");
				yield return new TestCaseData("0.1,3", caseValidator.validator)
					.SetName($"Too_Much_Separators_Different_Places_{caseValidator.Category}");
				yield return new TestCaseData("++0.1", caseValidator.validator)
					.SetName($"Too_Much_Signs_{caseValidator.Category}");
				yield return new TestCaseData("0.112", caseValidator.validator)
					.SetName($"Frac_Is_Bigger_Than_Scale_{caseValidator.Category}");
				yield return new TestCaseData("1110.11", caseValidator.validator)
					.SetName($"Number_Is_Bigger_Than_Precision_{caseValidator.Category}");
				yield return new TestCaseData("0x01", caseValidator.validator)
					.SetName($"Hex_Number_{caseValidator.Category}");
				yield return new TestCaseData("aboba", caseValidator.validator)
					.SetName($"Random_String_{caseValidator.Category}");
				yield return new TestCaseData("110/11", caseValidator.validator)
					.SetName($"Invalid_Separator_\"/\"_{caseValidator.Category}");
				yield return new TestCaseData("110;11", caseValidator.validator)
					.SetName($"Invalid_Separator_\";\"_{caseValidator.Category}");
				yield return new TestCaseData("23\n23", caseValidator.validator)
					.SetName($"Invalid_Separator_\"\\n\"_{caseValidator.Category}");
				yield return new TestCaseData(" ", caseValidator.validator)
					.SetName($"Space_String_{caseValidator.Category}");
				yield return new TestCaseData("0+.0", caseValidator.validator)
					.SetName($"Sign_Wrong_Position_{caseValidator.Category}");
				yield return new TestCaseData("0.0+", caseValidator.validator)
					.SetName($"Sign_After_Number_{caseValidator.Category}");
				yield return new TestCaseData("\n", caseValidator.validator)
					.SetName($"New_Line_String_{caseValidator.Category}");
				yield return new TestCaseData("\t", caseValidator.validator)
					.SetName($"Tab_String_{caseValidator.Category}");
			}
		}
	}
}
