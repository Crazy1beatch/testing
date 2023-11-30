using System;
using System.Text.RegularExpressions;
using FluentAssertions;
using NUnit.Framework;

namespace HomeExercises
{
	[TestFixture]
	public class NumberValidatorTests
	{
		private static NumberValidator numberValidatorOnlyPositive = null!;
		private static NumberValidator numberValidatorNegative = null!;

		[SetUp]
		public void CreateNumberValidators()
		{
			numberValidatorOnlyPositive = new NumberValidator(5, 2, true);
			numberValidatorNegative = new NumberValidator(5, 2);
		}

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
		public void NumberValidatorCreatesCorrectly_WithValidArguments(int precision, int scale, bool onlyPositive)
		{
			var action = new Action(() => new NumberValidator(precision, scale, onlyPositive));
			action.Should().NotThrow();
		}

		[TestCase(-1, 2, TestName = "Negative_Precision")]
		[TestCase(0, 2, TestName = "Zero_Precision")]
		[TestCase(1, -1, TestName = "Negative_Scale")]
		[TestCase(2, 3, TestName = "Scale_Grater_Than_Precision")]
		[TestCase(2, 2, TestName = "Scale_Equals_To_Precision")]
		public void ThrowException_WithInvalidArguments(int precision, int scale)
		{
			var action = new Action(() => new NumberValidator(precision, scale, true));
			action.Should().Throw<ArgumentException>();
		}

		[TestCase("1.62", true, TestName = "No_Sign_Number_True")]
		[TestCase("+64.2", true, TestName = "Positive_Sign_Number_True")]
		[TestCase("-12.13", false, TestName = "Negative_Sign_Number_False")]
		public void ValidatesCorrectly_WithOnlyPositive(string number, bool expected)
		{
			numberValidatorOnlyPositive.IsValidNumber(number).Should().Be(expected);
		}

		[TestCase("1.62", true, TestName = "No_Sign_Number_True")]
		[TestCase("+64.2", true,  TestName = "Positive_Sign_Number_True")]
		[TestCase("-12.13", true, TestName = "Negative_Sign_Number_True")]
		public void ValidatesCorrectly_WithNegative(string number, bool expected)
		{
			numberValidatorNegative.IsValidNumber(number).Should().Be(expected);
		}

		[TestCase("a.sd", TestName = "Not_A_Number")]
		[TestCase("", TestName = "Empty_String")]
		[TestCase(null, TestName = "Null_String")]
		[TestCase("?0.1", TestName = "Unexpected_Symbol")]
		[TestCase("10.", TestName = "No_Frac_Part")]
		[TestCase(".12", TestName = "No_Digit_Part")]
		[TestCase(" 0", TestName = "Space_Before_Number")]
		[TestCase("0 ", TestName = "Space_After_Number")]
		[TestCase("0.,1", TestName = "Too_Much_Separators")]
		[TestCase("0.1,3", TestName = "Too_Much_Separators_Different_Places")]
		[TestCase("++0.1", TestName = "Too_Much_Signs")]
		[TestCase("0.112", TestName = "Frac_Is_Bigger_Than_Scale")]
		[TestCase("1110.11", TestName = "Number_Is_Bigger_Than_Precision")]
		[TestCase("+110.11", TestName = "Number_Is_Bigger_Than_Precision_With_Sign")]
		public void ReturnsFalse_With(string number)
		{
			numberValidatorOnlyPositive.IsValidNumber(number).Should().Be(false);
			numberValidatorNegative.IsValidNumber(number).Should().Be(false);
		}
	}

	public class NumberValidator
	{
		private readonly Regex numberRegex;
		private readonly bool onlyPositive;
		private readonly int precision;
		private readonly int scale;

		public NumberValidator(int precision, int scale = 0, bool onlyPositive = false)
		{
			this.precision = precision;
			this.scale = scale;
			this.onlyPositive = onlyPositive;
			if (precision <= 0)
				throw new ArgumentException("precision must be a positive number");
			if (scale < 0 || scale >= precision)
				throw new ArgumentException("scale must be a non-negative number less or equal than precision");
			numberRegex = new Regex(@"^([+-]?)(\d+)([.,](\d+))?$", RegexOptions.IgnoreCase);
		}

		public bool IsValidNumber(string value)
		{
			// Проверяем соответствие входного значения формату N(m,k), в соответствии с правилом, 
			// описанным в Формате описи документов, направляемых в налоговый орган в электронном виде по телекоммуникационным каналам связи:
			// Формат числового значения указывается в виде N(m.к), где m – максимальное количество знаков в числе, включая знак (для отрицательного числа), 
			// целую и дробную часть числа без разделяющей десятичной точки, k – максимальное число знаков дробной части числа. 
			// Если число знаков дробной части числа равно 0 (т.е. число целое), то формат числового значения имеет вид N(m).

			if (string.IsNullOrEmpty(value))
				return false;

			var match = numberRegex.Match(value);
			if (!match.Success)
				return false;

			// Знак и целая часть
			var intPart = match.Groups[1].Value.Length + match.Groups[2].Value.Length;
			// Дробная часть
			var fracPart = match.Groups[4].Value.Length;

			if (intPart + fracPart > precision || fracPart > scale)
				return false;

			if (onlyPositive && match.Groups[1].Value == "-")
				return false;
			return true;
		}
	}
}
