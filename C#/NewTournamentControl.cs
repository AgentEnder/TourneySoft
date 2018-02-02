using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TourneySoft
{
    public partial class NewTournamentControl : UserControl
    {
        public NewTournamentControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Start the tournament based on control values
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null || numericUpDown1.Value == 0) //Dont do anything if there is no tourney type selected
            {
                return;
            }
            switch (comboBox2.SelectedItem.ToString())
            {
                case "SemiSwiss": //SemiSwiss selected
                    {
                        /*Global.currentTournament.tourneyType = "semiswiss"; //Assign tourney type
                        Global.currentTournament.isActive = true; //Set tourney to active*/
                        SemiSwiss tourney = new SemiSwiss(Global.currentTournament.players, (int)numericUpDown1.Value);
                        Global.currentTournament = tourney;
                        Global.currentTournament.isActive = true;
                        if (this.Parent.Parent == null || this.Parent.Parent.GetType() != typeof(mainWindow))
                            return; //Future proofing, dont do work to parent if usercontrol is not initialized in main window
                        this.Parent.Controls.Add(new PairingView()); //Add tourney to workspace
                        this.Parent.Controls.Remove(this); //Remove this from workspace
                    }
                    break;
                case "Swiss": //SemiSwiss selected
                    {
                        Swiss tourney = new Swiss(Global.currentTournament.players, (int)numericUpDown1.Value);
                        Global.currentTournament = tourney;
                        Global.currentTournament.isActive = true;
                        if (this.Parent.Parent == null || this.Parent.Parent.GetType() != typeof(mainWindow))
                            return; //Future proofing, dont do work to parent if usercontrol is not initialized in main window
                        this.Parent.Controls.Add(new PairingView()); //Add tourney to workspace
                        this.Parent.Controls.Remove(this); //Remove this from workspace
                    }
                    break;
                default:
                    {
                        Exception ex = new Exception("Invalid combobox selection in NewTournamentControl");
                        throw (ex); //Invalid combobox selection
                    }
            }
        }

        /// <summary>
        /// Load function, sets dockstyle and typespecifics if type is chosen
        /// </summary>
        private void NewTournamentControl_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill; //Fill parent
            if(comboBox2.SelectedItem != null) { setTypeSpecifics(); }
        }

        /// <summary>
        /// Open the edit player dialog
        /// </summary>
        private void EditPlayerButton_Click(object sender, EventArgs e)
        {
            if (this.Parent == null || this.Parent.GetType() != typeof(Panel))
            {
                return; //Future proofing
            }
            this.Parent.Controls.Add(new EditPlayersControl());//Add editplayer dialog
            this.Parent.Controls.Remove(this); //Remove this.
        }

        /// <summary>
        /// Set default values for a specific tournament type
        /// </summary>
        private void setTypeSpecifics()
        {
            switch (comboBox2.SelectedItem.ToString())
            {
                case "SemiSwiss":
                    {
                        textBox1.Text = SemiSwiss.description;
                        numericUpDown1.Value = SemiSwiss.getLengthReccomendation();
                    }
                    break;
            }
        }

        /// <summary>
        /// Update the type specifics any time the combobox is changed
        /// </summary>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            setTypeSpecifics();
        }
    }
}
