using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TheoryOfGraphs
{
    class ColoringOfGraphs
    {
        //гамма-соответствие: строка - вершина, массив строк - вершины, в которые можно попасть из данной
        //Dictionary<Top, Top[]> graph = new Dictionary<Top, Top[]>();
        List<Top> graph = new List<Top>();
        //массив дуг
        Arc[] arcs;
        double OMEGA = Int16.MaxValue;
        int[,] B;
        public string error = "";
        public void setGraph(ViewMethods vm, int n)
        {
            arcs = new Arc[vm.arcs.Count];
            int i = 0;
            B = vm.getArrB(n);
            foreach (Arc a in vm.arcs)
            {
                arcs[i] = a;
                i++;
            }
            graph.Clear();
            for (int j = 0; j < arcs.Length; j++)
            {
                if (!myContains(arcs[j].getBegin(), graph.ToArray(), true))
                {
                    graph.Add(arcs[j].getBegin());
                    foreach (Arc a in arcs)
                        if (graph[graph.Count - 1].isEqual(a.getBegin(), true))
                            if (!myContains(graph[graph.Count - 1].getArcs().ToArray(), a, true))
                                graph[graph.Count - 1].setArc(new Arc(a.getBegin(), a.getEnd(), a.getWeight(), a.getNumber(), a.getColor()));
                }

            }
            if (n != graph.Count)
            {
                string[] arr = vm.smart_gamma_method;
                foreach (string s in arr)
                {
                    if (s != null)
                        if (s.IndexOf("{}") != -1)
                        {
                            Top top = new Top(vm.getTopParent(s), Convert.ToInt16(vm.getTopParent(s).Substring(1)), Color.White);
                            graph.Add(top);
                        }
                }
            }
            for (int k = 0; k < graph.Count; k++)
                graph[k].setNumber(k + 1);
        }

        bool myContains(Arc[] arcs, Arc a, bool withoutComparsionArcs)
        {
            foreach (Arc ar in arcs)
                if (ar.isEqual(a))
                    return true;
            return false;
        }

        bool myContains(Top top, Top[] tops, bool withoutComparsionArcs)
        {
            foreach (Top t in tops)
                if (t.isEqual(top, withoutComparsionArcs))
                    return true;
            return false;
        }

        Top[] getChilds(Top t)
        {
            List<Top> list = new List<Top>();
            if (graph.IndexOf(t) != -1)
            {
                foreach (Arc a in graph[graph.IndexOf(t)].getArcs())
                    foreach(Top top in graph)
                        if (top.getName().Equals(a.getEnd().getName()))
                            list.Add(top);
            }
            return list.ToArray();
        }

        //множество вершин, из которых можно попасть в заданную
        Top[] getGammaMinusOne(Top t)
        {
            List<Top> list = new List<Top>();
            int index = Convert.ToInt16(t.getName().Substring(1)) - 1;
            for (int j = 0; j < B.GetLength(1); j++)
            {
                if (B[index, j] == -1)
                    for (int k = 0; k < B.GetLength(0); k++)
                    {
                        if (B[k, j] == 1)
                        {
                            string stop = "x" + (k + 1);
                            foreach (Top top in graph)
                            {
                                if (top.getName().Equals(stop))
                                    list.Add(top);
                            }
                        }
                    }
            }
            return list.ToArray();
        }

        Top getMinParentTop(Top child)
        {
            double value = Int16.MaxValue;
            double currentValue = 0;
            Top minTop = null;
            Top[] parents = getGammaMinusOne(child);
            List<double> values = new List<double>();
            List<Top> valuesTOP = new List<Top>();
            foreach (Top top in parents)
            {
                currentValue = top.getWeight() + top.getArc(child).getWeight();
                values.Add(currentValue);
                valuesTOP.Add(top);
            }
            if (values.Count != 0)
            {
                value = values.Min();
                int index = values.IndexOf(value);
                minTop = valuesTOP[index];
                double ras = value - OMEGA;
                if (child.getWeight() > value || child.getWeight() >= OMEGA && child.getWeight() < value)
                    setGraphsTopWeight(child, value);
                return minTop;
            }
            return null;
        }

        Top getMaxParentTop(Top child)
        {
            double value = 0;
            double currentValue = 0;
            Top maxTop = null;
            Top[] parents = getGammaMinusOne(child);
            List<double> values = new List<double>();
            List<Top> valuesTOP = new List<Top>();
            foreach (Top top in parents)
            {
                currentValue = top.getWeight() + top.getArc(child).getWeight();
                values.Add(currentValue);
                valuesTOP.Add(top);
            }
            value = values.Max();
            int index = values.IndexOf(value);
            maxTop = valuesTOP[index];
            double ras = value - OMEGA;
            if (child.getWeight() > value || child.getWeight() >= OMEGA && child.getWeight() < value)
                setGraphsTopWeight(child, value);
            return maxTop;
        }

        void setGraphsTopWeight(Top t, double weight)
        {
            for (int i = 0; i < graph.Count; i++)
            {
                if (graph[i].isEqual(t, true))
                {
                    graph[i].setWeight(weight);
                    return;
                }
            }
        }

        void setDefaultWeight(Top t)
        {
            foreach (Top top in graph)
            {
                if (t.isEqual(top, false))
                {
                    top.setWeight(0);
                }
                else
                {
                    top.setWeight(OMEGA);
                }
            }
        }

        void setZeroWeight()
        {
            foreach (Top top in graph)
            {
                top.setWeight(0);
            }
        }

        
        //условие выхода из цикла
        List<double> oldGraph;
        //матрица расстояний
        double[,] wayMatrix;
        //посчитанные кратчайшие пути
        Dictionary<string, string> ways = new Dictionary<string, string>();
        public string searchingMinimalWay()
        {
            try
            {
                string result = "";
                wayMatrix = new double[graph.Count, graph.Count];
                oldGraph = new List<double>();
                for (int k = 0; k < graph.Count; k++)
                    oldGraph.Add(graph[k].getWeight());
                int i = 0;
                foreach(Top top in graph)
                {
                    //заполнили все, кроме указанной, вершины максимальным значением
                    setDefaultWeight(top);
                    count = 0;
                    while (count != 2)
                    {
                        if (!anythingChanged())
                            count++;
                        countSet = 0;
                        findMinWay(top);
                    }
                    listMinWay.Clear();
                    //top - начальная, t - конечная 
                    foreach (Top t in graph)
                    {
                        if (!t.getName().Equals(top.getName()))
                        {
                            Top previous = find(t.getMinPrevious());
                            listMinWay.Add(t);
                            if (previous != null && !top.getMinPrevious().Equals(""))
                            {
                                while (!previous.getName().Equals(top.getName()))
                                {
                                    listMinWay.Add(previous);
                                    previous = find(previous.getMinPrevious());
                                }
                                listMinWay.Add(previous);
                                listMinWay.Reverse();
                                ways.Add(top.getName() + "-" + t.getName(), listToString(listMinWay));
                                listMinWay.Clear();
                            }
                        }
                    }
                    for (int j = 0; j < graph.Count; j++)
                    {
                        wayMatrix[i, j] = graph[j].getWeight();
                    }
                    i++;
                }
                foreach (string s in ways.Keys)
                    result += s + ": " + ways[s];
                return result;
            }
            catch (Exception e)
            {
                error += e.Message + "\n" + e.StackTrace;
                return "error";
            }
        }

        string listToString(List<Top> l)
        {
           string result = "";
           for (int i = 0; i < l.Count; i++)
                if (i != l.Count - 1)
                    result += l[i].getName() + "->";
                else
                    result += l[i].getName() + "\n";
            return result;
        }
        //ищет первое вхождение вершины с таким именем в graph
        Top find(string name)
        {
            foreach (Top t in graph)
            {
                if (t.getName().Equals(name))
                    return t;
            }
            return null;
        }

        List<Top> listMinWay = new List<Top>();
        //договоримся о том, что в весах вершины будем содержать путь к ней из данной вершины
        int countSet = 0;
        //ищет все минимальные пути до всех вершин
        public void findMinWay(Top top)
        {
            countSet++;
            Top t = getMinParentTop(top);
            //отмечаем вершину, в которой достигается минимум
            if (t != null)
            {
                setMinPrevious(top, t);
                if (top.getNumber() < graph.Count)
                {
                    findMinWay(graph[top.getNumber()]);//раз у нас номер на 1 больше, чем индекс массива вершин, то ничего инкрементировать не нужно
                }
                else if (countSet < graph.Count)
                {
                    findMinWay(graph[0]);
                }
            }
            else if (countSet < graph.Count)
            {
                findMinWay(graph[0]);
            }

        }

        List<Top> listMaxWay = new List<Top>();
        //ищет все максимальные пути до всех вершин
        public void findMaxWay(Top top)
        {
            countSet++;
            Top t = getMaxParentTop(top);
            //отмечаем вершину, в которой достигается минимум
            setMaxPrevious(top, t);
            if (t != null)
            {
                if (top.getNumber() < graph.Count)
                {
                    findMaxWay(graph[top.getNumber()]);//раз у нас номер на 1 больше, чем индекс массива вершин, то ничего инкрементировать не нужно
                }
                else if (countSet < graph.Count)
                {
                    findMaxWay(graph[0]);
                }
            }
        }

        void setMinPrevious(Top top, Top parent)
        {
            graph[graph.IndexOf(top)].setMinPrevious(parent.getName());
        }

        void setMaxPrevious(Top top, Top parent)
        {
            graph[graph.IndexOf(top)].setMaxPrevious(parent.getName());
        }

        public string writeMatrix()
        {
            string result = "";
            for (int i = -1; i < wayMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < wayMatrix.GetLength(1); j++)
                {
                    if (i == -1)
                    {
                        result += "\tx" + (j + 1);
                    }
                    else
                    {
                        if (j == 0)
                            result += "x" + (i + 1) + "\t" + wayMatrix[i, j];
                        else
                            result += "\t" + wayMatrix[i, j];
                    }
                }
                result += "\n";
            }

            return result;
        }

        int count = 0;
        bool anythingChanged()
        {
            bool changed = false;
            for (int i = 0; i < graph.Count; i++)
            {
                if (graph[i].getWeight() != oldGraph[i])
                {
                    oldGraph[i] = graph[i].getWeight();
                    if (oldGraph[i] < OMEGA)
                    {
                        changed = true;
                        count = 0;
                    }
                }
            }
            return changed;
        }

        public string getWay(string text)
        {
            string result = "";
            string[] tops = text.Split('-');
            if (tops.Length == 2)
            {
                Top begin = new Top();
                Top end = new Top();
                foreach (Top top in graph)
                    if (top.getName().Equals(tops[0]))
                        begin = top;
                    else if (top.getName().Equals(tops[1]))
                        end = top;
                return "Кратчайшее растояние от вершины " + begin.getName() + " до вершины " + end.getName() + " : " + ways[begin.getName() + "-" + end.getName()] + "\n";
            }
            else
            {
                error += "Неверный формат ввода пути";
                return "";
            }
        }
        //
        //
        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> нумерация вершин ниже
        //
        //
        public string getNumerationGraph(int n)
        {
            string result = "";
            int[] rangs = new int[n];
            //найти начальную (дуги только выходят)
            Top beginTop = new Top();
            foreach (Top top in graph)
            {
                if (isBeginTop(top))
                {
                    beginTop = top;
                    top.setRang(0);
                }
            }
            //считаем максимальное количество дуг до начальной (ранг)
            setRang(beginTop);
            foreach (Top top in graph)
                result += top.getName() + " rang = " + top.getRang() + "\n";
            numerateTops();
            foreach (Top top in graph)
                result += top.getName() + " new number = " + top.getNumber() + "\n";
            return result;
        }

        //нумеруем вершины
        void numerateTops()
        {
            int number = 1;
            List<List<Top>> all = getLevels();
            for (int i = 0; i < all.Count; i++)
                for (int j = 0; j < all[i].Count; j++)
                {
                    all[i][j].setNumber(number);
                    number++;
                }
        }


        List<List<Top>> getLevels()
        {
            //на всех уровнях
            List<List<Top>> allLevels = new List<List<Top>>();
            int maxLevel = getMaxLevel();
            //на одном уровне
            List<Top> lvlTop;
            
            for (int level = 0; level <= maxLevel; level++)
            {
                lvlTop = new List<Top>();
                foreach (Top top in graph)
                {
                    if (level == top.getRang())
                        lvlTop.Add(top);
                }
                allLevels.Add(lvlTop);
            }
            return allLevels;
        }

        int getMaxLevel()
        {
            int max = 0;
            foreach (Top top in graph)
            {
                if (top.getRang() > max)
                    max = top.getRang();
            }
            return max;
        }

        //вычисляет ранги вершин
        void setRang(Top BeginTop)
        {
            Top[] childs = getChilds(BeginTop);
            foreach (Top top in childs)
            {
                if (graph.IndexOf(top) != -1)
                {
                    if (graph[graph.IndexOf(top)].getRang() < BeginTop.getRang() + 1)
                        graph[graph.IndexOf(top)].setRang(BeginTop.getRang() + 1);
                    setRang(top);
                }
            }
        }

        //определяет, является ли вершина начальной
        bool isBeginTop(Top top)
        {
            bool result = true;
            foreach (Top t in graph)
            {
                if (!top.isEqual(t, true))
                    foreach (Arc arc in t.getArcs())
                    {
                        if (arc.getEnd().getName().Equals(top.getName()))
                            return false;
                        
                    
                    }
            }
            return result;
        }

        
    }
}
