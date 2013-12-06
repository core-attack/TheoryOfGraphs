using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TheoryOfGraphs
{
    class Top
    {
        string name = "";
        int number = 0;
        double weight = 0;
        int rang = 0;
        Color color = Color.White;
        //начальная вершина в этих дугах - это всегда данная вершина
        List<Arc> arcs = new List<Arc>();
        //минимальная вершина, которая предшествует этой
        string minPrevious = "";
        string maxPrevious = "";
        double E = 0;
        double L = 0;

        public Top()
        { }

        public Top(string name)
        {
            this.name = name;
        }

        public Top(string name, int number, Color color)
        {
            this.name = name;
            this.number = number;
            this.color = color;
        }

        public Top(string name, int number, Color color, int rang)
        {
            this.name = name;
            this.number = number;
            this.color = color;
            this.rang = rang;
        }

        public Top(string name, int number, double weight, Color color, int rang, int E, int L)
        {
            this.name = name;
            this.number = number;
            this.color = color;
            this.rang = rang;
            this.weight = weight;
            this.E = E;
            this.L = L;
        }

        public bool isEmptyTop()
        {
            if (this.getName().Equals(""))
                return true;
            return false;
        }

        public bool isEqual(Top t, bool withoutComparsionArcs /*true - сравнивает только атрибуты, false - сравнивает атрибуты и дуги*/)
        {
            if (t.getName().Equals(this.name) && t.getNumber() == this.number && t.getWeight() == this.weight)
                if (!withoutComparsionArcs)
                {
                    if (this.isArcsEqual(t))
                        return true;
                }
                else
                {
                    return true;
                }
                
            return false;
        }

        public bool isArcsEqual(Top t)
        {
            int count = 0;
            if (this.arcs.Count != t.arcs.Count)
                return false;
            else
            {
                for (int i = 0; i < this.arcs.Count; i++)
                    for (int j = 0; j < t.arcs.Count; j++)
                        if (this.arcs[i].isEqual(t.arcs[j]))
                            count++;
                if (count == this.arcs.Count)
                    return true;

            }
            return false;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setNumber(int number)
        {
            this.number = number;
        }

        public void setWeight(double weight)
        {
            this.weight = weight;
        }

        public void setColor(Color color)
        {
            this.color = color;
        }

        public void setRang(int rang)
        {
            this.rang = rang;
        }

        public void setArc(Arc a)
        {
            arcs.Add(a);
        }

        public void changeArc(int index, Top t)
        {
            if (arcs[index].getBegin().getName().Equals(t.getName()))
            {
                arcs[index].getBegin().setWeight(t.getWeight());
            }
            else if (arcs[index].getEnd().getName().Equals(t.getName()))
            {
                arcs[index].getEnd().setWeight(t.getWeight());
            }
        }

        public Arc getArc(Top end)
        {
            foreach (Arc a in this.getArcs())
            {
                if (a.getEnd().getName().Equals(end.getName()))
                    return a;
            }
            return null;
        }

        public void setArcs(List<Arc> a)
        {
            arcs.AddRange(a);
        }

        public void clearArcs()
        {
            arcs.Clear();
        }

        public string getName()
        {
            return name;
        }

        public int getNumber()
        {
            return number;
        }

        public double getWeight()
        {
            return weight;
        }

        public Color getColor()
        {
            return color;
        }

        public int getRang()
        {
            return rang;
        }

        public List<Arc> getArcs()
        {
            return arcs;
        }

        public string toString()
        {
            string s = "";
            for (int i = 0; i < this.getArcs().Count; i++)
                s += String.Format("{0}) Name: {1}, ", i + 1, this.getArcs()[i].getEnd().getName());
            return String.Format("Name: {0}; Number: {1}; Weight: {2}; Color: {3}; E: {4}; L: {5} Arcs to: {6}.",
                this.getName(), this.getNumber(), this.getWeight(), this.getColor(), this.getE(), this.getL(), s);
        }

        public string getMinPrevious()
        {
            return minPrevious;
        }

        public string getMaxPrevious()
        {
            return maxPrevious;
        }

        public void setMinPrevious(string minPrevious)
        {
            this.minPrevious = minPrevious;
        }

        public void setMaxPrevious(string maxPrevious)
        {
            this.maxPrevious = maxPrevious;
        }

        public void setE(double E)
        {
            this.E = E;
        }

        public void setL(double L)
        {
            this.L = L;
        }

        public double getE()
        {
            return E;
        }

        public double getL()
        {
            return L;
        }

        public Arc getArcWithEnd(Top t)
        {
            foreach (Arc a in arcs)
            {
                if (a.getEnd().getName().Equals(t.getName()))
                    return a;
            }
            return null;
        }

        
    }
}
