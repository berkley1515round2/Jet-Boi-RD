using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Jet_Boi_RD.Screens
{
    public partial class GameScreen : UserControl
    {
        #region globals
        public bool up = false;
        public bool grounded = true;
        public bool jumping = false;
        Classes.Player player = new Classes.Player(50, 0);
        Pen playerPen = new Pen(Color.White);
        SolidBrush coinBrush = new SolidBrush(Color.Gold);
        public const int pheight = 50;
        public const int pwidth = 20;
        const int longLaser = 200;
        const int midLaser = 150;
        const int smallLaser = 100;
        const int laserWidth = 50;
        int airDownFrames = 0;
        Form form;
        int timeBtwnLasers = 240;
        int tick = 0;
        Random r = new Random();
        List<Classes.laser> lasers = new List<Classes.laser>();
        List<Classes.coin> coins = new List<Classes.coin>();
        public static int backgroundMoveSpd = 8;
        Classes.laser laserToRemove;
        Classes.coin coinToRemove;
        public static long dist;
        public static long maxDist;
        public static float actualDist;
        public static int coinScore = 0;
        int coinChance = 30;
        bool endGame = false;
        int bounce;
        bool bounceUp = true;
        static Dictionary<string, bool> mechs = new Dictionary<string, bool>();
        static Dictionary<string, bool> upgrades = new Dictionary<string, bool>();
        static Dictionary<string, bool> upgradesActv = new Dictionary<string, bool>();
        static Dictionary<string, Dictionary<string, bool>> mechUpgs = new Dictionary<string, Dictionary<string, bool>>();
        #endregion
        public GameScreen()
        {
            InitializeComponent();
            if (!Form1.start)
            {
                mechs.Add("superJump", false);
                mechs.Add("telporter", false);
                mechs.Add("gravity", false);
                upgrades.Add("jumpBoost", false);
                upgradesActv.Add("jumpBoost", false);
                upgrades.Add("ironBoi", false);
                upgradesActv.Add("ironBoi", false);
                Dictionary<string, bool> a = new Dictionary<string, bool>();
                a.Add("magnet", false);
                a.Add("golden", false);
                Dictionary<string, bool> b = new Dictionary<string, bool>();
                b.Add("magnet", false);
                b.Add("golden", false);
                Dictionary<string, bool> c = new Dictionary<string, bool>();
                c.Add("magnet", false);
                c.Add("golden", false);
                mechUpgs.Add("teleporter", a);
                mechUpgs.Add("superJump", b);
                mechUpgs.Add("gravity", c);
                Form1.start = true;
            }
            //xmlSave();
            xmlLoad();
        }

        public static void xmlLoad()
        {

            //creates variables and xml reader needed
            XmlReader reader = XmlReader.Create("player1.xml");
            reader.ReadToFollowing("coin");
            coinScore = Convert.ToInt16(reader.ReadString());
            reader.ReadToFollowing("maxDist");
            maxDist = Convert.ToInt64(reader.ReadString());
            //Grabs all the blocks for the current level and adds them to the list
            Dictionary<string, bool> upgd = new Dictionary<string, bool>();
            foreach(KeyValuePair<string, bool> b in upgrades)
            {
                reader.ReadToFollowing("upgrade");
                string key = reader.GetAttribute("name");
                upgd[key] = XmlConvert.ToBoolean(reader.GetAttribute("value").ToLower());            
            }
            upgrades = upgd;
            Dictionary<string, bool> upgdA = new Dictionary<string, bool>();
            foreach (KeyValuePair<string, bool> b in upgradesActv)
            {
                reader.ReadToFollowing("upgrade");
                string key = reader.GetAttribute("name");
                upgdA[key] = XmlConvert.ToBoolean(reader.GetAttribute("value").ToLower());
            }
            upgradesActv = upgdA;
            reader.ReadToFollowing("mechs");
            Dictionary<string, bool> mch = new Dictionary<string, bool>();
            foreach (KeyValuePair<string, bool> b in mechs)
            {
                reader.ReadToFollowing(b.Key);
                mch[b.Key] = XmlConvert.ToBoolean(reader.ReadString().ToLower());
            }
            mechs = mch;
            reader.ReadToFollowing("mechUpgs");
            Dictionary<string, Dictionary<string, bool>> mchUpgd = new Dictionary<string, Dictionary<string, bool>>();
            foreach (KeyValuePair<string, Dictionary<string, bool>> c in mechUpgs)
            {

                reader.ReadToFollowing(c.Key);
                Dictionary<string, bool> x = new Dictionary<string, bool>();
                foreach (KeyValuePair<string, bool> b in c.Value)
                {

                    reader.ReadToFollowing(b.Key);
                    
                    x.Add(b.Key, XmlConvert.ToBoolean(reader.ReadString().ToLower()));
                    
                }
                mchUpgd[c.Key] = x;
            }
            mechUpgs = mchUpgd;
            reader.Close();
        }
        public static void xmlSave()
        {
            /*
            highscores.Add(score);
            //can be used to create farthest distance
            highscores.Sort();
            highscores.Reverse();
            */
            XmlWriter writer = XmlWriter.Create("player1.xml", null);
            writer.WriteStartElement("player");
            writer.WriteString("\n");
            writer.WriteElementString("coin", "" + coinScore);
            writer.WriteString("\n");
            writer.WriteElementString("maxDist", "" + maxDist);
            writer.WriteString("\n");
            writer.WriteStartElement("upgradesPurch");
            writer.WriteString("\n");
            foreach (KeyValuePair<string, bool> b in upgrades)
            {
                writer.WriteStartElement("upgrade");
                writer.WriteAttributeString("name", b.Key);
                writer.WriteAttributeString("value", "" + b.Value);
                writer.WriteEndElement();
                writer.WriteString("\n");
            }
            writer.WriteEndElement();
            writer.WriteString("\n");
            writer.WriteStartElement("upgradesActv");
            writer.WriteString("\n");
            foreach (KeyValuePair<string, bool> b in upgradesActv)
            {
                writer.WriteStartElement("upgrade");
                writer.WriteAttributeString("name", b.Key);
                writer.WriteAttributeString("value", "" + b.Value);
                writer.WriteEndElement();
                writer.WriteString("\n");
            }
            writer.WriteEndElement();
            writer.WriteString("\n");
            writer.WriteStartElement("mechs");
            writer.WriteString("\n");
            foreach (KeyValuePair<string, bool> b in mechs)
            {
                writer.WriteStartElement(b.Key);
                writer.WriteString("" + b.Value);
                writer.WriteEndElement();
                writer.WriteString("\n");
            }
            writer.WriteEndElement();
            writer.WriteString("\n");
            writer.WriteStartElement("mechUpgs");
            writer.WriteString("\n");
            foreach (KeyValuePair<string, Dictionary<string, bool>> c in mechUpgs)
            {
                writer.WriteStartElement(c.Key);
                writer.WriteString("\n");
                foreach (KeyValuePair<string, bool> b in c.Value)
                {
                    writer.WriteStartElement(b.Key);
                    writer.WriteString("" + b.Value);
                    writer.WriteEndElement();
                    writer.WriteString("\n");
                }
                writer.WriteEndElement();
                writer.WriteString("\n");
            }
            writer.WriteEndElement();
            writer.WriteString("\n");

            writer.Close();

        }

        public void GameOver()
        {
            if (dist > maxDist) maxDist = dist;
            xmlSave();
            gameTimer.Stop();
            
            if (coinScore >= 250)
            {
                revivePopup rp = new revivePopup();

                DialogResult result = rp.ShowDialog();

                if (result == DialogResult.Yes)
                {
                    gameTimer.Enabled = true;
                    backgroundMoveSpd = 8;
                    timeBtwnLasers = 120;
                    coinScore -= 250;
                    endGame = false;
                    lasers.Clear();
                }
                else if (result == DialogResult.No)
                {
                    Form1.switchScreen(this, "shop");
                    dist = 0;
                }
            }
            else
            {
                Form1.switchScreen(this, "shop");
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            tick++;
            dist += backgroundMoveSpd;
            actualDist = dist / 100;
            if (endGame && tick % 15 == 0)
            {
                if (backgroundMoveSpd > 0) backgroundMoveSpd--;
                else GameOver();
            }
            if (endGame && player.hb.Bottom > this.Height - pheight - 1 && !bounceUp)
            {
                bounceUp = true;
                bounce /= 2;
            }

             if (player.hb.Bottom > this.Height - bounce && bounceUp && endGame)
            {
                player.move(-10);
                airDownFrames = 0;
            }
            else
            {
                bounceUp = false;
                
            }

            if (r.Next(0, 101) < coinChance && tick % 120 == 0 && !endGame)
            {
                generateCoin(r.Next(0, 3), r.Next(100, this.Height - 100));
            }
            if (tick == 180)
            {
                lasers.Add(new Classes.laser(this.Width, 10, midLaser, laserWidth));
            }
            else if (tick % timeBtwnLasers == 0 && !endGame)
            {
                generateLaser(player.y);
            }
            if (tick % 1200 == 0 && coinChance < 35 && !endGame) coinChance += 5;
            if (tick % 720 == 0 && !endGame)
            {

                if (backgroundMoveSpd < 20)
                {
                    backgroundMoveSpd += 2;

                }

                //if (timeBtwnLasers > 15) timeBtwnLasers -= 15;
            }
            if(tick % 120 == 0)
            {
                if (timeBtwnLasers > 20) timeBtwnLasers -= 5;
                else
                {
                     //timeBtwnLasers = 5;
                }
            }
            if (up)
            {
                if (player.hb.Y > 0)
                {
                    player.move(-8);
                }
                if (airDownFrames > 0) airDownFrames -= 2;
            }
            if (player.hb.Bottom < this.Height - pheight) // if player is not touching bottom of screen
            {
                if (!up)
                {
                    if (player.hb.Bottom + 2 < this.Height)
                    {
                        player.move(2 + (int)Math.Floor(Math.Pow(airDownFrames, 2) / 10));
                    }
                    else
                    {
                        player.hb.Y = this.Height - player.hb.Height;
                        player.y = this.Height - player.hb.Height;
                    }
                    if (airDownFrames < 10) airDownFrames++;
                }
                /*
                 * (int)Math.Floor(Math.Pow(airDownFrames, 2) / 20);
                if( airDownFrames < 13) airDownFrames++;
                 */
            }
            else if (player.hb.Bottom >= this.Height - pheight) // if player is touching bottom of screen
            {
                grounded = true;
            }
            if (jumping)
            {
                player.move(-30);
                jumping = false;
            }
            if (endGame) up = false;
            Refresh();
        }

        private void GameScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if(endGame)
            {

            }
            else if (e.KeyCode == Keys.Space && !endGame)
            {
                if (grounded) // boost
                {
                    grounded = false;
                    jumping = true;
                    up = true;
                }
                else if(!endGame)
                {
                    up = true;
                }
            }


        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(playerPen, player.hb);

            foreach (Classes.laser l in lasers)
            {
                l.move();
                e.Graphics.DrawRectangle(playerPen, l.hb);
                if (l.hb.Right <= 0) laserToRemove = l;
                if (player.hb.IntersectsWith(l.hb))
                {
                    //GameOver();
                    if (!endGame)
                    {
                        endGame = true;
                        bounce = (this.Height - player.y) / 2;
                    }
                }
            }
            List<Classes.coin> delete = new List<Classes.coin>();
            foreach (Classes.coin c in coins)
            {
                if (c.hb.IntersectsWith(player.hb))
                {
                    coinScore++;
                    //coinToRemove = c;
                    delete.Add(c);
                }
                else
                {
                    c.move();
                    e.Graphics.FillEllipse(coinBrush, c.hb);
                    if (c.hb.Right <= 0)
                    {
                        //coinToRemove = c;
                        delete.Add(c);
                    }
                }
            }
            e.Graphics.DrawString("coins " + actualDist, Font, new SolidBrush(Color.White), 0, 10);
            RemoveLaser(laserToRemove);
            RemoveCoin(delete);
            coinToRemove = null;
            laserToRemove = null;
        }

        public void RemoveLaser(Classes.laser l)
        {
            lasers.Remove(l);
        }
        public void RemoveCoin(Classes.coin l)
        {
            coins.Remove(l);
        }
        public void RemoveCoin(List<Classes.coin> l)
        {
            foreach (Classes.coin c in l)
            {
                coins.Remove(c);
            }
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            player.y = this.Height - pheight * 2;
            player.hb.Y = this.Height - pheight * 2;
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            up = false;
        }

        private void generateCoin(int pattern, int pos)
        {
            List<Classes.coin> c = new List<Classes.coin>();
            switch (pattern)
            {
                case 0:
                    for (int x = 0; x < 5; x++)
                    {
                        for (int y = pos; y < pos + 70; y += 10)
                        {
                            c.Add(new Classes.coin(this.Width + x * 10, y));
                        }
                    }

                    break;
                case 1:
                    c.Add(new Classes.coin(this.Width, pos - 12));
                    for (int x = 0; x > -5; x--)
                    {

                        c.Add(new Classes.coin(this.Width - (x - 1) * 10, pos + x * 10));

                    }
                    break;
                case 2:
                    int n;
                    n = r.Next(20, 100);
                    for (int x = 0; x < 18; x++)
                    {
                        c.Add(new Classes.coin(this.Width + x * n, pos + n * (float)Math.Sin(45 * x * (Math.PI / 180))));
                    }
                    break;
                case 3:

                    break;
                case 4:
                    break;
            }
            foreach (Classes.coin coin in c)
            {
                coins.Add(coin);
            }
        }

        private void generateLaser(int py)
        {
            int w = 0;
            int l = 0;
            int y = 0;
            switch (r.Next(0, 2))
            {
                case 0: // vert rect
                    switch (r.Next(0, 3))
                    {
                        case 0:
                            l = smallLaser;
                            break;
                        case 1:
                            l = midLaser;
                            break;
                        case 2:
                            l = longLaser;
                            break;
                    }
                    if (py <= l / 2) y = 0;
                    else y = py - l / 2;
                    w = laserWidth;
                    break;
                case 1: // horiz rect
                    switch (r.Next(0, 3))
                    {
                        case 0:
                            w = smallLaser;
                            break;
                        case 1:
                            w = midLaser;
                            break;
                        case 2:
                            w = longLaser;
                            break;
                    }
                    if (py <= w / 2) y = 0;
                    else y = py - w / 2;
                    l = laserWidth;
                    break;
            }
            if (py <= l) y = 0;
            else y = py - l / 2;

            lasers.Add(new Classes.laser(this.Width, y, l, w));
        }
        
    }
}
