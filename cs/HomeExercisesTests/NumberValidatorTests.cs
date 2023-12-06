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

		[TestCaseSource(typeof(TestData), nameof(TestData.TestCasesNumberValidation))]
		public void ValidatesCorrectly_When(string number, NumberValidator validator, bool expected)
		{
			validator.IsValidNumber(number).Should().Be(expected);
		}

		[TestCaseSource(typeof(TestData), nameof(TestData.TestCasesNumberValidatesFalse))]
		public void ReturnsFalse_When(string number, NumberValidator numberValidator)
		{
			numberValidator.IsValidNumber(number).Should().Be(false);
		}
	}
}
