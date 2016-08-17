using System;
using System.IO;
using System.Diagnostics;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

using MS.WindowsAPICodePack.Internal;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace PlasticNotifier
{
    class Toaster
    {
        internal static void ShowTextToast(string appId, string title, string text)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(
                ToastTemplateType.ToastText02);

            // Fill in the text elements
            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
            stringElements[0].AppendChild(toastXml.CreateTextNode(title));
            stringElements[1].AppendChild(toastXml.CreateTextNode(text));

            // Create the toast and attach event listeners
            ToastNotification toast = new ToastNotification(toastXml);

            ToastEvents events = new ToastEvents();

            toast.Activated += events.ToastActivated;
            toast.Dismissed += events.ToastDismissed;
            toast.Failed += events.ToastFailed;

            ToastNotificationManager.CreateToastNotifier(appId).Show(toast);
        }

        internal static void ShowImageToast(
            string appId,
            string title,
            string text,
            string filePath)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(
                ToastTemplateType.ToastImageAndText02);

            // Fill in the text elements
            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
            stringElements[0].AppendChild(toastXml.CreateTextNode(title));
            stringElements[1].AppendChild(toastXml.CreateTextNode(text));

            // Specify the absolute path to an image
            String imagePath = "file:///" + filePath;
            XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
            imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;

            // Create the toast and attach event listeners
            ToastNotification toast = new ToastNotification(toastXml);

            ToastEvents events = new ToastEvents();

            toast.Activated += events.ToastActivated;
            toast.Dismissed += events.ToastDismissed;
            toast.Failed += events.ToastFailed;

            // Show the toast. Be sure to specify the AppUserModelId on your application's shortcut!
            ToastNotificationManager.CreateToastNotifier(appId).Show(toast);
        }

        class ToastEvents
        {
            internal void ToastActivated(ToastNotification sender, object e)
            {
                Console.WriteLine("User activated the toast");
            }

            internal void ToastDismissed(ToastNotification sender, ToastDismissedEventArgs e)
            {
                string outputText = "";
                switch (e.Reason)
                {
                    case ToastDismissalReason.ApplicationHidden:
                        outputText = "The app hid the toast using ToastNotifier.Hide";
                        break;
                    case ToastDismissalReason.UserCanceled:
                        outputText = "The user dismissed the toast";
                        break;
                    case ToastDismissalReason.TimedOut:
                        outputText = "The toast has timed out";
                        break;
                }

                Console.WriteLine(outputText);
            }

            internal void ToastFailed(ToastNotification sender, ToastFailedEventArgs e)
            {
                Console.WriteLine("The toast encountered an error.");
            }
        }

        internal static class ShortCutCreator
        {
            // In order to display toasts, a desktop application must
            // have a shortcut on the Start menu.
            // Also, an AppUserModelID must be set on that shortcut.
            // The shortcut should be created as part of the installer.
            // The following code shows how to create a shortcut and assign
            // an AppUserModelID using Windows APIs. You must download and
            // include the Windows API Code Pack for Microsoft .NET Framework
            // for this code to function

            internal static bool TryCreateShortcut(string appId, string shortcutName)
            {
                String shortcutPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData) +
                    "\\Microsoft\\Windows\\Start Menu\\Programs\\"+shortcutName+".lnk";

                if (File.Exists(shortcutPath))
                    return true;

                InstallShortcut(shortcutPath, appId);
                return true;
            }

            static void InstallShortcut(string shortcutPath, string appId)
            {
                // Find the path to the current executable
                String exePath = Process.GetCurrentProcess().MainModule.FileName;
                IShellLinkW newShortcut = (IShellLinkW)new CShellLink();

                // Create a shortcut to the exe
                VerifySucceeded(newShortcut.SetPath(exePath));
                VerifySucceeded(newShortcut.SetArguments(""));

                // Open the shortcut property store, set the AppUserModelId property
                IPropertyStore newShortcutProperties = (IPropertyStore)newShortcut;

                using (PropVariant appIdProp = new PropVariant(appId))
                {
                    VerifySucceeded(newShortcutProperties.SetValue(
                        SystemProperties.System.AppUserModel.ID, appIdProp));
                    VerifySucceeded(newShortcutProperties.Commit());
                }

                // Commit the shortcut to disk
                IPersistFile newShortcutSave = (IPersistFile)newShortcut;

                VerifySucceeded(newShortcutSave.Save(shortcutPath, true));
            }

            static void VerifySucceeded(UInt32 hresult)
            {
                if (hresult <= 1)
                    return;
                throw new Exception("Failed with HRESULT: " + hresult.ToString("X"));
            }
        }
    }
}
