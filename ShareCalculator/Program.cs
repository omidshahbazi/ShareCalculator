using System;
using System.Collections.Generic;

namespace ShareCalculator
{
	class Program
	{
		class Person
		{
			public string Name;
			public uint SubsetPeople;
			public uint Spent;

			public float Balance;
		}

		static void Main(string[] args)
		{
			Console.WriteLine("Share Calculator");

			List<Person> persons = new List<Person>();

			while (true)
			{
				Console.WriteLine("Enter person name or leave it blank to end");

				Person person = new Person();

				person.Name = ReadLine<string>();
				if (string.IsNullOrEmpty(person.Name))
					break;

				Console.WriteLine("How many people are subset of {0}?", person.Name);
				person.SubsetPeople = ReadLine<uint>();
				person.SubsetPeople = Math.Max(1, person.SubsetPeople);

				Console.WriteLine("How much {0} spent?", person.Name);
				person.Spent = ReadLine<uint>();

				persons.Add(person);
			}

			Console.Clear();

			Console.WriteLine("=============================================================");

			for (int i = 0; i < persons.Count; ++i)
			{
				Person person = persons[i];

				Console.WriteLine("{0} with {1} people, spent {2}", person.Name, person.SubsetPeople, person.Spent);
			}

			float totalSpent = 0;
			float totalPeople = 0;
			for (int i = 0; i < persons.Count; ++i)
			{
				Person person = persons[i];

				totalSpent += person.Spent;
				totalPeople += person.SubsetPeople;
			}

			Console.WriteLine("=============================================================");
			float eachPersonShare = totalSpent / totalPeople;
			Console.WriteLine("Each one share is {0}", eachPersonShare);

			for (int i = 0; i < persons.Count; ++i)
			{
				Person person = persons[i];

				person.Balance = person.Spent - (person.SubsetPeople * eachPersonShare);
			}

			Console.WriteLine("=============================================================");

			for (int i = 0; i < persons.Count; ++i)
			{
				Person deptorPerson = persons[i];
				if (deptorPerson.Balance >= 0)
					continue;

				for (int j = 0; j < persons.Count; ++j)
				{
					Person creditorPerson = persons[j];
					if (creditorPerson.Balance <= 0)
						continue;

					float value = Math.Min(creditorPerson.Balance, Math.Abs(deptorPerson.Balance));

					creditorPerson.Balance -= value;

					deptorPerson.Balance += value;

					Console.WriteLine("{0} should pay {1}, {2}", deptorPerson.Name, creditorPerson.Name, value);
				}
			}

			Console.WriteLine("=============================================================");

			Console.ReadLine();
		}

		static T ReadLine<T>(T Default = default(T))
		{
			string line = Console.ReadLine();

			try
			{
				return (T)Convert.ChangeType(line, typeof(T));
			}
			catch
			{ }

			return Default;
		}
	}
}
