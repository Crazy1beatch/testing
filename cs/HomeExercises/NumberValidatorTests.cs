using System;
using FluentAssertions;
using NUnit.Framework;

namespace HomeExercises
{
	[TestFixture]
	public class NumberValidatorTests
	{
		[TestCase(1, 0, TestName = "Small_PrecisionAndScale")]
		[TestCase(99999, 99998, TestName = "Big_PrecisionAndScale")]
		public void NumberValidatorCreatesCorrectly_NoOnlyPositive(int precision, int scale)
		{
			var action = new Action(() => new NumberValidator(precision, scale));
			action.Should().NotThrow();
		}

		[TestCase(1, 0, false, TestName = "Small_PrecisionAndScale_OnlyPositive_False")]
		[TestCase(1, 0, true, TestName = "Small_PrecisionAndScale_OnlyPositive_True")]
		[TestCase(99999, 99998, false, TestName = "Big_PrecisionAndScale_OnlyPositive_False")]
		[TestCase(99999, 99998, true, TestName = "Big_PrecisionAndScale_OnlyPositive_True")]
		public void NumberValidatorCreatesCorrectly_WhenValidArguments(int precision, int scale, bool onlyPositive)
		{
			var action = new Action(() => new NumberValidator(precision, scale, onlyPositive));
			action.Should().NotThrow();
		}

		[TestCase(-1, 2, TestName = "Negative_Precision")]
		[TestCase(0, 2, TestName = "Zero_Precision")]
		[TestCase(1, -1, TestName = "Negative_Scale")]
		[TestCase(2, 3, TestName = "Scale_Grater_Than_Precision")]
		[TestCase(2, 2, TestName = "Scale_Equals_To_Precision")]
		public void ThrowException_WhenInvalidArguments(int precision, int scale)
		{
			var action = new Action(() => new NumberValidator(precision, scale, true));
			action.Should().Throw<ArgumentException>();
		}

		[TestCaseSource(typeof(TestData), nameof(TestData.TestCasesNumberValidation), new object []{true})]
		public void ValidatesCorrectly_When_OnlyPositive(string number, bool expected)
		{
			var numberValidator = new NumberValidator(5, 2, true);
			numberValidator.IsValidNumber(number).Should().Be(expected);
		}

		[TestCaseSource(typeof(TestData), nameof(TestData.TestCasesNumberValidation), new object []{false})]
		public void ValidatesCorrectly_When_Negative(string number, bool expected)
		{
			var numberValidator = new NumberValidator(5, 2);
			numberValidator.IsValidNumber(number).Should().Be(expected);
		}

		[TestCase("a.sd", TestName = "Not_A_Number")]
		[TestCase("", TestName = "Empty_String")]
		[TestCase(null, TestName = "Null_String")]
		[TestCase("?0.1", TestName = "Unexpected_Symbol")]
		[TestCase("10.", TestName = "No_Frac_Part")]
		[TestCase(".12", TestName = "No_Digit_Part")]
		[TestCase(" 0", TestName = "Space_Before_Number")]
		[TestCase("0 ", TestName = "Space_After_Number")]
		[TestCase("0.,1", TestName = "Too_Much_Different_Separators")]
		[TestCase("0..1", TestName = "Too_Much_Dot_Separators")]
		[TestCase("0,,1", TestName = "Too_Much_Coma_Separators")]
		[TestCase("0.1,3", TestName = "Too_Much_Separators_Different_Places")]
		[TestCase("++0.1", TestName = "Too_Much_Signs")]
		[TestCase("0.112", TestName = "Frac_Is_Bigger_Than_Scale")]
		[TestCase("1110.11", TestName = "Number_Is_Bigger_Than_Precision")]
		[TestCase("+110.11", TestName = "Number_Is_Bigger_Than_Precision_When_Sign")]
		public void ReturnsFalse_When(string number)
		{
			var numberValidatorOnlyPositive = new NumberValidator(5, 2, true);
			var numberValidatorNegative = new NumberValidator(5, 2);
			numberValidatorOnlyPositive.IsValidNumber(number).Should().Be(false);
			numberValidatorNegative.IsValidNumber(number).Should().Be(false);
		}
	}
}
