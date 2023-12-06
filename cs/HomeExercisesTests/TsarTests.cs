using FluentAssertions;
using NUnit.Framework;

namespace HomeExercises
{
	public class TsarTests
	{
		[Test]
		[Description("Проверка текущего царя")]
		[Category("ToRefactor")]
		public void CheckCurrentTsar()
		{
			var actualTsar = TsarRegistry.GetCurrentTsar();

			var expectedTsar = new Person("Ivan IV The Terrible", 54, 170, 70,
				new Person("Vasili III of Russia", 28, 170, 60, null));

			// Перепишите код на использование Fluent Assertions.
			actualTsar.Should().BeEquivalentTo(expectedTsar, parameters =>
				parameters.Excluding(tsar =>
					tsar.SelectedMemberInfo.DeclaringType == typeof(Person) &&
					tsar.SelectedMemberInfo.Name.Equals(nameof(Person.Id))));

			/*
			 * Такой тест не придётся переписывать при добавлении новых полей и свойств, за исключением тех,
			 * которые не должы совпадать, например ID. Так же более информативно показывается, почему не одинаковы
			 * цари.
			 */
		}

		[Test]
		[Description("Альтернативное решение. Какие у него недостатки?")]
		public void CheckCurrentTsar_WithCustomEquality()
		{
			var actualTsar = TsarRegistry.GetCurrentTsar();
			var expectedTsar = new Person("Ivan IV The Terrible", 54, 170, 70,
				new Person("Vasili III of Russia", 28, 170, 60, null));

			// Какие недостатки у такого подхода? 
			Assert.True(AreEqual(actualTsar, expectedTsar));
			/*
			 * 1 Такой тест не даст информативный ответ, почему не одинаковы цари.
			 * 2 При добавлении нового поля или свойства, придётся менять функцию AreEqual, иначе тест будет невалидным.
			 * 3 Достаточно сломать или написать AreEqual не правильно, так что тест перестанет проходить,
			 *		а понять это будет сложно.
			 * 4 Сложно читать такую портянку из логических условий и не путаться.
			 * 5 Мы отдаём тестирование другой функции, а не проводим его в тесте, из-за чего можем забыть поменять её,
			 *		от чего тест станет невалидным
			 * 6 Рекурсивный вызов, что может привести к переполнению стека, если Person1.Parent.Parent = Person1
			 */
		}

		private bool AreEqual(Person? actual, Person? expected)
		{
			if (actual == expected) return true;
			if (actual == null || expected == null) return false;
			return
				actual.Name == expected.Name
				&& actual.Age == expected.Age
				&& actual.Height == expected.Height
				&& actual.Weight == expected.Weight
				&& AreEqual(actual.Parent, expected.Parent);
		}
	}
}
