using _30XX_Save_Editor;
using _30XX_Save_Editor.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Simple_30XX_Save_Editor
{
    public partial class Form1 : Form
    {
        private byte[] memoria = new byte[4];
        private byte[] orbs = new byte[4];
        private byte[] entropy = new byte[4];
        private byte[] itemValues = new byte[14];
        private int[] itemDecimals = new int[5];
        Image ace = Resources.ace_leviathan;

        private static readonly Dictionary<int, string> ItemIDs = new Dictionary<int, string>()
        {
            {1, "Small Health"},
            {2, "Large Health"},
            {3, "Small Energy"},
            {4, "Large Energy"},
            {5, "Small Nuts"},
            {6, "Large Nuts"},
            {7, "Memoria"},
            {8, "Shiny Token"},
            {14, "N-Buster"},
            {15, "Star Beam"},
            {16, "Forkalator"},
            {17, "Wave Beam"},
            {18, "Scatterblast"},
            {19, "Arc Beam"},
            {20, "Seeker Beam PH"},
            {21, "Frost Beam"},
            {22, "Overcharge Beam PH"},
            {23, "Retrobeam"},
            {24, "Vertibeam"},
            {25, "Slow Beam PH"},
            {26, "Toy Beam"},
            {30, "A-Saber"},
            {32, "Rippling Axe"},
            {33, "Laurent"},
            {34, "Spinning Glaive"},
            {35, "Tonbokiri"},
            {36, "Lucavi"},
            {37, "Lara"},
            {38, "Grandmaster"},
            {39, "Thanatos"},
            {40, "Edgewall"},
            {41, "Dawnbind"},
            {49, "Unstoppable Force"},
            {50, "Volt Edge"},
            {51, "Vega"},
            {52, "Peacebringer"},
            {53, "Gemini"},
            {54, "Rapture"},
            {65, "V-Shooter"},
            {106, "Plasma Beam"},
            {107, "Sage Beam"},
            {108, "Wall Beam"},
            {109, "Chonk Beam"},
            {110, "Retrofork"},
            {111, "Bouncy Beam"},
            {112, "Verity Beam"},
            {113, "Spiral Beam"},
            {114, "Aim Beam"},
            {115, "Rocket Beam"},
            {116, "Sniper Beam"},
            {117, "??? Beam"},
            {118, "Juice Beam"},
            {119, "Chaos Beam"},
            {150, "Vera"},
            {151, "Zen Mortar"},
            {152, "Boomerang Blade"},
            {153, "Dixie Twist"},
            {154, "Shadespur"},
            {156, "Crushing Void"},
            {157, "Transposer"},
            {158, "Flameshield"},
            {159, "Splinterfrost"},
            {160, "Negation Pulse"},
            {161, "Wildfire"},
            {162, "Blastjump"},
            {163, "Seeking Striker"},
            {165, "Protorifle"},
            {169, "Transmutation"},
            {170, "World Igniter"},
            {176, "Rending Whirl"},
            {177, "Crystal Wave"},
            {178, "Aiming Gear"},
            {179, "Jagged Bolt"},
            {180, "Autodrone"},
            {183, "Unleash Blade"},
            {184, "Zen Ascent"},
            {185, "Echo Shell"},
            {186, "Dolomite Link"},
            {187, "Raijin Call"},
            {188, "Osafune"},
            {189, "Void Double"},
            {190, "Ryuusei"},
            {191, "Leviathan"},
            {196, "The cooler Crushing Void"},
            {333, "Sealing Record Key"},
            {334, "Essence of a Sealed Hero"},
            {348, "Imbued Chronovane"},
            {362, "Coalesced Entropy"},
            {382, "Multiversal Map"},
            {383, "Blueprint: Reversal Device"},
            {384, "Memento of Harmony"},
            {385, "Memento of Devotion"},
            {386, "Causation Invalidator"},
            {387, "Cathedral Coordinate"},
            {388, "Compressed Chronopulse"},
            {400, "Plumber Hat"},
            {401, "Strongarm Band"},
            {402, "Speed Juicer"},
            {404, "Power Enhancer"},
            {406, "Heart Container"},
            {407, "Lander's Essence"},
            {408, "Potato Battery"},
            {409, "Cerebral Nourishment Meal"},
            {410, "Zephyr"},
            {413, "Forcemetal Shell"},
            {414, "XCALBER"},
            {415, "Glass Cannon"},
            {418, "Vitality Scavenger"},
            {419, "Energy Scavenger"},
            {420, "Scrapmetal Scavenger"},
            {421, "Seven Leaf Clover"},
            {422, "Spillover Matrix"},
            {423, "Health Nut"},
            {424, "Charged Nuts"},
            {428, "Nut Replicator"},
            {429, "Orbital Barrier"},
            {430, "Murderdrone"},
            {431, "Renewal Pod"},
            {432, "Shinier Token"},
            {433, "Nutstack"},
            {434, "Re-Flapp"},
            {435, "Tiny Flamespewer"},
            {436, "Gapminder"},
            {437, "Thorned Revenger"},
            {438, "Reboot"},
            {439, "Vendsmasher"},
            {440, "Nutsaving Stringwire"},
            {441, "Armor Nut"},
            {442, "Regenerative Plating"},
            {443, "Enerative Plating"},
            {444, "Armorative Plating"},
            {445, "Emergency Beacon"},
            {446, "Thrift Actuator"},
            {447, "Crisis Overdrive"},
            {448, "Crisis Timestopper"},
            {449, "System Restore"},
            {450, "Armor Spreader"},
            {451, "Choicebooster"},
            {452, "Armor Bloom"},
            {453, "Meganut"},
            {454, "Pure Flame"},
            {455, "Forgotten Memento"},
            {456, "Bracer of Battle"},
            {457, "Mobility Realizer"},
            {458, "Life Extender"},
            {459, "Megaheart"},
            {460, "Vibroreserve"},
            {461, "Reclaimed Spark"},
            {462, "Armor Scavenger"},
            {463, "Entropy Lock"},
            {464, "Striking Feather"},
            {465, "Thunderous Boon"},
            {466, "Band of Might"},
            {467, "Mistimed Protector"},
            {468, "The Volunteer"},
            {469, "Thrillseeker"},
            {470, "Quantum Spook"},
            {471, "Hoarder's Might"},
            {472, "Contractor Plus"},
            {473, "Contractor Omega"},
            {474, "Mixmatch Mastery"},
            {475, "Charging Magnet"},
            {476, "World Slug"},
            {477, "Leafmetal Plating"},
            {478, "Zookeeper's Sigil"},
            {479, "Nutspewer"},
            {480, "Static Clicklets"},
            {481, "Fortune Stabilizer"},
            {482, "Spicy Incense"},
            {483, "Patchwork Connector"},
            {484, "Vitaboost"},
            {485, "Thorned Hull"},
            {486, "Juiced Reserves"},
            {487, "Kinetic Converter"},
            {488, "Boltdash"},
            {489, "Reflex Rapidifier"},
            {490, "Utilifier"},
            {491, "Vibrofocus"},
            {492, "Leafpetal"},
            {493, "Splintering Twinifier"},
            {494, "Zookeeper's Zeal"},
            {495, "Crisis Lifebank"},
            {496, "Deconstructor's Might"},
            {497, "Armorall"},
            {498, "Unflappable"},
            {499, "Intensifier"},
            {500, "Force Resonator"},
            {501, "Case Resonator"},
            {502, "Cranial Resonator"},
            {503, "Kinetic Resonator"},
            {504, "Core Extender"},
            {505, "Core Expander"},
            {506, "Core Bulkener"},
            {507, "Core Emboldener"},
            {508, "Core Luddite"},
            {509, "Field Integrator"},
            {510, "Mobility Enabler"},
            {511, "Utility Enabler"},
            {512, "Resilience Enabler"},
            {513, "Force Enabler"},
            {514, "Health Coronator"},
            {515, "Core Widener"},
            {516, "Core Overloader"},
            {517, "Core-Hull Multiplexer"},
            {518, "Core-Vibro Multiplexer"},
            {519, "Core-Force Multiplexer"},
            {520, "Core-Power Multiplexer"},
            {521, "Frail Glory"},
            {522, "Shopaholic"},
            {523, "Advanced Repair"},
            {524, "Might Harvester"},
            {525, "Power Harvester"},
            {526, "Kinetic Harvester"},
            {527, "Resilience Harvester"},
            {528, "Orb Enthusiast"},
            {529, "Scrap Scrambler"},
            {530, "Solution Gambler"},
            {531, "Autotank"},
            {534, "Spent Autotank"},
            {535, "Uncertain Blessing"},
            {536, "Prototype Resonator"},
            {537, "Protobalancer"},
            {538, "Shopsmasher"},
            {539, "Speed Demon"},
            {540, "Unstoppable Force"},
            {541, "Coup de Grace"},
            {542, "Thrill of Battle"},
            {543, "Barbarian's Resolve"},
            {544, "Juggernaut"},
            {545, "Mind Palace"},
            {546, "Fatal Fury"},
            {547, "Excitement Engine"},
            {548, "Calculated Strike"},
            {549, "Burn for Glory"},
            {550, "Grazing Harvester"},
            {551, "Blood Price"},
            {552, "Greased Fate"},
            {553, "Counterstrike"},
            {554, "Top Shelf"},
            {555, "Shield Bash"},
            {556, "Crisis Clarity"},
            {557, "Pain Circuit"},
            {558, "Rage Circuit"},
            {559, "Scrap Trawler"},
            {560, "Charisma Protocol"},
            {561, "Armor Integrator"},
            {562, "Autocompleter"},
            {563, "Scrap Sleuth"},
            {564, "Smuggler Attractor"},
            {565, "Scrappy Scrapper"},
            {566, "Useless Garbage"},
            {567, "Into the Fray"},
            {568, "Patient Hunter"},
            {569, "Doppelgel"},
            {570, "Challenger's Banner"},
            {571, "Superweight"},
            {650, "CQC Enthusiast"},
            {651, "Combo Demon"},
            {652, "Zephyr"},
            {653, "Secret Techniques"},
            {654, "Surging Lubricant"},
            {655, "Pressure Strikes"},
            {675, "Barrel Grease"},
            {676, "Power Purist"},
            {677, "Vibrodecay"},
            {678, "Marksman's Might"},
            {679, "Beam Enthusiast"},
            {680, "Heavy Juice"},
            {681, "Lil' Hoot"},
            {682, "Penance"},
            {683, "Facesmasher"},
            {684, "Grooving Pulsar"},
            {685, "Vital Crystal"},
            {687, "Time Anchor"},
            {688, "Chronopointer"},
            {689, "Hoot's Hourglass"},
            {690, "Kinetic Desctructor"},
            {691, "Bounding Blaster"},
            {692, "Graceful Strikes"},
            {693, "Vibroharvester"},
            {695, "Scrapbits"},
            {750, "Force"},
            {751, "Sagelens"},
            {752, "Clover"},
            {753, "Nutfinder"},
            {754, "Core"},
            {755, "Hysteria"},
            {756, "Mender"},
            {757, "Everfont"},
            {758, "Grab Bag"},
            {759, "Archive"},
            {760, "Utilifier Max"},
            {761, "Gears of Industry"},
            {762, "Market Fluctuations"},
            {763, "Grey Goo"},
            {764, "Focused Repair"},
            {765, "Elliecare"},
            {766, "Healing Flair"},
            {767, "Mobility Repositioner"},
            {768, "Vibrodevourer"},
            {769, "Final Bargain"},
            {770, "Vital Gambit"},
            {771, "Warping Waters"},
            {772, "Stalwart Waters"},
            {773, "Oops! All Refunds"},
            {774, "Burn for Glory"},
            {775, "Crushing Hysteria"},
            {776, "Flaring Hysteria"},
            {777, "Vexing Clover"},
            {778, "Wincing Clover"},
            {779, "Kinghealer"},
            {780, "Patchwork Overloader"},
            {781, "Patchwork Contract"},
            {782, "Consuming Stamina"},
            {783, "Consuming Habits"},
            {784, "Blue Streak"},
            {785, "Fresh Moves"},
            {786, "Charging Force"},
            {787, "Autoloader"},
            {788, "Earthmetal Plating"},
            {789, "Patchwork Integrator"},
            {790, "Consuming Fury"},
            {791, "Kingseeker"},
            {792, "Defiant Decree"},
            {793, "Violence Enhancer"},
            {794, "Uncharging Force"},
            {795, "Blinding Hysteria"},
            {796, "Solution Gambler"},
            {797, "Grey Goo"},
            {798, "The One and Only"},
            {800, "Debilitating"},
            {801, "Defiant"},
            {802, "Famished"},
            {803, "Powerless"},
            {804, "Noodly"},
            {805, "Outcast"},
            {806, "Immaterial"},
            {807, "Uncharging"},
            {808, "Stoic"},
            {809, "Fragile"},
            {810, "Purifying"},
            {811, "Thieving"},
            {812, "Edges of Madness"},
            {813, "Final Shell"},
            {814, "Purifying Waters"},
            {815, "Contractor Beta"},
            {816, "Zookeeper's Burden"},
            {817, "Symbol of Submission"},
            {818, "Symbol of Peace"},
            {850, "datalore received"},
            {851, "Armor Capsule"},
            {852, "Potentia Fragments"},
            {853, "Titan Shards"},
            {865, "Armatort's Shell"},
            {866, "Oxjack's Guile"},
            {867, "Dracopent's Pride"},
            {868, "Owlhawk's Reign"},
            {869, "Armatort's Dome"},
            {870, "Oxjack's Ken"},
            {871, "Dracopent's Fang"},
            {872, "Owlhawk's Focus"},
            {873, "Armatort's Momentum"},
            {874, "Oxjack's Blitz"},
            {875, "Dracopent's Bound"},
            {876, "Owlhawk's Feather"},
            {877, "Armatort's Pound"},
            {878, "Oxjack's Fury"},
            {879, "Dracopent's Claw"},
            {880, "Owlhawk's Talon"},
            {881, "Armatort's Wish"},
            {882, "Oxjack's Wish"},
            {883, "Dracopent's Wish"},
            {884, "Owlhawk's Wish"},
            {885, "Harmony Circuit"},
            {886, "Big Chunk"},
            {887, "Vagrant's Yearn"},
            {888, "Vagrant's Sonata"},
            {889, "Vagrant's Waltz"},
            {890, "Vagrant's Dissonance"},
            {891, "Zookeeper's Phalanx"},
            {892, "Zookeeper's Command"},
            {893, "Zookeeper's March"},
            {894, "Zookeeper's Wrath"},
            {895, "Scrooge's Insurance"},
            {896, "Scrooge's Bounty"},
            {897, "Scrooge's Bootstrap"},
            {898, "Scrooge's Wager"},
            {899, "No Arms Aug"},
            {900, "No Body Aug"},
            {901, "No Legs Aug"},
            {902, "No Head Aug"},
            {903, "Armatort, the Unstoppable"},
            {904, "Oxjack, the Flash"},
            {905, "Dracopent, the Foul"},
            {906, "Owlhawk, the Wise"},
            {909, "Remnant of Flame"},
            {910, "Remnant of Sound"},
            {911, "Remnant of Time"},
            {912, "Remnant of Wind"},
            {913, "Remnant of Spirit"},
            {914, "Remnant of Kindness"},
            {915, "Remnant of Determination"},
            {916, "Remnant of Sorrow"},
            {917, "Wild Mortar"},
            {918, "Serene Mortar"},
            {920, "Crystal Chains"},
            {921, "Unyielding Wave"},
            {923, "Metal Gear"},
            {924, "Disruptor Gear"},
            {926, "Whirlspur"},
            {927, "Unending Whirl"},
            {929, "Desperate Void"},
            {930, "Efficient Void"},
            {932, "Mirror Pulse"},
            {933, "Pulse Engine"},
            {935, "Bouncing Bolt"},
            {936, "Supersonic Bolt"},
            {938, "Vicious Drone"},
            {939, "Devoted Drone"},
            {941, "Furious Ascent"},
            {942, "Redeemer's Ascent"},
            {944, "Mobile Shell"},
            {945, "Mirror Shell"},
            {947, "Severing Link"},
            {948, "Shield Link"},
            {950, "Stormherald"},
            {951, "Raijin Engine"},
            {953, "Hungering Void"},
            {954, "Void Persistence"},
            {956, "Geoshift"},
            {957, "Shaded Strikes"},
            {959, "Falling Star"},
            {960, "Astral Terminus"},
            {962, "Sacred Minnow"},
            {963, "Enter the Void"},
            {965, "Saber Impulse"},
            {966, "Resonant Saber"},
            {967, "Super Tonbo"},
            {968, "Deadly Precision"},
            {969, "Wavethrower"},
            {970, "Shock Actuator"},
            {971, "Effortless Blades"},
            {972, "Phase Knives"},
            {973, "Lingering Fist"},
            {974, "Rapid Fist"},
            {975, "Cycling Edge"},
            {976, "Magnetic Edge"},
            {977, "Lethal Evade"},
            {978, "Critical Blade"},
            {979, "Reaper's Call"},
            {980, "Graceful Spinning"},
            {981, "Chainbind"},
            {982, "Stylestrike"}
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents\\30XX");
            openFileDialog.FileName = "30XXv001.sav";
            openFileDialog.Filter = "SAV files (*.sav)|*.sav";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                using (var stream = File.OpenRead(openFileDialog.FileName))
                {

                    stream.Seek(0x4, SeekOrigin.Begin);
                    stream.Read(memoria, 0, 4);

                    stream.Seek(0xC, SeekOrigin.Begin);
                    stream.Read(orbs, 0, 4);

                    stream.Seek(0x863, SeekOrigin.Begin);
                    stream.Read(itemValues, 0, 14);

                    stream.Seek(0x872, SeekOrigin.Begin);
                    stream.Read(entropy, 0, 1);

                }

                Array.Reverse(memoria);
                Array.Reverse(orbs);

                byte[] reversedBytes = new byte[10];

                for (int i = 0; i < 5; i++)
                {
                    //System.Diagnostics.Debug.WriteLine(values[3*i]);
                    reversedBytes[i * 2] = itemValues[i * 3 + 1];
                    reversedBytes[i * 2 + 1] = itemValues[i * 3];
                }

                int memoriaDecimal = BitConverter.ToInt32(memoria, 0);
                int orbsDecimal = BitConverter.ToInt32(orbs, 0);
                int entropyDecimal = BitConverter.ToInt32(entropy, 0);
                

                for (int i = 0; i < 5; i++)
                {
                    itemDecimals[i] = BitConverter.ToInt16(reversedBytes, i * 2);
                }

                System.Windows.Forms.Label[] labels = new System.Windows.Forms.Label[]
                {
                    label1,
                    label2,
                    label3,
                    label4,
                    label5
                };

                System.Windows.Forms.ComboBox[] comboBoxes = new System.Windows.Forms.ComboBox[] {
                    comboBox1,
                    comboBox2,
                    comboBox3,
                    comboBox4,
                    comboBox5
                };

                try
                {
                    memoriaNumeric.Value = memoriaDecimal;
                    orbsNumeric.Value = orbsDecimal;
                    ecNumeric.Value = entropyDecimal;

                    for (int i = 0; i < 5; i++)
                    {
                        comboBoxes[i].DataSource = new BindingSource(ItemIDs, null);
                        comboBoxes[i].DisplayMember = "Value";
                        comboBoxes[i].ValueMember = "Key";
                        comboBoxes[i].SelectedValue = itemDecimals[i];
                        labels[i].Text = comboBoxes[i].SelectedValue.ToString();
                    }

                    //imageComboBox1.DataSource = new BindingSource(ItemIDs, null);
                    //imageComboBox1.DisplayMember = "Value";
                    //imageComboBox1.ValueMember = "Key";
                    //imageComboBox1.SelectedValue = 911;

                    foreach (KeyValuePair<int, string> pair in ItemIDs)
                    {
                        ImageComboBoxItem item = new ImageComboBoxItem(pair.Value, pair.Key, ace);
                        imageComboBox1.Items.Add(item);
                    }



                    comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
                    comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
                    comboBox3.SelectedIndexChanged += ComboBox3_SelectedIndexChanged;
                    comboBox4.SelectedIndexChanged += ComboBox4_SelectedIndexChanged;
                    comboBox5.SelectedIndexChanged += ComboBox5_SelectedIndexChanged;

                    SaveButton.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: The save file structure probably changed, so this version of the editor will no longer work.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
        }




        //private static readonly Dictionary<string, int> ReverseItemIDs = ItemIDs.ToDictionary(x => x.Value, x => x.Key);

        private static String DisplayValues(int itemValue)
        {
            if (ItemIDs.TryGetValue(itemValue, out string description))
            {
                return description;
            }
            else
            {
                return "Unknown";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "SAV files (*.sav)|*.sav";
            saveFileDialog.FileName = "30XXv001.sav";
            saveFileDialog.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents\\30XX");
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                memoria = BitConverter.GetBytes((int)memoriaNumeric.Value);
                Array.Reverse(memoria);

                orbs = BitConverter.GetBytes((int)orbsNumeric.Value);
                Array.Reverse(orbs);

                entropy = BitConverter.GetBytes((int)ecNumeric.Value);

                byte[] item1 = BitConverter.GetBytes((Int16)(int)comboBox1.SelectedValue);
                Array.Reverse(item1);

                byte[] item2 = BitConverter.GetBytes((Int16)(int)comboBox2.SelectedValue);
                Array.Reverse(item2);

                byte[] item3 = BitConverter.GetBytes((Int16)(int)comboBox3.SelectedValue);
                Array.Reverse(item3);

                byte[] item4 = BitConverter.GetBytes((Int16)(int)comboBox4.SelectedValue);
                Array.Reverse(item4);

                byte[] item5 = BitConverter.GetBytes((Int16)(int)comboBox5.SelectedValue);
                Array.Reverse(item5);

                using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    fileStream.Seek(0x4, SeekOrigin.Begin);
                    fileStream.Write(memoria, 0, 4);

                    fileStream.Seek(0xC, SeekOrigin.Begin);
                    fileStream.Write(orbs, 0, 4);

                    fileStream.Seek(0x863, SeekOrigin.Begin);
                    fileStream.Write(item1, 0, item1.Length);

                    fileStream.Seek(0x866, SeekOrigin.Begin);
                    fileStream.Write(item2, 0, item2.Length);

                    fileStream.Seek(0x869, SeekOrigin.Begin);
                    fileStream.Write(item3, 0, item3.Length);

                    fileStream.Seek(0x86C, SeekOrigin.Begin);
                    fileStream.Write(item4, 0, item4.Length);

                    fileStream.Seek(0x86F, SeekOrigin.Begin);
                    fileStream.Write(item5, 0, item5.Length);

                    fileStream.Seek(0x872, SeekOrigin.Begin);
                    fileStream.Write(entropy, 0, 1);
                }
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = comboBox1.SelectedValue.ToString();
        }
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = comboBox2.SelectedValue.ToString();
        }
        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            label3.Text = comboBox3.SelectedValue.ToString();
        }
        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            label4.Text = comboBox4.SelectedValue.ToString();
        }
        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            label5.Text = comboBox5.SelectedValue.ToString();
        }

    }




}