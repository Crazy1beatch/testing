using System;
using FluentAssertions;
using NUnit.Framework;

namespace HomeExercises
{
	[TestFixture]
	public class NumberValidatorTests
	{
		[TestCaseSource(typeof(TestData), nameof(TestData.TestCasesNumberValidatorThrows_On_Creation))]
		public void ThrowException_WhenInvalidArguments(int precision, int scale)
		{
			var action = new Action(() => new NumberValidator(precision, scale, true));
			action.Should().Throw<ArgumentException>();
		}

		[TestCaseSource(typeof(TestData), nameof(TestData.TestCasesNumberValidation), new object[] { true })]
		public void ValidatesCorrectly_When_OnlyPositive(string number, bool expected)
		{
			var numberValidator = new NumberValidator(5, 2, true);
			numberValidator.IsValidNumber(number).Should().Be(expected);
		}

		[TestCaseSource(typeof(TestData), nameof(TestData.TestCasesNumberValidation), new object[] { false })]
		public void ValidatesCorrectly_When_Negative(string number, bool expected)
		{
			var numberValidator = new NumberValidator(5, 2);
			numberValidator.IsValidNumber(number).Should().Be(expected);
		}

		[TestCaseSource(typeof(TestData), nameof(TestData.TestCasesNumberValidatesFalse))]
		public void ReturnsFalse_WhenOnlyPositive_And(string number)
		{
			var numberValidatorOnlyPositive = new NumberValidator(5, 2, true);
			numberValidatorOnlyPositive.IsValidNumber(number).Should().Be(false);
		}

		[TestCaseSource(typeof(TestData), nameof(TestData.TestCasesNumberValidatesFalse))]
		public void ReturnsFalse_WhenNotOnlyPositive_And(string number)
		{
			var numberValidatorNegative = new NumberValidator(5, 2);
			numberValidatorNegative.IsValidNumber(number).Should().Be(false);
		}
	}
}
