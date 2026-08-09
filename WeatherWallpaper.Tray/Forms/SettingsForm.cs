using System.Drawing;
using System.Windows.Forms;

namespace WeatherWallpaper.Tray.Forms;

public sealed class SettingsForm : Form
{
    public SettingsForm()
    {
        InitializeWindow();
    }

    private void InitializeWindow()
    {
        Text = "Weather Wallpaper Settingss";

        StartPosition = FormStartPosition.CenterScreen;

        FormBorderStyle = FormBorderStyle.FixedDialog;

        MaximizeBox = false;
        MinimizeBox = false;

        ClientSize = new Size(520, 420);
    }
}