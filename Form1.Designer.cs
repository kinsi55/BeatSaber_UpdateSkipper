
namespace BeatSaberNoUpdate {
	partial class Form1 {
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.installFolderLabel = new System.Windows.Forms.Label();
			this.browseButton = new System.Windows.Forms.Button();
			this.textbox_path = new System.Windows.Forms.TextBox();
			this.manifestIdLabel = new System.Windows.Forms.Label();
			this.textbox_manifest = new System.Windows.Forms.TextBox();
			this.applyButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.aboutButton = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.getManifestButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// installFolderLabel
			// 
			this.installFolderLabel.AutoSize = true;
			this.installFolderLabel.Location = new System.Drawing.Point(10, 11);
			this.installFolderLabel.Name = "installFolderLabel";
			this.installFolderLabel.Size = new System.Drawing.Size(124, 13);
			this.installFolderLabel.TabIndex = 0;
			this.installFolderLabel.Text = "Beat Saber install Folder:";
			// 
			// browseButton
			// 
			this.browseButton.Location = new System.Drawing.Point(247, 27);
			this.browseButton.Name = "browseButton";
			this.browseButton.Size = new System.Drawing.Size(64, 20);
			this.browseButton.TabIndex = 4;
			this.browseButton.Text = "Browse...";
			this.browseButton.UseVisualStyleBackColor = true;
			this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
			// 
			// textbox_path
			// 
			this.textbox_path.Location = new System.Drawing.Point(10, 27);
			this.textbox_path.Name = "textbox_path";
			this.textbox_path.Size = new System.Drawing.Size(232, 20);
			this.textbox_path.TabIndex = 3;
			// 
			// manifestIdLabel
			// 
			this.manifestIdLabel.AutoSize = true;
			this.manifestIdLabel.Location = new System.Drawing.Point(10, 57);
			this.manifestIdLabel.Name = "manifestIdLabel";
			this.manifestIdLabel.Size = new System.Drawing.Size(149, 13);
			this.manifestIdLabel.TabIndex = 5;
			this.manifestIdLabel.Text = "Latest Beat Saber Manifest ID";
			// 
			// textbox_manifest
			// 
			this.textbox_manifest.Location = new System.Drawing.Point(10, 73);
			this.textbox_manifest.MaxLength = 19;
			this.textbox_manifest.Name = "textbox_manifest";
			this.textbox_manifest.Size = new System.Drawing.Size(232, 20);
			this.textbox_manifest.TabIndex = 7;
			// 
			// applyButton
			// 
			this.applyButton.Location = new System.Drawing.Point(10, 128);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(136, 20);
			this.applyButton.TabIndex = 8;
			this.applyButton.Text = "Apply";
			this.applyButton.UseVisualStyleBackColor = true;
			this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 151);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(272, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Created by Kinsi. Check out my Mods they\'re kinda nice:";
			// 
			// linkLabel2
			// 
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.Location = new System.Drawing.Point(282, 151);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(29, 13);
			this.linkLabel2.TabIndex = 10;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "#AD";
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			// 
			// aboutButton
			// 
			this.aboutButton.Location = new System.Drawing.Point(175, 128);
			this.aboutButton.Name = "aboutButton";
			this.aboutButton.Size = new System.Drawing.Size(136, 20);
			this.aboutButton.TabIndex = 11;
			this.aboutButton.Text = "What will this do?";
			this.aboutButton.UseVisualStyleBackColor = true;
			this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(10, 98);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(195, 17);
			this.checkBox1.TabIndex = 12;
			this.checkBox1.Text = "Disable Autoupdate if its not already";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// getManifestButton
			// 
			this.getManifestButton.Location = new System.Drawing.Point(247, 73);
			this.getManifestButton.Name = "getManifestButton";
			this.getManifestButton.Size = new System.Drawing.Size(64, 20);
			this.getManifestButton.TabIndex = 13;
			this.getManifestButton.Text = "Retrieve";
			this.getManifestButton.UseVisualStyleBackColor = true;
			this.getManifestButton.Click += new System.EventHandler(this.getManifestButton_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(321, 172);
			this.Controls.Add(this.getManifestButton);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.aboutButton);
			this.Controls.Add(this.linkLabel2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.applyButton);
			this.Controls.Add(this.textbox_manifest);
			this.Controls.Add(this.manifestIdLabel);
			this.Controls.Add(this.browseButton);
			this.Controls.Add(this.textbox_path);
			this.Controls.Add(this.installFolderLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Beat Saber Update Skipper (Steam Only)";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label installFolderLabel;
		private System.Windows.Forms.Button browseButton;
		private System.Windows.Forms.TextBox textbox_path;
		private System.Windows.Forms.Label manifestIdLabel;
		private System.Windows.Forms.TextBox textbox_manifest;
		private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.Button aboutButton;
		private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button getManifestButton;
    }
}

