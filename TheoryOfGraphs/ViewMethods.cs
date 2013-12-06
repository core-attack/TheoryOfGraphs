using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace TheoryOfGraphs
{
    class ViewMethods
    {
        public string[] gamma_method;
        public string[] smart_gamma_method;
        public int[,] A;
        public int[,] B;
        public List<Arc> arcs = new List<Arc>();
        public Top[] graph;

        public string error = "";


        /* задание атрибутов вершин
         * Г(x1[name="", number=0, weight= 0, rang=0, color="Red", E=0, L=0]) = {x2[number=3, weight=1, color=Color.Red], x3, x4}
         */
        public void setGraph(string[] smart_gamma_method)
        {
            List<Top> tempGraph = new List<Top>();
            foreach (string top in smart_gamma_method)
            {
                char[] ch = { 'Г', '(', ')', '{', '}' };
                if (top.IndexOfAny(ch) != -1)
                {
                    string name = top.Split(')')[0].Split('(')[1].Split('[')[0];
                    int number = Convert.ToInt16(top.Split(')')[0].Split('(')[1].Split('[')[0].Substring(1));
                    double weight = 0;
                    int rang = 0;
                    Color color = Color.White;
                    int E = 0;
                    int L = 0;
                    //заполнить вершину значениями атрибутов
                    string[] atrs = new string[0];
                    if (top.IndexOf('[') != -1 && top.IndexOf(']') != -1 && top.IndexOf(',') != -1)
                        atrs = top.Split(')')[0].Split('(')[1].Split('[')[1].Split(']')[0].Split(',');
                    foreach (string atr in atrs)
                    {
                        if (atr.IndexOf("name") != -1)
                        { name = getStringValue(atr); }
                        else if (atr.IndexOf("number") != -1)
                        { number = getInt16Value(atr); }
                        else if (atr.IndexOf("weight") != -1)
                        { weight = getDoubleValue(atr); }
                        else if (atr.IndexOf("rang") != -1)
                        { rang = getInt16Value(atr); }
                        else if (atr.IndexOf("color") != -1)
                        { color = getColorValue(atr); }
                        else if (atr.IndexOf("E") != -1)
                        { E = getInt16Value(atr); }
                        else if (atr.IndexOf("L") != -1)
                        { L = getInt16Value(atr); }
                    }
                    Top t = new Top(name, number, weight, color, rang, E, L);

                    //заполнить вершину дугами
                    string[] arcs = top.Split('}')[0].Split('{')[1].Split(';');
                    foreach (string arc in arcs)
                    {
                        if (!arc.Equals(""))
                        {
                            int arcNumber = 0;
                            double arcWeight = 0;
                            Color arcColor = Color.White;
                            string endTopName = arc.Split('[')[0].Trim();
                            atrs = arc.Split('[')[1].Split(']')[0].Split(',');
                            foreach (string atr in atrs)
                            {
                                if (atr.IndexOf("number") != -1)
                                { arcNumber = getInt16Value(atr); }
                                else if (atr.IndexOf("weight") != -1)
                                { arcWeight = getDoubleValue(atr); }
                                else if (atr.IndexOf("color") != -1)
                                { arcColor = getColorValue(atr); }
                            }
                            Arc a = new Arc(t, new Top(endTopName), arcWeight, arcNumber, arcColor);
                            t.setArc(a);
                        }
                    }
                    tempGraph.Add(t);
                }
            }
            graph = tempGraph.ToArray();
            foreach (Top t1 in graph)
                foreach (Arc arc in t1.getArcs())
                {
                    Top temp = getTopWithName(arc.getEnd().getName());
                    if (temp != null)
                        arc.setEnd(temp);
                }
            setArcs();
        }
        string getStringValue(string s)
        {
            return s.Split('=')[1].Trim();
        }

        double getDoubleValue(string s)
        {
            try
            {
                return Convert.ToDouble(s.Split('=')[1].Trim());
            }
            catch (Exception e)
            {
                error += e.Message + "\n" + e.StackTrace;
                return 0;
            }
        }

        int getInt16Value(string s)
        {
            try
            {
                return Convert.ToInt16(s.Split('=')[1].Trim());
            }
            catch (Exception e)
            {
                error += e.Message + "\n" + e.StackTrace;
                return 0;
            }
        }

        Color getColorValue(string s)
        {
            try
            {
                return Color.FromName(s.Split('=')[1].Trim());
            }
            catch (Exception e)
            {
                error += e.Message + "\n" + e.StackTrace;
                return Color.White;
            }
        }

        //возвращает вершину, из которой выходит дуга
        public string getTopParent(string s)
        {
            if (s.IndexOf('(') != -1 && s.IndexOf(')') != -1)
                return s.Substring(s.IndexOf('(') + 1, s.IndexOf(')') - 1 - (s.IndexOf('(')));
            else
                return "";
        }

        //возвращает вершины, в которые выходит дуга
        public string[] getTopChilds(string s)
        {
            if (s.IndexOf('{') != -1 && s.IndexOf('}') != -1)
            {
                return s.Substring(s.IndexOf('{') + 1, s.Length - 2 - s.IndexOf('{')).Split(',').ToArray();
            }
            else
                return null;
        }

        void setAtributs(string child, /*out int number, */out int weight, out Color color)
        {
            weight = 0;
            color = Color.White;
            if (child.IndexOf('[') != -1 && child.IndexOf(']') != -1)
            {
                string atr = child.Split('[')[1].Split(']')[0];
                /*придумать новый способ ввода и вывода дуг
                 */
                string[] atrs = atr.Split(',');
                for (int i = 0; i < atrs.Length; i++)
                {
                    /*if (atrs[i].IndexOf("number") != -1)
                    {
                        number = Convert.ToInt16(atrs[i].Split('=')[1].Trim());
                    }
                    else*/ if (atrs[i].IndexOf("weight") != -1)
                    {
                        weight = Convert.ToInt16(atrs[i].Split('=')[1].Trim());
                    }
                    else if (atrs[i].IndexOf("color") != -1)
                    {
                        color = Color.FromName(atrs[i].Split('=')[1].Trim());
                    }
                }
                
            }
        }

        //задает гамма соотвествие
        /*
         * задание атрибутов дуг
         * Г(x1) = {x2[number=3, weight=1, color=Color.Red], x3, x4}
         * длина пути
         * Г(x1) = {x2[weight=1], x3, x4}
         */
        public void readG(string[] str_arr)
        {
            arcs.Clear();
            gamma_method = new string[str_arr.Length];
            smart_gamma_method = new string[str_arr.Length];
            int index = -1;
            foreach (string s in str_arr)
            {
                char[] ch = { 'Г', '(', ')', '{', '}' };
                if (s.IndexOfAny(ch) != -1)
                {
                    index++;
                    string parent = getTopParent(s).Trim();
                    gamma_method[index] = "Г(" + parent.Split('[')[0] + ") = {";
                    smart_gamma_method[index] = "Г(" + parent.Split('[')[0] + ") = {";

                    int number = Convert.ToInt16(parent.Split('[')[0].Substring(1));
                    string[] childs = getTopChilds(s);
                    for (int i = 0; i < childs.Length; i++)
                    {
                        
                        if (!childs[i].Equals(""))
                        {
                            Color color = new Color();
                            
                            int weight = 0;
                            setAtributs(childs[i], out weight, out color);
                            string end = childs[i].Trim();
                            if (end.IndexOf('[') != -1)
                                end = end.Split('[')[0].Trim();
                            Top b = new Top(getTopParent(s).Trim());
                            Top e = new Top(end);
                            Arc a = new Arc(b, e, weight, number, color);
                            arcs.Add(a);
                        }
                        if (i != childs.Length - 1)
                        {
                            gamma_method[index] += childs[i].Trim() + ", ";
                            if (!childs[i].Equals(""))
                            {
                                smart_gamma_method[index] += String.Format("{0}[weight={1}, number={2}, color={3}]", 
                                    arcs[arcs.Count - 1].getEnd().getName(), arcs[arcs.Count - 1].getWeight(), arcs[arcs.Count - 1].getNumber(), arcs[arcs.Count - 1].getColor()) + ", ";
                            }
                        }
                        else
                        {
                            gamma_method[index] += childs[i].Trim() + "}";
                            if (!childs[i].Equals(""))
                            {
                                smart_gamma_method[index] += String.Format("{0}[weight={1}, number={2}, color={3}]",
                                    arcs[arcs.Count - 1].getEnd().getName(), arcs[arcs.Count - 1].getWeight(), arcs[arcs.Count - 1].getNumber(), arcs[arcs.Count - 1].getColor());
                            }
                        }
                    }
                    smart_gamma_method[index] += "}"; 
                }
            }
        }

        //возвращает гамма соответствие
        string getG()
        {
            string s = "";
            foreach (string ss in gamma_method)
            {
                s += ss + "\n";
            }
            return s;
        }

        public string getSmartG()
        {
            string s = "";
            foreach (string ss in smart_gamma_method)
            {
                s += ss + "\n";
            }
            return s;
        }

        //задает матрицу А
        public void readA(string[] str_arr, int n)
        {
            arcs.Clear();
            bool firstline = true;
            A = new int[n, n];
            int i = 0;
            foreach (string s in str_arr)
            {
                //игнорируем первую строку
                if (!firstline)
                {
                    string[] elem = s.Split('\t');
                    //игнорируем первый столбец
                    for (int j = 1; j < elem.Length; j++)
                        A[i, j - 1] = Convert.ToInt16(elem[j].Trim());
                    i++;
                }
                else
                {
                    firstline = false;
                }
            }
            for (int p = 0; p < A.GetLength(0); p++)
                for (int q = 0; q < A.GetLength(0); q++)
                {
                    if (A[p, q] == 1)
                    {
                        Arc a = new Arc();
                        a.setBegin(new Top('x' + (p + 1).ToString()));
                        a.setEnd(new Top('x' + (q + 1).ToString()));
                        arcs.Add(a);
                    }
                }
        
        }

        //возвращает матрицу А в строчном представлении
        string getA()
        {
            string s = "";
            for (int i = -1; i < A.GetLength(0); i++)
            {
                if (i != -1)
                    s += (i + 1).ToString();
                for (int j = 0; j < A.GetLength(1); j++)
                    if (i == -1)
                    {
                        s += "\t" + (j + 1).ToString();
                    }
                    else
                    {
                        s += "\t" + A[i, j].ToString();
                    }
                s += "\n";
            }
            return s;
        }

        //задает матрицу B
        public void readB(string[] str_arr, int n, string text)
        {
            arcs.Clear();
            bool firstline = true;
            int m = str_arr[0].Split('\t').Length - 1;
            B = new int[n, m];
            int i = 0;
            foreach (string s in str_arr)
            {
                //игнорируем первую строку
                if (!firstline)
                {
                    if (s != "")
                    {
                        string[] elem = s.Split('\t');
                        //игнорируем первый столбец
                        for (int j = 1; j < elem.Length; j++)
                        {
                            B[i, j - 1] = Convert.ToInt16(elem[j].Trim());
                        }
                        i++;
                    }
                }
                else
                {
                    firstline = false;
                }
            }
            for (int q = 0; q < B.GetLength(1); q++)
            {
                Arc a = new Arc();
                for (int p = 0; p < B.GetLength(0); p++)
                {
                    if (B[p, q] == 1)
                    {
                        a.setBegin(new Top('x' + (p + 1).ToString()));
                    }
                    else if (B[p, q] == -1)
                    {
                        a.setEnd(new Top('x' + (p + 1).ToString()));

                    }
                }
                arcs.Add(a);
            }
        }

        //возвращает матрицу В в строчном представлении
        string getB()
        {
            string s = "";
            for (int i = -1; i < B.GetLength(0); i++)
            {
                if (i != -1)
                    s += (i + 1).ToString();
                for (int j = 0; j < B.GetLength(1); j++)
                    if (i == -1)
                    {
                        s += "\t" + (j + 1).ToString();
                    }
                    else
                    {
                        s += "\t" + B[i, j].ToString();
                        
                    }
                s += "\n";
            }
            return s;
        }

        public int[,] getArrA()
        {
            return A;
        }

        public int[,] getArrB(int n)
        {
            setB(n);
            return B;
        }

        public string view(int n, int index)
        {
            string result = "";
            if (index == 0)
            { 
                //матрица А
                result += "\nМатрица смежности:\n" + setA(n);
                //матрица В
                result += "Матрица инцидентности:\n" + setB(n);
            }
            else if (index == 1)
            {
                //гамма соответствие
                result += "\nГамма соответствие:\n" + setG(n);
                //матрица В
                result += "Матрица инцидентности:\n" + setB(n);
            }
            else
            {

                //гамма соответствие
                result += "\nГамма соответствие:\n" + setG(n);
                //матрица А
                result += "Матрица смежности:\n" + setA(n);
            }

            return result;
        }

        string setG(int n)
        {
            //Array.Clear(gamma_method, 0, gamma_method.Length);
            string s = "";
            gamma_method = new string[n];
            for (int i = 0; i < gamma_method.Length; i++)
                gamma_method[i] = String.Format("Г(x{0}) = ", i + 1) + "{";
            List<string> was = new List<string>();
            int index = 0;
            for (int i = 0; i < arcs.Count; i++)
            {
                //записываем вершину, чтобы не повторять поиск
                if (!was.Contains(arcs[i].getBegin().getName()))
                {
                    was.Add(arcs[i].getBegin().getName());
                    index = Convert.ToInt16(arcs[i].getBegin().getName().Trim().Substring(1, arcs[i].getBegin().getName().Length - 1)) - 1;
                    s = "";
                    for (int j = 0; j < arcs.Count; j++)
                    {
                        if (arcs[i].getBegin().getName().Equals(arcs[j].getBegin().getName()))
                        {
                            s += arcs[j].getEnd().getName() + ",";
                        }
                    }
                    //удаляем последнюю запятую
                    s = s.Remove(s.Length - 1, 1);
                    gamma_method[index] += s;
                    index++;
                }
            }
            for (int i = 0; i < gamma_method.Length; i++)
                gamma_method[i] += "}";
            return getG();
        }

        int getIndex(string var)
        {
            if (var.IndexOf('x') != 0)
            { 
                return Convert.ToInt16(var.Replace("x", ""));
            }
            else
                return -1;
        }

        string setA(int n)
        {
            A = new int[n, n];
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(1); j++)
                    A[i, j] = 0;
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(1); j++)
                    for (int k = 0; k < arcs.Count; k++)
                        if (("x" + (i + 1).ToString()).Equals(arcs[k].getBegin().getName()) && ("x" + (j + 1).ToString()).Equals(arcs[k].getEnd().getName()))
                            A[i, j] = 1;
            return getA();
        }

        string setB(int n)
        {
            int m = arcs.Count;
            B = new int[n, m];
            for (int i = 0; i < B.GetLength(0); i++)
                for (int j = 0; j < B.GetLength(1); j++)
                    B[i, j] = 0;
            for (int k = 0; k < arcs.Count; k++)
                for (int i = 0; i < B.GetLength(0); i++)
                {
                    if (("x" + (i + 1).ToString()).Equals(arcs[k].getBegin().getName()))
                    {
                        B[i, k] = 1;
                        B[Convert.ToInt16(arcs[k].getEnd().getName().Remove(0, 1)) - 1, k] = -1;
                        break;
                    }
                }
            return getB();
        }

        //заполняет арки, используя уже заданный граф
        void setArcs()
        {
            arcs.Clear();
            foreach (Top t in graph)
                foreach(Arc a in t.getArcs())
                    arcs.Add(new Arc(a.getBegin(), a.getEnd(), a.getWeight(), a.getNumber(), a.getColor()));
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


    }
}
