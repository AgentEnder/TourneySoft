using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TourneySoft
{
    public partial class mainWindow : Form
    {
        public mainWindow()
        {
            Global.currentTournament = new Tournament();
            InitializeComponent();
            Workspace.Controls.Add(new NewTournamentControl()); //Default to the new tournament screen
        }
        /// <summary>
        /// Load player edit screen
        /// </summary>
        private void playersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Workspace.Controls.Clear(); //Clear workspace
            Workspace.Controls.Add(new EditPlayersControl()); //Open player screen
            setActiveTab(); //Update tabs
        }

        /// <summary>
        /// Set tab highlighting to indicate active section
        /// </summary>
        private void setActiveTab()
        {
            Control childControl = Workspace.Controls[0]; //First control in active workspace
            if (childControl.GetType() == typeof(NewTournamentControl) || childControl.GetType() == typeof(PairingView) || childControl.GetType() == typeof(tournamentResults)) //If workspace is working on active tournament
            {
                tournamentToolStripMenuItem.BackColor = Color.Gray; //Set active color
                playersToolStripMenuItem.BackColor = Control.DefaultBackColor; //Set default
            }else if(childControl.GetType() == typeof(EditPlayersControl)) //Player tab is open
            {
                playersToolStripMenuItem.BackColor = Color.Gray; //Set active
                tournamentToolStripMenuItem.BackColor = Control.DefaultBackColor; //Set default
            }
        }

        /// <summary>
        /// Load active tournament screen
        /// </summary>
        private void tournamentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Workspace.Controls.Clear(); //Clear workspace
            if (!Global.currentTournament.isActive) //New tournament screen
            {
                if (Global.currentTournament.isOver)
                {
                    Workspace.Controls.Add(new tournamentResults());
                }else
                {
                    Workspace.Controls.Add(new NewTournamentControl());
                }
            }
            else
            {
                switch (Global.currentTournament.tourneyType) //For future implementations
                {
                    case "semiswiss":
                        {
                            Workspace.Controls.Add(new PairingView());
                        }break;
                    default:
                        {
                            Exception ex = new Exception("Invalid tourney type fed to mainWindow.cs");
                        }break;
                }
            }
            setActiveTab();
        }

        /// <summary>
        /// Update the workspace tabs if they change
        /// </summary>
        private void Workspace_ControlRemoved(object sender, ControlEventArgs e) //Update active tab
        {
            if(Workspace.Controls.Count == 1) //Only if there is still a control
            {
                setActiveTab();
            }
        }

        /// <summary>
        /// Update the workspace tabs if they change
        /// </summary>
        private void Workspace_ControlAdded(object sender, ControlEventArgs e)
        {
            setActiveTab(); //New active tab
        }
    }
}
