using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win32AuthNative;
using ErrorCode = Win32AuthNative.Win32Authentication.ErrorCode;

namespace LoginExample
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Win32Authentication.AuthenticateUser(tbDomain +"\\" + tbUsername.Text, tbPassword.Text);
            }
            catch (Win32Exception ex)
            {
                switch (ex.NativeErrorCode)
                {
                    case (int)ErrorCode.ERROR_LOGON_FAILURE: // incorrect user name or password
                        break ;
                    case (int)ErrorCode.ERROR_ACCOUNT_RESTRICTION:
                        break;
                    case (int)ErrorCode.ERROR_PASSWORD_EXPIRED:
                        break;
                    case (int)ErrorCode.ERROR_ACCOUNT_DISABLED:
                        break;
                    case (int)ErrorCode.ERROR_PASSWORD_MUST_CHANGE:
                        break;
                    case (int)ErrorCode.ERROR_ACCOUNT_LOCKED_OUT:
                        break;
                    default: // Other
                        break;
                }

                MessageBox.Show(ex.Message, "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            MessageBox.Show("Successfully logged in", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tbUsername.Text = String.Empty;
            tbPassword.Text = String.Empty;

        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            tbUsername.Text = String.Empty;
            tbPassword.Text = String.Empty;
            return;
        }
    }
}
