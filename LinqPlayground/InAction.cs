using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LinqPlayground
{
    public static class InAction
    {
        private static readonly List<Person> People = Person.GetPeople();

        private static readonly List<Pet> Pets = Pet.GetPets();

        /// <summary>
        /// Min, Max, Average, Sumなどの集計関数の例
        /// </summary>
        public static void MinMaxAverage()
        {
            //デモ用配列（整数）
            var array = Enumerable.Range(1, 1000).ToArray();

            Console.WriteLine("最大値: " + array.Max());
            Console.WriteLine("最小値: " + array.Min());
            Console.WriteLine("平均値: " + array.Average());
            Console.WriteLine("総和: " + array.Sum());

            //Nullable
            //int型の最年長の年齢を抽出
            Console.WriteLine(People.Max(p => p.Age));
            //double型も同じメソッドで抽出可能(single, float, decimalも可能)
            var tallest = People.Where(p => p.Height == People.Max(person => person.Height));
            foreach (var person in tallest)
            {
                Console.WriteLine("{0}は{1}センチで一番身長が高いです", person.Name, person.Height);
            }
            //デリゲート入り
            //BMIを求め、BMIが最小の値を抽出
            //BMIは体重(kg)÷身長(m)^2
            Console.WriteLine("最小のBMIは{0:F}です", People.Min(p => p.Weight/Math.Pow(p.Height*0.01, 2)));
        }

        /// <summary>
        /// All, Any, Containsなどの例
        /// </summary>
        public static void AllAnyContains()
        {
            //Allメソッドで集合の構成要素全てが条件を満たしているか判定する
            Console.WriteLine("皆さん成人でお酒は飲めますね？");
            Console.WriteLine(People.All(p => p.Age >= 20) ? "皆さん二十歳以上ですもんね" : "未成年がいましたか...");
            
            //Anyメソッドは要素のいづれかが条件を満たしているかを判定する
            //引数なしで要素の有無を簡単に確認できる
            Console.WriteLine(People.Any());//true
            Console.WriteLine(Enumerable.Empty<string>().Any());//false
            //Anyメソッドにデリゲートを入れて条件を満たすデータの有無を確認できる
            Console.WriteLine(People.Any(p => p.Height > 180.0));
            
            //Containsは値型のデータ比較によって正しい場合trueを返す
            //文字列オブジェクトは参照型だが、参照先が同じなので正しく判定される
            //Console.WriteLine(people.Contains();
            //IEqualityCompererを継承し値型としてEqualsメソッドが定義されていない場合はきちんと判定されるかわからない
        }

        /// <summary>
        /// Count, LongCountの例
        /// </summary>
        public static void CountExamples()
        {
            var array = Enumerable.Repeat(0, Int32.MaxValue);
            var longArray = array.Concat(new [] {0});
            //オーバーフローする
            try
            {
                Console.WriteLine(longArray.AsParallel().Count());
            }
            catch (AggregateException e)
            {
                foreach (var exception in e.InnerExceptions)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("やっぱりオーバーフローしちゃった・・・");
                }
            }
            //オーバーフローしない
            Console.WriteLine(int.MaxValue);
            Console.WriteLine("32bit整数型最大数を超える要素を持つコレクションの要素数: " + longArray.LongCount());        
        }

        public static void BasicSqlLikeMethods()
        {
            //SQLのように書ける
            var olds = People.Where(p => p.Age >= 65).Select(p => p.Name);
            foreach (var old in olds)
            {
                Console.WriteLine("{0}さんは高齢者です", old);
            }
            //SELECTでは新しいオブジェクトを生成することも可能
            //匿名オブジェクトを作成出来る
            //なお、この型をメソッドの戻り値には出来ないがその場の処理で使い捨てならば問題ない
            //(Dynamic型で返すという手も無きにしもあらずだが、C#書いている意味がないはず)
            var bmis =
                People.Where(p => p.Weight.HasValue)
                    .Select(
                        p =>
                            new
                            {
                                p.Name,
                                p.Height,
                                p.Weight,
                                Bmi = p.Weight / Math.Pow(p.Height * 0.01, 2) 
                                //このようなプロパティはpersonクラスには存在しない
                            })
                    .OrderByDescending(p => p.Bmi)
                    .ThenBy(p => p.Weight);
            foreach (var bmi in bmis)
            {
                Console.WriteLine("{0}さんのBMIは{1:F}です(身長:{2}, 体重:{3})", 
                    bmi.Name, bmi.Bmi, bmi.Height, bmi.Weight);
            }
        }

        /// <summary>
        /// 年代別の集計をgroup by一本で
        /// </summary>
        public static void GroupByIEnumerable()
        {
            //SQLでいうところの
            //SELECT Age, count(*) from people group by age
            var q = People.OrderBy(p => p.Age).GroupBy(
                        p => Math.Truncate(p.Age / 10.0), //キーを選択
                        (key, p) => new { generation = (key * 10) + "代", count = p.Count()});//結果を選択

            foreach (var p in q)
            {
                Console.WriteLine("{0}の人は{1}人います", p.generation, p.count);
            }
        }
        /// <summary>
        /// 年代別の集計を行う。SQLとは異なり、集計した要素以外の要素も参照できるので
        /// ひと味違った結果が得られる
        /// </summary>
        public static void GroupByIGrouping()
        {
            var q = People.OrderBy(p => p.Age).GroupBy(p => Math.Truncate(p.Age / 10.0));

            foreach (var p in q)
            {
                //1つ目のループでキーの要素が取得可能
                Console.WriteLine("{0}代の人:", p.Key * 10);
                //2つ目のループでそのキーを持つデータがコレクションとして抽出可能
                foreach (var person in p)
                {
                    Console.WriteLine("{0}歳: {1}さん", person.Age, person.Name);
                }
            }
        }

        /// <summary>
        /// 集合演算（カラム方向）
        /// </summary>
        public static void JoinGroupJoinExample()
        {
            //Joinメソッドの用法
            //基本的に1対1のデータの結合処理を行う
            //SQLのinner joinに相当
            var q = Pets.Join(People, //Join対象のコレクション
                p => p.OwnerId, //Joinする際のJoinされる側のコレクションのキー項目
                p => p.Id, //Join対象のコレクションのキー項目
                (pet, person) =>
                    new {PetName = pet.Name, OwnerName = person.Name});
                                  //戻り値となる要素

            foreach (var petowner in q)
            {
                Console.WriteLine("{0}の飼い主は{1}さんです", petowner.PetName, petowner.OwnerName);
            }

            //GroupJoin使用例
            //基本的に1対多の結合処理を行う
            //SQLでいうLeft joinに相当
            var q1 = People.GroupJoin(Pets, p => p.Id, p => p.OwnerId,
                (owner, pets) => new {OwnerName = owner.Name, Pets = pets});

            foreach (var owner in q1)
            {
                Console.WriteLine("{0}さんのペットは以下のペットです", owner.OwnerName);
                foreach (var pet in owner.Pets)
                {
                    Console.WriteLine("ペット名: {0}", pet.Name);
                }
            }

            //ループが2回で紛らわしい場合はSelectManyを用いることで一回のforeachで
            //列挙出来るようにコレクションをフラットにすることが出来る
            
            //Zipメソッドで2つのコレクションをファスナーのようにつなぎ合わせることが可能
            var men = People.Where(p => p.Gender == Gender.Male);
            var women = People.Where(p => p.Gender == Gender.Female);
            var couples = men.Zip(women,
                (m, w) => string.Format("{0}さん({1}) ♡ {2}さん({3})", m.Name, m.Age, w.Name, w.Age));

            foreach (var couple in couples)
                Console.WriteLine(couple);        
        }

        /// <summary>
        /// 集合演算を行うメソッド（レコード方向）
        /// </summary>
        public static void UnionExceptIntersectSamples()
        {
            var young = People.Where(p => p.Age < 35).ToArray();
            var middle = People.Where(p => p.Age > 25 && p.Age < 55).ToArray();

            var union = young.Union(middle);
            var except = young.Except(middle);
            var inters = young.Intersect(middle);

            Console.WriteLine("Unionの結果");
            foreach (var person in union)
            {
                Console.WriteLine("{0}さん({1}歳)", person.Name, person.Age);
            }

            Console.WriteLine("Exceptの結果");
            foreach (var person in except)
            {
                Console.WriteLine("{0}さん({1}歳)", person.Name, person.Age);
            }
            Console.WriteLine("Intersectの結果");
            foreach (var person in inters)
            {
                Console.WriteLine("{0}さん({1}歳)", person.Name, person.Age);
            }
        }

        /// <summary>
        /// SelectMany
        /// </summary>
        public static void AdvancedSqlLikeMethods()
        {
            
        }

        public static void FirstLastExamples()
        {
            var heightSeries = People.OrderByDescending(p => p.Height).ToArray();
            var tallest = heightSeries.First();
            var smallest = heightSeries.Last();

            Console.WriteLine("一番背が高いのは{0}さんです({1}cm)", tallest.Name, tallest.Height);
            Console.WriteLine("一番背が低いのは{0}さんです({1}cm)", smallest.Name, smallest.Height);

            var babies = People.Where(p => p.Age < 1).ToArray();

            try
            {
                Console.WriteLine(babies.First());//Error!
                Console.WriteLine(babies.Last());//Error!
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }          
            Console.WriteLine(babies.FirstOrDefault());//Errorにならずnullを返す
            Console.WriteLine(babies.LastOrDefault());//Errorにならずnullを返す
            //C# 6だったらnull判定演算子を用いて以下のように記述可能
            Console.WriteLine("{0}ちゃんこんにちは！", babies.FirstOrDefault()?.Name);

            //Singleメソッドを用いてキー項目で抽出したユニークデータを抽出可能
            Pet dog = null;

            try
            {
                //犬は2匹いるからエラー
                dog = Pets.Single(p => p.Animal == Animal.Dog);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            var rabbit = Pets.Single(p => p.Animal == Animal.Rabbit);
            Console.WriteLine("ペットのうさぎの名前は{0}です", rabbit.Name);
        }

        public static void SkipTakeExamples()
        {
            
        }

        /// <summary>
        /// LINQの実行結果のコレクションを特定のコレクション型に変換するメソッド
        /// </summary>
        public static void ConvertMethods()
        {
            
        }

        
    }
}
