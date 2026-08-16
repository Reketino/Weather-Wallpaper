using System.Drawing;
using System.Windows.Forms;
using WeatherWallpaper.Core;

namespace WeatherWallpaper.Tray.Forms;

public sealed class SettingsForm : Form
{
    private readonly AppSettings _config;

    private readonly NumericUpDown _updateInterval;
    public SettingsForm(AppSettings config)
    {
        _config = config;
        _updateInterval = new NumericUpDown();

        InitializeWindow();
        
    }

    private void InitializeWindow()
    {
        Text = "Weather Wallpaper Settings";

        StartPosition = FormStartPosition.CenterScreen;

        FormBorderStyle = FormBorderStyle.FixedDialog;

        MaximizeBox = false;
        MinimizeBox = false;

        ClientSize = new Size(520, 420);

        Font = new Font("Segoe UI", 10);
    }
}