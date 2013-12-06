using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace TheoryOfGraphs
{
    class NetworkPlanning
    {
        Top[] graph;
        int[,] B;       
        double[] Rfull;//полный резерв
        double[] Rfree;//свободный резерв
        double[] Rinde;//независимый резерв
        public string error = "";
        ViewMethods vm;

        public void setGraph(ViewMethods vm, int n)
        {
            this.vm = vm;
            this.graph = vm.graph;
            B = vm.getArrB(n);
        }

        public string print()
        {
            string s = "";
            foreach (Top t in graph)
            {
                s += t.toString() + "\n";
            }
            return s;
        }

        //множество вершин, из которых можно попасть в заданную
        Top[] getGammaMinusOne(Top t)
        {
            List<Top> list = new List<Top>();
            for (int j = 0; j < B.GetLength(1); j++)
            {
                if (B[t.getNumber() - 1, j] == -1)
                    for (int k = 0; k < B.GetLength(0); k++)
                    {
                        if (B[k, j] == 1)
                        {
                            string stop = "x" + (k + 1);
                            list.Add(getTopWithName(stop));
                            break;
                        }
                    }
            }
            return list.ToArray();
        }

        Top getTopWithNumber(int index)
        {
            foreach (Top t in graph)
            {
                if (t.getNumber() == index)
                    return t;
            }
            return null;
        }

        Top getTopWithName(string name)
        {
            foreach (Top t in graph)
            {
                if (t.getName().Equals(name))
                    return t;
            }
            return null;
        }

        void setE(Top t)
        {
            Top[] all = getGammaMinusOne(t);
            double[] arr = new double[all.Length];
            int i = 0;
            foreach (Top top in all)
            {
                Arc arc = top.getArcWithEnd(t);
                arr[i] = top.getE() + arc.getWeight();
                i++;
            }
            if (arr.Length != 0)
                t.setE(arr.Max());
        }

        void setL(Top t)
        {
            double[] arr = new double[t.getArcs().Count];
            int i = 0;
            foreach (Arc a in t.getArcs())
            {
                arr[i] = getTopWithName(a.getEnd().getName()).getL() - a.getWeight();
                i++;
            }
            if (arr.Length != 0)
                t.setL(arr.Min());
        }

        string calc()
        {
            try
            {
                //считаем наиболее ранние и наиболее поздние сроки всех событий
                for (int i = 1; i < graph.Length + 1; i++)
                {
                    Top t = getTopWithNumber(i);
                    setE(t);
                    if (i == graph.Length)
                        t.setL(t.getE());
                }
                for (int i = graph.Length - 1; i > 0; i--)
                {
                    Top t = getTopWithNumber(i);
                    setL(t);
                }

                Rfull = new double[vm.arcs.Count];
                Rfree = new double[vm.arcs.Count];
                Rinde = new double[vm.arcs.Count];

                for (int i = 0; i < vm.arcs.Count; i++)
                {
                    //считаем полный резерв
                    Rfull[i] = getTopWithName(vm.arcs[i].getEnd().getName()).getL() - getTopWithName(vm.arcs[i].getBegin().getName()).getE() - vm.arcs[i].getWeight();
                    //считаем свободный резерв
                    Rfree[i] = getTopWithName(vm.arcs[i].getEnd().getName()).getE() - getTopWithName(vm.arcs[i].getBegin().getName()).getE() - vm.arcs[i].getWeight();
                    //считаем независимый резерв
                    Rinde[i] = getTopWithName(vm.arcs[i].getEnd().getName()).getE() - getTopWithName(vm.arcs[i].getBegin().getName()).getL() - vm.arcs[i].getWeight();
                    //if (Rinde[i] < 0)
                    //    Rinde[i] = 0;
                }
                List<Arc> kritWay = new List<Arc>();
                for (int i = 0; i < Rfull.Length; i++)
                    if (Rfull[i] == 0)
                    {
                        kritWay.Add(vm.arcs[i]);
                    }
                string result = "";
                for (int i = 0; i < kritWay.Count; i++)
                {
                    if (i != kritWay.Count - 1)
                        result += kritWay[i].getBegin().getName() + "->";
                    else if (i == kritWay.Count - 1)
                        result += kritWay[i].getEnd().getName() + ".";
                }
                return result;
            }
            catch (Exception e)
            {
                error += e.Message + "\n" + e.StackTrace;
                return error;
            }
        }

        public string writeTable()
        {
            string s = "\nКритический путь: " + calc();
            s += String.Format("\n№\t(x, y)\t\tt(x, y)\tRполн\tRсв\tRнез\n");
            for (int i = 0; i < vm.arcs.Count; i++)
            {
                if (vm.arcs[i].getBegin().getName().Length > 2 && vm.arcs[i].getEnd().getName().Length > 2)
                    s += String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\n", 
                        i + 1,
                        String.Format("({0}, {1})", vm.arcs[i].getBegin().getName(), vm.arcs[i].getEnd().getName()),
                        vm.arcs[i].getWeight(),
                        Rfull[i],
                        Rfree[i],
                        Rinde[i]);
                else
                    s += String.Format("{0}\t{1}\t\t{2}\t{3}\t{4}\t{5}\n", 
                        i + 1,
                        String.Format("({0}, {1})", vm.arcs[i].getBegin().getName(), vm.arcs[i].getEnd().getName()),
                        vm.arcs[i].getWeight(),
                        Rfull[i],
                        Rfree[i],
                        Rinde[i]);
            }

            return s;
        }

        //
        /***************************************************************************************************************************************/
        //

        //условие выхода из цикла
        List<double> oldGraph;
        //матрица расстояний
        double[,] wayMatrix;
        //посчитанные кратчайшие пути
        Dictionary<string, string> ways = new Dictionary<string, string>();
        //нахождение критических путей с помощью алгоритма Форда
        public string fordsMethod()
        {
            try
            {
                string result = "";
                wayMatrix = new double[graph.Length, graph.Length];
                oldGraph = new List<double>();
                for (int k = 0; k < graph.Length; k++)
                    oldGraph.Add(graph[k].getE());
                //заполнили все вершины нулевым значением
                setZeroE();
                //заполняем наиболее ранние сроки наступления событий
                int count = 0;
                while (count != 2)
                {
                    if (!anythingChanged())
                        count++;
                    findCriticalWay(getTopWithNumber(1));
                }
                //ищем критические пути
                List<Top> criticalWay = new List<Top>();
                Top top = graph[graph.Length - 1];
                criticalWay.Add(top);
                while (top.getNumber() != 1)
                {
                    top = getTopWithName(top.getMaxPrevious());
                    criticalWay.Add(top);
                }
                criticalWay.Reverse();

                for (int i = 0; i < criticalWay.Count; i++)
                {
                    if (i != criticalWay.Count - 1)
                    {
                        result += criticalWay[i].getName() + " (" + criticalWay[i].getE() + ") ->";
                    }
                    else
                    {
                        result += criticalWay[i].getName() + " (" + criticalWay[i].getE() + ")\n";
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                error += e.Message + "\n" + e.StackTrace;
                return "error";
            }
        }

        
        void setZeroE()
        {
            foreach (Top top in graph)
            {
                top.setE(0);
            }
        }

        bool anythingChanged()
        {
            bool changed = false;
            for (int i = 0; i < graph.Length; i++)
            {
                if (graph[i].getE() != oldGraph[i])
                {
                    oldGraph[i] = graph[i].getE();
                    changed = true;
                }
            }
            return changed;
        }

        bool anythingChanged(double[] V)
        {
            bool changed = false;
            for (int i = 0; i < V.Length; i++)
            {
                if (V[i] != oldGraph[i])
                {
                    oldGraph[i] = V[i];
                    changed = true;
                }
            }
            return changed;
        }


        void findCriticalWay(Top parent)
        {
            foreach (Arc a in parent.getArcs())
            {
                Top t = getTopWithName(a.getEnd().getName());
                double value = t.getE() - parent.getE();
                double weight = a.getWeight();
                if (value < weight)
                {
                    t.setE(parent.getE() + weight);
                    t.setMaxPrevious(parent.getName());
                }
                findCriticalWay(t);
            }
        }

        //
        /***************************************************************************************************************************************/
        //

        //нахождение критических путей с помощью алгоритма Беллмана
        public string bellmanMethod()
        {
            try
            {
                string result = "";
                wayMatrix = new double[graph.Length, graph.Length];
                oldGraph = new List<double>();
                
                //заполняем времена всех операций
                int n = graph.Length - 1;
                double[,] t = new double[graph.Length, graph.Length];
                for (int i = 0; i < graph.Length; i++ )
                    for (int j = 0; j < graph.Length; j++)
                    {
                        Arc a = graph[i].getArc(graph[j]);
                        if (i == j)
                            t[i, j] = 0;
                        else if (a != null)
                        {
                            t[i, j] = a.getWeight();
                        }
                        else 
                        {
                            t[i, j] = Int16.MinValue;
                        }
                    }
                //наиболее поздний срок события, V[i] - длина максимального пути от вершины x[i] до x[n]
                double[] V = new double[graph.Length];
                for (int i = 0; i < graph.Length; i++)
                {
                    if (i == n)
                        V[n] = 0;
                    else
                        V[i] = t[i, n];
                }
                for (int k = 0; k < graph.Length; k++)
                    oldGraph.Add(V[k]);
                int count = 0;
                int countIterations = 0;
                while (count != 2)
                {
                    countIterations++;
                    if (!anythingChanged(V))
                        count++;
                    for (int i = graph.Length - 1; i >= 0; i--)
                    {
                        if (i == n)
                            V[n] = 0;
                        else
                        {
                            double temp = getMaxBellmanCalc(V, t, i, n);
                            if (V[i] < temp)
                                V[i] = temp;
                        }
                    }
                    //result += println(V, "V");
                }
                foreach (Top top in graph)
                {
                    result += top.getName();
                    Top child = getTopWithName(top.getMaxPrevious());
                    while (child != null && !child.getMaxPrevious().Equals(""))
                    {
                        result += "->" + child.getName();
                        child = getTopWithName(child.getMaxPrevious());

                    }
                    if (child != null && child.getMaxPrevious().Equals(""))
                        result += "->" + child.getName();
                    result += "\n";
                }
                
                result += "\nДлина максимального пути\n";
                for (int i = 0; i < V.Length; i++ )
                    result += String.Format("от x{0} до x{1} = {2}\n", i + 1, V.Length, V[i]);
                
                return result;
            }
            catch (Exception e)
            {
                error += e.Message + "\n" + e.StackTrace;
                return "error";
            }
        }

        double getMaxBellmanCalc(double[] V, double[,] t, int i, int n)
        {
            double[] temp = new double[n + 1];
            for (int j = 0; j < temp.Length; j++)
            {
                temp[j] = V[j] + t[i, j];
            }
            double max = temp.Max();
            int index = temp.ToList().IndexOf(max);
            if (index != -1)
                for (int j = 0; j < temp.Length; j++)
                    if (i != j)
                        if (t[i, j] == max - V[j])
                            graph[i].setMaxPrevious(graph[j].getName());
            return max;
        }

        string println(double[] d, string name)
        {
            string result = name + "\n";
            for (int i = 0; i < d.Length; i++)
                result += String.Format("{0}[{1}] = {2}", name, i, d[i]) + "\n";
            return result;
        }
    }
}
