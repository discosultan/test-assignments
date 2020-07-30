using System;
using System.Collections.Generic;

namespace FlattenList
{
    internal class Program
    {
        // ülessanne: implementeeri meetod "PrintList" nii et oodatud tulemus pinditakse konsooli

        // sisendandmed: sisendiks on objekt TestData, mida võib vaadelda Listina, mille igaks elemendiks on List või Väärtus
        // JSON'ina visualiseerituna oleks TestData:
        // {"a",{"b","c",{"d","e"},{"f"},"g"},"h","i",{"j","k"}}
        // C# sisendandmete struktuur:
        // * abstraktne klass Node -  sellest koosnevad List'id
        // * klass ValueNode:Node - klass millega hoitakse listis Väärtust
        // * klass ListNode:Node - klass millega hoitakse listis alamlisti

        // oodatud tulemus:
        /*
		0:A
		1.0:B
		1.1:C
		1.2.0:D
		1.2.1:E
		1.3.0:F
		1.4:G
		2:H
		3:I
		4.0:J
		4.1:K
		*/
        // ehk prinditakse "elemendi positioon listis : väärtus" juhul kui tegu on alamlistiga, prinditakse selle ette "alamlisti positsioon listis"+'.' jne.

        // lahendamise juhised:
        // kasutada rekursiooni, tulemus peab olema korrektne iga sisendi korral
        // eeldada võib et Value/Values ei ole kunagi NULL (ValueNode.Values võib küll olla tühi list)
        // lahendus (funktsiooni PrintList sisu) peaks olema mõistliku pikkusega (kuni ~10 rida)

        private static void Main()
        {
            //prindib tulemuse
            PrintList(TestData);
            //ootab kasutaja inputi enne akna sulgemist
            Console.ReadLine();
        }

        //static string PrintList(List<Node> l,string p="",int i=0)
        //{
        //    l.ForEach(n => Console.Write((n is ValueNode) ? $"{p}{i++}:{((ValueNode)n).Value.ToUpper()}\n" : PrintList(((ListNode)n).Values, $"{p}{i++}.")));
        //    return "";
        //}

        static void PrintList(List<Node> l,string p="",int i=0)
        {
            l.ForEach(n=>(n is ValueNode?()=>Console.Write($"{p}{i++}:{((ValueNode)n).Value}\n"):(Action)(()=>PrintList(((ListNode)n).Values,$"{p}{i++}.")))());
        }

        //static void PrintList(IEnumerable<dynamic> testData, string s = "")
        //{
        //    testData.Select((v, i) => new Dictionary<bool, Action> { { true, () => Console.WriteLine(s + i + ":" + v["Value"]) }, { false, () => PrintList(v["Values"], s + i + ".") }}).ToList().ForEach(x => x[x.First() is ValueNode]());

        //    //    foreach (var el in testData.Select((v, i) => new { v, ix = i }))
        //    //    {
        //    //        new Dictionary<bool, Action> {
        //    //            {true, () => Console.WriteLine(s + el.ix + ":" + ((dynamic)el.v).Value)},
        //    //            {false, () => PrintList(((dynamic)el.v).Values, s + el.ix + ".")}
        //    //        }[el.v is ValueNode]();
        //    //    }
        //}

        //        static void PrintList(IEnumerable<dynamic> l)

        //static void PrintList(IEnumerable<dynamic> l,string s="")
        //{
        //    l.Select((v,i)=>new{v,j=i}).ToList().ForEach(el=>new Dictionary<bool,Action>{{true,()=>Console.WriteLine(s+el.j+":"+el.v.Value.ToUpper())},{false,()=>PrintList(el.v.Values,s+el.j+".")}}[el.v is ValueNode]());
        //}

        public static readonly List<Node> TestData = new List<Node>
        {
            new ValueNode {Value = "a"},
            new ListNode
            {
                Values = new List<Node>
                {
                    new ValueNode {Value = "b"},
                    new ValueNode {Value = "c"},
                    new ListNode
                    {
                        Values = new List<Node>
                        {
                            new ValueNode {Value = "d"},
                            new ValueNode {Value = "e"}
                        }
                    },
                    new ListNode
                    {
                        Values = new List<Node>
                        {
                            new ValueNode {Value = "f"}
                        }
                    },
                    new ValueNode {Value = "g"}
                }
            },
            new ValueNode {Value = "h"},
            new ValueNode {Value = "i"},
            new ListNode
            {
                Values = new List<Node>
                {
                    new ValueNode {Value = "j"},
                    new ValueNode {Value = "k"}
                }
            },
        };

        /// <summary>
        /// abstraktne klass, kõik List'id koosnevad selle klassi alamklassidest
        /// </summary>
        public abstract class Node
        {
            protected object Data;
        }
        /// <summary>
        /// klass väärtuse hoidmiseks
        /// </summary>
        public class ValueNode : Node
        {
            public string Value
            {
                get { return (string)Data; }
                set { Data = value; }
            }
        }
        /// <summary>
        /// klass alamlisti hoidmiseks
        /// </summary>
        public class ListNode : Node
        {
            public List<Node> Values
            {
                get { return (List<Node>)Data; }
                set { Data = value; }
            }
        }
    }
}
