using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

using GodAndItemController;
using DankSmite.Properties;
using System.Reflection;

namespace DankSmite
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            WelcomeCutton_Click(null, null);
        }

        int RerollAmount = 0;

        public bool CredsFlag = false;
        public bool HelpFlag = false;



        static Item[] _build = new Item[9] {
            new Item("null","null",false, false, false),
            new Item("null","null",false, false, false),
            new Item("null","null",false, false, false),
            new Item("null","null",false, false, false),
            new Item("null","null",false, false, false),
            new Item("null","null",false, false, false),
            new Item("null","null",false, false, false),
            new Item("null","null",false, false, false),
            new Item("null","null",false, false, false)
        };

        static God _god = new God("nope", "nope");
        static List<God> legalGods;
        static List<Item> legalItems;
        static List<Item> legalBoots;
        static List<Item> legalRelics;
        static List<Item> legalStarters;
        static GodCollector godCol = new GodCollector();
        static ItemCollector itemCol = new ItemCollector();
        private Random rnd = new Random();


        public static Item[] Build
        {
            get { return _build; }
            set { _build = value; }
        }
        public static God God
        {
            get { return _god; }
            set { _god = value; }
        }

        public bool first = true;
        public bool clickedRebuild = false;

        

        private void BuildButton(object sender, EventArgs e)
        {
            //Setup for next roll
            this.ErrorTextBox.Visible = false;
            this.ErrorTextBox.Text = "=================\nERROR\n=================\n";
            this.GeneralTextBox.Visible = false;
            this.CurrentItemNameLabel.Text = "";
            RerollAmount = 0;

            //Validation of proper input
            if (!((this.AssassinCheckBox.Checked == false
                && this.GuardianCheckBox.Checked == false
                && this.HunterCheckBox.Checked == false
                && this.MageCheckBox.Checked == false
                && this.WarriorCheckBox.Checked == false)
                || (this.DamageCheckBox.Checked == false
                && this.DefenseCheckBox.Checked == false
                && this.HybridCheckBox.Checked == false)))
            {
                //Sets labels
                this.StarterItemLabel.Visible = true;
                this.RelicLabel1.Visible = true;
                this.RelicLabel2.Visible = true;
                this.ItemLabel1.Visible = true;
                this.ItemLabel2.Visible = true;
                this.ItemLabel3.Visible = true;
                this.ItemLabel4.Visible = true;
                this.ItemLabel5.Visible = true;
                this.ItemLabel6.Visible = true;
                clickedRebuild = true;




                //Some dankness


                if (string.IsNullOrEmpty(this.TextInput.Text))
                {
                    fullRebuild(
                    this.GuardianCheckBox.Checked,
                    this.MageCheckBox.Checked,
                    this.HunterCheckBox.Checked,
                    this.AssassinCheckBox.Checked,
                    this.WarriorCheckBox.Checked,
                    this.DamageCheckBox.Checked,
                    this.HybridCheckBox.Checked,
                    this.DefenseCheckBox.Checked
                    );
                }
                else if (this.TextInput.Text == "op")
                {

                    _god = new God("Ymir", "g");
                    for (int i = 0; i < 6; i++)
                    {
                        Build[i] = new Item("Deathbringer", "dmg", false, true, false);
                    }
                    Build[6] = new Item("Blink_Rune", "dmg", true, true, false);
                    Build[7] = new Item("Blink_Rune", "dmg", true, true, false);
                    Build[8] = new Item("Deathbringer", "dmg", false, true, false);
                }
                else
                {
                    fullRebuild(
                    this.TextInput.Text,
                    this.DamageCheckBox.Checked,
                    this.HybridCheckBox.Checked,
                    this.DefenseCheckBox.Checked
                    );
                }
                Draw();
            }
            else
            {
                _god = new God("Ymir", "g");
                for (int i = 0; i < 6; i++)
                {
                    Build[i] = new Item("Deathbringer", "dmg", false, true, false);
                }
                Build[6] = new Item("Blink_Rune", "dmg", true, true, false);
                Build[7] = new Item("Blink_Rune", "dmg", true, true, false);
                Build[8] = new Item("Deathbringer", "dmg", false, true, false);
                Draw();
            }

        }
        

        private void TextInput_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                BuildButton(null, null);
                e.SuppressKeyPress = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        #region HelpAndCredits
        private void CreditsButon_Click(object sender, EventArgs e)
        {
            if (CredsFlag == false)
            {
                GeneralTextBox.Text = "Created by: Trickermand, Nazzyc, Kloppermand.\n\nSmite is property of Hi-Rez studio" +
                                    ". All pictures are created and owned by Hi-Rez Studio";
                CredsFlag = true;
                HelpFlag = false;
                this.GeneralTextBox.Visible = true;
            }
            else
            {
                CredsFlag = false;
                HelpFlag = false;
                this.GeneralTextBox.Visible = false;
            }
        }
        

        private void WelcomeCutton_Click(object sender, EventArgs e)
        {
            if (HelpFlag == false)
            {
                GeneralTextBox.Text = "Welcome to DankSmite!\nClick the Get Me A Build button to get a god and a build! C" +
                                    "lick the pictures to reroll them, should you want to do so!\n\n\nWorks for smite ve" +
                                    "rsion: 4.21\nDanksmite version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
                CredsFlag = false;
                HelpFlag = true;
                this.GeneralTextBox.Visible = true;
            }
            else
            {
                CredsFlag = false;
                HelpFlag = false;
                this.GeneralTextBox.Visible = false;
            }
        }
        #endregion

        #region Rerolls

        private void godPictureBox_Click(object sender, EventArgs e)
        {
            if (clickedRebuild)
            {
                rerollGod();
                this.GodPicture.Image = (Image)Resources.ResourceManager.GetObject(_god.name);
                this.GodLabel.Text = _god.name.Replace('_', ' ');
                RerollAmount++;
                this.RerollNumber.Text = RerollAmount.ToString();
            }
        }

        private void StarterItemPicture_Click(object sender, EventArgs e)
        {
            if (clickedRebuild)
            {
                rerollItem(8);
                this.StarterItemPicture.Image = (Image)Resources.ResourceManager.GetObject(_build[8].name);
                this.CurrentItemNameLabel.Text = Build[8].name.Replace('_', ' ');
                RerollAmount++;
                this.RerollNumber.Text = RerollAmount.ToString();
            }
        }

        private void ItemPicture1_Click(object sender, EventArgs e)
        {
            if (clickedRebuild)
            {
                rerollItem(0);
                this.ItemPicture1.Image = (Image)Resources.ResourceManager.GetObject(Build[0].name);
                this.CurrentItemNameLabel.Text = Build[0].name.Replace('_', ' ');
                RerollAmount++;
                this.RerollNumber.Text = RerollAmount.ToString();
            }
        }

        private void ItemPicture2_Click(object sender, EventArgs e)
        {
            if (clickedRebuild)
            {
                rerollItem(1);
                this.ItemPicture2.Image = (Image)Resources.ResourceManager.GetObject(Build[1].name);
                this.CurrentItemNameLabel.Text = Build[1].name.Replace('_', ' ');
                RerollAmount++;
                this.RerollNumber.Text = RerollAmount.ToString();
            }
        }

        private void ItemPicture3_Click(object sender, EventArgs e)
        {
            if (clickedRebuild)
            {
                rerollItem(2);
                this.ItemPicture3.Image = (Image)Resources.ResourceManager.GetObject(Build[2].name);
                this.CurrentItemNameLabel.Text = Build[2].name.Replace('_', ' ');
                RerollAmount++;
                this.RerollNumber.Text = RerollAmount.ToString();
            }
        }

        private void ItemPicture4_Click(object sender, EventArgs e)
        {
            if (clickedRebuild)
            {
                rerollItem(3);
                this.ItemPicture4.Image = (Image)Resources.ResourceManager.GetObject(Build[3].name);
                this.CurrentItemNameLabel.Text = Build[3].name.Replace('_', ' ');
                RerollAmount++;
                this.RerollNumber.Text = RerollAmount.ToString();
            }
        }

        private void ItemPicture5_Click(object sender, EventArgs e)
        {
            if (clickedRebuild)
            {
                rerollItem(4);
                this.ItemPicture5.Image = (Image)Resources.ResourceManager.GetObject(Build[4].name);
                this.CurrentItemNameLabel.Text = Build[4].name.Replace('_', ' ');
                RerollAmount++;
                this.RerollNumber.Text = RerollAmount.ToString();
            }
        }

        private void ItemPicture6_Click(object sender, EventArgs e)
        {
            if (clickedRebuild)
            {
                rerollItem(5);
                this.ItemPicture6.Image = (Image)Resources.ResourceManager.GetObject(Build[5].name);
                this.CurrentItemNameLabel.Text = Build[5].name.Replace('_', ' ');
                RerollAmount++;
                this.RerollNumber.Text = RerollAmount.ToString();
            }
        }

        private void RelicPicture1_Click(object sender, EventArgs e)
        {
            if (clickedRebuild)
            {
                rerollItem(6);
                this.Relic1.Image = (Image)Resources.ResourceManager.GetObject(Build[6].name);
                this.CurrentItemNameLabel.Text = Build[6].name.Replace('_', ' ');
                RerollAmount++;
                this.RerollNumber.Text = RerollAmount.ToString();
            }
        }

        private void RelicPicture2_Click(object sender, EventArgs e)
        {
            if (clickedRebuild)
            {
                rerollItem(7);
                this.Relic2.Image = (Image)Resources.ResourceManager.GetObject(Build[7].name);
                this.CurrentItemNameLabel.Text = Build[7].name.Replace('_', ' ');
                RerollAmount++;
                this.RerollNumber.Text = RerollAmount.ToString();
            }
        }

        #endregion
        
        #region DrawingSection
        private void Draw()
        {
            this.GodPicture.Image = (Image)Resources.ResourceManager.GetObject(_god.name);
            this.GodLabel.Text = _god.name.Replace("_"," ");
            this.RerollNumber.Text = RerollAmount.ToString();

            this.ItemPicture1.Image = (Image)Resources.ResourceManager.GetObject(Build[0].name);
            this.ItemPicture2.Image = (Image)Resources.ResourceManager.GetObject(Build[1].name);
            this.ItemPicture3.Image = (Image)Resources.ResourceManager.GetObject(Build[2].name);
            this.ItemPicture4.Image = (Image)Resources.ResourceManager.GetObject(Build[3].name);
            this.ItemPicture5.Image = (Image)Resources.ResourceManager.GetObject(Build[4].name);
            this.ItemPicture6.Image = (Image)Resources.ResourceManager.GetObject(Build[5].name);

            this.Relic1.Image = (Image)Resources.ResourceManager.GetObject(Build[6].name);
            this.Relic2.Image = (Image)Resources.ResourceManager.GetObject(Build[7].name);

            this.StarterItemPicture.Image = (Image)Resources.ResourceManager.GetObject(Build[8].name);
        }
        
        private void pictureBox1_Hover(object sender, EventArgs e)
        {
            this.CurrentItemNameLabel.Text = Build[0].name.Replace('_', ' ');
        }

        private void pictureBox2_Hover(object sender, EventArgs e)
        {
            this.CurrentItemNameLabel.Text = Build[1].name.Replace('_', ' ');
        }

        private void pictureBox3_Hover(object sender, EventArgs e)
        {
            this.CurrentItemNameLabel.Text = Build[2].name.Replace('_', ' ');
        }

        private void pictureBox4_Hover(object sender, EventArgs e)
        {
            this.CurrentItemNameLabel.Text = Build[3].name.Replace('_', ' ');
        }

        private void pictureBox5_Hover(object sender, EventArgs e)
        {
            this.CurrentItemNameLabel.Text = Build[4].name.Replace('_', ' ');
        }

        private void pictureBox6_Hover(object sender, EventArgs e)
        {
            this.CurrentItemNameLabel.Text = Build[5].name.Replace('_', ' ');
        }

        private void Relic1_Hover(object sender, EventArgs e)
        {
            this.CurrentItemNameLabel.Text = Build[6].name.Replace('_', ' ');
        }

        private void Relic2_Hover(object sender, EventArgs e)
        {
            this.CurrentItemNameLabel.Text = Build[7].name.Replace('_', ' ');
        }

        private void StarterItemPicture_Hover(object sender, EventArgs e)
        {
            this.CurrentItemNameLabel.Text = Build[8].name.Replace('_', ' ');
        }

        private void ClearLabels(object sender, EventArgs e)
        {
            this.CurrentItemNameLabel.Text = "";
        }



        #endregion

        private void TextInput_Click(object sender, EventArgs e)
        {
            if (first)
            {
                this.TextInput.Text = "";
                this.TextInput.ForeColor = System.Drawing.SystemColors.ControlText;
                first = false;                   
            }
        }


        #region BodySection

        //Checkboxes with gods and itemcolours, sets _build to a full build, and _god to a god
        public void fullRebuild(bool g, bool m, bool h, bool a, bool w, bool dmg, bool hyb, bool def)
        {
            //Choose god
            int choice;
            legalGods = godCol.retrieveLegalList(g, m, h, a, w);

            choice = rnd.Next(legalGods.Count);
            God = legalGods[choice];

            legalBoots = itemCol.retrieveLegalBoots(dmg, hyb, def, God.type);
            legalItems = itemCol.retrieveLegalItems(dmg, hyb, def, God.type);
            legalRelics = itemCol.retrieveLegalRelics(dmg, hyb, def, God.type);
            legalStarters = itemCol.retrieveLegalStarters(dmg, hyb, def, God.type);

            for (int i = 0; i < 9; i++)
            {
                rerollItem(i);
            }
            
        }

        public void fullRebuild(string potGod, bool dmg, bool hyb, bool def)
        {
            bool foundGod = false;
            List<string> ls = new List<string>();
            foreach (God gd in godCol.retrieveLegalList(true, true, true, true, true))
            {
                if (gd.name.ToLower().Equals(this.TextInput.Text.ToLower().Replace(" ", "_")))
                {
                    foundGod = true;
                    _god = gd;
                    if (_god.type.Equals("g"))
                    {
                        legalGods = godCol.retrieveLegalList(true, false, false, false, false);
                    }
                    else if (_god.type.Equals("m"))
                    {
                        legalGods = godCol.retrieveLegalList(false, true, false, false, false);
                    }
                    else if (_god.type.Equals("h"))
                    {
                        legalGods = godCol.retrieveLegalList(false, false, true, false, false);
                    }
                    else if (_god.type.Equals("a"))
                    {
                        legalGods = godCol.retrieveLegalList(false, false, false, true, false);
                    }
                    else if (_god.type.Equals("w"))
                    {
                        legalGods = godCol.retrieveLegalList(false, false, false, false, true);
                    }

                    legalBoots = itemCol.retrieveLegalBoots(dmg, hyb, def, God.type);
                    legalItems = itemCol.retrieveLegalItems(dmg, hyb, def, God.type);
                    legalRelics = itemCol.retrieveLegalRelics(dmg, hyb, def, God.type);
                    legalStarters = itemCol.retrieveLegalStarters(dmg, hyb, def, God.type);

                    for (int i = 0; i < 9; i++)
                    {
                        rerollItem(i);
                    }
                }

                if (!foundGod)
                {
                    fullRebuild(
                        this.GuardianCheckBox.Checked,
                        this.MageCheckBox.Checked,
                        this.HunterCheckBox.Checked,
                        this.AssassinCheckBox.Checked,
                        this.WarriorCheckBox.Checked,
                        this.DamageCheckBox.Checked,
                        this.HybridCheckBox.Checked,
                        this.DefenseCheckBox.Checked
                    );
                }
            }
        }


        //Takes an int itemNumber:
        //0: Boots
        //1-5: Items
        //6-7: Relics
        //8: Starter
        public void rerollItem(int itemNumber)
        {
            int choice;
            Item itemCandidate;

            //Boots
            if (itemNumber == 0)
            {
                do
                {
                    choice = rnd.Next(legalBoots.Count);
                    itemCandidate = legalBoots[choice];
                } while (itemInBuild(itemCandidate));
            }

            //Normal items
            else if (1 <= itemNumber && itemNumber < 6)
            {
                do
                {
                    choice = rnd.Next(legalItems.Count);
                    itemCandidate = legalItems[choice];
                } while (itemInBuild(itemCandidate) || (itemCandidate.isKatana && God.type.Equals("h")));
            }

            //Relics
            else if (6 <= itemNumber && itemNumber < 8)
            {
                do
                {
                    choice = rnd.Next(legalRelics.Count);
                    itemCandidate = legalRelics[choice];
                } while (itemInBuild(itemCandidate));
            }

            //Fallback on starter item
            else
            {
                do
                {
                    choice = rnd.Next(legalStarters.Count);
                    itemCandidate = legalStarters[choice];
                } while (itemInBuild(itemCandidate));
            }

            Build[itemNumber] = itemCandidate;
        }

        //Rerolls the god, taking into account whether or not Katanas are in the tree.
        public void rerollGod()
        {
            bool isMag = false;
            if (God.type.Equals("g") || God.type.Equals("m"))
            {
                isMag = true;
            }

            bool hasKatana = buildContainsKatana();
            int choice;
            God godCandidate;

            if (isMag)
            {
                do
                {
                    choice = rnd.Next(legalGods.Count);
                    godCandidate = legalGods[choice];
                } while (
                            !godCandidate.type.Equals("m") 
                            && !godCandidate.type.Equals("g")
                            || godCandidate.name.Equals(God.name));

            }
            else
            {
                if (hasKatana)
                {
                    do
                    {
                        choice = rnd.Next(legalGods.Count);
                        godCandidate = legalGods[choice];
                    } while (
                               !godCandidate.type.Equals("a")
                                && !godCandidate.type.Equals("w")
                            || godCandidate.name.Equals(God.name)
                                );
                }
                else
                {
                    do
                    {
                        choice = rnd.Next(legalGods.Count);
                        godCandidate = legalGods[choice];
                    } while (
                               (!godCandidate.type.Equals("h")
                                && !godCandidate.type.Equals("a")
                                && !godCandidate.type.Equals("w"))
                            || godCandidate.name.Equals(God.name)
                                );
                }
            }
            God = godCandidate;
        }


        public bool itemInBuild(Item item)
        {

            foreach (Item builditem in Build)
            {
                if (builditem != null && builditem.name.Equals(item.name))
                {
                    return true;
                }
            }

            return false;

        }

        public bool buildContainsKatana()
        {
            foreach (Item item in Build)
            {
                if (item.isKatana)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion


        #region MajorLabels
        private void ItemIntensityLabel_Click(object sender, EventArgs e)
        {
            if (!this.DamageCheckBox.Checked || !this.HybridCheckBox.Checked || !this.DefenseCheckBox.Checked)
            {
                this.DamageCheckBox.Checked = true;
                this.HybridCheckBox.Checked = true;
                this.DefenseCheckBox.Checked = true;
            }
            else
            {
                this.DamageCheckBox.Checked = false;
                this.HybridCheckBox.Checked = false;
                this.DefenseCheckBox.Checked = false;
            }
        }

        private void PhysicalLabel_Click(object sender, EventArgs e)
        {
            if (!this.WarriorCheckBox.Checked || !this.HunterCheckBox.Checked || !this.AssassinCheckBox.Checked)
            {
                this.WarriorCheckBox.Checked = true;
                this.HunterCheckBox.Checked = true;
                this.AssassinCheckBox.Checked = true;
            }
            else
            {
                this.HunterCheckBox.Checked = false;
                this.WarriorCheckBox.Checked = false;
                this.AssassinCheckBox.Checked = false;
            }
        }

        private void MagicalLabel_Click(object sender, EventArgs e)
        {
            if (!this.GuardianCheckBox.Checked || !this.MageCheckBox.Checked)
            {
                this.GuardianCheckBox.Checked = true;
                this.MageCheckBox.Checked = true;
            }
            else
            {
                this.GuardianCheckBox.Checked = false;
                this.MageCheckBox.Checked = false;
            }
        }
        #endregion

        private void SpecificsButton_Click(object sender, EventArgs e)
        {

        }
    }
}
