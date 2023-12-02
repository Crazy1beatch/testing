using System;
using FluentAssertions;
using NUnit.Framework;

namespace HomeExercises
{
	[TestFixture]
	public class NumberValidatorTests
	{
		[Category("Creation_Tests")]
		[TestCaseSource(typeof(TestData), nameof(TestData.TestCasesNumberValidatorCreation))]
		public void NumberValidatorCreatesCorrectly_NoOnlyPositive(int precision, int scale)
		{
			var action = new Action(() => new NumberValidator(precision, scale));
			action.Should().NotThrow();
		}
		
		[Category("Creation_Tests")]
		[TestCaseSource(typeof(TestData), nameof(TestData.TestCasesNumberValidatorCreation))]
		public void NumberValidatorCreatesCorrectly_WhenValidArguments_OnlyPositive_True(int precision, int scale)
		{
			var action = new Action(() => new NumberValidator(precision, scale, true));
			action.Should().NotThrow();
		}
		
		[Category("Creation_Tests")]
		[TestCaseSource(typeof(TestData), nameof(TestData.TestCasesNumberValidatorCreation))]
		public void NumberValidatorCreatesCorrectly_WhenValidArguments_OnlyPositive_False(int precision, int scale)
		{
			var action = new Action(() => new NumberValidator(precision, scale, false));
			action.Should().NotThrow();
		}
		
		[TestCaseSource(typeof(TestData), nameof(TestData.TestCasesNumberValidatorThrows_On_Creation))]
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

		[TestCaseSource(typeof(TestData), nameof(TestData.TestCasesNumberValidatesFalse))]
		public void ReturnsFalse_When(string number)
		{
			var numberValidatorOnlyPositive = new NumberValidator(5, 2, true);
			var numberValidatorNegative = new NumberValidator(5, 2);
			numberValidatorOnlyPositive.IsValidNumber(number).Should().Be(false);
			numberValidatorNegative.IsValidNumber(number).Should().Be(false);
		}
	}
}
