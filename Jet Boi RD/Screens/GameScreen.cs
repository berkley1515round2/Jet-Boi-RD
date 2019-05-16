using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public static int coinScore = 0;
        int coinChance = 15;
        bool endGame = false;
        int bounce;
        bool bounceUp = true;
        #endregion
        public GameScreen()
        {
            InitializeComponent();


        }
        public void GameOver()
        {
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            tick++;
            if (endGame && tick % 30 == 0)
            {
                if (backgroundMoveSpd > 0) backgroundMoveSpd--;
                else GameOver();
            }
            if (endGame && player.y > this.Height - pheight - 50)
            {
                bounceUp = true;
                bounce /= 2;
                
            }

             if (player.y > bounce && bounceUp && endGame)
            {
                player.y -= 10;
                player.hb.Y -= 10;
                airDownFrames = 0;
            }
            else
            {
                bounceUp = false;
                
            }

            if (r.Next(0, 101) < coinChance && tick % 120 == 0)
            {
                generateCoin(r.Next(0, 3), r.Next(100, this.Height - 100));
            }
            if (tick == 180)
            {
                lasers.Add(new Classes.laser(this.Width, 10, midLaser, laserWidth));
            }
            else if (tick % timeBtwnLasers == 0)
            {
                generateLaser(player.y);
            }
            if (tick % 1200 == 0 && coinChance < 35) coinChance += 5;
            if (tick % 720 == 0)
            {

                if (backgroundMoveSpd < 20)
                {
                    backgroundMoveSpd += 2;

                }

                if (timeBtwnLasers > 30) timeBtwnLasers -= 15;
            }
            if (up)
            {
                if (player.hb.Y > 0)
                {
                    player.y -= 6;
                    player.hb.Y -= 6;
                }
                if (airDownFrames > 0) airDownFrames -= 2;
            }
            else if (player.hb.Bottom < this.Height - pheight)
            {
                if (player.hb.Bottom + 2 < this.Height)
                {
                    player.y += 2 + (int)Math.Floor(Math.Pow(airDownFrames, 2) / 10);
                    player.hb.Y += 2 + (int)Math.Floor(Math.Pow(airDownFrames, 2) / 10);
                }
                else
                {
                    player.hb.Y = this.Height - player.hb.Height;
                    player.y = this.Height - player.hb.Height;
                }
                if (airDownFrames < 10) airDownFrames++;

                /*
                 * (int)Math.Floor(Math.Pow(airDownFrames, 2) / 20);
                if( airDownFrames < 13) airDownFrames++;
                 */
            }
            else if (player.hb.Bottom >= this.Height - pheight)
            {
                grounded = true;
            }
            if (jumping)
            {
                player.y -= 30;
                player.hb.Y -= 30;
                jumping = false;
            }

            Refresh();
        }

        private void GameScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !endGame)
            {
                if (grounded) // boost
                {
                    grounded = false;
                    jumping = true;
                    up = true;
                }
                else
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
            e.Graphics.DrawString("coins " + coinScore, Font, new SolidBrush(Color.White), 0, 10);
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
