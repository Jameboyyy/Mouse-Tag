using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

public class mouseTagForm : Form
{
  //label declarations
  private Label title = new Label();
  private Label rodentWins = new Label();
  private Label taggedLabel = new Label();
  private Label taggedElapsed = new Label();
  private Label totalLabel = new Label();
  private Label totalElapsed = new Label();
  private Label percentLabel = new Label();
  private Label percentElapsed = new Label();

  //button declarations
  private Button startButton = new Button();
  private Button resetButton = new Button();
  private Button exitButton = new Button();

  //panel declarations
  private Panel headerPanel = new Panel();
  private Panel displayPanel = new Panel();
  private Panel controlPanel = new Panel();

  //timer tracker
  private bool isTimer = false;

  //rectangle and mouse  info
  private double rodentX, rodentY; //mouse coords
  private double rodentVelocity, rodentAngle; //velocity and angle of mouse
  private double rodentDiametre = 21.0;
  private int velocityTimeMS = 20; //time in milisecs between each velocity clock tick
  Random randSpeed = new Random();
  Random randDirect = new Random();

  //timer declarations
  System.Windows.Forms.Timer clock = new System.Windows.Forms.Timer();
  System.Windows.Forms.Timer speedClock = new System.Windows.Forms.Timer();

  //paint brush declarations
  System.Drawing.Brush rodentBrush;

  //mouse variables
  private Rectangle rodentBounds;
  private int points = 0;
  private double percentage = 0.0;
  private int totalMice = 0;
  private int maxMice = 10;

  public mouseTagForm() //start of constructor
  {
    //set text
    Text = "Mouse Tag";
    title.Text = "Mouse Tag by James Cadavona";

    rodentWins.Text = "mouse won";
    rodentWins.Font = new Font("Arial", 24);

    startButton.Text = "Start";
    resetButton.Text = "Reset";
    exitButton.Text = "Exit";

    taggedLabel.Text = "Tagged";
    taggedElapsed.Text = "0";
    totalLabel.Text = "Total";
    totalElapsed.Text = "0";
    percentLabel.Text = "Percent";
    percentElapsed.Text = "0.0";

    //set sizing
    Size = new Size(900, 730);
    headerPanel.Size = new Size(Size.Width, Size.Height / 10);
    displayPanel.Size = new Size(Size.Width, 7 * Size.Height / 10);
    controlPanel.Size = new Size(Size.Width, 2 * Size.Height / 10);

    title.Size = new Size(headerPanel.Width, headerPanel.Height);

    rodentWins.Size = new Size(displayPanel.Width, displayPanel.Height);

    startButton.Size = new Size(controlPanel.Width / 12, controlPanel.Height / 2);
    resetButton.Size = new Size(startButton.Width, startButton.Height);
    exitButton.Size = new Size(startButton.Width, startButton.Height);

    taggedLabel.Size = new Size(startButton.Width, startButton.Height / 2);
    taggedElapsed.Size = new Size(taggedLabel.Width, taggedLabel.Height);

    totalLabel.Size = new Size(taggedLabel.Width, taggedLabel.Height);
    totalElapsed.Size = new Size(taggedLabel.Width, taggedLabel.Height);

    percentLabel.Size = new Size(taggedLabel.Width, taggedLabel.Height);
    percentElapsed.Size = new Size(taggedLabel.Width, taggedLabel.Height);

    //alignment and disable autosizing
    title.AutoSize = false;
    title.TextAlign = ContentAlignment.MiddleCenter;
    rodentWins.AutoSize = false;
    rodentWins.TextAlign = ContentAlignment.MiddleCenter;
    taggedLabel.AutoSize = false;
    taggedLabel.TextAlign = ContentAlignment.MiddleCenter;
    totalLabel.AutoSize = false;
    totalLabel.TextAlign = ContentAlignment.MiddleCenter;
    percentLabel.AutoSize = false;
    percentLabel.TextAlign = ContentAlignment.MiddleCenter;

    //fixing borders
    headerPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
    displayPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
    controlPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

    //set locations
    headerPanel.Location = new Point(0, 0);
    displayPanel.Location = new Point(0, 1 * Size.Height / 10);
    controlPanel.Location = new Point(0, 8 * Size.Height / 10);

    title.Location = new Point(0, 0);

    rodentWins.Location = new Point(0, 0);

    startButton.Location = new Point(controlPanel.Width / 16, controlPanel.Height / 6);
    resetButton.Location = new Point(11 * controlPanel.Width / 16, controlPanel.Height / 6);
    exitButton.Location = new Point(14 * controlPanel.Width / 17, controlPanel.Height / 6);

    taggedLabel.Location = new Point(4 * controlPanel.Width / 16, controlPanel.Height / 6);
    taggedElapsed.Location = new Point(4 * controlPanel.Width / 16, 3 * controlPanel.Height / 7);

    totalLabel.Location = new Point(6 * controlPanel.Width / 16, controlPanel.Height / 6);
    totalElapsed.Location = new Point(6 * controlPanel.Width / 16, 3 * controlPanel.Height / 7);

    percentLabel.Location = new Point(8 * controlPanel.Width / 16, controlPanel.Height / 6);
    percentElapsed.Location = new Point(8 * controlPanel.Width / 16, 3 * controlPanel.Height / 7);

    //set time stuff - intervals and refresh rodentes
    clock.Interval = 33;
    speedClock.Interval = velocityTimeMS;

    //set balls coords
    rodentX = displayPanel.Width / 2;
    rodentY = displayPanel.Height / 2;

    //set backcolours
    headerPanel.BackColor = Color.LightSkyBlue;
    displayPanel.BackColor = Color.AliceBlue;
    controlPanel.BackColor = Color.LightCyan;

    startButton.BackColor = Color.CornflowerBlue;
    resetButton.BackColor = Color.Plum;
    exitButton.BackColor = Color.Fuchsia;

    taggedLabel.BackColor = Color.Coral;
    taggedElapsed.BackColor = Color.LightCoral;

    totalLabel.BackColor = Color.Salmon;
    totalElapsed.BackColor = Color.LightSalmon;

    percentLabel.BackColor = Color.MediumOrchid;
    percentElapsed.BackColor = Color.LightPink;

    rodentBrush = System.Drawing.Brushes.Gray;

    //enabling UI items
    title.Enabled = false;

    rodentWins.Enabled = false;
    rodentWins.Visible = false;

    startButton.Enabled = true;
    resetButton.Enabled = true;
    exitButton.Enabled = true;

    taggedLabel.Enabled = false;
    taggedElapsed.Enabled = false;

    totalLabel.Enabled = false;
    totalElapsed.Enabled = false;

    percentLabel.Enabled = false;
    percentElapsed.Enabled = false;

    //adding everything to the form
    Controls.Add(headerPanel);
    headerPanel.Controls.Add(title);

    Controls.Add(displayPanel);
    displayPanel.Controls.Add(rodentWins);

    Controls.Add(controlPanel);
    controlPanel.Controls.Add(startButton);
    controlPanel.Controls.Add(resetButton);
    controlPanel.Controls.Add(exitButton);

    controlPanel.Controls.Add(taggedLabel);
    controlPanel.Controls.Add(taggedElapsed);

    controlPanel.Controls.Add(totalLabel);
    controlPanel.Controls.Add(totalElapsed);

    controlPanel.Controls.Add(percentLabel);
    controlPanel.Controls.Add(percentElapsed);

    //setting eveyrthing to work,, tick, paint, click, mouse
    clock.Tick += new EventHandler(Clock_Tick);
    speedClock.Tick += new EventHandler(Speed_Tick);

    displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(Display_Paint);
    displayPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(Mouse_Click);

    startButton.Click += new EventHandler(Start_Click);
    resetButton.Click += new EventHandler(Reset_Click);
    exitButton.Click += new EventHandler(Exit_Click);

    //centre UI to user screen
    CenterToScreen();
  }//end of constructor

  //when startButton is clicked
  protected void Start_Click(Object obj, EventArgs evt)
  {
    if(isTimer)
    {
      //stop the clocks
      clock.Stop();
      speedClock.Stop();

      isTimer = false;

      //change the startButton appearance
      startButton.Text = "Resume";
      startButton.BackColor = Color.CornflowerBlue;

      displayPanel.Refresh();
    }
    else
    {
      rodentVelocity = randSpeed.Next(20, 500);
      rodentAngle = randDirect.Next(10, 360);

      isTimer = true;

      //change startButton appearance
      startButton.Text = "Pause";
      startButton.BackColor = Color.BlueViolet;

      //start the clocks
      clock.Start();
      speedClock.Start();
    }
  }//end of Start_Click

  //when resetButton is clicked
  protected void Reset_Click(Object obj, EventArgs evt)
  {
    //stop the clocks
    clock.Stop();
    speedClock.Stop();

    isTimer = false;

    //change startButton appearance to original
    startButton.Text = "Start";
    startButton.BackColor = Color.CornflowerBlue;

    //reset the rodent
    rodentX = displayPanel.Width / 2;
    rodentY = displayPanel.Height / 2;
    rodentBrush = System.Drawing.Brushes.Gray;

    this.Refresh();
    displayPanel.Refresh();
    rodentWins.Visible = false;

    percentage = 100.0 * ((double)points / (double)totalMice);
    percentElapsed.Text = percentage.ToString();

    if(maxMice == totalMice)
      gameOver();
  }//end of Reset_Click

  //when exitButton is clicked
  protected void Exit_Click(Object obj, EventArgs evt)
  {
    Close();
  }//end of Exit_Click

  //refreshes controlPanel and displayPanel based on each tick
  protected void Clock_Tick(Object sender, EventArgs ev)
  {
    //refresh displayPanel
    displayPanel.Refresh();

    percentElapsed.Text = percentage.ToString();

    //refresh controlPanel
    controlPanel.Refresh();
  }//end of Clock_Tick

  //does stuff
  protected void Speed_Tick(Object sender, EventArgs ev)
  {
    //calculate xDistance and yDistance using trig formulas
    double xDistance = Math.Cos(rodentAngle * Math.PI / 180.0) * (rodentVelocity / (1000.0 / velocityTimeMS));
    double yDistance = Math.Sin(rodentAngle * Math.PI / 180.0) * (rodentVelocity / (1000.0 / velocityTimeMS));

    //update rodent coords according to the distances calculations above
    rodentX += xDistance;
    rodentY += yDistance;

    //go to loser function if rodent passes the bounds
    if((rodentX + rodentDiametre) > Convert.ToDouble(displayPanel.Width) || rodentX < 0)
      _ = LoserX();
    if(rodentY < 0 || (rodentY + rodentDiametre) > Convert.ToDouble(displayPanel.Height))
      _ = LoserY();
  }//end of Speed_Tick

  //this function is executed when the player loses anbd the rodent leaves the screen
  protected async Task LoserX()
  {
    //stop the clocks
    clock.Stop();
    speedClock.Stop();

    isTimer = false;

    //display the phrase "mouse won"
    rodentWins.Visible = true;

    totalMice++;

    totalElapsed.Text = totalMice.ToString();
    taggedElapsed.Text = points.ToString();

    percentage = 100.0 * ((double)points / (double)totalMice);
    percentElapsed.Text = percentage.ToString();

    //reset the game by clicking the reset button
    if(rodentWins.Visible)
    {
      System.Console.WriteLine("nooo");
      //have the program wait 3 seconds before clicking the reset button
      await Task.Delay(3000);
      resetButton.PerformClick();
    }
  }

  //this function is executed when the player loses anbd the rodent leaves the screen
  protected async Task LoserY()
  {
    //stop the clocks
    clock.Stop();
    speedClock.Stop();

    isTimer = false;

    //display the phrase "mouse won"
    rodentWins.Visible = true;

    totalMice++;

    totalElapsed.Text = totalMice.ToString();
    taggedElapsed.Text = points.ToString();

    percentage = 100.0 * ((double)points / (double)totalMice);
    percentElapsed.Text = percentage.ToString();

    //reset the game by clicking the reset button
    if(rodentWins.Visible)
    {
      System.Console.WriteLine("nooo");
      //have the program wait 3 seconds before clicking the reset button
      await Task.Delay(3000);
      resetButton.PerformClick();
    }

  }

  protected void winner()
  {
    points++;
    totalMice++;

    totalElapsed.Text = totalMice.ToString();
    taggedElapsed.Text = points.ToString();

    resetButton.PerformClick();
  }

  protected void gameOver()
  {
    startButton.Enabled = false;
    resetButton.Enabled = false;

    if(points > 5 || points == totalMice)
    {
      rodentWins.Visible = true;
      rodentWins.Text = "Over 50% of mice! You Win!";
    }
    else if(points == 5)
    {
      rodentWins.Visible = true;
      rodentWins.Text = "Caught 50% of mice. Draw!";
    }
    else if (points < 5)
    {
      rodentWins.Visible = true;
      rodentWins.Text = "Under 50% of mice. You lose.";
    }
  }

  protected void Mouse_Click(Object sender, System.Windows.Forms.MouseEventArgs e)
  {
    if(isTimer && rodentBounds.Contains(e.Location))
    {
      System.Console.WriteLine("YES!");
      winner();
    }
  }

  //paints the rodent in the displayPanel
  protected void Display_Paint(Object sender, System.Windows.Forms.PaintEventArgs e)
  {
    //create the graphing tool
    Graphics graph = e.Graphics;

    //get actual coords of the rodent
    int xActual = Convert.ToInt32(Math.Ceiling(rodentX) - (rodentDiametre / 2.0));
    int yActual = Convert.ToInt32(Math.Ceiling(rodentY) - (rodentDiametre / 2.0));

    rodentBounds = new Rectangle(xActual, yActual, Convert.ToInt32(rodentDiametre), Convert.ToInt32(rodentDiametre));

    rodentBrush = System.Drawing.Brushes.Gray;
    graph.FillEllipse(rodentBrush, rodentBounds);

  }//end of Display_Paint

}//end of mouseTagForm Class
