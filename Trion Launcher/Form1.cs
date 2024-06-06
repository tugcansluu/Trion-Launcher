using CmlLib.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CmlLib.Core.Auth;
using DiscordRpcDemo;


namespace Trion_Launcher
{
    public partial class Form1 : Form
    {
        private DiscordRpc.EventHandlers handlers;
        private DiscordRpc.RichPresence presence;
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public static string version;

        private void path()
        {
            var path = new MinecraftPath();
            var launcher = new CMLauncher(path);

            foreach (var item in launcher.GetAllVersions())
            {
                versions.Items.Add(item.Name);
            }

        }

        private void launch()
        {
            var path = new MinecraftPath();
            var launcher = new CMLauncher(path);
            var launcherOption = new MLaunchOption
            {
                MaximumRamMb = 4096,
                Session = MSession.GetOfflineSession(textBox1.Text),
                ServerIp = "",
            };
            version = versions.SelectedItem.ToString();
            var process = launcher.CreateProcess(version, launcherOption);
            process.Start();
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.handlers = default(DiscordRpc.EventHandlers);
            DiscordRpc.Initialize("1248383265497419860", ref this.handlers, true, null);

            this.presence = new DiscordRpc.RichPresence
            {
                details = "Login Screen",
                state = "Launching...",
                largeImageKey = "https://i.hizliresim.com/yrzig7g.png",
                smallImageKey = "https://i.hizliresim.com/mirxx6b.png"
            };
            DiscordRpc.UpdatePresence(ref this.presence);
            path();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled= false;
            Thread thread = new Thread(() => launch());
            thread.Start();
            label2.Visible= true;
        }

        
    }
}
