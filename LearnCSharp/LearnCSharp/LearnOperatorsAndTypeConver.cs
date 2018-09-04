using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCSharp
{
	public class LearnOperatorsAndTypeConver
	{
		public void TestOperators()
		{
			//chedked 执行溢出检查，如果溢出会抛出OverflowException异常
			//unchedked 禁用溢出检查，这是默认选项
			byte b = 255;
			//checked
			//{
			//	b++;
			//}
			unchecked
			{
				b++;
			}
			Console.WriteLine(b);

			/*is运算符
			*检测对象是否是特定的类型兼容
			* 兼容表示对象或者该类型，或者派生自该类型
			*/
			int i = 10;
			if (i is object)
			{
				Console.WriteLine("i is an object");
			}
			ChinesePerson cp = new ChinesePerson();
			if (cp is Person)
			{
				Console.WriteLine("cp is Person");
			}

			/*as 运算符
			*用于执行引用类型的显式类型转换。
			* 如果要转换的类型与指定的类型兼容，转换就会成功进行。
			* 如果类型不兼容，as运算符就会返回null值
			* as运算符允许在一步中进行安全的类型转换，不需要先使用is运算符测试类型，在执行转换。
			*/
			object obj1 = "Some String";
			object obj2 = 5;

			string str1 = obj1 as string;
			string str2 = obj2 as string;
			Console.WriteLine(str1);
			Console.WriteLine(str2);

			/*sizeof运算符
			 * 确定栈中值类型需要的长度（单位是字节）。
			 * 对于复杂类型（和非基元类型）使用sizeof运算符需要把代码放入unsafe块中
			 */
			Console.WriteLine(sizeof(int));
			//unsafe
			//{
			//	Console.WriteLine(sizeof(Person));
			//}

			/*typeof运算符
			 * typeof(string)返回表示System.String类型的Type对象
			 */
			Console.WriteLine(typeof(string)); //System.String

			/*可空类型（?）和运算符
			 * 如果在程序中使用可空类型，就必须考虑null值在于各种运算符一起使用时的影响
			 * 通常可空类型于医院或二院运算符一起使用时，如果其中一个操作数或两个操作数都是null，其结果就是null
			 * 在比较可空类型时，只要其中一个操作数时null，比较的结果就是false
			 * null可能值表示，不能随意合并表达式中的可空类型和非可空类型
			 */
			int? nullable1 = null;
			int? nullable2 = 5;
			int? result1 = nullable1 + nullable2;
			int? result2 = nullable1 * nullable2;
			Console.WriteLine(result1);
			Console.WriteLine(result2);

			if (nullable1 >= nullable2)
			{
				Console.WriteLine("a >= b");
			}
			else
			{
				Console.WriteLine("a < b");
			}

			/*空合并运算符（??）
			 * 可以在处理可空类型和引用类型时表示null可能的值
			 * 这个运算符放在两个操作数之间，第一个操作数必须是一个可空类型或引用类型；第二个操作数必须与第一个操作数的类型相同，或者可以隐式转换成第一个操作数的类型。
			 * 如果第一个操作数不是null，真个表达式就等于第一个操作数的值。
			 * 如果第一个操作数时null，整个表达式就等于第二个操作数的值。
			 * 如果第二个操作数不能隐式转换成第一个操作数的类型，就会生成一个编译错误。
			 */

			int? nullint1 = null;
			int c = 10;

			int result3 = nullint1 ?? c;  //result3 has the value 10
			Console.WriteLine(result3);
			nullint1 = 3;
			result3 = nullint1 ?? 10; //result3 has value 3
			Console.WriteLine(result3);

			/*int和string类型转换
			 */
			int convertInt = 10;
			string stringResult = convertInt.ToString();
			Console.WriteLine(stringResult);

			string convertString = "100";
			int intResult = int.Parse(convertString);
			Console.WriteLine(intResult + 50);

			//对比相等性
			Person person1 = new Person();
			Person person2 = new Person();
			Person person3;
			person3 = person1;
			bool boolResult1 = ReferenceEquals(person1,person2); //False
			bool boolResult2 = ReferenceEquals(person1, person3);//True ReferenceEquals对比的是指针地址。
			bool boolResult3 = ReferenceEquals(person1, null); //False
			bool boolResult4 = ReferenceEquals(null, null); //True :ReferenceEquals认为null等于null。

			int equalInt1 = 5;
			int equalInt2 = 5;
			bool boolResult5 = ReferenceEquals(equalInt1, equalInt2); //False: ReferenceEquals对比值类型时总是返回False，因为要装箱，装箱后变成不同对象

			Console.WriteLine(boolResult1);
			Console.WriteLine(boolResult2);
			Console.WriteLine(boolResult3);
			Console.WriteLine(boolResult4);
			Console.WriteLine(boolResult5);





		}

		public void TestOperatorReload()
		{
			Vector vector1 = new Vector(1.4,3.5,4.6);
			Vector vector2 = new Vector(3.6,5.5,6.4);
			Vector result = vector1 + vector2;

			Console.WriteLine(result.ToString());
			Console.WriteLine((2.5 * vector1).ToString());
			Console.WriteLine((vector1 * 2.5).ToString());
		}

	}

	//运算符重载
	struct Vector
	{
		public double x, y, z;

		public Vector(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vector(Vector rhs)
		{
			x = rhs.x;
			y = rhs.y;
			z = rhs.z;
		}

		public override string ToString()
		{
			return "( " + x + ", " + y + ", " + z +" )";
		}

		/*C#要求所有的运算符重载都声明为 public static，这表示它们与它们的类或者结构相关联，而不是与某个特定实例相关联，所以运算符重载的代码体不能访问非静态成员，
		 * 也不能防伪this标识符
		 * operator关键字告诉编译器，它实际上是一个自定义的运算符重载，后面的相关运算符的实际符号。
		 * 对于二元运算符，第一个参数是左边的，第二个参数是右边的。
		 */
		public static Vector operator +(Vector lhs, Vector rhs)
		{
			Vector result = new Vector(lhs);
			result.x += rhs.x;
			result.y += rhs.y;
			result.z += rhs.z;

			return result;
		}

		public static Vector operator *(double lhs, Vector rhs)
		{
			Vector result = new Vector(rhs);
			result.x = result.x * lhs;
			result.y = result.y * lhs;
			result.z = result.z * lhs;

			return result;
		}

		public static Vector operator *(Vector lhs, double rhs)
		{
			return rhs * lhs;
		}

		/*比较运算符重载
		 * 
		 */

	}

	public class ChinesePerson : Person
	{
		public string province { get; set; }
	}
}
