using System;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class WelcomePage : Form
    {
        public static ResourceManager rm;

        private Int32 m_Counter = 0;
        private String m_AppLanguage = String.Empty;
        private static String m_CurrentAppLanguage = Properties.Settings.Default.ApplicationLanguage;
        public WelcomePage()
        {
            InitializeComponent();

            //TODO get the system's Culture info and apply it to the program to change some default values used by the code, ie MessageBoxButtons.YesNo buttons.

            GetCurrentApplicationLanguage();

            DialogResult dr = MessageBox.Show(rm.GetString("LOC_UpdateUIObjectsText"), rm.GetString("LOC_Warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                AssignTextToUIObjects();
        }

        private void GetCurrentApplicationLanguage()
        {
            switch (m_CurrentAppLanguage)
            {
                case "TR":
                    rm = new ResourceManager("WindowsFormsApp1.Resources.lang_tr", Assembly.GetExecutingAssembly());
                    break;
                case "EN":
                    rm = new ResourceManager("WindowsFormsApp1.Resources.lang_en", Assembly.GetExecutingAssembly());
                    break;
                default:
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            switch (cb.Text)
            {
                case "Türkçe":
                    rm = new ResourceManager("WindowsFormsApp1.Resources.lang_tr", Assembly.GetExecutingAssembly());
                    m_AppLanguage = "TR";
                    break;
                case "English":
                    rm = new ResourceManager("WindowsFormsApp1.Resources.lang_en", Assembly.GetExecutingAssembly());
                    m_AppLanguage = "EN";
                    break;
                default:
                    break;
            }
        }

        private void AssignTextToUIObjects()
        {
            button1.Text = rm.GetString("LOC_Okay");
            button2.Text = rm.GetString("LOC_Cancel");
            button3.Text = rm.GetString("LOC_Increase");
            button4.Text = rm.GetString("LOC_Save");
            button5.Text = rm.GetString("LOC_Refresh");
            label1.Text = String.Format(rm.GetString("LOC_RemainingDays"), m_Counter, "çok");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_Counter++;
            label1.Text = String.Format(rm.GetString("LOC_RemainingDays"), m_Counter, "very");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //if app lang combobox selection is empty, show message and return
            if(String.IsNullOrEmpty(m_AppLanguage))
            {
                string msg = rm.GetString("LOC_AppLanguageNotSelected");
                MessageBox.Show(msg);
                return;
            }

            Properties.Settings.Default.ApplicationLanguage = m_AppLanguage;
            Properties.Settings.Default.Save();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AssignTextToUIObjects();
        }
    }
}


