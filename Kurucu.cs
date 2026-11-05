using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace AmongUsInstaller
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InstallerForm());
        }
    }

    public class InstallerForm : Form
    {
        private TextBox txtPath;
        private Button btnBrowse;
        private Button btnInstall;
        private Button btnOpenFolder;
        private Label lblStatus;
        private ProgressBar progressBar;
        private RichTextBox txtLog;
        private string detectedPlatform = "";

        public InstallerForm()
        {
            InitializeComponent();
            DetectGamePath();
        }

        private void InitializeComponent()
        {
            this.Text = "Among Us - Kozmetik Menüsü Otomatik Kurucu";
            this.Size = new Size(630, 490);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(24, 26, 32);
            this.ForeColor = Color.White;

            // Başlık Paneli
            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 75;
            headerPanel.BackColor = Color.FromArgb(18, 20, 24);

            Label lblTitle = new Label();
            lblTitle.Text = "🚀 Among Us - Kozmetik Menüsü Kurucu";
            lblTitle.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(70, 180, 255);
            lblTitle.Location = new Point(20, 12);
            lblTitle.AutoSize = true;

            Label lblSubtitle = new Label();
            lblSubtitle.Text = "Epic Games (64-bit) ve Steam (32-bit) otomatik algılama ve kurulum aracı";
            lblSubtitle.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            lblSubtitle.ForeColor = Color.FromArgb(170, 175, 190);
            lblSubtitle.Location = new Point(22, 42);
            lblSubtitle.AutoSize = true;

            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblSubtitle);
            this.Controls.Add(headerPanel);

            // Ana İçerik
            Label lblPathDesc = new Label();
            lblPathDesc.Text = "Among Us Oyun Klasörü:";
            lblPathDesc.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblPathDesc.Location = new Point(22, 95);
            lblPathDesc.AutoSize = true;
            this.Controls.Add(lblPathDesc);

            txtPath = new TextBox();
            txtPath.Location = new Point(24, 122);
            txtPath.Size = new Size(460, 26);
            txtPath.Font = new Font("Segoe UI", 9.5f);
            txtPath.BackColor = Color.FromArgb(34, 38, 48);
            txtPath.ForeColor = Color.White;
            txtPath.BorderStyle = BorderStyle.FixedSingle;
            txtPath.TextChanged += TxtPath_TextChanged;
            this.Controls.Add(txtPath);

            btnBrowse = new Button();
            btnBrowse.Text = "Gözat...";
            btnBrowse.Location = new Point(495, 120);
            btnBrowse.Size = new Size(95, 29);
            btnBrowse.Font = new Font("Segoe UI", 9.5f);
            btnBrowse.BackColor = Color.FromArgb(45, 50, 65);
            btnBrowse.ForeColor = Color.White;
            btnBrowse.FlatStyle = FlatStyle.Flat;
            btnBrowse.FlatAppearance.BorderColor = Color.FromArgb(70, 80, 105);
            btnBrowse.Click += BtnBrowse_Click;
            this.Controls.Add(btnBrowse);

            lblStatus = new Label();
            lblStatus.Text = "Oyun klasörü taranıyor...";
            lblStatus.Font = new Font("Segoe UI", 9f);
            lblStatus.ForeColor = Color.FromArgb(255, 205, 90);
            lblStatus.Location = new Point(24, 155);
            lblStatus.Size = new Size(565, 20);
            this.Controls.Add(lblStatus);

            btnInstall = new Button();
            btnInstall.Text = "⭐ KURULUMU YAP VE TÜM KOZMETİKLERİ AÇ";
            btnInstall.Location = new Point(24, 185);
            btnInstall.Size = new Size(566, 46);
            btnInstall.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnInstall.BackColor = Color.FromArgb(0, 155, 110);
            btnInstall.ForeColor = Color.White;
            btnInstall.FlatStyle = FlatStyle.Flat;
            btnInstall.FlatAppearance.BorderSize = 0;
            btnInstall.Cursor = Cursors.Hand;
            btnInstall.Click += BtnInstall_Click;
            this.Controls.Add(btnInstall);

            progressBar = new ProgressBar();
            progressBar.Location = new Point(24, 240);
            progressBar.Size = new Size(566, 12);
            progressBar.Visible = false;
            this.Controls.Add(progressBar);

            Label lblLogTitle = new Label();
            lblLogTitle.Text = "İşlem Detayları:";
            lblLogTitle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblLogTitle.ForeColor = Color.FromArgb(160, 165, 180);
            lblLogTitle.Location = new Point(24, 260);
            lblLogTitle.AutoSize = true;
            this.Controls.Add(lblLogTitle);

            txtLog = new RichTextBox();
            txtLog.Location = new Point(24, 282);
            txtLog.Size = new Size(566, 120);
            txtLog.Font = new Font("Consolas", 8.5f);
            txtLog.BackColor = Color.FromArgb(16, 18, 22);
            txtLog.ForeColor = Color.FromArgb(200, 210, 225);
            txtLog.ReadOnly = true;
            txtLog.BorderStyle = BorderStyle.None;
            this.Controls.Add(txtLog);

            btnOpenFolder = new Button();
            btnOpenFolder.Text = "📁 Oyun Klasörünü Aç";
            btnOpenFolder.Location = new Point(425, 412);
            btnOpenFolder.Size = new Size(165, 28);
            btnOpenFolder.Font = new Font("Segoe UI", 9f);
            btnOpenFolder.BackColor = Color.FromArgb(35, 40, 52);
            btnOpenFolder.ForeColor = Color.White;
            btnOpenFolder.FlatStyle = FlatStyle.Flat;
            btnOpenFolder.FlatAppearance.BorderColor = Color.FromArgb(60, 68, 85);
            btnOpenFolder.Click += BtnOpenFolder_Click;
            this.Controls.Add(btnOpenFolder);

            Label lblFooter = new Label();
            lblFooter.Text = "Oyun açıldıktan sonra DELETE tuşu ile menüyü açabilirsiniz.";
            lblFooter.Font = new Font("Segoe UI", 8.5f, FontStyle.Italic);
            lblFooter.ForeColor = Color.FromArgb(120, 130, 150);
            lblFooter.Location = new Point(24, 416);
            lblFooter.AutoSize = true;
            this.Controls.Add(lblFooter);
        }

        private void DetectGamePath()
        {
            string path = "";

            // 1. Epic Games Manifest Taraması
            string epicManifestDir = @"C:\ProgramData\Epic\EpicGamesLauncher\Data\Manifests";
            if (Directory.Exists(epicManifestDir))
            {
                try
                {
                    string[] items = Directory.GetFiles(epicManifestDir, "*.item");
                    foreach (string item in items)
                    {
                        string content = File.ReadAllText(item);
                        if (content.IndexOf("AmongUs", StringComparison.OrdinalIgnoreCase) >= 0 ||
                            content.IndexOf("Among Us", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            Match m = Regex.Match(content, "\"InstallLocation\"\\s*:\\s*\"([^\"]+)\"");
                            if (m.Success)
                            {
                                string candidate = m.Groups[1].Value.Replace("\\\\", "\\");
                                if (IsValidGameDirectory(candidate))
                                {
                                    path = candidate;
                                    detectedPlatform = "Epic Games (Manifest)";
                                    break;
                                }
                            }
                        }
                    }
                }
                catch { }
            }

            // 2. Standart Epic Games Dizinleri
            if (string.IsNullOrEmpty(path))
            {
                string[] epicPaths = new string[]
                {
                    @"C:\Program Files\Epic Games\AmongUs",
                    @"C:\Program Files\Epic Games\Among Us",
                    @"D:\Program Files\Epic Games\AmongUs",
                    @"D:\Epic Games\AmongUs",
                    @"E:\Epic Games\AmongUs",
                    @"F:\Epic Games\AmongUs"
                };

                foreach (string ep in epicPaths)
                {
                    if (IsValidGameDirectory(ep))
                    {
                        path = ep;
                        detectedPlatform = "Epic Games (Standart Dizin)";
                        break;
                    }
                }
            }

            // 3. Steam Dizinleri
            if (string.IsNullOrEmpty(path))
            {
                string[] steamPaths = new string[]
                {
                    @"C:\Program Files (x86)\Steam\steamapps\common\Among Us",
                    @"C:\Program Files\Steam\steamapps\common\Among Us",
                    @"D:\SteamLibrary\steamapps\common\Among Us",
                    @"E:\SteamLibrary\steamapps\common\Among Us"
                };

                foreach (string sp in steamPaths)
                {
                    if (IsValidGameDirectory(sp))
                    {
                        path = sp;
                        detectedPlatform = "Steam (Standart Dizin)";
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(path))
            {
                txtPath.Text = path;
                UpdatePlatformInfo(path);
            }
            else
            {
                lblStatus.Text = "⚠ Oyun klasörü bulunamadı. Lütfen 'Gözat' butonundan Among Us klasörünüzü seçin.";
                lblStatus.ForeColor = Color.FromArgb(255, 180, 80);
                AppendLog("Oyun klasörü otomatik olarak tespit edilemedi.");
                AppendLog("Lütfen 'Gözat...' butonuna tıklayarak Among Us klasörünüzü seçin.");
            }
        }

        private void TxtPath_TextChanged(object sender, EventArgs e)
        {
            UpdatePlatformInfo(txtPath.Text.Trim());
        }

        private void UpdatePlatformInfo(string dir)
        {
            if (IsValidGameDirectory(dir))
            {
                int bitness = GetExecutableBitness(Path.Combine(dir, "Among Us.exe"));
                string bitnessStr = bitness == 64 ? "64-bit" : (bitness == 32 ? "32-bit" : "Bilinmiyor");
                string plat = dir.IndexOf("Epic", StringComparison.OrdinalIgnoreCase) >= 0 ? "Epic Games" :
                             (dir.IndexOf("Steam", StringComparison.OrdinalIgnoreCase) >= 0 ? "Steam" : "Among Us");

                lblStatus.Text = string.Format("✓ Oyun tespit edildi: {0} ({1})", plat, bitnessStr);
                lblStatus.ForeColor = Color.FromArgb(70, 230, 140);
                AppendLog(string.Format("Seçili oyun: {0} [{1}] - {2}", plat, bitnessStr, dir));
            }
            else
            {
                lblStatus.Text = "⚠ Seçilen klasörde Among Us.exe bulunamadı.";
                lblStatus.ForeColor = Color.FromArgb(255, 180, 80);
            }
        }

        private bool IsValidGameDirectory(string dir)
        {
            if (string.IsNullOrEmpty(dir) || !Directory.Exists(dir)) return false;

            return File.Exists(Path.Combine(dir, "Among Us.exe")) ||
                   File.Exists(Path.Combine(dir, "GameAssembly.dll"));
        }

        private int GetExecutableBitness(string exePath)
        {
            try
            {
                if (!File.Exists(exePath)) return 0;
                using (FileStream fs = new FileStream(exePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    byte[] header = new byte[1024];
                    fs.Read(header, 0, header.Length);
                    int peOffset = BitConverter.ToInt32(header, 0x3C);
                    ushort machine = BitConverter.ToUInt16(header, peOffset + 4);
                    if (machine == 0x014C) return 32;
                    if (machine == 0x8664) return 64;
                }
            }
            catch { }
            return 0;
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Lütfen Among Us oyununun kurulu olduğu klasörü seçin:";
                if (Directory.Exists(txtPath.Text))
                {
                    fbd.SelectedPath = txtPath.Text;
                }

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void BtnInstall_Click(object sender, EventArgs e)
        {
            string targetDir = txtPath.Text.Trim();
            if (string.IsNullOrEmpty(targetDir))
            {
                MessageBox.Show("Lütfen önce Among Us oyun klasörünü seçin.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(targetDir))
            {
                MessageBox.Show("Seçilen klasör mevcut değil:\n" + targetDir, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Hedef oyunun 32-bit mi 64-bit mi olduğunu belirle
            int bitness = GetExecutableBitness(Path.Combine(targetDir, "Among Us.exe"));
            string archSubFolder = (bitness == 64) ? "x64" : "x86";

            string baseDataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Dosyalar");
            string sourceDir = Path.Combine(baseDataDir, archSubFolder);

            // Eğer x86 / x64 alt klasörü bulunamazsa doğrudan Dosyalar klasörünü kullan
            if (!Directory.Exists(sourceDir))
            {
                sourceDir = baseDataDir;
            }

            if (!Directory.Exists(Path.Combine(sourceDir, "BepInEx")))
            {
                MessageBox.Show("Gerekli kurulum dosyaları ('Dosyalar\\" + archSubFolder + "') bulunamadı!\nLütfen klasördeki dosyaları silmeden kurucuyu çalıştırın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                btnInstall.Enabled = false;
                progressBar.Visible = true;
                progressBar.Value = 10;
                AppendLog("\n--- KURULUM BAŞLATILDI ---");
                AppendLog("Hedef: " + targetDir);
                AppendLog("Hedef Mimari: " + (bitness == 64 ? "64-bit (x64)" : "32-bit (x86)"));
                AppendLog("Kaynak Paket: " + archSubFolder);

                // Oyun çalışıyor mu kontrol et
                Process[] procs = Process.GetProcessesByName("Among Us");
                if (procs.Length > 0)
                {
                    DialogResult res = MessageBox.Show("Among Us şu anda çalışıyor!\nDosyaların kopyalanabilmesi için oyunu kapatmanız gerekiyor.\n\nOyunu otomatik kapatmak istiyor musunuz?", "Oyun Açık", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        foreach (Process p in procs)
                        {
                            try { p.Kill(); p.WaitForExit(3000); } catch { }
                        }
                        AppendLog("Çalışan Among Us süreci kapatıldı.");
                    }
                    else
                    {
                        AppendLog("Kurulum kullanıcı tarafından iptal edildi (Oyun açık).");
                        btnInstall.Enabled = true;
                        progressBar.Visible = false;
                        return;
                    }
                }

                progressBar.Value = 30;

                // Dosyaları kopyala
                CopyDirectoryRecursive(sourceDir, targetDir);

                progressBar.Value = 100;
                AppendLog("Tüm dosyalar başarıyla kopyalandı!");
                AppendLog("--- KURULUM TAMAMLANDI ---\n");

                lblStatus.Text = "✅ Kurulum tamamlandı! Oyunu açıp DELETE tuşuna basabilirsiniz.";
                lblStatus.ForeColor = Color.FromArgb(70, 230, 140);

                string msg = "⭐ Kurulum Başarıyla Tamamlandı!\n\n" +
                             "Seçilen Platform: " + (bitness == 64 ? "Epic Games / 64-bit" : "Steam / 32-bit") + "\n\n" +
                             "Nasıl Kullanılır?\n" +
                             "1. Epic Games Launcher veya Steam üzerinden Among Us'ı başlatın.\n" +
                             "2. Oyun açıldığında klavyenizdeki [DELETE] tuşuna basın.\n" +
                             "3. 'Tüm Kozmetikleri Aç' menüsü açılacaktır.\n" +
                             "Tüm kostümler, şapkalar, petler ve vizörler anında aktif olacaktır!\n\n" +
                             "İyi oyunlar!";

                MessageBox.Show(msg, "Başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                AppendLog("HATA: " + ex.Message);
                MessageBox.Show("Kurulum sırasında bir hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnInstall.Enabled = true;
            }
        }

        private void CopyDirectoryRecursive(string source, string target)
        {
            Directory.CreateDirectory(target);

            foreach (string file in Directory.GetFiles(source))
            {
                string fileName = Path.GetFileName(file);
                if (fileName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) ||
                    fileName.EndsWith(".pdb", StringComparison.OrdinalIgnoreCase) ||
                    fileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase) ||
                    fileName.EndsWith(".bat", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                string destFile = Path.Combine(target, fileName);
                File.Copy(file, destFile, true);
                AppendLog("Kopyalandı: " + fileName);
            }

            foreach (string dir in Directory.GetDirectories(source))
            {
                string dirName = Path.GetFileName(dir);
                string destDir = Path.Combine(target, dirName);
                CopyDirectoryRecursive(dir, destDir);
            }
        }

        private void BtnOpenFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtPath.Text))
            {
                Process.Start("explorer.exe", txtPath.Text);
            }
            else
            {
                MessageBox.Show("Hedef klasör bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AppendLog(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action<string>(AppendLog), message);
                return;
            }
            txtLog.AppendText(message + Environment.NewLine);
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }
    }
}
