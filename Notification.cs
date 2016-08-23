using System.Windows.Forms;

namespace PlasticNotifier
{
    static class Notification
    {
        static internal void Show(string title, string text)
        {
            var form = new NotificationForm(title, text);

            form.Icon = Resources.App;

            Application.Run(form);
        }
    }
}
