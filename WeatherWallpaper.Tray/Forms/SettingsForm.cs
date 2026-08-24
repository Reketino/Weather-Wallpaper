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
        InitializeControls();
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

    private void InitializeControls()
    {
        var updateIntervalLabel = new Label
        {
            Text = "Update interval",
            AutoSize = true,
            Location = new Point(30, 30)
        };

        _updateIntervalInput.Minimum = 1;
        _updateIntervalInput.Maximum = 1440;
        _updateIntervalInput.Value = config.Wallpaper.UpdateIntervalMinutes;
        _updateIntervalInput.Location = new Point(30, 60);
        
        var minutesLabel = new Label
        {
            Text="minutes",
            AutoSize = true,
            
        };
    }
}