﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using gaiController;
using System.Reflection;
using DankSmite.Properties;

namespace DankSmite
{
    public partial class Form1 : Form
    {
        public List<string> AllActualGods = godCol.retrieveAllGodNames();

        public Form1()
        {
            InitializeComponent();
            WelcomeButton_Click(null, null);
            this.Text += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
            foreach (string GodName in AllActualGods)
            {
                this.DropDownMenu.Items.Add(GodName);
            }
            this.DropDownMenu.SelectedIndex = 0;

        }
        
        int RerollAmount = 0;
        string SmiteVersion = "4.23";

        public bool CredsFlag = false;
        public bool WelcomeFlag = false;
        public bool DetailsFlag = false;
        public bool FirstDropDown = true;
        

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

        #region God Input
        private void TextInput_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                BuildButton(null, null);
                e.SuppressKeyPress = true;
            }
            else
            {
                this.DropDownMenu.SelectedIndex = 0;
            }
        }

        private void TextInput_Click(object sender, EventArgs e)
        {
            if (first)
            {
                this.TextInput.Text = "";
                this.TextInput.ForeColor = System.Drawing.SystemColors.ControlText;
                first = false;                   
            }
            this.DropDownMenu.SelectedIndex = 0;
        }
        
        private void DropDownMenu_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                BuildButton(null, null);
                e.SuppressKeyPress = true;
            }
        }

        public void DropDownMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FirstDropDown)
            {
                FirstDropDown = false;
            }
            else if (this.DropDownMenu.SelectedIndex == 0)
            {
                this.TextInput.Text = "";
            }
            else
            {
                this.TextInput.ForeColor = System.Drawing.SystemColors.ControlText;
                this.TextInput.Text = AllActualGods[this.DropDownMenu.SelectedIndex];
            }
        }
        #endregion

        #region Build button
        private void BuildButton(object sender, EventArgs e)
        {
            //Setup for next roll
            this.GeneralTextBox.Visible = false;
            this.CurrentItemNameLabel.Text = "";
            RerollAmount = 0;
            CredsFlag = false;
            WelcomeFlag = false;
            DetailsFlag = false;

            //Some dankness


            #region Background
            if (this.TextInput.Text.ToLower().Equals("splatt>klopper"))
            {
                this.BackgroundImage = (Image)Resources.ResourceManager.GetObject("SplattGreaterThanKlopper");
            }
            else if (this.TextInput.Text.ToLower().Equals("klopper is shit"))
            {
                this.BackgroundImage = (Image)Resources.ResourceManager.GetObject("KlopperIsShit");
            }
            else if (this.TextInput.Text.ToLower().Equals("ares sucks"))
            {
                this.BackgroundImage = (Image)Resources.ResourceManager.GetObject("AresSucks");
            }
            else if (this.TextInput.Text.ToLower().Equals("bellona is bae"))
            {
                this.BackgroundImage = (Image)Resources.ResourceManager.GetObject("BellonaIsBae");
            }
            else if (this.TextInput.Text.ToLower().Equals("ymir is the best"))
            {
                this.BackgroundImage = (Image)Resources.ResourceManager.GetObject("YmirIsTheBest");
            }
            else if (this.TextInput.Text.ToLower().Equals("standard"))
            {
                this.BackgroundImage = (Image)Resources.ResourceManager.GetObject("NewDankSmiteBrackground");
            }
            #endregion


            #region Dank
            if (this.TextInput.Text == "op")
            {

                _god = new God("Ymir", "g");
                for (int i = 0; i < 6; i++)
                {
                    Build[i] = new Item("Deathbringer", "dmg", false, true, false);
                }
                Build[6] = new Item("Blink_Rune", "dmg", true, true, false);
                Build[7] = new Item("Blink_Rune", "dmg", true, true, false);
                Build[8] = new Item("Deathbringer", "dmg", false, true, false);

                legalBoots = itemCol.retrieveLegalBoots(true, true, true, God.type);
                legalItems = itemCol.retrieveLegalItems(true, true, true, God.type);
                legalRelics = itemCol.retrieveLegalRelics(true, true, true, God.type);
                legalStarters = itemCol.retrieveLegalStarters(true, true, true, God.type);
            }
            else if (this.TextInput.Text.ToLower() == "trickermand")
            {
                _god = new God("Ymir", "g");

                Build[0] = new Item("Shoes_of_the_Magi", "dmg", true, false, false);
                Build[1] = new Item("Dynasty_Plate_Helm", "hyb", true, false, false);
                Build[2] = new Item("BreastPlate_of_Valor", "def", true, true, false);
                Build[3] = new Item("Hide_of_the_Urchin", "def", true, true, false);
                Build[5] = new Item("Shogun's_Kusari", "dmg", true, false, false);
                Build[4] = new Item("Obsidian_Shard", "dmg", true, false, false);

                Build[6] = new Item("Blink_Rune", "dmg", true, true, false);
                Build[7] = new Item("Magic_Shell", "dmg", true, true, false);
                Build[8] = new Item("Mark_of_the_Vanguard", "dmg", false, true, false);

                legalBoots = itemCol.retrieveLegalBoots(true, true, true, God.type);
                legalItems = itemCol.retrieveLegalItems(true, true, true, God.type);
                legalRelics = itemCol.retrieveLegalRelics(true, true, true, God.type);
                legalStarters = itemCol.retrieveLegalStarters(true, true, true, God.type);
            }
            else if (this.TextInput.Text.ToLower() == "nazzyc")
            {
                _god = new God("Medusa", "h");
                for (int i = 0; i < 6; i++)
                {
                    Build[i] = new Item("Odysseus'_Bow", "dmg", false, true, false);
                }
                Build[6] = new Item("Bracer_of_Undoing", "def", true, true, false);
                Build[7] = new Item("Bracer_of_Undoing", "def", true, true, false);
                Build[8] = new Item("Odysseus'_Bow", "dmg", false, true, false);

                legalBoots = itemCol.retrieveLegalBoots(true, true, true, God.type);
                legalItems = itemCol.retrieveLegalItems(true, true, true, God.type);
                legalRelics = itemCol.retrieveLegalRelics(true, true, true, God.type);
                legalStarters = itemCol.retrieveLegalStarters(true, true, true, God.type);
            }
            else if (this.TextInput.Text.ToLower() == "kloppermand")
            {
                _god = new God("Ares", "g");

                Build[0] = new Item("Shoes_of_the_Magi", "dmg", true, false, false);
                Build[1] = new Item("Void_Stone", "hyb", true, false, false);
                Build[2] = new Item("Mystical_Mail", "def", true, true, false);
                Build[3] = new Item("Hide_of_the_Urchin", "def", true, true, false);
                Build[4] = new Item("Obsidian_Shard", "dmg", true, false, false);
                Build[5] = new Item("Rod_of_Tahuti", "dmg", true, false, false);

                Build[6] = new Item("Blink_Rune", "hyb", true, true, false);
                Build[7] = new Item("Shield_of_Thorns", "def", true, true, false);
                Build[8] = new Item("Mark_of_the_Vanguard", "def", true, true, false);

                legalBoots = itemCol.retrieveLegalBoots(true, true, true, God.type);
                legalItems = itemCol.retrieveLegalItems(true, true, true, God.type);
                legalRelics = itemCol.retrieveLegalRelics(true, true, true, God.type);
                legalStarters = itemCol.retrieveLegalStarters(true, true, true, God.type);
            }
            #endregion
            //Validation of proper input
            else if (!((this.AssassinCheckBox.Checked == false
                && this.GuardianCheckBox.Checked == false
                && this.HunterCheckBox.Checked == false
                && this.MageCheckBox.Checked == false
                && this.WarriorCheckBox.Checked == false)
                || (this.DamageCheckBox.Checked == false
                && this.DefenseCheckBox.Checked == false
                && this.HybridCheckBox.Checked == false)))
            {
                
                
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
                else
                {
                    fullRebuild(
                    this.TextInput.Text,
                    this.DamageCheckBox.Checked,
                    this.HybridCheckBox.Checked,
                    this.DefenseCheckBox.Checked
                    );
                }
                
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

                legalBoots = itemCol.retrieveLegalBoots(true, true, true, God.type);
                legalItems = itemCol.retrieveLegalItems(true, true, true, God.type);
                legalRelics = itemCol.retrieveLegalRelics(true, true, true, God.type);
                legalStarters = itemCol.retrieveLegalStarters(true, true, true, God.type);
            }

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
            
            Draw();

        }
        #endregion
        
        #region HelpAndCredits
        private void CreditsButon_Click(object sender, EventArgs e)
        {
            if (CredsFlag == false)
            {
                GeneralTextBox.Text = "Created by: Trickermand, Nazzyc, Kloppermand.\n\nSmite is property of Hi-Rez studio" +
                                    ". All pictures are created and owned by Hi-Rez Studio\n\nBorders created by to J. W. Bjerk.";
                CredsFlag = true;
                WelcomeFlag = false;
                DetailsFlag = false;
                this.GeneralTextBox.Visible = true;
            }
            else
            {
                CredsFlag = false;
                WelcomeFlag = false;
                DetailsFlag = false;
                this.GeneralTextBox.Visible = false;
            }
        }
        

        private void WelcomeButton_Click(object sender, EventArgs e)
        {
            if (WelcomeFlag == false)
            {
                GeneralTextBox.Text = "Welcome to DankSmite!\nClick the Build button to get a god and a build! C" +
                                    "lick the pictures to reroll them, should you want to do so!\n\n\nWorks for smite ve" +
                                    "rsion: " + SmiteVersion + "\nDanksmite version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
                CredsFlag = false;
                WelcomeFlag = true;
                DetailsFlag = false;
                this.GeneralTextBox.Visible = true;
            }
            else
            {
                CredsFlag = false;
                WelcomeFlag = false;
                DetailsFlag = false;
                this.GeneralTextBox.Visible = false;
            }
        }

        private void DetailsButton_Click(object sender, EventArgs e)
        {
            if (DetailsFlag == false)
            {
                GeneralTextBox.Text = "Version of DankSmite is " + Assembly.GetExecutingAssembly().GetName().Version.ToString(4);
                CredsFlag = false;
                WelcomeFlag = false;
                DetailsFlag = true;
                this.GeneralTextBox.Visible = true;
            }
            else
            {
                CredsFlag = false;
                WelcomeFlag = false;
                DetailsFlag = false;
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
                DrawGodPictureBox();
                RerollAmount++;
                this.RerollNumber.Text = RerollAmount.ToString();
            }
        }

        private void StarterItemPicture_Click(object sender, EventArgs e)
        {
            rerollItemWrapper(8, StarterItemPicture);
        }

        private void ItemPicture1_Click(object sender, EventArgs e)
        {
            rerollItemWrapper(0, ItemPicture1);
        }

        private void ItemPicture2_Click(object sender, EventArgs e)
        {
            rerollItemWrapper(1, ItemPicture2);
        }

        private void ItemPicture3_Click(object sender, EventArgs e)
        {
            rerollItemWrapper(2, ItemPicture3);
        }

        private void ItemPicture4_Click(object sender, EventArgs e)
        {
            rerollItemWrapper(3, ItemPicture4);
        }

        private void ItemPicture5_Click(object sender, EventArgs e)
        {
            rerollItemWrapper(4, ItemPicture5);
        }

        private void ItemPicture6_Click(object sender, EventArgs e)
        {
            rerollItemWrapper(5, ItemPicture6);
        }

        private void RelicPicture1_Click(object sender, EventArgs e)
        {
            rerollItemWrapper(6, RelicPicture1);
        }

        private void RelicPicture2_Click(object sender, EventArgs e)
        {
            rerollItemWrapper(7, RelicPicture2);
        }

        private void rerollItemWrapper(int i, PictureBox pictureBox)
        {
            if (clickedRebuild)
            {
                rerollItem(i);
                AssignBorder(i, pictureBox);
                pictureBox.BackgroundImage = (Image)Resources.ResourceManager.GetObject(NamingFormatter(Build[i].name));
                this.CurrentItemNameLabel.Text = Build[i].name.Replace('_', ' ');
                RerollAmount++;
                this.RerollNumber.Text = RerollAmount.ToString();
            }
        }

        private void AssignBorder(int i, PictureBox pictureBox)
        {
            if (i != 6 && i != 7 && i != 0 && i != 8)
            {
                if (Build[i].itemColor.Equals("dmg"))
                {
                    pictureBox.Image = (Image)Resources.ResourceManager.GetObject("ItemFrameDamage");
                }
                else if (Build[i].itemColor.Equals("hyb"))
                {
                    pictureBox.Image = (Image)Resources.ResourceManager.GetObject("ItemFrameHybrid");
                }
                else if (Build[i].itemColor.Equals("def"))
                {
                    pictureBox.Image = (Image)Resources.ResourceManager.GetObject("ItemFrameDefense");
                }
            }
            else
            {
                pictureBox.Image = (Image)Resources.ResourceManager.GetObject("ItemFrameNeutral");
            }
        }
        #endregion

        //==================================DRAW=====================================//
        #region DrawingSection
        private void Draw()
        {
            DrawGodPictureBox();
            AssignBorder(0, ItemPicture1);
            AssignBorder(1, ItemPicture2);
            AssignBorder(2, ItemPicture3);
            AssignBorder(3, ItemPicture4);
            AssignBorder(4, ItemPicture5);
            AssignBorder(5, ItemPicture6);
            AssignBorder(6, RelicPicture1);
            AssignBorder(7, RelicPicture2);
            AssignBorder(8, StarterItemPicture);
           
            this.RerollNumber.Text = RerollAmount.ToString();

            this.ItemPicture1.BackgroundImage = (Image)Resources.ResourceManager.GetObject(NamingFormatter(Build[0].name));
            this.ItemPicture2.BackgroundImage = (Image)Resources.ResourceManager.GetObject(NamingFormatter(Build[1].name));
            this.ItemPicture3.BackgroundImage = (Image)Resources.ResourceManager.GetObject(NamingFormatter(Build[2].name));
            this.ItemPicture4.BackgroundImage = (Image)Resources.ResourceManager.GetObject(NamingFormatter(Build[3].name));
            this.ItemPicture5.BackgroundImage = (Image)Resources.ResourceManager.GetObject(NamingFormatter(Build[4].name));
            this.ItemPicture6.BackgroundImage = (Image)Resources.ResourceManager.GetObject(NamingFormatter(Build[5].name));

            this.RelicPicture1.BackgroundImage = (Image)Resources.ResourceManager.GetObject(NamingFormatter(Build[6].name));
            this.RelicPicture2.BackgroundImage = (Image)Resources.ResourceManager.GetObject(NamingFormatter(Build[7].name));

            this.StarterItemPicture.BackgroundImage = (Image)Resources.ResourceManager.GetObject(NamingFormatter(Build[8].name));
        }

        private string NamingFormatter(string txt)
        {
            txt = txt.Replace("_", "-");
            txt = txt.Replace("\'", "").ToLower();
            return txt;
        }

        public void DrawGodPictureBox()
        {
            this.GodPicture.BackgroundImage = (Image)Resources.ResourceManager.GetObject(NamingFormatter(_god.name));
            if (_god.type.Equals("g") || _god.type.Equals("m"))
            {
                this.GodPicture.Image = (Image)Resources.ResourceManager.GetObject("DankFrameBlue");
            }
            else
            {
                this.GodPicture.Image = (Image)Resources.ResourceManager.GetObject("DankFrameRed");
            }
            this.GodLabel.Text = _god.name.Replace("_", " ");
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
        

    }
}
