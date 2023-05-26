using Microsoft.Win32;
using SteamKit2;
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
using System.Windows.Forms;

namespace BeatSaberNoUpdate {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
		}

		void LaunchUrl(string url) {
			Process.Start(new ProcessStartInfo(url) {
				UseShellExecute = true,
				Verb = "open"
			});
		}

		private void browseButton_Click(object sender, EventArgs e) {
			using(var dialog = new FolderBrowserDialog()) {
				if(dialog.ShowDialog() != DialogResult.OK)
					return;

				textbox_path.Text = dialog.SelectedPath;
			}
		}

		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			LaunchUrl("https://kinsi.me/bsmods.html");
		}

		private void Form1_Load(object sender, EventArgs e) {
			var steamPath = FindSteamFolder();
			if (steamPath == null) {
				return;
			}

			var p = GetAppPath(steamPath, AppInfo.APPID);
			if (p != null && CheckFolderPath((string)p))
				textbox_path.Text = (string)p;
		}

		private string GetAppPath(string steamPath, uint appId)
		{
			var filePath = Path.Combine(steamPath, "config", "libraryfolders.vdf");

			if(!File.Exists(filePath))
				return null;

			try {
				var vdf = KeyValue.LoadAsText(filePath);
				var tAppId = appId.ToString();

				foreach(var library in vdf.Children) {
					var libPath = library.Children.FirstOrDefault(x => x.Name == "path").Value;

					if(libPath == null || !Directory.Exists(libPath))
						continue;

					if(library.Children.FirstOrDefault(x => x.Name == "apps")?.Children.Exists(x => x.Name == tAppId) != true)
						continue;

					var fullPath = Path.Combine(libPath, "steamapps", "common", "Beat Saber");

					if(CheckFolderPath(fullPath))
						return fullPath;
				}
			} catch { }

			return null;
		}

		private string FindSteamFolder() {
			var registryPaths = new []{
				@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam",
				@"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam"
			};

			foreach(var registryPath in registryPaths) {
				try {
					var path = Registry.GetValue(registryPath, "InstallPath", null) as string;
					if(path != null && Directory.Exists(path))
						return path;
				} catch { }
			}

			return null;
		}

		bool CheckFolderPath(string path) {
			if(!Directory.Exists(path))
				return false;

			if(!File.Exists(Path.Combine(path, "..", "..", "appmanifest_620980.acf")))
				return false;

			return true;
		}

		void Bad(string str) {
			MessageBox.Show(str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		void SetKv(ref string input, string key, string val) {
			input = Regex.Replace(input, $"\"{key}\".*", $"\"{key}\"\t\"{val}\"", RegexOptions.IgnoreCase);
		}

		private void applyButton_Click(object sender, EventArgs e) {
			if(!CheckFolderPath(textbox_path.Text)) {
				Bad("It seems like the Folder you selected is incorrect. You can go to the properties of Beat Saber in Steam and click on 'Browse Game files' for an easy method to get the correct path");
				return;
			}

			if(!Regex.IsMatch(textbox_manifest.Text, "[0-9]{16,19}")) {
				Bad("The Manifest ID you entered is incorrect. It should be a 16-19 digit number. Make sure to not accidently enter any spaces");
				return;
			}

#if !DEBUG
			if(Process.GetProcesses().Any(x => x.ProcessName.ToLower() == "steam")) {
				Bad("Steam seems to be running, please exit it to apply the patch");
				return;
			}
#endif

			var p = Path.Combine(textbox_path.Text, "..", "..", "appmanifest_620980.acf");

			var acf = File.ReadAllText(p);

			if(acf.Contains(textbox_manifest.Text))
				Bad("Seems like this update is already applied. I'll apply it again for good measure");

			SetKv(ref acf, "ScheduledAutoUpdate", "0");
			SetKv(ref acf, "LastUpdated", ((uint)DateTimeOffset.Now.ToUnixTimeSeconds()).ToString());
			SetKv(ref acf, "StateFlags", "4");
			SetKv(ref acf, "UpdateResult", "0");

			// Disable Autoupdate if its not already (1 is NOT "enable auto updates"; its "Only update this game when I launch it")
			if(checkBox1.Checked)
				SetKv(ref acf, "AutoUpdateBehavior", "1");

			acf = Regex.Replace(acf, "(\"InstalledDepots\".*?\"" + AppInfo.DEPOT_ID + "\".*?\"manifest\"\\s*?)\"[0-9]{16,19}\"", $"$1\"{textbox_manifest.Text}\"", RegexOptions.Singleline | RegexOptions.IgnoreCase);

			File.WriteAllText(p, acf);

			MessageBox.Show("Patch applied. Steam might still claim that an update available, but it should not actually download anything.\n\nIn doubt, create a backup.\n\nTo actually update your game at a later point, go to the properties of the game in Steam and verify the game integrity.\n**Just simply installing an update at a later point without verifying the game integrity will probably break your game**", "Success");
		}

		private void aboutButton_Click(object sender, EventArgs e) {
			MessageBox.Show(
				"This tool modifies a config file of Steam to make it think that you already have the last version (Indicated by the correct Manifest ID) eventho you are not.\n" +
				"\n" +
				"You will need to redo this whenever an Update is released (Basically whenever Steam prompts you to Update to start the game you use this tool instead to fake that you did update"
			);
		}

		private async void getManifestButton_Click(object sender, EventArgs e) {
			getManifestButton.Enabled = false;
			getManifestButton.Text = "Loading...";

			AppInfo appInfo = new AppInfo();
			string manifest = await appInfo.TryRetrieve();
			// if successful fill in textbox
			if(manifest != null) {
				textbox_manifest.Text = manifest;
			} else {
				MessageBox.Show("Automatically retreiving the Manifest ID failed. Copy the latest 'Manifest ID' from the site. Make sure that 'Last update' looks correct, to confirm the site has already spotted the latest update!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
				LaunchUrl($"https://steamdb.info/depot/{AppInfo.DEPOT_ID}/manifests");
			}

			getManifestButton.Enabled = true;
			getManifestButton.Text = "Retrieve";
		}
	}
}
