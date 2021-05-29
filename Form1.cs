using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatSaberNoUpdate {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			using(var dialog = new FolderBrowserDialog()) {
				if(dialog.ShowDialog() != DialogResult.OK)
					return;

				textbox_path.Text = dialog.SelectedPath;
			}
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			MessageBox.Show("Copy the latest 'Manifest ID' from the site. Make sure that 'Last update' looks correct, to make sure the site has already spotted the update!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
			Process.Start(new ProcessStartInfo("https://steamdb.info/depot/620981/manifests") {
				UseShellExecute = true,
				Verb = "open"
			});
		}

		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Process.Start(new ProcessStartInfo("https://github.com/kinsi55?tab=repositories&q=BeatSaber") {
				UseShellExecute = true,
				Verb = "open"
			});
		}

		private void Form1_Load(object sender, EventArgs e) {
			try {
				var p = Registry.GetValue(
					@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 620980",
					"InstallLocation", 
					null
				);

				if(p != null && CheckFolderPath((string)p))
					textbox_path.Text = (string)p;
			} catch { }
		}

		bool CheckFolderPath(string path) {
			if(!Directory.Exists(path))
				return false;

			if(!File.Exists(Path.Join(path, "..", "..", "appmanifest_620980.acf")))
				return false;

			return true;
		}

		void Bad(string str) {
			MessageBox.Show(str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

		}

		void SetKv(ref string input, string key, string val) {
			input = Regex.Replace(input, $"\"{key}\".*", $"\t\"{key}\"\t\"{val}\"", RegexOptions.IgnoreCase);
		}

		private void button2_Click(object sender, EventArgs e) {
			if(!CheckFolderPath(textbox_path.Text)) {
				Bad("It seems like the Folder you selected is incorrect. You can go to the properties of Beat Saber in Steam and click on 'Browse Game files' for an easy method to get the correct path");
				return;
			}

			if(!Regex.IsMatch(textbox_manifest.Text, "[0-9]{19}")) {
				Bad("The Manifest ID you entered is incorrect. It should be a 19 digit number. Make sure to not accidently enter any spaces");
				return;
			}

			if(Process.GetProcesses().Any(x => x.ProcessName.ToLower().StartsWith("steam"))) {
				Bad("Steam seems to be running, please exit it to apply the patch");
				return;
			}

			var p = Path.Join(textbox_path.Text, "..", "..", "appmanifest_620980.acf");

			var acf = File.ReadAllText(p);

			if(acf.Contains(textbox_manifest.Text))
				Bad("Seems like this update is already applied. I'll apply it again for good measure");

			SetKv(ref acf, "ScheduledAutoUpdate", "0");
			SetKv(ref acf, "LastUpdated", ((uint)DateTimeOffset.UnixEpoch.ToUnixTimeSeconds()).ToString());
			SetKv(ref acf, "StateFlags", "4");
			SetKv(ref acf, "UpdateResult", "0");

			if(checkBox1.Checked)
				SetKv(ref acf, "AutoUpdateBehavior", "1");

			acf = Regex.Replace(acf, "(\"620981\".*?\"manifest\".*)\"[0-9]{19}\"", $"$1\"{textbox_manifest.Text}\"", RegexOptions.Singleline | RegexOptions.IgnoreCase);

			File.WriteAllText(p, acf);

			MessageBox.Show("Patch applied. If everything worked correctly Steam should not require you to update Beat Saber any more the next time you start it", "Success");
		}

		private void button3_Click(object sender, EventArgs e) {
			MessageBox.Show(
				"This tool modifies a config file of Steam to make it think that you already have the last version (Indicated by the correct Manifest ID) eventho you are not.\n" +
				"\n" +
				"You will need to redo this whenever an Update is released (Basically whenever Steam prompts you to Update to start the game you use this tool instead to fake that you did update"
			);
		}
	}
}
