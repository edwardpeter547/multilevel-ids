using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

namespace TaskWork1
{
    public partial class CreateUser : Form
    {
        public CreateUser()
        {
            InitializeComponent();
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            
            foreach (var c in this.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
            }
            txtcloudid.Focus();
        }

        private void btncreate_Click(object sender, EventArgs e)
        {
            var password_field = txtpassword.Text;
            var confirm_password = txtconfirmpassword.Text;
            var email_address = txtemail.Text;
            validate_compulsory_fields(get_text_fields(this));
            if (!(validate_email_address(email_address)))
            {
                MessageBox.Show($"{email_address} is not a valid email address.", "Invalid Email Address", MessageBoxButtons.RetryCancel);
                return;
            }
            if(password_field != confirm_password)
            {
                MessageBox.Show("Passwords do not match!", "Password Mismatch", MessageBoxButtons.RetryCancel);
                txtconfirmpassword.Clear();
                txtconfirmpassword.Focus();
                return;
            }
            UserModel user = new UserModel();
            user.FirstName = txtfirstname.Text;
            user.LastName = txtlastname.Text;
            user.Email = txtemail.Text;
            user.Username = txtcloudid.Text;
            user.Password = EasyEncryption.MD5.ComputeMD5Hash(password_field);
            DataAccessLayer.CreateUser(user);
            MessageBox.Show($"Cloud Account Created for: {user.DisplayName} click Ok to proceed to login", "Account Creation Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void validate_compulsory_fields(List<TextBox> fields)
        {
            var SortedFields = fields.OrderBy(f => f.TabIndex);
            foreach (var Field in SortedFields)
            {
                if (Field.Text == string.Empty)
                {
                    MessageBox.Show($"{Field.Tag} cannot be empty!", "compulsory field", MessageBoxButtons.OK);
                    Field.Focus();
                    break;
                }
            }
        }

        private List<TextBox> get_text_fields(Form form)
        {
            var Fields = new List<TextBox>();

            foreach (var c in form.Controls)
            {
                if (c is TextBox)
                {
                    Fields.Add(((TextBox)c));
                }
            }
            return Fields;
        }

        private bool validate_email_address(string emailaddress)
        {
            try
            {
                MailAddress address = new MailAddress(emailaddress);
                return true;
            }
            catch(FormatException)
            {
                return false;
            }
        }
    }
}
