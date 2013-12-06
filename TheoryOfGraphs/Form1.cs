using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TheoryOfGraphs
{
    public partial class Form1 : Form
    {
        string errorfile = "errors.txt";
        ViewMethods vm = new ViewMethods();
        StrongComponents sc = new StrongComponents();
        ColoringOfGraphs cog = new ColoringOfGraphs();
        string[] firstS = {"Г(x1) = {x2, x3, x4}\nГ(x2) = {}\nГ(x3) = {}\nГ(x4) = {x3, x5}\nГ(x5) = {x2}",
                              " \t1\t2\t3\t4\t5\n"+
                              "1\t0\t1\t1\t1\t0\n"+
                              "2\t0\t0\t0\t0\t0\n"+
                              "3\t0\t0\t0\t0\t0\n"+
                              "4\t0\t0\t1\t0\t1\n"+
                              "5\t0\t1\t0\t0\t0\n"
                              , 
                              " \t1\t2\t3\t4\t5\t6\n"+
                              "1\t1\t1\t1\t0\t0\t0\n"+
                              "2\t-1\t0\t0\t0\t0\t-1\n"+
                              "3\t0\t-1\t0\t-1\t0\t0\n"+
                              "4\t0\t0\t-1\t1\t1\t0\n"+
                              "5\t0\t0\t0\t0\t-1\t1\n" 
                            ,
                            "Г(x1) = {x2, x5, x6}\nГ(x2) = {x1}\nГ(x3) = {x2, x5, x4}\nГ(x4) = {x9}\nГ(x5) = {x1, x7}\nГ(x6) = {x5, x8, x10}\nГ(x7) = {x4}\nГ(x8) = {x10}\nГ(x9) = {x7}\nГ(x10) = {x7, x8}\nГ(x11) = {x8, x13}\nГ(x12) = {x11}\nГ(x13) = {x11, x12}"
                            ,
                            "Г(x1) = {x2, x5, x6}\nГ(x2) = {x1}\nГ(x3) = {x2, x5, x4}\nГ(x4) = {x9}\nГ(x5) = {x1, x7}\nГ(x6) = {x5, x8, x10}\nГ(x7) = {x4}\nГ(x8) = {x10}\nГ(x9) = {x7}\nГ(x10) = {x7, x8}\nГ(x11) = {x8, x13}\nГ(x12) = {x11}\nГ(x13) = {x11, x12}"
                            ,
                            "Г(x1) = {x4[weight=3], x7[weight=11]}\nГ(x2) = {x5[weight=5], x7[weight=2]}\nГ(x3) = {x5[weight=7]}\nГ(x4) = {x1[weight=3], x5[weight=2], x7[weight=8]}\nГ(x5) = {x2[weight=5], x3[weight=7], x4[weight=2], x7[weight=6]}\nГ(x6) = {x7[weight=12]}\nГ(x7) = {x1[weight=11], x2[weight=2], x4[weight=8], x5[weight=6], x6[weight=12]}\n",
                            "Г(x1) = {x2, x6}\nГ(x2) = {x3}\nГ(x3) = {}\nГ(x4) = {x3}\nГ(x5) = {x1, x4, x6}\nГ(x6) = {x2, x3}",
                            "Г(x1[E=0, L=0]) = {x2[weight=6]; x3[weight=7]}\nГ(x2) = {x4[weight=4]; x5[weight=3]}\nГ(x3) = {x6[weight=5]; x7[weight=4]; x8[weight=8]}\nГ(x4) = {x9[weight=12]; x10[weight=8]}\nГ(x5) = {x10[weight=17]}\nГ(x6) = {x10[weight=8]}\nГ(x7) = {x10[weight=4]}\nГ(x8) = {x10[weight=3]}\nГ(x9) = {x11[weight=11]}\nГ(x10) = {x11[weight=8]}\nГ(x11) = {}\n",
                            "Г(x1[E=0, L=0]) = {x2[weight=6]; x3[weight=7]}\nГ(x2) = {x4[weight=4]; x5[weight=3]}\nГ(x3) = {x6[weight=5]; x7[weight=4]; x8[weight=8]}\nГ(x4) = {x9[weight=12]; x10[weight=8]}\nГ(x5) = {x10[weight=17]}\nГ(x6) = {x10[weight=8]}\nГ(x7) = {x10[weight=4]}\nГ(x8) = {x10[weight=3]}\nГ(x9) = {x11[weight=11]}\nГ(x10) = {x11[weight=8]}\nГ(x11) = {}\n",
                            "Г(x1[E=0, L=0]) = {x2[weight=6]; x3[weight=7]}\nГ(x2) = {x4[weight=4]; x5[weight=3]}\nГ(x3) = {x6[weight=5]; x7[weight=4]; x8[weight=8]}\nГ(x4) = {x9[weight=12]; x10[weight=8]}\nГ(x5) = {x10[weight=17]}\nГ(x6) = {x10[weight=8]}\nГ(x7) = {x10[weight=4]}\nГ(x8) = {x10[weight=3]}\nГ(x9) = {x11[weight=11]}\nГ(x10) = {x11[weight=8]}\nГ(x11) = {}\n",
                            ""
                          };
        public Form1()
        {
            InitializeComponent();
            comboBoxMethods.Items.Add("Г-соответствие");
            comboBoxMethods.Items.Add("Матрица смежности");
            comboBoxMethods.Items.Add("Матрица инцидентности");
            comboBoxMethods.Items.Add("Сильные компоненты графа (через матрицы)");
            comboBoxMethods.Items.Add("Сильные компоненты графа (через множества)");
            comboBoxMethods.Items.Add("Задача о минимальных путях");
            comboBoxMethods.Items.Add("Нумерация и ранги вершин");
            comboBoxMethods.Items.Add("Вычисление критических путей при помощи резервов");
            comboBoxMethods.Items.Add("Нахождение критических путей с помощью алгоритма Форда");
            comboBoxMethods.Items.Add("Нахождение критических путей с помощью алгоритма Беллмана");
            
            comboBoxMethods.SelectedIndex = 9;

        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxMethods.SelectedIndex <= 2)
                {
                    vm = new ViewMethods();
                    if (selectedContent.Checked)
                        read(richTextBoxView.SelectedText);
                    else
                        read(richTextBoxView.Text);
                    writeMethods();
                }
                else if (comboBoxMethods.SelectedIndex == 3)/*через матрицы*/
                {
                    sc = new StrongComponents();
                    vm = new ViewMethods();
                    if (selectedContent.Checked)
                        vm.readG(richTextBoxView.SelectedText.Split('\n'));
                    else
                        vm.readG(richTextBoxView.Text.Split('\n'));
                    int n = Convert.ToInt16(textBoxN.Text);
                    sc.setGraph(vm.gamma_method, vm.getArrB(n), n);
                    richTextBoxView.Text += "\n" + vm.view(n, 0);
                    richTextBoxView.Text += "\n" + sc.writeR();
                    richTextBoxView.Text += "\n" + sc.writeQ();
                    richTextBoxView.Text += "\nМатрица достижимостей:\n" + sc.writeMatrixR(n);
                    richTextBoxView.Text += "\nМатрица контрадостижимостей:\n" + sc.writeMatrixQ(n);
                    richTextBoxView.Text += "\nМатрица произведения:\n" + sc.writeMultMatrix();
                    richTextBoxView.Text += "\nПроверка на ацикличность: " + sc.ciclicGraph() + "\n";
                    richTextBoxView.Text += "\nСильные компоненты:\n" + sc.writeStrongComponents();
                    richTextBoxView.Text += "\nМатрица R:\n" + sc.getMatrixR(n);
                    richTextBoxView.Text += "\nГраф конденсации:\n" + sc.getCondensationGraph();
                    richTextBoxView.Text += "\nБаза B*:\n" + sc.getBase();
                    richTextBoxView.Text += "\nБазы исходного графа:\n" + sc.getAllBases();
                    if (!sc.error.Equals(""))
                        MessageBox.Show(sc.error);
                }
                else if (comboBoxMethods.SelectedIndex == 4)/*через множества*/
                {
                    sc = new StrongComponents();
                    vm = new ViewMethods();
                    if (selectedContent.Checked)
                        vm.readG(richTextBoxView.SelectedText.Split('\n'));
                    else
                        vm.readG(richTextBoxView.Text.Split('\n'));
                    int n = Convert.ToInt16(textBoxN.Text);
                    sc.setGraph(vm.gamma_method, vm.getArrB(n), n);
                    richTextBoxView.Text += "\n" + vm.view(n, 0);
                    richTextBoxView.Text += "\n" + sc.writeR();
                    richTextBoxView.Text += "\n" + sc.writeQ();
                    richTextBoxView.Text += "\nМатрица достижимостей:\n" + sc.writeMatrixR(n);
                    richTextBoxView.Text += "\nМатрица контрадостижимостей:\n" + sc.writeMatrixQ(n);
                    richTextBoxView.Text += "\nМатрица произведения:\n" + sc.writeMultMatrix();
                    richTextBoxView.Text += "\nПроверка на ацикличность: " + sc.ciclicGraph() + "\n";
                    richTextBoxView.Text += "\nСильные компоненты:\n" + sc.getStrongComponents();
                    richTextBoxView.Text += "\nМатрица R:\n" + sc.getMatrixR(n);
                    richTextBoxView.Text += "\nГраф конденсации:\n" + sc.getCondensationGraph();
                    richTextBoxView.Text += "\nБаза B*:\n" + sc.getBase();
                    richTextBoxView.Text += "\nБазы исходного графа:\n" + sc.getAllBases();
                    if (!sc.error.Equals(""))
                        MessageBox.Show(sc.error);
                }
                else if (comboBoxMethods.SelectedIndex == 5)/*задача о минимальных путях*/
                {
                    try
                    {
                        vm = new ViewMethods();
                        if (selectedContent.Checked)
                            vm.readG(richTextBoxView.SelectedText.Split('\n'));
                        else
                            vm.readG(richTextBoxView.Text.Split('\n'));
                        int n = Convert.ToInt16(textBoxN.Text);
                        cog = new ColoringOfGraphs();
                        cog.setGraph(vm, n);
                        richTextBoxView.Text += "Кратчайшие пути:\n" + cog.searchingMinimalWay();
                        richTextBoxView.Text += "Пути:\n" + cog.writeMatrix();
                        if (!cog.error.Equals(""))
                        {
                            MessageBox.Show(cog.error);
                            string[] arr = cog.error.Split('\n');
                            File.WriteAllLines(errorfile, arr);
                            System.Diagnostics.Process.Start(errorfile);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n" + ex.StackTrace, comboBoxMethods.Items[comboBoxMethods.SelectedIndex].ToString());
                    }
                }
                else if (comboBoxMethods.SelectedIndex == 6)/*задача о нумерации и рангах*/
                {
                    try
                    {
                        vm = new ViewMethods();
                        if (selectedContent.Checked)
                            vm.readG(richTextBoxView.SelectedText.Split('\n'));
                        else
                            vm.readG(richTextBoxView.Text.Split('\n'));
                        int n = Convert.ToInt16(textBoxN.Text);
                        cog = new ColoringOfGraphs();
                        cog.setGraph(vm, n);
                        richTextBoxView.Text += "Перенумерованный граф:\n" + cog.getNumerationGraph(n);
                        //richTextBoxView.Text += "Пути:\n" + cog.writeMatrix();
                        if (!cog.error.Equals(""))
                            MessageBox.Show(cog.error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n" + ex.StackTrace, comboBoxMethods.Items[comboBoxMethods.SelectedIndex].ToString());
                    }
                }
                else if (comboBoxMethods.SelectedIndex == 7)/*вычисление критических путей при помощи резервов*/
                {
                    try
                    {
                        //на вход подаётся сеть!
                        vm = new ViewMethods();
                        if (selectedContent.Checked)
                        {
                            vm.setGraph(richTextBoxView.SelectedText.Split('\n'));
                        }
                        else
                        {
                            vm.setGraph(richTextBoxView.Text.Split('\n'));
                        }
                        int n = Convert.ToInt16(textBoxN.Text);
                        NetworkPlanning np = new NetworkPlanning();
                        np.setGraph(vm, n);
                        //richTextBoxView.Text += vm.view(11, 0); 
                        richTextBoxView.Text += np.writeTable();

                        if (!np.error.Equals(""))
                        {
                            MessageBox.Show(np.error);
                            string[] arr = np.error.Split('\n');
                            File.WriteAllLines(errorfile, arr);
                            System.Diagnostics.Process.Start(errorfile);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n" + ex.StackTrace, comboBoxMethods.Items[comboBoxMethods.SelectedIndex].ToString());
                    }
                }
                else if (comboBoxMethods.SelectedIndex == 8)/*вычисление критических путей с помощью алгоритма Форда*/
                {
                    try
                    {
                        //на вход подаётся сеть!
                        vm = new ViewMethods();
                        if (selectedContent.Checked)
                            vm.setGraph(richTextBoxView.SelectedText.Split('\n'));
                        else
                            vm.setGraph(richTextBoxView.Text.Split('\n'));
                        int n = Convert.ToInt16(textBoxN.Text);
                        NetworkPlanning np = new NetworkPlanning();
                        np.setGraph(vm, n);
                        richTextBoxView.Text += "Критический путь:\n" + np.fordsMethod();
                        
                        if (!np.error.Equals(""))
                        {
                            MessageBox.Show(np.error);
                            string[] arr = np.error.Split('\n');
                            File.WriteAllLines(errorfile, arr);
                            System.Diagnostics.Process.Start(errorfile);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n" + ex.StackTrace, comboBoxMethods.Items[comboBoxMethods.SelectedIndex].ToString());
                    }
                }
                else if (comboBoxMethods.SelectedIndex == 9) /*нахождение критических путей с помощью алгоритма Беллмана*/
                {
                    try
                    {
                        vm = new ViewMethods();
                        if (selectedContent.Checked)
                            vm.setGraph(richTextBoxView.SelectedText.Split('\n'));
                        else
                            vm.setGraph(richTextBoxView.Text.Split('\n'));
                        int n = Convert.ToInt16(textBoxN.Text);
                        NetworkPlanning np = new NetworkPlanning();
                        np.setGraph(vm, n);
                        richTextBoxView.Text += "Критические пути:\n" + np.bellmanMethod();
                        
                        if (!np.error.Equals(""))
                        {
                            MessageBox.Show(np.error);
                            string[] arr = np.error.Split('\n');
                            File.WriteAllLines(errorfile, arr);
                            System.Diagnostics.Process.Start(errorfile);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n" + ex.StackTrace, comboBoxMethods.Items[comboBoxMethods.SelectedIndex].ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        void read(string STRING)
        {
            string[] str_arr = STRING.Split('\n');
            switch (comboBoxMethods.SelectedIndex)
            {
                case 0:
                    {
                        vm.readG(str_arr);
                    }
                    break;
                case 1:
                    {
                        vm.readA(str_arr, Convert.ToInt16(textBoxN.Text));
                    }
                    break;
                case 2:
                    {
                        vm.readB(str_arr, Convert.ToInt16(textBoxN.Text), STRING);
                    }
                    break;
                case 3: { } 
                    break;
            }
        }



        void writeMethods()
        {
            richTextBoxView.Text += "\n";
            switch (comboBoxMethods.SelectedIndex)
            {

                case 0:
                    {
                        richTextBoxView.Text += vm.view(Convert.ToInt16(textBoxN.Text), 0);
                    }
                    break;
                case 1:
                    {
                        richTextBoxView.Text += vm.view(Convert.ToInt16(textBoxN.Text), 1);
                    }
                    break;
                case 2:
                    {
                        richTextBoxView.Text += vm.view(Convert.ToInt16(textBoxN.Text), 2);
                    }
                    break;
            }

        }

        private void comboBoxMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                richTextBoxView.Text = firstS[comboBoxMethods.SelectedIndex];
                textBoxN.Text = "5";
                if (comboBoxMethods.SelectedIndex == 3 || comboBoxMethods.SelectedIndex == 4)
                    textBoxN.Text = "13";
                else if (comboBoxMethods.SelectedIndex == 5)
                    textBoxN.Text = "7";
                else if (comboBoxMethods.SelectedIndex == 6)
                    textBoxN.Text = "6";
                else if (comboBoxMethods.SelectedIndex == 7 || comboBoxMethods.SelectedIndex == 8 || comboBoxMethods.SelectedIndex == 9)
                    textBoxN.Text = "11";

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void richTextBoxView_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void richTextBoxView_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void richTextBoxView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checkBoxTab.Checked)
                if (Char.IsDigit(e.KeyChar))
                {
                    
                }
                else if (e.KeyChar.Equals(' '))
                {
                    richTextBoxView.Text += "\t";
                    richTextBoxView.SelectionStart = richTextBoxView.Text.Length;
                    e.Handled = true;
                }
        }

        private void работатьСВыделеннойОбластьюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedContent.Checked = !selectedContent.Checked;
        }

        private void показатьКратчайшийПутьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxView.Text += cog.getWay(textBoxMinWay.Text);
        }
    }
}
