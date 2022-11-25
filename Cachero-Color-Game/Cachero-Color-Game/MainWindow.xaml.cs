﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cachero_Color_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AmazonDBDataContext amazonDB = new AmazonDBDataContext(Properties.Settings.Default.Group2_RoyalCasinoConnectionString);
        private Label[] gameColorDice = new Label[] { }; 
        private Grid gameDiceGrid = new Grid();
        private Color[] listOfColors = new Color[6];
        private int[] selectedColors = new int[] { };
        private int nextcustomerID = 0;
        private Dictionary<string, int> bettedColors = new Dictionary<string, int>();
        private Dictionary<string, int> matchedColors = new Dictionary<string, int>();     
        private List<string> prizeColors = new List<string>();
        private Regex pattern = new Regex("^[0-9]+$", RegexOptions.IgnoreCase);
        private string[] errorMessage = new string[3];

        public MainWindow()
        {
            InitializeComponent();
            initDices();
            if (uNameLbl.Content.ToString().Split(':')[1] == " ") 
            {
                confirmWagerBtn.IsEnabled = false;
                blueWagerTbx.IsEnabled = false;
                yellowWagerTbx.IsEnabled = false;
                redWagerTbx.IsEnabled = false;
                greenWagerTbx.IsEnabled = false;
                blueWagerTbx.IsEnabled = false;
                purpleWagerTbx.IsEnabled=false;
                orangeWagerTbx.IsEnabled = false;
            }
        }

        private string formatErrMessage(string[] errMess)
        {
            string errMessToShow = string.Empty;

            errMessToShow = "Error Code: " + errMess[0] + "\n" +
                "Error Name: " + errMess[1] + "\n" +
                "Error Description: " + errMess[2];

            return errMessToShow;
        }

        private void initDices() 
        {
            initDiceGrid();
            int h = 5;

            gameColorDice = new Label[3];

            for (int i = 0; i < gameColorDice.Length; i++) 
            {
                Label dice = new Label();
                dice.VerticalAlignment = VerticalAlignment.Top;
                dice.HorizontalAlignment = HorizontalAlignment.Left;
                dice.VerticalContentAlignment= VerticalAlignment.Center;
                dice.HorizontalContentAlignment= HorizontalAlignment.Center;
                dice.Width = 80;
                dice.Height = 80;
                dice.Margin = new Thickness(h,10,0,0);
                dice.BorderBrush = new SolidColorBrush(Colors.Black);
                dice.BorderThickness = new Thickness(1,1,1,1);
                switch (i) 
                {
                    case 0:
                        dice.Content = "Dice 1";
                        break;
                    case 1:
                        dice.Content = "Dice 2";
                        break;
                    case 2:
                        dice.Content = "Dice 3";
                        break;
                }
                gameColorDice[i] = dice;
                gameDiceGrid.Children.Add(gameColorDice[i]);

                h += 10 + (int)dice.Width;
            }

        }

        private void initDiceGrid() 
        {
            gameDiceGrid = new Grid();
            gameDiceGrid.HorizontalAlignment = HorizontalAlignment.Left;
            gameDiceGrid.VerticalAlignment = VerticalAlignment.Top;
            gameDiceGrid.Width = 300;
            gameDiceGrid.Height = 90;
            gameDiceGrid.Margin = new Thickness(350,200,0,0);
            mainGrid.Children.Add(gameDiceGrid);
        }

        private  async void generateColor() 
        {
            confirmWagerBtn.IsEnabled = false;
            selectedColors = new int[3];
            Random random= new Random();
            int colorIndex = 0;
            int diceCounter = 0;
            int rollCounter = 0;

            while (diceCounter < 3) 
            {
                while (rollCounter < 20) 
                {
                    await Task.Delay(50);
                    colorIndex = random.Next(1000000);
                    colorIndex %= 100000;
                    colorIndex %= 10000;
                    colorIndex %= 100;
                    colorIndex %= 6;

                    gameColorDice[diceCounter].Background = new SolidColorBrush(listOfColors[colorIndex]);              
                    rollCounter++;              
                }
                selectedColors[diceCounter] = colorIndex; 
                colorIndex = 0;
                rollCounter = 0;
                diceCounter++;
            }

            getColorMatches(selectedColors);
            getWinnerColors(matchedColors);
            confirmWagerBtn.IsEnabled = true;

        }

        private void addElColorList() 
        {
            listOfColors = new Color[6];
            listOfColors[0] = Color.FromRgb(0, 0, 255); //Color of Blue
            listOfColors[1] = Color.FromRgb(0, 255, 0); //Color of Green
            listOfColors[2] = Color.FromRgb(255, 255, 0); //Color of Yellow
            listOfColors[3] = Color.FromRgb(255, 0, 0); //Color of Red
            listOfColors[4] = Color.FromRgb(255, 140, 0); //Color of Orange
            listOfColors[5] = Color.FromRgb(255, 0, 255); // Color of Purple
        }

        private string wagerAmountVerification() 
        {
            Dictionary<string, string> forChecking = new Dictionary<string, string>();

            string redWager = redWagerTbx.Text;
            string orangeWager = orangeWagerTbx.Text;
            string yellowWager = yellowWagerTbx.Text;
            string greenWager = greenWagerTbx.Text;
            string blueWager = blueWagerTbx.Text;
            string purpleWager = purpleWagerTbx.Text;
            string errMessage = string.Empty;         

            forChecking.Add("red", redWager);
            forChecking.Add("orange", orangeWager);
            forChecking.Add("yellow", yellowWager);
            forChecking.Add("green", greenWager);
            forChecking.Add("blue", blueWager);
            forChecking.Add("purple", purpleWager);

            for (int i = 0; i < forChecking.Count; i++) 
            {
                string tempVal = forChecking.Values.ElementAt(i);
                string tempKey = forChecking.Keys.ElementAt(i);

                if (!pattern.IsMatch(tempVal))
                {
                    errMessage += $"INVALID AMOUNT: COLOR {tempKey.ToUpper()} \n\n";
                }
            }
            
            return errMessage;

        }

        private string noZeroAmountVerification() 
        {
            int[] valuesForChecking = new int[6];
            string errMess = string.Empty;
            int redWager = int.Parse(redWagerTbx.Text);
            int orangeWager = int.Parse(orangeWagerTbx.Text);
            int yellowWager = int.Parse(yellowWagerTbx.Text);
            int greenWager = int.Parse(greenWagerTbx.Text);
            int blueWager = int.Parse(blueWagerTbx.Text);
            int purpleWager = int.Parse(purpleWagerTbx.Text);
            int zeroChecker = 0;

            for (int i = 0; i < valuesForChecking.Length; i++) 
            {
                switch (i) 
                {
                    case 0:
                        valuesForChecking[i] = redWager;
                        break;
                    case 1:
                        valuesForChecking[i] = orangeWager;
                        break;
                    case 2:
                        valuesForChecking[i] = yellowWager;
                        break;
                    case 3:
                        valuesForChecking[i] = greenWager;
                        break;
                    case 4:
                        valuesForChecking[i] = blueWager;
                        break;
                    case 5:
                        valuesForChecking[i] = purpleWager;
                        break;
                }
            }

            for (int i = 0; i < valuesForChecking.Length; i++) 
            {
                if (valuesForChecking[i] < 1) 
                {
                    zeroChecker++;
                }
            }

            if (zeroChecker == 6) 
            {
                errMess = "TOTAL WAGER CANNOT BE EQUAL TO ZERO!";
            }

            return errMess;
        }

        private string balanceWagerVerification() 
        {
            decimal playerBalance = decimal.Parse(uBalanceLbl.Content.ToString().Split(':')[1]);
            int[] valuesForChecking = new int[6];
            string errMess = string.Empty;
            int redWager = int.Parse(redWagerTbx.Text);
            int orangeWager = int.Parse(orangeWagerTbx.Text);
            int yellowWager = int.Parse(yellowWagerTbx.Text);
            int greenWager = int.Parse(greenWagerTbx.Text);
            int blueWager = int.Parse(blueWagerTbx.Text);
            int purpleWager = int.Parse(purpleWagerTbx.Text);
            int totalBetChecker = 0 ;

            for (int i = 0; i < valuesForChecking.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        valuesForChecking[i] = redWager;
                        break;
                    case 1:
                        valuesForChecking[i] = orangeWager;
                        break;
                    case 2:
                        valuesForChecking[i] = yellowWager;
                        break;
                    case 3:
                        valuesForChecking[i] = greenWager;
                        break;
                    case 4:
                        valuesForChecking[i] = blueWager;
                        break;
                    case 5:
                        valuesForChecking[i] = purpleWager;
                        break;
                }
            }

            for (int i = 0; i < valuesForChecking.Length; i++)
            {
              totalBetChecker += valuesForChecking[i];
            }

            if (totalBetChecker > playerBalance) 
            {
                errMess = "INSUFFICIENT FUNDS!";
            }

            return errMess;
        }

        private void confirmWagerBtn_Click(object sender, RoutedEventArgs e)
        {
          
            var confirm = MessageBox.Show("Do you want to proceed with this action?", "Confirmation", MessageBoxButton.YesNo);
           
            if (confirm == MessageBoxResult.Yes)
            {
                if (wagerAmountVerification() != String.Empty)
                {
                    for (int i = 0; i < errorMessage.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                errorMessage[i] = "E1";
                                break;
                            case 1:
                                errorMessage[i] = "INPUT ERROR!";
                                break;
                            case 2:
                                errorMessage[i] = $"\n\n{wagerAmountVerification()}";
                                break;
                        }
                    }

                    MessageBox.Show(formatErrMessage(errorMessage));
                }
                else if (noZeroAmountVerification() != String.Empty)
                {
                    for (int i = 0; i < errorMessage.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                errorMessage[i] = "E1";
                                break;
                            case 1:
                                errorMessage[i] = "INPUT ERROR!";
                                break;
                            case 2:
                                errorMessage[i] = $"{noZeroAmountVerification()}";
                                break;
                        }
                    }

                    MessageBox.Show(formatErrMessage(errorMessage));
                }
                else if (balanceWagerVerification() != String.Empty) 
                {
                    for (int i = 0; i < errorMessage.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                errorMessage[i] = "E1";
                                break;
                            case 1:
                                errorMessage[i] = "INPUT ERROR!";
                                break;
                            case 2:
                                errorMessage[i] = $"{balanceWagerVerification()}";
                                break;
                        }
                    }

                    MessageBox.Show(formatErrMessage(errorMessage));
                }
                else
                {
                    getBettedColors();
                    for (int i = 0; i < gameColorDice.Length; i++)
                        gameColorDice[i].Background = new SolidColorBrush(Colors.Transparent);
                    addElColorList();
                    generateColor();
                }              
            }
            else
            {
                MessageBox.Show("Cancelled");
            }   
        }

        private void getColorMatches(int[] colorArr) 
        {
            prizeColors = new List<string>();
            matchedColors = new Dictionary<string, int>();
           

            for (int i = 0; i < colorArr.Length; i++) 
            {
                switch (colorArr[i]) 
                {
                    case 0:
                        prizeColors.Add("blue");
                        break;
                    case 1:
                        prizeColors.Add("green");
                        break;
                    case 2:
                        prizeColors.Add("yellow");
                        break;
                    case 3:
                        prizeColors.Add("red");
                        break;
                    case 4:
                        prizeColors.Add("orange");
                        break;
                    case 5:
                        prizeColors.Add("purple");
                        break;
                }
            }

            for (int i = 0; i < prizeColors.Count; i++) 
            {
                for (int x = 0; x < bettedColors.Count; x++) 
                {
                    if (prizeColors.ElementAt(i) == bettedColors.Keys.ElementAt(x)) 
                    {
                        string keyTemp = $"Key{x+1}:{bettedColors.Keys.ElementAt(x)}";
                        matchedColors.Add(keyTemp, bettedColors.Values.ElementAt(x));
                        keyTemp = string.Empty;
                    }
                }
            }
        }

        private void getBettedColors() 
        {
            bettedColors = new Dictionary<string, int>();
            int redWager = int.Parse(redWagerTbx.Text);
            int orangeWager = int.Parse(orangeWagerTbx.Text);
            int yellowWager = int.Parse(yellowWagerTbx.Text);
            int greenWager = int.Parse(greenWagerTbx.Text);
            int blueWager = int.Parse(blueWagerTbx.Text);
            int purpleWager = int.Parse(purpleWagerTbx.Text);
            int totalWagerValue = 0;
            decimal playerBalance = decimal.Parse(uBalanceLbl.Content.ToString().Split(':')[1]);

            if (redWager != 0 && redWager > 0) 
            {
                bettedColors.Add("red",redWager);
            }
            if (orangeWager != 0 && orangeWager > 0) 
            {
                bettedColors.Add("orange", orangeWager);
            }
            if (yellowWager != 0 && yellowWager > 0)
            {
                bettedColors.Add("yellow", yellowWager);
            }
            if (greenWager != 0 && greenWager > 0 )
            {
                bettedColors.Add("green", greenWager);
            }
            if (blueWager != 0 && blueWager > 0 )
            {
                bettedColors.Add("blue", blueWager);
            }
            if (purpleWager != 0 && purpleWager > 0 ) 
            {
                bettedColors.Add("purple", purpleWager);
            }

            for (int i = 0; i < bettedColors.Count; i++) 
            {
                totalWagerValue += bettedColors.Values.ElementAt(i);
            }

            uBalanceLbl.Content = $"Player Balance : {playerBalance - totalWagerValue}";
            MessageBox.Show($"You wagered {totalWagerValue}");
          
        }

        private void login()
        {
            lblloginstatus.Content = "Log In Status : ";
            uNameStatusLbl.Content = "Username Status : ";
            uPasswordStatuslbl.Content = "Password Status : ";

            decimal playerbalance = 0;
            string playerName = string.Empty;

            var allCustomers = (from b in amazonDB.table_Customers
                                where b.Customer_Username == txtloginUsername.Text
                                select b).ToList();

            var customerLogin = (from a in amazonDB.table_Customers
                                 where a.Customer_Username == txtloginUsername.Text
                                 select a).FirstOrDefault();

            string[] customers = new string[nextcustomerID + 1];
            string password = "";

            int y = 0;

            foreach (var customer in allCustomers)
            {
                customers[y] = customer.Customer_Username.ToString();
                y++;
            }

            if (customers.Contains(txtloginUsername.Text))
            {
                uNameStatusLbl.Content += "Username Found";
                password = customerLogin.Customer_Password.ToString();

                if (txtloginPassword.Password == password)
                {
                    lblloginstatus.Content += "Login Success";
                    uPasswordStatuslbl.Content += "Password Match!";
                    txtloginPassword.Password = string.Empty;
                    txtloginUsername.Text = string.Empty;
                    lginBtn.Content = "Log Out";
                    confirmWagerBtn.IsEnabled = true;
                    blueWagerTbx.IsEnabled = true;
                    yellowWagerTbx.IsEnabled = true;
                    redWagerTbx.IsEnabled = true;
                    greenWagerTbx.IsEnabled = true;
                    blueWagerTbx.IsEnabled = true;
                    purpleWagerTbx.IsEnabled = true;
                    orangeWagerTbx.IsEnabled = true;
                    txtloginUsername.IsEnabled = false;
                    txtloginPassword.IsEnabled = false;
                    playerbalance = decimal.Parse(customerLogin.Customer_CurrentBalance.ToString());
                    playerName = customerLogin.Customer_FirstName.ToString();

                    uBalanceLbl.Content += playerbalance.ToString();
                    uNameLbl.Content += playerName;
                }
                else
                {
                    uPasswordStatuslbl.Content += "Incorrect Password";
                    lblloginstatus.Content += "Login Failed";
                }
            }
            else
            {
                uNameStatusLbl.Content += "Username Not Found";
                lblloginstatus.Content += "Login Failed";
            }
        }

        private void lginBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (lginBtn.Content) 
            {
                case "Log In":
                    login();
                    break;

                case "Log Out":
                    logout();
                    break;
            }          
        }

        private void logout() 
        {
            string message = "Do you want to proceed with this action?";
            string caption = "LOGGING OUT";
            var confirm = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

            if (confirm == MessageBoxResult.Yes) 
            {
                txtloginUsername.IsEnabled = true;
                txtloginPassword.IsEnabled = true;
                confirmWagerBtn.IsEnabled = false;
                blueWagerTbx.IsEnabled = false;
                yellowWagerTbx.IsEnabled = false;
                redWagerTbx.IsEnabled = false;
                greenWagerTbx.IsEnabled = false;
                blueWagerTbx.IsEnabled = false;
                purpleWagerTbx.IsEnabled = false;
                orangeWagerTbx.IsEnabled = false;
                lblloginstatus.Content = "Log In Status : ";
                uNameStatusLbl.Content = "Username Status : ";
                uPasswordStatuslbl.Content = "Password Status : ";
                uNameLbl.Content = "Player Name : ";
                uBalanceLbl.Content = "Player Balance : ";
                lginBtn.Content = "Log In";
            }    
        }

        private void getWinnerColors(Dictionary<string, int> matchedColors) 
        {
            //red = x5
            //orange = x5
            //yellow = x4
            //green = x4
            //blue = x3
            //purple = x3

            decimal playerBalance = decimal.Parse(uBalanceLbl.Content.ToString().Split(':')[1]);

            int totalWinnings = 0;

            for (int i = 0; i < matchedColors.Count; i++) 
            {
                switch (matchedColors.Keys.ElementAt(i).ToString().Split(':')[1]) 
                {
                    case "red":
                        totalWinnings += matchedColors.Values.ElementAt(i) * 5;
                        break;
                    case "orange":
                        totalWinnings += matchedColors.Values.ElementAt(i) * 5;
                        break;
                    case "yellow":
                        totalWinnings += matchedColors.Values.ElementAt(i) * 4;
                        break;
                    case "green":
                        totalWinnings += matchedColors.Values.ElementAt(i) * 4;
                        break;
                    case "blue":
                        totalWinnings += matchedColors.Values.ElementAt(i) * 3;
                        break;
                    case "purple":
                        totalWinnings += matchedColors.Values.ElementAt(i) * 3;
                        break;
                }

            }

            uBalanceLbl.Content = $"Player Balance : {playerBalance + totalWinnings}";
            MessageBox.Show($"You won P {totalWinnings}");
        }
    }
}
