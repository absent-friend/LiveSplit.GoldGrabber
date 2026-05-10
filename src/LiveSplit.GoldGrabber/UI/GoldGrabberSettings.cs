using System.Windows.Forms;
using System.Xml;
using LiveSplit.UI;
using LiveSplit.Web;

namespace LiveSplit.GoldGrabber.UI;

public partial class GoldGrabberSettings : UserControl
{
    public GoldGrabberSettings()
    {
        InitializeComponent();

        textHost.DataBindings.Add(nameof(TextBox.Text), this, nameof(ObsHost), false, DataSourceUpdateMode.OnPropertyChanged);
        textPort.DataBindings.Add(nameof(TextBox.Text), this, nameof(ObsPort), false, DataSourceUpdateMode.OnPropertyChanged);
        textPassword.DataBindings.Add(nameof(TextBox.Text), this, nameof(ObsPassword), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    public string ObsHost { get; set; } = "localhost";
    
    // default port is 4455. ref: https://github.com/obsproject/obs-websocket/blob/master/src/Config.h#L38
    public int ObsPort { get; set; } = 4455;

    public string ObsPassword { get; set; } = "";

    public XmlNode GetSettings(XmlDocument document)
    {
        var parent = document.CreateElement("Settings");
        CreateSettingsNodes(document, parent);
        CredentialManager.WriteCredential("LiveSplit.GoldGrabber.ObsPassword", null, ObsPassword);
        return parent;
    }

    public int GetSettingsHashCode()
    {
        return CreateSettingsNodes(null, null);
    }

    public void SetSettings(XmlNode settings)
    {
        if (settings is not XmlElement element)
            return;

        ObsHost = SettingsHelper.ParseString(element[nameof(ObsHost)], ObsHost);
        ObsPort = SettingsHelper.ParseInt(element[nameof(ObsPort)], ObsPort);
        ObsPassword = CredentialManager.ReadCredential("LiveSplit.GoldGrabber.ObsPassword")?.Password ?? ObsPassword;
    }

    private int CreateSettingsNodes(XmlDocument document, XmlElement parent)
    {
        return SettingsHelper.CreateSetting(document, parent, nameof(ObsHost), ObsHost)
            ^ SettingsHelper.CreateSetting(document, parent, nameof(ObsPort), ObsPort)
            ^ ObsPassword.GetHashCode();
    }
}
