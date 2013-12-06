namespace TheoryOfGraphs
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.comboBoxMethods = new System.Windows.Forms.ComboBox();
            this.richTextBoxView = new System.Windows.Forms.RichTextBox();
            this.contextMenuStripR = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectedContent = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonView = new System.Windows.Forms.Button();
            this.textBoxN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxTab = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.показатьКратчайшийПутьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxMinWay = new System.Windows.Forms.ToolStripTextBox();
            this.contextMenuStripR.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxMethods
            // 
            this.comboBoxMethods.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxMethods.FormattingEnabled = true;
            this.comboBoxMethods.Location = new System.Drawing.Point(12, 12);
            this.comboBoxMethods.Name = "comboBoxMethods";
            this.comboBoxMethods.Size = new System.Drawing.Size(307, 23);
            this.comboBoxMethods.TabIndex = 0;
            this.comboBoxMethods.SelectedIndexChanged += new System.EventHandler(this.comboBoxMethods_SelectedIndexChanged);
            // 
            // richTextBoxView
            // 
            this.richTextBoxView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxView.ContextMenuStrip = this.contextMenuStripR;
            this.richTextBoxView.Location = new System.Drawing.Point(12, 41);
            this.richTextBoxView.Name = "richTextBoxView";
            this.richTextBoxView.Size = new System.Drawing.Size(625, 336);
            this.richTextBoxView.TabIndex = 1;
            this.richTextBoxView.Text = "";
            this.richTextBoxView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBoxView_KeyDown);
            this.richTextBoxView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBoxView_KeyPress);
            this.richTextBoxView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.richTextBoxView_KeyUp);
            // 
            // contextMenuStripR
            // 
            this.contextMenuStripR.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectedContent,
            this.показатьКратчайшийПутьToolStripMenuItem});
            this.contextMenuStripR.Name = "contextMenuStripR";
            this.contextMenuStripR.Size = new System.Drawing.Size(269, 70);
            // 
            // selectedContent
            // 
            this.selectedContent.Name = "selectedContent";
            this.selectedContent.Size = new System.Drawing.Size(260, 22);
            this.selectedContent.Text = "Работать с выделенной областью";
            this.selectedContent.Click += new System.EventHandler(this.работатьСВыделеннойОбластьюToolStripMenuItem_Click);
            // 
            // buttonView
            // 
            this.buttonView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonView.Location = new System.Drawing.Point(562, 10);
            this.buttonView.Name = "buttonView";
            this.buttonView.Size = new System.Drawing.Size(75, 23);
            this.buttonView.TabIndex = 4;
            this.buttonView.Text = "Показать";
            this.buttonView.UseVisualStyleBackColor = true;
            this.buttonView.Click += new System.EventHandler(this.buttonView_Click);
            // 
            // textBoxN
            // 
            this.textBoxN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxN.Location = new System.Drawing.Point(357, 12);
            this.textBoxN.Name = "textBoxN";
            this.textBoxN.Size = new System.Drawing.Size(26, 23);
            this.textBoxN.TabIndex = 3;
            this.toolTip1.SetToolTip(this.textBoxN, "Количество вершин графа");
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(326, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "n =";
            this.toolTip1.SetToolTip(this.label1, "Количество вершин графа");
            // 
            // checkBoxTab
            // 
            this.checkBoxTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxTab.AutoSize = true;
            this.checkBoxTab.Location = new System.Drawing.Point(389, 14);
            this.checkBoxTab.Name = "checkBoxTab";
            this.checkBoxTab.Size = new System.Drawing.Size(167, 19);
            this.checkBoxTab.TabIndex = 8;
            this.checkBoxTab.Text = "Отступ после ввода цифр";
            this.checkBoxTab.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // показатьКратчайшийПутьToolStripMenuItem
            // 
            this.показатьКратчайшийПутьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textBoxMinWay});
            this.показатьКратчайшийПутьToolStripMenuItem.Name = "показатьКратчайшийПутьToolStripMenuItem";
            this.показатьКратчайшийПутьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.показатьКратчайшийПутьToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.показатьКратчайшийПутьToolStripMenuItem.Text = "Показать кратчайший путь";
            this.показатьКратчайшийПутьToolStripMenuItem.Click += new System.EventHandler(this.показатьКратчайшийПутьToolStripMenuItem_Click);
            // 
            // textBoxMinWay
            // 
            this.textBoxMinWay.Name = "textBoxMinWay";
            this.textBoxMinWay.Size = new System.Drawing.Size(100, 23);
            this.textBoxMinWay.Text = "x1-x2";
            this.textBoxMinWay.ToolTipText = "Формат: вершина1-вершина2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 389);
            this.Controls.Add(this.checkBoxTab);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxN);
            this.Controls.Add(this.buttonView);
            this.Controls.Add(this.richTextBoxView);
            this.Controls.Add(this.comboBoxMethods);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MinimumSize = new System.Drawing.Size(347, 340);
            this.Name = "Form1";
            this.Text = "Теория графов";
            this.contextMenuStripR.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxMethods;
        private System.Windows.Forms.RichTextBox richTextBoxView;
        private System.Windows.Forms.Button buttonView;
        private System.Windows.Forms.TextBox textBoxN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripR;
        private System.Windows.Forms.CheckBox checkBoxTab;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem selectedContent;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem показатьКратчайшийПутьToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox textBoxMinWay;

    }
}

