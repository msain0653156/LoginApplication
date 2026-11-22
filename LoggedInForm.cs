using System.Drawing;
using System.Windows.Forms;

namespace LoginApplication
{
    public class LoggedInForm : Form
    {
        public LoggedInForm(string username)
        {
            Text = "Logged In";
            ClientSize = new Size(400, 200);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Font = new Font("Segoe UI", 10);

            Label message = new Label();
            message.Text = "Welcome, " + username + "!\nYou have successfully logged in.";
            message.TextAlign = ContentAlignment.MiddleCenter;
            message.SetBounds(20, 25, 360, 90);
            Controls.Add(message);

            Button btnLogout = new Button();
            btnLogout.Text = "Logout";
            btnLogout.SetBounds(65, 135, 125, 35);
            btnLogout.DialogResult = DialogResult.OK;
            Controls.Add(btnLogout);

            Button btnExit = new Button();
            btnExit.Text = "Exit";
            btnExit.SetBounds(210, 135, 125, 35);
            btnExit.DialogResult = DialogResult.Cancel;
            Controls.Add(btnExit);

            CancelButton = btnExit;
        }
    }
}
