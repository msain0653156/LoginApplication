using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace LoginApplication
{
    public class LoginForm : Form
    {
        private TextBox txtLoginUsername = new TextBox();
        private TextBox txtLoginPassword = new TextBox();
        private TextBox txtNewUsername = new TextBox();
        private TextBox txtNewPassword = new TextBox();
        private TextBox txtConfirmPassword = new TextBox();
        private UserValidator validator = new UserValidator();
        private UserDatabase database = new UserDatabase();

        public LoginForm()
        {
            Text = "User Login";
            ClientSize = new Size(420, 430);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Font = new Font("Segoe UI", 10);

            GroupBox loginBox = new GroupBox();
            loginBox.Text = "Login";
            loginBox.SetBounds(20, 15, 380, 155);
            Controls.Add(loginBox);

            AddField(loginBox, "Username", txtLoginUsername, 30, 50, false);
            AddField(loginBox, "Password", txtLoginPassword, 65, 128, true);

            Button btnLogin = new Button();
            btnLogin.Text = "Login";
            btnLogin.SetBounds(140, 105, 120, 32);
            btnLogin.Click += LoginClick;
            loginBox.Controls.Add(btnLogin);
            AcceptButton = btnLogin;

            GroupBox createBox = new GroupBox();
            createBox.Text = "Create Account";
            createBox.SetBounds(20, 190, 380, 215);
            Controls.Add(createBox);

            AddField(createBox, "Username", txtNewUsername, 32, 50, false);
            AddField(createBox, "Password", txtNewPassword, 70, 128, true);
            AddField(createBox, "Confirm Password", txtConfirmPassword, 108, 128, true);

            Label hint = new Label();
            hint.Text = "Password must have at least 8 characters.";
            hint.SetBounds(15, 145, 350, 22);
            createBox.Controls.Add(hint);

            Button btnCreate = new Button();
            btnCreate.Text = "Create User";
            btnCreate.SetBounds(140, 173, 120, 30);
            btnCreate.Click += CreateUserClick;
            createBox.Controls.Add(btnCreate);
        }

        private void AddField(GroupBox box, string labelText, TextBox textBox, int y, int maxLength, bool password)
        {
            Label label = new Label();
            label.Text = labelText;
            label.SetBounds(15, y + 3, 140, 25);
            textBox.SetBounds(160, y, 200, 28);
            textBox.MaxLength = maxLength;
            textBox.UseSystemPasswordChar = password;
            box.Controls.Add(label);
            box.Controls.Add(textBox);
        }

        private void LoginClick(object sender, EventArgs e)
        {
            string username = txtLoginUsername.Text.Trim();
            string password = txtLoginPassword.Text;
            string error = validator.ValidateLogin(username, password);

            if (error != "")
            {
                MessageBox.Show(error);
                return;
            }

            try
            {
                if (!database.Login(username, password))
                {
                    MessageBox.Show("Incorrect username or password.");
                    return;
                }

                txtLoginPassword.Clear();
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                Hide();

                using (LoggedInForm form = new LoggedInForm(username))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        txtLoginUsername.Clear();
                        Show();
                        txtLoginUsername.Focus();
                    }
                    else
                        Close();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Could not access the database. Check your SQL Server connection and run Database.sql.");
            }
        }

        private void CreateUserClick(object sender, EventArgs e)
        {
            string username = txtNewUsername.Text.Trim();
            string password = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string error = validator.ValidateNewUser(username, password, confirmPassword);

            if (error != "")
            {
                MessageBox.Show(error);
                return;
            }

            try
            {
                if (!database.CreateUser(username, password))
                {
                    MessageBox.Show("Username already exists. Choose a different username.");
                    return;
                }

                MessageBox.Show("Account successfully created!");
                txtNewUsername.Clear();
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                txtLoginUsername.Text = username;
                txtLoginPassword.Clear();
                txtLoginPassword.Focus();
            }
            catch (SqlException)
            {
                MessageBox.Show("Could not access the database. Check your SQL Server connection and run Database.sql.");
            }
        }
    }
}
