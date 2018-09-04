using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;

namespace LearnCSharp
{
	public class LearnArray
	{
		/*数组可以支持协变，但协变只能支持引用类型，不适用值类型。
		 * 协变只能在运行时才能发现错误。
		 */
		public void TestArray()
		{
			Person[] myPersons = new Person[2];
			myPersons[0] = new Person { FirstName="Jun",SecondName="Wang" };
			myPersons[1] = new Person { FirstName = "Rui", SecondName = "Yang" };
			Console.WriteLine(myPersons[0].ToString() + "\n" + myPersons[1].ToString());

			Person[] myPersons2 =
			{
				new Person{FirstName="James",SecondName="Zhang"},
				new Person{FirstName="Tomas",SecondName="SiMa"}
			};

			Console.WriteLine(myPersons2[0].ToString() + "\n" + myPersons2[1].ToString());

			//多维数组
			int[,] twoDim =
			{
				{1,2,3 },
				{4,5,6 },
				{7,8,9 }
			};

			int[,,] threeDim =
			{
				{{1,2},{3,4} },
				{{5,6},{7,8} },
				{{9,10},{11,12} }
			};

			//锯齿数组（交叉数组）
			int[][] jagged = new int[3][];
			jagged[0] = new int[2] { 1, 2 };
			jagged[1] = new int[5] { 3, 4, 5, 6, 7 };
			jagged[2] = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

			for(int row = 0;row < jagged.Length;row++)
			{
				for(int element = 0;element < jagged[row].Length;element++)
				{
					Console.WriteLine("Row:{0},Element:{1},Value:{2}",row,element,jagged[row][element]);
				}
			}

			//Array类
			Array intArray1 = Array.CreateInstance(typeof(int),5);
			for(int i = 0;i < 5;i++)
			{
				intArray1.SetValue(33,i);
			}

			for(int i = 0;i < 5;i++)
			{
				Console.WriteLine(intArray1.GetValue(i));
			}

			//强制转换成int数组
			int[] intArray2 = (int[])intArray1;
			//CreateInstance创建一个2x3的数组
			int[] lengths = { 2,3};
			int[] lowerBounds = { 1, 10 };
			//第一维基于1，第二维基于10
			Array racers = Array.CreateInstance(typeof(Person),lengths,lowerBounds);

			racers.SetValue(new Person { FirstName="AAA",SecondName="111"},1,10);
			racers.SetValue(new Person { FirstName = "BBB", SecondName = "222" }, 1, 10);
			racers.SetValue(new Person { FirstName = "CCC", SecondName = "333" }, 1, 11);
			racers.SetValue(new Person { FirstName = "DDD", SecondName = "444" }, 1, 12);
			racers.SetValue(new Person { FirstName = "EEE", SecondName = "555" }, 2, 10);
			racers.SetValue(new Person { FirstName = "FFF", SecondName = "666" }, 2, 11);
			racers.SetValue(new Person { FirstName = "GGG", SecondName = "777" }, 2, 12);

			Person[,] racers2 = (Person[,])racers;
			Person first = racers2[1, 10];
			Person second = racers2[2, 12];

			//Clone()方法会创建数组的浅层副本，如果元素是值类型，会复制所有值，修改原数组内容副本内容不会变化
			int[] cloneArray1 = new int[] { 1, 2, 3 };
			int[] cloneArray2 = (int[])cloneArray1.Clone();
			cloneArray1[0] = 100;
			Console.WriteLine(cloneArray2[0]); //输出1

			//如果元素是引用类型，则会复制元素的引用。
			Person[] clonePersons = (Person[])myPersons.Clone();
			myPersons[0].FirstName = "000";
			myPersons[0].SecondName = "VVV";
			//修改元素内容副本元素的内容也会变化
			Console.WriteLine(clonePersons[0].ToString());//输出：000 VVV
			//重置原数组元素副本内容不会受影响
			myPersons[0] = new Person { FirstName = "MMM", SecondName = "---" };
			Console.WriteLine(clonePersons[0].ToString()); //输出：000 VVV

			/*Copy()方法也会创建浅层复制，区别在于：
			 *Copy()方法:必须传递阶数相同且有足够元素的已有数组。
			 * Clone()方法：会创建一个新数组。
			 * 如果需要创建包含引用类型的数组的深层副本，就必须迭代数组病创建新对象。
			 */
		}

		/*数组段：ArraySegment<T>
		 *返回数组的一部分
		 * 只需要传入一个参数，结构体中包含数组段的偏移量和个数
		 * 数组段不复制原数组的元素，但原数组可以通过数组段访问，如果数组段的元素变化了，这些变化会反映到原数组中。
		 */
		 //计算数组段所有元素的和
		 static int SumOfSegments(ArraySegment<int>[] segments)
		{
			int sum = 0;
			foreach(var segment in segments)
			{
				for(int i = segment.Offset;i < segment.Offset + segment.Count;i++)
				{
					sum += segment.Array[i];
				}
			}
			return sum;
		}

		public void CalculateArraySum()
		{
			int[] intArray = new int[] { 1,2,2,3,3,3,4,4,4,4,5,5,5,5,5,6,6,6,6,6,6};
			var segments = new ArraySegment<int>[6]
			{
				new ArraySegment<int>(intArray,0,1),
				new ArraySegment<int>(intArray,1,2),
				new ArraySegment<int>(intArray,3,3),
				new ArraySegment<int>(intArray,6,4),
				new ArraySegment<int>(intArray,10,5),
				new ArraySegment<int>(intArray,15,6)
			};
			int sum = SumOfSegments(segments);
			Console.WriteLine(sum);
		}

		public void TestSortArray()
		{
			/*数组排序需要元素的类里实现IComparable接口
			 * string和int已经实现，自定义类需要自己继承接口并实现方法CompareTo()
			 */
			string[] stringArray = new string[] { "Z","G","A","T","1","F","90"};
			Array.Sort(stringArray);
			foreach(var str in stringArray)
			{
				Console.WriteLine(str);
			}

			//Person对象排序
			Person[] sortPersons1 = new Person[]
			{
				new Person{FirstName="GGG",SecondName="QQQ"},
				new Person{FirstName="AAA",SecondName="QQQ"},
				new Person{FirstName="FFF",SecondName="UUU"},
				new Person{FirstName="111",SecondName="AAA"}
			};

			Array.Sort(sortPersons1);
			foreach(var p in sortPersons1)
			{
				Console.WriteLine(p.ToString());
			}

			Array.Sort(sortPersons1, new PersonComparer(PersonCompareType.FirstName));
			foreach (var p in sortPersons1)
			{
				Console.WriteLine(p.ToString());
			}

		}

	}

	//IComparer:IComparer独立于要比较的类，所以他的Compare方法有两个参数
	public enum PersonCompareType
	{
		FirstName,
		SecondName
	}

	public class PersonComparer : IComparer<Person>
	{
		private PersonCompareType compareType;
		
		public PersonComparer(PersonCompareType type)
		{
			compareType = type;
		}

		public int Compare(Person person1,Person person2)
		{
			if (person1 == null) throw new ArgumentNullException("person1");
			if (person2 == null) throw new ArgumentNullException("person2");

			switch (compareType)
			{
				case PersonCompareType.FirstName:
					return person1.FirstName.CompareTo(person2.FirstName);
				case PersonCompareType.SecondName:
					return person1.SecondName.CompareTo(person2.SecondName);
				default:
					throw new ArgumentException("Unexcepted Sort Type!");
			}
		}
	}

	//继承IComparable<Person>接口
	public class Person:IComparable<Person>,IEquatable<Person>
	{
		public int Id { get; private set; }

		public string FirstName { get; set; }
		public string SecondName { get; set; }

		public override bool Equals(object obj)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			return Equals(obj as Person);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public bool Equals(Person other)
		{
			if (other == null)
				throw new ArgumentNullException("other");

			return this.Id == other.Id && this.FirstName == other.FirstName && this.SecondName == other.SecondName;
		}

		public override string ToString()
		{
			return String.Format("{0} {1}",FirstName,SecondName);
		}

		public int CompareTo(Person other)
		{
			if (other == null)
				throw new ArgumentNullException("other");

			int result = this.SecondName.CompareTo(other.SecondName);
			if(result == 0)
			{
				result = this.FirstName.CompareTo(other.FirstName);
			}

			return result;
		}

	}

	public class HelloCollection
	{
		public IEnumerator<string> GetEnumerator()
		{
			yield return "Hello";
			yield return "World";
		}
	}
}
