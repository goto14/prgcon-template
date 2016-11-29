using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

public class Program
{
    public void Proc() {
        int diceCount = int.Parse(Reader.ReadLine());

        Group rt = new Group();
        for(int i=0; i<diceCount; i++) {
            rt.Items.Add(new Dice());
        }

        long total = 0;
        while(rt.Items.Count > 0) {
            rt.Roll();
            long rolled = rt.GetTotal();
            total+=rolled;
            Console.WriteLine(rt.GetStr() + " " + total);
            rt.SetNext();
        }
    }

    public interface Node {
        long GetTotal();
        string GetStr();

        void Roll();

        void Sort();

        int SortKey();

    }

    
    public class Dice:Node {
        private static Random Rand = new Random();
        public void Roll() {
            this.Value = Rand.Next(1, 6);
        }

        public void Sort() {
        }

        public int Value;

        public long GetTotal() {
            return Value;
        }
        public string GetStr() {
            return Value.ToString();
        }
        public int SortKey() {
            return Value;
        }
        
    }

    public class Group:Node {
        public bool CanNext = true;
        public void SetNext() {
            Dictionary<int, List<Dice>> dc = new Dictionary<int, List<Dice>>();
            for(int i=this.Items.Count-1; i>=0; i--) {
                Dice d = this.Items[i] as Dice;
                Group g = this.Items[i] as Group;
                if(d == null) {
                    g.SetNext();
                    if(g.Items.Count == 0) {
                        this.Items.RemoveAt(i);
                    }
                } else {
                    if(!dc.ContainsKey(d.Value)) {
                        dc.Add(d.Value, new List<Dice>());
                    }
                    dc[d.Value].Add(d);
                    Items.RemoveAt(i);
                }
            }
            dc.Where(a=>a.Value.Count > 1).OrderByDescending(a=>a.Key).ToList().ForEach(a=>{
                Group grp = new Group();
                a.Value.ForEach(b=>grp.Items.Add(b));
                Items.Add(grp);
            });
        }


        public void Roll() {
            this.Items.ForEach(a=>a.Roll());
            this.Sort();
        }


        public void Sort() {
            this.Items.Sort((a,b)=>{
                if(a.GetType() != b.GetType()) {
                    if((a as Group) != null) {
                        return -1;
                    } else {
                        return 1;
                    }
                } else {
                    return a.SortKey().CompareTo(b.SortKey()) * -1;
                }
            });
            this.Items.ForEach(a=>a.Sort());
        }

        public List<Node> Items = new List<Node>();
        public long GetTotal() {
            return Items.Sum(a=>a.GetTotal());
        }
        public string GetStr() {
            return "{" + string.Join(",", Items.Select(a=>a.GetStr())) + "}";
        }

        public int SortKey() {
            return this.Items.First().SortKey();
        }
    }


    public class Reader {
        public static bool IsDebug = true;
        private static System.IO.StringReader SReader;
        private static string InitText = @"



100


";
        public static string ReadLine() {
            if(IsDebug) {
                if(SReader == null) {
                    SReader = new System.IO.StringReader(InitText.Trim());
                }
                return SReader.ReadLine();
            } else {
                return Console.ReadLine();
            }
        }

    }
    public static void Main(string[] args)
    {
        Program prg = new Program();
        prg.Proc();
    }
}
