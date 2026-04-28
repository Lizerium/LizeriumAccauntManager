/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 28 апреля 2026 14:26:16
 * Version: 1.0.24
 */

using System;
using System.Text;
using System.Windows.Forms;

using Root.Services;

namespace Root
{
    public partial class IniFileWindow : Form
    {
        public IniFileWindow()
        {
            InitializeComponent();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    IFLDataFileService file = new FLDataFileService(openFileDialog1.FileName, false);
                    // Build the ini file in a string, section by section
                    StringBuilder strToSave = new StringBuilder();
                    foreach (FLDataFileSection section in file.Sections)
                    {
                        strToSave.AppendLine("[" + section.sectionName + "]");

                        foreach (FLDataFileSetting entry in section.settings)
                        {
                            string line = entry.settingName;
                            if (entry.NumValues() > 0)
                            {
                                line += " = ";
                                for (int i = 0; i < entry.NumValues(); i++)
                                {
                                    if (i != 0)
                                        line += ", ";
                                    line += entry.Str(i);
                                }
                            }
                            strToSave.AppendLine(line);
                        }
                        strToSave.AppendLine("");
                    }
                    richTextBoxFileView.Text = strToSave.ToString();
                    this.Text = "INI File View: " + openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    richTextBoxFileView.Text = ex.ToString();
                    this.Text = "INI File View";
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = openFileDialog1.FileName;
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    IFLDataFileService editedFile = new FLDataFileService(Encoding.Default.GetBytes(richTextBoxFileView.Text), saveFileDialog1.FileName, false);
                    editedFile.SaveSettings(saveFileDialog1.FileName, false);
                }
                catch (Exception ex)
                {
                    richTextBoxFileView.Text = ex.ToString();
                }
            }
        }
    }
}
