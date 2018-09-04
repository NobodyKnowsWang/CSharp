using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCSharp
{
	class Program
	{
		static void Main(string[] args)
		{
			LearnArray arrayTest = new LearnArray();
			arrayTest.TestArray();
			arrayTest.TestSortArray();
			arrayTest.CalculateArraySum();

			HelloCollection hc = new HelloCollection();
			foreach (var str in hc)
			{
				Console.WriteLine(str);
			}

			var tuple = Tuple.Create<string, string, string, int, int, int, double, Tuple<string, string>>
				("A","B","C",1,2,3,4.5,
				Tuple.Create<string,string>("AAA","BBB")
				);

			var janet = new Person{ FirstName = "Janet",SecondName ="Jackson"};
			var janet1 = new Person { FirstName = "Janet", SecondName = "Jackson" };
			Person[] persons1 = new Person[] {
				new Person{FirstName = "Jun", SecondName="Wang" },
				janet
			};
			Person[] persons2 = new Person[] {
				new Person{FirstName = "Jun", SecondName="Wang" },
				janet
			};

			if (janet == janet1)
			{
				Console.WriteLine("相等");
			}
			else
			{
				Console.WriteLine("不相等");
			}

			LearnOperatorsAndTypeConver ope = new LearnOperatorsAndTypeConver();
			ope.TestOperators();
			ope.TestOperatorReload();

		}
	}
}
