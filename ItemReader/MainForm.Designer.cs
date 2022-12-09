using System.ComponentModel.Design;

namespace ItemReader
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components is not null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.finder = new System.Windows.Forms.Button();
            this.scanner = new System.Windows.Forms.Button();
            this.fileOpener = new System.Windows.Forms.Button();
            this.planner = new System.Windows.Forms.LinkLabel();
            this.logger = new System.Windows.Forms.RichTextBox();
            this.jsonOpener = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // finder
            // 
            this.finder.Location = new System.Drawing.Point(12, 12);
            this.finder.Name = "finder";
            this.finder.Size = new System.Drawing.Size(223, 129);
            this.finder.TabIndex = 0;
            this.finder.Text = "Find Genshin Window";
            this.finder.UseVisualStyleBackColor = true;
            this.finder.Click += new System.EventHandler(this.GenshinFinderOnClick);
            // 
            // scanner
            // 
            this.scanner.Location = new System.Drawing.Point(265, 12);
            this.scanner.Name = "scanner";
            this.scanner.Size = new System.Drawing.Size(223, 129);
            this.scanner.TabIndex = 1;
            this.scanner.Text = "Scan Inventory";
            this.scanner.UseVisualStyleBackColor = true;
            this.scanner.Click += new System.EventHandler(this.ScanItemsOnClick);
            // 
            // fileOpener
            // 
            this.fileOpener.Location = new System.Drawing.Point(321, 665);
            this.fileOpener.Name = "fileOpener";
            this.fileOpener.Size = new System.Drawing.Size(167, 23);
            this.fileOpener.TabIndex = 2;
            this.fileOpener.Text = "Open json location";
            this.fileOpener.UseVisualStyleBackColor = true;
            // 
            // planner
            // 
            this.planner.AutoSize = true;
            this.planner.Location = new System.Drawing.Point(12, 669);
            this.planner.Name = "planner";
            this.planner.Size = new System.Drawing.Size(123, 15);
            this.planner.TabIndex = 3;
            this.planner.TabStop = true;
            this.planner.Text = "open Genshin Planner";
            // 
            // logger
            // 
            this.logger.Location = new System.Drawing.Point(12, 147);
            this.logger.Name = "logger";
            this.logger.Size = new System.Drawing.Size(476, 512);
            this.logger.TabIndex = 4;
            this.logger.Text = "";
            this.logger.TextChanged += new System.EventHandler(this.LoggerTextChanged);
            // 
            // jsonOpener
            // 
            this.jsonOpener.FileName = "inventory.json";
            this.jsonOpener.FileOk += new System.ComponentModel.CancelEventHandler(this.JsonOpenerFileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 700);
            this.Controls.Add(this.logger);
            this.Controls.Add(this.planner);
            this.Controls.Add(this.fileOpener);
            this.Controls.Add(this.scanner);
            this.Controls.Add(this.finder);
            this.Name = "Form1";
            this.Text = "GenshinReader";
            this.Load += new System.EventHandler(this.LoadForm);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button finder;
        private Button scanner;
        private Button fileOpener;
        private LinkLabel planner;
        private RichTextBox logger;
        private OpenFileDialog jsonOpener;


    }
}