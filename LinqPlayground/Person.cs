using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPlayground
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public double Height { get; set; }
        public double? Weight { get; set; }

        public static List<Person> GetPeople()
        {
            return new List<Person>()
            {
                new Person() {Id = 1, Name = "山田一郎", Age = 20, Gender = Gender.Male, Height = 180.5, Weight = 60.0},
                new Person() {Id = 2, Name = "田中花子", Age = 25, Gender = Gender.Female, Height = 160.3},
                new Person() {Id = 3, Name = "斉藤大介", Age = 30, Gender = Gender.Male, Height = 165.0, Weight = 65.2},
                new Person() {Id = 4, Name = "佐藤次郎", Age = 10, Gender = Gender.Male, Height = 134.1 },
                new Person() {Id = 5, Name = "伊藤景子", Age = 22, Gender = Gender.Female, Height = 161.2 },
                new Person() {Id = 6, Name = "鈴木三郎", Age = 46, Gender = Gender.Male, Height = 178.3, Weight = 70.8},
                new Person() {Id = 7, Name = "渡辺幸子", Age = 37, Gender = Gender.Female, Height = 188.4 },
                new Person() {Id = 8, Name = "小林四郎", Age = 29, Gender = Gender.Male, Height = 155.5 },
                new Person() {Id = 9, Name = "加藤五郎", Age = 57, Gender = Gender.Male, Height = 170.6 },
                new Person() {Id = 10, Name = "佐々木桜子", Age = 38, Gender = Gender.Female, Height = 165.7 },
                new Person() {Id = 11, Name = "松本正雄", Age = 63, Gender = Gender.Male, Height = 177.8, Weight = 84.2},
                new Person() {Id = 12, Name = "木村健", Age = 76, Gender = Gender.Male, Height = 190.9 },
                new Person() {Id = 13, Name = "山本明", Age = 19, Gender = Gender.Male, Height = 151.4 },
                new Person() {Id = 14, Name = "中村正子", Age = 31, Gender = Gender.Female, Height = 149.2 },
                new Person() {Id = 15, Name = "山崎愛子", Age = 62, Gender = Gender.Female, Height = 159.1 },
                new Person() {Id = 16, Name = "森浩子", Age = 58, Gender = Gender.Female, Height = 178.1 },
                new Person() {Id = 17, Name = "池田駿", Age = 22, Gender = Gender.Male, Height = 193.6, Weight = 108.3 },
                new Person() {Id = 18, Name = "大田仁", Age = 17, Gender = Gender.Male, Height = 181.3, Weight = 81.1},
                new Person() {Id = 19, Name = "藤原翼", Age = 44, Gender = Gender.Female, Height = 162.1 },
                new Person() {Id = 20, Name = "金子明日香", Age = 36, Gender = Gender.Female, Height = 167.9 }
            };
        }
    }

    

    public enum Gender
    {
        Female,
        Male
    }

    public enum Animal
    {
        Dog,
        Cat,
        Bird,
        Fish,
        Rabbit,
        Other
    }
}
