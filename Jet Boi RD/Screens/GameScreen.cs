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
        Classes.mechToken MTokenToRemove;
        Classes.rocket rocketToRemove;
        public static long dist;
        public static long maxDist;
        public static float actualDist;
        public static int coinScore = 0;
        int coinChance = 30;
        bool endGame = false;
        int bounce;
        bool bounceUp = true;

        bool spawnBarrage = false;
        int barrageSpawns = 0;
        public static Dictionary<string, bool> mechs = new Dictionary<string, bool>();
        public static Dictionary<string, bool> upgrades = new Dictionary<string, bool>();
        public static Dictionary<string, bool> upgradesActv = new Dictionary<string, bool>();
        public static Dictionary<string, Dictionary<string, bool>> mechUpgs = new Dictionary<string, Dictionary<string, bool>>();
        static List<Classes.mechToken> MTokens = new List<Classes.mechToken>();
        static List<Classes.rocket> rockets = new List<Classes.rocket>();

        public bool toClearLsers = false;

        public bool keydown = false;

        int spdStorage = 0;

        public static string curntMech = "none";
        public static bool abort = false;

        public bool gravDown = true; //for gravity suit

        public bool teleporterUp = false; // for teleporter
        public int teleporterMarkerY = 0;

        public int upFrames = 0; // for super jump


        #endregion
        public GameScreen()
        {
            InitializeComponent();
            if (!Form1.start)
            {
                mechs.Add("superJump", true);
                mechs.Add("teleporter", true);
                mechs.Add("gravity", true);
                upgrades.Add("jumpBoost", false);
                upgradesActv.Add("jumpBoost", false);
                upgrades.Add("ironBoi", false);
                upgradesActv.Add("ironBoi", false);
                upgrades.Add("xray", false);
                upgradesActv.Add("xray", false);
                upgrades.Add("jammer", false);
                upgradesActv.Add("jammer", false);
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
            xmlSave();
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
            foreach (KeyValuePair<string, bool> b in upgrades)
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

            if (coinScore >= 0)
            {
                revivePopup rp = new revivePopup();

                DialogResult result = rp.ShowDialog();

                if (result == DialogResult.Yes)
                {
                    gameTimer.Enabled = true;
                    backgroundMoveSpd = spdStorage;
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
                else if (result == DialogResult.OK)
                {
                    ShopScreen.switchS = true;
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
                else if (bounce < 10 && backgroundMoveSpd == 0) GameOver();
            }
            if (endGame && player.hb.Bottom >= this.Height && !bounceUp)
            {
                bounceUp = true;
                bounce /= 2;
                if (bounce < 15) bounce = 0;
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
            if (r.Next(0, 2) == 0 && tick % 1200 == 0 && !endGame && curntMech == "none")
            {
                Classes.mechToken m = new Classes.mechToken(this.Width, r.Next(0, this.Height - 50));
                if (!abort)
                {
                    MTokens.Add(m);
                }
                else abort = false;
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
            if (tick % 120 == 0)
            {
                if (timeBtwnLasers > 20) timeBtwnLasers -= 5;
                else
                {
                    //timeBtwnLasers = 5;
                }
            }
            if(tick % 600 == 0 && r.Next(0,3) == 0)
            {
                if (r.Next(0, 5) == 0)
                {
                    spawnBarrage = true;
                    barrageSpawns = 0;
                }

                Classes.rocket rc = new Classes.rocket(this.Width, player.y + 25);
                rockets.Add(rc);


            }
            if(spawnBarrage && tick % 30 == 0 && barrageSpawns < 5)
            {
                Classes.rocket rc = new Classes.rocket(this.Width, player.y + 25);
                rockets.Add(rc);
                barrageSpawns++;
            }
            



            switch (curntMech)
            {
                case "none":
                    standardGrounded();
                    standardUp();
                    standardGav();
                    standardJump();
                    break;
                case "teleporter":
                    grounded = true;
                    break;
                case "superJump":
                    superJumpUp();
                    superJumpGav();
                    break;
                case "gravity":
                    GsuitGav();
                    break;
            }




            if (endGame) up = false;
            Refresh();
        }

        private void GameScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (endGame)
            {

            }
            else if (e.KeyCode == Keys.Space && !endGame && curntMech != "gravity")
            {
                if (grounded) // boost
                {
                    grounded = false;
                    jumping = true;
                    up = true;
                }
                else if (!endGame)
                {
                    up = true;
                }
            }


        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            switch (curntMech)
            {
                case "none":
                    //e.Graphics.DrawRectangle(playerPen, player.hb);
                    break;
                case "teleporter":
                    break;
                case "superJump":
                    break;
                case "gravity":
                    break;

            }

            
            e.Graphics.DrawRectangle(playerPen, player.hb);
            if (curntMech == "teleporter") teleporterDraw(e.Graphics, teleporterMarkerY);
            foreach (Classes.rocket l in rockets)
            {
                if (l.launchingRocket && l.launchingTicks < 180)
                {
                    l.launchingTicks++;
                    e.Graphics.DrawRectangle(new Pen(Color.White), this.Width - 30, player.hb.Top + 25, 30, 25);
                }
                if(l.launchingTicks == 180 && l.launchingRocket)
                {
                    l.hb.Y = player.y + 25;
                    l.y = player.y + 25;
                    l.launchingRocket = false;
                }
                if (!l.launchingRocket)
                {                        
                    l.move();
                    e.Graphics.DrawRectangle(playerPen, l.hb);
                    if (l.hb.Right <= 0) rocketToRemove = l;
                    if (player.hb.IntersectsWith(l.hb))
                    {
                        //GameOver();
                        if (!endGame && curntMech == "none")
                        {
                            endGame = true;
                            spdStorage = backgroundMoveSpd;
                            bounce = (this.Height - player.y) / 2;
                        }
                        else if (curntMech != "none")
                        {
                            toClearLsers = true;
                            grounded = false;
                            curntMech = "none";

                        }
                    }
                }
            }

            foreach (Classes.laser l in lasers)
            {
                l.move();
                e.Graphics.DrawRectangle(playerPen, l.hb);
                if (l.hb.Right <= 0) laserToRemove = l;
                if (player.hb.IntersectsWith(l.hb))
                {
                    //GameOver();
                    if (!endGame && curntMech == "none")
                    {
                        spdStorage = backgroundMoveSpd;
                        endGame = true;
                        bounce = (this.Height - player.y) / 2;
                    }
                    else if (curntMech != "none")
                    {
                        toClearLsers = true;
                        grounded = false;
                        curntMech = "none";
                    }
                }
            }
            if (toClearLsers)
            {
                lasers.Clear();
                rockets.Clear();
                toClearLsers = false;
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
            foreach (Classes.mechToken m in MTokens)
            {
                if (m.hb.IntersectsWith(player.hb))
                {
                    MTokenToRemove = m;
                    curntMech = m.type;
                    lasers.Clear();
                    rockets.Clear();
                }
                else
                {
                    m.move();
                    e.Graphics.FillRectangle(coinBrush, m.hb);
                    if (m.hb.Right <= 0)
                    {
                        MTokenToRemove = m;

                    }
                }
            }
            e.Graphics.DrawString("Coins " + coinScore, Font, new SolidBrush(Color.White), 0, 10);
            e.Graphics.DrawString("Distance " + actualDist, Font, new SolidBrush(Color.White), 0, 20);
            RemoveLaser(laserToRemove);
            RemoveCoin(delete);
            RemoveMToken(MTokenToRemove);
            MTokenToRemove = null;
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
        public void RemoveMToken(Classes.mechToken m)
        {
            MTokens.Remove(m);
        }
        public void RemoveRocket(Classes.rocket r)
        {
            rockets.Remove(r);
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            player.y = this.Height - pheight;
            player.hb.Y = this.Height - pheight;
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            up = false;
            keydown = false;
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

        public void teleporterMove(int y)
        {
            player.moveTo(y);
        }

        public void teleporterDraw(Graphics g, int y)
        {
            if (y >= this.Height - pheight)
            {
                teleporterUp = true;
            }
            if (y < 0)
            {
                teleporterUp = false;
            }


            if (teleporterUp) y -= 10;
            else y += 10;

            g.DrawRectangle(new Pen(Color.White), player.x, y, pwidth, pheight);
            teleporterMarkerY = y;
        }

        private void GameScreen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space && !keydown)
            {
                switch (curntMech)
                {
                    case "none":
                        break;
                    case "teleporter":
                        teleporterMove(teleporterMarkerY);
                        break;
                    case "superJump":
                        break;
                    case "gravity":
                        gravDown = !gravDown;
                        airDownFrames = 0;
                        break;
                }
                keydown = true;
            }
        }
        #region gravities
        public void standardGav()
        {
            if (player.hb.Bottom < this.Height) // if player is not touching bottom of screen
            {
                if (!up)
                {

                    if (player.hb.Bottom < this.Height)
                    {
                        player.move(2 + (int)Math.Floor(Math.Pow(airDownFrames, 2) / 20));
                    }


                    if (airDownFrames < 15) airDownFrames++;
                }
                /*
                 * (int)Math.Floor(Math.Pow(airDownFrames, 2) / 20);
                if( airDownFrames < 13) airDownFrames++;
                 */
            }
        }

        public void GsuitGav()
        {
            if (gravDown)
            {
                if (player.hb.Bottom < this.Height) // if player is not touching bottom of screen
                {


                    if (player.hb.Bottom < this.Height)
                    {
                        player.move(2 + (int)Math.Floor(Math.Pow(airDownFrames, 2) / 30));
                    }



                    if (airDownFrames < 20) airDownFrames++;

                    /*
                     * (int)Math.Floor(Math.Pow(airDownFrames, 2) / 20);
                    if( airDownFrames < 13) airDownFrames++;
                     */
                }
                else airDownFrames = 0;
            }
            else
            {
                if (player.hb.Top > 0) // if player is not touching bottom of screen
                {


                    if (player.hb.Top > 0)
                    {
                        player.move(-(2 + (int)Math.Floor(Math.Pow(airDownFrames, 2) / 30)));
                    }



                    if (airDownFrames < 20) airDownFrames++;

                    /*
                     * (int)Math.Floor(Math.Pow(airDownFrames, 2) / 20);
                    if( airDownFrames < 13) airDownFrames++;
                     */
                }
                else airDownFrames = 0;
            }
        }

        public void superJumpGav()
        {
            if (player.hb.Bottom < this.Height) // if player is not touching bottom of screen
            {


                if (player.hb.Bottom < this.Height)
                {
                    player.move(1 + (int)Math.Floor(Math.Pow(airDownFrames, 2) / 20));
                }

                if (upFrames > 0 && !jumping) upFrames--;
                if (airDownFrames < 12 && !grounded) airDownFrames++;

                /*
                 * (int)Math.Floor(Math.Pow(airDownFrames, 2) / 20);
                if( airDownFrames < 13) airDownFrames++;
                 */
            }
            else
            {
                grounded = true;
                airDownFrames = 0;
            }

        }
        #endregion

        #region ups
        public void standardUp()
        {
            if (up)
            {
                if (player.hb.Y > 0)
                {
                    player.move(-8);
                }
                if (airDownFrames > 0) airDownFrames -= 2;
            }
        }
        public void superJumpUp()
        {
            if (jumping && upFrames < 20)
            {
                if (player.hb.Y > 0)
                {
                    player.move(-15);
                    upFrames++;
                }

            }
            else jumping = false;
        }
        #endregion
        #region jumps
        public void standardJump()
        {
            if (jumping)
            {
                player.move(-30);
                jumping = false;
            }
        }


        #endregion
        public void standardGrounded()
        {
            if (player.hb.Bottom >= this.Height) // if player is touching bottom of screen
            {
                grounded = true;
            }
        }
    }
}
