using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheoryOfGraphs
{
    class StrongComponents
    {
        //гамма-соответствие: строка - вершина, массив строк - вершины, в которые можно попасть из данной
        Dictionary<string, string[]> graph = new Dictionary<string, string[]>();
        //матрица инцидентности
        int[,] B = new int[0, 0];
        public string error = "";

        //задает значения графа
        public void setGraph(string[] gamma_method, int[,] B, int n)
        {
            arrR = new List<string>[n];
            arrQ = new List<string>[n];
            for (int i = 0; i < n; i++)
            {
                arrR[i] = new List<string>();
                arrQ[i] = new List<string>();
            }
            strongComponents = new List<string[]>();
            graph.Clear();
            ViewMethods vm = new ViewMethods();
            foreach (string s in gamma_method)
            {
                graph.Add(vm.getTopParent(s), vm.getTopChilds(s));
            }
            //убираем лишние пробелы
            foreach (string key in graph.Keys)
                for (int i = 0; i < graph[key].Length; i++)
                    graph[key][i] = graph[key][i].Trim();
            this.B = new int[B.GetLength(0), B.GetLength(1)];
            for (int i = 0; i < B.GetLength(0); i++)
                for (int j = 0; j < B.GetLength(1); j++)
                    this.B[i, j] = B[i, j];


        }
        //композиция гамма (множество вершин, в которые можно попасть из заданной)
        List<string> composition = new List<string>();
        //контркомпозиция гамма (множество вершин, из которых можно попасть в заданную)
        List<string> contrComposition = new List<string>();
        List<string> getAllComposition(string x)
        {
            if (!composition.Contains(x))
                composition.Add(x);
            List<string> temp = new List<string>();
            foreach (string top in graph[x])
            {
                if (!composition.Contains(top))
                {
                    composition.Add(top);
                    temp = getAllComposition(top);
                    if (temp.Count != 0)
                        composition.AddRange(temp);
                }
            }
            return temp;
        }
        //на вход подается индекс вершины
        List<string> getAllContrComposition(int x)
        {
            List<string> temp = new List<string>();
            if (!contrComposition.Contains("x" + x))
                contrComposition.Add("x" + x);
            for (int j = 0; j < B.GetLength(1); j++)
            {
                if (B[x - 1, j] == -1)
                {
                    for (int k = 0; k < B.GetLength(0); k++)
                    {
                        if (B[k, j] == 1)
                        {
                            string top = "x" + (k + 1);
                            if (!contrComposition.Contains(top))
                            {
                                contrComposition.Add(top);
                                temp = getAllContrComposition(k + 1);
                                if (temp.Count != 0)
                                    contrComposition.AddRange(temp);
                            }
                        }
                    }
                }
            }
            return temp;
        }
        public void clearCompositionList()
        {
            composition.Clear();
        }
        public void clearContrCompositionList()
        {
            contrComposition.Clear();
        }

        List<string>[] arrR;
        //множество достижимостей для вершины х
        string[] R()
        {
            try
            {
                string[] result = new string[graph.Keys.Count];
                int k = 0;
                foreach (string top in graph.Keys)
                {
                    clearCompositionList();
                    getAllComposition(top).ToArray();
                    result[k] += "R(" + top + ") = {";
                    for (int i = 0; i < composition.Count; i++)
                    {
                        if (!arrR[Convert.ToInt16(top.Replace("x", "")) - 1].Contains(composition[i]))
                            arrR[Convert.ToInt16(top.Replace("x", "")) - 1].Add(composition[i]);
                        if (i != composition.Count - 1)
                            result[k] += composition[i] + ", ";
                        else
                            result[k] += composition[i];
                    }
                    result[k] += "}\n";
                    k++;
                }
                return result; 
            }
            catch (Exception e)
            {
                error += e.Message + "\n" + e.StackTrace;
                return null;
            }
        }

        //матрица достижимостей
        int[,] matrixR;
        //матрица контрдостижимостей
        int[,] matrixQ;
        void setmatrix(int n, out int[,] matrix, List<string>[] list)
        {
            matrix = new int[n, n];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i < list.Length)
                    {
                        if (list[i].Contains("x" + (j + 1)))
                            matrix[i, j] = 1;
                    }
                    else
                        matrix[i, j] = 0;
                }
            }
        }

        public string writeMatrix(int n, int[,] matrix)
        {
            
            string result = "";
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result += matrix[i, j].ToString() + "\t";
                }
                result += "\n";
            }
            return result;
        }

        public string writeMatrixR(int n)
        {
            writeR();
            setmatrix(n, out matrixR, arrR);
            return writeMatrix(n, matrixR);
        }

        public string writeMatrixQ(int n)
        {
            writeQ();
            setmatrix(n, out matrixQ, arrQ);
            return writeMatrix(n, matrixQ);
        }

        List<string>[] arrQ;
        //множество контрадостижимостей (будем использовать матрицу инцидентности)
        string[] Q()
        {
            try
            {
                string[] result = new string[B.GetLength(0)];
                ViewMethods vm = new ViewMethods();

                for (int i = 0; i < B.GetLength(0); i++)
                {
                    clearContrCompositionList();
                    getAllContrComposition(i + 1);
                    //contrComposition.Sort();
                    result[i] += "Q(x" + (i + 1) + ") = {";
                    for (int k = 0; k < contrComposition.Count; k++)
                    {
                        if (!arrQ[Convert.ToInt16(("x" + (i + 1)).Replace("x", "")) - 1].Contains(contrComposition[k]))
                            arrQ[Convert.ToInt16(("x" + (i + 1)).Replace("x", "")) - 1].Add(contrComposition[k]);
                        if (k != contrComposition.Count - 1)
                            result[i] += contrComposition[k] + ", ";
                        else
                            result[i] += contrComposition[k];
                    }
                    result[i] += "}\n";
                }
                return result;
            }
            catch (Exception e)
            {
                error += e.Message + "\n" + e.StackTrace;
                return null;
            }
        }


        public string writeR()
        {
            string result = "";
            string[] arr = R();
            foreach (string comp in arr)
            {
                result += comp;
            }
            return result;
        }

        public string writeQ()
        {
            string result = "";
            string[] arr = Q();
            foreach (string contComp in arr)
            {
                result += contComp;
            }
            return result;
        }

        string[] getIntersection(string[] R, string[] Q) 
        {
            List<string> result = new List<string>();
            
            for (int i = 0; i < R.Length; i++)
                for (int j = 0; j < Q.Length; j++)
                {
                    if (R[i].Equals(Q[j]))
                        if (!result.Contains(R[i]))
                            result.Add(R[i]);
                }
            result.Sort();
            return result.ToArray();
        }

        //поэлементное умножение матриц
        int[,] multMatrix;
        void multipleMatrix(int[,] a, int[,] b)
        {
            if (a.GetLength(0) == b.GetLength(0) && a.GetLength(1) == b.GetLength(1))
            {
                multMatrix = new int[a.GetLength(0), a.GetLength(1)];
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        if (a[i, j] == b[i, j] && a[i, j] == 1)
                            multMatrix[i, j] = 1;
                        else
                            multMatrix[i, j] = 0;
                    } 
                }
            }
        }

        public string writeMultMatrix()
        {
            multipleMatrix(matrixR, matrixQ);
            return writeMatrix(multMatrix.GetLength(0), multMatrix);
        }

        bool isGraphCiclic(int[,] a)
        {
            int k = 0;
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (i == j && a[i, j] == 1)
                        k++;
                    else if (a[i, j] == 1)
                        return false;
                }
            }
            if (k == a.GetLength(0))
                return true;
            return false;
        }

        public string ciclicGraph()
        {
            if (isGraphCiclic(multMatrix))
                return "граф циклический";
            else
                return "граф ациклический";
        }

        //сильные компоненты для матричного метода поиска
        void getStrongComponentsMatrix()
        {
            strongComponents.Clear();
            int doesNotFit = 0;
            List<string> strong = new List<string>();
            for (int i = 0; i < multMatrix.GetLength(0); i++)
            {
                doesNotFit = 0;
                for (int j = 0; j < multMatrix.GetLength(0); j++)
                {
                    if (i != j)
                        if (lineEquals(getLine(multMatrix, i), getLine(multMatrix, j)))
                        {
                            if (!strong.Contains("x" + (i + 1)))
                                strong.Add("x" + (i + 1));
                            if (!strong.Contains("x" + (j + 1)))
                                strong.Add("x" + (j + 1));
                        }
                        else
                            doesNotFit++;
                }
                if (doesNotFit == multMatrix.GetLength(0) - 1)
                {
                    strong.Add("x" + (i + 1));
                    strongComponents.Add(strong.ToArray());
                }
                strong.Sort();
                if (!myListContains(strongComponents, strong.ToArray()))
                    strongComponents.Add(strong.ToArray());
                strong.Clear();
            }
        }

        public string writeStrongComponents()
        {
            string result = "";
            getStrongComponentsMatrix();
            for (int index = 0; index < strongComponents.Count; index++)
            {
                result += "CK" + (index + 1) + " = {";
                for (int j = 0; j < strongComponents[index].Length; j++)
                {
                    if (j != strongComponents[index].Length - 1)
                        result += strongComponents[index][j] + ", ";
                    else
                        result += strongComponents[index][j];
                }
                result += "}\n";
            }
            return result;
        }

        int[] getLine(int[,] a, int k)
        {
            int[] line = new int[a.GetLength(1)];
            for (int j = 0; j < a.GetLength(1); j++)
                line[j] = a[k, j];
            return line;
                    
        }

        //сравнивает массивы на идентичность
        bool lineEquals(int[] a, int[] b)
        {
            int k = 0;
            if (a.Length == b.Length)
            {
                for (int i = 0; i < a.Length; i++)
                    if (a[i] == b[i])
                        k++;
                if (k == a.Length)
                    return true;
            }
            return false;
        }

        List<string[]> strongComponents;
        public string getStrongComponents()
        {
            string result = ""; 
            string[] array = new string[0];
            int index = 0;
            for (int i = 0; i < arrR.Length; i++)
            {
                string[] temp = getIntersection(arrR[i].ToArray(), arrQ[i].ToArray());
                if (!myListContains(strongComponents, temp))
                {
                    if (temp.Length != 0)
                    {
                        strongComponents.Add(temp);
                        result += "CK" + (index + 1) + " = {";
                        for (int j = 0; j < strongComponents[index].Length; j++)
                        {
                            if (j != strongComponents[index].Length - 1)
                                result += strongComponents[index][j] + ", ";
                            else
                                result += strongComponents[index][j];
                        }
                        result += "}\n";
                        index++;
                    }
                }
            }
            return result;
        }

        bool myListContains(List<string[]> arr, string[] a)
        {
            bool ok = false;
            if (arr != null)
                for (int i = 0; i < arr.Count; i++)
                {
                    if (myContains(arr[i], a))
                        ok = true;
                }
            return ok;
        }

        bool myContains(string[] a, string[] b)
        {
            bool ok = false;
            int count = 0;
            
            if (a.Length == b.Length)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i].Equals(b[i]))
                        count++;
                }
                if (count == a.Length)
                    ok = true;
            }
            return ok;
        }

        public string getMatrixR(int n)
        {
            string result = "";
            List<string> axis = new List<string>();
            //записаны ли вершины на осях матрицы
            bool[] tops = new bool[n];

            int[,] arr = new int[n, n];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = 0;
                }
                tops[i] = false;
            }
            for (int i = 0; i < strongComponents.Count; i++)
            {
                int index = getFirstFalseIndex(tops);
                string top = "x" + (index + 1);
                if (strongComponents[i].Contains(top))
                {
                    int sum = axis.Count;
                    for (int j = 0; j < strongComponents[i].Length; j++)
                    {
                        for (int k = 0; k < strongComponents[i].Length; k++)
                            arr[sum + j, sum + k] = 1;
                        axis.Add(strongComponents[i][j]);
                        tops[Convert.ToInt16(strongComponents[i][j].Substring(1)) - 1] = true;
                    }
                }
                else
                {
                    axis.Add(top);
                    tops[index] = true;
                    i--;
                }
            }

            for (int i = -1; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (i == -1)
                        result += "\t" + axis[j];
                    else if (j == 0)
                        result += axis[i] + "\t" + arr[i, j]  + "\t";
                    else
                        result += arr[i, j] + "\t";
                }
                result += "\n";
            }

            return result;
        }

        int getFirstFalseIndex(bool[] b)
        {
            for (int i = 0; i < b.Length; i++)
                if (b[i] == false)
                    return i;
            return -1;
        }

        List<string>[] condensationGraph;
        public string getCondensationGraph()
        {
            try
            {
                string result = "";
                List<string> results = new List<string>();
                string[] sep = { " is achieved " };
                string temp = "";
                for (int i = 0; i < strongComponents.Count; i++)
                {
                    for (int j = 0; j < strongComponents.Count; j++)
                    {
                        if (i != j)
                            for (int k = 0; k < strongComponents[j].Length; k++)
                                if (isContains(strongComponents[i], strongComponents[j][k]))
                                {
                                    temp = (j + 1) + sep[0] + (i + 1);
                                    if (!results.Contains(temp))
                                        results.Add(temp);
                                }
                    }
                }
                string[] gamma = new string[strongComponents.Count];
                condensationGraph = new List<string>[strongComponents.Count];
                for (int i = 0; i < condensationGraph.Length; i++)
                    condensationGraph[i] = new List<string>();
                int index1 = 0;
                int index2 = 0;
                for (int i = 0; i < results.Count; i++)
                {
                    index1 = Convert.ToInt16(results[i].Split(sep, StringSplitOptions.None)[1]);
                    index2 = Convert.ToInt16(results[i].Split(sep, StringSplitOptions.None)[0]);
                    gamma[index1 - 1] += "CK" + index2 + ", ";
                    condensationGraph[index1 - 1].Add("CK" + index2);
                }
                for (int i = 0; i < gamma.Length; i++)
                    gamma[i] = "CK" + (i + 1) + " = {" + gamma[i] + "}";
                foreach (string s in gamma)
                    result += s + "\n";
                return result;
            }
            catch (Exception e)
            {
                error += e.Message + "\n" + e.StackTrace;
                return null;
            }
        }
        //достигается ли вершина из любой вершины входного массива
        bool isContains(string[] arr, string t)
        {
            for (int i = 0; i < arr.Length; i++)
                if (isAchieved(arr[i], t))
                    return true;
            return false;
        }

        //достигается ли вторая вершина из первой
        bool isAchieved(string t1, string t2)
        {
            int index = Convert.ToInt16(t1.Substring(1));
            if (arrR[index - 1].Contains(t2))
                return true;
            return false;
        }

        List<string> Base;
        List<string[]> BasesCurrentGraph;
        //ищет базу ациклического графа и все базы исходного графа (наименьшее множество сильных компонент, из которых достигаются все вершины графа)
        public string getBase()
        {
            string result = "";
            Base = new List<string>();
            BasesCurrentGraph = new List<string[]>();
            List<string> strongCompQ = new List<string>();
            foreach (List<string> l in condensationGraph)
                foreach (string s in l)
                    if (!strongCompQ.Contains(s))
                        strongCompQ.Add(s);
            for (int i = 0; i < condensationGraph.Length; i++)
                if (!strongCompQ.Contains("CK" + (i + 1)))
                {
                    Base.Add("CK" + (i + 1));
                    BasesCurrentGraph.Add(strongComponents[i]);
                }
            result += "B* = {";
            for (int i = 0; i < Base.Count; i++)
                if (i != Base.Count - 1)
                    result += Base[i] + ",";
                else
                    result += Base[i];
            result += "}\n";
            

            return result;
        }

        public string getAllBases()
        {
            int index = 0;
            string result = "";
            //найти максимальный по длине и в цикле по его елементам делать
            int max = getMaxBasesCurrentGraphLength();
            while (index < max)
            {
                index++;
                result += "B" + index + " = {";
                result += getAllBases(0, index - 1);
                result = result.Substring(0, result.Length - 2);
                result += "}\n";
            }
            return result;
        }

        int getMaxBasesCurrentGraphLength()
        {
            int max = 0;
            for (int i = 0; i < BasesCurrentGraph.Count; i++)
                if (BasesCurrentGraph[i].Length > max)
                    max = BasesCurrentGraph[i].Length;
            return max;
        }

        //возвращает всевозможные базы текущего графа (на вход счетчик и индекс массива)
        string getAllBases(int arrayIndex, int elementIndex)
        {
            if (arrayIndex < BasesCurrentGraph.Count)
            {
                string result = "";
                if (elementIndex < BasesCurrentGraph[arrayIndex].Length)
                {
                    result += BasesCurrentGraph[arrayIndex][elementIndex] + ", " + getAllBases(arrayIndex + 1, elementIndex);
                }
                else
                {
                    result += BasesCurrentGraph[arrayIndex][BasesCurrentGraph[arrayIndex].Length - 1] + ", " + getAllBases(arrayIndex + 1, elementIndex);
                }
                
                return result;
            }
            return "";
        }



    }
}
